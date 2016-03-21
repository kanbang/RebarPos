using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsSystem;
using Autodesk.AutoCAD.GraphicsInterface;
using System.ComponentModel;

namespace RebarPosCommands
{
    public class DrawingPreview : Panel
    {
        private bool m_Selected;
        private Color m_SelectionColor;
        private List<Drawable> items;

        private bool init;
        private bool disposed;
        private bool suspended;
        private Extents3d extents;
        private Autodesk.AutoCAD.GraphicsSystem.Device device = null;
        private Autodesk.AutoCAD.GraphicsSystem.View view = null;
        private Autodesk.AutoCAD.GraphicsSystem.Model model = null;

        [Browsable(true), Category("Appearance"), DefaultValue(false)]
        public bool Selected { get { return m_Selected; } set { m_Selected = value; Refresh(); } }
        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "Highlight")]
        public Color SelectionColor { get { return m_SelectionColor; } set { m_SelectionColor = value; Refresh(); } }

        protected bool IsDesigner
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() == "devenv");
            }
        }

        public DrawingPreview()
        {
            items = new List<Drawable>();

            if (IsDesigner)
                this.BackColor = System.Drawing.SystemColors.Control;
            else
                this.BackColor = DWGUtility.ModelBackgroundColor();
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "DrawingPreview";
            this.Size = new System.Drawing.Size(600, 400);

            m_Selected = false;
            m_SelectionColor = SystemColors.Highlight;

            init = false;
            disposed = false;
            suspended = false;
        }

        private void Init()
        {
            if (!init && !disposed && !Disposing && !IsDesigner)
            {
                extents = new Extents3d();

                Autodesk.AutoCAD.GraphicsSystem.Manager gsm = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.GraphicsManager;

#if REBARPOS2015
                Autodesk.AutoCAD.GraphicsSystem.KernelDescriptor descriptor = new Autodesk.AutoCAD.GraphicsSystem.KernelDescriptor();
                descriptor.addRequirement(Autodesk.AutoCAD.UniqueString.Intern("3D Drawing"));
                Autodesk.AutoCAD.GraphicsSystem.GraphicsKernel kernel = Autodesk.AutoCAD.GraphicsSystem.Manager.AcquireGraphicsKernel(descriptor);

                device = gsm.CreateAutoCADOffScreenDevice(kernel);
                device.DeviceRenderType = Autodesk.AutoCAD.GraphicsSystem.RendererType.Default;
                device.BackgroundColor = BackColor;

                view = new Autodesk.AutoCAD.GraphicsSystem.View();
                view.VisualStyle = new Autodesk.AutoCAD.GraphicsInterface.VisualStyle(Autodesk.AutoCAD.GraphicsInterface.VisualStyleType.Wireframe2D);
                model = gsm.CreateAutoCADModel(kernel);
#elif REBARPOS2013
                device = gsm.CreateAutoCADOffScreenDevice();
                device.DeviceRenderType = Autodesk.AutoCAD.GraphicsSystem.RendererType.Default;
                device.BackgroundColor = BackColor;

                view = new Autodesk.AutoCAD.GraphicsSystem.View();
                view.VisualStyle = new Autodesk.AutoCAD.GraphicsInterface.VisualStyle(Autodesk.AutoCAD.GraphicsInterface.VisualStyleType.Wireframe2D);
                model = gsm.CreateAutoCADModel();
#elif REBARPOS2010
                device = gsm.CreateAutoCADOffScreenDevice();
                device.DeviceRenderType = Autodesk.AutoCAD.GraphicsSystem.RendererType.Default;
                device.BackgroundColor = BackColor;

                view = new Autodesk.AutoCAD.GraphicsSystem.View();
                view.VisualStyle = new Autodesk.AutoCAD.GraphicsInterface.VisualStyle(Autodesk.AutoCAD.GraphicsInterface.VisualStyleType.Wireframe2D);
                model = gsm.CreateAutoCADModel();
#endif

                device.Add(view);

                init = true;
            }
        }

        public void AddItem(Drawable item)
        {
            if (IsDesigner) return;

            Init();
            Extents3d? itemExtents = item.Bounds;
            if (itemExtents.HasValue)
            {
                if (items.Count == 0)
                    extents.Set(itemExtents.Value.MinPoint, itemExtents.Value.MaxPoint);
                else
                    extents.AddExtents(itemExtents.Value);
            }
            items.Add(item);

            Refresh();
        }

        public void AddItem(IEnumerable<Drawable> items)
        {
            if (IsDesigner) return;

            Init();
            SuspendUpdate();
            foreach (Drawable item in items)
            {
                AddItem(item);
            }
            ResumeUpdate();
        }

        public void ClearItems()
        {
            if (IsDesigner) return;

            Init();
            items.Clear();
            extents = new Extents3d();
            Refresh();
        }

        public void SuspendUpdate()
        {
            suspended = true;
        }

        public void ResumeUpdate()
        {
            suspended = false;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!suspended && init && !disposed && !Disposing && !IsDesigner)
            {
                device.BackgroundColor = BackColor;
                device.OnSize(ClientSize);
                view.EraseAll();
                Point3d pt1 = extents.MinPoint;
                Point3d pt2 = extents.MaxPoint;
                Point3d ptm = new Point3d((pt1.X + pt2.X) / 2.0, (pt1.Y + pt2.Y) / 2.0, 0.0);
                Point3d ptc = new Point3d(ptm.X, ptm.Y, 1.0);
                double fw = Math.Abs(pt2.X - pt1.X);
                double fh = Math.Abs(pt2.Y - pt1.Y);
                view.SetView(ptc, ptm, Vector3d.YAxis, fw, fh);
                foreach (Drawable item in items)
                {
                    view.Add(item, model);
                }
                view.Update();
                using (Bitmap bmp = view.GetSnapshot(ClientRectangle))
                {
                    e.Graphics.DrawImageUnscaled(bmp, 0, 0);
                }

                if (m_Selected)
                {
                    using (Pen pen = new Pen(m_SelectionColor, 2.0f))
                    {
                        Rectangle rec = ClientRectangle;
                        rec.Inflate(-2, -2);
                        e.Graphics.DrawRectangle(pen, rec);
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (init && !disposed && !Disposing && !IsDesigner && !ClientSize.IsEmpty)
            {
                device.OnSize(ClientSize);
            }
            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (device != null)
                {
                    device.EraseAll();
                }
                if (view != null)
                {
                    view.EraseAll();
                    view.Dispose();
                    view = null;
                }
                if (model != null)
                {
                    model.Dispose();
                    model = null;
                }
                if (device != null)
                {
                    device.Dispose();
                    device = null;
                }

                init = false;
                disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}
