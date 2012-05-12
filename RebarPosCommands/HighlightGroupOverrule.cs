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
    public class HighlightGroupOverrule : DrawableOverrule
    {
        private static HighlightGroupOverrule mInstance = new HighlightGroupOverrule();
        private Dictionary<ObjectId, Autodesk.AutoCAD.Colors.Color> mGroups;

        public static HighlightGroupOverrule Instance { get { return mInstance; } }

        public HighlightGroupOverrule()
        {
            mGroups = new Dictionary<ObjectId, Autodesk.AutoCAD.Colors.Color>();
            SetCustomFilter();
        }
        public void AddGroup(ObjectId id, Autodesk.AutoCAD.Colors.Color color)
        {
            if(!mGroups.ContainsKey(id))
                mGroups.Add(id, color);
        }

        public void RemoveGroup(ObjectId id)
        {
            mGroups.Remove(id);
        }

        public override bool IsApplicable(RXObject overruledSubject)
        {
            // Shade current group only
            RebarPos pos = overruledSubject as RebarPos;
            return (mGroups.ContainsKey(pos.GroupId));
        }

        public override bool WorldDraw(Drawable drawable, WorldDraw wd)
        {
            if (wd.RegenAbort || wd.IsDragging)
            {
                return base.WorldDraw(drawable, wd);
            }

            RebarPos pos = drawable as RebarPos;

            // Get color
            short color = 0;
            bool found = false;
            foreach (KeyValuePair<ObjectId, Autodesk.AutoCAD.Colors.Color> item in mGroups)
            {
                if (item.Key.Database == pos.Database)
                {
                    color = item.Value.ColorIndex;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return base.WorldDraw(drawable, wd);
            }

            WorldGeometry g = wd.Geometry;
            SubEntityTraits s = wd.SubEntityTraits;

            // Get geometry
            Point3d pt = pos.BasePoint;
            Vector3d dir = pos.DirectionVector;
            Vector3d up = pos.UpVector;
            Vector3d norm = pos.NormalVector;

            double w = pos.Width / dir.Length;
            double h = pos.Height / dir.Length;

            // Transform to object orientation
            Matrix3d trans = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, pt, dir, up, norm);

            // Transform to match text orientation
            g.PushModelTransform(trans);
            FillType filltype = s.FillType;
            s.FillType = FillType.FillAlways;
            // TODO: Set to defpoints
            s.Color = color;
            Point3dCollection rec = new Point3dCollection();
            rec.Add(new Point3d(-0.15, -0.15, 0));
            rec.Add(new Point3d(w + .15, -0.15, 0));
            rec.Add(new Point3d(w + .15, h + .15, 0));
            rec.Add(new Point3d(-0.15, h + .15, 0));
            g.Polygon(rec);
            s.FillType = filltype;

            // Reset transform
            g.PopModelTransform();

            // Draw the entity over shading
            base.WorldDraw(drawable, wd);

            return true;
        }
    }
}
