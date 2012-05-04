using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Interop;

namespace RebarPosCommands
{
    public partial class EditPosForm : Form
    {
        ObjectId m_Pos;
        ObjectId m_Group;
        ObjectId m_Shape;
        List<int> m_StandardDiameters;
        Dictionary<string, ObjectId> m_Groups;
        Dictionary<string, ObjectId> m_Shapes;
        RebarPos.HitTestResult hit;

        public EditPosForm()
        {
            InitializeComponent();

            m_Pos = ObjectId.Null;
            m_Group = ObjectId.Null;
            m_Shape = ObjectId.Null;
            m_StandardDiameters = new List<int>();
            m_Groups = new Dictionary<string, ObjectId>();
            m_Shapes = new Dictionary<string, ObjectId>();

            AcadPreferences pref = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            uint indexColor = pref.Display.GraphicsWinModelBackgrndColor;
            posShapeView.BackColor = ColorTranslator.FromOle((int)indexColor);
        }

        public bool Init(ObjectId id, Point3d pt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    m_Pos = id;

                    RebarPos pos = tr.GetObject(m_Pos, OpenMode.ForRead) as RebarPos;
                    if (pos == null) return false;

                    m_Group = pos.GroupId;
                    m_Shape = pos.ShapeId;

                    PosShape shape = tr.GetObject(m_Shape, OpenMode.ForRead) as PosShape;
                    if (shape == null) return false;
                    PosGroup group = tr.GetObject(m_Group, OpenMode.ForRead) as PosGroup;
                    if (group == null) return false;

                    m_StandardDiameters = new List<int>();
                    string stdd = group.StandardDiameters;
                    foreach (string ds in stdd.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int d;
                        if (int.TryParse(ds, out d))
                        {
                            m_StandardDiameters.Add(d);
                        }
                    }

                    m_Groups = DWGUtility.GetGroups();
                    if (m_Groups.Count == 0)
                    {
                        return false;
                    }

                    m_Shapes = DWGUtility.GetShapes();
                    if (m_Shapes.Count == 0)
                    {
                        return false;
                    }

                    txtPosMarker.Text = pos.Pos;
                    txtPosCount.Text = pos.Count;
                    cbPosDiameter.Items.Clear();
                    foreach (int d in m_StandardDiameters)
                    {
                        cbPosDiameter.Items.Add(d.ToString());
                    }
                    cbPosDiameter.Text = pos.Diameter;
                    txtPosSpacing.Text = pos.Spacing;
                    txtPosMultiplier.Text = pos.Multiplier.ToString();
                    chkIncludePos.Checked = (pos.Multiplier > 0);
                    txtPosMultiplier.Enabled = (pos.Multiplier > 0);
                    txtPosNote.Text = pos.Note;

                    int i = 0;
                    foreach (KeyValuePair<string, ObjectId> pair in m_Groups)
                    {
                        cbGroup.Items.Add(pair.Key);
                        if (pair.Value == pos.GroupId) cbGroup.SelectedIndex = i;
                        i++;
                    }
                    int precision = group.Precision;
                    string shapename = "";
                    foreach (KeyValuePair<string, ObjectId> pair in m_Shapes)
                    {
                        if (pair.Value == pos.ShapeId)
                        {
                            shapename = pair.Key;
                            break;
                        }
                    }

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

                    if (!txtA.Enabled) txtA.Text = "";
                    if (!txtB.Enabled) txtB.Text = "";
                    if (!txtC.Enabled) txtC.Text = "";
                    if (!txtD.Enabled) txtD.Text = "";
                    if (!txtE.Enabled) txtE.Text = "";
                    if (!txtF.Enabled) txtF.Text = "";

                    lblPosShape.Text = shapename;
                    lblAverageLength.Text = ((pos.MinLength + pos.MaxLength) / 2.0).ToString("F" + precision.ToString());
                    if (pos.IsVarLength)
                        lblTotalLength.Text = pos.MinLength.ToString("F" + precision.ToString()) + "~" + pos.MaxLength.ToString("F" + precision.ToString());
                    else
                        lblTotalLength.Text = pos.MinLength.ToString("F" + precision.ToString());

                    hit = pos.HitTest(pt);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            UpdateShapeView();

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool haserror = false;
            if (!CheckPosMarker()) haserror = true;
            if (!CheckPosCount()) haserror = true;
            if (!CheckPosDiameter()) haserror = true;
            if (!CheckPosSpacing()) haserror = true;
            if (!CheckPosMultiplier()) haserror = true;
            if (!CheckPosLength(txtA)) haserror = true;
            if (!CheckPosLength(txtB)) haserror = true;
            if (!CheckPosLength(txtC)) haserror = true;
            if (!CheckPosLength(txtD)) haserror = true;
            if (!CheckPosLength(txtE)) haserror = true;
            if (!CheckPosLength(txtF)) haserror = true;

            if (haserror)
            {
                MessageBox.Show("Lütfen hatalı değerleri düzeltin.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    RebarPos pos = tr.GetObject(m_Pos, OpenMode.ForWrite) as RebarPos;
                    if (pos == null) return;

                    pos.Pos = txtPosMarker.Text;
                    pos.Count = txtPosCount.Text;
                    pos.Diameter = cbPosDiameter.Text;
                    pos.Spacing = txtPosSpacing.Text;
                    pos.Multiplier = int.Parse(txtPosMultiplier.Text);
                    if (!chkIncludePos.Checked) pos.Multiplier = 0;
                    pos.Note = txtPosNote.Text;
                    pos.GroupId = m_Groups[(string)cbGroup.SelectedItem];
                    pos.ShapeId = m_Shape;

                    if (rbShowAll.Checked)
                        pos.Display = RebarPos.DisplayStyle.All;
                    else if (rbWithoutLength.Checked)
                        pos.Display = RebarPos.DisplayStyle.WithoutLength;
                    else if (rbMarkerOnly.Checked)
                        pos.Display = RebarPos.DisplayStyle.MarkerOnly;

                    pos.A = txtA.Text;
                    pos.B = txtB.Text;
                    pos.C = txtC.Text;
                    pos.D = txtD.Text;
                    pos.E = txtE.Text;
                    pos.F = txtF.Text;

                    tr.Commit();

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelectA_Click(object sender, EventArgs e)
        {
            Hide();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityResult per = ed.GetEntity("\nSelect entity: ");
            Show();
        }

        private void posShapeView_Click(object sender, EventArgs e)
        {
            SelectShapeForm form = new SelectShapeForm();
            form.SetShapes(m_Shape);
            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
            {
                m_Shape = form.Current;
                UpdateShapeView();
            }
        }

        private void UpdateShapeView()
        {
            posShapeView.Reset();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosShape shape = tr.GetObject(m_Shape, OpenMode.ForRead) as PosShape;
                    if (shape == null)
                        return;

                    txtA.Enabled = btnSelectA.Enabled = btnMeasureA.Enabled = (shape.Fields >= 1);
                    txtB.Enabled = btnSelectB.Enabled = btnMeasureB.Enabled = (shape.Fields >= 2);
                    txtC.Enabled = btnSelectC.Enabled = btnMeasureC.Enabled = (shape.Fields >= 3);
                    txtD.Enabled = btnSelectD.Enabled = btnMeasureD.Enabled = (shape.Fields >= 4);
                    txtE.Enabled = btnSelectE.Enabled = btnMeasureE.Enabled = (shape.Fields >= 5);
                    txtF.Enabled = btnSelectF.Enabled = btnMeasureF.Enabled = (shape.Fields >= 6);

                    if (!txtA.Enabled) txtA.Text = "";
                    if (!txtB.Enabled) txtB.Text = "";
                    if (!txtC.Enabled) txtC.Text = "";
                    if (!txtD.Enabled) txtD.Text = "";
                    if (!txtE.Enabled) txtE.Text = "";
                    if (!txtF.Enabled) txtF.Text = "";

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
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkIncludePos_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIncludePos.Checked)
            {
                txtPosMultiplier.Text = "0";
                txtPosMultiplier.Enabled = false;
            }
            else
            {
                int mult = 0;
                if (!int.TryParse(txtPosMultiplier.Text, out mult))
                    mult = 0;
                if (mult == 0)
                    txtPosMultiplier.Text = "1";
                txtPosMultiplier.Enabled = true;
            }
        }

        private void EditPosForm_Shown(object sender, EventArgs e)
        {
            switch (hit)
            {
                case RebarPos.HitTestResult.Count:
                    txtPosCount.Select();
                    txtPosCount.SelectAll();
                    break;
                case RebarPos.HitTestResult.Diameter:
                    cbPosDiameter.Select();
                    cbPosDiameter.SelectAll();
                    break;
                case RebarPos.HitTestResult.Length:
                    txtA.Select();
                    txtA.SelectAll();
                    break;
                case RebarPos.HitTestResult.Group:
                    cbGroup.SelectAll();
                    break;
                case RebarPos.HitTestResult.Multiplier:
                    txtPosMultiplier.Select();
                    txtPosMultiplier.SelectAll();
                    break;
                case RebarPos.HitTestResult.Note:
                    txtPosNote.Select();
                    txtPosNote.SelectAll();
                    break;
                case RebarPos.HitTestResult.Pos:
                    txtPosMarker.Select();
                    txtPosMarker.SelectAll();
                    break;
                case RebarPos.HitTestResult.Spacing:
                    txtPosSpacing.Select();
                    txtPosSpacing.SelectAll();
                    break;
                default:
                    txtPosMarker.Select();
                    txtPosMarker.SelectAll();
                    break;
            }
        }

        private void txtPosMarker_Validating(object sender, CancelEventArgs e)
        {
            CheckPosMarker();
        }

        private void txtPosCount_Validating(object sender, CancelEventArgs e)
        {
            CheckPosCount();
        }

        private void cbPosDiameter_Validating(object sender, CancelEventArgs e)
        {
            CheckPosDiameter();
        }

        private void txtPosSpacing_Validating(object sender, CancelEventArgs e)
        {
            CheckPosSpacing();
        }

        private void txtPosMultiplier_Validating(object sender, CancelEventArgs e)
        {
            if (CheckPosMultiplier())
            {
                int mult = 0;
                int.TryParse(txtPosMultiplier.Text, out mult);

                if (mult == 0)
                {
                    chkIncludePos.Checked = false;
                    txtPosMultiplier.Enabled = false;
                }
            }
        }

        private void txtLength_Validating(object sender, CancelEventArgs e)
        {
            CheckPosLength((TextBox)sender);
        }

        private bool CheckPosMarker()
        {
            int val = 0;
            if (string.IsNullOrEmpty(txtPosMarker.Text) || int.TryParse(txtPosMarker.Text, out val))
            {
                errorProvider.SetError(txtPosMarker, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtPosMarker, "Poz numarası tamsayı olmalıdır.");
                errorProvider.SetIconAlignment(txtPosMarker, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosCount()
        {
            string str = txtPosCount.Text;
            str = str.Replace('x', '*');
            str = str.Replace('X', '*');

            if (string.IsNullOrEmpty(str) || Calculator.IsValid(str))
            {
                errorProvider.SetError(txtPosCount, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtPosCount, "Poz adedi yalnız rakam ve aritmetik işlemler içerebilir.");
                errorProvider.SetIconAlignment(txtPosCount, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosDiameter()
        {
            if (string.IsNullOrEmpty(cbPosDiameter.Text) || cbPosDiameter.Items.Contains(cbPosDiameter.Text))
            {
                errorProvider.SetError(cbPosDiameter, "");
                return true;
            }
            else
            {
                errorProvider.SetError(cbPosDiameter, "Poz adedi standart çap listesi içinden seçilmelidir.");
                errorProvider.SetIconAlignment(cbPosDiameter, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosSpacing()
        {
            if (string.IsNullOrEmpty(txtPosSpacing.Text) || txtPosSpacing.Text.Trim(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', '-', '~' }).Length == 0)
            {
                errorProvider.SetError(txtPosSpacing, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtPosSpacing, "Poz aralığı yalnız rakam ve aralık işareti (~ veya -) içerebilir.");
                errorProvider.SetIconAlignment(txtPosSpacing, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosMultiplier()
        {
            int mult = 0;
            if (string.IsNullOrEmpty(txtPosMultiplier.Text) || int.TryParse(txtPosMultiplier.Text, out mult))
            {
                errorProvider.SetError(txtPosMultiplier, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtPosMultiplier, "Poz çarpanı tamsayı olmalıdır.");
                errorProvider.SetIconAlignment(txtPosMultiplier, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosLength(TextBox source)
        {
            // Split var lengths
            string[] strparts = source.Text.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);

            bool isempty = false;
            bool haserror = false;
            foreach (string str in strparts)
            {
                string oldstr = str;
                oldstr = oldstr.Replace('d', '0');
                oldstr = oldstr.Replace('r', '0');
                oldstr = oldstr.Replace('x', '*');
                oldstr = oldstr.Replace('X', '*');

                if (string.IsNullOrEmpty(oldstr))
                {
                    isempty = true;
                    break;
                }
                else if (!Calculator.IsValid(oldstr))
                {
                    haserror = true;
                    break;
                }
            }

            if (isempty)
            {
                errorProvider.SetError(source, "Lütfen parça boyunu girin.");
                errorProvider.SetIconAlignment(source, ErrorIconAlignment.MiddleLeft);
                return false;
            }
            else if (haserror)
            {
                errorProvider.SetError(source, "Parça boyu yalnız rakam ve aritmetik işlemler içerebilir.");
                errorProvider.SetIconAlignment(source, ErrorIconAlignment.MiddleLeft);
                return false;
            }
            else
            {
                errorProvider.SetError(source, "");
                return true;
            }
        }
    }
}
