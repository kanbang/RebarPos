using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;
using Autodesk.AutoCAD.EditorInput;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private bool DrawBOQ()
        {
            DrawBOQForm form = new DrawBOQForm();

            // Pos error check
            PromptSelectionResult sel = DWGUtility.SelectAllPosUser();
            if (sel.Status != PromptStatus.OK) return false;
            ObjectId[] items = sel.Value.GetObjectIds();

            List<PosCheckResult> check = PosCheckResult.CheckAllInSelection(items, true, false);
            if (check.Count != 0)
            {
                PosCheckResult.ConsoleOut(check);
                Autodesk.AutoCAD.ApplicationServices.Application.DisplayTextScreen = true;
                return false;
            }

            // Pos similarity check
            List<PosCheckResult> checks = PosCheckResult.CheckAllInSelection(items, false, true);
            if (checks.Count != 0)
            {
                PosCheckResult.ConsoleOut(checks);
                Autodesk.AutoCAD.ApplicationServices.Application.DisplayTextScreen = true;
                PromptKeywordOptions opts = new PromptKeywordOptions("\nMetraja devam edilsin mi? [Evet/Hayir]", "Yes No");
                PromptResult res = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetKeywords(opts);
                if (res.Status != PromptStatus.OK || res.StringResult == "No")
                {
                    return true;
                }
            }

            if (!form.Init())
                return false;

            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) != System.Windows.Forms.DialogResult.OK)
                return true;

            List<PosCopy> posList = new List<PosCopy>();
            try
            {
                posList = PosCopy.ReadAllInSelection(items, true, PosCopy.PosGrouping.PosMarker);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (posList.Count == 0)
            {
                MessageBox.Show("Seçilen grupta poz mevcut değil.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            posList = RemoveEmpty(posList);
            if (!form.HideMissing)
            {
                posList = AddMissing(posList);
            }
            posList = SortList(posList);

            PromptPointResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetPoint("Baz noktası: ");
            if (result.Status != PromptStatus.OK)
                return true;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    BOQTable table = new BOQTable();

                    Point3d pt = result.Value;
                    table.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                    table.TransformBy(Matrix3d.Scaling(form.TextHeight, pt));
                    table.Note = form.TableNote;
                    table.Heading = form.TableHeader;
                    table.Footing = form.TableFooter;
                    table.Multiplier = form.Multiplier;
                    table.StyleId = form.TableStyle;
                    table.MaxHeight = form.TableHeight;
                    table.TableSpacing = form.TableMargin;

                    // Add rows
                    foreach (PosCopy copy in posList)
                    {
                        if (copy.existing)
                            table.Items.Add(int.Parse(copy.pos), copy.count, double.Parse(copy.diameter), copy.length1, copy.length2, copy.isVarLength, copy.shapename, copy.a, copy.b, copy.c, copy.d, copy.e, copy.f);
                        else
                            table.Items.Add(int.Parse(copy.pos));
                    }

                    table.SetDatabaseDefaults(db);

                    btr.AppendEntity(table);
                    tr.AddNewlyCreatedDBObject(table, true);

                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }

        private List<PosCopy> AddMissing(List<PosCopy> list)
        {
            list = RemoveEmpty(list);

            int lastpos = 0;
            foreach (PosCopy copy in list)
            {
                int posno;
                if (int.TryParse(copy.pos, out posno))
                {
                    lastpos = Math.Max(lastpos, posno);
                }
            }
            for (int i = 1; i <= lastpos; i++)
            {
                if (!list.Exists(p => p.pos == i.ToString()))
                {
                    PosCopy copy = new PosCopy();
                    copy.pos = i.ToString();
                    list.Add(copy);
                }
            }

            return SortList(list);
        }

        private List<PosCopy> RemoveEmpty(List<PosCopy> list)
        {
            list.RemoveAll(p => p.existing == false);
            return list;
        }

        private List<PosCopy> SortList(List<PosCopy> list)
        {
            list.Sort(new CompareByPosNumber());
            return list;
        }

        private class CompareByPosNumber : IComparer<PosCopy>
        {
            public int Compare(PosCopy e1, PosCopy e2)
            {
                int p1 = 0;
                int p2 = 0;
                int.TryParse(e1.pos, out p1);
                int.TryParse(e2.pos, out p2);

                return (p1 == p2 ? 0 : (p1 < p2 ? -1 : 1));
            }
        }
    }
}
