using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public delegate void MultiPosShapeViewClickEventHandler(object sender, MultiPosShapeViewClickEventArgs e);

    public class MultiPosShapeViewClickEventArgs
    {
        public string Shape { get; private set; }

        public MultiPosShapeViewClickEventArgs(string shape)
        {
            Shape = shape;
        }
    }

    public class MultiPosShapeView : Panel
    {
        public event MultiPosShapeViewClickEventHandler ShapeClick;

        private bool init;
        private bool disposed;
        private Autodesk.AutoCAD.GraphicsSystem.Device device = null;
        private Autodesk.AutoCAD.GraphicsSystem.View view = null;
        private Autodesk.AutoCAD.GraphicsSystem.Model model = null;

        private FlowLayoutPanel layoutPanel;

        private string mSelectedShape;
        private Size mCellSize;
        private Color mSelectionColor;

        private Dictionary<int, List<string>> pieceLengths;

        public string SelectedShape { get { return mSelectedShape; } set { mSelectedShape = value; Refresh(); } }
        public Size CellSize { get { return mCellSize; } set { mCellSize = value; UpdateCells(); } }
        public Color CellBackColor { get; set; }
        public bool ShowShapeNames { get; set; }
        public Color SelectionColor { get { return mSelectionColor; } set { mSelectionColor = value; Refresh(); } }

        protected bool IsDesigner
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() == "devenv");
            }
        }

        public MultiPosShapeView()
        {
            if (IsDesigner)
                this.CellBackColor = System.Drawing.SystemColors.Control;
            else
                this.CellBackColor = DWGUtility.ModelBackgroundColor();
            mSelectedShape = string.Empty;
            mCellSize = new Size(475, 150);
            mSelectionColor = SystemColors.Highlight;

            this.Name = "MultiPosShapeView";
            this.Size = new System.Drawing.Size(900, 450);
            this.SuspendLayout();

            this.layoutPanel = new FlowLayoutPanel();
            this.layoutPanel.Dock = DockStyle.Fill;
            this.layoutPanel.FlowDirection = FlowDirection.LeftToRight;
            this.layoutPanel.AutoScroll = true;
            this.Controls.Add(layoutPanel);

            this.ResumeLayout(false);

            mSelectedShape = string.Empty;

            pieceLengths = new Dictionary<int, List<string>>();

            init = false;
            disposed = false;
        }

        private void Init()
        {
            if (!init && !disposed && !Disposing && !IsDesigner)
            {
                Autodesk.AutoCAD.GraphicsSystem.Manager gsm = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.GraphicsManager;

                Autodesk.AutoCAD.GraphicsSystem.KernelDescriptor descriptor = new Autodesk.AutoCAD.GraphicsSystem.KernelDescriptor();
                descriptor.addRequirement(Autodesk.AutoCAD.UniqueString.Intern("3D Drawing"));
                Autodesk.AutoCAD.GraphicsSystem.GraphicsKernel kernel = Autodesk.AutoCAD.GraphicsSystem.Manager.AcquireGraphicsKernel(descriptor);

                device = gsm.CreateAutoCADOffScreenDevice(kernel);
                device.DeviceRenderType = Autodesk.AutoCAD.GraphicsSystem.RendererType.Default;
                device.BackgroundColor = CellBackColor;

                view = new Autodesk.AutoCAD.GraphicsSystem.View();
                view.VisualStyle = new Autodesk.AutoCAD.GraphicsInterface.VisualStyle(Autodesk.AutoCAD.GraphicsInterface.VisualStyleType.Wireframe2D);
                model = gsm.CreateAutoCADModel(kernel);

                device.Add(view);

                init = true;
            }
        }

        public void SetShapes(IEnumerable<string> shapes)
        {
            Init();

            this.layoutPanel.SuspendLayout();

            foreach (Control cell in layoutPanel.Controls)
            {
                cell.Paint -= new PaintEventHandler(cell_Paint);
            }
            this.layoutPanel.Controls.Clear();

            foreach (string shape in shapes)
            {
                PictureBox cell = new PictureBox();
                cell.Size = mCellSize;
                cell.Tag = shape;
                cell.Paint += new PaintEventHandler(cell_Paint);
                cell.Click += new EventHandler(cell_Click);
                this.layoutPanel.Controls.Add(cell);
            }

            this.layoutPanel.ResumeLayout();

            device.OnSize(mCellSize);
        }

        public void SetPieceLengths(int index, string a, string b, string c, string d, string e, string f)
        {
            List<string> lengths = new List<string>() { a, b, c, d, e, f };
            if (pieceLengths.ContainsKey(index))
                pieceLengths[index] = lengths;
            else
                pieceLengths.Add(index, lengths);
        }

        public void UpdateCells()
        {
            Init();

            this.layoutPanel.SuspendLayout();
            foreach (Control cell in layoutPanel.Controls)
            {
                cell.Size = mCellSize;
            }
            this.layoutPanel.ResumeLayout();

            device.OnSize(mCellSize);

            Refresh();
        }

        void cell_Click(object sender, EventArgs e)
        {
            Control cell = (Control)sender;
            string shape = (string)cell.Tag;
            if (string.IsNullOrEmpty(shape)) return;
            mSelectedShape = shape;
            Refresh();

            if (ShapeClick != null)
                ShapeClick(this, new MultiPosShapeViewClickEventArgs(mSelectedShape));
        }

        void cell_Paint(object sender, PaintEventArgs e)
        {
            if (init && !disposed && !Disposing && !IsDesigner)
            {
                Control cell = (Control)sender;
                string shapeName = (string)cell.Tag;
                if (string.IsNullOrEmpty(shapeName)) return;
                int index = layoutPanel.Controls.IndexOf(cell);
                PosShape shape = PosShape.GetPosShape(shapeName);
                List<string> lengths = new List<string>();
                if (pieceLengths.TryGetValue(index, out lengths) && (lengths.Count == 6))
                {
                    shape.SetShapeTexts(lengths[0], lengths[1], lengths[2], lengths[3], lengths[4], lengths[5]);
                }

                using (Bitmap bmp = shape.ToBitmap(device, view, model, CellBackColor, mCellSize.Width, mCellSize.Height))
                {
                    e.Graphics.DrawImageUnscaled(bmp, 0, 0);
                }

                shape.ClearShapeTexts();

                if (ShowShapeNames)
                {
                    using (Brush brush = new SolidBrush(IsDark(CellBackColor) ? Color.White : Color.Black))
                    {
                        e.Graphics.DrawString(shapeName, Font, brush, 4, 6);
                    }
                }

                if (mSelectedShape == shapeName)
                {
                    using (Pen pen = new Pen(mSelectionColor, 2.0f))
                    {
                        Rectangle rec = new Rectangle(0, 0, mCellSize.Width, mCellSize.Height);
                        rec.Inflate(-2, -2);
                        e.Graphics.DrawRectangle(pen, rec);
                    }
                }
            }
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

        private bool IsDark(Color c)
        {
            return Math.Sqrt(c.R * c.R * .241 + c.G * c.G * .691 + c.B * c.B * .068) < 130;
        }
    }
}
