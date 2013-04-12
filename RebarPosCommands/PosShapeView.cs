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
    public class PosShapeView : DrawingPreview
    {
        private string mShapeName;
        public string ShapeName { get { return mShapeName; } }

        public PosShapeView()
        {
            this.Name = "PosShapeView";
            this.Size = new System.Drawing.Size(300, 150);

            mShapeName = string.Empty;
        }

        public void SetShape(string shapeName)
        {
            ClearItems();
            mShapeName = shapeName;
            if (!string.IsNullOrEmpty(mShapeName))
            {
                AddItem(PosShape.GetPosShape(shapeName));
            }
        }
    }
}
