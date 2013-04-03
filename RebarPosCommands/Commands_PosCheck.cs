using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.EditorInput;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void PosCheck()
        {
            PromptSelectionResult sel = DWGUtility.SelectAllPosUser(true);
            if (sel.Status != PromptStatus.OK) return;
            ObjectId[] items = sel.Value.GetObjectIds();

            using (CheckForm form = new CheckForm())
            {
                if (form.Init(items))
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false);
                }
            }
        }
    }
}
