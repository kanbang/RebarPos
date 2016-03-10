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

        public static CountOverrule Instance { get { return mInstance; } }

        private CountOverrule()
        {
            mColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 1);
            SetCustomFilter();
        }

        public void SetProperties(Autodesk.AutoCAD.Colors.Color color)
        {
            mColor = color;
        }

        public override bool IsApplicable(RXObject overruledSubject)
        {
            // Shade current group only
            RebarPos pos = overruledSubject as RebarPos;
            if (pos == null) return false;
            return (!pos.IncludeInBOQ || pos.Detached);
        }

        public override bool WorldDraw(Drawable drawable, WorldDraw wd)
        {
            if (wd.RegenAbort || wd.IsDragging)
            {
                return base.WorldDraw(drawable, wd);
            }

            RebarPos pos = drawable as RebarPos;
            if (pos == null || (pos.IncludeInBOQ && !pos.Detached))
            {
                return base.WorldDraw(drawable, wd);
            }

            // Get geometry
            Point3d minpt;
            Point3d maxpt;
            pos.TextBox(out minpt, out maxpt);
            minpt = minpt.DivideBy(pos.Scale);
            maxpt = maxpt.DivideBy(pos.Scale);

            using (Solid solid = new Solid())
            {
                solid.SetPointAt(0, new Point3d(minpt.X - 0.15, minpt.Y - 0.15, 0));
                solid.SetPointAt(1, new Point3d(maxpt.X + 0.15, minpt.Y - 0.15, 0));
                solid.SetPointAt(2, new Point3d(minpt.X - 0.15, maxpt.Y + 0.15, 0));
                solid.SetPointAt(3, new Point3d(maxpt.X + 0.15, maxpt.Y + 0.15, 0));
                solid.Color = mColor;
                solid.LayerId = PosUtility.DefpointsLayer;

                Matrix3d trans = Matrix3d.AlignCoordinateSystem(
                    Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis,
                    pos.BasePoint, pos.DirectionVector, pos.UpVector, pos.NormalVector);

                solid.TransformBy(trans);
                wd.Geometry.Draw(solid);
            }

            // Draw the entity over shading
            return base.WorldDraw(drawable, wd);
        }
    }
}
