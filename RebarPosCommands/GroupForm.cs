using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;

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
            public ObjectId styleId;
        }

        List<GroupCopy> m_Copies;
        Dictionary<string, ObjectId> m_Groups;
        Dictionary<string, ObjectId> m_Styles;

        public ObjectId CurrentId { get; private set; }
        public string CurrentName { get; private set; }

        public GroupForm()
        {
            InitializeComponent();

            CurrentId = ObjectId.Null;
            m_Copies = new List<GroupCopy>();
            m_Groups = new Dictionary<string, ObjectId>();
            m_Styles = new Dictionary<string, ObjectId>();
        }

        public bool Init(ObjectId currentId)
        {
            m_Groups = DWGUtility.GetGroups();
            m_Styles = DWGUtility.GetStyles();

            if (m_Groups.Count == 0 || m_Styles.Count == 0)
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
                        copy.styleId = group.StyleId;

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

            foreach (string name in m_Styles.Keys)
            {
                cbStyle.Items.Add(name);
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

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> item in m_Styles)
            {
                if (item.Value == copy.styleId)
                {
                    cbStyle.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
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
            copy.drawingUnits = PosGroup.DrawingUnits.Millimeter;
            copy.displayUnits = PosGroup.DrawingUnits.Millimeter;
            copy.precision = 0;
            copy.maxLength = 12.0;
            copy.bending = false;
            copy.styleId = DWGUtility.CreateDefaultStyles();

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
                return;
            }
            else
            {
                btnRemove.Enabled = true;
                btnRename.Enabled = true;
                gbOptions.Enabled = true;
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
        }

        private void udPrecision_ValueChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.precision = (int)udPrecision.Value;
        }

        private void txtMaxLength_TextChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            double length;
            if (double.TryParse(txtMaxLength.Text, out length))
            {
                copy.maxLength = length;
            }
        }

        private void chkBending_CheckedChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            copy.bending = chkBending.Checked;
        }

        private void cbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupCopy copy = GetSelected();
            if (copy == null) return;
            ObjectId id;
            if (m_Styles.TryGetValue((string)cbStyle.SelectedItem, out id))
            {
                copy.styleId = id;
            }
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
                            group.StyleId = copy.styleId;
                            copy.id = dict.SetAt("*", group);
                            tr.AddNewlyCreatedDBObject(group, true);
                        }
                        else
                        {
                            PosGroup group= (PosGroup)tr.GetObject(copy.id, OpenMode.ForRead);
                            if (group.Name != copy.name)
                            {
                                group.UpgradeOpen();
                                group.Name = copy.name;
                                group.DowngradeOpen();
                            }
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
                                    CurrentName = it.Key;
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
    }
}
