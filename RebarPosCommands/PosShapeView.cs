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
        private string m_ShapeName = "";

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
                    if (!IsDesigner) SetShape();
                }
            }
        }

        public PosShapeView()
        {
            this.Name = "PosShapeView";

            m_ShapeName = "";
        }

        private void SetShape()
        {
            ClearItems();
            AddItem(PosShape.GetPosShape(m_ShapeName));
        }
    }
}
