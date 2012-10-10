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

            if (form.Init())
            {
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == DialogResult.OK)
                {
                    SetCurrentGroup();
                }
            }
        }

        public void SetCurrentGroup()
        {
            ObjectId id = PosGroup.GroupId;
            string name = "";
            Autodesk.AutoCAD.Colors.Color countColor = new Autodesk.AutoCAD.Colors.Color();
            ObjectId defpointsLayer = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup item = tr.GetObject(id, OpenMode.ForRead) as PosGroup;
                    if (item == null) return;
                    name = item.Name;
                    countColor = item.CountColor;
                    defpointsLayer = item.HiddenLayerId;
                }
                catch
                {
                    ;
                }
            }

            CurrentGroupId = id;
            CurrentGroupName = name;

            CountOverrule.Instance.SetProperties(countColor, defpointsLayer);

            DWGUtility.RefreshAllPos();
        }
    }
}
