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

        public void SetShapes(IEnumerable<string> inshapes)
        {
            SetShapes("", inshapes);
        }

        public void SetShapes(string current, IEnumerable<string> inshapes)
        {
            m_Current = current;

            shapesView.SetShapes(inshapes);
            shapesView.CellBackColor = DWGUtility.ModelBackgroundColor();
            shapesView.CellSize = new Size(190, 70);
            shapesView.SelectedShape = m_Current;
            shapesView.ShowShapeNames = true;
            shapesView.ShapeClick += new MultiPosShapeViewClickEventHandler(posShapeView_Click);
        }

        public void SetPieceLengths(int index, string a, string b, string c, string d, string e, string f)
        {
            shapesView.SetPieceLengths(index, a, b, c, d, e, f);
        }

        private void posShapeView_Click(object sender, MultiPosShapeViewClickEventArgs e)
        {
            m_Current = e.Shape;
            DialogResult = DialogResult.OK;
            Close();
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
