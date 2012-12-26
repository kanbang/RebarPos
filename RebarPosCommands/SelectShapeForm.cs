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
        public class ShapeDisplay
        {
            public string Shape { get; private set; }
            public string A { get; private set; }
            public string B { get; private set; }
            public string C { get; private set; }
            public string D { get; private set; }
            public string E { get; private set; }
            public string F { get; private set; }

            public ShapeDisplay(string shape, string a, string b, string c, string d, string e, string f)
            {
                Shape = shape;
                A = a; B = b; C = c; D = d; E = e; F = f;
            }

            public ShapeDisplay(string shape)
                : this(shape, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
            {
                ;
            }
        }

        string m_Current;

        public SelectShapeForm()
        {
            InitializeComponent();

            // Set size
            if (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width > 1500)
                this.Size = new Size(1366, 933);
            else
                this.Size = new Size(1040, 933);

            m_Current = string.Empty;
        }

        public string Current { get { return m_Current; } }

        public void SetShapes(string current)
        {
            SetShapes(current, PosShape.GetAllPosShapes().Keys);
        }

        public void SetShapes(string current, IEnumerable<string> inshapenames)
        {
            List<ShapeDisplay> shapes = new List<ShapeDisplay>();
            foreach (string name in inshapenames)
            {
                shapes.Add(new ShapeDisplay(name));
            }
            SetShapes(current, shapes);
        }

        public void SetShapes(string current, IEnumerable<ShapeDisplay> inshapes)
        {
            m_Current = current;

            layoutPanel.Controls.Clear();

            List<ShapeDisplay> shapes = new List<ShapeDisplay>();
            foreach (ShapeDisplay name in inshapes)
            {
                shapes.Add(name);
            }
            shapes.Sort(new CompareShapesForSort());

            // Get AutoCad model background color
            Color backColor = DWGUtility.ModelBackgroundColor();

            try
            {
                foreach (ShapeDisplay name in shapes)
                {
                    PosShape shape = PosShape.GetPosShape(name.Shape);
                    if (shape != null)
                    {
                        Panel panel = new Panel();
                        panel.Size = new Size(50 * 475 / 75, 13 + 2 + 50);

                        Label shapeLabel = new Label();
                        shapeLabel.Size = new Size(50 * 475 / 75, 13);
                        shapeLabel.Text = shape.Name;
                        shapeLabel.Location = new Point(0, 0);
                        panel.Controls.Add(shapeLabel);

                        PosShapeView posShapeView = new PosShapeView();
                        posShapeView.Selected = (shape.Name == m_Current);
                        posShapeView.Visible = true;
                        posShapeView.Size = new Size(50 * 475 / 75, 50);
                        posShapeView.Tag = shape.Name;
                        posShapeView.BackColor = backColor;
                        posShapeView.Location = new Point(0, 15);

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

                                string str = text.Text;
                                if (str == "A" && !string.IsNullOrEmpty(name.A)) str = name.A;
                                if (str == "B" && !string.IsNullOrEmpty(name.B)) str = name.B;
                                if (str == "C" && !string.IsNullOrEmpty(name.C)) str = name.C;
                                if (str == "D" && !string.IsNullOrEmpty(name.D)) str = name.D;
                                if (str == "E" && !string.IsNullOrEmpty(name.E)) str = name.E;
                                if (str == "F" && !string.IsNullOrEmpty(name.F)) str = name.F;

                                posShapeView.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, str, horizontal, vertical, text.Visible);
                            }
                        }

                        panel.Controls.Add(posShapeView);
                        layoutPanel.Controls.Add(panel);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class CompareShapesForSort : IComparer<ShapeDisplay>
        {
            public int Compare(ShapeDisplay p1, ShapeDisplay p2)
            {
                if (p1 == p2) return 0;

                if (p1.Shape == "GENEL")
                    return 1;
                else if (p2.Shape == "GENEL")
                    return -1;
                else
                {
                    int n1 = 0;
                    int n2 = 0;
                    if (int.TryParse(p1.Shape, out n1) && int.TryParse(p2.Shape, out n2))
                    {
                        return n1 < n2 ? -1 : n1 > n2 ? 1 : 0;
                    }
                    else
                        return string.CompareOrdinal(p1.Shape, p2.Shape);
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
