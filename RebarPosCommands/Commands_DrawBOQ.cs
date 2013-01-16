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
        public class BOQStyle
        {
            public string Name;
            public string Columns;

            public ObjectId TextStyleId;
            public ObjectId HeadingStyleId;
            public ObjectId FootingStyleId;

            public string PosLabel;
            public string CountLabel;
            public string DiameterLabel;
            public string LengthLabel;
            public string ShapeLabel;
            public string TotalLengthLabel;
            public string DiameterListLabel;
            public string DiameterLengthLabel;
            public string UnitWeightLabel;
            public string WeightLabel;
            public string GrossWeightLabel;
            public string MultiplierHeadingLabel;

            private BOQStyle()
            {
                ;
            }

            public override string ToString()
            {
                return Name;
            }

            public static List<BOQStyle> GetStyles()
            {
                List<BOQStyle> list = new List<BOQStyle>();

                BOQStyle styletr = new BOQStyle();

                styletr.Name = "TableStyle - TR";
                styletr.Columns = "[M][N][D][L][SH][TL]";

                styletr.TextStyleId = PosUtility.CreateTextStyle("Rebar BOQ Style", "romanstw.shx", 0.7);
                styletr.HeadingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Heading Style", "Arial.ttf", 1.0);
                styletr.FootingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Footing Style", "simplxtw.shx", 1.0);

                styletr.PosLabel = "POZ";
                styletr.CountLabel = "ADET";
                styletr.DiameterLabel = "ÇAP";
                styletr.LengthLabel = "BOY\\P([U])";
                styletr.ShapeLabel = "DEMİR ŞEKLİ";
                styletr.TotalLengthLabel = "TOPLAM BOY (m)";
                styletr.DiameterListLabel = "[TD]";
                styletr.DiameterLengthLabel = "TOPLAM BOY (m)";
                styletr.UnitWeightLabel = "BIRIM AGIRLIK (kg/m)";
                styletr.WeightLabel = "TOPLAM AGIRLIK (kg)";
                styletr.GrossWeightLabel = "GENEL TOPLAM (kg)";
                styletr.MultiplierHeadingLabel = "GENEL TOPLAM [N] ADET İÇİNDİR";
                
                list.Add(styletr);

                BOQStyle styleen = new BOQStyle();

                styleen.Name = "TableStyle - EN";
                styleen.Columns = "[M][N][D][L][SH][TL]";

                styleen.TextStyleId = PosUtility.CreateTextStyle("Rebar BOQ Style", "romanstw.shx", 0.65);
                styleen.HeadingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Heading Style", "Arial.ttf", 1.0);
                styleen.FootingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Footing Style", "simplxtw.shx", 1.0);

                styleen.PosLabel = "POS";
                styleen.CountLabel = "NO.";
                styleen.DiameterLabel = "DIA";
                styleen.LengthLabel = "LENGTH\\P([U])";
                styleen.ShapeLabel = "SHAPE";
                styleen.TotalLengthLabel = "TOTAL LENGTH (m)";
                styleen.DiameterListLabel = "[TD]";
                styleen.DiameterLengthLabel = "TOTAL LENGTH (m)";
                styleen.UnitWeightLabel = "UNIT WEIGHT (kg/m)";
                styleen.WeightLabel = "WEIGHT (kg)";
                styleen.GrossWeightLabel = "TOTAL WEIGHT (kg)";
                styleen.MultiplierHeadingLabel = "BOQ CALCULATED FOR [N] COMPLETES";

                list.Add(styleen);

                return list;
            }
        }

        private bool DrawBOQ()
        {
            DrawBOQForm form = new DrawBOQForm();

            // Pos error check
            PromptSelectionResult sel = DWGUtility.SelectAllPosUser();
            if (sel.Status != PromptStatus.OK) return false;
            ObjectId[] items = sel.Value.GetObjectIds();

            List<PosCheckResult> errors = PosCheckResult.CheckAllInSelection(items, true, false);
            List<PosCheckResult> warnings = PosCheckResult.CheckAllInSelection(items, false, true);

            if (errors.Count != 0) PosCheckResult.ConsoleOut(errors);
            if (warnings.Count != 0) PosCheckResult.ConsoleOut(warnings);

            if (errors.Count != 0)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.DisplayTextScreen = true;
                return false;
            }

            // Pos similarity check
            if (warnings.Count != 0)
            {
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
                    table.SuspendUpdate();

                    Point3d pt = result.Value;
                    table.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                    table.TransformBy(Matrix3d.Scaling(form.TextHeight, pt));
                    table.Note = form.TableNote;
                    table.Heading = form.TableHeader;
                    table.Footing = form.TableFooter;

                    table.DisplayUnit = form.DisplayUnit;
                    table.Precision = form.Precision;

                    table.Multiplier = form.Multiplier;

                    BOQStyle style = form.TableStyle;
                    table.ColumnDef = style.Columns;

                    table.TextStyleId = style.TextStyleId;
                    table.HeadingStyleId = style.HeadingStyleId;
                    table.FootingStyleId = style.FootingStyleId;

                    table.PosLabel = style.PosLabel;
                    table.CountLabel = style.CountLabel;
                    table.DiameterLabel = style.DiameterLabel;
                    table.LengthLabel = style.LengthLabel;
                    table.ShapeLabel = style.ShapeLabel;
                    table.TotalLengthLabel = style.TotalLengthLabel;
                    table.DiameterListLabel = style.DiameterListLabel;
                    table.DiameterLengthLabel = style.DiameterLengthLabel;
                    table.UnitWeightLabel = style.UnitWeightLabel;
                    table.WeightLabel = style.WeightLabel;
                    table.GrossWeightLabel = style.GrossWeightLabel;
                    table.MultiplierHeadingLabel = style.MultiplierHeadingLabel;

                    table.MaxRows = form.TableRows;
                    table.TableSpacing = form.TableMargin;

                    double lengthScale = 1.0;
                    switch (table.DisplayUnit)
                    {
                        case BOQTable.DrawingUnits.Millimeter:
                            lengthScale = 1.0;
                            break;
                        case BOQTable.DrawingUnits.Centimeter:
                            lengthScale = 0.1;
                            break;
                        case BOQTable.DrawingUnits.Decimeter:
                            lengthScale = 0.01;
                            break;
                        case BOQTable.DrawingUnits.Meter:
                            lengthScale = 0.001;
                            break;
                    }

                    // Add rows
                    foreach (PosCopy copy in posList)
                    {
                        if (copy.existing)
                        {
                            string a = string.Empty;
                            string b = string.Empty;
                            string c = string.Empty;
                            string d = string.Empty;
                            string e = string.Empty;
                            string f = string.Empty;

                            if (copy.isVarA)
                                a = (copy.minA * lengthScale).ToString("F0") + "~" + (copy.maxA * lengthScale).ToString("F0");
                            else
                                a = (copy.minA * lengthScale).ToString("F0");
                            if (copy.isVarB)
                                b = (copy.minB * lengthScale).ToString("F0") + "~" + (copy.maxB * lengthScale).ToString("F0");
                            else
                                b = (copy.minB * lengthScale).ToString("F0");
                            if (copy.isVarC)
                                c = (copy.minC * lengthScale).ToString("F0") + "~" + (copy.maxC * lengthScale).ToString("F0");
                            else
                                c = (copy.minC * lengthScale).ToString("F0");
                            if (copy.isVarD)
                                d = (copy.minD * lengthScale).ToString("F0") + "~" + (copy.maxD * lengthScale).ToString("F0");
                            else
                                d = (copy.minD * lengthScale).ToString("F0");
                            if (copy.isVarE)
                                e = (copy.minE * lengthScale).ToString("F0") + "~" + (copy.maxE * lengthScale).ToString("F0");
                            else
                                e = (copy.minE * lengthScale).ToString("F0");
                            if (copy.isVarF)
                                f = (copy.minF * lengthScale).ToString("F0") + "~" + (copy.maxF * lengthScale).ToString("F0");
                            else
                                f = (copy.minF * lengthScale).ToString("F0");

                            table.Items.Add(int.Parse(copy.pos), copy.count, double.Parse(copy.diameter), copy.length1, copy.length2, copy.isVarLength, copy.shapename, a, b, c, d, e, f);
                        }
                        else
                        {
                            table.Items.Add(int.Parse(copy.pos));
                        }
                    }

                    table.ResumeUpdate();

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
