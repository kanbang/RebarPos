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
    public partial class GroupForm : Form
    {
        private class GroupCopy
        {
            public ObjectId id;
            public string name;

            public bool isNew;
            public bool isUsed;
            public bool isDeleted;
            public bool isCurrent;

            public PosGroup.DrawingUnits drawingUnits;
            public PosGroup.DrawingUnits displayUnits;
            public int precision;
            public double maxLength;
            public bool bending;

            public string formula;
            public string formulaWithoutLength;
            public string formulaPosOnly;

            public string standardDiameters;

            public Autodesk.AutoCAD.Colors.Color textColor;
            public Autodesk.AutoCAD.Colors.Color posColor;
            public Autodesk.AutoCAD.Colors.Color circleColor;
            public Autodesk.AutoCAD.Colors.Color multiplierColor;
            public Autodesk.AutoCAD.Colors.Color groupColor;
            public Autodesk.AutoCAD.Colors.Color currentGroupHighlightColor;

            public ObjectId textStyleId;
            public ObjectId noteStyleId;

            public double noteScale;
        }

        List<GroupCopy> m_Copies;
        Dictionary<string, ObjectId> m_Groups;
        Dictionary<string, ObjectId> m_TextStyles;

        public ObjectId CurrentId { get; private set; }

        public GroupForm()
        {
            InitializeComponent();

            CurrentId = ObjectId.Null;
            m_Copies = new List<GroupCopy>();
            m_Groups = new Dictionary<string, ObjectId>();
            m_TextStyles = new Dictionary<string, ObjectId>();

            AcadPreferences pref = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            uint indexColor = pref.Display.GraphicsWinModelBackgrndColor;
            posStylePreview.BackColor = ColorTranslator.FromOle((int)indexColor);
        }

        public bool Init(ObjectId currentId)
        {
            m_Groups = DWGUtility.GetGroups();

            if (m_Groups.Count == 0)
            {
                return false;
            }

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (KeyValuePair<string, ObjectId> item in m_Groups)
                    {
                        PosGroup group = tr.GetObject(item.Value, OpenMode.ForRead) as PosGroup;
                        if (group == null) continue;

                        GroupCopy copy = new GroupCopy();

                        copy.name = item.Key;

                        copy.id = item.Value;
                        copy.isNew = false;
                        copy.isDeleted = false;
                        copy.isUsed = false;
                        copy.isCurrent = (copy.id == currentId);

                        copy.drawingUnits = group.DrawingUnit;
                        copy.displayUnits = group.DisplayUnit;
                        copy.precision = group.Precision;
                        copy.maxLength = group.MaxBarLength;
                        copy.bending = group.Bending;

                        copy.formula = group.Formula;
                        copy.formulaWithoutLength = group.FormulaWithoutLength;
                        copy.formulaPosOnly = group.FormulaPosOnly;

                        copy.standardDiameters = group.StandardDiameters;

                        copy.textColor = group.TextColor;
                        copy.posColor = group.PosColor;
                        copy.circleColor = group.CircleColor;
                        copy.multiplierColor = group.MultiplierColor;
                        copy.groupColor = group.GroupColor;
                        copy.currentGroupHighlightColor = group.CurrentGroupHighlightColor;

                        copy.textStyleId = group.TextStyleId;
                        copy.noteStyleId = group.NoteStyleId;
                        copy.noteScale = group.NoteScale;

                        m_Copies.Add(copy);
                    }

                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                    using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            RebarPos pos = tr.GetObject(it.Current, OpenMode.ForRead) as RebarPos;
                            if (pos != null)
                            {
                                GroupCopy copy = m_Copies.Find(p => p.id == pos.GroupId);
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
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos");
                }
            }

            int i = 0;
            foreach (GroupCopy copy in m_Copies)
            {
                ListViewItem lv = new ListViewItem(copy.name);
                lbGroups.Items.Add(lv);

                if (copy.isCurrent)
                {
                    lbGroups.SelectedIndices.Add(i);
                }
                i++;
            }
            UpdateItemImages();

            m_TextStyles = DWGUtility.GetTextStyles();
            foreach (string name in m_TextStyles.Keys)
            {
                cbTextStyle.Items.Add(name);
                cbNoteStyle.Items.Add(name);
            }

            CurrentId = currentId;
            SetGroup();

            return true;
        }

        public void SetGroup()
        {
            if (lbGroups.SelectedIndices.Count == 0) return;

            GroupCopy copy = m_Copies.Find(p => p.name == lbGroups.SelectedItems[0].Text);
            if (copy == null)
                return;

            btnRemove.Enabled = !copy.isCurrent && !copy.isUsed;
            cbDrawingUnit.SelectedIndex = (copy.drawingUnits == PosGroup.DrawingUnits.Millimeter ? 0 : 1);
            cbDisplayUnit.SelectedIndex = (copy.displayUnits == PosGroup.DrawingUnits.Millimeter ? 0 : 1);
            udPrecision.Value = copy.precision;
            txtMaxLength.Text = copy.maxLength.ToString();
            chkBending.Checked = copy.bending;

            txtFormula.Text = copy.formula;
            txtFormulaWithoutLength.Text = copy.formulaWithoutLength;
            txtFormulaPosOnly.Text = copy.formulaPosOnly;
            posStylePreview.SetFormula(copy.formula, copy.formulaWithoutLength, copy.formulaPosOnly);

            txtDiameterList.Text = copy.standardDiameters;

            btnPickTextColor.BackColor = copy.textColor.ColorValue;
            btnPickPosColor.BackColor = copy.posColor.ColorValue;
            btnPickCircleColor.BackColor = copy.circleColor.ColorValue;
            btnPickMultiplierColor.BackColor = copy.multiplierColor.ColorValue;
            btnPickGroupColor.BackColor = copy.groupColor.ColorValue;
            btnPickCurrentGroupColor.BackColor = copy.currentGroupHighlightColor.ColorValue;

            posStylePreview.TextColor = copy.textColor.ColorValue;
            posStylePreview.PosColor = copy.posColor.ColorValue;
            posStylePreview.CircleColor = copy.circleColor.ColorValue;
            posStylePreview.MultiplierColor = copy.multiplierColor.ColorValue;
            posStylePreview.GroupColor = copy.groupColor.ColorValue;
            posStylePreview.CurrentGroupHighlightColor = copy.currentGroupHighlightColor.ColorValue;

            if (copy.displayUnits == PosGroup.DrawingUnits.Millimeter)
                posStylePreview.SetPos("1", "2x4", "16", "200", "2400");
            else
                posStylePreview.SetPos("1", "2x4", "16", "20", "240");

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in m_TextStyles)
            {
                if (item.Value == copy.textStyleId)
                {
                    cbTextStyle.SelectedIndex = i;
                }
                if (item.Value == copy.noteStyleId)
                {
                    cbNoteStyle.SelectedIndex = i;
                }
                i++;
            }

            txtNoteScale.Text = copy.noteScale.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GroupCopy org = m_Copies[0];
            if (lbGroups.SelectedIndices.Count != 0)
                org = m_Copies.Find(p => p.name == lbGroups.SelectedItems[0].Text);

            int i = 1;
            while (m_Copies.Exists(p => p.name.ToUpperInvariant() == "GROUP" + i.ToString()))
            {
                i++;
            }

            GroupCopy copy = new GroupCopy();

            copy.name = "Group" + i.ToString();

            copy.id = ObjectId.Null;
            copy.isNew = true;
            copy.isDeleted = false;
            copy.isUsed = false;
            copy.isCurrent = false;

            copy.drawingUnits = org.drawingUnits;
            copy.displayUnits = org.displayUnits;
            copy.precision = org.precision;
            copy.maxLength = org.maxLength;
            copy.bending = org.bending;

            copy.formula = org.formula;
            copy.formulaWithoutLength = org.formulaWithoutLength;
            copy.formulaPosOnly = org.formulaPosOnly;

            copy.standardDiameters = "8 10 12 14 16 18 20 22 25 26 32 36";

            copy.textColor = org.textColor;
            copy.posColor = org.posColor;
            copy.circleColor = org.circleColor;
            copy.multiplierColor = org.multiplierColor;
            copy.groupColor = org.groupColor;
            copy.currentGroupHighlightColor = org.currentGroupHighlightColor;

            copy.textStyleId = org.textStyleId;
            copy.noteStyleId = org.noteStyleId;
            copy.noteScale = org.noteScale;

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.name);
            lv.ImageIndex = 2;
            lbGroups.Items.Add(lv);
            lbGroups.SelectedIndices.Clear();
            lbGroups.SelectedIndices.Add(lbGroups.Items.Count - 1);
            lv.BeginEdit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbGroups.SelectedIndices.Count == 0) return;

            GroupCopy copy = m_Copies.Find(p => p.name == lbGroups.SelectedItems[0].Text);
            if (copy == null)
            {
                return;
            }
            copy.isDeleted = true;
            UpdateItemImages();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lbGroups.SelectedIndices.Count == 0) return;
            lbGroups.SelectedItems[0].BeginEdit();
        }

        private void btnSetCurrent_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            foreach (GroupCopy item in m_Copies)
            {
                item.isCurrent = false;
            }
            copy.isCurrent = true;
            UpdateItemImages();
        }

        private void lbGroups_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            if (lbGroups.SelectedIndices.Count == 0)
            {
                e.CancelEdit = true;
                return;
            }
            foreach (ListViewItem item in lbGroups.Items)
            {
                if (item.Index != lbGroups.SelectedIndices[0] && item.Text == e.Label)
                {
                    MessageBox.Show("Bu isim zaten mevcut. Lütfen farklı bir isim seçin.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.CancelEdit = true;
                    return;
                }
            }
            GroupCopy copy = m_Copies.Find(p => p.name == lbGroups.Items[e.Item].Text);
            if (copy == null)
            {
                e.CancelEdit = true;
                return;
            }
            copy.name = e.Label;
        }

        private void lbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGroups.SelectedIndices.Count == 0)
            {
                btnRemove.Enabled = false;
                btnRename.Enabled = false;
                gbOptions.Enabled = false;
                btnSetCurrent.Enabled = false;
                return;
            }
            else
            {
                btnRemove.Enabled = true;
                btnRename.Enabled = true;
                gbOptions.Enabled = true;
                btnSetCurrent.Enabled = true;
                SetGroup();
            }
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < m_Copies.Count; i++)
            {
                GroupCopy copy = m_Copies[i];
                ListViewItem lv = lbGroups.Items[i];
                if (copy.isCurrent)
                    lv.ImageIndex = 4;
                else if (copy.isUsed)
                    lv.ImageIndex = 0;
                else if (copy.isDeleted)
                    lv.ImageIndex = 3;
                else if (copy.isNew)
                    lv.ImageIndex = 2;
                else
                    lv.ImageIndex = 1;
            }
        }

        private GroupCopy GetSelected()
        {
            if (lbGroups.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.name == lbGroups.SelectedItems[0].Text);
        }

        private void cbDrawingUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.drawingUnits = (cbDrawingUnit.SelectedIndex == 0 ? PosGroup.DrawingUnits.Millimeter : PosGroup.DrawingUnits.Centimeter);
        }

        private void cbDisplayUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.displayUnits = (cbDisplayUnit.SelectedIndex == 0 ? PosGroup.DrawingUnits.Millimeter : PosGroup.DrawingUnits.Centimeter);

            if (copy.displayUnits == PosGroup.DrawingUnits.Millimeter)
                posStylePreview.SetPos("1", "2x4", "16", "200", "2400");
            else
                posStylePreview.SetPos("1", "2x4", "16", "20", "240");
        }

        private void udPrecision_ValueChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.precision = (int)udPrecision.Value;
        }

        private void chkBending_CheckedChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.bending = chkBending.Checked;
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
                    DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosGroup.TableName), OpenMode.ForWrite);
                    foreach (GroupCopy copy in m_Copies)
                    {
                        if (copy.isDeleted)
                        {
                            dict.Remove(copy.id);
                        }
                        else if (copy.isNew)
                        {
                            PosGroup group = new PosGroup();

                            group.Name = copy.name;

                            group.DrawingUnit = copy.drawingUnits;
                            group.DisplayUnit = copy.displayUnits;
                            group.Precision = copy.precision;
                            group.MaxBarLength = copy.maxLength;
                            group.Bending = copy.bending;

                            group.Formula = copy.formula;
                            group.FormulaWithoutLength = copy.formulaWithoutLength;
                            group.FormulaPosOnly = copy.formulaPosOnly;

                            group.StandardDiameters = copy.standardDiameters;

                            group.TextColor = copy.textColor;
                            group.PosColor = copy.posColor;
                            group.CircleColor = copy.circleColor;
                            group.MultiplierColor = copy.multiplierColor;
                            group.GroupColor = copy.groupColor;
                            group.CurrentGroupHighlightColor = copy.currentGroupHighlightColor;

                            group.TextStyleId = copy.textStyleId;
                            group.NoteStyleId = copy.noteStyleId;
                            group.NoteScale = copy.noteScale;

                            copy.id = dict.SetAt("*", group);
                            tr.AddNewlyCreatedDBObject(group, true);
                        }
                        else
                        {
                            PosGroup group = (PosGroup)tr.GetObject(copy.id, OpenMode.ForWrite);

                            group.Name = copy.name;

                            group.DrawingUnit = copy.drawingUnits;
                            group.DisplayUnit = copy.displayUnits;
                            group.Precision = copy.precision;
                            group.MaxBarLength = copy.maxLength;
                            group.Bending = copy.bending;

                            group.Formula = copy.formula;
                            group.FormulaWithoutLength = copy.formulaWithoutLength;
                            group.FormulaPosOnly = copy.formulaPosOnly;

                            group.StandardDiameters = copy.standardDiameters;

                            group.TextColor = copy.textColor;
                            group.PosColor = copy.posColor;
                            group.CircleColor = copy.circleColor;
                            group.MultiplierColor = copy.multiplierColor;
                            group.GroupColor = copy.groupColor;
                            group.CurrentGroupHighlightColor = copy.currentGroupHighlightColor;

                            group.TextStyleId = copy.textStyleId;
                            group.NoteStyleId = copy.noteStyleId;
                            group.NoteScale = copy.noteScale;
                        }

                        if (!copy.isDeleted && copy.isCurrent)
                        {
                            DbDictionaryEnumerator it = dict.GetEnumerator();
                            while (it.MoveNext())
                            {
                                PosGroup group = tr.GetObject(it.Value, OpenMode.ForWrite) as PosGroup;
                                if (it.Value == copy.id)
                                {
                                    CurrentId = it.Value;
                                    group.Current = true;
                                }
                                else
                                {
                                    group.Current = false;
                                }
                            }
                        }
                    }
                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos");
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtMaxLength_Validating(object sender, CancelEventArgs e)
        {
            double val;
            if (!double.TryParse(txtMaxLength.Text, out val))
            {
                e.Cancel = true;
                errorProvider.SetError(txtMaxLength, "Lütfen bir reel sayı girin.");
            }
            else
                errorProvider.SetError(txtMaxLength, "");
        }

        private void txtMaxLength_Validated(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            double val;
            if (double.TryParse(txtMaxLength.Text, out val))
            {
                copy.maxLength = val;
            }
        }

        private void txtDiameterList_Validating(object sender, CancelEventArgs e)
        {
            foreach (string ds in txtDiameterList.Text.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int d = 0;
                if (!int.TryParse(ds, out d))
                {
                    errorProvider.SetError(txtDiameterList, "Çaplar tam sayı olarak girilip boşluk karakteri ile ayrılmalıdır.");
                    e.Cancel = true;
                }
            }
        }

        private void txtDiameterList_Validated(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            List<int> diameters = new List<int>();
            foreach (string ds in txtDiameterList.Text.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int d = 0;
                if (int.TryParse(ds, out d) && d != 0)
                {
                    diameters.Add(d);
                }
            }
            copy.standardDiameters = string.Join(" ", diameters.ConvertAll<string>(p => p.ToString()).ToArray());
        }

        private void txtNoteScale_Validating(object sender, CancelEventArgs e)
        {
            double val;
            if (!double.TryParse(txtNoteScale.Text, out val))
            {
                errorProvider.SetError(txtNoteScale, "Lütfen bir reel sayı girin.");
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtNoteScale, "");
        }

        private void txtNoteScale_Validated(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            double val;
            if (double.TryParse(txtNoteScale.Text, out val))
            {
                copy.noteScale = val;
            }
        }

        private void txtFormula_Validated(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.formula = txtFormula.Text;

            posStylePreview.SetFormula(txtFormula.Text, txtFormulaWithoutLength.Text, txtFormulaPosOnly.Text);
        }

        private void txtFormulaWithoutLength_Validated(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.formulaWithoutLength = txtFormulaWithoutLength.Text;

            posStylePreview.SetFormula(txtFormula.Text, txtFormulaWithoutLength.Text, txtFormulaPosOnly.Text);
        }

        private void txtFormulaPosOnly_Validated(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.formulaPosOnly = txtFormulaPosOnly.Text;

            posStylePreview.SetFormula(txtFormula.Text, txtFormulaWithoutLength.Text, txtFormulaPosOnly.Text);
        }

        private void cbTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbTextStyle.SelectedItem];
            copy.textStyleId = id;
        }

        private void cbNoteStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id = m_TextStyles[(string)cbNoteStyle.SelectedItem];
            copy.noteStyleId = id;
        }

        private void btnPickTextColor_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
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
                posStylePreview.TextColor = copy.textColor.ColorValue;
            }
        }

        private void btnPickPosColor_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
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
                posStylePreview.PosColor = copy.posColor.ColorValue;
            }
        }

        private void btnPickCircleColor_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.circleColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CircleColor = copy.circleColor.ColorValue;
            }
        }

        private void btnPickMultiplierColor_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.multiplierColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.MultiplierColor = copy.multiplierColor.ColorValue;
            }
        }

        private void btnPickGroupColor_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.groupColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.GroupColor = copy.groupColor.ColorValue;
            }
        }

        private void btnPickCurrentGroupColor_Click(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                copy.currentGroupHighlightColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CurrentGroupHighlightColor = copy.currentGroupHighlightColor.ColorValue;
            }
        }
    }
}
