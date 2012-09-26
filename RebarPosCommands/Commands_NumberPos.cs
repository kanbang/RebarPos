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
        private bool NumberPos()
        {
            // Pos error check
            PromptSelectionResult sel = DWGUtility.SelectAllPosUser();
            if (sel.Status != PromptStatus.OK) return false;
            ObjectId[] items = sel.Value.GetObjectIds();

            List<PosCheckResult> check = PosCheckResult.CheckAllInSelection(CurrentGroupId,items, PosCheckResult.CheckType.Errors);
            if (check.Count != 0)
            {
                PosCheckResult.ConsoleOut(check);
                Autodesk.AutoCAD.ApplicationServices.Application.DisplayTextScreen = true;
                return false;
            }

            NumberingForm form = new NumberingForm();

            if (form.Init(items))
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
