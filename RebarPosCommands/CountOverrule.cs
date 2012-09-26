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
    public class CountOverrule : DrawableOverrule
    {
        private static CountOverrule mInstance = new CountOverrule();
        private Autodesk.AutoCAD.Colors.Color mColor;
        private ObjectId mLayer;

        public static CountOverrule Instance { get { return mInstance; } }

        public CountOverrule()
        {
            mColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 1);
            mLayer = ObjectId.Null;
            SetCustomFilter();
        }

        public void SetProperties(Autodesk.AutoCAD.Colors.Color color, ObjectId layer)
        {
            mColor = color;
            mLayer = layer;
        }

        public override bool IsApplicable(RXObject overruledSubject)
        {
            // Shade current group only
            RebarPos pos = overruledSubject as RebarPos;
            return (!pos.IncludeInBOQ);
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
            if (pos.IncludeInBOQ)
            {
                return base.WorldDraw(drawable, wd);
            }

            WorldGeometry g = wd.Geometry;

            // Get geometry
            Point3d pt = pos.BasePoint;
            Vector3d dir = pos.DirectionVector;
            Vector3d up = pos.UpVector;
            Vector3d norm = pos.NormalVector;

            double w = pos.Width / dir.Length;
            double h = pos.Height / dir.Length;

            // Transform to object orientation
            Matrix3d trans = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, pt, dir, up, norm);

            Solid solid = new Solid();
            solid.SetPointAt(0, new Point3d(-0.15, -0.15, 0));
            solid.SetPointAt(1, new Point3d(w + .15, -0.15, 0));
            solid.SetPointAt(2, new Point3d(-0.15, h + .15, 0));
            solid.SetPointAt(3, new Point3d(w + .15, h + .15, 0));
            solid.Color = mColor;
            solid.LayerId = mLayer;
            solid.TransformBy(trans);
            g.Draw(solid);

            // Draw the entity over shading
            base.WorldDraw(drawable, wd);

            return true;
        }
    }
}
