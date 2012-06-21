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
            public ObjectId id;
            public string name;

            public bool isNew;
            public bool isUsed;
            public bool isDeleted;

            public BOQStyle.DrawingUnits displayUnits;
            public int precision;

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

            public Autodesk.AutoCAD.Colors.Color textColor;
            public Autodesk.AutoCAD.Colors.Color posColor;
            public Autodesk.AutoCAD.Colors.Color lineColor;
            public Autodesk.AutoCAD.Colors.Color separatorColor;
            public Autodesk.AutoCAD.Colors.Color borderColor;
            public Autodesk.AutoCAD.Colors.Color headingColor;
            public Autodesk.AutoCAD.Colors.Color footingColor;

            public ObjectId textStyleId;
            public ObjectId headingStyleId;
            public ObjectId footingStyleId;
            public double headingScale;
            public double footingScale;

            public double rowSpacing;
        }

        List<TableStyleCopy> m_Copies;
        Dictionary<string, ObjectId> m_Styles;
        Dictionary<string, ObjectId> m_TextStyles;

        public TableStyleForm()
        {
            InitializeComponent();

            m_Copies = new List<TableStyleCopy>();
            m_Styles = new Dictionary<string, ObjectId>();
            m_TextStyles = new Dictionary<string, ObjectId>();
        }

        public bool Init()
        {
            m_Styles = DWGUtility.GetTableStyles();

            if (m_Styles.Count == 0)
            {
                return false;
            }

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (KeyValuePair<string, ObjectId> item in m_Styles)
                    {
                        BOQStyle style = tr.GetObject(item.Value, OpenMode.ForRead) as BOQStyle;
                        if (style == null) continue;

                        TableStyleCopy copy = new TableStyleCopy();

                        copy.name = item.Key;

                        copy.id = item.Value;
                        copy.isNew = false;
                        copy.isDeleted = false;
                        copy.isUsed = false;

                        copy.displayUnits = style.DisplayUnit;
                        copy.precision = style.Precision;

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

                        copy.textColor = style.TextColor;
                        copy.posColor = style.PosColor;
                        copy.lineColor = style.LineColor;
                        copy.separatorColor = style.SeparatorColor;
                        copy.borderColor = style.BorderColor;
                        copy.headingColor = style.HeadingColor;
                        copy.footingColor = style.FootingColor;

                        copy.textStyleId = style.TextStyleId;
                        copy.headingStyleId = style.HeadingStyleId;
                        copy.footingStyleId = style.FootingStyleId;
                        copy.headingScale = style.HeadingScale;
                        copy.footingScale = style.FootingScale;

                        copy.rowSpacing = style.RowSpacing;

                        m_Copies.Add(copy);
                    }

                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                    using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            BOQTable table = tr.GetObject(it.Current, OpenMode.ForRead) as BOQTable;
                            if (table != null)
                            {
                                TableStyleCopy copy = m_Copies.Find(p => p.id == table.StyleId);
                                if (copy != null)
                                {
                                    copy.isUsed = true;
                                    continue;
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            int i = 0;
            foreach (TableStyleCopy copy in m_Copies)
            {
                ListViewItem lv = new ListViewItem(copy.name);
                lbStyles.Items.Add(lv);

                i++;
            }
            lbStyles.SelectedIndices.Add(0);
            UpdateItemImages();

            m_TextStyles = DWGUtility.GetTextStyles();
            foreach (string name in m_TextStyles.Keys)
            {
                cbTextStyle.Items.Add(name);
                cbHeadingStyle.Items.Add(name);
                cbFootingStyle.Items.Add(name);
            }

            SetTableStyle();

            return true;
        }

        public void SetTableStyle()
        {
            if (lbStyles.SelectedIndices.Count == 0) return;

            TableStyleCopy copy = m_Copies.Find(p => p.name == lbStyles.SelectedItems[0].Text);
            if (copy == null)
                return;

            btnRemove.Enabled = !copy.isUsed;
            cbDisplayUnit.SelectedIndex = (copy.displayUnits == BOQStyle.DrawingUnits.Millimeter ? 0 : 1);
            udPrecision.Value = copy.precision;

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

            btnPickTextColor.BackColor = copy.textColor.ColorValue;
            btnPickPosColor.BackColor = copy.posColor.ColorValue;
            btnPickLineColor.BackColor = copy.lineColor.ColorValue;
            btnPickSeparatorColor.BackColor = copy.separatorColor.ColorValue;
            btnPickBorderColor.BackColor = copy.borderColor.ColorValue;
            btnPickHeadingColor.BackColor = copy.headingColor.ColorValue;
            btnPickFootingColor.BackColor = copy.footingColor.ColorValue;

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

            txtHeadingScale.Text = copy.headingScale.ToString();
            txtFootingScale.Text = copy.footingScale.ToString();

            txtRowSpacing.Text = copy.rowSpacing.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TableStyleCopy org = m_Copies[0];
            if (lbStyles.SelectedIndices.Count != 0)
                org = m_Copies.Find(p => p.name == lbStyles.SelectedItems[0].Text);

            int i = 1;
            while (m_Copies.Exists(p => p.name.ToUpperInvariant() == "TABLESTYLE" + i.ToString()))
            {
                i++;
            }

            TableStyleCopy copy = new TableStyleCopy();

            copy.name = "TableStyle" + i.ToString();

            copy.id = ObjectId.Null;
            copy.isNew = true;
            copy.isDeleted = false;
            copy.isUsed = false;

            copy.displayUnits = org.displayUnits;
            copy.precision = org.precision;

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

            copy.textColor = org.textColor;
            copy.posColor = org.posColor;
            copy.lineColor = org.lineColor;
            copy.separatorColor = org.separatorColor;
            copy.borderColor = org.borderColor;
            copy.headingColor = org.headingColor;
            copy.footingColor = org.footingColor;

            copy.textStyleId = org.textStyleId;
            copy.headingStyleId = org.headingStyleId;
            copy.footingStyleId = org.footingStyleId;
            copy.headingScale = org.headingScale;
            copy.footingScale = org.footingScale;
            copy.rowSpacing = org.rowSpacing;

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.name);
            lv.ImageIndex = 2;
            lbStyles.Items.Add(lv);
            lbStyles.SelectedIndices.Clear();
            lbStyles.SelectedIndices.Add(lbStyles.Items.Count - 1);
            lv.BeginEdit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbStyles.SelectedIndices.Count == 0) return;

            TableStyleCopy copy = m_Copies.Find(p => p.name == lbStyles.SelectedItems[0].Text);
            if (copy == null)
            {
                return;
            }
            copy.isDeleted = true;
            UpdateItemImages();
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
                btnRemove.Enabled = false;
                btnRename.Enabled = false;
            }
            else
            {
                btnRemove.Enabled = true;
                btnRename.Enabled = true;
                SetTableStyle();
            }

            gbDisplay.Enabled = (lbStyles.SelectedIndices.Count != 0);
            gbColumns.Enabled = (lbStyles.SelectedIndices.Count != 0);
            gbRows.Enabled = (lbStyles.SelectedIndices.Count != 0);
            gbOptions.Enabled = (lbStyles.SelectedIndices.Count != 0);
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < m_Copies.Count; i++)
            {
                TableStyleCopy copy = m_Copies[i];
                ListViewItem lv = lbStyles.Items[i];
                if (copy.isUsed)
                    lv.ImageIndex = 0;
                else if (copy.isDeleted)
                    lv.ImageIndex = 3;
                else if (copy.isNew)
                    lv.ImageIndex = 2;
                else
                    lv.ImageIndex = 1;
            }
        }

        private TableStyleCopy GetSelected()
        {
            if (lbStyles.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.name == lbStyles.SelectedItems[0].Text);
        }

        private void cbDisplayUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.displayUnits = (cbDisplayUnit.SelectedIndex == 0 ? BOQStyle.DrawingUnits.Millimeter : BOQStyle.DrawingUnits.Centimeter);
        }

        private void udPrecision_ValueChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.precision = (int)udPrecision.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Apply changes
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(BOQStyle.TableName), OpenMode.ForWrite);
                    foreach (TableStyleCopy copy in m_Copies)
                    {
                        if (copy.isDeleted)
                        {
                            if (!copy.id.IsNull)
                                dict.Remove(copy.id);
                        }
                        else if (copy.isNew)
                        {
                            BOQStyle style = new BOQStyle();

                            style.Name = copy.name;

                            style.DisplayUnit = copy.displayUnits;
                            style.Precision = copy.precision;

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

                            style.TextColor = copy.textColor;
                            style.PosColor = copy.posColor;
                            style.LineColor = copy.lineColor;
                            style.SeparatorColor = copy.separatorColor;
                            style.BorderColor = copy.borderColor;
                            style.HeadingColor = copy.headingColor;
                            style.FootingColor = copy.footingColor;

                            style.TextStyleId = copy.textStyleId;
                            style.HeadingStyleId = copy.headingStyleId;
                            style.FootingStyleId = copy.headingStyleId;
                            style.HeadingScale = copy.headingScale;
                            style.FootingScale = copy.footingScale;
                            style.RowSpacing = copy.rowSpacing;

                            copy.id = dict.SetAt("*", style);
                            tr.AddNewlyCreatedDBObject(style, true);
                        }
                        else
                        {
                            BOQStyle style = tr.GetObject(copy.id, OpenMode.ForWrite) as BOQStyle;

                            style.Name = copy.name;

                            style.DisplayUnit = copy.displayUnits;
                            style.Precision = copy.precision;

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

                            style.TextColor = copy.textColor;
                            style.PosColor = copy.posColor;
                            style.LineColor = copy.lineColor;
                            style.SeparatorColor= copy.separatorColor;
                            style.BorderColor= copy.borderColor;
                            style.HeadingColor = copy.headingColor;
                            style.FootingColor= copy.footingColor;

                            style.TextStyleId = copy.textStyleId;
                            style.HeadingStyleId= copy.headingStyleId;
                            style.FootingStyleId = copy.footingStyleId;
                            style.HeadingScale = copy.headingScale;
                            style.FootingScale = copy.footingScale;

                            style.RowSpacing = copy.rowSpacing;

                            DWGUtility.RefreshTableWithStyle(copy.id);
                        }
                    }
                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtHeadingScale_Validating(object sender, CancelEventArgs e)
        {
            double val;
            if (!double.TryParse(txtHeadingScale.Text, out val))
            {
                errorProvider.SetError(txtHeadingScale, "Lütfen bir reel sayı girin.");
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtHeadingScale, "");
        }

        private void txtHeadingScale_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            double val;
            if (double.TryParse(txtHeadingScale.Text, out val))
            {
                copy.headingScale = val;
            }
        }

        private void txtFootingScale_Validating(object sender, CancelEventArgs e)
        {
            double val;
            if (!double.TryParse(txtFootingScale.Text, out val))
            {
                errorProvider.SetError(txtFootingScale, "Lütfen bir reel sayı girin.");
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtFootingScale, "");
        }

        private void txtFootingScale_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            double val;
            if (double.TryParse(txtFootingScale.Text, out val))
            {
                copy.footingScale = val;
            }
        }

        private void txtRowSpacing_Validating(object sender, CancelEventArgs e)
        {
            double val;
            if (!double.TryParse(txtRowSpacing.Text, out val))
            {
                errorProvider.SetError(txtRowSpacing, "Lütfen bir reel sayı girin.");
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtRowSpacing, "");
        }

        private void txtRowSpacing_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            double val;
            if (double.TryParse(txtRowSpacing.Text, out val))
            {
                copy.rowSpacing = val;
            }
        }


        private void txtColumns_Validating(object sender, CancelEventArgs e)
        {
            string text = txtColumns.Text;
            if(text.Contains("[TL]") && !text.EndsWith("[TL]"))
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
        }

        private void cbTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbTextStyle.SelectedItem];
            copy.textStyleId = id;
        }

        private void cbHeadingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbHeadingStyle.SelectedItem];
            copy.headingStyleId = id;
        }

        private void cbFootingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbFootingStyle.SelectedItem];
            copy.footingStyleId = id;
        }

        private void btnPickTextColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.textColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void btnPickPosColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.posColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void btnPickLineColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.lineColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void btnPickSeparatorColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.separatorColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void btnPickBorderColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.borderColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void btnPickFootingColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.footingColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void btnPickHeadingColor_Click(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.headingColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }

        private void txtPosColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.posColumn = ((TextBox)sender).Text;
        }

        private void txtCountCoumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.countColumn = ((TextBox)sender).Text;
        }

        private void txtDiameterColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.diameterColumn = ((TextBox)sender).Text;
        }

        private void txtLengthColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.lengthColumn = ((TextBox)sender).Text;
        }

        private void txtShapeColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.shapeColumn = ((TextBox)sender).Text;
        }

        private void txtTotalLengthColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.totalLengthColumn = ((TextBox)sender).Text;
        }

        private void txtDiameterListColumn_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.diameterListColumn = ((TextBox)sender).Text;
        }

        private void txtTotalLengthRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.totalLengthRow = ((TextBox)sender).Text;
        }

        private void txtUnitWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.unitWeightRow = ((TextBox)sender).Text;
        }

        private void txtWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.weightRow = ((TextBox)sender).Text;
        }

        private void txtGrossWeightRow_Validated(object sender, EventArgs e)
        {
            TableStyleCopy copy = GetSelected();
            if (copy == null) return;
            copy.grossWeightRow = ((TextBox)sender).Text;
        }
    }
}
