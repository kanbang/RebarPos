using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Colors;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public class ShowShapesOverrule : DrawableOverrule
    {
        private static ShowShapesOverrule mInstance = new ShowShapesOverrule();
        private Dictionary<string, bool> mIds;

        public static ShowShapesOverrule Instance { get { return mInstance; } }

        public ShowShapesOverrule()
        {
            mIds = new Dictionary<string, bool>();
            SetCustomFilter();
        }

        public void Add()
        {
            string key = HostApplicationServices.WorkingDatabase.FingerprintGuid;
            if (!mIds.ContainsKey(key))
                mIds.Add(key, false);
        }

        public void Remove()
        {
            mIds.Remove(HostApplicationServices.WorkingDatabase.FingerprintGuid);
        }

        public bool Has()
        {
            return mIds.ContainsKey(HostApplicationServices.WorkingDatabase.FingerprintGuid);
        }

        public override bool IsApplicable(RXObject overruledSubject)
        {
            RebarPos pos = overruledSubject as RebarPos;
            Database db = pos.Database;
            if (db == null) return false;
            return (mIds.ContainsKey(db.FingerprintGuid));
        }

        public override bool WorldDraw(Drawable drawable, WorldDraw wd)
        {
            if (wd.RegenAbort || wd.IsDragging)
            {
                return base.WorldDraw(drawable, wd);
            }

            RebarPos pos = drawable as RebarPos;
            if (pos == null)
            {
                return base.WorldDraw(drawable, wd);
            }

            PosShape shape = PosShape.GetPosShape(pos.Shape);
            Extents3d? shapeExt = shape.Bounds;
            if (!shapeExt.HasValue)
            {
                return base.WorldDraw(drawable, wd);
            }
            Extents3d ext = shapeExt.Value;

            WorldGeometry g = wd.Geometry;
            SubEntityTraits s = wd.SubEntityTraits;
            TextStyle style = new TextStyle();

            // Get geometry
            Point3d pt = pos.BasePoint;
            Vector3d dir = pos.DirectionVector;
            Vector3d up = pos.UpVector;
            Vector3d norm = pos.NormalVector;

            double w = pos.Width / dir.Length;
            double h = pos.Height / dir.Length;

            // Draw shape
            double xmin = ext.MinPoint.X, ymin = ext.MinPoint.Y, xmax = ext.MaxPoint.X, ymax = ext.MaxPoint.Y;

            // Scale
            double scale = 0.025;
            // Client offsets
            double xoff = (w - scale * (xmax - xmin)) / 2.0;
            double yoff = 2.0 * h;//(h - scale * (ymax - ymin)) / 2.0;

            // Transform
            Matrix3d trans = Matrix3d.AlignCoordinateSystem(new Point3d(xmin, ymin, 0), Vector3d.XAxis / scale, Vector3d.YAxis / scale, Vector3d.ZAxis / scale, pt + dir * xoff + up * yoff, dir, up, norm);
            g.PushModelTransform(trans);

            // Draw shapes
            g.Draw(shape);

            // Reset transform
            g.PopModelTransform();

            return base.WorldDraw(drawable, wd);
        }
    }
}
