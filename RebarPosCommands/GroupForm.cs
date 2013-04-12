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
            public PosGroup.DrawingUnits DrawingUnit;
            public PosGroup.DrawingUnits DisplayUnit;
            public int Precision;
            public double MaxBarLength;
            public bool Bending;

            public string Formula;
            public string FormulaVariableLength;
            public string FormulaLengthOnly;
            public string FormulaPosOnly;

            public string StandardDiameters;

            public Autodesk.AutoCAD.Colors.Color TextColor;
            public Autodesk.AutoCAD.Colors.Color PosColor;
            public Autodesk.AutoCAD.Colors.Color CircleColor;
            public Autodesk.AutoCAD.Colors.Color MultiplierColor;
            public Autodesk.AutoCAD.Colors.Color GroupColor;
            public Autodesk.AutoCAD.Colors.Color NoteColor;
            public Autodesk.AutoCAD.Colors.Color CurrentGroupHighlightColor;
            public Autodesk.AutoCAD.Colors.Color CountColor;

            public ObjectId TextStyleId;
            public ObjectId NoteStyleId;

            public double NoteScale;
        }

        private GroupCopy mCopy;
        private Dictionary<string, ObjectId> mTextStyles;

        public GroupForm()
        {
            InitializeComponent();
            Width = 425;

            mCopy = new GroupCopy();
            mTextStyles = new Dictionary<string, ObjectId>();

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

                    mCopy.DrawingUnit = group.DrawingUnit;
                    mCopy.DisplayUnit = group.DisplayUnit;
                    mCopy.Precision = group.Precision;
                    mCopy.MaxBarLength = group.MaxBarLength;
                    mCopy.Bending = group.Bending;

                    mCopy.Formula = group.Formula;
                    mCopy.FormulaVariableLength = group.FormulaVariableLength;
                    mCopy.FormulaLengthOnly = group.FormulaLengthOnly;
                    mCopy.FormulaPosOnly = group.FormulaPosOnly;

                    mCopy.StandardDiameters = group.StandardDiameters;

                    mCopy.TextColor = group.TextColor;
                    mCopy.PosColor = group.PosColor;
                    mCopy.CircleColor = group.CircleColor;
                    mCopy.MultiplierColor = group.MultiplierColor;
                    mCopy.GroupColor = group.GroupColor;
                    mCopy.NoteColor = group.NoteColor;
                    mCopy.CurrentGroupHighlightColor = group.CurrentGroupHighlightColor;
                    mCopy.CountColor = group.CountColor;

                    mCopy.TextStyleId = group.TextStyleId;
                    mCopy.NoteStyleId = group.NoteStyleId;
                    mCopy.NoteScale = group.NoteScale;
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            
            mTextStyles = DWGUtility.GetTextStyles();
            foreach (string name in mTextStyles.Keys)
            {
                cbTextStyle.Items.Add(name);
                cbNoteStyle.Items.Add(name);
            }
            
            SetGroup();
            
            return true;
        }

        public void SetGroup()
        {
            cbDrawingUnit.SelectedIndex = (mCopy.DrawingUnit == PosGroup.DrawingUnits.Millimeter ? 0 : 1);
            cbDisplayUnit.SelectedIndex = (mCopy.DisplayUnit == PosGroup.DrawingUnits.Millimeter ? 0 : 1);
            udPrecision.Value = mCopy.Precision;
            txtMaxLength.Text = mCopy.MaxBarLength.ToString();
            chkBending.Checked = mCopy.Bending;

            txtFormula.Text = mCopy.Formula;
            txtFormulaVariableLength.Text = mCopy.FormulaVariableLength;
            txtFormulaLengthOnly.Text = mCopy.FormulaLengthOnly;
            txtFormulaPosOnly.Text = mCopy.FormulaPosOnly;

            txtDiameterList.Text = mCopy.StandardDiameters;

            btnPickTextColor.BackColor = mCopy.TextColor.ColorValue;
            btnPickPosColor.BackColor = mCopy.PosColor.ColorValue;
            btnPickCircleColor.BackColor = mCopy.CircleColor.ColorValue;
            btnPickMultiplierColor.BackColor = mCopy.MultiplierColor.ColorValue;
            btnPickGroupColor.BackColor = mCopy.GroupColor.ColorValue;
            btnPickNoteColor.BackColor = mCopy.NoteColor.ColorValue;
            btnPickCountColor.BackColor = mCopy.CountColor.ColorValue;

            posStylePreview.SuspendUpdate();

            posStylePreview.Formula = mCopy.Formula;
            posStylePreview.FormulaVariableLength = mCopy.FormulaVariableLength;
            posStylePreview.FormulaLengthOnly = mCopy.FormulaLengthOnly;
            posStylePreview.FormulaPosOnly = mCopy.FormulaPosOnly;

            posStylePreview.TextColor = mCopy.TextColor.ColorValue;
            posStylePreview.PosColor = mCopy.PosColor.ColorValue;
            posStylePreview.CircleColor = mCopy.CircleColor.ColorValue;
            posStylePreview.MultiplierColor = mCopy.MultiplierColor.ColorValue;
            posStylePreview.GroupColor = mCopy.GroupColor.ColorValue;
            posStylePreview.NoteColor = mCopy.NoteColor.ColorValue;
            posStylePreview.CurrentGroupHighlightColor = mCopy.CurrentGroupHighlightColor.ColorValue;
            posStylePreview.CountColor = mCopy.CountColor.ColorValue;

            posStylePreview.TextStyleId = mCopy.TextStyleId;
            posStylePreview.NoteStyleId = mCopy.NoteStyleId;
            posStylePreview.NoteScale = mCopy.NoteScale;

            posStylePreview.SetGroup();
            posStylePreview.ResumeUpdate();

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in mTextStyles)
            {
                if (item.Value == mCopy.TextStyleId)
                {
                    cbTextStyle.SelectedIndex = i;
                }
                if (item.Value == mCopy.NoteStyleId)
                {
                    cbNoteStyle.SelectedIndex = i;
                }
                i++;
            }

            txtNoteScale.Text = mCopy.NoteScale.ToString();
        }

        private void cbDrawingUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.DrawingUnit = (cbDrawingUnit.SelectedIndex == 0 ? PosGroup.DrawingUnits.Millimeter : PosGroup.DrawingUnits.Centimeter);
        }

        private void cbDisplayUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.DisplayUnit = (cbDisplayUnit.SelectedIndex == 0 ? PosGroup.DrawingUnits.Millimeter : PosGroup.DrawingUnits.Centimeter);
        }

        private void udPrecision_ValueChanged(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.Precision = (int)udPrecision.Value;
        }

        private void chkBending_CheckedChanged(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.Bending = chkBending.Checked;
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

                    group.DrawingUnit = mCopy.DrawingUnit;
                    group.DisplayUnit = mCopy.DisplayUnit;
                    group.Precision = mCopy.Precision;
                    group.MaxBarLength = mCopy.MaxBarLength;
                    group.Bending = mCopy.Bending;

                    group.Formula = mCopy.Formula;
                    group.FormulaVariableLength = mCopy.FormulaVariableLength;
                    group.FormulaLengthOnly = mCopy.FormulaLengthOnly;
                    group.FormulaPosOnly = mCopy.FormulaPosOnly;

                    group.StandardDiameters = mCopy.StandardDiameters;

                    group.TextColor = mCopy.TextColor;
                    group.PosColor = mCopy.PosColor;
                    group.CircleColor = mCopy.CircleColor;
                    group.MultiplierColor = mCopy.MultiplierColor;
                    group.GroupColor = mCopy.GroupColor;
                    group.NoteColor = mCopy.NoteColor;
                    group.CurrentGroupHighlightColor = mCopy.CurrentGroupHighlightColor;
                    group.CountColor = mCopy.CountColor;

                    group.TextStyleId = mCopy.TextStyleId;
                    group.NoteStyleId = mCopy.NoteStyleId;
                    group.NoteScale = mCopy.NoteScale;

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
            if (mCopy == null) return;
            double val;
            if (double.TryParse(txtMaxLength.Text, out val))
            {
                mCopy.MaxBarLength = val;
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
            if (mCopy == null) return;
            List<int> diameters = new List<int>();
            foreach (string ds in txtDiameterList.Text.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int d = 0;
                if (int.TryParse(ds, out d) && d != 0)
                {
                    diameters.Add(d);
                }
            }
            mCopy.StandardDiameters = string.Join(" ", diameters.ConvertAll<string>(p => p.ToString()).ToArray());
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
            if (mCopy == null) return;
            double val;
            if (double.TryParse(txtNoteScale.Text, out val))
            {
                mCopy.NoteScale = val;
                posStylePreview.NoteScale = val;
                posStylePreview.SetGroup();
            }
        }

        private void txtFormula_Validated(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.Formula = txtFormula.Text;
            posStylePreview.Formula = txtFormula.Text;
            posStylePreview.SetGroup();
        }

        private void txtFormulaVariableLength_Validated(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.FormulaVariableLength = txtFormulaVariableLength.Text;
            posStylePreview.FormulaVariableLength = txtFormulaVariableLength.Text;
            posStylePreview.SetGroup();
        }

        private void txtFormulaLengthOnly_Validated(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.FormulaLengthOnly = txtFormulaLengthOnly.Text;
            posStylePreview.FormulaLengthOnly = txtFormulaLengthOnly.Text;
            posStylePreview.SetGroup();
        }

        private void txtFormulaPosOnly_Validated(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            mCopy.FormulaPosOnly = txtFormulaPosOnly.Text;
            posStylePreview.FormulaPosOnly = txtFormulaPosOnly.Text;
            posStylePreview.SetGroup();
        }

        private void cbTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            ObjectId id = mTextStyles[(string)cbTextStyle.SelectedItem];
            mCopy.TextStyleId = id;
            posStylePreview.TextStyleId = id;
            posStylePreview.SetGroup();
        }

        private void cbNoteStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            ObjectId id = mTextStyles[(string)cbNoteStyle.SelectedItem];
            mCopy.NoteStyleId = id;
            posStylePreview.NoteStyleId = id;
            posStylePreview.SetGroup();
        }

        private void btnPickTextColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.TextColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.TextColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnPickPosColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.PosColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.PosColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnPickCircleColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.CircleColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CircleColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnPickMultiplierColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.MultiplierColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.MultiplierColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();

            }
        }

        private void btnPickGroupColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.GroupColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.GroupColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnPickCurrentGroupColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.CurrentGroupHighlightColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CurrentGroupHighlightColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnPickNoteColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.NoteColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.NoteColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnPickCountColor_Click(object sender, EventArgs e)
        {
            if (mCopy == null) return;
            Button btn = (Button)sender;
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
            cd.Color = Autodesk.AutoCAD.Colors.Color.FromColor(btn.BackColor);
            cd.IncludeByBlockByLayer = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                mCopy.CountColor = cd.Color;
                btn.BackColor = cd.Color.ColorValue;
                posStylePreview.CountColor = cd.Color.ColorValue;
                posStylePreview.SetGroup();
            }
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            Width = 1038;
            gbDisplay.Visible = true;
            btnExpand.Visible = false;
            gbDisplay.Focus();
        }

        private void btnDisplayStandard_Click(object sender, EventArgs e)
        {
            mCopy.Formula = "[M:C][N][TD][\"/\":S]";
            mCopy.FormulaVariableLength = "[M:C][N][TD][\"/\":S]";
            mCopy.FormulaLengthOnly = "[\"L=\":L]";
            mCopy.FormulaPosOnly = "[M:C]";
            mCopy.TextColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2);
            mCopy.PosColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4);
            mCopy.CircleColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1);
            mCopy.MultiplierColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 33);
            mCopy.GroupColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 9);
            mCopy.NoteColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 30);
            mCopy.CurrentGroupHighlightColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 8);
            mCopy.CountColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 5);

            SetGroup();
        }

        private void btnDisplayBS_Click(object sender, EventArgs e)
        {
            mCopy.Formula = "[N] [\"T\":D] [MM] [S]";
            mCopy.FormulaVariableLength = "[N] [\"T\":D] [MM] [\"(1-\":NX:\")\"] [S]";
            mCopy.FormulaLengthOnly = "[\"L=\":L]";
            mCopy.FormulaPosOnly = "[M]";
            mCopy.TextColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 2);
            mCopy.PosColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 4);
            mCopy.CircleColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 1);
            mCopy.MultiplierColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 33);
            mCopy.GroupColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 9);
            mCopy.NoteColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 30);
            mCopy.CurrentGroupHighlightColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 8);
            mCopy.CountColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 5);

            SetGroup();
        }
    }
}
