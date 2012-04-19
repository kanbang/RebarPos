using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class RebarPosCommands
    {
        private void NewPos()
        {
            PromptPointResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetPoint("Baz noktası: ");
            if (result.Status == PromptStatus.OK)
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        //Open the block table record model space for write
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                        Point3d pt = result.Value;
                        RebarPos pos = new RebarPos();
                        pos.BasePoint = pt;
                        pos.Pos = "Test";
                        pos.A = "100";
                        pos.B = "20";
                        pos.SetDatabaseDefaults(db);

                        btr.AppendEntity(pos);
                        tr.AddNewlyCreatedDBObject(pos, true);

                        tr.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "RebarPos");
                    }
                }
            }
        }
    }
}
