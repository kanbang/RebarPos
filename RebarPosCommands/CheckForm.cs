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

        public bool Init(ObjectId groupId)
        {
            lblGroup.Text = DWGUtility.GetGroupName(groupId);

            ReadPos(groupId);
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

        private void ReadPos(ObjectId groupId)
        {
            try
            {
                m_PosList = PosCheckResult.CheckAllInGroup(groupId, PosCheckResult.CheckType.Errors);
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
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (lbItems.SelectedItems.Count == 0) return;

            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PosCheckResult result = lbItems.SelectedItems[0].Tag as PosCheckResult;
            List<ObjectId> list = new List<ObjectId>();
            foreach (KeyValuePair<PosCheckResult.CheckResult, List<ObjectId>> res in result.results)
            {
                list.AddRange(res.Value);
            }

            Extents3d outerext = new Extents3d();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId id in list)
                    {
                        Entity ent = tr.GetObject(id, OpenMode.ForRead) as Entity;
                        Extents3d ext = ent.GeometricExtents;
                        outerext.AddExtents(ext);
                    }
                }
                catch
                {
                    ;
                }
            }

            outerext.TransformBy(ed.CurrentUserCoordinateSystem.Inverse());
            Point2d min2d = new Point2d(outerext.MinPoint.X, outerext.MinPoint.Y);
            Point2d max2d = new Point2d(outerext.MaxPoint.X, outerext.MaxPoint.Y);

            ViewTableRecord view = new ViewTableRecord();

            view.CenterPoint = min2d + ((max2d - min2d) / 2.0);
            view.Height = max2d.Y - min2d.Y;
            view.Width = max2d.X - min2d.X;
            
            ed.SetCurrentView(view);
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
    }
}
