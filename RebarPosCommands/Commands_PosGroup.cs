using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void PosGroups()
        {
            GroupForm form = new GroupForm();

            if (form.Init(CurrentGroupId))
            {
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == DialogResult.OK)
                {
                    CurrentGroupId = form.CurrentId;
                    CurrentGroupName = form.CurrentName;
                }
            }
        }
    }
}
