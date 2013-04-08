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

        public void SetShapes()
        {
            SetShapes("GENEL");
        }

        public void SetShapes(string current)
        {
            SetShapes(current, PosShape.GetAllPosShapes());
        }

        public void SetShapes(string current, IEnumerable<string> inshapes)
        {
            m_Current = current;

            List<string> shapes = new List<string>(inshapes);

            layoutPanel.Controls.Clear();

            // Get AutoCad model background color
            Color backColor = DWGUtility.ModelBackgroundColor();
            try
            {
                foreach (string name in shapes)
                {
                    PosShape shape = PosShape.GetPosShape(name);
                    if (shape != null)
                    {
                        Panel panel = new Panel();
                        panel.Size = new Size(145, 110);

                        Label shapeLabel = new Label();
                        shapeLabel.Size = new Size(145, 13);
                        shapeLabel.Text = shape.Name;
                        shapeLabel.Location = new Point(0, 0);
                        panel.Controls.Add(shapeLabel);

                        PosShapeView posShapeView = new PosShapeView();
                        posShapeView.SuspendUpdate();
                        posShapeView.ShapeName = shape.Name;
                        posShapeView.Selected = (shape.Name == m_Current);
                        posShapeView.Visible = true;
                        posShapeView.Size = new Size(145, 90);
                        posShapeView.BackColor = backColor;
                        posShapeView.Location = new Point(0, 15);
                        posShapeView.Click += new EventHandler(posShapeView_Click);
                        posShapeView.ResumeUpdate();

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

        private void posShapeView_Click(object sender, EventArgs e)
        {
            PosShapeView ctrl = sender as PosShapeView;
            if (ctrl != null)
            {
                m_Current = ctrl.ShapeName;
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
