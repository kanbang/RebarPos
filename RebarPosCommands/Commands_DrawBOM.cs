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
                        table.Items.Add(1, 6, 12, 1000, 2000, true, ObjectId.Null);
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
