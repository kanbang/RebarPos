using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.Interop;

namespace RebarPosCommands
{
    public partial class TableStyleForm : Form
    {
        private class TableStyleCopy
        {
            public string Name;

            public bool IsBuiltin;

            public string Columns;

            public string PosColumn;
            public string CountColumn;
            public string DiameterColumn;
            public string LengthColumn;
            public string ShapeColumn;
            public string TotalLengthColumn;
            public string DiameterListColumn;

            public string TotalLengthRow;
            public string UnitWeightRow;
            public string WeightRow;
            public string GrossWeightRow;

            public string MultiplierHeadingLabel;

            public ObjectId TextStyleId;
            public ObjectId HeadingStyleId;
            public ObjectId FootingStyleId;
        }

        List<TableStyleCopy> m_Copies;
        List<string> m_Styles;
        Dictionary<string, ObjectId> m_TextStyles;

        public TableStyleForm()
        {
            InitializeComponent();

            m_Copies = new List<TableStyleCopy>();
            m_Styles = new List<string>();
            m_TextStyles = new Dictionary<string, ObjectId>();

            tableView.BackColor = DWGUtility.ModelBackgroundColor();
        }

        public bool Init()
        {
            m_Styles = BOQStyle.GetAllBOQStyles();

            if (m_Styles.Count == 0)
            {
                return false;
            }

            foreach (string item in m_Styles)
            {
                TableStyleCopy copy = new TableStyleCopy();
                BOQStyle style = BOQStyle.GetBOQStyle(item);

                copy.Name = item;

                copy.IsBuiltin = style.IsBuiltIn;

                copy.Columns = style.Columns;

                copy.PosColumn = style.PosLabel;
                copy.CountColumn = style.CountLabel;
                copy.DiameterColumn = style.DiameterLabel;
                copy.LengthColumn = style.LengthLabel;
                copy.ShapeColumn = style.ShapeLabel;
                copy.TotalLengthColumn = style.TotalLengthLabel;
                copy.DiameterListColumn = style.DiameterListLabel;

                copy.TotalLengthRow = style.DiameterLengthLabel;
                copy.UnitWeightRow = style.UnitWeightLabel;
                copy.WeightRow = style.WeightLabel;
                copy.GrossWeightRow = style.GrossWeightLabel;

                copy.MultiplierHeadingLabel = style.MultiplierHeadingLabel;

                copy.TextStyleId = style.TextStyleId;
                copy.HeadingStyleId = style.HeadingStyleId;
                copy.FootingStyleId = style.FootingStyleId;

                m_Copies.Add(copy);
            }

            m_TextStyles = DWGUtility.GetTextStyles();
            foreach (string name in m_TextStyles.Keys)
            {
                cbTextStyle.Items.Add(name);
                cbHeadingStyle.Items.Add(name);
                cbFootingStyle.Items.Add(name);
            }

            PopulateList();

            return true;
        }

        private void PopulateList()
        {
            lbStyles.Items.Clear();
            foreach (TableStyleCopy copy in m_Copies)
            {
                if (!chkUserOnly.Checked || (chkUserOnly.Checked && !copy.IsBuiltin))
                {
                    ListViewItem lv = new ListViewItem(copy.Name);
                    lbStyles.Items.Add(lv);
                }
            }

            if (lbStyles.Items.Count != 0)
                lbStyles.SelectedIndices.Add(0);

            UpdateItemImages();

            SetTableStyle();
        }

        public void SetTableStyle()
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;

            txtColumns.Text = copy.Columns;

            txtPosColumn.Text = copy.PosColumn;
            txtCountCoumn.Text = copy.CountColumn;
            txtDiameterColumn.Text = copy.DiameterColumn;
            txtLengthColumn.Text = copy.LengthColumn;
            txtShapeColumn.Text = copy.ShapeColumn;
            txtTotalLengthColumn.Text = copy.TotalLengthColumn;
            txtDiameterListColumn.Text = copy.DiameterListColumn;

            txtTotalLengthRow.Text = copy.TotalLengthRow;
            txtUnitWeightRow.Text = copy.UnitWeightRow;
            txtWeightRow.Text = copy.WeightRow;
            txtGrossWeightRow.Text = copy.GrossWeightRow;

            txtMultiplierHeadingLabel.Text = copy.MultiplierHeadingLabel;

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in m_TextStyles)
            {
                if (item.Value == copy.TextStyleId)
                {
                    cbTextStyle.SelectedIndex = i;
                }
                if (item.Value == copy.HeadingStyleId)
                {
                    cbHeadingStyle.SelectedIndex = i;
                }
                if (item.Value == copy.FootingStyleId)
                {
                    cbFootingStyle.SelectedIndex = i;
                }
                i++;
            }

