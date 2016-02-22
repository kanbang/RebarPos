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
        private static SelectionFilter SSPosFilter(bool includeDetached)
        {
            if (includeDetached)
            {
                TypedValue[] tvs = new TypedValue[] {
                    new TypedValue((int)DxfCode.Start, "REBARPOS")
                };
                return new SelectionFilter(tvs);
            }
            else
            {
                TypedValue[] tvs = new TypedValue[] {
                    new TypedValue((int)DxfCode.Start, "REBARPOS"),
                    new TypedValue((int)(DxfCode.Bool + 1), false)
                };
                return new SelectionFilter(tvs);
            }
        }

        private static SelectionFilter SSPosFilter()
        {
            return SSPosFilter(false);
        }

        private static SelectionFilter SSPosAndBOQTableFilter()
        {
            TypedValue[] tvs = new TypedValue[] {
                new TypedValue((int)DxfCode.Operator, "<OR"),
                new TypedValue((int)DxfCode.Start, "REBARPOS"),
                new TypedValue((int)DxfCode.Start, "BOQTABLE"),
                new TypedValue((int)DxfCode.Operator, "OR>"),
            };
            return new SelectionFilter(tvs);
        }

        public static PromptSelectionResult SelectAllPosUser(bool includeDetached)
        {
            try
            {
                return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(SSPosFilter(includeDetached));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static PromptSelectionResult SelectAllPosUser()
        {
            return SelectAllPosUser(false);
        }

        public static PromptSelectionResult SelectAllPosAndBOQTableUser()
        {
            try
            {
                return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(SSPosAndBOQTableFilter());
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

        public static void DrawShape(string name, Point3d inspt, double height)
        {
            DrawShape(PosShape.GetPosShape(name), inspt, height);
        }

        public static void DrawShape(PosShape shape, Point3d inspt, double height)
        {
            Extents3d? bounds = shape.Bounds;
            if (!bounds.HasValue) return;
            Point3d p1 = bounds.Value.MinPoint;
            Point3d p2 = bounds.Value.MaxPoint;
            double scale = height / (p2.Y - p1.Y);
            Matrix3d trans = Matrix3d.AlignCoordinateSystem(p1, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis,
                inspt, Vector3d.XAxis * scale, Vector3d.YAxis * scale, Vector3d.ZAxis * scale);

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    Point3dCollection vertices = new Point3dCollection(new Point3d[]{
                            new Point3d(p1.X, p1.Y, 0),
                            new Point3d(p2.X, p1.Y, 0),
                            new Point3d(p2.X, p2.Y, 0),
                            new Point3d(p1.X, p2.Y, 0),
                        });
                    Polyline2d rec = new Polyline2d(Poly2dType.SimplePoly, vertices, 0, true, 0, 0, null);
                    rec.TransformBy(trans);
                    btr.AppendEntity(rec);
                    tr.AddNewlyCreatedDBObject(rec, true);

                    ObjectId hiddenLayer = PosUtility.DefpointsLayer;

                    foreach (PosShape.Shape item in shape.Items)
                    {
                        Entity en = null;

                        if (item is PosShape.ShapeLine)
                        {
                            PosShape.ShapeLine line = (PosShape.ShapeLine)item;
                            en = new Line(new Point3d(line.X1, line.Y1, 0), new Point3d(line.X2, line.Y2, 0));
                        }
                        else if (item is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc arc = (PosShape.ShapeArc)item;
                            en = new Arc(new Point3d(arc.X, arc.Y, 0), arc.R, arc.StartAngle, arc.EndAngle);
                        }
                        else if (item is PosShape.ShapeCircle)
                        {
                            PosShape.ShapeCircle circle = (PosShape.ShapeCircle)item;
                            en = new Circle(new Point3d(circle.X, circle.Y, 0), Vector3d.ZAxis, circle.R);
                        }
                        else if (item is PosShape.ShapeText)
                        {
                            PosShape.ShapeText text = (PosShape.ShapeText)item;
                            DBText dbobj = new DBText();
                            dbobj.TextString = text.Text;
                            dbobj.Position = new Point3d(text.X, text.Y, 0);
                            dbobj.TextStyleId = PosUtility.CreateTextStyle("ShapeDump_" + shape.Name, text.Font, text.Width);
                            dbobj.Height = text.Height;
                            dbobj.WidthFactor = text.Width;
                            dbobj.HorizontalMode = text.HorizontalAlignment;
                            if (text.VerticalAlignment == TextVerticalMode.TextBottom)
                                dbobj.VerticalMode = TextVerticalMode.TextBase;
                            else
                                dbobj.VerticalMode = text.VerticalAlignment;

                            if (dbobj.HorizontalMode != TextHorizontalMode.TextLeft || dbobj.VerticalMode != TextVerticalMode.TextBase)
                            {
                                dbobj.AlignmentPoint = new Point3d(text.X, text.Y, 0);
                            }
                            en = dbobj;
                        }

                        if (en != null)
                        {
                            en.Color = item.Color;
                            if (!item.Visible) en.LayerId = hiddenLayer;
                            en.TransformBy(trans);

                            btr.AppendEntity(en);
                            tr.AddNewlyCreatedDBObject(en, true);
                        }
                    }

                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
    }
}
