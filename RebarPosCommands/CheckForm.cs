using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.Geometry;

namespace RebarPosCommands
{
    public partial class CheckForm : Form
    {
        List<PosCheckResult> m_PosList;

        public CheckForm()
        {
            InitializeComponent();

            m_PosList = new List<PosCheckResult>();
        }

        public bool Init(ObjectId[] items)
        {
            ReadPos(items);
            PopulateList();

            return true;
        }

        private void PopulateList()
        {
            if (m_PosList.Count == 0)
            {
                lbItems.Enabled = false;
                return;
            }
            else
            {
                lbItems.Enabled = true;
            }

            lbItems.Items.Clear();
            foreach (PosCheckResult copy in m_PosList)
            {
                foreach (KeyValuePair<PosCheckResult.CheckResult, List<ObjectId>> res in copy.results)
                {
                    ListViewItem item = new ListViewItem(copy.pos);
                    item.Tag = copy;
                    string err = string.Empty;
                    switch (res.Key)
                    {
                        case PosCheckResult.CheckResult.DiameterError:
                            err = "Çap Hatası";
                            break;
                        case PosCheckResult.CheckResult.LengthError:
                            err = "Boy Hatası";
                            break;
                        case PosCheckResult.CheckResult.PieceLengthError:
                            err = "Parça Boyu Hatası";
                            break;
                        case PosCheckResult.CheckResult.SamePosKeyWarning:
                            err = "Poz Açılımı Aynı";
                            break;
                        case PosCheckResult.CheckResult.ShapeError:
                            err = "Şekil Hatası";
                            break;
                        case PosCheckResult.CheckResult.StandardDiameterError:
                            err = "Standart Olmayan Çap Hatası";
                            break;
                    }
                    item.SubItems.Add(err);
                    item.SubItems.Add(res.Value.Count.ToString());
                    lbItems.Items.Add(item);
                }
            }

            lbItems_SelectedIndexChanged(null, new EventArgs());
        }

        private void ReadPos(ObjectId[] items)
        {
            try
            {
                m_PosList = PosCheckResult.CheckAllInSelection(items, PosCheckResult.CheckType.Errors);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnZoom.Enabled = (lbItems.SelectedIndices.Count != 0);
            btnSelect.Enabled = (lbItems.SelectedIndices.Count != 0);
            btnSelectAll.Enabled = (lbItems.SelectedIndices.Count != 0);
            lbSubItems.Enabled = (lbItems.SelectedIndices.Count == 1);
            lblSelectHint.Visible = (lbItems.SelectedIndices.Count != 1);

            lbSubItems.Items.Clear();
            if (lbItems.SelectedIndices.Count == 1)
            {
                PosCheckResult result = lbItems.SelectedItems[0].Tag as PosCheckResult;
                List<ObjectId> list = new List<ObjectId>();
                foreach (KeyValuePair<PosCheckResult.CheckResult, List<ObjectId>> res in result.results)
                {
                    list.AddRange(res.Value);
                }
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        foreach (ObjectId id in list)
                        {
                            RebarPos pos = tr.GetObject(id, OpenMode.ForRead) as RebarPos;
                            if (pos != null)
                            {
                                ListViewItem subitem = new ListViewItem(pos.Pos);
                                subitem.Tag = id;
                                subitem.SubItems.Add(pos.Diameter);
                                subitem.SubItems.Add(pos.Shape);
                                subitem.SubItems.Add(pos.A);
                                subitem.SubItems.Add(pos.B);
                                subitem.SubItems.Add(pos.C);
                                subitem.SubItems.Add(pos.D);
                                subitem.SubItems.Add(pos.E);
                                subitem.SubItems.Add(pos.F);
                                subitem.SubItems.Add(pos.Length);
                                lbSubItems.Items.Add(subitem);
                            }
                        }
                    }
                    catch
                    {
                        ;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (lbItems.SelectedItems.Count == 0) return;

            PosCheckResult result = lbItems.SelectedItems[0].Tag as PosCheckResult;
            List<ObjectId> list = new List<ObjectId>();
            foreach (KeyValuePair<PosCheckResult.CheckResult, List<ObjectId>> res in result.results)
            {
                list.AddRange(res.Value);
            }
            DWGUtility.ZoomToObjects(list);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lbItems.SelectedItems.Count == 0) return;
            PosCheckResult result = lbItems.SelectedItems[0].Tag as PosCheckResult;
            List<ObjectId> list = new List<ObjectId>();
            foreach (KeyValuePair<PosCheckResult.CheckResult, List<ObjectId>> res in result.results)
            {
                list.AddRange(res.Value);
            }

            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(list.ToArray());
            Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (lbItems.SelectedItems.Count == 0) return;
            List<ObjectId> list = new List<ObjectId>();
            foreach (ListViewItem item in lbItems.SelectedItems)
            {
                PosCheckResult result = item.Tag as PosCheckResult;
                foreach (KeyValuePair<PosCheckResult.CheckResult, List<ObjectId>> res in result.results)
                {
                    list.AddRange(res.Value);
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(list.ToArray());
            Close();
        }

        private void lbSubItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnZoomSubItem.Enabled = (lbSubItems.SelectedIndices.Count != 0);
            btnSelectSubItem.Enabled = (lbSubItems.SelectedIndices.Count != 0);
            btnSelectAllSubItem.Enabled = (lbSubItems.SelectedIndices.Count != 0);
        }

        private void btnZoomSubItem_Click(object sender, EventArgs e)
        {
            if (lbSubItems.SelectedItems.Count == 0) return;

            List<ObjectId> list = new List<ObjectId>();
            foreach (ListViewItem item in lbSubItems.SelectedItems)
            {
                if (item.Tag != null)
                {
                    ObjectId id = (ObjectId)item.Tag;
                    list.Add(id);
                }
            }
            DWGUtility.ZoomToObjects(list);
        }

        private void btnSelectSubItem_Click(object sender, EventArgs e)
        {
            if (lbSubItems.SelectedItems.Count == 0) return;
            List<ObjectId> list = new List<ObjectId>();
            foreach (ListViewItem item in lbSubItems.SelectedItems)
            {
                if (item.Tag != null)
                {
                    ObjectId id = (ObjectId)item.Tag;
                    list.Add(id);
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(list.ToArray());
            Close();
        }

        private void btnSelectAllSubItem_Click(object sender, EventArgs e)
        {
            if (lbSubItems.Items.Count == 0) return;
            List<ObjectId> list = new List<ObjectId>();
            foreach (ListViewItem item in lbSubItems.Items)
            {
                if (item.Tag != null)
                {
                    ObjectId id = (ObjectId)item.Tag;
                    list.Add(id);
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(list.ToArray());
            Close();
        }

        private void lbItems_DoubleClick(object sender, EventArgs e)
        {
            btnZoom_Click(sender, e);
        }

        private void lbSubItems_DoubleClick(object sender, EventArgs e)
        {
            btnZoomSubItem_Click(sender, e);
        }
    }
}
