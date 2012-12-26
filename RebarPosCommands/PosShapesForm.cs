using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.EditorInput;

namespace RebarPosCommands
{
    public partial class PosShapesForm : Form
    {
        private class ShapeCopy
        {
            public string name;
            public int fields;
            public string formula;
            public string formulaBending;
            public int priority;

            public List<PosShape.Shape> shapes;

            public ShapeCopy()
            {
                shapes = new List<PosShape.Shape>();
            }
        }

        List<ShapeCopy> m_Copies;

        public bool ShowShapes { get { return chkShowShapes.Checked; } }

        public PosShapesForm()
        {
            InitializeComponent();

            m_Copies = new List<ShapeCopy>();
            posShapeView.BackColor = DWGUtility.ModelBackgroundColor();
        }

        public bool Init(bool showShapes)
        {
            chkShowShapes.Checked = showShapes;

            Dictionary<string, PosShape> shapes = PosShape.GetAllPosShapes();

            if (shapes.Count == 0)
            {
                return false;
            }

            try
            {
                foreach (KeyValuePair<string, PosShape> item in shapes)
                {
                    ShapeCopy copy = new ShapeCopy();

                    copy.name = item.Key;
                    copy.fields = item.Value.Fields;
                    copy.formula = item.Value.Formula;
                    copy.formulaBending = item.Value.FormulaBending;
                    copy.priority = item.Value.Priority;

                    for (int j = 0; j < item.Value.Items.Count; j++)
                    {
                        copy.shapes.Add(item.Value.Items[j]);
                    }

                    m_Copies.Add(copy);
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            m_Copies.Sort(new CompareShapesForSort());
            foreach (ShapeCopy copy in m_Copies)
            {
                ListViewItem lv = new ListViewItem(copy.name);
                lbShapes.Items.Add(lv);
            }
            if (lbShapes.Items.Count != 0)
                lbShapes.SelectedIndices.Add(0);
            UpdateItemImages();

            SetShape();

            return true;
        }

        private class CompareShapesForSort : IComparer<ShapeCopy>
        {
            public int Compare(ShapeCopy p1, ShapeCopy p2)
            {
                if (p1 == p2) return 0;
                if (p1.name == p2.name) return 0;

                if (p1.name == "GENEL")
                    return 1;
                else if (p2.name == "GENEL")
                    return -1;
                else
                {
                    int n1 = 0;
                    int n2 = 0;
                    if (int.TryParse(p1.name, out n1) && int.TryParse(p2.name, out n2))
                    {
                        return n1 < n2 ? -1 : n1 > n2 ? 1 : 0;
                    }
                    else
                        return string.CompareOrdinal(p1.name, p2.name);
                }
            }
        }

        public void SetShape()
        {
            if (lbShapes.SelectedIndices.Count == 0) return;

            ShapeCopy copy = m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);
            if (copy == null)
                return;

            udFields.Value = copy.fields;
            txtFormula.Text = copy.formula;
            txtFormulaBending.Text = copy.formulaBending;
            udPriority.Value = Math.Max(0, Math.Min(copy.priority, 99));

            posShapeView.Reset();
            foreach (PosShape.Shape draw in copy.shapes)
            {
                if (draw is PosShape.ShapeLine)
                {
                    PosShape.ShapeLine line = draw as PosShape.ShapeLine;
                    posShapeView.AddLine(line.Color.ColorValue, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2, line.Visible);
                }
                else if (draw is PosShape.ShapeArc)
                {
                    PosShape.ShapeArc arc = draw as PosShape.ShapeArc;
                    posShapeView.AddArc(arc.Color.ColorValue, (float)arc.X, (float)arc.Y, (float)arc.R, (float)(arc.StartAngle * 180.0 / Math.PI), (float)(arc.EndAngle * 180.0 / Math.PI), arc.Visible);
                }
                else if (draw is PosShape.ShapeText)
                {
                    PosShape.ShapeText text = draw as PosShape.ShapeText;
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
                    posShapeView.AddText(text.Color.ColorValue, (float)text.X, (float)text.Y, (float)text.Height, text.Text, horizontal, vertical, text.Visible);
                }
            }
        }

        private void lbShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbShapes.SelectedIndices.Count == 0)
            {
                gbOptions.Enabled = false;
                posShapeView.Enabled = false;
                return;
            }
            else
            {
                gbOptions.Enabled = true;
                posShapeView.Enabled = true;
                SetShape();
            }
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < m_Copies.Count; i++)
            {
                ShapeCopy copy = m_Copies[i];
                ListViewItem lv = lbShapes.Items[i];
                lv.ImageIndex = 0;
            }
        }

        private ShapeCopy GetSelected()
        {
            if (lbShapes.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (chkShowShapes.Checked)
                ShowShapesOverrule.Instance.Add();
            else
                ShowShapesOverrule.Instance.Remove();
            DWGUtility.RefreshAllPos();

            Close();
        }
    }
}
