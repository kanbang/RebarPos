using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

namespace RebarPosCommands
{
    public partial class DrawBOQForm : Form
    {
        List<PosCopy> m_PosList;
        Dictionary<string, ObjectId> m_Groups;
        Dictionary<string, ObjectId> m_Styles;
        ObjectId m_CurrentGroup;
        ObjectId m_CurrentStyle;

        public List<PosCopy> PosList { get { return m_PosList; } }
        public ObjectId CurrentGroup { get { return m_CurrentGroup; } }
        public ObjectId TableStyle { get { return m_CurrentStyle; } }
        public string TableHeader { get { return txtHeader.Text; } }
        public string TableFooter { get { return txtFooter.Text; } }
        public int Multiplier { get { return (int)udMultiplier.Value; } }
        public bool HideMissing { get { return chkHideMissing.Checked; } }

        public DrawBOQForm()
        {
            InitializeComponent();

            m_PosList = new List<PosCopy>();
            m_Groups = new Dictionary<string, ObjectId>();
            m_Styles = new Dictionary<string, ObjectId>();
        }

        public bool Init(ObjectId groupId)
        {
            m_Groups = DWGUtility.GetGroups();
            m_Styles = DWGUtility.GetTableStyles();

            if (m_Groups.Count == 0)
            {
                return false;
            }
            if (m_Styles.Count == 0)
            {
                return false;
            }

            int i = 0;
            foreach (KeyValuePair<string, ObjectId> pair in m_Groups)
            {
                cbGroup.Items.Add(pair.Key);
                if (pair.Value == groupId) cbGroup.SelectedIndex = i;
                i++;
            }
            i = 0;
            foreach (KeyValuePair<string, ObjectId> pair in m_Styles)
            {
                if (i == 0) m_CurrentStyle = pair.Value;
                cbStyle.Items.Add(pair.Key);
                i++;
            }
            cbStyle.SelectedIndex = 0;


            ReadPos(groupId);
            return true;
        }

        private void ReadPos(ObjectId groupId)
        {
            try
            {
                m_CurrentGroup = groupId;
                m_PosList = PosCopy.ReadAllInGroup(groupId, PosCopy.PosGrouping.PosMarker);
                SortList();
                RemoveEmpty();
                if (chkHideMissing.Checked)
                {
                    AddMissing();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddMissing()
        {
            RemoveEmpty();

            int lastpos = 0;
            foreach (PosCopy copy in m_PosList)
            {
                int posno;
                if (int.TryParse(copy.newpos, out posno))
                {
                    lastpos = Math.Max(lastpos, posno);
                }
            }
            for (int i = 1; i <= lastpos; i++)
            {
                if (!m_PosList.Exists(p => p.newpos == i.ToString()))
                {
                    PosCopy copy = new PosCopy();
                    copy.pos = i.ToString();
                    m_PosList.Add(copy);
                }
            }
        }

        private void RemoveEmpty()
        {
            m_PosList.RemoveAll(p => p.existing == false);
        }

        private void SortList()
        {
            m_PosList.Sort(new CompareByPosNumber());
        }

        private class CompareByPosNumber : IComparer<PosCopy>
        {
            public int Compare(PosCopy e1, PosCopy e2)
            {
                int p1 = 0;
                int p2 = 0;
                int.TryParse(e1.pos, out p1);
                int.TryParse(e2.pos, out p2);

                return (p1 == p2 ? 0 : (p1 < p2 ? -1 : 1));
            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            foreach (ObjectId id in m_Groups.Values)
            {
                if (i == cbGroup.SelectedIndex)
                {
                    ReadPos(id);
                    break;
                }
                i++;
            }
        }

        private void cbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            foreach (ObjectId id in m_Styles.Values)
            {
                if (i == cbStyle.SelectedIndex)
                {
                    m_CurrentStyle = id;
                    break;
                }
                i++;
            }
        }

        private void chkHideMissing_CheckedChanged(object sender, EventArgs e)
        {
            RemoveEmpty();
            if (chkHideMissing.Checked)
            {
                AddMissing();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
