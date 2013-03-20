using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsSystem;
using Autodesk.AutoCAD.GraphicsInterface;

namespace RebarPosCommands
{
    public partial class DrawingPreview : UserControl
    {
        private Bitmap mImage;
        private List<Drawable> mItems;

        public DrawingPreview()
        {
            InitializeComponent();

            mImage = null;
            mItems = new List<Drawable>();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            if (mImage != null)
            {
                e.Graphics.DrawImageUnscaled(mImage, (ClientRectangle.Width - mImage.Width) / 2, (ClientRectangle.Height - mImage.Height) / 2);
            }
        }

        public void AddItem(Drawable item)
        {
            mItems.Add(item);
            UpdateView();
        }

        public void AddItem(IEnumerable<Drawable> items)
        {
            mItems.AddRange(items);
            UpdateView();
        }

        public void ClearItems()
        {
            foreach (Drawable item in mItems)
            {
                item.Dispose();
            }
            mItems.Clear();
            mImage.Dispose();
            mImage = null;
            UpdateView();
        }

        private void UpdateView()
        {
            Size size = this.ClientSize;
            mImage = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            mImage.SetResolution(200, 200);

            Manager gsm = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.GraphicsManager;

            using (Autodesk.AutoCAD.GraphicsSystem.View view = new Autodesk.AutoCAD.GraphicsSystem.View())
            {
                view.EraseAll();
                view.VisualStyle = new Autodesk.AutoCAD.GraphicsInterface.VisualStyle(Autodesk.AutoCAD.GraphicsInterface.VisualStyleType.Wireframe2D);

                using (Model model = gsm.CreateAutoCADModel())
                {
                    // Add items to view
                    Extents3d extents = new Extents3d();
                    foreach (Drawable item in mItems)
                    {
                        view.Add(item, model);
                        Extents3d? itemExtents = item.Bounds;
                        if (itemExtents.HasValue)
                        {
                            extents.AddExtents(itemExtents.Value);
                        }
                    }

                    // Set the view
                    view.Viewport = new Rectangle(0, 0, size.Width, size.Height);
                    view.SetView(new Point3d((extents.MinPoint.X + extents.MaxPoint.X) / 2.0, (extents.MinPoint.Y + extents.MaxPoint.Y) / 2.0, 1.0),
                        new Point3d((extents.MinPoint.X + extents.MaxPoint.X) / 2.0, (extents.MinPoint.Y + extents.MaxPoint.Y) / 2.0, 0.0),
                        new Vector3d(0.0, 1.0, 0.0),
                        Math.Abs(extents.MaxPoint.X - extents.MinPoint.X),
                        Math.Abs(extents.MaxPoint.Y - extents.MinPoint.Y));

                    // Take the snapshot
                    using (Device dev = gsm.CreateAutoCADOffScreenDevice())
                    {
                        dev.OnSize(gsm.DisplaySize);
                        dev.DeviceRenderType = RendererType.Default;
                        dev.BackgroundColor = BackColor;

                        dev.Add(view);
                        dev.Update();

                        view.ZoomExtents(extents.MinPoint, extents.MaxPoint);
                        mImage = view.GetSnapshot(view.Viewport);
                        dev.Erase(view);
                    }
                }
            }
            Refresh();
        }

        private void DrawingPreview_SizeChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
