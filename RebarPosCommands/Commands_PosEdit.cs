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
            EditPosForm form = new EditPosForm();

            if (form.Init(id, pt))
            {
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
