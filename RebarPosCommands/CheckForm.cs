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
        List<ObjectId> m_Items;
        List<PosCheckResult> m_PosList;

        public CheckForm()
        {
            InitializeComponent();

            m_Items = new List<ObjectId>();
            m_PosList = new List<PosCheckResult>();

            ListViewExtender extender = new ListViewExtender(lbItems);
            extender.ExtendAsImageButtonColumn(3, Properties.Resources.editlist, new Size(24, 24));
            extender.ExtendAsImageButtonColumn(4, Properties.Resources.zoomtolist, new Size(24, 24));
            extender.ExtendAsImageButtonColumn(5, Properties.Resources.selectlist, new Size(24, 24));
            extender.SetRowHeight(24);

            extender.CustomColumnClick += new ListViewExtender.CustomColumnClickEventHandler(extender_CustomColumnClick);
        }

        void extender_CustomColumnClick(object sender, CheckForm.ListViewExtender.CustomColumnClickEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                // Fix
                if (e.Item == null) return;
                PosCheckResult check = e.Item.Tag as PosCheckResult;
                if (check.Fix())
                {
                    ReadPos(m_Items);
                    PopulateList();
                }
            }
            else if (e.ColumnIndex == 4)
            {
                // Zoom
                if (e.Item == null) return;
                PosCheckResult check = e.Item.Tag as PosCheckResult;
                DWGUtility.ZoomToObjects(check.Items);
            }
            else if (e.ColumnIndex == 5)
            {
                // Select and close form
                if (e.Item == null) return;
                PosCheckResult check = e.Item.Tag as PosCheckResult;
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(check.Items.ToArray());
                Close();
            }
        }

        public bool Init(IEnumerable<ObjectId> items)
        {
            foreach (ObjectId id in items)
                m_Items.Add(id);

            ReadPos(m_Items);
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
            foreach (PosCheckResult check in m_PosList)
            {
                ListViewItem item = new ListViewItem(check.Pos);
                ListViewGroup group = lbItems.Groups[check.Key];
                if (group == null) group = lbItems.Groups.Add(check.Key, check.Description);
                item.Group = group;
                item.Tag = check;
                item.SubItems.Add(check.ErrorMessage);
                item.SubItems.Add(check.Items.Count.ToString() + " adet");
                item.SubItems.Add("Düzelt");
                item.SubItems.Add("Zoom");
                item.SubItems.Add("Select");
                lbItems.Items.Add(item);
            }
        }

        private void ReadPos(IEnumerable<ObjectId> items)
        {
            try
            {
                m_PosList = PosCheckResult.CheckAllInSelection(items, true, true);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbItems_DoubleClick(object sender, EventArgs e)
        {
            if (lbItems.SelectedItems.Count == 0) return;
            PosCheckResult check = lbItems.SelectedItems[0].Tag as PosCheckResult;
            DWGUtility.ZoomToObjects(check.Items);
        }

        #region ListViewExtender
        public class ListViewExtender : IDisposable
        {
            private int mouseDownColumn;
            private ListView listView;
            private ImageList imageList;

            private Dictionary<int, bool> columns;
            private Dictionary<int, System.Drawing.Image> images;
            private Dictionary<int, Size> buttonSizes;

            public delegate void CustomColumnClickEventHandler(object sender, CustomColumnClickEventArgs e);
            public event CustomColumnClickEventHandler CustomColumnClick;

            public class CustomColumnClickEventArgs
            {
                public ListViewItem Item { get; private set; }
                public ListViewItem.ListViewSubItem SubItem { get; private set; }
                public int ColumnIndex { get; private set; }

                public CustomColumnClickEventArgs(ListViewItem item, ListViewItem.ListViewSubItem subItem, int columnIndex)
                {
                    Item = item;
                    SubItem = subItem;
                    ColumnIndex = columnIndex;
                }
            }

            public ListViewExtender(ListView listview)
            {
                listView = listview;
                imageList = new ImageList();

                columns = new Dictionary<int, bool>();
                buttonSizes = new Dictionary<int, Size>();
                images = new Dictionary<int, System.Drawing.Image>();

                listview.View = View.Details;
                listview.OwnerDraw = true;
                listview.FullRowSelect = true;
                listview.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(lv_DrawColumnHeader);
                listview.DrawItem += new DrawListViewItemEventHandler(lv_DrawItem);
                listview.DrawSubItem += new DrawListViewSubItemEventHandler(lv_DrawSubItem);
                mouseDownColumn = -1;
                listview.MouseDown += new MouseEventHandler(lv_MouseDown);
                listview.MouseUp += new MouseEventHandler(lv_MouseUp);
                listview.MouseLeave += new EventHandler(lv_MouseLeave);
                listview.ColumnWidthChanging += new ColumnWidthChangingEventHandler(lv_ColumnWidthChanging);
            }

            void lv_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
            {
                if (columns.ContainsKey(e.ColumnIndex))
                {
                    e.Cancel = true;
                    e.NewWidth = listView.Columns[e.ColumnIndex].Width;
                }
            }

            public void ExtendAsButtonColumn(int index)
            {
                ExtendAsButtonColumn(index, Size.Empty);
            }

            public void ExtendAsButtonColumn(int index, Size size)
            {
                columns.Add(index, false);
                buttonSizes.Add(index, size);
            }

            public void ExtendAsImageButtonColumn(int index, System.Drawing.Image image)
            {
                ExtendAsImageButtonColumn(index, image, Size.Empty);
            }

            public void ExtendAsImageButtonColumn(int index, System.Drawing.Image image, Size size)
            {
                columns.Add(index, false);
                images.Add(index, image);
                buttonSizes.Add(index, size);
            }

            public void SetRowHeight(int height)
            {
                imageList.ImageSize = new Size(height, height);
                listView.SmallImageList = imageList;
            }

            void lv_MouseLeave(object sender, EventArgs e)
            {
                if (mouseDownColumn > 0)
                {
                    mouseDownColumn = -1;
                    listView.Refresh();
                }
            }

            protected virtual void OnCustomColumnClick(CustomColumnClickEventArgs e)
            {
                if (CustomColumnClick != null)
                    CustomColumnClick(listView, e);
            }

            void lv_MouseUp(object sender, MouseEventArgs e)
            {
                ListViewHitTestInfo info = listView.HitTest(e.Location);
                if (mouseDownColumn > 0 && info.Item != null && info.SubItem != null)
                {
                    int index = listView.Items[info.Item.Index].SubItems.IndexOf(info.SubItem);
                    if (columns.ContainsKey(index))
                    {
                        OnCustomColumnClick(new CustomColumnClickEventArgs(info.Item, info.SubItem, index));
                        mouseDownColumn = -1;
                        listView.Refresh();
                    }
                }
            }

            void lv_MouseDown(object sender, MouseEventArgs e)
            {
                ListViewHitTestInfo info = listView.HitTest(e.Location);
                if (info.Item != null && info.SubItem != null)
                {
                    int index = listView.Items[info.Item.Index].SubItems.IndexOf(info.SubItem);
                    if (columns.ContainsKey(index))
                    {
                        mouseDownColumn = index;
                        listView.Refresh();
                    }
                }
            }

            void lv_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
            {
                e.DrawDefault = true;
            }

            void lv_DrawItem(object sender, DrawListViewItemEventArgs e)
            {
                ;
            }

            void lv_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
            {
                e.Graphics.SetClip(e.Bounds);

                Color backColor = SystemColors.Window;
                Color foreColor = SystemColors.WindowText;

                if ((e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected)
                {
                    backColor = SystemColors.Highlight;
                    foreColor = SystemColors.HighlightText;
                }

                using (Brush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                // Button
                if (columns.ContainsKey(e.ColumnIndex))
                {
                    Rectangle rec = e.Bounds;
                    Size sz = Size.Empty;
                    if (buttonSizes.TryGetValue(e.ColumnIndex, out sz))
                    {
                        if (!sz.IsEmpty)
                        {
                            rec = new Rectangle(e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2, sz.Width, sz.Height);
                        }
                    }

                    if ((e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected && mouseDownColumn == e.ColumnIndex)
                        ButtonRenderer.DrawButton(e.Graphics, rec, System.Windows.Forms.VisualStyles.PushButtonState.Pressed);
                    else
                        ButtonRenderer.DrawButton(e.Graphics, rec, System.Windows.Forms.VisualStyles.PushButtonState.Normal);
                    foreColor = SystemColors.WindowText;
                }

                // Image
                if (images.ContainsKey(e.ColumnIndex))
                {
                    System.Drawing.Image image = images[e.ColumnIndex];
                    int x = e.Bounds.Left + (e.Bounds.Width - image.Width) / 2;
                    int y = e.Bounds.Top + (e.Bounds.Height - image.Height) / 2;
                    e.Graphics.DrawImageUnscaled(image, x, y);
                }

                // Text
                if (columns.ContainsKey(e.ColumnIndex))
                {
                    if (!images.ContainsKey(e.ColumnIndex))
                    {
                        Rectangle rec = e.Bounds;
                        rec.Inflate(-2, 0);
                        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, rec, foreColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                    }
                }
                else
                {
                    TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
                    if (listView.Columns[e.ColumnIndex].TextAlign == HorizontalAlignment.Center)
                        flags |= TextFormatFlags.HorizontalCenter;
                    else if (listView.Columns[e.ColumnIndex].TextAlign == HorizontalAlignment.Left)
                        flags |= TextFormatFlags.Left;
                    else if (listView.Columns[e.ColumnIndex].TextAlign == HorizontalAlignment.Right)
                        flags |= TextFormatFlags.Right;
                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, e.Bounds, foreColor, flags);
                }

                e.Graphics.ResetClip();
            }

            public void Dispose()
            {
                imageList.Dispose();
            }
        }
        #endregion
    }
}
