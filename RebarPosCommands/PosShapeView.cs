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
    public partial class PosShapeView : UserControl
    {
        private string m_ShapeName;
        private bool m_Selected;
        private Color m_SelectionColor;
        private Bitmap bmp = null;
        PosShape shape = null;
        private bool suspended;

        [Browsable(true), Category("Appearance"), DefaultValue("")]
        public string ShapeName
        {
            get
            {
                return m_ShapeName;
            }
            set
            {
                if (m_ShapeName != value)
                {
                    m_ShapeName = value;
                    //ClearItems();
                    if (!string.IsNullOrEmpty(m_ShapeName))
                    {
                        shape = PosShape.GetPosShape(m_ShapeName);
                        bmp = shape.ToBitmap(BackColor, ClientRectangle.Width, ClientRectangle.Height);
                    }
                }
            }
        }
        [Browsable(true), Category("Appearance"), DefaultValue(false)]
        public bool Selected { get { return m_Selected; } set { m_Selected = value; Refresh(); } }
        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "Highlight")]
        public Color SelectionColor { get { return m_SelectionColor; } set { m_SelectionColor = value; Refresh(); } }

        public PosShapeView()
        {
            InitializeComponent();

            BackColor = DWGUtility.ModelBackgroundColor();

            m_ShapeName = "";
            m_Selected = false;
            m_SelectionColor = SystemColors.Highlight;

            suspended = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!suspended)
            {
                if (bmp != null)
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

            base.OnPaint(e);
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

        protected override void OnSizeChanged(EventArgs e)
        {
            if (bmp != null) { bmp.Dispose(); bmp = null; }
            if (shape != null) bmp = shape.ToBitmap(BackColor, ClientRectangle.Width, ClientRectangle.Height);

            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);

            if (bmp != null) { bmp.Dispose(); bmp = null; }
        }
    }
}
