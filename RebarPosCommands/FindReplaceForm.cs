using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.Interop;

namespace RebarPosCommands
{
    public partial class FindReplaceForm : Form
    {
        ObjectId[] m_Selection;

        SortedDictionary<string, List<ObjectId>> m_PosList;
        SortedDictionary<string, List<ObjectId>> m_CountList;
        SortedDictionary<string, List<ObjectId>> m_DiameterList;
        SortedDictionary<string, List<ObjectId>> m_SpacingList;
        SortedDictionary<string, List<ObjectId>> m_NoteList;
        SortedDictionary<int, List<ObjectId>> m_MultiplierList;
        SortedDictionary<ObjectId, List<ObjectId>> m_ShapeList;

        ObjectId m_FindShape;
        ObjectId m_ReplaceShape;
        int m_FindFields;
        int m_ReplaceFields;

        bool init;

        public FindReplaceForm()
        {
            InitializeComponent();

            m_Selection = new ObjectId[0];

            m_PosList = new SortedDictionary<string, List<ObjectId>>();
            m_CountList = new SortedDictionary<string, List<ObjectId>>();
            m_DiameterList = new SortedDictionary<string, List<ObjectId>>();
            m_SpacingList = new SortedDictionary<string, List<ObjectId>>();
            m_NoteList = new SortedDictionary<string, List<ObjectId>>();
            m_MultiplierList = new SortedDictionary<int, List<ObjectId>>();
            m_ShapeList = new SortedDictionary<ObjectId, List<ObjectId>>();

            m_FindShape = ObjectId.Null;
            m_ReplaceShape = ObjectId.Null;
            m_FindFields = 0;
            m_ReplaceFields = 0;

            psvFind.BackColor = DWGUtility.ModelBackgroundColor();
            psvReplace.BackColor = DWGUtility.ModelBackgroundColor();

            init = false;
        }

        public bool Init(ObjectId[] items)
        {
            init = true;

            m_Selection = items;

            UpdateUI();
            ReadSelection();

            init = false;
            return true;
        }

