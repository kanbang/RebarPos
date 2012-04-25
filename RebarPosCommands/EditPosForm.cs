﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class EditPosForm : Form
    {
        public EditPosForm()
        {
            InitializeComponent();
        }

        public void SetPos(RebarPos pos, PosShape shape)
        {
            txtPosMarker.Text = pos.Pos;
            txtPosCount.Text = pos.Count;
            txtPosDiameter.Text = pos.Diameter;
            txtPosSpacing.Text = pos.Spacing;
            txtPosMultiplier.Text = pos.Multiplier.ToString();
            chkIncludePos.Checked = (pos.Multiplier > 0);
            txtPosMultiplier.Enabled = (pos.Multiplier > 0);
            txtPosNote.Text = pos.Note;

            rbShowAll.Checked = (pos.Display == RebarPos.DisplayStyle.All);
            rbWithoutLength.Checked = (pos.Display == RebarPos.DisplayStyle.WithoutLength);
            rbMarkerOnly.Checked = (pos.Display == RebarPos.DisplayStyle.MarkerOnly);

            txtA.Text = pos.A;
            txtB.Text = pos.B;
            txtC.Text = pos.C;
            txtD.Text = pos.D;
            txtE.Text = pos.E;
            txtF.Text = pos.F;

            txtA.Enabled = btnSelectA.Enabled = btnMeasureA.Enabled = (shape.Fields >= 1);
            txtB.Enabled = btnSelectB.Enabled = btnMeasureB.Enabled = (shape.Fields >= 2);
            txtC.Enabled = btnSelectC.Enabled = btnMeasureC.Enabled = (shape.Fields >= 3);
            txtD.Enabled = btnSelectD.Enabled = btnMeasureD.Enabled = (shape.Fields >= 4);
            txtE.Enabled = btnSelectE.Enabled = btnMeasureE.Enabled = (shape.Fields >= 5);
            txtF.Enabled = btnSelectF.Enabled = btnMeasureF.Enabled = (shape.Fields >= 6);

            for (int i = 0; i < shape.Items.Count; i++)
            {
                PosShape.Shape sh = shape.Items[i];
                Color color = sh.Color.ColorValue;
                if (sh is PosShape.ShapeLine)
                {
                    PosShape.ShapeLine line = sh as PosShape.ShapeLine;
                    posShapeView.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2);
                }
                else if (sh is PosShape.ShapeArc)
                {
                    PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                    posShapeView.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)arc.StartAngle, (float)arc.EndAngle);
                }
                else if (sh is PosShape.ShapeText)
                {
                    PosShape.ShapeText text = sh as PosShape.ShapeText;
                    posShapeView.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}