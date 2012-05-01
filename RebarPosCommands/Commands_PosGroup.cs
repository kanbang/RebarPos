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
                    SetCurrentGroup(form.CurrentId);
                }
            }
        }

        public void SetCurrentGroup(ObjectId id)
        {
            ObjectId oldid = CurrentGroupId;
            string name = "";

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    if (namedDict.Contains(PosGroup.TableName))
                    {
                        DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosGroup.TableName), OpenMode.ForRead);
                        using (DbDictionaryEnumerator it = dict.GetEnumerator())
                        {
                            while (it.MoveNext())
                            {
                                if (it.Value == oldid || it.Value == id)
                                {
                                    PosGroup item = (PosGroup)tr.GetObject(it.Value, OpenMode.ForWrite);
                                    item.Current = (it.Value == id);
                                    if (it.Value == id) name = item.Name;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    ;
                }
            }

            CurrentGroupId = id;
            CurrentGroupName = name;

            DWGUtility.RefreshPosInGroup(oldid);
            DWGUtility.RefreshPosInGroup(id);
        }
    }
}