            tableView.SuspendUpdate();

            tableView.Columns = copy.Columns;

            tableView.PosLabel = copy.PosColumn;
            tableView.CountLabel = copy.CountColumn;
            tableView.DiameterLabel = copy.DiameterColumn;
            tableView.LengthLabel = copy.LengthColumn;
            tableView.ShapeLabel = copy.ShapeColumn;
            tableView.TotalLengthLabel = copy.TotalLengthColumn;
            tableView.DiameterListLabel = copy.DiameterListColumn;

            tableView.DiameterLengthLabel = copy.TotalLengthRow;
            tableView.UnitWeightLabel = copy.UnitWeightRow;
            tableView.WeightLabel = copy.WeightRow;
            tableView.GrossWeightLabel = copy.GrossWeightRow;

            tableView.MultiplierHeadingLabel = copy.MultiplierHeadingLabel;

            tableView.TextStyleId = copy.TextStyleId;
            tableView.HeadingStyleId = copy.HeadingStyleId;
            tableView.FootingStyleId = copy.FootingStyleId;

            tableView.SetTable();
            tableView.ResumeUpdate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TableStyleCopy org = GetSelected();
            if (org == null) return;

            int i = 1;
            while (m_Copies.Exists(p => p.Name.ToUpperInvariant() == "TABLESTYLE" + i.ToString()))
            {
                i++;
            }

            TableStyleCopy copy = new TableStyleCopy();

            copy.Name = "TableStyle" + i.ToString();

            copy.IsBuiltin = false;

            copy.Columns = org.Columns;

            copy.PosColumn = org.PosColumn;
            copy.CountColumn = org.CountColumn;
            copy.DiameterColumn = org.DiameterColumn;
            copy.LengthColumn = org.LengthColumn;
            copy.ShapeColumn = org.ShapeColumn;
            copy.TotalLengthColumn = org.TotalLengthColumn;
            copy.DiameterListColumn = org.DiameterListColumn;

            copy.TotalLengthRow = org.TotalLengthRow;
            copy.UnitWeightRow = org.UnitWeightRow;
            copy.WeightRow = org.WeightRow;
            copy.GrossWeightRow = org.GrossWeightRow;

            copy.MultiplierHeadingLabel = org.MultiplierHeadingLabel;

