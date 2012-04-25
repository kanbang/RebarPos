using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class RebarPosCommands
    {
        private bool PosEdit(ObjectId id, Point3d pt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                    if (pos == null) return false;
                    PosShape shape = tr.GetObject(pos.ShapeId, OpenMode.ForRead) as PosShape;
                    if (shape == null) return false;

                    EditPosForm form = new EditPosForm();
                    form.SetPos(pos, shape);
                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
                    {
                        ;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }
    }
}
