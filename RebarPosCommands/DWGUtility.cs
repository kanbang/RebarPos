using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;

namespace RebarPosCommands
{
    public static class DWGUtility
    {
        private static SelectionFilter SSPosFilter()
        {
            TypedValue[] tvs = new TypedValue[] {
                new TypedValue((int)DxfCode.Start, "REBARPOS")
            };
            return new SelectionFilter(tvs);
        }

        public static PromptSelectionResult SelectPosUser()
        {
            return Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(SSPosFilter());
        }

        public static PromptSelectionResult SelectAllPos()
        {
            return Application.DocumentManager.MdiActiveDocument.Editor.SelectAll(SSPosFilter());
        }

        public static ObjectId CreateDefaultShapes()
        {
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    DBDictionary dict = null;
                    if (!namedDict.Contains(PosShape.TableName))
                    {
                        dict = new DBDictionary();
                        namedDict.UpgradeOpen();
                        namedDict.SetAt(PosShape.TableName, dict);
                        namedDict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(dict, true);
                    }
                    else
                    {
                        dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosShape.TableName), OpenMode.ForRead);
                    }

                    if (dict.Count == 0)
                    {
                        PosShape shape = new PosShape();
                        shape.Name = "Duz Demir";
                        shape.Fields = 1;
                        shape.Formula = "A";
                        shape.FormulaBending = "A";
                        shape.Items.AddLine(0, 0, 100, 0, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1));
                        shape.Items.AddText(50, 5, 10, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2));
                        dict.UpgradeOpen();
                        id = dict.SetAt("*", shape);
                        dict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(shape, true);
                    }
                    else
                    {
                        foreach (DBDictionaryEntry entry in dict)
                        {
                            id = entry.Value;
                            break;
                        }
                    }

                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos");
                }

                return id;
            }
        }

        public static ObjectId CreateDefaultGroups()
        {
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    DBDictionary dict = null;
                    if (!namedDict.Contains(PosGroup.TableName))
                    {
                        dict = new DBDictionary();
                        namedDict.UpgradeOpen();
                        namedDict.SetAt(PosGroup.TableName, dict);
                        namedDict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(dict, true);
                    }
                    else
                    {
                        dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosGroup.TableName), OpenMode.ForRead);
                    }

                    if (dict.Count == 0)
                    {
                        PosGroup group = new PosGroup();
                        group.Name = "0";
                        group.Formula = "[M:C][N][\"T\":D][\"/\":S][\" L=\":L]";
                        group.FormulaWithoutLength = "[M:C][N][\"T\":D][\"/\":S]";
                        group.FormulaPosOnly = "[M:C]";
                        group.TextStyleId = DWGUtility.CreateTextStyle("Rebar Text Style", "leroy.shx", 0.7);
                        group.NoteStyleId = DWGUtility.CreateTextStyle("Rebar Note Style", "simplxtw.shx", 0.9);
                        group.Current = true;
                        dict.UpgradeOpen();
                        id = dict.SetAt("*", group);
                        dict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(group, true);
                    }
                    else
                    {
                        foreach (DBDictionaryEntry entry in dict)
                        {
                            id = entry.Value;
                            break;
                        }
                    }

                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos");
                }

                return id;
            }
        }

        // Creates a new text style and returns the ObjectId of the new style 
        public static ObjectId CreateTextStyle(string name, string filename, double scale)
        {
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    TextStyleTable table = (TextStyleTable)tr.GetObject(db.TextStyleTableId, OpenMode.ForRead);
                    if (!table.Has(name))
                    {
                        TextStyleTableRecord style = new TextStyleTableRecord();
                        style.Name = name;
                        style.FileName = filename;
                        style.XScale = scale;
                        table.UpgradeOpen();
                        id = table.Add(style);
                        table.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(style, true);
                    }
                    else
                    {
                        id = table[name];
                    }

                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos");
                }
            }

            return id;
        }

        // Returns all text styles
        public static Dictionary<string, ObjectId> GetTextStyles()
        {
            Dictionary<string, ObjectId> list=new Dictionary<string,ObjectId>();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    TextStyleTable table = (TextStyleTable)tr.GetObject(db.TextStyleTableId, OpenMode.ForRead);
                    SymbolTableEnumerator it = table.GetEnumerator();
                    while(it.MoveNext())
                    {
                        ObjectId id= it.Current;
                        TextStyleTableRecord style = (TextStyleTableRecord)tr.GetObject(id, OpenMode.ForRead);
                        list.Add(style.Name,id);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos");
                }
            }

            return list;               
        }

        // Returns all items in the dictionary.
        public static Dictionary<string, ObjectId> GetGroups()
        {
            Dictionary<string, ObjectId> list = new Dictionary<string, ObjectId>();
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
                                PosGroup item = (PosGroup)tr.GetObject(it.Value, OpenMode.ForRead);
                                list.Add(item.Name, it.Value);
                            }
                        }
                    }
                }
                catch
                {
                    ;
                }
            }
            return list;
        }

        // Returns all items in the dictionary.
        public static Dictionary<string, ObjectId> GetShapes()
        {
            Dictionary<string, ObjectId> list = new Dictionary<string, ObjectId>();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    if (namedDict.Contains(PosShape.TableName))
                    {
                        DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosShape.TableName), OpenMode.ForRead);
                        using (DbDictionaryEnumerator it = dict.GetEnumerator())
                        {
                            while (it.MoveNext())
                            {
                                PosShape item = (PosShape)tr.GetObject(it.Value, OpenMode.ForRead);
                                list.Add(item.Name, it.Value);
                            }
                        }
                    }
                }
                catch
                {
                    ;
                }
            }
            return list;
        }

        // Refreshes all items in group
        public static void RefreshPosInGroup(ObjectId id)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                    using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            RebarPos pos = tr.GetObject(it.Current, OpenMode.ForWrite) as RebarPos;
                            if (pos != null && pos.GroupId == id)
                            {
                                pos.Update();
                            }
                        }
                    }
                    tr.Commit();
                }
                catch
                {
                    ;
                }
            }
        }
    }
}