        private void ReadSelection()
        {
            m_PosList = new SortedDictionary<string, List<ObjectId>>();
            m_CountList = new SortedDictionary<string, List<ObjectId>>();
            m_DiameterList = new SortedDictionary<string, List<ObjectId>>();
            m_SpacingList = new SortedDictionary<string, List<ObjectId>>();
            m_NoteList = new SortedDictionary<string, List<ObjectId>>();
            m_MultiplierList = new SortedDictionary<int, List<ObjectId>>();
            m_ShapeList = new SortedDictionary<ObjectId, List<ObjectId>>();

            cbFindPosNumber.Items.Clear();
            cbFindCount.Items.Clear();
            cbFindDiameter.Items.Clear();
            cbFindSpacing.Items.Clear();
            cbFindNote.Items.Clear();
            cbFindMultiplier.Items.Clear();

            if (m_Selection == null || m_Selection.Length == 0)
            {
                return;
            }

            // Read all pos properties in selection
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId id in m_Selection)
                    {
                        RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                        if (pos != null)
                        {
                            List<ObjectId> list = null;
                            if (m_PosList.TryGetValue(pos.Pos, out list))
                                list.Add(id);
                            else
                                m_PosList.Add(pos.Pos, new List<ObjectId>() { id });
                            if (m_CountList.TryGetValue(pos.Count, out list))
                                list.Add(id);
                            else
                                m_CountList.Add(pos.Count, new List<ObjectId>() { id });
                            if (m_DiameterList.TryGetValue(pos.Diameter, out list))
                                list.Add(id);
                            else
                                m_DiameterList.Add(pos.Diameter, new List<ObjectId>() { id });
                            if (m_SpacingList.TryGetValue(pos.Spacing, out list))
                                list.Add(id);
                            else
                                m_SpacingList.Add(pos.Spacing, new List<ObjectId>() { id });
                            if (m_NoteList.TryGetValue(pos.Note, out list))
                                list.Add(id);
                            else
                                m_NoteList.Add(pos.Note, new List<ObjectId>() { id });
                            if (m_MultiplierList.TryGetValue(pos.Multiplier, out list))
                                list.Add(id);
                            else
                                m_MultiplierList.Add(pos.Multiplier, new List<ObjectId>() { id });
                            if (m_ShapeList.TryGetValue(pos.ShapeId, out list))
                                list.Add(id);
                            else
                                m_ShapeList.Add(pos.ShapeId, new List<ObjectId>() { id });
                        }
                    }

                }
                catch (System.Exception)
                {
                    ;
                }
            }

            // Populate list boxes
            foreach (string name in m_PosList.Keys)
                cbFindPosNumber.Items.Add(name);
            foreach (string name in m_CountList.Keys)
                cbFindCount.Items.Add(name);
            foreach (string name in m_DiameterList.Keys)
                cbFindDiameter.Items.Add(name);
            foreach (string name in m_SpacingList.Keys)
                cbFindSpacing.Items.Add(name);
            foreach (string name in m_NoteList.Keys)
                cbFindNote.Items.Add(name);
            foreach (int mult in m_MultiplierList.Keys)
                cbFindMultiplier.Items.Add(mult.ToString());

            cbReplaceDiameter.Items.Clear();

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(PosGroup.GroupId, OpenMode.ForRead) as PosGroup;
                    if (group != null)
                    {
                        string stdd = group.StandardDiameters;
                        if (!string.IsNullOrEmpty(stdd))
                        {
                            foreach (string ds in stdd.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                int d;
                                if (int.TryParse(ds, out d))
                                {
                                    cbReplaceDiameter.Items.Add(d.ToString());
                                }
                            }
                        }
                    }

                }
                catch (System.Exception)
                {
                    ;
                }
            }

            if (cbReplaceDiameter.Items.Count > 0) cbReplaceDiameter.SelectedIndex = 0;

            if (m_ShapeList.Count > 0)
            {
                foreach (ObjectId id in m_ShapeList.Keys)
                {
                    SetFindShape(id);
                    SetReplaceShape(id);
                    break;
                }
            }

            if (cbFindPosNumber.Items.Count > 0) cbFindPosNumber.SelectedIndex = 0;
            if (cbFindCount.Items.Count > 0) cbFindCount.SelectedIndex = 0;
            if (cbFindDiameter.Items.Count > 0) cbFindDiameter.SelectedIndex = 0;
            if (cbFindSpacing.Items.Count > 0) cbFindSpacing.SelectedIndex = 0;
            if (cbFindNote.Items.Count > 0) cbFindNote.SelectedIndex = 0;
            if (cbFindMultiplier.Items.Count > 0) cbFindMultiplier.SelectedIndex = 0;
        }

        private void SetFindShape(ObjectId id)
        {
            psvFind.Reset();

            m_FindShape = id;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosShape shape = tr.GetObject(id, OpenMode.ForRead) as PosShape;
                    if (shape == null)
                        return;

                    m_FindFields = shape.Fields;

                    for (int i = 0; i < shape.Items.Count; i++)
                    {
                        PosShape.Shape sh = shape.Items[i];
                        Color color = sh.Color.ColorValue;
                        if (sh is PosShape.ShapeLine)
                        {
                            PosShape.ShapeLine line = sh as PosShape.ShapeLine;
                            psvFind.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2, line.Visible);
                        }
                        else if (sh is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                            psvFind.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)(arc.StartAngle * 180.0 / Math.PI), (float)(arc.EndAngle * 180.0 / Math.PI), arc.Visible);
                        }
                        else if (sh is PosShape.ShapeText)
                        {
                            PosShape.ShapeText text = sh as PosShape.ShapeText;
                            StringAlignment horizontal = StringAlignment.Near;
                            StringAlignment vertical = StringAlignment.Near;
                            switch (text.HorizontalAlignment)
                            {
                                case TextHorizontalMode.TextCenter:
                                    horizontal = StringAlignment.Center;
                                    break;
                                case TextHorizontalMode.TextRight:
                                    horizontal = StringAlignment.Far;
                                    break;
                            }
                            switch (text.VerticalAlignment)
                            {
                                case TextVerticalMode.TextVerticalMid:
                                    vertical = StringAlignment.Center;
                                    break;
                                case TextVerticalMode.TextTop:
                                    vertical = StringAlignment.Far;
                                    break;
                            }
                            psvFind.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text, horizontal, vertical, text.Visible);
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
            UpdateUI();
        }

        private void SetReplaceShape(ObjectId id)
        {
            psvReplace.Reset();

            m_ReplaceShape = id;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosShape shape = tr.GetObject(id, OpenMode.ForRead) as PosShape;
                    if (shape == null)
                        return;

                    m_ReplaceFields = shape.Fields;

                    for (int i = 0; i < shape.Items.Count; i++)
                    {
                        PosShape.Shape sh = shape.Items[i];
                        Color color = sh.Color.ColorValue;
                        if (sh is PosShape.ShapeLine)
                        {
                            PosShape.ShapeLine line = sh as PosShape.ShapeLine;
                            psvReplace.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2, line.Visible);
                        }
                        else if (sh is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                            psvReplace.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)(arc.StartAngle * 180.0 / Math.PI), (float)(arc.EndAngle * 180.0 / Math.PI), arc.Visible);
                        }
                        else if (sh is PosShape.ShapeText)
                        {
                            PosShape.ShapeText text = sh as PosShape.ShapeText;
                            StringAlignment horizontal = StringAlignment.Near;
                            StringAlignment vertical = StringAlignment.Near;
                            switch (text.HorizontalAlignment)
                            {
                                case TextHorizontalMode.TextCenter:
                                    horizontal = StringAlignment.Center;
                                    break;
                                case TextHorizontalMode.TextRight:
                                    horizontal = StringAlignment.Far;
                                    break;
                            }
                            switch (text.VerticalAlignment)
                            {
                                case TextVerticalMode.TextVerticalMid:
                                    vertical = StringAlignment.Center;
                                    break;
                                case TextVerticalMode.TextTop:
                                    vertical = StringAlignment.Far;
                                    break;
                            }
                            psvReplace.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text, horizontal, vertical, text.Visible);
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (m_Selection == null || m_Selection.Length == 0)
            {
                gbFind.Enabled = false;
                gbReplace.Enabled = false;
                btnFind.Enabled = false;
                btnReplace.Enabled = false;
            }
            else
            {
                gbFind.Enabled = true;
                gbReplace.Enabled = true;
                btnFind.Enabled = true;
                btnReplace.Enabled = true;
            }

            cbFindPosNumber.Enabled = rbFindPosNumber.Checked;
            cbFindCount.Enabled = rbFindCount.Checked;
            cbFindDiameter.Enabled = rbFindDiameter.Checked;
            cbFindSpacing.Enabled = rbFindSpacing.Checked;
            cbFindNote.Enabled = rbFindNote.Checked;
            cbFindMultiplier.Enabled = rbFindMultiplier.Checked;
            psvFind.Enabled = rbFindShape.Checked;

            txtFindA.Enabled = rbFindShape.Checked && (m_FindFields >= 1);
            txtFindB.Enabled = rbFindShape.Checked && (m_FindFields >= 2);
            txtFindC.Enabled = rbFindShape.Checked && (m_FindFields >= 3);
            txtFindD.Enabled = rbFindShape.Checked && (m_FindFields >= 4);
            txtFindE.Enabled = rbFindShape.Checked && (m_FindFields >= 5);
            txtFindF.Enabled = rbFindShape.Checked && (m_FindFields >= 6);
            if (!txtFindA.Enabled) txtFindA.Text = "";
            if (!txtFindB.Enabled) txtFindB.Text = "";
            if (!txtFindC.Enabled) txtFindC.Text = "";
            if (!txtFindD.Enabled) txtFindD.Text = "";
            if (!txtFindE.Enabled) txtFindE.Text = "";
            if (!txtFindF.Enabled) txtFindF.Text = "";

            txtReplaceCount.Enabled = rbReplaceCount.Checked;
            cbReplaceDiameter.Enabled = rbReplaceDiameter.Checked;
            txtReplaceSpacing.Enabled = rbReplaceSpacing.Checked;
            txtReplaceNote.Enabled = rbReplaceNote.Checked;
            txtReplaceMultiplier.Enabled = rbReplaceMultiplier.Checked;
            psvReplace.Enabled = rbReplaceShape.Checked;

            txtReplaceA.Enabled = rbReplaceShape.Checked && (m_ReplaceFields >= 1);
            txtReplaceB.Enabled = rbReplaceShape.Checked && (m_ReplaceFields >= 2);
            txtReplaceC.Enabled = rbReplaceShape.Checked && (m_ReplaceFields >= 3);
            txtReplaceD.Enabled = rbReplaceShape.Checked && (m_ReplaceFields >= 4);
            txtReplaceE.Enabled = rbReplaceShape.Checked && (m_ReplaceFields >= 5);
            txtReplaceF.Enabled = rbReplaceShape.Checked && (m_ReplaceFields >= 6);
            if (!txtReplaceA.Enabled) txtReplaceA.Text = "";
            if (!txtReplaceB.Enabled) txtReplaceB.Text = "";
            if (!txtReplaceC.Enabled) txtReplaceC.Text = "";
            if (!txtReplaceD.Enabled) txtReplaceD.Text = "";
            if (!txtReplaceE.Enabled) txtReplaceE.Text = "";
            if (!txtReplaceF.Enabled) txtReplaceF.Text = "";
        }

        private void rbFindOptions_CheckedChanged(object sender, EventArgs e)
        {
            if (init) return;
            UpdateUI();
        }

        private void psvFind_Click(object sender, EventArgs e)
        {
            SelectShapeForm form = new SelectShapeForm();
            form.SetShapes(m_FindShape, m_ShapeList.Keys);
            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
            {
                SetFindShape(form.Current);
            }
        }

        private void psvReplace_Click(object sender, EventArgs e)
        {
            SelectShapeForm form = new SelectShapeForm();
            form.SetShapes(m_ReplaceShape);
            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
            {
                SetReplaceShape(form.Current);
            }
        }

        private bool CheckPosCount()
        {
            string str = txtReplaceCount.Text;
            str = str.Replace('x', '*');
            str = str.Replace('X', '*');

            if (string.IsNullOrEmpty(str) || Utility.ValidateFormula(str))
            {
                errorProvider.SetError(txtReplaceCount, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtReplaceCount, "Poz adedi yalnız rakam ve aritmetik işlemler içerebilir.");
                errorProvider.SetIconAlignment(txtReplaceCount, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosSpacing()
        {
            if (txtReplaceSpacing.IsValid)
            {
                errorProvider.SetError(txtReplaceSpacing, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtReplaceSpacing, "Poz aralığı yalnız rakam ve aralık işareti (~ veya -) içerebilir.");
                errorProvider.SetIconAlignment(txtReplaceSpacing, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosMultiplier()
        {
            int mult = 0;
            if (string.IsNullOrEmpty(txtReplaceMultiplier.Text) || int.TryParse(txtReplaceMultiplier.Text, out mult))
            {
                errorProvider.SetError(txtReplaceMultiplier, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtReplaceMultiplier, "Poz çarpanı tam sayı olmalıdır.");
                errorProvider.SetIconAlignment(txtReplaceMultiplier, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }

        private bool CheckPosLength(TextBox source)
        {
            bool haserror = false;

            // Split var lengths
            if (!string.IsNullOrEmpty(source.Text))
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
                        haserror = true;
                        break;
                    }
                    else if (!Utility.ValidateFormula(oldstr))
                    {
                        haserror = true;
                        break;
                    }
                }
            }

            if (haserror)
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

        private void txtReplaceCount_Validating(object sender, CancelEventArgs e)
        {
            CheckPosCount();
        }

        private void txtReplaceSpacing_Validating(object sender, CancelEventArgs e)
        {
            CheckPosSpacing();
        }

        private void txtReplaceMultiplier_Validating(object sender, CancelEventArgs e)
        {
            CheckPosMultiplier();
        }

        private void txtFindLength_Validating(object sender, CancelEventArgs e)
        {
            CheckPosLength((TextBox)sender);
        }

        private void txtReplaceLength_Validating(object sender, CancelEventArgs e)
        {
            CheckPosLength((TextBox)sender);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (m_Selection == null || m_Selection.Length == 0)
            {
                return;
            }

            bool haserror = false;
            if (rbFindShape.Checked)
            {
                if (m_FindFields >= 1)
                    if (!CheckPosLength(txtFindA)) haserror = true;
                if (m_FindFields >= 2)
                    if (!CheckPosLength(txtFindB)) haserror = true;
                if (m_FindFields >= 3)
                    if (!CheckPosLength(txtFindC)) haserror = true;
                if (m_FindFields >= 4)
                    if (!CheckPosLength(txtFindD)) haserror = true;
                if (m_FindFields >= 5)
                    if (!CheckPosLength(txtFindE)) haserror = true;
                if (m_FindFields >= 6)
                    if (!CheckPosLength(txtFindF)) haserror = true;
            }

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
                    // Filter selection
                    List<ObjectId> list = new List<ObjectId>();
                    foreach (ObjectId id in m_Selection)
                    {
                        RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                        if (pos == null) continue;

                        if (rbFindPosNumber.Checked && cbFindPosNumber.SelectedIndex != -1 && (string)cbFindPosNumber.SelectedItem != pos.Pos)
                            continue;
                        if (rbFindCount.Checked && cbFindCount.SelectedIndex != -1 && (string)cbFindCount.SelectedItem != pos.Count)
                            continue;
                        if (rbFindDiameter.Checked && cbFindDiameter.SelectedIndex != -1 && (string)cbFindDiameter.SelectedItem != pos.Diameter)
                            continue;
                        if (rbFindSpacing.Checked && cbFindSpacing.SelectedIndex != -1 && (string)cbFindSpacing.SelectedItem != pos.Spacing)
                            continue;
                        if (rbFindNote.Checked && cbFindNote.SelectedIndex != -1 && (string)cbFindNote.SelectedItem != pos.Note)
                            continue;
                        if (rbFindMultiplier.Checked && cbFindMultiplier.SelectedIndex != -1 && (string)cbFindMultiplier.SelectedItem != pos.Multiplier.ToString())
                            continue;
                        if (rbFindShape.Checked && m_FindShape != pos.ShapeId)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindA.Text) && txtFindA.Text != pos.A)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindB.Text) && txtFindA.Text != pos.B)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindC.Text) && txtFindA.Text != pos.C)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindD.Text) && txtFindA.Text != pos.D)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindE.Text) && txtFindA.Text != pos.E)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindF.Text) && txtFindA.Text != pos.F)
                            continue;

                        list.Add(id);
                    }

                    // Select
                    Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(list.ToArray());

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (m_Selection == null || m_Selection.Length == 0)
            {
                return;
            }

            bool haserror = false;
            if (rbReplaceCount.Checked && !CheckPosCount()) haserror = true;
            if (rbReplaceSpacing.Checked && !CheckPosSpacing()) haserror = true;
            if (rbReplaceMultiplier.Checked && !CheckPosMultiplier()) haserror = true;
            if (rbFindShape.Checked)
            {
                if (m_FindFields >= 1)
                    if (!CheckPosLength(txtFindA)) haserror = true;
                if (m_FindFields >= 2)
                    if (!CheckPosLength(txtFindB)) haserror = true;
                if (m_FindFields >= 3)
                    if (!CheckPosLength(txtFindC)) haserror = true;
                if (m_FindFields >= 4)
                    if (!CheckPosLength(txtFindD)) haserror = true;
                if (m_FindFields >= 5)
                    if (!CheckPosLength(txtFindE)) haserror = true;
                if (m_FindFields >= 6)
                    if (!CheckPosLength(txtFindF)) haserror = true;
            }
            if (rbReplaceShape.Checked)
            {
                if (m_ReplaceFields >= 1)
                    if (!CheckPosLength(txtReplaceA)) haserror = true;
                if (m_ReplaceFields >= 2)
                    if (!CheckPosLength(txtReplaceB)) haserror = true;
                if (m_ReplaceFields >= 3)
                    if (!CheckPosLength(txtReplaceC)) haserror = true;
                if (m_ReplaceFields >= 4)
                    if (!CheckPosLength(txtReplaceD)) haserror = true;
                if (m_ReplaceFields >= 5)
                    if (!CheckPosLength(txtReplaceE)) haserror = true;
                if (m_ReplaceFields >= 6)
                    if (!CheckPosLength(txtReplaceF)) haserror = true;
            }

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
                    // Filter selection
                    List<ObjectId> list = new List<ObjectId>();
                    foreach (ObjectId id in m_Selection)
                    {
                        RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                        if (pos == null) continue;

                        if (rbFindPosNumber.Checked && cbFindPosNumber.SelectedIndex != -1 && (string)cbFindPosNumber.SelectedItem != pos.Pos)
                            continue;
                        if (rbFindCount.Checked && cbFindCount.SelectedIndex != -1 && (string)cbFindCount.SelectedItem != pos.Count)
                            continue;
                        if (rbFindDiameter.Checked && cbFindDiameter.SelectedIndex != -1 && (string)cbFindDiameter.SelectedItem != pos.Diameter)
                            continue;
                        if (rbFindSpacing.Checked && cbFindSpacing.SelectedIndex != -1 && (string)cbFindSpacing.SelectedItem != pos.Spacing)
                            continue;
                        if (rbFindNote.Checked && cbFindNote.SelectedIndex != -1 && (string)cbFindNote.SelectedItem != pos.Note)
                            continue;
                        if (rbFindMultiplier.Checked && cbFindMultiplier.SelectedIndex != -1 && (string)cbFindMultiplier.SelectedItem != pos.Multiplier.ToString())
                            continue;
                        if (rbFindShape.Checked && m_FindShape != pos.ShapeId)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindA.Text) && txtFindA.Text != pos.A)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindB.Text) && txtFindA.Text != pos.B)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindC.Text) && txtFindA.Text != pos.C)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindD.Text) && txtFindA.Text != pos.D)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindE.Text) && txtFindA.Text != pos.E)
                            continue;
                        if (rbFindShape.Checked && !string.IsNullOrEmpty(txtFindF.Text) && txtFindA.Text != pos.F)
                            continue;

                        list.Add(id);
                    }

                    // Apply changes
                    foreach (ObjectId id in list)
                    {
                        RebarPos pos = tr.GetObject(id, OpenMode.ForWrite) as RebarPos;
                        if (pos == null) continue;

                        if (rbReplaceCount.Checked) pos.Count = txtReplaceCount.Text;
                        if (rbReplaceDiameter.Checked) pos.Diameter = (string)cbReplaceDiameter.SelectedItem;
                        if (rbReplaceSpacing.Checked) pos.Spacing = txtReplaceSpacing.Text;
                        if (rbReplaceNote.Checked) pos.Note = txtReplaceNote.Text;
                        if (rbReplaceMultiplier.Checked) pos.Multiplier = int.Parse(txtReplaceMultiplier.Text);
                        if (rbReplaceShape.Checked && !m_ReplaceShape.IsNull) pos.ShapeId = m_ReplaceShape;
                        if (rbReplaceShape.Checked && !string.IsNullOrEmpty(txtReplaceA.Text)) pos.A = txtReplaceA.Text;
                        if (rbReplaceShape.Checked && !string.IsNullOrEmpty(txtReplaceB.Text)) pos.B = txtReplaceB.Text;
                        if (rbReplaceShape.Checked && !string.IsNullOrEmpty(txtReplaceC.Text)) pos.C = txtReplaceC.Text;
                        if (rbReplaceShape.Checked && !string.IsNullOrEmpty(txtReplaceD.Text)) pos.D = txtReplaceD.Text;
                        if (rbReplaceShape.Checked && !string.IsNullOrEmpty(txtReplaceE.Text)) pos.E = txtReplaceE.Text;
                        if (rbReplaceShape.Checked && !string.IsNullOrEmpty(txtReplaceF.Text)) pos.F = txtReplaceF.Text;
                    }

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
    }
}
