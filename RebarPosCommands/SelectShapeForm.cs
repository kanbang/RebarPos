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

namespace RebarPosCommands
{
    public partial class SelectShapeForm : Form
    {
        string m_Current;

        public SelectShapeForm()
        {
            InitializeComponent();

            m_Current = string.Empty;
        }

        public string Current { get { return m_Current; } }

        public void SetShapes(string current)
        {
            Dictionary<string, ObjectId> shapedict = DWGUtility.GetShapes();
            List<KeyValuePair<string, ObjectId>> items = new List<KeyValuePair<string, ObjectId>>();
            foreach (KeyValuePair<string, ObjectId> item in shapedict)
            {
                items.Add(item);
            }
            items.Sort(new CompareShapesForSort());
            List<string> shapes = new List<string>();
            foreach (KeyValuePair<string, ObjectId> item in items)
            {
                shapes.Add(item.Key);
            }

            SetShapes(current, shapes);
        }

        public void SetShapes(string current, IEnumerable<string> shapes)
        {
            m_Current = current;

            layoutPanel.Controls.Clear();

            // Get AutoCad model background color
            Color backColor = DWGUtility.ModelBackgroundColor();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (string name in shapes)
                    {
                        PosShape shape = tr.GetObject(PosShape.GetShapeId(name), OpenMode.ForRead) as PosShape;
                        if (shape != null)
                        {
                            PosShapeView posShapeView = new PosShapeView();
                            posShapeView.ShapeName = shape.Name;
                            posShapeView.ShowName = true;
                            posShapeView.Selected = (shape.Name == m_Current);
                            posShapeView.Visible = true;
                            posShapeView.Size = new Size(200, 100);
                            posShapeView.Tag = shape.Name;
                            posShapeView.BackColor = backColor;

                            posShapeView.Click += new EventHandler(posShapeView_Click);

                            for (int i = 0; i < shape.Items.Count; i++)
                            {
                                PosShape.Shape sh = shape.Items[i];
                                Color color = sh.Color.ColorValue;
                                if (sh is PosShape.ShapeLine)
                                {
                                    PosShape.ShapeLine line = sh as PosShape.ShapeLine;
                                    posShapeView.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2, line.Visible);
                                }
                                else if (sh is PosShape.ShapeArc)
                                {
                                    PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                                    posShapeView.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)(arc.StartAngle * 180.0 / Math.PI), (float)(arc.EndAngle * 180.0 / Math.PI), arc.Visible);
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
                                    posShapeView.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text, horizontal, vertical, text.Visible);
                                }
                            }
                            layoutPanel.Controls.Add(posShapeView);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private class CompareShapesForSort : IComparer<KeyValuePair<string, ObjectId>>
        {
            public int Compare(KeyValuePair<string, ObjectId> p1, KeyValuePair<string, ObjectId> p2)
            {
                if (p1.Key == p2.Key) return 0;

                if (p1.Key == "GENEL")
                    return -1;
                else if (p2.Key == "GENEL")
                    return 1;
                else
                {
                    int n1 = 0;
                    int n2 = 0;
                    if (int.TryParse(p1.Key, out n1) && int.TryParse(p2.Key, out n2))
                    {
                        return n1 < n2 ? -1 : n1 > n2 ? 1 : 0;
                    }
                    else
                        return string.CompareOrdinal(p1.Key, p2.Key);
                }
            }
        }

        private void posShapeView_Click(object sender, EventArgs e)
        {
            PosShapeView ctrl = sender as PosShapeView;
            if (ctrl != null)
            {
                m_Current = (string)ctrl.Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void SelectShapeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Escape) != Keys.None)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
