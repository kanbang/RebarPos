using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;

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
        public ObjectId shapeId;
        public string shapename;
        public double x;
        public double y;
        public double length1;
        public double length2;
        public bool isVarLength;
        public bool existing;

        public PosCopy()
        {
            list = new List<ObjectId>();
            x = double.MaxValue;
            y = double.MaxValue;
            count = 0;
            priority = -1;
            length = string.Empty;
            length1 = 0.0;
            length2 = 0.0;
            isVarLength = false;
            pos = string.Empty;
            newpos = string.Empty;
            shapeId = ObjectId.Null;
            shapename = string.Empty;
            diameter = string.Empty;
            key = string.Empty;
            existing = false;
        }

        public enum PosGrouping
        {
            None = 0,
            PosKey = 1,
            PosMarker = 2
        }

        public static List<PosCopy> ReadAllInSelection(IEnumerable<ObjectId> items, PosGrouping grouping)
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
                        PosCopy copy = null;
                        if (grouping == PosGrouping.PosKey)
                        {
                            copy = poslist.Find(p => p.key == pos.PosKey); ;
                        }
                        else if (grouping == PosGrouping.PosMarker)
                        {
                            copy = poslist.Find(p => p.pos == pos.Pos); ;
                        }

                        if (copy != null)
                        {
                            copy.list.Add(id);
                            if (pos.IncludeInBOQ)
                            {
                                copy.count += pos.CalcProperties.Count * pos.Multiplier;
                            }
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
                            copy.x = pos.BasePoint.X;
                            copy.y = pos.BasePoint.Y;
                            copy.shapeId = pos.ShapeId;
                            PosShape shape = tr.GetObject(copy.shapeId, OpenMode.ForRead) as PosShape;
                            if (shape != null)
                            {
                                copy.priority = shape.Priority;
                                copy.shapename = shape.Name;
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

        public static List<PosCopy> ReadAll(PosGrouping grouping)
        {
            List<PosCopy> poslist = new List<PosCopy>();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                {
                    while (it.MoveNext())
                    {
                        RebarPos pos = tr.GetObject(it.Current, OpenMode.ForRead) as RebarPos;
                        if (pos != null)
                        {
                            PosCopy copy = null;
                            if (grouping == PosGrouping.PosKey)
                            {
                                copy = poslist.Find(p => p.key == pos.PosKey); ;
                            }
                            else if (grouping == PosGrouping.PosMarker)
                            {
                                copy = poslist.Find(p => p.pos == pos.Pos); ;
                            }

                            if (copy != null)
                            {
                                copy.list.Add(it.Current);
                                if (pos.IncludeInBOQ)
                                {
                                    copy.count += pos.CalcProperties.Count * pos.Multiplier;
                                }
                                copy.x = Math.Min(copy.x, pos.BasePoint.X);
                                copy.y = Math.Min(copy.y, pos.BasePoint.Y);
                            }
                            else
                            {
                                copy = new PosCopy();
                                copy.key = pos.PosKey;
                                copy.list.Add(it.Current);
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
                                copy.x = pos.BasePoint.X;
                                copy.y = pos.BasePoint.Y;
                                copy.shapeId = pos.ShapeId;
                                PosShape shape = tr.GetObject(copy.shapeId, OpenMode.ForRead) as PosShape;
                                if (shape != null)
                                {
                                    copy.priority = shape.Priority;
                                    copy.shapename = shape.Name;
                                }
                                copy.length1 = pos.CalcProperties.MinLength;
                                copy.length2 = pos.CalcProperties.MaxLength;
                                copy.isVarLength = pos.CalcProperties.IsVarLength;
                                poslist.Add(copy);
                            }
                        }
                    }
                }
            }

            return poslist;
        }
    }
}
