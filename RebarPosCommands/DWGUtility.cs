using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;

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
                        shape.Fields = 1;
                        shape.Formula = "A";
                        shape.FormulaBending = "A";
                        shape.Items.AddLine(0, 0, 100, 0, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1));
                        dict.UpgradeOpen();
                        id = dict.SetAt("Duz Demir", shape);
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

        public static ObjectId CreateDefaultStyles()
        {
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    DBDictionary dict = null;
                    if (!namedDict.Contains(PosStyle.TableName))
                    {
                        dict = new DBDictionary();
                        namedDict.UpgradeOpen();
                        namedDict.SetAt(PosStyle.TableName, dict);
                        namedDict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(dict, true);
                    }
                    else
                    {
                        dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosStyle.TableName), OpenMode.ForRead);
                    }

                    if (dict.Count == 0)
                    {
                        PosStyle style = new PosStyle();
                        style.Formula = "[MC][N][DT][D][DS][S:0] L=[L:0]";
                        style.FormulaWithoutLength = "[MC][N][DT][D][DS][S:0]";
                        style.FormulaPosOnly = "[MC]";
                        style.TextStyleId = DWGUtility.CreateTextStyle("Rebar Text Style", "leroy.shx", 0.7);
                        style.NoteStyleId = DWGUtility.CreateTextStyle("Rebar Note Style", "simplxtw.shx", 0.9);
                        dict.UpgradeOpen();
                        id = dict.SetAt("Standard", style);
                        dict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(style, true);
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
                        group.StyleId = DWGUtility.CreateDefaultStyles();
                        dict.UpgradeOpen();
                        id = dict.SetAt("0", group);
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
    }
}
