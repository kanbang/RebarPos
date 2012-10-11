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
            public PosGroup.DrawingUnits drawingUnits;
            public PosGroup.DrawingUnits displayUnits;
            public int precision;
            public double maxLength;
            public bool bending;

            public string formula;
            public string formulaLengthOnly;
            public string formulaPosOnly;

            public string standardDiameters;

            public Autodesk.AutoCAD.Colors.Color textColor;
            public Autodesk.AutoCAD.Colors.Color posColor;
            public Autodesk.AutoCAD.Colors.Color circleColor;
            public Autodesk.AutoCAD.Colors.Color multiplierColor;
            public Autodesk.AutoCAD.Colors.Color groupColor;
            public Autodesk.AutoCAD.Colors.Color noteColor;
            public Autodesk.AutoCAD.Colors.Color currentGroupHighlightColor;
            public Autodesk.AutoCAD.Colors.Color countColor;

            public ObjectId textStyleId;
            public ObjectId noteStyleId;

            public double noteScale;
        }

        GroupCopy m_Copy;
        Dictionary<string, ObjectId> m_TextStyles;

        public GroupForm()
        {
            InitializeComponent();

            m_Copy = new GroupCopy();
            m_TextStyles = new Dictionary<string, ObjectId>();

            posStylePreview.BackColor = DWGUtility.ModelBackgroundColor();
        }

        public bool Init()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(PosGroup.GroupId, OpenMode.ForRead) as PosGroup;
                    if (group == null) return false;

                    m_Copy.drawingUnits = group.DrawingUnit;
                    m_Copy.displayUnits = group.DisplayUnit;
                    m_Copy.precision = group.Precision;
                    m_Copy.maxLength = group.MaxBarLength;
                    m_Copy.bending = group.Bending;

                    m_Copy.formula = group.Formula;
                    m_Copy.formulaLengthOnly = group.FormulaLengthOnly;
                    m_Copy.formulaPosOnly = group.FormulaPosOnly;

                    m_Copy.standardDiameters = group.StandardDiameters;

                    m_Copy.textColor = group.TextColor;
                    m_Copy.posColor = group.PosColor;
                    m_Copy.circleColor = group.CircleColor;
                    m_Copy.multiplierColor = group.MultiplierColor;
                    m_Copy.groupColor = group.GroupColor;
                    m_Copy.noteColor = group.NoteColor;
                    m_Copy.currentGroupHighlightColor = group.CurrentGroupHighlightColor;
                    m_Copy.countColor = group.CountColor;

                    m_Copy.textStyleId = group.TextStyleId;
                    m_Copy.noteStyleId = group.NoteStyleId;
                    m_Copy.noteScale = group.NoteScale;
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            m_TextStyles = DWGUtility.GetTextStyles();
            foreach (string name in m_TextStyles.Keys)
            {
                cbTextStyle.Items.Add(name);
                cbNoteStyle.Items.Add(name);
            }

            SetGroup();

            return true;
        }

        public void SetGroup()
        {
            cbDrawingUnit.SelectedIndex = (m_Copy.drawingUnits == PosGroup.DrawingUnits.Millimeter ? 0 : 1);
            cbDisplayUnit.SelectedIndex = (m_Copy.displayUnits == PosGroup.DrawingUnits.Millimeter ? 0 : 1);
            udPrecision.Value = m_Copy.precision;
            txtMaxLength.Text = m_Copy.maxLength.ToString();
            chkBending.Checked = m_Copy.bending;

            txtFormula.Text = m_Copy.formula;
            txtFormulaWithoutLength.Text = m_Copy.formulaLengthOnly;
            txtFormulaPosOnly.Text = m_Copy.formulaPosOnly;
            posStylePreview.SetFormula(m_Copy.formula, m_Copy.formulaLengthOnly, m_Copy.formulaPosOnly);

            txtDiameterList.Text = m_Copy.standardDiameters;

            btnPickTextColor.BackColor = m_Copy.textColor.ColorValue;
            btnPickPosColor.BackColor = m_Copy.posColor.ColorValue;
            btnPickCircleColor.BackColor = m_Copy.circleColor.ColorValue;
            btnPickMultiplierColor.BackColor = m_Copy.multiplierColor.ColorValue;
            btnPickGroupColor.BackColor = m_Copy.groupColor.ColorValue;
            btnPickNoteColor.BackColor = m_Copy.noteColor.ColorValue;
            btnPickCountColor.BackColor = m_Copy.countColor.ColorValue;

            posStylePreview.TextColor = m_Copy.textColor.ColorValue;
            posStylePreview.PosColor = m_Copy.posColor.ColorValue;
            posStylePreview.CircleColor = m_Copy.circleColor.ColorValue;
            posStylePreview.MultiplierColor = m_Copy.multiplierColor.ColorValue;
            posStylePreview.GroupColor = m_Copy.groupColor.ColorValue;
            posStylePreview.NoteColor = m_Copy.noteColor.ColorValue;
            posStylePreview.CurrentGroupHighlightColor = m_Copy.currentGroupHighlightColor.ColorValue;

            if (m_Copy.displayUnits == PosGroup.DrawingUnits.Millimeter)
                posStylePreview.SetPos("1", "2x4", "16", "200", "2400");
            else
                posStylePreview.SetPos("1", "2x4", "16", "20", "240");

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in m_TextStyles)
            {
                if (item.Value == m_Copy.textStyleId)
                {
                    cbTextStyle.SelectedIndex = i;
                }
                if (item.Value == m_Copy.noteStyleId)
                {
                    cbNoteStyle.SelectedIndex = i;
                }
                i++;
            }

            txtNoteScale.Text = m_Copy.noteScale.ToString();
        }

        private void cbDrawingUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.drawingUnits = (cbDrawingUnit.SelectedIndex == 0 ? PosGroup.DrawingUnits.Millimeter : PosGroup.DrawingUnits.Centimeter);
        }

        private void cbDisplayUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.displayUnits = (cbDisplayUnit.SelectedIndex == 0 ? PosGroup.DrawingUnits.Millimeter : PosGroup.DrawingUnits.Centimeter);

            if (m_Copy.displayUnits == PosGroup.DrawingUnits.Millimeter)
                posStylePreview.SetPos("1", "2x4", "16", "200", "2400");
            else
                posStylePreview.SetPos("1", "2x4", "16", "20", "240");
        }

        private void udPrecision_ValueChanged(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.precision = (int)udPrecision.Value;
        }

        private void chkBending_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.bending = chkBending.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Apply changes
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(PosGroup.GroupId, OpenMode.ForWrite) as PosGroup;

                    group.DrawingUnit = m_Copy.drawingUnits;
                    group.DisplayUnit = m_Copy.displayUnits;
                    group.Precision = m_Copy.precision;
                    group.MaxBarLength = m_Copy.maxLength;
                    group.Bending = m_Copy.bending;

                    group.Formula = m_Copy.formula;
                    group.FormulaLengthOnly = m_Copy.formulaLengthOnly;
                    group.FormulaPosOnly = m_Copy.formulaPosOnly;

                    group.StandardDiameters = m_Copy.standardDiameters;

                    group.TextColor = m_Copy.textColor;
                    group.PosColor = m_Copy.posColor;
                    group.CircleColor = m_Copy.circleColor;
                    group.MultiplierColor = m_Copy.multiplierColor;
                    group.GroupColor = m_Copy.groupColor;
                    group.NoteColor = m_Copy.noteColor;
                    group.CurrentGroupHighlightColor = m_Copy.currentGroupHighlightColor;
                    group.CountColor = m_Copy.countColor;

                    group.TextStyleId = m_Copy.textStyleId;
                    group.NoteStyleId = m_Copy.noteStyleId;
                    group.NoteScale = m_Copy.noteScale;

                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            DWGUtility.RefreshAllPos();
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
            if (m_Copy == null) return;
            double val;
            if (double.TryParse(txtMaxLength.Text, out val))
            {
                m_Copy.maxLength = val;
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
                else
                    errorProvider.SetError(txtDiameterList, "");
            }
        }

        private void txtDiameterList_Validated(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            List<int> diameters = new List<int>();
            foreach (string ds in txtDiameterList.Text.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int d = 0;
                if (int.TryParse(ds, out d) && d != 0)
                {
                    diameters.Add(d);
                }
            }
            m_Copy.standardDiameters = string.Join(" ", diameters.ConvertAll<string>(p => p.ToString()).ToArray());
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
            if (m_Copy == null) return;
            double val;
            if (double.TryParse(txtNoteScale.Text, out val))
            {
                m_Copy.noteScale = val;
            }
        }

        private void txtFormula_Validated(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.formula = txtFormula.Text;

            posStylePreview.SetFormula(txtFormula.Text, txtFormulaWithoutLength.Text, txtFormulaPosOnly.Text);
        }

        private void txtFormulaWithoutLength_Validated(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.formulaLengthOnly = txtFormulaWithoutLength.Text;

            posStylePreview.SetFormula(txtFormula.Text, txtFormulaWithoutLength.Text, txtFormulaPosOnly.Text);
        }

        private void txtFormulaPosOnly_Validated(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            m_Copy.formulaPosOnly = txtFormulaPosOnly.Text;

            posStylePreview.SetFormula(txtFormula.Text, txtFormulaWithoutLength.Text, txtFormulaPosOnly.Text);
        }

        private void cbTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            ObjectId id = m_TextStyles[(string)cbTextStyle.SelectedItem];
            m_Copy.textStyleId = id;
        }

        private void cbNoteStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            ObjectId id = m_TextStyles[(string)cbNoteStyle.SelectedItem];
            m_Copy.noteStyleId = id;
        }

        private void btnPickTextColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.textColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.TextColor = m_Copy.textColor.ColorValue;
            }
        }

        private void btnPickPosColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.posColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.PosColor = m_Copy.posColor.ColorValue;
            }
        }

        private void btnPickCircleColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.circleColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CircleColor = m_Copy.circleColor.ColorValue;
            }
        }

        private void btnPickMultiplierColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.multiplierColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.MultiplierColor = m_Copy.multiplierColor.ColorValue;
            }
        }

        private void btnPickGroupColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.groupColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.GroupColor = m_Copy.groupColor.ColorValue;
            }
        }

        private void btnPickCurrentGroupColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.currentGroupHighlightColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CurrentGroupHighlightColor = m_Copy.currentGroupHighlightColor.ColorValue;
            }
        }

        private void btnPickNoteColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.noteColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.NoteColor = m_Copy.noteColor.ColorValue;
            }
        }

        private void btnPickCountColor_Click(object sender, EventArgs e)
        {
            if (m_Copy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                m_Copy.countColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
            }
        }
    }
}
