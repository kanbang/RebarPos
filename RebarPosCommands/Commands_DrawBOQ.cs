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
        private void DrawBOQ()
        {
            DrawBOQForm form = new DrawBOQForm();

            if (!form.Init(CurrentGroupId))
                return;

            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) != System.Windows.Forms.DialogResult.OK)
                return;

            if (form.PosList.Count == 0)
            {
                MessageBox.Show("Seçilen grupta poz mevcut değil.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PromptPointResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetPoint("Baz noktası: ");
            if (result.Status != PromptStatus.OK)
                return;

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

                    table.Heading = form.TableHeader;
                    table.Footing = form.TableFooter;
                    table.Multiplier = form.Multiplier;
                    table.StyleId = form.TableStyle;
                    table.MaxHeight = form.TableHeight;
                    table.TableSpacing = form.TableMargin;

                    // Add rows
                    foreach (PosCopy copy in form.PosList)
                    {
                        table.Items.Add(int.Parse(copy.pos), copy.count, double.Parse(copy.diameter), copy.length1, copy.length2, copy.isVarLength, copy.shapeId);
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
        }
    }
}
