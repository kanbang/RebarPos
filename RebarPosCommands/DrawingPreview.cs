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
    public partial class DrawingPreview : Panel
    {
        private bool m_Selected;
        private Color m_SelectionColor;

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

                device = gsm.CreateAutoCADDevice(this.Handle);
                device.DeviceRenderType = RendererType.Default;
                device.BackgroundColor = BackColor;

                view = new Autodesk.AutoCAD.GraphicsSystem.View();
                view.VisualStyle = new Autodesk.AutoCAD.GraphicsInterface.VisualStyle(Autodesk.AutoCAD.GraphicsInterface.VisualStyleType.Wireframe2D);

                model = gsm.CreateAutoCADModel();

                device.Add(view);
                device.Update();

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
                extents.AddExtents(itemExtents.Value);
            }
            view.Add(item, model);
            view.SetView(new Point3d((extents.MinPoint.X + extents.MaxPoint.X) / 2.0, (extents.MinPoint.Y + extents.MaxPoint.Y) / 2.0, 1.0),
                new Point3d((extents.MinPoint.X + extents.MaxPoint.X) / 2.0, (extents.MinPoint.Y + extents.MaxPoint.Y) / 2.0, 0.0),
                new Vector3d(0.0, 1.0, 0.0),
                Math.Abs(extents.MaxPoint.X - extents.MinPoint.X),
                Math.Abs(extents.MaxPoint.Y - extents.MinPoint.Y));
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
            view.EraseAll();
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
                device.Update();

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
            if (init && !disposed && !Disposing && !IsDesigner && this.Size.Width > 0 && this.Size.Height > 0)
            {
                device.OnSize(this.Size);
            }

            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

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
    }
}
