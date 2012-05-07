using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Colors;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public class DrawOverrule : DrawableOverrule
    {
        private static DrawOverrule mInstance = new DrawOverrule();

        public static DrawOverrule Instance { get { return mInstance; } }
        public ObjectId CurrentGroupId { get; set; }
        public Autodesk.AutoCAD.Colors.Color CurrentGroupHightlightColor { get; set; }

        public override bool WorldDraw(Drawable drawable, WorldDraw wd)
        {
            if (wd.RegenAbort || wd.IsDragging)
            {
                return base.WorldDraw(drawable, wd);
            }

            if (drawable is RebarPos)
            {
                RebarPos pos = drawable as RebarPos;

                // Get geometry
                Point3d pt = pos.BasePoint;
                Vector3d dir = pos.DirectionVector;
                Vector3d up = pos.UpVector;
                Vector3d norm = pos.NormalVector;

                double w = pos.Width / dir.Length;
                double h = pos.Height / dir.Length;

                // Shade current group
                if (pos.GroupId == CurrentGroupId)
                {
                    Matrix3d trans = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, pt, dir, up, norm);

                    // Transform to match text orientation
                    wd.Geometry.PushModelTransform(trans);
                    FillType filltype = wd.SubEntityTraits.FillType;
                    wd.SubEntityTraits.FillType = FillType.FillAlways;
                    // TODO: Set to defpoints
                    wd.SubEntityTraits.Color = CurrentGroupHightlightColor.ColorIndex;
                    Point3dCollection rec = new Point3dCollection();
                    rec.Add(new Point3d(-0.15, -0.15, 0));
                    rec.Add(new Point3d(w + .15, -0.15, 0));
                    rec.Add(new Point3d(w + .15, h + .15, 0));
                    rec.Add(new Point3d(-0.15, h + .15, 0));
                    wd.Geometry.Polygon(rec);
                    wd.SubEntityTraits.FillType = filltype;
                    wd.Geometry.PopModelTransform();

                    // Draw the entity over shading
                    base.WorldDraw(drawable, wd);

                    return true;
                }
            }

            return base.WorldDraw(drawable, wd);
        }
    }
}
