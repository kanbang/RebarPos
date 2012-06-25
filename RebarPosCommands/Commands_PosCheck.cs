using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void PosCheck()
        {
            CheckForm form = new CheckForm();

            if (form.Init(CurrentGroupId))
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false);
            }
        }
    }
}
