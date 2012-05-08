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
        ObjectId m_GroupId;
        SortedDictionary<string, ObjectId> m_Groups;
        ObjectId[] m_Selection;

        SortedDictionary<string, List<ObjectId>> m_PosList;
        SortedDictionary<string, List<ObjectId>> m_CountList;
        SortedDictionary<string, List<ObjectId>> m_DiameterList;
        SortedDictionary<string, List<ObjectId>> m_SpacingList;
        SortedDictionary<string, List<ObjectId>> m_NoteList;
        SortedDictionary<int, List<ObjectId>> m_MultiplierList;
        SortedDictionary<ObjectId, List<ObjectId>> m_GroupList;
        SortedDictionary<ObjectId, List<ObjectId>> m_ShapeList;

        ObjectId m_FindShape;
        ObjectId m_ReplaceShape;
        int m_FindFields;
        int m_ReplaceFields;

        bool init;

        public FindReplaceForm()
        {
            InitializeComponent();

            m_GroupId = ObjectId.Null;
            m_Groups = new SortedDictionary<string, ObjectId>();
            m_Selection = new ObjectId[0];

            m_PosList = new SortedDictionary<string, List<ObjectId>>();
            m_CountList = new SortedDictionary<string, List<ObjectId>>();
            m_DiameterList = new SortedDictionary<string, List<ObjectId>>();
            m_SpacingList = new SortedDictionary<string, List<ObjectId>>();
            m_NoteList = new SortedDictionary<string, List<ObjectId>>();
            m_MultiplierList = new SortedDictionary<int, List<ObjectId>>();
            m_GroupList = new SortedDictionary<ObjectId, List<ObjectId>>();
            m_ShapeList = new SortedDictionary<ObjectId, List<ObjectId>>();

            m_FindShape = ObjectId.Null;
            m_ReplaceShape = ObjectId.Null;
            m_FindFields = 0;
            m_ReplaceFields = 0;

            AcadPreferences pref = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            uint indexColor = pref.Display.GraphicsWinModelBackgrndColor;
            psvFind.BackColor = ColorTranslator.FromOle((int)indexColor);
            psvReplace.BackColor = ColorTranslator.FromOle((int)indexColor);

            init = false;
        }

        public bool Init(ObjectId currentGroup)
        {
            init = true;
            m_GroupId = currentGroup;
            m_Groups = new SortedDictionary<string, ObjectId>(DWGUtility.GetGroups());

            if (m_Groups.Count == 0)
            {
                init = false;
                return false;
            }
            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in m_Groups)
            {
                cbSelectGroup.Items.Add(item.Key);
                if (item.Value == m_GroupId) cbSelectGroup.SelectedIndex = i;
                i++;
            }

            // Get implied selection
            PromptSelectionResult res = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SelectImplied();
            if (res.Status == PromptStatus.OK)
            {
                List<ObjectId> idlist = new List<ObjectId>();
                foreach (SelectedObject sel in res.Value)
                {
                    ObjectId id = sel.ObjectId;
                    if (id.ObjectClass == Autodesk.AutoCAD.Runtime.RXObject.GetClass(typeof(RebarPos)))
                        idlist.Add(id);
                }
                m_Selection = idlist.ToArray();

                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(new ObjectId[0]);
            }

            if (m_Selection != null && m_Selection.Length != 0)
            {
                rbSelectUser.Checked = true;
            }
            else
            {
                m_Selection = DWGUtility.GetPosInGroup(m_GroupId);
                if (m_Selection != null && m_Selection.Length != 0)
                {
                    rbSelectGroup.Checked = true;
                }
                else
                {
                    m_Selection = DWGUtility.GetAllPos();
                    if (m_Selection != null && m_Selection.Length != 0)
                    {
                        rbSelectAll.Checked = true;
                    }
                    else
                    {
                        init = false;
                        return false;
                    }
                }
            }

            cbReplaceGroup.Items.Clear();
            foreach (string name in m_Groups.Keys)
            {
                cbReplaceGroup.Items.Add(name);
            }
            if (cbReplaceGroup.Items.Count > 0) cbReplaceGroup.SelectedIndex = 0;

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
            m_GroupList = new SortedDictionary<ObjectId, List<ObjectId>>();
            m_ShapeList = new SortedDictionary<ObjectId, List<ObjectId>>();

            cbFindPosNumber.Items.Clear();
            cbFindCount.Items.Clear();
            cbFindDiameter.Items.Clear();
            cbFindSpacing.Items.Clear();
            cbFindNote.Items.Clear();
            cbFindMultiplier.Items.Clear();
            cbFindGroup.Items.Clear();

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
                            if (m_GroupList.TryGetValue(pos.GroupId, out list))
                                list.Add(id);
                            else
                                m_GroupList.Add(pos.GroupId, new List<ObjectId>() { id });
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
                cbFindMultiplier.Items.Add(mult);
            foreach (ObjectId id in m_GroupList.Keys)
            {
                foreach (KeyValuePair<string, ObjectId> item in m_Groups)
                {
                    if (item.Value == id)
                    {
                        cbFindGroup.Items.Add(item.Key);
                        break;
                    }
                }
            }

            cbReplaceDiameter.Items.Clear();
            if (m_GroupList.Count == 1)
            {
                foreach (ObjectId id in m_GroupList.Keys)
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        try
                        {
                            PosGroup group = tr.GetObject(id, OpenMode.ForRead) as PosGroup;
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
                                            cbReplaceDiameter.Items.Add(d);
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
                    break;
                }
            }
            if (cbReplaceDiameter.Items.Count > 0) cbReplaceDiameter.SelectedIndex = 0;

            if (m_ShapeList.Count != 1)
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
            if (cbFindGroup.Items.Count > 0) cbFindGroup.SelectedIndex = 0;
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
                            psvFind.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2);
                        }
                        else if (sh is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                            psvFind.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)arc.StartAngle, (float)arc.EndAngle);
                        }
                        else if (sh is PosShape.ShapeText)
                        {
                            PosShape.ShapeText text = sh as PosShape.ShapeText;
                            psvFind.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text);
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
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
                            psvReplace.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2);
                        }
                        else if (sh is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                            psvReplace.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)arc.StartAngle, (float)arc.EndAngle);
                        }
                        else if (sh is PosShape.ShapeText)
                        {
                            PosShape.ShapeText text = sh as PosShape.ShapeText;
                            psvReplace.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text);
                        }
                    }
                }
                catch (System.Exception)
                {
                    ;
                }
            }
        }

        private void UpdateUI()
        {
            cbSelectGroup.Enabled = rbSelectGroup.Checked;
            btnSelectUser.Enabled = rbSelectUser.Checked;

            if (m_Selection == null || m_Selection.Length == 0)
            {
                gbFind.Enabled = false;
                gbReplace.Enabled = false;
                lblSelectStatus.Text = "";
                btnOK.Enabled = false;
            }
            else
            {
                gbFind.Enabled = true;
                gbReplace.Enabled = true;
                lblSelectStatus.Text = m_Selection.Length.ToString() + " adet poz seçildi.";
                btnOK.Enabled = true;
            }

            cbFindPosNumber.Enabled = rbFindPosNumber.Checked;
            cbFindCount.Enabled = rbFindCount.Checked;
            cbFindDiameter.Enabled = rbFindDiameter.Checked;
            cbFindSpacing.Enabled = rbFindSpacing.Checked;
            cbFindNote.Enabled = rbFindNote.Checked;
            cbFindMultiplier.Enabled = rbFindMultiplier.Checked;
            cbFindGroup.Enabled = rbFindGroup.Checked;
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
            cbReplaceGroup.Enabled = rbReplaceGroup.Checked;
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

        private void rbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (init) return;
            if (rbSelectAll.Checked)
            {
                m_Selection = DWGUtility.GetAllPos();
                ReadSelection();
            }
            UpdateUI();
        }

        private void rbSelectGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (init) return;
            if (rbSelectGroup.Checked)
            {
                m_Selection = DWGUtility.GetPosInGroup(m_GroupId);
                ReadSelection();
                UpdateUI();
            }
        }

        private void cbSelectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init) return;
            if (cbSelectGroup.SelectedIndex != -1)
            {
                m_Selection = null;
                string name = (string)cbSelectGroup.SelectedItem;
                if (m_Groups.TryGetValue(name, out m_GroupId))
                {
                    m_Selection = DWGUtility.GetPosInGroup(m_GroupId);
                    ReadSelection();
                }
                UpdateUI();
            }
        }

        private void rbSelectUser_CheckedChanged(object sender, EventArgs e)
        {
            if (init) return;
            if (rbSelectUser.Checked)
            {
                m_Selection = null;
                Hide();
                PromptSelectionResult res = DWGUtility.SelectAllPosUser();
                Show();
                if (res.Status == PromptStatus.OK)
                {
                    m_Selection = res.Value.GetObjectIds();
                    ReadSelection();
                }
                UpdateUI();
            }
        }

        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            if (init) return;
            m_Selection = null;
            Hide();
            PromptSelectionResult res = DWGUtility.SelectAllPosUser();
            Show();
            if (res.Status == PromptStatus.OK)
            {
                m_Selection = res.Value.GetObjectIds();
                ReadSelection();
            }
            UpdateUI();
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
    }
}
