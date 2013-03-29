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
    public partial class PosShapeView : DrawingPreview
    {
        private string m_ShapeName;
        private bool m_Selected;
        private Color m_SelectionColor;

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
                    ClearItems();
                    if (!string.IsNullOrEmpty(m_ShapeName))
                    {
                        PosShape shape = PosShape.GetPosShape(m_ShapeName);
                        AddItem(shape);
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

            m_ShapeName = "";
            m_Selected = false;
            m_SelectionColor = SystemColors.Highlight;
        }

        private void PosShapeView_Paint(object sender, PaintEventArgs e)
        {
            // Selection mark
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
}
