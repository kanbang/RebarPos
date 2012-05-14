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
        private enum UpdateLengthResult
        {
            OK = 1,
            SystemError = 2,
            ExceedsMaximum = 3,
            InvalidLength = 4
        }

        ObjectId m_Pos;
        ObjectId m_Group;
        ObjectId m_Shape;
        List<int> m_StandardDiameters;
        Dictionary<string, ObjectId> m_Groups;
        Dictionary<string, ObjectId> m_Shapes;
        RebarPos.HitTestResult hit;
        string m_Formula;
        bool m_Bending;
        int m_Fields;
        int m_Precision;
        PosGroup.DrawingUnits m_DisplayUnits;
        PosGroup.DrawingUnits m_DrawingUnits;
        double m_MaxLength;

        bool init = false;

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
            init = true;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    m_Pos = id;

                    RebarPos pos = tr.GetObject(m_Pos, OpenMode.ForRead) as RebarPos;
                    if (pos == null)
                    {
                        init = false;
                        return false;
                    }

                    m_Group = pos.GroupId;
                    m_Shape = pos.ShapeId;

                    m_Groups = DWGUtility.GetGroups();
                    if (m_Groups.Count == 0)
                    {
                        init = false;
                        return false;
                    }

                    int i = 0;
                    foreach (KeyValuePair<string, ObjectId> pair in m_Groups)
                    {
                        cbGroup.Items.Add(pair.Key);
                        if (pair.Value == pos.GroupId) cbGroup.SelectedIndex = i;
                        i++;
                    }

                    m_Shapes = DWGUtility.GetShapes();
                    if (m_Shapes.Count == 0)
                    {
                        init = false;
                        return false;
                    }

                    txtPosMarker.Text = pos.Pos;
                    txtPosCount.Text = pos.Count;
                    cbPosDiameter.Text = pos.Diameter;
                    txtPosSpacing.Text = pos.Spacing;
                    txtPosMultiplier.Text = pos.Multiplier.ToString();
                    chkIncludePos.Checked = (pos.Multiplier > 0);
                    txtPosMultiplier.Enabled = (pos.Multiplier > 0);
                    txtPosNote.Text = pos.Note;

                    txtA.Text = pos.A;
                    txtB.Text = pos.B;
                    txtC.Text = pos.C;
                    txtD.Text = pos.D;
                    txtE.Text = pos.E;
                    txtF.Text = pos.F;

                    rbShowAll.Checked = (pos.Display == RebarPos.DisplayStyle.All);
                    rbWithoutLength.Checked = (pos.Display == RebarPos.DisplayStyle.WithoutLength);
                    rbMarkerOnly.Checked = (pos.Display == RebarPos.DisplayStyle.MarkerOnly);

                    if (!SetGroup())
                    {
                        init = false;
                        return false;
                    }
                    if (!SetShape())
                    {
                        init = false;
                        return false;
                    }

                    UpdateLength();

                    hit = pos.HitTest(pt);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    init = false;
                    return false;
                }
            }

            init = false;
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
            if (m_Fields >= 1)
                if (!CheckPosLength(txtA)) haserror = true;
            if (m_Fields >= 2)
                if (!CheckPosLength(txtB)) haserror = true;
            if (m_Fields >= 3)
                if (!CheckPosLength(txtC)) haserror = true;
            if (m_Fields >= 4)
                if (!CheckPosLength(txtD)) haserror = true;
            if (m_Fields >= 5)
                if (!CheckPosLength(txtE)) haserror = true;
            if (m_Fields >= 6)
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
                    pos.GroupId = m_Group;
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

        private void GetLengthFromEntity(TextBox txt)
        {
            Hide();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityOptions opts = new PromptEntityOptions("\nSelect entity: ");
            opts.SetRejectMessage("\nSelect a LINE, TEXT, MTEXT or DIMENSION entity.");
            opts.AddAllowedClass(typeof(Line), false);
            opts.AddAllowedClass(typeof(DBText), false);
            opts.AddAllowedClass(typeof(MText), false);
            opts.AddAllowedClass(typeof(Dimension), false);
            PromptEntityResult per = ed.GetEntity(opts);
            Show();
            if (per.Status == PromptStatus.OK)
            {
                ObjectId id = per.ObjectId;
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        DBObject obj = tr.GetObject(per.ObjectId, OpenMode.ForRead);
                        if (obj is Line)
                        {
                            Line dobj = obj as Line;
                            txt.Text = dobj.Length.ToString();
                        }
                        else if (obj is DBText)
                        {
                            DBText dobj = obj as DBText;
                            txt.Text = dobj.TextString;
                        }
                        else if (obj is MText)
                        {
                            MText dobj = obj as MText;
                            txt.Text = dobj.Text;
                        }
                        else if (obj is Dimension)
                        {
                            Dimension dobj = obj as Dimension;
                            txt.Text = dobj.Measurement.ToString();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void GetLengthFromMeasurement(TextBox txt)
        {
            Hide();
            string length = "";
            double length1 = 0;
            if (GetDistance("\nFirst point: ", "\nSecond point: ", out length1) == PromptStatus.OK)
            {
                length = length1.ToString("F0");
                double length2 = 0;
                if (GetDistance("\n" + length + "-... First point: ", "\n" + length + "-... Second point: ", out length2, true) == PromptStatus.OK)
                {
                    length += "~" + length2.ToString("F0");
                }
            }
            Show();
            txt.Text = length;
        }

        private PromptStatus GetDistance(string message1, string message2, out double length, bool allowEmpty)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointOptions opt1 = new PromptPointOptions(message1);
            opt1.AllowNone = allowEmpty;
            PromptPointResult res1 = ed.GetPoint(opt1);
            if (res1.Status == PromptStatus.OK)
            {
                PromptDistanceOptions opt2 = new PromptDistanceOptions(message2);
                opt2.AllowNone = allowEmpty;
                opt2.BasePoint = res1.Value;
                opt2.UseBasePoint = true;
                PromptDoubleResult res2 = ed.GetDistance(opt2);
                if (res2.Status == PromptStatus.OK)
                {
                    length = res2.Value;
                    return PromptStatus.OK;
                }
            }

            length = 0;
            return PromptStatus.Cancel;
        }

        private PromptStatus GetDistance(string message1, string message2, out double length)
        {
            return GetDistance(message1, message2, out length, false);
        }

        private void posShapeView_Click(object sender, EventArgs e)
        {
            SelectShapeForm form = new SelectShapeForm();
            form.SetShapes(m_Shape);
            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
            {
                m_Shape = form.Current;
                SetShape();
                UpdateLength();
            }
        }

        private bool SetGroup()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
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

                    m_DisplayUnits = group.DisplayUnit;
                    m_DrawingUnits = group.DrawingUnit;
                    m_Bending = group.Bending;
                    m_Precision = group.Precision;
                    m_MaxLength = group.MaxBarLength;

                    cbPosDiameter.Items.Clear();
                    foreach (int d in m_StandardDiameters)
                    {
                        cbPosDiameter.Items.Add(d.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private bool SetShape()
        {
            posShapeView.Reset();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosShape shape = tr.GetObject(m_Shape, OpenMode.ForRead) as PosShape;
                    if (shape == null)
                        return false;

                    if (m_Bending)
                        m_Formula = shape.FormulaBending;
                    else
                        m_Formula = shape.Formula;

                    m_Fields = shape.Fields;
                    txtA.Enabled = btnSelectA.Enabled = btnMeasureA.Enabled = (m_Fields >= 1);
                    txtB.Enabled = btnSelectB.Enabled = btnMeasureB.Enabled = (m_Fields >= 2);
                    txtC.Enabled = btnSelectC.Enabled = btnMeasureC.Enabled = (m_Fields >= 3);
                    txtD.Enabled = btnSelectD.Enabled = btnMeasureD.Enabled = (m_Fields >= 4);
                    txtE.Enabled = btnSelectE.Enabled = btnMeasureE.Enabled = (m_Fields >= 5);
                    txtF.Enabled = btnSelectF.Enabled = btnMeasureF.Enabled = (m_Fields >= 6);

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

                    string shapename = "";
                    foreach (KeyValuePair<string, ObjectId> pair in m_Shapes)
                    {
                        if (pair.Value == m_Shape)
                        {
                            shapename = pair.Key;
                            break;
                        }
                    }
                    lblPosShape.Text = shapename;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private UpdateLengthResult UpdateLength()
        {
            // Get lengths
            double minLengthMM = 0;
            double maxLengthMM = 0;
            bool isVarLength = false;
            bool check = false;
            try
            {
                check = RebarPos.GetTotalLengths(m_Formula, m_Fields, m_DrawingUnits, txtA.Text, txtB.Text, txtC.Text, txtD.Text, txtE.Text, txtF.Text, cbPosDiameter.Text, out minLengthMM, out maxLengthMM, out isVarLength);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return UpdateLengthResult.SystemError;
            }

            if (check && (minLengthMM > double.Epsilon) && (maxLengthMM > double.Epsilon))
            {
                string unitPrefix = "mm";
                switch (m_DrawingUnits)
                {
                    case PosGroup.DrawingUnits.Millimeter:
                        unitPrefix = "mm";
                        break;
                    case PosGroup.DrawingUnits.Centimeter:
                        unitPrefix = "cm";
                        break;
                }

                // Scale from MM to drawing units
                double scale = RebarPos.ConvertLength(1.0, PosGroup.DrawingUnits.Millimeter, m_DrawingUnits);
                double minLength = minLengthMM * scale;
                double maxLength = maxLengthMM * scale;

                if (isVarLength)
                {
                    lblTotalLength.Text = minLength.ToString("F" + m_Precision.ToString()) + "~" + maxLength.ToString("F" + m_Precision.ToString()) + " " + unitPrefix +
                         " (" + (minLengthMM / 1000.0).ToString("F2") + " m ~ " + (maxLengthMM / 1000.0).ToString("F2") + " m)";
                    lblAverageLength.Text = ((minLength + maxLength) / 2.0).ToString("F" + m_Precision.ToString()) + " " + unitPrefix +
                        " (" + ((minLengthMM + maxLengthMM) / 2.0 / 1000.0).ToString("F2") + " m)";

                    lblAverageLengthCaption.Visible = true;
                    lblAverageLength.Visible = true;
                }
                else
                {
                    lblTotalLength.Text = minLength.ToString("F" + m_Precision.ToString()) + " " + unitPrefix +
                        " (" + (minLengthMM / 1000.0).ToString("F2") + " m)";

                    lblAverageLengthCaption.Visible = false;
                    lblAverageLength.Visible = false;
                }

                if (minLengthMM > m_MaxLength * 1000.0 || maxLengthMM > m_MaxLength * 1000.0)
                {
                    lblAverageLength.ForeColor = Color.Red;
                    lblTotalLength.ForeColor = Color.Red;

                    return UpdateLengthResult.ExceedsMaximum;
                }
                else
                {
                    lblAverageLength.ForeColor = SystemColors.ControlText;
                    lblTotalLength.ForeColor = SystemColors.ControlText;

                    return UpdateLengthResult.OK;
                }
            }
            else
            {
                lblTotalLength.Text = "Hatalı Boy!";
                lblTotalLength.ForeColor = Color.Red;

                lblAverageLengthCaption.Visible = false;
                lblAverageLength.Visible = false;

                return UpdateLengthResult.InvalidLength;
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

            if (string.IsNullOrEmpty(str) || Utility.ValidateFormula(str))
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
                UpdateLength();
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
            if (txtPosSpacing.IsValid)
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
            bool isempty = false;
            bool haserror = false;

            // Split var lengths
            if (string.IsNullOrEmpty(source.Text))
            {
                isempty = true;
            }
            else
            {
                string[] strparts = source.Text.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);

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
                    else if (!Utility.ValidateFormula(oldstr))
                    {
                        haserror = true;
                        break;
                    }
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

            UpdateLengthResult check = UpdateLength();
            if (check == UpdateLengthResult.OK)
            {
                errorProvider.SetError(source, "");
                return true;
            }
            else
            {
                if (check == UpdateLengthResult.ExceedsMaximum)
                    errorProvider.SetError(source, "Toplam boy maximum demir boyundan büyük olamaz.");
                else if (check == UpdateLengthResult.InvalidLength)
                    errorProvider.SetError(source, "Geçersiz parça boyu.");
                else
                    errorProvider.SetError(source, "Geçersiz boy.");

                errorProvider.SetIconAlignment(source, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init) return;

            if (cbGroup.SelectedIndex == -1) return;
            m_Group = m_Groups[(string)cbGroup.SelectedItem];

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(m_Group, OpenMode.ForRead) as PosGroup;
                    if (group == null)
                        return;

                    m_DisplayUnits = group.DisplayUnit;
                    m_DrawingUnits = group.DrawingUnit;
                    m_Bending = group.Bending;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            UpdateLength();
        }

        private void btnSelectA_Click(object sender, EventArgs e)
        {
            GetLengthFromEntity(txtA);
        }

        private void btnSelectB_Click(object sender, EventArgs e)
        {
            GetLengthFromEntity(txtB);
        }

        private void btnSelectC_Click(object sender, EventArgs e)
        {
            GetLengthFromEntity(txtC);
        }

        private void btnSelectD_Click(object sender, EventArgs e)
        {
            GetLengthFromEntity(txtD);
        }

        private void btnSelectE_Click(object sender, EventArgs e)
        {
            GetLengthFromEntity(txtE);
        }

        private void btnSelectF_Click(object sender, EventArgs e)
        {
            GetLengthFromEntity(txtF);
        }

        private void btnMeasureA_Click(object sender, EventArgs e)
        {
            GetLengthFromMeasurement(txtA);
        }

        private void btnMeasureB_Click(object sender, EventArgs e)
        {
            GetLengthFromMeasurement(txtB);
        }

        private void btnMeasureC_Click(object sender, EventArgs e)
        {
            GetLengthFromMeasurement(txtC);
        }

        private void btnMeasureD_Click(object sender, EventArgs e)
        {
            GetLengthFromMeasurement(txtD);
        }

        private void btnMeasureE_Click(object sender, EventArgs e)
        {
            GetLengthFromMeasurement(txtE);
        }

        private void btnMeasureF_Click(object sender, EventArgs e)
        {
            GetLengthFromMeasurement(txtF);
        }

        private void btnPickNumber_Click(object sender, EventArgs e)
        {
            Hide();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            TypedValue[] tvs = new TypedValue[] {
                new TypedValue((int)DxfCode.Operator, "<OR"),
                new TypedValue((int)DxfCode.Start, "TEXT"),
                new TypedValue((int)DxfCode.Start, "MTEXT"),
                new TypedValue((int)DxfCode.Operator, "OR>")
            };
            PromptSelectionResult res = ed.GetSelection(new SelectionFilter(tvs));
            Show();
            if (res.Status == PromptStatus.OK)
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        int total = 0;
                        foreach (SelectedObject sobj in res.Value)
                        {
                            string text = "";
                            DBObject obj = tr.GetObject(sobj.ObjectId, OpenMode.ForRead);
                            if (obj is DBText)
                            {
                                DBText dobj = obj as DBText;
                                text = dobj.TextString;
                            }
                            else if (obj is MText)
                            {
                                MText dobj = obj as MText;
                                text = dobj.Text;
                            }
                            if (!string.IsNullOrEmpty(text))
                            {
                                text = text.TrimStart('(').TrimEnd(')');
                                int num = 0;
                                if (int.TryParse(text, out num))
                                {
                                    total += num;
                                }
                            }
                        }
                        if (total != 0)
                        {
                            txtPosCount.Text = total.ToString();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPickSpacing_Click(object sender, EventArgs e)
        {
            Hide();
            double length;
            if (GetDistance("\nStart point: ", "\nEnd point: ", out length) == PromptStatus.OK)
            {
                double spa = 0;
                if (double.TryParse(txtPosSpacing.Text, out spa) && spa > double.Epsilon)
                {
                    int num = 1 + (int)Math.Ceiling(length / spa);
                    txtPosCount.Text = num.ToString();
                }
            }
            Show();
        }
    }
}
