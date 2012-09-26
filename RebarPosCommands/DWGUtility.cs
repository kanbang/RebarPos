using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;

using OZOZ.RebarPosWrapper;
using System.Windows.Forms;

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

        private static SelectionFilter SSPosGroupFilter(ObjectId groupId)
        {
            TypedValue[] tvs = new TypedValue[] {
                new TypedValue((int)DxfCode.Start, "REBARPOS"),
                new TypedValue((int)DxfCode.HardPointerId + 1, groupId.Handle)
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

        public static PromptSelectionResult SelectGroupUser(ObjectId groupid)
        {
            try
            {
                return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(SSPosGroupFilter(groupid));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static PromptSelectionResult SelectGroup(ObjectId groupid)
        {
            try
            {
                return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SelectAll(SSPosGroupFilter(groupid));
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

        public static ObjectId[] GetPosInGroup(ObjectId groupId)
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
                                if (pos.GroupId == groupId)
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

        public static ObjectId[] GetPosWithShape(ObjectId shapeId)
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
                                if (pos.ShapeId == shapeId)
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

        // Creates default shapes
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
                        dict.UpgradeOpen();

                        PosShape shape0 = new PosShape();
                        shape0.Name = "GENEL";
                        shape0.Fields = 1;
                        shape0.Formula = "A";
                        shape0.FormulaBending = "A";
                        dict.SetAt("*", shape0);
                        tr.AddNewlyCreatedDBObject(shape0, true);

                        PosShape shape1 = new PosShape();
                        shape1.Name = "25";
                        shape1.Fields = 5;
                        shape1.Formula = "A+B+E";
                        shape1.FormulaBending = "A+B+E";
                        shape1.Items.AddText(20717.7498047594, 5522.18008204755, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape1.Items.AddText(20933.1937269272, 5520.55678864965, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape1.Items.AddText(20612.0785105111, 5528.3165675784, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape1.Items.AddText(21039.7419376633, 5527.13639615375, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape1.Items.AddText(20829.0222151811, 5521.03628752322, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape1.Items.AddLine(21069.8323573148, 5573.24724177817, 21069.8323573148, 5498.24724177817, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape1.Items.AddLine(20594.8323573148, 5498.24724177817, 20594.8323573148, 5573.24724177817, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape1.Items.AddLine(20594.8323573148, 5573.24724177817, 21069.8323573148, 5573.24724177817, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape1.Items.AddLine(21069.8323573148, 5498.24724177817, 20594.8323573148, 5498.24724177817, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape1.Items.AddLine(21031.4966822258, 5522.96933819009, 21029.6857128588, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21029.6857128588, 5514.36962085657, 21031.4966822258, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21027.8747434917, 5522.96933819009, 21029.6857128588, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21029.6857128588, 5514.36962085657, 21027.8747434917, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21031.4966822258, 5548.50917587865, 21029.6857128588, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21029.6857128588, 5557.10889321217, 21031.4966822258, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21027.8747434917, 5548.50917587865, 21029.6857128588, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21029.6857128588, 5557.10889321217, 21027.8747434917, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20636.789971138, 5522.96933819009, 20634.9790017709, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20634.9790017709, 5514.36962085657, 20636.789971138, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20633.1680324038, 5522.96933819009, 20634.9790017709, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20634.9790017709, 5514.36962085657, 20633.1680324038, 5522.96933819009, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20636.789971138, 5548.50917587865, 20634.9790017709, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20634.9790017709, 5557.10889321217, 20636.789971138, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20633.1680324038, 5548.50917587865, 20634.9790017709, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20634.9790017709, 5557.10889321217, 20633.1680324038, 5548.50917587865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20629.3041946974, 5557.10889321218, 20707.0944372144, 5557.10889321218, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20629.3041946974, 5514.36962085656, 20748.3829142087, 5514.36962085656, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20634.9790017709, 5557.10889321218, 20634.9790017709, 5514.36962085656, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20957.5458986749, 5557.10889321218, 21035.3605199323, 5557.10889321218, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20916.5982891588, 5514.36962085656, 21035.3605199323, 5514.36962085656, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(21029.6857128588, 5557.10889321218, 21029.6857128588, 5514.36962085656, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape1.Items.AddLine(20764.1122593208, 5514.36962085656, 20717.3323573148, 5557.10889321218, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape1.Items.AddLine(20900.5524553088, 5514.36962085656, 20947.3323573148, 5557.10889321218, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape1.Items.AddLine(20900.5524553088, 5514.36962085656, 20764.1122593208, 5514.36962085656, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape1);
                        tr.AddNewlyCreatedDBObject(shape1, true);

                        PosShape shape2 = new PosShape();
                        shape2.Name = "26";
                        shape2.Fields = 4;
                        shape2.Formula = "A+B+C";
                        shape2.FormulaBending = "A+B+C";
                        shape2.Items.AddText(20769.3012038966, 5383.68661066055, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape2.Items.AddText(20863.234739259, 5381.2808415814, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape2.Items.AddText(20909.4847690985, 5401.62719759021, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape2.Items.AddText(21039.7419376633, 5393.27485145212, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape2.Items.AddLine(21069.8323573148, 5360.8975649155, 20594.8323573148, 5360.8975649155, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape2.Items.AddLine(20594.8323573148, 5360.8975649155, 20594.8323573148, 5435.8975649155, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape2.Items.AddLine(20594.8323573148, 5435.8975649155, 21069.8323573148, 5435.8975649155, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape2.Items.AddLine(21069.8323573148, 5435.8975649155, 21069.8323573148, 5360.8975649155, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape2.Items.AddLine(21031.4966822258, 5385.61966132742, 21029.6857128588, 5385.61966132742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21029.6857128588, 5377.01994399389, 21031.4966822258, 5385.61966132742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21027.8747434917, 5385.61966132742, 21029.6857128588, 5385.61966132742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21029.6857128588, 5377.01994399389, 21027.8747434917, 5385.61966132742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21031.4966822258, 5417.45618453089, 21029.6857128588, 5417.45618453089, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21029.6857128588, 5426.05590186442, 21031.4966822258, 5417.45618453089, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21027.8747434917, 5417.45618453089, 21029.6857128588, 5417.45618453089, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21029.6857128588, 5426.05590186442, 21027.8747434917, 5417.45618453089, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(20886.8115839697, 5426.05590186443, 20947.3323573148, 5426.05590186442, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape2.Items.AddLine(20957.5458986749, 5426.05590186443, 21035.3605199323, 5426.05590186443, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(20869.0495159302, 5377.01994399388, 21035.3605199323, 5377.01994399388, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(21029.6857128588, 5426.05590186443, 21029.6857128588, 5377.01994399388, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape2.Items.AddLine(20886.8115839697, 5426.05590186443, 20823.8535504096, 5377.01994399388, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape2.Items.AddLine(20823.8535504096, 5377.01994399388, 20717.3323573148, 5377.01994399388, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape2);
                        tr.AddNewlyCreatedDBObject(shape2, true);

                        PosShape shape3 = new PosShape();
                        shape3.Name = "27";
                        shape3.Fields = 4;
                        shape3.Formula = "A+B+C";
                        shape3.FormulaBending = "A+B+C-0.5*r-d";
                        shape3.Items.AddText(20756.2258511996, 5239.69992523917, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape3.Items.AddText(20854.3468778571, 5257.22089374146, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape3.Items.AddText(20927.8964245794, 5250.85675087367, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape3.Items.AddText(20613.2811076487, 5255.69178387009, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape3.Items.AddLine(21069.8323573148, 5223.54788805282, 20594.8323573148, 5223.54788805282, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape3.Items.AddLine(20594.8323573148, 5298.54788805282, 21069.8323573148, 5298.54788805282, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape3.Items.AddLine(20594.8323573148, 5223.54788805282, 20594.8323573148, 5298.54788805282, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape3.Items.AddLine(21069.8323573148, 5298.54788805282, 21069.8323573148, 5223.54788805282, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape3.Items.AddLine(20636.789971138, 5254.41031835159, 20634.9790017709, 5254.41031835159, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20634.9790017709, 5245.81060101807, 20636.789971138, 5254.41031835159, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20633.1680324038, 5254.41031835159, 20634.9790017709, 5254.41031835159, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20634.9790017709, 5245.81060101807, 20633.1680324038, 5254.41031835159, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20636.789971138, 5272.63169425966, 20634.9790017709, 5272.63169425965, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20634.9790017709, 5281.23141159318, 20636.789971138, 5272.63169425966, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20633.1680324038, 5272.63169425966, 20634.9790017709, 5272.63169425965, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20634.9790017709, 5281.23141159318, 20633.1680324038, 5272.63169425966, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20947.3323573148, 5282.06921644305, 20947.3323573148, 5249.42231651116, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape3.Items.AddLine(20782.0553056053, 5282.06921644305, 20947.3323573148, 5282.06921644305, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape3.Items.AddLine(20629.3041946974, 5281.23141159319, 20762.4229028474, 5281.23141159319, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20629.3041946974, 5245.81060101806, 20705.7979630397, 5245.81060101806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20634.9790017709, 5281.23141159319, 20634.9790017709, 5245.81060101806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape3.Items.AddLine(20782.0553056053, 5282.06921644305, 20717.3323573148, 5245.81060101806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape3);
                        tr.AddNewlyCreatedDBObject(shape3, true);

                        PosShape shape4 = new PosShape();
                        shape4.Name = "28";
                        shape4.Fields = 4;
                        shape4.Formula = "A+B+C";
                        shape4.FormulaBending = "A+B+C-0.5*r-d";
                        shape4.Items.AddText(20747.982769555, 5115.06982380911, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape4.Items.AddText(20863.101937189, 5130.9890699, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape4.Items.AddText(20959.8051360696, 5135.62795238237, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape4.Items.AddText(21039.7419376633, 5101.35701899384, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape4.Items.AddLine(20594.8323573148, 5086.19821119015, 20594.8323573148, 5161.19821119015, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape4.Items.AddLine(21069.8323573148, 5161.19821119015, 21069.8323573148, 5086.19821119015, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape4.Items.AddLine(20594.8323573148, 5161.19821119015, 21069.8323573148, 5161.19821119015, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape4.Items.AddLine(21069.8323573148, 5086.19821119015, 20594.8323573148, 5086.19821119015, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape4.Items.AddLine(21031.4966822258, 5106.8954151235, 21029.6857128588, 5106.8954151235, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21029.6857128588, 5098.29569778998, 21031.4966822258, 5106.8954151235, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21027.8747434917, 5106.8954151235, 21029.6857128588, 5106.8954151235, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21029.6857128588, 5098.29569778998, 21027.8747434917, 5106.8954151235, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21031.4966822258, 5118.03016942307, 21029.6857128588, 5118.03016942307, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21029.6857128588, 5126.6298867566, 21031.4966822258, 5118.03016942307, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21027.8747434917, 5118.03016942307, 21029.6857128588, 5118.03016942307, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21029.6857128588, 5126.6298867566, 21027.8747434917, 5118.03016942307, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(20947.3323573148, 5152.80357007483, 20947.3323573148, 5126.62988675661, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape4.Items.AddLine(20807.5536222422, 5126.62988675661, 20947.3323573148, 5126.62988675661, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape4.Items.AddLine(20961.9997972566, 5126.62988675661, 21035.3605199323, 5126.62988675661, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(20773.5002230095, 5098.29569778998, 21035.3605199323, 5098.29569778998, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(21029.6857128588, 5126.62988675661, 21029.6857128588, 5098.29569778998, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape4.Items.AddLine(20807.5536222422, 5126.62988675661, 20717.3323573148, 5099.13350263984, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape4);
                        tr.AddNewlyCreatedDBObject(shape4, true);

                        PosShape shape5 = new PosShape();
                        shape5.Name = "29";
                        shape5.Fields = 4;
                        shape5.Formula = "A+B+C";
                        shape5.FormulaBending = "A+B+C-r-2*d";
                        shape5.Items.AddText(20819.1168810615, 4971.63758007253, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape5.Items.AddText(20922.5830814409, 4985.5185963256, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape5.Items.AddText(20764.5361030172, 4989.82909077029, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape5.Items.AddText(21039.7419376633, 4981.78070878506, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape5.Items.AddLine(21069.8323573148, 4948.84853432748, 20594.8323573148, 4948.84853432748, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape5.Items.AddLine(20594.8323573148, 4948.84853432748, 20594.8323573148, 5023.84853432748, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape5.Items.AddLine(21069.8323573148, 5023.84853432748, 21069.8323573148, 4948.84853432748, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape5.Items.AddLine(20594.8323573148, 5023.84853432748, 21069.8323573148, 5023.84853432748, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape5.Items.AddLine(21031.4966822258, 4973.57063073939, 21029.6857128588, 4973.57063073939, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21029.6857128588, 4964.97091340586, 21031.4966822258, 4973.57063073939, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21027.8747434917, 4973.57063073939, 21029.6857128588, 4973.57063073939, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21029.6857128588, 4964.97091340586, 21027.8747434917, 4973.57063073939, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21031.4966822258, 5005.41100866001, 21029.6857128588, 5005.41100866001, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21029.6857128588, 5014.01072599354, 21031.4966822258, 5005.41100866001, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21027.8747434917, 5005.41100866001, 21029.6857128588, 5005.41100866001, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21029.6857128588, 5014.01072599354, 21027.8747434917, 5005.41100866001, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(20863.4560621213, 5014.01072599354, 20717.3323573148, 5014.01072599354, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape5.Items.AddLine(20891.4783940126, 5014.01072599354, 21035.3605199323, 5014.01072599355, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(20957.5458986749, 4964.97091340586, 21035.3605199323, 4964.97091340586, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(21029.6857128588, 5014.01072599355, 21029.6857128588, 4964.97091340586, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape5.Items.AddLine(20863.4560621213, 5014.01072599354, 20947.3323573148, 4964.97091340586, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape5.Items.AddLine(20947.3323573148, 4964.97091340586, 20717.7415299843, 4964.97091340586, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape5);
                        tr.AddNewlyCreatedDBObject(shape5, true);

                        PosShape shape6 = new PosShape();
                        shape6.Name = "31";
                        shape6.Fields = 4;
                        shape6.Formula = "A+B+C+D";
                        shape6.FormulaBending = "A+B+C+D-1.5*r-3*d";
                        shape6.Items.AddText(20746.9588839227, 4845.82337891265, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape6.Items.AddText(20702.1673988151, 4846.57388244142, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape6.Items.AddText(20828.4507866097, 4834.28790320985, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape6.Items.AddText(20955.1584107996, 4844.20278212954, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape6.Items.AddLine(21069.8323573148, 4811.4988574648, 20594.8323573148, 4811.4988574648, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape6.Items.AddLine(20594.8323573148, 4811.4988574648, 20594.8323573148, 4886.4988574648, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape6.Items.AddLine(21069.8323573148, 4886.4988574648, 21069.8323573148, 4811.4988574648, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape6.Items.AddLine(20594.8323573148, 4886.4988574648, 21069.8323573148, 4886.4988574648, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape6.Items.AddLine(20717.3323573148, 4870.36050889881, 20778.420923031, 4870.36050889881, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape6.Items.AddLine(20717.3323573148, 4827.62123654319, 20717.3323573148, 4870.36050889881, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape6.Items.AddLine(20947.3323573148, 4827.62123654319, 20947.3323573148, 4869.37855316834, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape6.Items.AddLine(20947.3323573148, 4827.62123654319, 20717.3323573148, 4827.62123654319, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape6);
                        tr.AddNewlyCreatedDBObject(shape6, true);

                        PosShape shape7 = new PosShape();
                        shape7.Name = "32";
                        shape7.Fields = 4;
                        shape7.Formula = "A+B+C+D";
                        shape7.FormulaBending = "A+B+C+D-1.5*r-3*d";
                        shape7.Items.AddText(20746.3265738785, 4710.0824458417, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape7.Items.AddText(20702.1673988151, 4709.22420557875, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape7.Items.AddText(20828.4507866097, 4711.58012559201, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape7.Items.AddText(20955.1584107996, 4688.41000297117, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape7.Items.AddLine(20594.8323573148, 4674.14918060213, 20594.8323573148, 4749.14918060213, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape7.Items.AddLine(21069.8323573148, 4674.14918060213, 20594.8323573148, 4674.14918060213, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape7.Items.AddLine(20594.8323573148, 4749.14918060213, 21069.8323573148, 4749.14918060213, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape7.Items.AddLine(21069.8323573148, 4749.14918060213, 21069.8323573148, 4674.14918060213, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape7.Items.AddLine(20717.3323573148, 4733.01083203614, 20778.420923031, 4733.01083203614, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape7.Items.AddLine(20717.3323573148, 4704.91345892534, 20717.3323573148, 4733.01083203614, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape7.Items.AddLine(20947.3323573148, 4685.40394375628, 20947.3323573148, 4704.91345892534, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape7.Items.AddLine(20947.3323573148, 4704.91345892534, 20717.3323573148, 4704.91345892534, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape7);
                        tr.AddNewlyCreatedDBObject(shape7, true);

                        PosShape shape8 = new PosShape();
                        shape8.Name = "34";
                        shape8.Fields = 5;
                        shape8.Formula = "A+B+C+E";
                        shape8.FormulaBending = "A+B+C+E-0.5*r-d";
                        shape8.Items.AddText(20736.9223029118, 4560.42635433437, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape8.Items.AddText(20814.3448460552, 4557.04362850571, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape8.Items.AddText(20886.6429959264, 4577.30924037233, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape8.Items.AddText(21039.7419376633, 4569.17679027608, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape8.Items.AddText(20955.1584107996, 4576.04785663263, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape8.Items.AddLine(21031.4966822258, 4561.52160015137, 21029.6857128588, 4561.52160015137, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(20594.8323573148, 4536.79950373945, 20594.8323573148, 4611.79950373945, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape8.Items.AddLine(21069.8323573148, 4536.79950373945, 20594.8323573148, 4536.79950373945, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape8.Items.AddLine(21069.8323573148, 4611.79950373945, 21069.8323573148, 4536.79950373945, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape8.Items.AddLine(20594.8323573148, 4611.79950373945, 21069.8323573148, 4611.79950373945, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape8.Items.AddLine(21029.6857128588, 4552.92188281785, 21031.4966822258, 4561.52160015137, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21027.8747434917, 4561.52160015137, 21029.6857128588, 4561.52160015137, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21029.6857128588, 4552.92188281785, 21027.8747434917, 4561.52160015137, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21031.4966822258, 4593.35812335485, 21029.6857128588, 4593.35812335485, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21029.6857128588, 4601.95784068837, 21031.4966822258, 4593.35812335485, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21027.8747434917, 4593.35812335485, 21029.6857128588, 4593.35812335485, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21029.6857128588, 4601.95784068837, 21027.8747434917, 4593.35812335485, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(20765.6551056517, 4553.7596876677, 20717.3323573148, 4553.7596876677, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape8.Items.AddLine(20947.3323573148, 4602.79564553824, 20947.3323573148, 4570.14874560635, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape8.Items.AddLine(20846.3980713582, 4602.79564553824, 20947.3323573148, 4602.79564553824, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape8.Items.AddLine(20957.5458986749, 4601.95784068838, 21035.3605199323, 4601.95784068838, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(20821.8139418151, 4552.92188281784, 21035.3605199323, 4552.92188281784, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(21029.6857128588, 4601.95784068838, 21029.6857128588, 4552.92188281784, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape8.Items.AddLine(20846.3980713582, 4602.79564553824, 20765.6551056517, 4553.7596876677, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape8);
                        tr.AddNewlyCreatedDBObject(shape8, true);

                        PosShape shape9 = new PosShape();
                        shape9.Name = "35";
                        shape9.Fields = 5;
                        shape9.Formula = "A+B+C+E";
                        shape9.FormulaBending = "A+B+C+E-0.5*r-d";
                        shape9.Items.AddText(20755.2252857343, 4418.18837532205, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape9.Items.AddText(20816.4601428336, 4431.0574763203, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape9.Items.AddText(20902.0308463051, 4447.24655103148, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape9.Items.AddText(21038.9598580424, 4417.03088661006, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape9.Items.AddText(20961.7420320002, 4447.87366495971, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape9.Items.AddLine(20594.8323573148, 4399.44982687678, 20594.8323573148, 4474.44982687678, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape9.Items.AddLine(21069.8323573148, 4474.44982687678, 21069.8323573148, 4399.44982687678, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape9.Items.AddLine(21069.8323573148, 4399.44982687678, 20594.8323573148, 4399.44982687678, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape9.Items.AddLine(20594.8323573148, 4474.44982687678, 21069.8323573148, 4474.44982687678, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape9.Items.AddLine(21031.4966822258, 4420.12142598891, 21029.6857128588, 4420.12142598891, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21029.6857128588, 4411.52170865539, 21031.4966822258, 4420.12142598891, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21029.6857128588, 4411.52170865539, 21027.8747434917, 4420.12142598891, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21027.8747434917, 4420.12142598891, 21029.6857128588, 4420.12142598891, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21031.4966822258, 4433.6281050995, 21029.6857128588, 4433.6281050995, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21029.6857128588, 4442.22782243302, 21031.4966822258, 4433.6281050995, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21027.8747434917, 4433.6281050995, 21029.6857128588, 4433.6281050995, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21029.6857128588, 4442.22782243302, 21027.8747434917, 4433.6281050995, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(20947.3323573148, 4442.22782243303, 20947.3323573148, 4463.09818137971, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape9.Items.AddLine(20866.3778407995, 4442.22782243303, 20947.3323573148, 4442.22782243303, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape9.Items.AddLine(20957.5458986749, 4442.22782243303, 21035.3605199323, 4442.22782243303, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(20852.2306065413, 4411.52170865538, 21035.3605199323, 4411.52170865538, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(21029.6857128588, 4442.22782243303, 21029.6857128588, 4411.52170865538, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape9.Items.AddLine(20866.3778407995, 4442.22782243303, 20798.2158975884, 4411.52170865538, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape9.Items.AddLine(20798.2158975884, 4411.52170865538, 20717.3323573148, 4411.52170865538, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape9);
                        tr.AddNewlyCreatedDBObject(shape9, true);

                        PosShape shape10 = new PosShape();
                        shape10.Name = "36";
                        shape10.Fields = 5;
                        shape10.Formula = "A+B+C+D";
                        shape10.FormulaBending = "A+B+C+D-r-2*d";
                        shape10.Items.AddText(20924.3388069013, 4285.54081098768, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape10.Items.AddText(20811.1745790312, 4284.88919575916, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape10.Items.AddText(20701.5959702437, 4291.42548430984, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape10.Items.AddText(20746.6816755674, 4297.79231021211, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape10.Items.AddText(21038.4939569013, 4289.15300764158, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape10.Items.AddLine(21031.4966822258, 4286.82224642602, 21029.6857128588, 4286.82224642603, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21029.6857128588, 4278.2225290925, 21031.4966822258, 4286.82224642602, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21027.8747434917, 4286.82224642602, 21029.6857128588, 4286.82224642603, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21029.6857128588, 4278.2225290925, 21027.8747434917, 4286.82224642602, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21031.4966822258, 4311.38012838411, 21029.6857128588, 4311.38012838411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21029.6857128588, 4319.97984571764, 21031.4966822258, 4311.38012838411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21027.8747434917, 4311.38012838411, 21029.6857128588, 4311.38012838411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21029.6857128588, 4319.97984571764, 21027.8747434917, 4311.38012838411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(20957.5458986749, 4319.97984571765, 21035.3605199323, 4319.97984571765, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(20895.6153442926, 4278.22252909249, 21035.3605199323, 4278.22252909249, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(21029.6857128588, 4319.97984571765, 21029.6857128588, 4278.22252909249, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape10.Items.AddLine(20717.3323573148, 4320.96180144811, 20778.420923031, 4320.96180144811, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape10.Items.AddLine(20594.8323573148, 4262.10015001411, 20594.8323573148, 4337.10015001411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape10.Items.AddLine(21069.8323573148, 4337.10015001411, 21069.8323573148, 4262.10015001411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape10.Items.AddLine(21069.8323573148, 4262.10015001411, 20594.8323573148, 4262.10015001411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape10.Items.AddLine(20717.3323573148, 4278.22252909249, 20717.3323573148, 4320.96180144811, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape10.Items.AddLine(20594.8323573148, 4337.10015001411, 21069.8323573148, 4337.10015001411, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape10.Items.AddLine(20874.0831970974, 4278.22252909249, 20947.3323573148, 4319.97984571765, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape10.Items.AddLine(20874.0831970974, 4278.22252909249, 20717.3323573148, 4278.22252909249, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape10);
                        tr.AddNewlyCreatedDBObject(shape10, true);

                        PosShape shape11 = new PosShape();
                        shape11.Name = "41";
                        shape11.Fields = 5;
                        shape11.Formula = "A+B+C+D+E";
                        shape11.FormulaBending = "A+B+C+D+E-2*r-4*d";
                        shape11.Items.AddText(20745.5824101092, 4159.49900606086, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape11.Items.AddText(20701.6395619284, 4153.46535918269, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape11.Items.AddText(20828.4507866097, 4147.53951889648, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape11.Items.AddText(20955.6862476863, 4151.09425887081, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape11.Items.AddText(20918.4428139072, 4159.49900606086, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape11.Items.AddLine(20886.7716284854, 4183.61212458543, 20947.8601942015, 4183.61212458543, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape11.Items.AddLine(20716.8045204281, 4183.61212458544, 20777.8930861443, 4183.61212458544, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape11.Items.AddLine(20716.8045204281, 4140.87285222982, 20716.8045204281, 4183.61212458544, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape11.Items.AddLine(20947.8601942015, 4140.87285222982, 20947.8601942015, 4183.61212458544, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape11.Items.AddLine(20947.8601942015, 4140.87285222982, 20716.8045204281, 4140.87285222982, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape11.Items.AddLine(21069.8323573148, 4124.75047315143, 20594.8323573148, 4124.75047315143, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape11.Items.AddLine(20594.8323573148, 4124.75047315143, 20594.8323573148, 4199.75047315143, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape11.Items.AddLine(20594.8323573148, 4199.75047315143, 21069.8323573148, 4199.75047315143, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape11.Items.AddLine(21069.8323573148, 4199.75047315143, 21069.8323573148, 4124.75047315143, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape11);
                        tr.AddNewlyCreatedDBObject(shape11, true);

                        PosShape shape12 = new PosShape();
                        shape12.Name = "44";
                        shape12.Fields = 5;
                        shape12.Formula = "A+B+C+D+E";
                        shape12.FormulaBending = "A+B+C+D+E-2*r-4*d";
                        shape12.Items.AddText(20734.2471456948, 4037.97784350536, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape12.Items.AddText(20746.8962223319, 4005.89078066871, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape12.Items.AddText(20828.4507866097, 4005.78417102437, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape12.Items.AddText(20907.4524406807, 4006.17343189437, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape12.Items.AddText(20929.9757284296, 4037.97784350536, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape12.Items.AddLine(20947.3323573148, 4030.32922110823, 20896.7294041645, 4030.32922110823, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape12.Items.AddLine(20717.3323573148, 4031.3111768387, 20767.9353104652, 4031.3111768387, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape12.Items.AddLine(20767.9353104652, 3999.1175043577, 20767.9353104652, 4031.3111768387, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape12.Items.AddLine(20896.7294041645, 3999.1175043577, 20896.7294041645, 4030.32922110823, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape12.Items.AddLine(20896.7294041645, 3999.1175043577, 20767.9353104652, 3999.1175043577, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape12.Items.AddLine(20594.8323573148, 3987.40079628876, 20594.8323573148, 4062.40079628876, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape12.Items.AddLine(21069.8323573148, 4062.40079628876, 21069.8323573148, 3987.40079628876, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape12.Items.AddLine(21069.8323573148, 3987.40079628876, 20594.8323573148, 3987.40079628876, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape12.Items.AddLine(20594.8323573148, 4062.40079628876, 21069.8323573148, 4062.40079628876, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape12);
                        tr.AddNewlyCreatedDBObject(shape12, true);

                        PosShape shape13 = new PosShape();
                        shape13.Name = "46";
                        shape13.Fields = 5;
                        shape13.Formula = "A+2*B+C+E";
                        shape13.FormulaBending = "A+2*B+C+E";
                        shape13.Items.AddText(20734.2471456948, 3899.55810466594, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape13.Items.AddText(20756.0615221139, 3865.49013339323, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape13.Items.AddText(20828.0466430291, 3867.36443218494, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape13.Items.AddText(20918.4428139072, 3899.55810466594, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape13.Items.AddText(21043.9116724508, 3866.93311217934, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape13.Items.AddLine(21031.4966822258, 3869.29748285181, 21029.6857128588, 3869.29748285181, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21029.6857128588, 3860.69776551829, 21031.4966822258, 3869.29748285181, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21027.8747434917, 3869.29748285181, 21029.6857128588, 3869.29748285181, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21029.6857128588, 3860.69776551829, 21027.8747434917, 3869.29748285181, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21031.4966822258, 3883.30976493528, 21029.6857128588, 3883.30976493527, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21029.6857128588, 3891.9094822688, 21031.4966822258, 3883.30976493528, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21027.8747434917, 3883.30976493528, 21029.6857128588, 3883.30976493527, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21029.6857128588, 3891.9094822688, 21027.8747434917, 3883.30976493528, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(20957.5458986749, 3891.90948226881, 21035.3605199323, 3891.90948226881, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(20957.5458986749, 3860.69776551828, 21035.3605199323, 3860.69776551828, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(21029.6857128588, 3891.90948226881, 21029.6857128588, 3860.69776551828, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape13.Items.AddLine(20947.3323573148, 3891.90948226881, 20903.1435028698, 3891.90948226881, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape13.Items.AddLine(20717.3323573148, 3892.89143799928, 20761.5212117599, 3892.89143799927, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape13.Items.AddLine(20794.208352469, 3860.69776551828, 20761.5212117599, 3892.89143799927, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape13.Items.AddLine(20870.4563621607, 3860.69776551828, 20903.1435028698, 3891.90948226881, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape13.Items.AddLine(20870.4563621607, 3860.69776551828, 20794.208352469, 3860.69776551828, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape13.Items.AddLine(20594.8323573148, 3850.05111942608, 20594.8323573148, 3925.05111942608, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape13.Items.AddLine(21069.8323573148, 3850.05111942608, 20594.8323573148, 3850.05111942608, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape13.Items.AddLine(21069.8323573148, 3925.05111942608, 21069.8323573148, 3850.05111942608, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape13.Items.AddLine(20594.8323573148, 3925.05111942608, 21069.8323573148, 3925.05111942608, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape13);
                        tr.AddNewlyCreatedDBObject(shape13, true);

                        PosShape shape14 = new PosShape();
                        shape14.Name = "47";
                        shape14.Fields = 4;
                        shape14.Formula = "2*A+B+C+D";
                        shape14.FormulaBending = "2*A+B+C+D+1.5*r-3*d";
                        shape14.Items.AddText(20701.0245416723, 3747.77646754003, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape14.Items.AddText(20828.7365008954, 3735.49048830846, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape14.Items.AddText(20742.9370395759, 3761.70047439885, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape14.Items.AddText(20915.5295648667, 3757.52346667115, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape14.Items.AddArc(20940.8323990629, 3771.56309399742, 6.49995825191945, 0, 3.14159265358979, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddLine(20934.332440811, 3751.79756145549, 20934.332440811, 3771.56309399742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddLine(20947.3323573148, 3728.82382164179, 20947.3323573148, 3771.56309399742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddArc(20723.8323155667, 3771.56309399742, 6.49995825191945, 0, 3.14159265358979, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddLine(20730.3322738187, 3751.79756145549, 20730.3322738187, 3771.56309399742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddLine(20717.3323573148, 3728.82382164179, 20717.3323573148, 3771.56309399742, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddLine(20947.3323573148, 3728.82382164179, 20717.3323573148, 3728.82382164179, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape14.Items.AddLine(20594.8323573148, 3712.70144256341, 20594.8323573148, 3787.70144256341, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape14.Items.AddLine(21069.8323573148, 3712.70144256341, 20594.8323573148, 3712.70144256341, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape14.Items.AddLine(21069.8323573148, 3787.70144256341, 21069.8323573148, 3712.70144256341, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape14.Items.AddLine(20594.8323573148, 3787.70144256341, 21069.8323573148, 3787.70144256341, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape14);
                        tr.AddNewlyCreatedDBObject(shape14, true);

                        PosShape shape15 = new PosShape();
                        shape15.Name = "51";
                        shape15.Fields = 4;
                        shape15.Formula = "2*A+2*B+C+D";
                        shape15.FormulaBending = "2*A+2*B+C+D-2.5*r-5*d";
                        shape15.Items.AddText(20828.165072324, 3590.24414000909, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape15.Items.AddText(20701.6395619284, 3597.92668110271, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape15.Items.AddText(20914.854749696, 3610.72814415663, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape15.Items.AddText(20934.2074210495, 3589.46073432475, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape15.Items.AddLine(20947.3323573148, 3634.21341713474, 20932.0937097944, 3620.63581936444, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape15.Items.AddLine(20947.3323573148, 3634.21341713474, 20716.8045204281, 3634.21341713474, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape15.Items.AddLine(20947.3323573148, 3625.984563177, 20932.0937097944, 3612.40696540669, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape15.Items.AddLine(20716.8045204281, 3583.57747334242, 20716.8045204281, 3634.21341713474, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape15.Items.AddLine(20947.3323573148, 3583.57747334242, 20947.3323573148, 3625.984563177, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape15.Items.AddLine(20947.3323573148, 3583.57747334242, 20716.8045204281, 3583.57747334242, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape15.Items.AddLine(21069.8323573148, 3575.35176570073, 20594.8323573148, 3575.35176570073, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape15.Items.AddLine(20594.8323573148, 3575.35176570073, 20594.8323573148, 3650.35176570073, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape15.Items.AddLine(20594.8323573148, 3650.35176570073, 21069.8323573148, 3650.35176570073, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape15.Items.AddLine(21069.8323573148, 3650.35176570073, 21069.8323573148, 3575.35176570073, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape15);
                        tr.AddNewlyCreatedDBObject(shape15, true);

                        PosShape shape16 = new PosShape();
                        shape16.Name = "56";
                        shape16.Fields = 6;
                        shape16.Formula = "A+B+C+D+E+F";
                        shape16.FormulaBending = "A+B+C+D+E+F-2.5*r-5*d";
                        shape16.Items.AddText(20828.165072324, 3456.40409165561, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape16.Items.AddText(20702.1673988151, 3468.69007088718, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape16.Items.AddText(20804.5818696515, 3481.24528529739, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape16.Items.AddText(20939.3877195394, 3471.64241651943, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape16.Items.AddText(20741.8856511857, 3453.8156426091, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape16.Items.AddText(20725.438393349, 3476.90527841104, 20, "F", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape16.Items.AddLine(20732.5710048353, 3470.81137462276, 20717.3323573148, 3457.23377685246, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape16.Items.AddLine(20732.5710048353, 3463.31502275925, 20717.3323573148, 3449.73742498894, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape16.Items.AddLine(20904.1065822933, 3504.67578950558, 20717.3323573148, 3504.67578950558, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape16.Items.AddLine(20717.3323573148, 3457.23377685246, 20717.3323573148, 3504.67578950558, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape16.Items.AddLine(20947.3323573148, 3449.73742498894, 20904.1065822933, 3504.67578950558, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape16.Items.AddLine(20947.3323573148, 3449.73742498894, 20717.3323573148, 3449.73742498894, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape16.Items.AddLine(21069.8323573148, 3513.00208883806, 21069.8323573148, 3438.00208883806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape16.Items.AddLine(20594.8323573148, 3438.00208883806, 20594.8323573148, 3513.00208883806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape16.Items.AddLine(21069.8323573148, 3438.00208883806, 20594.8323573148, 3438.00208883806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape16.Items.AddLine(20594.8323573148, 3513.00208883806, 21069.8323573148, 3513.00208883806, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape16);
                        tr.AddNewlyCreatedDBObject(shape16, true);

                        PosShape shape17 = new PosShape();
                        shape17.Name = "63";
                        shape17.Fields = 4;
                        shape17.Formula = "2*A+3*B+C+D";
                        shape17.FormulaBending = "2*A+3*B+C+D-3*r-6*d";
                        shape17.Items.AddText(20695.8757071979, 3327.19779083776, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape17.Items.AddText(20825.5725448675, 3316.96156127354, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape17.Items.AddText(20919.2202644979, 3321.21911828647, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape17.Items.AddText(20748.3470764072, 3323.47005199988, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape17.Items.AddLine(20724.5116838022, 3351.28520945165, 20739.7503313226, 3337.70761168134, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(20947.3323573148, 3351.28520945165, 20724.5116838022, 3351.28520945165, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(20947.3323573148, 3359.5140634094, 20932.0937097944, 3345.93646563909, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(20947.3323573148, 3359.5140634094, 20716.8045204281, 3359.51406340939, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(20716.8045204281, 3308.87811961707, 20716.8045204281, 3359.51406340939, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(20947.3323573148, 3308.87811961707, 20947.3323573148, 3351.28520945165, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(20947.3323573148, 3308.87811961707, 20716.8045204281, 3308.87811961707, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape17.Items.AddLine(21069.8323573148, 3300.65241197539, 20594.8323573148, 3300.65241197539, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape17.Items.AddLine(20594.8323573148, 3300.65241197539, 20594.8323573148, 3375.65241197539, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape17.Items.AddLine(21069.8323573148, 3375.65241197539, 21069.8323573148, 3300.65241197539, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape17.Items.AddLine(20594.8323573148, 3375.65241197539, 21069.8323573148, 3375.65241197539, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape17);
                        tr.AddNewlyCreatedDBObject(shape17, true);

                        PosShape shape18 = new PosShape();
                        shape18.Name = "64";
                        shape18.Fields = 6;
                        shape18.Formula = "A+B+C+2*D+E+F";
                        shape18.FormulaBending = "A+B+C+2*D+E+F-3*r-6*d";
                        shape18.Items.AddText(20754.8091276568, 3213.00586121629, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape18.Items.AddText(20702.1673988151, 3180.79968238475, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape18.Items.AddText(20859.6687881691, 3176.59504586845, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape18.Items.AddText(20955.1068412328, 3187.67779985131, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape18.Items.AddText(20896.5009458005, 3195.84574367542, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape18.Items.AddText(20811.8931289184, 3182.38809624131, 20, "F", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape18.Items.AddLine(20824.0431520527, 3176.90732929912, 20793.5317536035, 3176.90732929912, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20793.5317536035, 3176.90732929912, 20793.5317536035, 3222.16438654672, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20865.6574975652, 3209.0048768678, 20716.8045204281, 3209.00487686779, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20947.3323573148, 3222.16438654672, 20793.5317536035, 3222.16438654672, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20716.8045204281, 3171.5284427544, 20716.8045204281, 3209.00487686779, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20947.3323573148, 3171.5284427544, 20947.3323573148, 3222.16438654672, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20947.3323573148, 3171.5284427544, 20716.8045204281, 3171.5284427544, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape18.Items.AddLine(20594.8323573148, 3163.30273511271, 20594.8323573148, 3238.30273511271, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape18.Items.AddLine(21069.8323573148, 3163.30273511271, 20594.8323573148, 3163.30273511271, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape18.Items.AddLine(21069.8323573148, 3238.30273511271, 21069.8323573148, 3163.30273511271, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape18.Items.AddLine(20594.8323573148, 3238.30273511271, 21069.8323573148, 3238.30273511271, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape18);
                        tr.AddNewlyCreatedDBObject(shape18, true);

                        PosShape shape19 = new PosShape();
                        shape19.Name = "67";
                        shape19.Fields = 4;
                        shape19.Formula = "A";
                        shape19.FormulaBending = "A";
                        shape19.Items.AddText(20814.7921168808, 3075.8163124819, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape19.Items.AddText(20807.1376136705, 3042.82169029952, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape19.Items.AddText(20988.2181172542, 3050.44373302353, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape19.Items.AddText(20865.285089419, 3043.41915570175, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape19.Items.AddText(20839.3058197531, 3045.01200255318, 15, "R=", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBase, true);
                        shape19.Items.AddLine(20834.1433266819, 3063.41362056604, 20832.3323573148, 3063.41362056603, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20832.3323573148, 3072.01333789956, 20834.1433266819, 3063.41362056604, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20830.5213879478, 3063.41362056604, 20832.3323573148, 3063.41362056603, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20832.3323573148, 3072.01333789956, 20830.5213879478, 3063.41362056604, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20832.3323573148, 3072.01333789956, 20832.3323573148, 3031.43995287255, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20725.9320746483, 3037.46511068525, 20725.9320746483, 3039.27608005231, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20717.3323573147, 3039.27608005231, 20725.9320746483, 3037.46511068525, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20725.9320746483, 3041.08704941936, 20725.9320746483, 3039.27608005231, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20717.3323573147, 3039.27608005231, 20725.9320746483, 3041.08704941936, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20938.7326399814, 3037.46511068525, 20938.7326399814, 3039.27608005231, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20947.3323573149, 3039.27608005231, 20938.7326399814, 3037.46511068525, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20938.7326399814, 3041.08704941936, 20938.7326399814, 3039.27608005231, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20947.3323573149, 3039.27608005231, 20938.7326399814, 3041.08704941936, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20947.3323573149, 3046.79048668831, 20947.3323573149, 3033.60127297879, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20717.3323573147, 3046.79048668831, 20717.3323573147, 3033.60127297879, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20947.3323573149, 3039.27608005231, 20717.3323573147, 3039.27608005231, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddArc(20832.3323573148, 2797.2388465633, 274.774491336261, 1.13897567371799, 2.0026169798718, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape19.Items.AddLine(20832.3323573148, 3072.01333789956, 20981.1281059143, 3072.01333789956, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20976.8218206176, 3055.39020402185, 20975.0108512505, 3055.39020402185, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20975.0108512505, 3046.79048668833, 20976.8218206176, 3055.39020402185, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20973.1998818834, 3055.39020402185, 20975.0108512505, 3055.39020402185, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20975.0108512505, 3046.79048668833, 20973.1998818834, 3055.39020402185, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20976.8218206176, 3063.41362056604, 20975.0108512505, 3063.41362056603, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20975.0108512505, 3072.01333789956, 20976.8218206176, 3063.41362056604, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20973.1998818834, 3063.41362056604, 20975.0108512505, 3063.41362056603, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20975.0108512505, 3072.01333789956, 20973.1998818834, 3063.41362056604, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20947.3323573149, 3046.79048668833, 20980.685658324, 3046.79048668833, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20975.0108512505, 3072.01333789956, 20975.0108512505, 3046.79048668833, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape19.Items.AddLine(20594.8323573148, 3025.95305825004, 20594.8323573148, 3100.95305825004, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape19.Items.AddLine(21069.8323573148, 3025.95305825004, 20594.8323573148, 3025.95305825004, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape19.Items.AddLine(21069.8323573148, 3100.95305825004, 21069.8323573148, 3025.95305825004, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape19.Items.AddLine(20594.8323573148, 3100.95305825004, 21069.8323573148, 3100.95305825004, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape19);
                        tr.AddNewlyCreatedDBObject(shape19, true);

                        PosShape shape20 = new PosShape();
                        shape20.Name = "75";
                        shape20.Fields = 4;
                        shape20.Formula = "3.14*A+B+C+D";
                        shape20.FormulaBending = "3.14*A-3.14*d+B+C+D";
                        shape20.Items.AddText(20910.5916722495, 2930.17311435377, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBase, true);
                        shape20.Items.AddText(20809.2042433789, 2891.09799409837, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape20.Items.AddText(20773.2872160157, 2937.02553610389, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape20.Items.AddText(20839.3144973896, 2938.74012456487, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape20.Items.AddArc(20811.0211310968, 2936.9974663456, 23.8207435322511, 3.14159265358979, 0, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape20.Items.AddArc(20811.0211310968, 2936.9974663456, 23.8207435322511, 0, 3.14159265358979, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape20.Items.AddLine(20905.1340573667, 2921.15562896187, 20903.3230879997, 2921.15562896188, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20903.3230879997, 2912.55591162835, 20905.1340573667, 2921.15562896187, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20901.5121186326, 2921.15562896187, 20903.3230879997, 2921.15562896188, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20903.3230879997, 2912.55591162835, 20901.5121186326, 2921.15562896187, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20905.1340573667, 2950.4929678927, 20903.3230879997, 2950.4929678927, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20903.3230879997, 2959.09268522623, 20905.1340573667, 2950.4929678927, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20901.5121186326, 2950.4929678927, 20903.3230879997, 2950.4929678927, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20903.3230879997, 2959.09268522623, 20901.5121186326, 2950.4929678927, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20775.1297090395, 2925.22684068833, 20777.5926353337, 2925.82852978099, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20774.9325571548, 2931.37551190934, 20775.1297090395, 2925.22684068833, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20774.9325571548, 2931.37551190934, 20777.5926353337, 2925.82852978099, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20846.9125531541, 2925.22684068833, 20844.4496268599, 2925.82852978099, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20847.1097050388, 2931.37551190934, 20846.9125531541, 2925.22684068833, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20847.1097050388, 2931.37551190934, 20844.4496268599, 2925.82852978099, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20777.2151574626, 2932.42155464861, 20772.649956847, 2930.32946917007, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20844.827104731, 2932.42155464861, 20849.3923053466, 2930.32946917007, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20794.8197113248, 2930.20866746045, 20787.2003875646, 2936.9974663456, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape20.Items.AddLine(20827.2225508688, 2930.20866746045, 20834.8418746291, 2936.9974663456, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape20.Items.AddArc(20811.0211310968, 2936.9974663456, 36.5238489325545, 5.31144838590462, 6.12864533045018, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddArc(20811.0211310968, 2936.9974663456, 36.5238489325545, 3.29613263031875, 4.20440884155335, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20875.6445940641, 2959.09268522623, 20908.9978950732, 2959.09268522623, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20875.6445940641, 2912.55591162835, 20908.9978950732, 2912.55591162835, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20903.3230879997, 2959.09268522623, 20903.3230879997, 2912.55591162835, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape20.Items.AddLine(20594.8323573148, 2888.60338138737, 20594.8323573148, 2963.60338138737, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape20.Items.AddLine(21069.8323573148, 2963.60338138737, 21069.8323573148, 2888.60338138737, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape20.Items.AddLine(21069.8323573148, 2888.60338138737, 20594.8323573148, 2888.60338138737, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape20.Items.AddLine(20594.8323573148, 2963.60338138737, 21069.8323573148, 2963.60338138737, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape20);
                        tr.AddNewlyCreatedDBObject(shape20, true);

                        PosShape shape21 = new PosShape();
                        shape21.Name = "00";
                        shape21.Fields = 1;
                        shape21.Formula = "A";
                        shape21.FormulaBending = "A";
                        shape21.Items.AddText(20827.7609287434, 6499.42017508631, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape21.Items.AddLine(20594.8323573148, 6534.69497981689, 21069.8323573148, 6534.69497981689, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape21.Items.AddLine(20594.8323573148, 6459.69497981689, 20594.8323573148, 6534.69497981689, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape21.Items.AddLine(21069.8323573148, 6459.69497981689, 20594.8323573148, 6459.69497981689, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape21.Items.AddLine(21069.8323573148, 6534.69497981689, 21069.8323573148, 6459.69497981689, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape21.Items.AddLine(20717.3323573148, 6492.75350841964, 20947.3323573148, 6492.75350841964, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        id = dict.SetAt("*", shape21);
                        tr.AddNewlyCreatedDBObject(shape21, true);

                        PosShape shape22 = new PosShape();
                        shape22.Name = "77";
                        shape22.Fields = 5;
                        shape22.Formula = "3.14*A*C+D+E";
                        shape22.FormulaBending = "3.14*A*C-3.14*d*C+D+E";
                        shape22.Items.AddText(20724.9865803327, 2780.84005585922, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBase, true);
                        shape22.Items.AddText(20945.6208146698, 2786.93784974481, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape22.Items.AddText(20842.2027731253, 2787.83877877539, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape22.Items.AddText(20770.827565168, 2786.52610585953, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape22.Items.AddText(20894.9110755729, 2762.28265234657, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape22.Items.AddArc(20811.0211310968, 2789.92148420238, 23.8207435322511, 3.14159265358979, 0, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddArc(20811.0211310968, 2789.92148420238, 23.8207435322511, 0, 3.14159265358979, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddText(20923.5798805558, 2757.25371272298, 12, "TUR SAYISI", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBase, true);
                        shape22.Items.AddLine(20907.3233857025, 2758.09584708759, 20907.3233857025, 2759.90681645465, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20915.923103036, 2759.90681645465, 20907.3233857025, 2758.09584708759, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20907.3233857025, 2761.7177858217, 20907.3233857025, 2759.90681645465, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20915.923103036, 2759.90681645465, 20907.3233857025, 2761.7177858217, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20915.923103036, 2767.42122309065, 20915.923103036, 2754.23200938113, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20890.9097022145, 2758.09584708759, 20890.9097022145, 2759.90681645465, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20882.3099848809, 2759.90681645465, 20890.9097022145, 2758.09584708759, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20890.9097022145, 2761.7177858217, 20890.9097022145, 2759.90681645465, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20882.3099848809, 2759.90681645465, 20890.9097022145, 2761.7177858217, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20882.3099848809, 2767.42122309065, 20882.3099848809, 2754.23200938113, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20915.923103036, 2759.90681645465, 20882.3099848809, 2759.90681645465, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20794.8197113248, 2783.13268531723, 20787.2003875646, 2789.92148420238, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20827.2225508688, 2783.13268531723, 20834.8418746291, 2789.92148420238, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20931.2364501352, 2795.03481112747, 20929.9687715783, 2795.03481112748, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20929.9687715783, 2789.01500899401, 20931.2364501352, 2795.03481112747, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20928.7010930213, 2795.03481112747, 20929.9687715783, 2795.03481112748, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20929.9687715783, 2789.01500899401, 20928.7010930213, 2795.03481112747, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20931.2364501352, 2798.03460302251, 20929.9687715783, 2798.03460302251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20929.9687715783, 2804.05440515598, 20931.2364501352, 2798.03460302251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20928.7010930213, 2798.03460302251, 20929.9687715783, 2798.03460302251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20929.9687715783, 2804.05440515598, 20928.7010930213, 2798.03460302251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20748.3379246857, 2804.12481591489, 20746.5269553186, 2804.12481591489, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20746.5269553186, 2812.72453324842, 20748.3379246857, 2804.12481591489, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20744.7159859516, 2804.12481591489, 20746.5269553186, 2804.12481591489, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20746.5269553186, 2812.72453324842, 20744.7159859516, 2804.12481591489, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20748.3379246857, 2774.78747698408, 20746.5269553186, 2774.78747698408, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20746.5269553186, 2766.18775965055, 20748.3379246857, 2774.78747698408, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20744.7159859516, 2774.78747698408, 20746.5269553186, 2774.78747698408, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20746.5269553186, 2766.18775965055, 20744.7159859516, 2774.78747698408, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20924.2939645048, 2804.05440515599, 20935.6435786518, 2804.05440515599, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20924.2939645048, 2789.015008994, 20935.6435786518, 2789.015008994, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20929.9687715783, 2804.05440515599, 20929.9687715783, 2789.015008994, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20915.923103036, 2789.83303099958, 20882.3099848805, 2782.51366889531, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20915.923103036, 2789.83303099958, 20882.3099848805, 2797.15239310384, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20915.923103036, 2804.47175520811, 20882.3099848805, 2797.15239310384, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20915.923103036, 2804.47175520811, 20882.3099848805, 2811.79111731237, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20915.923103036, 2819.11047941664, 20882.3099848805, 2811.79111731237, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape22.Items.AddLine(20740.8521482451, 2812.72453324842, 20777.3893536829, 2812.72453324842, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20740.8521482451, 2766.18775965054, 20777.3893536829, 2766.18775965054, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(20746.5269553186, 2812.72453324842, 20746.5269553186, 2766.18775965054, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape22.Items.AddLine(21069.8323573148, 2751.25370452469, 20594.8323573148, 2751.25370452469, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape22.Items.AddLine(20594.8323573148, 2751.25370452469, 20594.8323573148, 2826.25370452469, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape22.Items.AddLine(20594.8323573148, 2826.25370452469, 21069.8323573148, 2826.25370452469, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape22.Items.AddLine(21069.8323573148, 2826.25370452469, 21069.8323573148, 2751.25370452469, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape22);
                        tr.AddNewlyCreatedDBObject(shape22, true);

                        PosShape shape23 = new PosShape();
                        shape23.Name = "93";
                        shape23.Fields = 4;
                        shape23.Formula = "A+B+C+D";
                        shape23.FormulaBending = "A+B+C+D-0.5*r-d";
                        shape23.Items.AddText(20719.0474552932, 2646.32549675385, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape23.Items.AddText(20828.0205440269, 2634.76663389474, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape23.Items.AddText(20957.8405487196, 2641.03747645112, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape23.Items.AddText(20921.1834676034, 2663.59225163031, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape23.Items.AddLine(20594.8323573148, 2613.90402766201, 20594.8323573148, 2688.90402766201, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape23.Items.AddLine(21069.8323573148, 2613.90402766201, 20594.8323573148, 2613.90402766201, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape23.Items.AddLine(21069.8323573148, 2688.90402766201, 21069.8323573148, 2613.90402766201, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape23.Items.AddLine(20594.8323573148, 2688.90402766201, 21069.8323573148, 2688.90402766201, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape23.Items.AddLine(20947.3323573148, 2671.78372336555, 20921.813766261, 2649.04672098202, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape23.Items.AddLine(20717.3323573148, 2630.02640674039, 20742.013646058, 2649.92306727581, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape23.Items.AddLine(20947.3323573148, 2630.02640674039, 20947.3323573148, 2671.78372336555, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape23.Items.AddLine(20947.3323573148, 2630.02640674039, 20717.3323573148, 2630.02640674039, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape23);
                        tr.AddNewlyCreatedDBObject(shape23, true);

                        PosShape shape24 = new PosShape();
                        shape24.Name = "94";
                        shape24.Fields = 3;
                        shape24.Formula = "A+B+C";
                        shape24.FormulaBending = "A+B+C-0.5*r-d";
                        shape24.Items.AddText(20750.6816755674, 2522.09956851144, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape24.Items.AddText(20828.0205440269, 2497.41695703208, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape24.Items.AddText(20699.9052720074, 2504.80377006369, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape24.Items.AddLine(20717.3323573148, 2535.41600223335, 20747.846170064, 2514.10006676345, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape24.Items.AddLine(20717.3323573148, 2492.67672987773, 20717.3323573148, 2535.41600223335, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape24.Items.AddLine(20947.3323573148, 2492.67672987773, 20717.3323573148, 2492.67672987773, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape24.Items.AddLine(20594.8323573148, 2476.55435079935, 20594.8323573148, 2551.55435079935, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape24.Items.AddLine(21069.8323573148, 2476.55435079935, 20594.8323573148, 2476.55435079935, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape24.Items.AddLine(21069.8323573148, 2551.55435079935, 21069.8323573148, 2476.55435079935, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape24.Items.AddLine(20594.8323573148, 2551.55435079935, 21069.8323573148, 2551.55435079935, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape24);
                        tr.AddNewlyCreatedDBObject(shape24, true);

                        PosShape shape25 = new PosShape();
                        shape25.Name = "95";
                        shape25.Fields = 4;
                        shape25.Formula = "A+2*B+C+D";
                        shape25.FormulaBending = "A+2*B+C+D-r-2*d";
                        shape25.Items.AddText(20750.6816755674, 2384.74989164876, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape25.Items.AddText(20828.0205440269, 2360.06728016941, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape25.Items.AddText(20699.9052720074, 2367.45409320101, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape25.Items.AddText(20913.5848587416, 2384.0276839255, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape25.Items.AddLine(20947.3323573148, 2397.08436964021, 20921.813766261, 2374.34736725668, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape25.Items.AddLine(20717.3323573148, 2398.06632537068, 20747.846170064, 2376.75038990077, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape25.Items.AddLine(20717.3323573148, 2355.32705301505, 20717.3323573148, 2398.06632537068, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape25.Items.AddLine(20947.3323573148, 2355.32705301506, 20947.3323573148, 2397.08436964021, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape25.Items.AddLine(20947.3323573148, 2355.32705301506, 20717.3323573148, 2355.32705301505, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape25.Items.AddLine(20594.8323573148, 2339.20467393667, 20594.8323573148, 2414.20467393667, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape25.Items.AddLine(21069.8323573148, 2414.20467393667, 21069.8323573148, 2339.20467393667, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape25.Items.AddLine(21069.8323573148, 2339.20467393667, 20594.8323573148, 2339.20467393667, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape25.Items.AddLine(20594.8323573148, 2414.20467393667, 21069.8323573148, 2414.20467393667, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape25);
                        tr.AddNewlyCreatedDBObject(shape25, true);

                        PosShape shape26 = new PosShape();
                        shape26.Name = "96";
                        shape26.Fields = 3;
                        shape26.Formula = "A+B+C";
                        shape26.FormulaBending = "A+B+C-0.5*r-d";
                        shape26.Items.AddText(20715.9688473599, 2235.06332188134, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape26.Items.AddText(20828.7365008954, 2224.64404281905, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape26.Items.AddText(20928.4275133565, 2227.41938467183, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape26.Items.AddLine(20717.3323573148, 2217.97737615238, 20742.013646058, 2237.87403668781, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape26.Items.AddLine(20947.3323573148, 2217.97737615238, 20947.3323573148, 2246.56219884071, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape26.Items.AddLine(20947.3323573148, 2217.97737615238, 20717.3323573148, 2217.97737615238, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape26.Items.AddLine(20594.8323573148, 2201.854997074, 20594.8323573148, 2276.854997074, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape26.Items.AddLine(21069.8323573148, 2201.854997074, 20594.8323573148, 2201.854997074, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape26.Items.AddLine(20594.8323573148, 2276.854997074, 21069.8323573148, 2276.854997074, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape26.Items.AddLine(21069.8323573148, 2276.854997074, 21069.8323573148, 2201.854997074, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape26);
                        tr.AddNewlyCreatedDBObject(shape26, true);

                        PosShape shape27 = new PosShape();
                        shape27.Name = "97";
                        shape27.Fields = 3;
                        shape27.Formula = "A+B+C";
                        shape27.FormulaBending = "A+B+C";
                        shape27.Items.AddText(20715.9688473599, 2097.71364501866, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape27.Items.AddText(20828.7365008954, 2087.29436595637, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape27.Items.AddText(20937.6257832715, 2096.89393715291, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape27.Items.AddLine(20947.3323573148, 2080.62769928971, 20922.6510685717, 2100.52435982513, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape27.Items.AddLine(20717.3323573148, 2080.62769928971, 20742.013646058, 2100.52435982513, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape27.Items.AddLine(20947.3323573148, 2080.62769928971, 20717.3323573148, 2080.62769928971, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape27.Items.AddLine(21069.8323573148, 2064.50532021132, 20594.8323573148, 2064.50532021132, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape27.Items.AddLine(20594.8323573148, 2064.50532021132, 20594.8323573148, 2139.50532021132, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape27.Items.AddLine(21069.8323573148, 2139.50532021132, 21069.8323573148, 2064.50532021132, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape27.Items.AddLine(20594.8323573148, 2139.50532021132, 21069.8323573148, 2139.50532021132, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape27);
                        tr.AddNewlyCreatedDBObject(shape27, true);

                        PosShape shape28 = new PosShape();
                        shape28.Name = "98";
                        shape28.Fields = 4;
                        shape28.Formula = "A+2*B+C+D";
                        shape28.FormulaBending = "A+2*B+C+D-2*r-4*d";
                        shape28.Items.AddText(20830.6731796923, 1961.93392400327, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape28.Items.AddText(20748.6051468836, 1965.33946151639, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape28.Items.AddText(20720.7746927241, 1934.52614732265, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape28.Items.AddText(20926.1653867869, 1962.34383514213, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape28.Items.AddLine(20764.9882972375, 1948.12979313776, 20717.3323573148, 1961.60194986532, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape28.Items.AddLine(20947.3323573148, 1951.28467939036, 20899.6764173921, 1964.75683611792, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape28.Items.AddLine(20764.9882972375, 1980.32346561876, 20764.9882972375, 1948.12979313776, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape28.Items.AddLine(20899.6764173921, 1995.96855286845, 20899.6764173921, 1964.75683611792, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape28.Items.AddLine(20899.6764173921, 1995.96855286845, 20764.9882972375, 1980.32346561876, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape28.Items.AddLine(20594.8323573148, 1927.15564334865, 20594.8323573148, 2002.15564334865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape28.Items.AddLine(21069.8323573148, 1927.15564334865, 20594.8323573148, 1927.15564334865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape28.Items.AddLine(20594.8323573148, 2002.15564334865, 21069.8323573148, 2002.15564334865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape28.Items.AddLine(21069.8323573148, 2002.15564334865, 21069.8323573148, 1927.15564334865, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape28);
                        tr.AddNewlyCreatedDBObject(shape28, true);

                        PosShape shape29 = new PosShape();
                        shape29.Name = "100";
                        shape29.Fields = 5;
                        shape29.Formula = "A+B+C+D";
                        shape29.FormulaBending = "A+B+C+D-0.5*r-d";
                        shape29.Items.AddText(20704.5142768829, 1799.45892229619, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape29.Items.AddText(20747.408685363, 1821.42766463371, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape29.Items.AddText(20832.836173336, 1842.68693608601, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape29.Items.AddText(20941.3686011816, 1833.06534728302, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape29.Items.AddText(20937.3153370453, 1799.56226839291, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape29.Items.AddLine(20901.0518273965, 1837.66387067253, 20947.3323573148, 1860.77959429783, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape29.Items.AddLine(20929.0700816078, 1802.70380360269, 20927.2591122407, 1802.70380360269, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20927.2591122408, 1794.10408626917, 20929.0700816078, 1802.70380360269, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20925.4481428737, 1802.70380360269, 20927.2591122407, 1802.70380360269, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20927.2591122407, 1794.10408626917, 20925.4481428737, 1802.70380360269, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20929.0700816078, 1808.62009256093, 20927.2591122408, 1808.62009256092, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20927.2591122408, 1817.21980989445, 20929.0700816078, 1808.62009256093, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20925.4481428737, 1808.62009256093, 20927.2591122408, 1808.62009256092, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20927.2591122407, 1817.21980989445, 20925.4481428737, 1808.62009256093, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20884.7946752756, 1817.21980989446, 20932.9339193143, 1817.21980989446, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20884.7946752756, 1794.10408626916, 20932.9339193143, 1794.10408626916, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20927.2591122407, 1817.21980989446, 20927.2591122407, 1794.10408626916, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape29.Items.AddLine(20763.6128872332, 1837.66387067253, 20901.0518273965, 1837.66387067253, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape29.Items.AddLine(20763.6128872332, 1817.21980989446, 20763.6128872332, 1837.66387067253, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape29.Items.AddLine(20717.3323573148, 1794.10408626916, 20763.6128872332, 1817.21980989446, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape29.Items.AddLine(20594.8323573148, 1789.80596648597, 20594.8323573148, 1864.80596648597, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape29.Items.AddLine(21069.8323573148, 1789.80596648597, 20594.8323573148, 1789.80596648597, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape29.Items.AddLine(21069.8323573148, 1864.80596648597, 21069.8323573148, 1789.80596648597, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape29.Items.AddLine(20594.8323573148, 1864.80596648597, 21069.8323573148, 1864.80596648597, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape29);
                        tr.AddNewlyCreatedDBObject(shape29, true);

                        PosShape shape30 = new PosShape();
                        shape30.Name = "101";
                        shape30.Fields = 3;
                        shape30.Formula = "A+2*B+2*C";
                        shape30.FormulaBending = "A+2*B+2*C-2*r-4*d";
                        shape30.Items.AddText(20943.9345826257, 1689.40972524058, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape30.Items.AddText(20828.3323573148, 1666.88416443473, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape30.Items.AddText(20702.2539053228, 1671.00103762706, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape30.Items.AddLine(20736.5547950044, 1715.15995609207, 20947.3323573148, 1715.15995609207, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape30.Items.AddLine(20736.5547950044, 1686.24782366215, 20736.5547950044, 1715.15995609207, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape30.Items.AddLine(20928.1099196253, 1694.28129777949, 20947.3323573148, 1715.15995609207, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape30.Items.AddLine(20717.3323573148, 1694.28129777949, 20928.1099196253, 1694.28129777949, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape30.Items.AddLine(20717.3323573148, 1665.36916534958, 20717.3323573148, 1694.28129777949, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape30.Items.AddLine(20594.8323573148, 1652.4562896233, 20594.8323573148, 1727.4562896233, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape30.Items.AddLine(21069.8323573148, 1727.4562896233, 21069.8323573148, 1652.4562896233, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape30.Items.AddLine(21069.8323573148, 1652.4562896233, 20594.8323573148, 1652.4562896233, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape30.Items.AddLine(20594.8323573148, 1727.4562896233, 21069.8323573148, 1727.4562896233, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape30);
                        tr.AddNewlyCreatedDBObject(shape30, true);

                        PosShape shape31 = new PosShape();
                        shape31.Name = "102";
                        shape31.Fields = 4;
                        shape31.Formula = "A+B+C+D";
                        shape31.FormulaBending = "A+B+C+D-r-2*d";
                        shape31.Items.AddText(20702.5834816984, 1537.03052651467, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape31.Items.AddText(20828.3323573148, 1567.95510245633, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape31.Items.AddText(20954.2220657007, 1545.5652030691, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape31.Items.AddText(20890.2321477489, 1529.69438152174, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape31.Items.AddLine(20947.3323573148, 1539.91879961185, 20947.3323573148, 1561.28843578966, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape31.Items.AddLine(20883.4767837815, 1520.2637787416, 20947.3323573148, 1539.91879961185, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape31.Items.AddLine(20717.3323573148, 1561.28843578966, 20947.3323573148, 1561.28843578966, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape31.Items.AddLine(20717.3323573148, 1518.54916343403, 20717.3323573148, 1561.28843578966, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape31.Items.AddLine(21069.8323573148, 1515.10661276062, 20594.8323573148, 1515.10661276062, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape31.Items.AddLine(20594.8323573148, 1515.10661276062, 20594.8323573148, 1590.10661276062, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape31.Items.AddLine(21069.8323573148, 1590.10661276062, 21069.8323573148, 1515.10661276062, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape31.Items.AddLine(20594.8323573148, 1590.10661276062, 21069.8323573148, 1590.10661276062, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape31);
                        tr.AddNewlyCreatedDBObject(shape31, true);

                        PosShape shape32 = new PosShape();
                        shape32.Name = "11";
                        shape32.Fields = 2;
                        shape32.Formula = "A+B";
                        shape32.FormulaBending = "A+B-0.5*r-d";
                        shape32.Items.AddText(20827.7609287434, 6362.07057885679, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape32.Items.AddText(20953.9464457319, 6360.6261799782, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape32.Items.AddLine(20947.3323573148, 6375.45041530213, 20947.3323573148, 6355.40391219012, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape32.Items.AddLine(20947.3323573148, 6355.40391219012, 20717.3323573148, 6355.40391219012, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape32.Items.AddLine(20594.8323573148, 6322.34530295422, 20594.8323573148, 6397.34530295422, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape32.Items.AddLine(21069.8323573148, 6322.34530295422, 20594.8323573148, 6322.34530295422, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape32.Items.AddLine(21069.8323573148, 6397.34530295422, 21069.8323573148, 6322.34530295422, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape32.Items.AddLine(20594.8323573148, 6397.34530295422, 21069.8323573148, 6397.34530295422, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape32);
                        tr.AddNewlyCreatedDBObject(shape32, true);

                        PosShape shape33 = new PosShape();
                        shape33.Name = "103";
                        shape33.Fields = 5;
                        shape33.Formula = "A+B+C+D";
                        shape33.FormulaBending = "A+B+C+D-0.5*r-d";
                        shape33.Items.AddText(20693.0392817223, 1428.85086071151, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape33.Items.AddText(20756.2258511996, 1411.71802380684, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape33.Items.AddText(20854.0611635714, 1394.19705530455, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape33.Items.AddText(20927.9954260117, 1400.56119817234, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape33.Items.AddText(20617.6658233667, 1395.72616517592, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape33.Items.AddLine(20717.3323573148, 1425.60734802795, 20717.3323573148, 1448.03353264516, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape33.Items.AddLine(20636.789971138, 1417.00763069442, 20634.9790017709, 1417.00763069442, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20634.9790017709, 1425.60734802795, 20636.789971138, 1417.00763069442, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20633.1680324038, 1417.00763069442, 20634.9790017709, 1417.00763069442, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20634.9790017709, 1425.60734802795, 20633.1680324038, 1417.00763069442, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20636.789971138, 1398.78625478636, 20634.9790017709, 1398.78625478636, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20634.9790017709, 1390.18653745283, 20636.789971138, 1398.78625478636, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20633.1680324038, 1398.78625478636, 20634.9790017709, 1398.78625478636, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20634.9790017709, 1390.18653745283, 20633.1680324038, 1398.78625478636, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20947.3323573148, 1389.34873260296, 20947.3323573148, 1421.99563253485, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape33.Items.AddLine(20782.0553056053, 1389.34873260296, 20947.3323573148, 1389.34873260296, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape33.Items.AddLine(20629.3041946974, 1390.18653745283, 20762.4229028474, 1390.18653745283, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20629.3041946974, 1425.60734802795, 20705.7979630397, 1425.60734802795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20634.9790017709, 1390.18653745283, 20634.9790017709, 1425.60734802795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape33.Items.AddLine(20782.0553056053, 1389.34873260296, 20717.3323573148, 1425.60734802795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape33.Items.AddLine(20594.8323573148, 1377.75693589795, 20594.8323573148, 1452.75693589795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape33.Items.AddLine(21069.8323573148, 1452.75693589795, 21069.8323573148, 1377.75693589795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape33.Items.AddLine(20594.8323573148, 1452.75693589795, 21069.8323573148, 1452.75693589795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape33.Items.AddLine(21069.8323573148, 1377.75693589795, 20594.8323573148, 1377.75693589795, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        dict.SetAt("*", shape33);
                        tr.AddNewlyCreatedDBObject(shape33, true);

                        PosShape shape34 = new PosShape();
                        shape34.Name = "104";
                        shape34.Fields = 6;
                        shape34.Formula = "A+B+C+D+E+2*F";
                        shape34.FormulaBending = "A+B+C+D+E+2*F-1.5*r-3*d";
                        shape34.Items.AddText(20812.2302511002, 1278.13659255787, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape34.Items.AddText(20701.6395619284, 1280.10055313544, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape34.Items.AddText(20712.4586947114, 1247.1035177972, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape34.Items.AddText(20961.9312394813, 1257.95827152483, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape34.Items.AddText(20848.8084883239, 1256.37756596659, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBase, true);
                        shape34.Items.AddText(20915.2824125265, 1269.16896243961, 20, "F", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape34.Items.AddLine(20594.8323573148, 1240.40725903528, 20594.8323573148, 1315.40725903528, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape34.Items.AddLine(21069.8323573148, 1240.40725903528, 20594.8323573148, 1240.40725903528, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape34.Items.AddLine(21069.8323573148, 1315.40725903528, 21069.8323573148, 1240.40725903528, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape34.Items.AddLine(20594.8323573148, 1315.40725903528, 21069.8323573148, 1315.40725903528, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape34.Items.AddLine(20737.430558392, 1252.18622228557, 20716.8045204281, 1273.77399132992, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape34.Items.AddLine(20947.3323573148, 1302.8221660779, 20932.0937097944, 1289.24456830759, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape34.Items.AddLine(20947.3323573148, 1302.8221660779, 20716.8045204281, 1302.8221660779, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape34.Items.AddLine(20947.3323573148, 1294.59331212015, 20932.0937097944, 1281.01571434984, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape34.Items.AddLine(20716.8045204281, 1273.77399132992, 20716.8045204281, 1302.8221660779, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape34.Items.AddLine(20947.3323573148, 1252.18622228557, 20947.3323573148, 1294.59331212015, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape34.Items.AddLine(20947.3323573148, 1252.18622228557, 20737.430558392, 1252.18622228557, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape34);
                        tr.AddNewlyCreatedDBObject(shape34, true);

                        PosShape shape35 = new PosShape();
                        shape35.Name = "105";
                        shape35.Fields = 5;
                        shape35.Formula = "A+B+C+D+E";
                        shape35.FormulaBending = "A+B+C+D+E-1.5*r-3*d";
                        shape35.Items.AddText(20750.6816755674, 1148.60279988469, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape35.Items.AddText(20828.0205440269, 1123.92018840534, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape35.Items.AddText(20699.9052720074, 1131.30700143694, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape35.Items.AddText(20970.4836691092, 1120.16229601302, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape35.Items.AddText(20919.7431188701, 1147.08508693121, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape35.Items.AddLine(20594.8323573148, 1103.0575821726, 20594.8323573148, 1178.0575821726, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape35.Items.AddLine(21069.8323573148, 1103.0575821726, 20594.8323573148, 1103.0575821726, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape35.Items.AddLine(21069.8323573148, 1178.0575821726, 21069.8323573148, 1103.0575821726, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape35.Items.AddLine(20594.8323573148, 1178.0575821726, 21069.8323573148, 1178.0575821726, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape35.Items.AddLine(20969.638220532, 1154.48041390915, 20935.9199073278, 1148.89068033546, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape35.Items.AddLine(20717.3323573148, 1161.91923360661, 20747.846170064, 1140.6032981367, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape35.Items.AddLine(20717.3323573148, 1119.17996125098, 20717.3323573148, 1161.91923360661, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape35.Items.AddLine(20947.3323573148, 1119.17996125099, 20969.638220532, 1154.48041390915, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape35.Items.AddLine(20947.3323573148, 1119.17996125099, 20717.3323573148, 1119.17996125098, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape35);
                        tr.AddNewlyCreatedDBObject(shape35, true);

                        PosShape shape36 = new PosShape();
                        shape36.Name = "106";
                        shape36.Fields = 5;
                        shape36.Formula = "A+B+C+D+E";
                        shape36.FormulaBending = "A+B+C+D+E-1.5*r-3*d";
                        shape36.Items.AddText(20744.329295174, 1014.03076553529, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape36.Items.AddText(20828.0205440269, 986.570511542661, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape36.Items.AddText(20699.9052720074, 993.957324574266, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape36.Items.AddText(20954.6270125478, 995.779106804506, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape36.Items.AddText(20901.5469902351, 1012.51305258181, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape36.Items.AddLine(20594.8323573148, 965.707905309926, 20594.8323573148, 1040.70790530993, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape36.Items.AddLine(21069.8323573148, 965.707905309926, 20594.8323573148, 965.707905309926, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape36.Items.AddLine(21069.8323573148, 1040.70790530993, 21069.8323573148, 965.707905309926, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape36.Items.AddLine(20594.8323573148, 1040.70790530993, 21069.8323573148, 1040.70790530993, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape36.Items.AddLine(20934.7679782129, 1021.6525127227, 20914.9458660598, 1007.47997188571, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape36.Items.AddLine(20717.3323573148, 1024.56955674393, 20740.1100146055, 1008.65784236876, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape36.Items.AddLine(20717.3323573148, 981.830284388309, 20717.3323573148, 1024.56955674393, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape36.Items.AddLine(20947.3323573148, 981.83028438831, 20934.7679782129, 1021.6525127227, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape36.Items.AddLine(20947.3323573148, 981.830284388311, 20717.3323573148, 981.830284388309, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape36);
                        tr.AddNewlyCreatedDBObject(shape36, true);

                        PosShape shape37 = new PosShape();
                        shape37.Name = "107";
                        shape37.Fields = 6;
                        shape37.Formula = "A+B+C+D+E+F";
                        shape37.FormulaBending = "A+B+C+D+E+F-1.5*r-3*d";
                        shape37.Items.AddText(20750.6816755674, 875.782122016458, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape37.Items.AddText(20828.0205440269, 841.395402319101, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape37.Items.AddText(20699.9052720074, 848.782215350706, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape37.Items.AddText(20970.4836691092, 837.637509926789, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape37.Items.AddText(20983.5533069405, 874.460371915476, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape37.Items.AddText(20936.6818647934, 871.531742471909, 20, "F", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape37.Items.AddLine(20594.8323573148, 828.358228447251, 20594.8323573148, 903.358228447251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape37.Items.AddLine(21069.8323573148, 828.358228447251, 20594.8323573148, 828.358228447251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape37.Items.AddLine(21069.8323573148, 903.358228447251, 21069.8323573148, 828.358228447251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape37.Items.AddLine(20594.8323573148, 903.358228447251, 21069.8323573148, 903.358228447251, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape37.Items.AddLine(20969.638220532, 893.32526400073, 20949.8161083789, 879.152723163738, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape37.Items.AddLine(20969.638220532, 871.955627822917, 20969.638220532, 893.32526400073, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape37.Items.AddLine(20717.3323573148, 889.098555738376, 20747.846170064, 867.782620268469, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape37.Items.AddLine(20717.3323573148, 836.655175164749, 20717.3323573148, 889.098555738376, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape37.Items.AddLine(20947.3323573148, 836.655175164751, 20969.638220532, 871.955627822917, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape37.Items.AddLine(20947.3323573148, 836.655175164751, 20717.3323573148, 836.655175164749, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape37);
                        tr.AddNewlyCreatedDBObject(shape37, true);

                        PosShape shape38 = new PosShape();
                        shape38.Name = "108";
                        shape38.Fields = 6;
                        shape38.Formula = "A+B+C+D+E+F";
                        shape38.FormulaBending = "A+B+C+D+E+F-1.5*r-3*d";
                        shape38.Items.AddText(20744.329295174, 731.505979449058, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape38.Items.AddText(20828.0205440269, 704.045725456427, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape38.Items.AddText(20699.9052720074, 711.432538488032, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape38.Items.AddText(20954.6270125478, 702.22796887684, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape38.Items.AddText(20944.6776750863, 738.470871620112, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape38.Items.AddText(20881.8393919571, 730.685496717311, 20, "F", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape38.Items.AddLine(20594.8323573148, 691.008551584577, 20594.8323573148, 766.008551584577, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape38.Items.AddLine(21069.8323573148, 691.008551584577, 20594.8323573148, 691.008551584577, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape38.Items.AddLine(21069.8323573148, 766.008551584577, 21069.8323573148, 691.008551584577, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape38.Items.AddLine(20594.8323573148, 766.008551584577, 21069.8323573148, 766.008551584577, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape38.Items.AddLine(20916.8185445656, 749.338404243185, 20896.9964324125, 735.165863406193, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape38.Items.AddLine(20916.8185445656, 749.338404243185, 20947.3323573148, 728.022468773278, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape38.Items.AddLine(20947.3323573148, 699.305498302077, 20947.3323573148, 728.022468773278, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape38.Items.AddLine(20717.3323573148, 742.044770657699, 20740.1100146055, 726.133056282521, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape38.Items.AddLine(20717.3323573148, 699.305498302075, 20717.3323573148, 742.044770657699, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape38.Items.AddLine(20947.3323573148, 699.305498302077, 20717.3323573148, 699.305498302075, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape38);
                        tr.AddNewlyCreatedDBObject(shape38, true);

                        PosShape shape39 = new PosShape();
                        shape39.Name = "109";
                        shape39.Fields = 5;
                        shape39.Formula = "A+B+C+D+E";
                        shape39.FormulaBending = "A+B+C+D+E-1.5*r-3*d";
                        shape39.Items.AddText(20784.4561911414, 569.530726132272, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape39.Items.AddText(20880.4237533728, 570.050172701471, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape39.Items.AddText(20958.5944489078, 587.370943606806, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape39.Items.AddText(20837.5912220025, 596.140293988167, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape39.Items.AddText(20739.501558118, 595.494342821285, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape39.Items.AddLine(20594.8323573148, 553.658874721902, 20594.8323573148, 628.658874721902, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape39.Items.AddLine(21069.8323573148, 553.658874721902, 20594.8323573148, 553.658874721902, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape39.Items.AddLine(21069.8323573148, 628.658874721902, 21069.8323573148, 553.658874721902, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape39.Items.AddLine(20594.8323573148, 628.658874721902, 21069.8323573148, 628.658874721902, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape39.Items.AddLine(20732.5710048353, 606.754977619112, 20717.3323573148, 620.332575389419, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape39.Items.AddLine(20778.3700967787, 578.971808643089, 20763.1314492582, 565.394210872783, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape39.Items.AddLine(20947.3323573148, 620.332575389421, 20717.3323573148, 620.332575389419, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape39.Items.AddLine(20947.3323573148, 565.394210872784, 20947.3323573148, 620.332575389421, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape39.Items.AddLine(20947.3323573148, 565.394210872784, 20763.1314492582, 565.394210872783, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape39);
                        tr.AddNewlyCreatedDBObject(shape39, true);

                        PosShape shape40 = new PosShape();
                        shape40.Name = "110";
                        shape40.Fields = 5;
                        shape40.Formula = "A+B+C+D+E";
                        shape40.FormulaBending = "A+B+C+D+E";
                        shape40.Items.AddText(20738.657099198, 432.181049269597, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape40.Items.AddText(20849.2926466547, 432.700495838797, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape40.Items.AddText(20938.7894184281, 450.021266744131, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape40.Items.AddText(20801.2819994565, 458.790617125493, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape40.Items.AddText(20739.501558118, 458.144665958611, 20, "E", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape40.Items.AddLine(20594.8323573148, 416.309197859227, 20594.8323573148, 491.309197859227, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape40.Items.AddLine(21069.8323573148, 416.309197859227, 20594.8323573148, 416.309197859227, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape40.Items.AddLine(21069.8323573148, 491.309197859227, 21069.8323573148, 416.309197859227, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape40.Items.AddLine(20594.8323573148, 491.309197859227, 21069.8323573148, 491.309197859227, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape40.Items.AddLine(20732.5710048353, 469.405300756437, 20717.3323573148, 482.982898526744, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape40.Items.AddLine(20732.5710048353, 441.622131780415, 20717.3323573148, 428.044534010108, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape40.Items.AddLine(20904.1065822933, 482.982898526746, 20717.3323573148, 482.982898526745, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape40.Items.AddLine(20947.3323573148, 428.04453401011, 20904.1065822933, 482.982898526746, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape40.Items.AddLine(20947.3323573148, 428.04453401011, 20717.3323573148, 428.044534010108, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape40);
                        tr.AddNewlyCreatedDBObject(shape40, true);

                        PosShape shape41 = new PosShape();
                        shape41.Name = "14";
                        shape41.Fields = 3;
                        shape41.Formula = "A+C";
                        shape41.FormulaBending = "A+C-4*d";
                        shape41.Items.AddText(20907.4127557798, 6224.1779279523, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape41.Items.AddText(21039.7419376633, 6216.73157214451, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape41.Items.AddText(20828.0466430291, 6207.7846718366, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape41.Items.AddLine(21069.8323573148, 6184.99562609154, 20594.8323573148, 6184.99562609154, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape41.Items.AddLine(21069.8323573148, 6259.99562609154, 21069.8323573148, 6184.99562609154, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape41.Items.AddLine(20594.8323573148, 6184.99562609154, 20594.8323573148, 6259.99562609154, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape41.Items.AddLine(20594.8323573148, 6259.99562609154, 21069.8323573148, 6259.99562609154, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape41.Items.AddLine(21031.4966822258, 6241.55424570694, 21029.6857128588, 6241.55424570694, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21029.6857128588, 6250.15396304046, 21031.4966822258, 6241.55424570694, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21027.8747434917, 6241.55424570694, 21029.6857128588, 6241.55424570694, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21029.6857128588, 6250.15396304046, 21027.8747434917, 6241.55424570694, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21031.4966822258, 6209.71772250346, 21029.6857128588, 6209.71772250346, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21029.6857128588, 6201.11800516994, 21031.4966822258, 6209.71772250346, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21027.8747434917, 6209.71772250346, 21029.6857128588, 6209.71772250346, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21029.6857128588, 6201.11800516994, 21027.8747434917, 6209.71772250346, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(20858.635065561, 6250.15396304047, 21035.3605199323, 6250.15396304047, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(20962.3645112888, 6201.11800516993, 21035.3605199323, 6201.11800516993, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(21029.6857128588, 6250.15396304047, 21029.6857128588, 6201.11800516993, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape41.Items.AddLine(20825.0398505022, 6250.15396304047, 20947.3323573148, 6201.11800516993, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape41.Items.AddLine(20947.3323573148, 6201.11800516993, 20717.3323573148, 6201.11800516993, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape41);
                        tr.AddNewlyCreatedDBObject(shape41, true);

                        PosShape shape42 = new PosShape();
                        shape42.Name = "15";
                        shape42.Fields = 3;
                        shape42.Formula = "A+C";
                        shape42.FormulaBending = "A+C";
                        shape42.Items.AddText(20921.4497450229, 6072.43058003508, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape42.Items.AddText(21039.7419376633, 6079.18548420931, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape42.Items.AddText(20780.0853901998, 6070.43499497392, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape42.Items.AddLine(21069.8323573148, 6122.64594922887, 21069.8323573148, 6047.64594922887, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape42.Items.AddLine(20594.8323573148, 6047.64594922887, 20594.8323573148, 6122.64594922887, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape42.Items.AddLine(21069.8323573148, 6047.64594922887, 20594.8323573148, 6047.64594922887, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape42.Items.AddLine(20594.8323573148, 6122.64594922887, 21069.8323573148, 6122.64594922887, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape42.Items.AddLine(21031.4966822258, 6072.36804564079, 21029.6857128588, 6072.36804564079, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21029.6857128588, 6063.76832830726, 21031.4966822258, 6072.36804564079, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21027.8747434917, 6072.36804564079, 21029.6857128588, 6072.36804564079, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21029.6857128588, 6063.76832830726, 21027.8747434917, 6072.36804564079, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21031.4966822258, 6104.20456884427, 21029.6857128588, 6104.20456884426, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21029.6857128588, 6112.80428617779, 21031.4966822258, 6104.20456884427, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21027.8747434917, 6104.20456884427, 21029.6857128588, 6104.20456884426, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21029.6857128588, 6112.80428617779, 21027.8747434917, 6104.20456884427, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(20960.3769853909, 6112.8042861778, 21035.3605199323, 6112.8042861778, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(20879.4401361552, 6063.76832830725, 21035.3605199323, 6063.76832830725, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(21029.6857128588, 6112.8042861778, 21029.6857128588, 6063.76832830725, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape42.Items.AddLine(20947.3323573148, 6112.8042861778, 20860.6029058239, 6063.76832830725, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape42.Items.AddLine(20860.6029058239, 6063.76832830725, 20716.8045204281, 6063.76832830725, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape42);
                        tr.AddNewlyCreatedDBObject(shape42, true);

                        PosShape shape43 = new PosShape();
                        shape43.Name = "21";
                        shape43.Fields = 3;
                        shape43.Formula = "A+B+C";
                        shape43.FormulaBending = "A+B+C-r-2*d";
                        shape43.Items.AddText(20701.0245416723, 5935.71885058676, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape43.Items.AddText(20828.7365008954, 5933.08531811125, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape43.Items.AddText(20955.1584107996, 5935.79576335244, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape43.Items.AddLine(20594.8323573148, 5910.2962723662, 20594.8323573148, 5985.2962723662, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape43.Items.AddLine(21069.8323573148, 5910.2962723662, 20594.8323573148, 5910.2962723662, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape43.Items.AddLine(21069.8323573148, 5985.2962723662, 21069.8323573148, 5910.2962723662, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape43.Items.AddLine(20594.8323573148, 5985.2962723662, 21069.8323573148, 5985.2962723662, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape43.Items.AddLine(20717.3323573148, 5926.41865144458, 20717.3323573148, 5969.1579238002, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape43.Items.AddLine(20947.3323573148, 5926.41865144458, 20947.3323573148, 5969.1579238002, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape43.Items.AddLine(20947.3323573148, 5926.41865144458, 20717.3323573148, 5926.41865144458, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape43);
                        tr.AddNewlyCreatedDBObject(shape43, true);

                        PosShape shape44 = new PosShape();
                        shape44.Name = "23";
                        shape44.Fields = 3;
                        shape44.Formula = "A+B+C";
                        shape44.FormulaBending = "A+B+C-r-2*d";
                        shape44.Items.AddText(20701.0245416723, 5814.60480258623, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape44.Items.AddText(20829.3729707713, 5786.83144177831, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape44.Items.AddText(20955.1584107996, 5794.85525626796, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape44.Items.AddLine(21069.8323573148, 5772.94659550352, 20594.8323573148, 5772.94659550352, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape44.Items.AddLine(20594.8323573148, 5772.94659550352, 20594.8323573148, 5847.94659550352, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape44.Items.AddLine(21069.8323573148, 5847.94659550352, 21069.8323573148, 5772.94659550352, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape44.Items.AddLine(20594.8323573148, 5847.94659550352, 21069.8323573148, 5847.94659550352, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape44.Items.AddLine(20717.3323573148, 5810.43861075972, 20717.3323573148, 5831.80824693753, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape44.Items.AddLine(20947.3323573148, 5789.06897458191, 20947.3323573148, 5810.43861075972, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape44.Items.AddLine(20947.3323573148, 5810.43861075972, 20717.3323573148, 5810.43861075972, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape44);
                        tr.AddNewlyCreatedDBObject(shape44, true);

                        PosShape shape45 = new PosShape();
                        shape45.Name = "24";
                        shape45.Fields = 4;
                        shape45.Formula = "A+B+C";
                        shape45.FormulaBending = "A+B+C";
                        shape45.Items.AddText(20779.0945483886, 5658.3859643859, 20, "A", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextCenter, TextVerticalMode.TextBottom, true);
                        shape45.Items.AddText(20877.7127624305, 5666.36266509933, 20, "B", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextRight, TextVerticalMode.TextBottom, true);
                        shape45.Items.AddText(20955.8336624143, 5682.27188564164, 20, "C", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape45.Items.AddText(21039.8088913267, 5654.43974558179, 20, "D", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2), TextHorizontalMode.TextLeft, TextVerticalMode.TextBottom, true);
                        shape45.Items.AddLine(21069.8323573148, 5635.59691864085, 20594.8323573148, 5635.59691864085, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape45.Items.AddLine(20594.8323573148, 5635.59691864085, 20594.8323573148, 5710.59691864085, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape45.Items.AddLine(21069.8323573148, 5710.59691864085, 21069.8323573148, 5635.59691864085, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape45.Items.AddLine(20594.8323573148, 5710.59691864085, 21069.8323573148, 5710.59691864085, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 7), false);
                        shape45.Items.AddLine(21031.4966822258, 5660.31901505276, 21029.6857128588, 5660.31901505276, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21029.6857128588, 5651.71929771923, 21031.4966822258, 5660.31901505276, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21027.8747434917, 5660.31901505276, 21029.6857128588, 5660.31901505276, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21029.6857128588, 5651.71929771923, 21027.8747434917, 5660.31901505276, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21031.4966822258, 5667.63755932097, 21029.6857128588, 5667.63755932097, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21029.6857128588, 5676.2372766545, 21031.4966822258, 5667.63755932097, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21027.8747434917, 5667.63755932097, 21029.6857128588, 5667.63755932097, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21029.6857128588, 5676.2372766545, 21027.8747434917, 5667.63755932097, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(20947.3323573148, 5676.2372766545, 20947.3323573148, 5700.75525558977, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape45.Items.AddLine(20957.5458986749, 5676.2372766545, 21035.3605199323, 5676.2372766545, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(20885.4220154942, 5651.71929771923, 21035.3605199323, 5651.71929771923, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(21029.6857128588, 5676.2372766545, 21029.6857128588, 5651.71929771923, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1), true);
                        shape45.Items.AddLine(20947.3323573148, 5676.2372766545, 20860.6029058239, 5651.71929771923, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        shape45.Items.AddLine(20860.6029058239, 5651.71929771923, 20717.3323573148, 5651.71929771923, Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4), true);
                        dict.SetAt("*", shape45);
                        tr.AddNewlyCreatedDBObject(shape45, true);

                        dict.DowngradeOpen();
                    }

                    bool hasDefault = false;
                    using (DbDictionaryEnumerator it = dict.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            PosShape item = (PosShape)tr.GetObject(it.Value, OpenMode.ForRead);
                            if (item.Name == "GENEL")
                            {
                                hasDefault = true;
                                id = it.Value;
                                break;
                            }
                        }
                    }

                    if (!hasDefault)
                    {
                        dict.UpgradeOpen();

                        PosShape shape0 = new PosShape();
                        shape0.Name = "GENEL";
                        shape0.Fields = 1;
                        shape0.Formula = "A";
                        shape0.FormulaBending = "A";
                        id = dict.SetAt("*", shape0);
                        tr.AddNewlyCreatedDBObject(shape0, true);

                        dict.DowngradeOpen();
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

        // Creates default groups
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
                        group.Formula = "[M:C][N][\"T\":D][\"/\":S]";
                        group.FormulaLengthOnly = "[\"L=\":L]";
                        group.FormulaPosOnly = "[M:C]";
                        group.StandardDiameters = "8 10 12 14 16 18 20 22 25 26 32 36";
                        group.HiddenLayerId = DWGUtility.CreateHiddenLayer("Rebar Defpoints", Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 8));
                        group.TextStyleId = DWGUtility.CreateTextStyle("Rebar Text Style", "leroy.shx", 0.7);
                        group.NoteStyleId = DWGUtility.CreateTextStyle("Rebar Note Style", "simplxtw.shx", 0.9);
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
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                return id;
            }
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
                        BOQStyle style = new BOQStyle();

                        style.Name = "TableStyle1";
                        style.Columns = "[M][N][D][L][SH][TL]";

                        style.TextStyleId = DWGUtility.CreateTextStyle("Rebar BOQ Style", "leroy.shx", 0.7);
                        style.HeadingStyleId = DWGUtility.CreateTextStyle("Rebar BOQ Heading Style", "Arial.ttf", 1.0);
                        style.FootingStyleId = DWGUtility.CreateTextStyle("Rebar BOQ Footing Style", "simplxtw.shx", 1.0);

                        style.Precision = 2;

                        style.PosLabel = "POS";
                        style.CountLabel = "NO.";
                        style.DiameterLabel = "DIA";
                        style.LengthLabel = "LENGTH\\P([U])";
                        style.ShapeLabel = "SHAPE";
                        style.TotalLengthLabel = "TOTAL LENGTH (m)";
                        style.DiameterListLabel = "T[D]";
                        style.DiameterLengthLabel = "TOTAL LENGTH (m)";
                        style.UnitWeightLabel = "UNIT WEIGHT (kg/m)";
                        style.WeightLabel = "WEIGHT (kg)";
                        style.GrossWeightLabel = "TOTAL WEIGHT (kg)";

                        dict.UpgradeOpen();
                        id = dict.SetAt("*", style);
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
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                return id;
            }
        }

        // Creates a new non plot layer and returns the ObjectId of the new layer
        public static ObjectId CreateHiddenLayer(string name, Autodesk.AutoCAD.Colors.Color color)
        {
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable table = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);
                    if (!table.Has(name))
                    {
                        LayerTableRecord style = new LayerTableRecord();
                        style.Name = name;
                        style.Color = color;
                        style.IsPlottable = false;
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
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return id;
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
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return id;
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

        // Returns the name of the group
        public static string GetGroupName(ObjectId id)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup item = tr.GetObject(id, OpenMode.ForRead) as PosGroup;
                    if (item != null) return item.Name;
                }
                catch
                {
                    ;
                }
            }
            return "";
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

        // Refreshes all items in group
        public static void RefreshPosInGroup(ObjectId id)
        {
            RefreshPos(GetPosInGroup(id));
        }

        // Refreshes all items with the given shape
        public static void RefreshPosWithShape(ObjectId id)
        {
            RefreshPos(GetPosWithShape(id));
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
    }
}
