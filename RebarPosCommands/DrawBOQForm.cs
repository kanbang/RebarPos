using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class DrawBOQForm : Form
    {
        public MyCommands.BOQStyle TableStyle { get { return (MyCommands.BOQStyle)cbStyle.SelectedItem; } }
        public string TableNote { get { return txtNote.Text; } }
        public string TableHeader { get { return txtHeader.Text; } }
        public string TableFooter { get { return txtFooter.Text; } }
        public int Multiplier { get { return (int)udMultiplier.Value; } }
        public bool HideMissing { get { return chkHideMissing.Checked; } }
        public double TextHeight { get { return double.Parse(txtTextHeight.Text); } }
        public int TableRows { get { return int.Parse(txtTableRows.Text); } }
        public double TableMargin { get { return double.Parse(txtTableMargin.Text); } }
        public BOQTable.DrawingUnits DisplayUnit { get { return cbDisplayUnit.SelectedIndex == 0 ? BOQTable.DrawingUnits.Millimeter : BOQTable.DrawingUnits.Centimeter; } }
        public int Precision { get { return cbPrecision.SelectedIndex; } }

        public DrawBOQForm()
        {
            InitializeComponent();
        }

        public bool Init()
        {
            cbDisplayUnit.SelectedIndex = 0;
            cbPrecision.SelectedIndex = 1;
            foreach (MyCommands.BOQStyle style in MyCommands.BOQStyle.GetStyles())
            {
                cbStyle.Items.Add(style);
            }
            cbStyle.SelectedIndex = 0;

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTextHeight_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            double val = 0;
            if (string.IsNullOrEmpty(txt.Text) || double.TryParse(txt.Text, out val))
            {
                if (val < 0.0001)
                {
                    errorProvider.SetError(txt, "Yazı yüksekliği sıfırdan büyük olmalı.");
                    errorProvider.SetIconAlignment(txt, ErrorIconAlignment.MiddleLeft);
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(txt, "");
                }
            }
            else
            {
                errorProvider.SetError(txt, "Lütfen bir reel sayı girin.");
                errorProvider.SetIconAlignment(txt, ErrorIconAlignment.MiddleLeft);
                e.Cancel = true;
            }
        }

        private void txtTableRows_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            int val = 0;
            if (string.IsNullOrEmpty(txt.Text) || int.TryParse(txt.Text, out val))
            {
                errorProvider.SetError(txt, "");
            }
            else
            {
                errorProvider.SetError(txt, "Lütfen bir tam sayı girin.");
                errorProvider.SetIconAlignment(txt, ErrorIconAlignment.MiddleLeft);
                e.Cancel = true;
            }
        }

        private void txtTableMargin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            double val = 0;
            if (string.IsNullOrEmpty(txt.Text) || double.TryParse(txt.Text, out val))
            {
                errorProvider.SetError(txt, "");
            }
            else
            {
                errorProvider.SetError(txt, "Lütfen bir reel sayı girin.");
                errorProvider.SetIconAlignment(txt, ErrorIconAlignment.MiddleLeft);
                e.Cancel = true;
            }
        }
    }
}
