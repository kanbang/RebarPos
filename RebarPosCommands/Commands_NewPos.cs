using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void NewPos()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointResult result = ed.GetPoint("Baz noktası: ");
            if (result.Status == PromptStatus.OK)
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                        Point3d pt = result.Value;
                        RebarPos pos = new RebarPos();
                        pos.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                        pos.TransformBy(Matrix3d.Scaling(25.0, pt));

                        pos.Pos = "";
                        pos.Count = "1";
                        pos.Diameter = "12";
                        pos.Spacing = "";
                        pos.Shape = "GENEL";
                        pos.A = "1000";
                        pos.Note = "";

#if DEBUG
                        pos.Pos = "1";
                        pos.Count = "2x3";
                        pos.Diameter = "12";
                        pos.Spacing = "200";
                        pos.Shape = "00";
                        pos.A = "1000";
                        pos.Note = "";
#endif

                        pos.SetDatabaseDefaults(db);

                        btr.AppendEntity(pos);
                        tr.AddNewlyCreatedDBObject(pos, true);

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
