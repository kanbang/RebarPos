using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class EditBOQForm : Form
    {
        public string TableHeader { get { return txtHeader.Text; } }
        public string TableFooter { get { return txtFooter.Text; } }
        public int Multiplier { get { return (int)udMultiplier.Value; } }
        public double TextHeight { get { return double.Parse(txtTextHeight.Text); } }
        public double TableHeight { get { return double.Parse(txtTableHeight.Text); } }
        public double TableMargin { get { return double.Parse(txtTableMargin.Text); } }

        public EditBOQForm()
        {
            InitializeComponent();
        }

        public bool Init(ObjectId tableid)
        {
            try
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BOQTable table = tr.GetObject(tableid, OpenMode.ForRead) as BOQTable;
                    if (table == null) return false;
                    txtHeader.Text = table.Heading;
                    txtFooter.Text = table.Footing;
                    udMultiplier.Value = table.Multiplier;
                    txtTextHeight.Text = table.Scale.ToString();
                    txtTableHeight.Text  = table.MaxHeight.ToString();
                    txtTableMargin.Text = table.TableSpacing.ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
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

        private void txtTableHeight_Validating(object sender, System.ComponentModel.CancelEventArgs e)
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
