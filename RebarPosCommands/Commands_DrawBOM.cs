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
        private void DrawBOM()
        {
            PromptPointResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetPoint("Baz noktası: ");
            if (result.Status == PromptStatus.OK)
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                        Point3d pt = result.Value;
                        BOQTable table = new BOQTable();
                        table.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                        table.TransformBy(Matrix3d.Scaling(25.0, pt));
                        table.Items.Add(1, 6, 26, 1000, 2000, true, ObjectId.Null);
                        table.Items.Add(2, 2, 16, 1000, ObjectId.Null);
                        table.Items.Add(3, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(4, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(5, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(6, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(7, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(8, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(9, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(10, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(11, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(12, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(13, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(14, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(15, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(16, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(17, 3, 22, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(18, 3, 20, 1300.345435435345, ObjectId.Null);
                        table.Items.Add(19, 3, 26, 1300.345435435345, ObjectId.Null);
                        table.StyleId = DWGUtility.CreateDefaultBOQStyles();

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
}