            copy.TextStyleId = org.TextStyleId;
            copy.HeadingStyleId = org.HeadingStyleId;
            copy.FootingStyleId = org.FootingStyleId;

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.Name);
            lv.ImageIndex = 1;
            lbStyles.Items.Add(lv);
            lbStyles.SelectedIndices.Clear();
            lbStyles.SelectedIndices.Add(lbStyles.Items.Count - 1);
            lv.BeginEdit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            if (copy.IsBuiltin) return;

            m_Copies.Remove(copy);
            lbStyles.Items.Remove(lbStyles.SelectedItems[0]);
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lbStyles.SelectedIndices.Count == 0) return;
            lbStyles.SelectedItems[0].BeginEdit();
        }

        private void lbStyles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            if (lbStyles.SelectedIndices.Count == 0)
            {
                e.CancelEdit = true;
                return;
            }
            foreach (ListViewItem item in lbStyles.Items)
            {
                if (item.Index != lbStyles.SelectedIndices[0] && item.Text == e.Label)
                {
                    MessageBox.Show("Bu isim zaten mevcut. Lütfen farklı bir isim seçin.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.CancelEdit = true;
                    return;
                }
            }
            TableStyleCopy copy = m_Copies.Find(p => p.Name == lbStyles.Items[e.Item].Text);
            if (copy == null)
            {
                e.CancelEdit = true;
                return;
            }
            copy.Name = e.Label;
        }

        private void lbStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbStyles.SelectedIndices.Count == 0)
            {
                gbColumns.Enabled = false;
                gbRows.Enabled = false;
                gbDisplay.Enabled = false;

                btnRemove.Enabled = false;
                btnRename.Enabled = false;
            }
            else
            {
                TableStyleCopy copy = GetSelected();
                bool enable = (copy != null) && !copy.IsBuiltin;

                gbColumns.Enabled = enable;
                gbRows.Enabled = enable;
                gbDisplay.Enabled = enable;

                btnRemove.Enabled = enable;
                btnRename.Enabled = enable;

                lbStyles.LabelEdit = enable;

                SetTableStyle();
            }
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < lbStyles.Items.Count; i++)
            {
                ListViewItem lv = lbStyles.Items[i];
                TableStyleCopy copy = m_Copies.Find(p => p.Name == lv.Text);
                if (copy.IsBuiltin)
                    lv.ImageIndex = 0;
                else
                    lv.ImageIndex = 1;
            }
        }

        private TableStyleCopy GetSelected()
        {
            if (lbStyles.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.Name == lbStyles.SelectedItems[0].Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RebarPos");
            string userStylesFile = System.IO.Path.Combine(userFolder, "TableStyles.txt");
            string newStylesFile = System.IO.Path.Combine(userFolder, "TableStyles.new");

            BOQStyle.ClearBOQStyles();
            foreach (TableStyleCopy copy in m_Copies)
            {
                if (!copy.IsBuiltin)
                {
                    BOQStyle style = new BOQStyle();

                    style.Name = copy.Name;

                    style.Columns = copy.Columns;

                    style.PosLabel = copy.PosColumn;
                    style.CountLabel = copy.CountColumn;
                    style.DiameterLabel = copy.DiameterColumn;
                    style.LengthLabel = copy.LengthColumn;
                    style.ShapeLabel = copy.ShapeColumn;
                    style.TotalLengthLabel = copy.TotalLengthColumn;
                    style.DiameterListLabel = copy.DiameterListColumn;

                    style.DiameterLengthLabel = copy.TotalLengthRow;
                    style.UnitWeightLabel = copy.UnitWeightRow;
                    style.WeightLabel = copy.WeightRow;
                    style.GrossWeightLabel = copy.GrossWeightRow;

                    style.MultiplierHeadingLabel = copy.MultiplierHeadingLabel;

                    style.TextStyleId = copy.TextStyleId;
                    style.HeadingStyleId = copy.HeadingStyleId;
                    style.FootingStyleId = copy.FootingStyleId;

                    BOQStyle.AddBOQStyle(style);
                }
            }

            try
            {
                BOQStyle.SaveBOQStylesToFile(newStylesFile);
                System.IO.File.Delete(userStylesFile);
                System.IO.File.Move(newStylesFile, userStylesFile);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            BOQStyle.ReadBOQStylesFromFile(userStylesFile);

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtColumns_Validating(object sender, CancelEventArgs e)
        {
            string text = txtColumns.Text;
            if (text.Contains("[TL]") && !text.EndsWith("[TL]"))
            {
                errorProvider.SetError(txtColumns, "Toplam sütunu son sütun olmalıdır.");
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtColumns, "");
        }

        private void txtColumns_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.Columns = txtColumns.Text;
            tableView.Columns = txtColumns.Text;
            tableView.SetTable();
        }

        private void cbTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbTextStyle.SelectedItem];
            copy.TextStyleId = id;
            tableView.TextStyleId = id;
            tableView.SetTable();
        }

        private void cbHeadingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbHeadingStyle.SelectedItem];
            copy.HeadingStyleId = id;
            tableView.HeadingStyleId = id;
            tableView.SetTable();
        }

        private void cbFootingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbFootingStyle.SelectedItem];
            copy.FootingStyleId = id;
            tableView.FootingStyleId = id;
            tableView.SetTable();
        }

        private void txtPosColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.PosColumn = ((TextBox)sender).Text;
            tableView.PosLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtCountCoumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.CountColumn = ((TextBox)sender).Text;
            tableView.CountLabel = ((TextBox)sender).Text;
            tableView.SetTable();

        }

        private void txtDiameterColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.DiameterColumn = ((TextBox)sender).Text;
            tableView.DiameterLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtLengthColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.LengthColumn = ((TextBox)sender).Text;
            tableView.LengthLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtShapeColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.ShapeColumn = ((TextBox)sender).Text;
            tableView.ShapeLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtTotalLengthColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.TotalLengthColumn = ((TextBox)sender).Text;
            tableView.TotalLengthLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtDiameterListColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.DiameterListColumn = ((TextBox)sender).Text;
            tableView.DiameterListLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtMultiplierHeadingLabel_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.MultiplierHeadingLabel = ((TextBox)sender).Text;
            tableView.MultiplierHeadingLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtTotalLengthRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.TotalLengthRow = ((TextBox)sender).Text;
            tableView.DiameterLengthLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtUnitWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.UnitWeightRow = ((TextBox)sender).Text;
            tableView.UnitWeightLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.WeightRow = ((TextBox)sender).Text;
            tableView.WeightLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtGrossWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.GrossWeightRow = ((TextBox)sender).Text;
            tableView.GrossWeightLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void chkUserOnly_CheckedChanged(object sender, EventArgs e)
        {
            PopulateList();
        }
    }
}