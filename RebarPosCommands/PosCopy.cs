using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.Runtime;

namespace RebarPosCommands
{
    public class PosCopy
    {
        public List<ObjectId> list;

        public string key;

        public string pos;
        public string newpos;

        public int count;
        public int priority;
        public string diameter;

        public string length;
        public string a;
        public string b;
        public string c;
        public string d;
        public string e;
        public string f;

        public string shapename;

        public double scale;

        public double x;
        public double y;

        public double length1;
        public double length2;
        public bool isVarLength;

        public bool existing;

        public PosCopy()
        {
            list = new List<ObjectId>();

            key = string.Empty;

            pos = string.Empty;
            newpos = string.Empty;

            count = 0;
            priority = -1;
            diameter = string.Empty;

            length = string.Empty;
            a = string.Empty;
            b = string.Empty;
            c = string.Empty;
            d = string.Empty;
            e = string.Empty;
            f = string.Empty;

            shapename = string.Empty;

            scale = 0;
            x = double.MaxValue;
            y = double.MaxValue;

            length1 = 0.0;
            length2 = 0.0;
            isVarLength = false;

            existing = false;
        }

        public enum PosGrouping
        {
            None = 0,
            PosKey = 1,
            PosMarker = 2,
            PosKeyDifferentMarker = 3
        }

        public static List<PosCopy> ReadAllInSelection(IEnumerable<ObjectId> items, bool skipEmpty, PosGrouping grouping)
        {
            List<PosCopy> poslist = new List<PosCopy>();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                foreach (ObjectId id in items)
                {
                    RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                    if (pos != null)
                    {
                        // Skip empty pos numbers
                        if (skipEmpty && string.IsNullOrEmpty(pos.Pos)) continue;
                        // Skip detached pos
                        if (pos.Detached) continue;

                        PosCopy copy = null;
                        if (grouping == PosGrouping.PosKey)
                        {
                            copy = poslist.Find(p => p.key == pos.PosKey);
                        }
                        else if (grouping == PosGrouping.PosMarker)
                        {
                            copy = poslist.Find(p => p.pos == pos.Pos);
                        }
                        else if (grouping == PosGrouping.PosKeyDifferentMarker)
                        {
                            copy = poslist.Find(p => p.key == pos.PosKey && p.pos == pos.Pos);
                        }

                        if (copy != null)
                        {
                            copy.list.Add(id);
                            if (pos.IncludeInBOQ)
                            {
                                copy.count += pos.CalcProperties.Count * pos.Multiplier;
                            }
                            copy.scale = Math.Max(copy.scale, pos.Scale);
                            copy.x = Math.Min(copy.x, pos.BasePoint.X);
                            copy.y = Math.Min(copy.y, pos.BasePoint.Y);
                        }
                        else
                        {
                            copy = new PosCopy();
                            copy.key = pos.PosKey;
                            copy.list.Add(id);
                            copy.pos = pos.Pos;
                            copy.newpos = pos.Pos;
                            copy.existing = true;
                            if (pos.IncludeInBOQ)
                            {
                                copy.count = pos.CalcProperties.Count * pos.Multiplier;
                            }
                            copy.diameter = pos.Diameter;
                            copy.length = pos.Length;
                            copy.a = pos.A;
                            copy.b = pos.B;
                            copy.c = pos.C;
                            copy.d = pos.D;
                            copy.e = pos.E;
                            copy.f = pos.F;
                            copy.scale = pos.Scale;
                            copy.x = pos.BasePoint.X;
                            copy.y = pos.BasePoint.Y;
                            copy.shapename = pos.Shape;
                            PosShape shape = PosShape.GetPosShape(copy.shapename);
                            if (shape != null)
                            {
                                copy.priority = shape.Priority;
                            }
                            copy.length1 = pos.CalcProperties.MinLength;
                            copy.length2 = pos.CalcProperties.MaxLength;
                            copy.isVarLength = pos.CalcProperties.IsVarLength;
                            poslist.Add(copy);
                        }
                    }
                }
            }

            return poslist;
        }

        public static List<PosCopy> ReadAll(PosGrouping grouping, bool skipEmpty)
        {
            List<ObjectId> items = new List<ObjectId>();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                {
                    while (it.MoveNext())
                    {
                        if (it.Current.ObjectClass == RXObject.GetClass(typeof(RebarPos)))
                        {
                            items.Add(it.Current);
                        }
                    }
                }
            }

            return ReadAllInSelection(items, skipEmpty, grouping);
        }
    }
}
