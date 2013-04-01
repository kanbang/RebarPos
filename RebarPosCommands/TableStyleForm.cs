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
            public string name;

            public bool isBuiltin;

            public string columns;

            public string posColumn;
            public string countColumn;
            public string diameterColumn;
            public string lengthColumn;
            public string shapeColumn;
            public string totalLengthColumn;
            public string diameterListColumn;

            public string totalLengthRow;
            public string unitWeightRow;
            public string weightRow;
            public string grossWeightRow;

            public string multiplierHeadingLabel;

            public ObjectId textStyleId;
            public ObjectId headingStyleId;
            public ObjectId footingStyleId;
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

                copy.name = item;

                copy.isBuiltin = style.IsBuiltIn;

                copy.columns = style.Columns;

                copy.posColumn = style.PosLabel;
                copy.countColumn = style.CountLabel;
                copy.diameterColumn = style.DiameterLabel;
                copy.lengthColumn = style.LengthLabel;
                copy.shapeColumn = style.ShapeLabel;
                copy.totalLengthColumn = style.TotalLengthLabel;
                copy.diameterListColumn = style.DiameterListLabel;

                copy.totalLengthRow = style.DiameterLengthLabel;
                copy.unitWeightRow = style.UnitWeightLabel;
                copy.weightRow = style.WeightLabel;
                copy.grossWeightRow = style.GrossWeightLabel;

                copy.multiplierHeadingLabel = style.MultiplierHeadingLabel;

                copy.textStyleId = style.TextStyleId;
                copy.headingStyleId = style.HeadingStyleId;
                copy.footingStyleId = style.FootingStyleId;

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
                if (!chkUserOnly.Checked || (chkUserOnly.Checked && !copy.isBuiltin))
                {
                    ListViewItem lv = new ListViewItem(copy.name);
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

            txtColumns.Text = copy.columns;

            txtPosColumn.Text = copy.posColumn;
            txtCountCoumn.Text = copy.countColumn;
            txtDiameterColumn.Text = copy.diameterColumn;
            txtLengthColumn.Text = copy.lengthColumn;
            txtShapeColumn.Text = copy.shapeColumn;
            txtTotalLengthColumn.Text = copy.totalLengthColumn;
            txtDiameterListColumn.Text = copy.diameterListColumn;

            txtTotalLengthRow.Text = copy.totalLengthRow;
            txtUnitWeightRow.Text = copy.unitWeightRow;
            txtWeightRow.Text = copy.weightRow;
            txtGrossWeightRow.Text = copy.grossWeightRow;

            txtMultiplierHeadingLabel.Text = copy.multiplierHeadingLabel;

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in m_TextStyles)
            {
                if (item.Value == copy.textStyleId)
                {
                    cbTextStyle.SelectedIndex = i;
                }
                if (item.Value == copy.headingStyleId)
                {
                    cbHeadingStyle.SelectedIndex = i;
                }
                if (item.Value == copy.footingStyleId)
                {
                    cbFootingStyle.SelectedIndex = i;
                }
                i++;
            }

            tableView.SuspendUpdate();

            tableView.Columns = copy.columns;

            tableView.PosLabel = copy.posColumn;
            tableView.CountLabel = copy.countColumn;
            tableView.DiameterLabel = copy.diameterColumn;
            tableView.LengthLabel = copy.lengthColumn;
            tableView.ShapeLabel = copy.shapeColumn;
            tableView.TotalLengthLabel = copy.totalLengthColumn;
            tableView.DiameterListLabel = copy.diameterListColumn;

            tableView.DiameterLengthLabel = copy.totalLengthRow;
            tableView.UnitWeightLabel = copy.unitWeightRow;
            tableView.WeightLabel = copy.weightRow;
            tableView.GrossWeightLabel = copy.grossWeightRow;

            tableView.MultiplierHeadingLabel = copy.multiplierHeadingLabel;

            tableView.TextStyleId = copy.textStyleId;
            tableView.HeadingStyleId = copy.headingStyleId;
            tableView.FootingStyleId = copy.footingStyleId;

            tableView.SetTable();
            tableView.ResumeUpdate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TableStyleCopy org = GetSelected();
            if (org == null) return;

            int i = 1;
            while (m_Copies.Exists(p => p.name.ToUpperInvariant() == "TABLESTYLE" + i.ToString()))
            {
                i++;
            }

            TableStyleCopy copy = new TableStyleCopy();

            copy.name = "TableStyle" + i.ToString();

            copy.isBuiltin = false;

            copy.columns = org.columns;

            copy.posColumn = org.posColumn;
            copy.countColumn = org.countColumn;
            copy.diameterColumn = org.diameterColumn;
            copy.lengthColumn = org.lengthColumn;
            copy.shapeColumn = org.shapeColumn;
            copy.totalLengthColumn = org.totalLengthColumn;
            copy.diameterListColumn = org.diameterListColumn;

            copy.totalLengthRow = org.totalLengthRow;
            copy.unitWeightRow = org.unitWeightRow;
            copy.weightRow = org.weightRow;
            copy.grossWeightRow = org.grossWeightRow;

            copy.multiplierHeadingLabel = org.multiplierHeadingLabel;

            copy.textStyleId = org.textStyleId;
            copy.headingStyleId = org.headingStyleId;
            copy.footingStyleId = org.footingStyleId;

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.name);
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
            if (copy.isBuiltin) return;

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
            TableStyleCopy copy = m_Copies.Find(p => p.name == lbStyles.Items[e.Item].Text);
            if (copy == null)
            {
                e.CancelEdit = true;
                return;
            }
            copy.name = e.Label;
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
                bool enable = (copy != null) && !copy.isBuiltin;

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
                TableStyleCopy copy = m_Copies.Find(p => p.name == lv.Text);
                if (copy.isBuiltin)
                    lv.ImageIndex = 0;
                else
                    lv.ImageIndex = 1;
            }
        }

        private TableStyleCopy GetSelected()
        {
            if (lbStyles.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.name == lbStyles.SelectedItems[0].Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RebarPos");
            string userStylesFile = System.IO.Path.Combine(userFolder, "TableStyles.txt");
            string newStylesFile = System.IO.Path.Combine(userFolder, "TableStyles.new");

            BOQStyle.ClearBOQStyles();
            foreach (TableStyleCopy copy in m_Copies)
            {
                if (!copy.isBuiltin)
                {
                    BOQStyle style = new BOQStyle();

                    style.Name = copy.name;

                    style.Columns = copy.columns;

                    style.PosLabel = copy.posColumn;
                    style.CountLabel = copy.countColumn;
                    style.DiameterLabel = copy.diameterColumn;
                    style.LengthLabel = copy.lengthColumn;
                    style.ShapeLabel = copy.shapeColumn;
                    style.TotalLengthLabel = copy.totalLengthColumn;
                    style.DiameterListLabel = copy.diameterListColumn;

                    style.DiameterLengthLabel = copy.totalLengthRow;
                    style.UnitWeightLabel = copy.unitWeightRow;
                    style.WeightLabel = copy.weightRow;
                    style.GrossWeightLabel = copy.grossWeightRow;

                    style.MultiplierHeadingLabel = copy.multiplierHeadingLabel;

                    style.TextStyleId = copy.textStyleId;
                    style.HeadingStyleId = copy.headingStyleId;
                    style.FootingStyleId = copy.headingStyleId;

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
            copy.columns = txtColumns.Text;
            tableView.Columns = txtColumns.Text;
            tableView.SetTable();
        }

        private void cbTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbTextStyle.SelectedItem];
            copy.textStyleId = id;
            tableView.TextStyleId = id;
            tableView.SetTable();
        }

        private void cbHeadingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbHeadingStyle.SelectedItem];
            copy.headingStyleId = id;
            tableView.HeadingStyleId = id;
            tableView.SetTable();
        }

        private void cbFootingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbFootingStyle.SelectedItem];
            copy.footingStyleId = id;
            tableView.FootingStyleId = id;
            tableView.SetTable();
        }

        private void txtPosColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.posColumn = ((TextBox)sender).Text;
            tableView.PosLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtCountCoumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.countColumn = ((TextBox)sender).Text;
            tableView.CountLabel = ((TextBox)sender).Text;
            tableView.SetTable();

        }

        private void txtDiameterColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.diameterColumn = ((TextBox)sender).Text;
            tableView.DiameterLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtLengthColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.lengthColumn = ((TextBox)sender).Text;
            tableView.LengthLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtShapeColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.shapeColumn = ((TextBox)sender).Text;
            tableView.ShapeLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtTotalLengthColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.totalLengthColumn = ((TextBox)sender).Text;
            tableView.TotalLengthLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtDiameterListColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.diameterListColumn = ((TextBox)sender).Text;
            tableView.DiameterListLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtMultiplierHeadingLabel_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.multiplierHeadingLabel = ((TextBox)sender).Text;
            tableView.MultiplierHeadingLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtTotalLengthRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.totalLengthRow = ((TextBox)sender).Text;
            tableView.DiameterLengthLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtUnitWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.unitWeightRow = ((TextBox)sender).Text;
            tableView.UnitWeightLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.weightRow = ((TextBox)sender).Text;
            tableView.WeightLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void txtGrossWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.grossWeightRow = ((TextBox)sender).Text;
            tableView.GrossWeightLabel = ((TextBox)sender).Text;
            tableView.SetTable();
        }

        private void chkUserOnly_CheckedChanged(object sender, EventArgs e)
        {
            PopulateList();
        }
    }
}