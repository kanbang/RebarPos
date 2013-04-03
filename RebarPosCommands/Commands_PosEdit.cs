using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private bool PosEdit(ObjectId id, Point3d pt)
        {
            bool detached = false;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                    if (pos == null) return false;

                    detached = pos.Detached;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (detached)
            {
                using (EditDetachedPosForm form = new EditDetachedPosForm())
                {
                    if (form.Init(id, pt))
                    {
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                using (EditPosForm form = new EditPosForm())
                {
                    if (form.Init(id, pt))
                    {
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
