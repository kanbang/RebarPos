using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Geometry;

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

        public static PromptSelectionResult SelectAllPosUser()
        {
            try
            {
                return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(SSPosFilter());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static PromptSelectionResult SelectAllPos()
        {
            try
            {
                return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SelectAll(SSPosFilter());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static ObjectId[] GetAllPos()
        {
            List<ObjectId> list = new List<ObjectId>();
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
                            if (it.Current.ObjectClass == Autodesk.AutoCAD.Runtime.RXObject.GetClass(typeof(RebarPos)))
                            {
                                list.Add(it.Current);
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
            return list.ToArray();
        }

        public static ObjectId[] GetPosWithShape(string shape)
        {
            List<ObjectId> list = new List<ObjectId>();
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
                            RebarPos pos = tr.GetObject(it.Current, OpenMode.ForRead) as RebarPos;
                            if (pos != null)
                            {
                                if (pos.Shape == shape)
                                {
                                    list.Add(it.Current);
                                }
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
            return list.ToArray();
        }

        public static ObjectId[] GetAllTables()
        {
            List<ObjectId> list = new List<ObjectId>();
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
                            if (it.Current.ObjectClass == Autodesk.AutoCAD.Runtime.RXObject.GetClass(typeof(BOQTable)))
                            {
                                list.Add(it.Current);
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
            return list.ToArray();
        }

        public static ObjectId[] GetTableWithStyle(ObjectId styleId)
        {
            List<ObjectId> list = new List<ObjectId>();
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
                            BOQTable table = tr.GetObject(it.Current, OpenMode.ForRead) as BOQTable;
                            if (table != null)
                            {
                                if (table.StyleId == styleId)
                                {
                                    list.Add(it.Current);
                                }
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
            return list.ToArray();
        }

        // Creates default BOQ styles
        public static ObjectId CreateDefaultBOQStyles()
        {
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    DBDictionary dict = null;
                    if (!namedDict.Contains(BOQStyle.TableName))
                    {
                        dict = new DBDictionary();
                        namedDict.UpgradeOpen();
                        namedDict.SetAt(BOQStyle.TableName, dict);
                        namedDict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(dict, true);
                    }
                    else
                    {
                        dict = (DBDictionary)tr.GetObject(namedDict.GetAt(BOQStyle.TableName), OpenMode.ForRead);
                    }

                    if (dict.Count == 0)
                    {
                        BOQStyle styleen = new BOQStyle();

                        styleen.Name = "TableStyle - EN";
                        styleen.Columns = "[M][N][D][L][SH][TL]";

                        styleen.TextStyleId = PosUtility.CreateTextStyle("Rebar BOQ Style", "romanstw.shx", 0.7);
                        styleen.HeadingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Heading Style", "Arial.ttf", 1.0);
                        styleen.FootingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Footing Style", "simplxtw.shx", 1.0);

                        styleen.Precision = 2;

                        styleen.PosLabel = "POS";
                        styleen.CountLabel = "NO.";
                        styleen.DiameterLabel = "DIA";
                        styleen.LengthLabel = "LENGTH\\P([U])";
                        styleen.ShapeLabel = "SHAPE";
                        styleen.TotalLengthLabel = "TOTAL LENGTH (m)";
                        styleen.DiameterListLabel = "T[D]";
                        styleen.DiameterLengthLabel = "TOTAL LENGTH (m)";
                        styleen.UnitWeightLabel = "UNIT WEIGHT (kg/m)";
                        styleen.WeightLabel = "WEIGHT (kg)";
                        styleen.GrossWeightLabel = "TOTAL WEIGHT (kg)";
                        styleen.MultiplierHeadingLabel = "BOQ CALCULATED FOR [N] COMPLETES";

                        dict.UpgradeOpen();
                        id = dict.SetAt("*", styleen);
                        dict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(styleen, true);

                        BOQStyle styletr = new BOQStyle();

                        styletr.Name = "TableStyle - TR";
                        styletr.Columns = "[M][N][D][L][SH][TL]";

                        styletr.TextStyleId = PosUtility.CreateTextStyle("Rebar BOQ Style", "romanstw.shx", 0.7);
                        styletr.HeadingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Heading Style", "Arial.ttf", 1.0);
                        styletr.FootingStyleId = PosUtility.CreateTextStyle("Rebar BOQ Footing Style", "simplxtw.shx", 1.0);

                        styletr.Precision = 2;

                        styletr.PosLabel = "POZ";
                        styletr.CountLabel = "ADET";
                        styletr.DiameterLabel = "ÇAP";
                        styletr.LengthLabel = "BOY\\P([U])";
                        styletr.ShapeLabel = "DEMİR ŞEKLİ";
                        styletr.TotalLengthLabel = "TOPLAM BOY (m)";
                        styletr.DiameterListLabel = "T[D]";
                        styletr.DiameterLengthLabel = "TOPLAM BOY (m)";
                        styletr.UnitWeightLabel = "BIRIM AGIRLIK (kg/m)";
                        styletr.WeightLabel = "TOPLAM AGIRLIK (kg)";
                        styletr.GrossWeightLabel = "GENEL TOPLAM (kg)";
                        styletr.MultiplierHeadingLabel = "GENEL TOPLAM [N] ADET İÇİNDİR";

                        dict.UpgradeOpen();
                        id = dict.SetAt("*", styletr);
                        dict.DowngradeOpen();
                        tr.AddNewlyCreatedDBObject(styletr, true);
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
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                return id;
            }
        }

        // Returns all text styles
        public static Dictionary<string, ObjectId> GetTextStyles()
        {
            Dictionary<string, ObjectId> list = new Dictionary<string, ObjectId>();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    TextStyleTable table = (TextStyleTable)tr.GetObject(db.TextStyleTableId, OpenMode.ForRead);
                    SymbolTableEnumerator it = table.GetEnumerator();
                    while (it.MoveNext())
                    {
                        ObjectId id = it.Current;
                        TextStyleTableRecord style = (TextStyleTableRecord)tr.GetObject(id, OpenMode.ForRead);
                        list.Add(style.Name, id);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return list;
        }

        // Returns all items in the dictionary.
        public static Dictionary<string, ObjectId> GetTableStyles()
        {
            Dictionary<string, ObjectId> list = new Dictionary<string, ObjectId>();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    if (namedDict.Contains(BOQStyle.TableName))
                    {
                        DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(BOQStyle.TableName), OpenMode.ForRead);
                        using (DbDictionaryEnumerator it = dict.GetEnumerator())
                        {
                            while (it.MoveNext())
                            {
                                BOQStyle item = (BOQStyle)tr.GetObject(it.Value, OpenMode.ForRead);
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

        // Refreshes given items
        public static void RefreshPos(IEnumerable<ObjectId> ids)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId posid in ids)
                    {
                        RebarPos pos = tr.GetObject(posid, OpenMode.ForWrite) as RebarPos;
                        pos.Update();
                        pos.Draw();
                    }

                    tr.Commit();
                }
                catch
                {
                    ;
                }
            }
        }

        // Refreshes all items with the given shape
        public static void RefreshPosWithShape(string name)
        {
            RefreshPos(GetPosWithShape(name));
        }

        // Refreshes all items
        public static void RefreshAllPos()
        {
            RefreshPos(GetAllPos());
        }

        // Refreshes given items
        public static void RefreshTable(IEnumerable<ObjectId> ids)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId posid in ids)
                    {
                        BOQTable table = tr.GetObject(posid, OpenMode.ForWrite) as BOQTable;
                        table.Update();
                        table.Draw();
                    }

                    tr.Commit();
                }
                catch
                {
                    ;
                }
            }
        }

        // Refreshes all items with given style
        public static void RefreshTableWithStyle(ObjectId id)
        {
            RefreshTable(GetTableWithStyle(id));
        }

        // Refreshes all items
        public static void RefreshAllTables()
        {
            RefreshTable(GetAllTables());
        }

        // Regenerates the drawing window
        public static void Regen()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.Regen();
        }

        // Gets the model space background color
        public static System.Drawing.Color ModelBackgroundColor()
        {
            try
            {
                AcadPreferences pref = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
                uint indexColor = pref.Display.GraphicsWinModelBackgrndColor;
                return System.Drawing.ColorTranslator.FromOle((int)indexColor);
            }
            catch
            {
                return System.Drawing.Color.Black;
            }
        }

        // Zooms to given objects
        public static void ZoomToObjects(IEnumerable<ObjectId> ids)
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Extents3d outerext = new Extents3d();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId id in ids)
                    {
                        Entity ent = tr.GetObject(id, OpenMode.ForRead) as Entity;
                        Extents3d ext = ent.GeometricExtents;
                        outerext.AddExtents(ext);
                    }
                }
                catch
                {
                    ;
                }
            }

            outerext.TransformBy(ed.CurrentUserCoordinateSystem.Inverse());
            Point2d min2d = new Point2d(outerext.MinPoint.X, outerext.MinPoint.Y);
            Point2d max2d = new Point2d(outerext.MaxPoint.X, outerext.MaxPoint.Y);

            ViewTableRecord view = new ViewTableRecord();

            view.CenterPoint = min2d + ((max2d - min2d) / 2.0);
            view.Height = max2d.Y - min2d.Y;
            view.Width = max2d.X - min2d.X;

            ed.SetCurrentView(view);
        }

        // Returns the list of standard diamaters
        public static List<int> GetStandardDiameters()
        {
            List<int> standardDiameters = new List<int>();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(PosGroup.GroupId, OpenMode.ForRead) as PosGroup;
                    if (group != null)
                    {
                        foreach (string ds in group.StandardDiameters.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            int d;
                            if (int.TryParse(ds, out d))
                            {
                                standardDiameters.Add(d);
                            }
                        }
                    }
                }
                catch
                {
                    ;
                }
            }
            return standardDiameters;
        }

        // Returns the maximum bar length in m
        public static double GetMaximumBarLength()
        {
            double len = 0;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(PosGroup.GroupId, OpenMode.ForRead) as PosGroup;
                    if (group != null)
                    {
                        len = group.MaxBarLength;
                    }
                }
                catch
                {
                    ;
                }
            }
            return len;
        }
    }
}
