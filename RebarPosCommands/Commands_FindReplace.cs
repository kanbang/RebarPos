using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private bool FindReplace(bool usePickSet)
        {
            FindReplaceForm form = new FindReplaceForm();

            if (form.Init(CurrentGroupId, usePickSet))
            {
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Çizimde poz bulunamadı.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }
    }
}
