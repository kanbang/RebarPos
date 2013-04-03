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
            using (GroupForm form = new GroupForm())
            {
                if (form.Init())
                {
                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == DialogResult.OK)
                    {
                        SetCurrentGroup();
                    }
                }
            }
        }

        public void SetCurrentGroup()
        {
            Autodesk.AutoCAD.Colors.Color countColor = new Autodesk.AutoCAD.Colors.Color();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup item = tr.GetObject(PosGroup.GroupId, OpenMode.ForRead) as PosGroup;
                    if (item == null) return;
                    countColor = item.CountColor;
                }
                catch
                {
                    ;
                }
            }

            CountOverrule.Instance.SetProperties(countColor);

            DWGUtility.RefreshAllPos();
        }
    }
}
