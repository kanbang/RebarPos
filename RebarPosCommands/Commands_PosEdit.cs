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
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                    if (pos == null) return false;
                    PosShape shape = tr.GetObject(pos.ShapeId, OpenMode.ForRead) as PosShape;
                    if (shape == null) return false;
                    PosGroup group = tr.GetObject(pos.GroupId, OpenMode.ForRead) as PosGroup;
                    if (group == null) return false;
                    Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> groups = new Dictionary<string, ObjectId>();
                    Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> shapes = new Dictionary<string, ObjectId>();
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    if (namedDict.Contains(PosGroup.TableName))
                    {
                        DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosGroup.TableName), OpenMode.ForRead);
                        DbDictionaryEnumerator it = dict.GetEnumerator();
                        while (it.MoveNext())
                        {
                            groups.Add(it.Key, it.Value);
                        }
                    }
                    if (namedDict.Contains(PosShape.TableName))
                    {
                        DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosShape.TableName), OpenMode.ForRead);
                        DbDictionaryEnumerator it = dict.GetEnumerator();
                        while (it.MoveNext())
                        {
                            shapes.Add(it.Key, it.Value);
                        }
                    }

                    EditPosForm form = new EditPosForm();
                    form.SetPos(pos, group, groups, shape, shapes);
                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
                    {
                        ;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }
    }
}
