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
    public partial class NumberingForm : Form
    {
        private class PosCopy
        {
            public List<ObjectId> list;
            public string key;
            public string pos;
            public int priority;
            public string diameter;
            public string length;
            public string a;
            public string b;
            public string c;
            public string d;
            public string e;
            public string f;
            public ObjectId shapeId;
            public string shapename;
            public double x;
            public double y;

            public PosCopy()
            {
                list = new List<ObjectId>();
                x = double.MaxValue;
                y = double.MaxValue;
            }
        }

        Dictionary<string, PosCopy> m_PosList;
        Dictionary<string, ObjectId> m_Groups;

        public NumberingForm()
        {
            InitializeComponent();

            m_PosList = new Dictionary<string, PosCopy>();
            m_Groups = new Dictionary<string, ObjectId>();
        }

        public bool Init(ObjectId groupId)
        {
            m_Groups = DWGUtility.GetGroups();
            if (m_Groups.Count == 0)
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

            cbOrder1.SelectedIndex = 0;
            cbOrder2.SelectedIndex = 1;
            cbOrder3.SelectedIndex = 2;
            cbOrder4.SelectedIndex = 3;
            cbOrder5.SelectedIndex = 4;

            ReadPos(groupId);
            if (m_PosList.Count == 0)
            {
                return false;
            }
            foreach (PosCopy copy in m_PosList.Values)
            {
                ListViewItem item = new ListViewItem(copy.pos);
                item.SubItems.Add(copy.pos);
                item.SubItems.Add(copy.priority.ToString());
                item.SubItems.Add(copy.diameter);
                item.SubItems.Add(copy.shapename);
                item.SubItems.Add(copy.length);
                item.SubItems.Add(copy.a);
                item.SubItems.Add(copy.b);
                item.SubItems.Add(copy.c);
                item.SubItems.Add(copy.d);
                item.SubItems.Add(copy.e);
                item.SubItems.Add(copy.f);
                lbItems.Items.Add(item);
            }
            return true;
        }

        public void ReadPos(ObjectId groupId)
        {
            m_PosList = new Dictionary<string, PosCopy>();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                    using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            RebarPos pos = tr.GetObject(it.Current, OpenMode.ForRead) as RebarPos;
                            if (pos != null && pos.GroupId == groupId)
                            {
                                PosCopy copy = null;
                                if (m_PosList.TryGetValue(pos.PosKey, out copy))
                                {
                                    copy.list.Add(it.Current);
                                    copy.x = Math.Min(copy.x, pos.BasePoint.X);
                                    copy.y = Math.Min(copy.y, pos.BasePoint.Y);
                                }
                                else
                                {
                                    copy = new PosCopy();
                                    copy.key = pos.PosKey;
                                    copy.list.Add(it.Current);
                                    copy.pos = pos.Pos;
                                    copy.diameter = pos.Diameter;
                                    copy.length = pos.Length;
                                    copy.a = pos.A;
                                    copy.b = pos.B;
                                    copy.c = pos.C;
                                    copy.d = pos.D;
                                    copy.e = pos.E;
                                    copy.f = pos.F;
                                    copy.x = pos.BasePoint.X;
                                    copy.y = pos.BasePoint.Y;
                                    copy.shapeId = pos.ShapeId;
                                    PosShape shape = tr.GetObject(copy.shapeId, OpenMode.ForRead) as PosShape;
                                    if (shape != null)
                                    {
                                        copy.priority = shape.Priority;
                                        copy.shapename = shape.Name;
                                    }
                                    m_PosList.Add(copy.key, copy);
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
