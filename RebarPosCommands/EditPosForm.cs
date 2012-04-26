using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace RebarPosCommands
{
    public partial class EditPosForm : Form
    {
        Autodesk.AutoCAD.DatabaseServices.ObjectId m_Group;
        Autodesk.AutoCAD.DatabaseServices.ObjectId m_Shape;
        Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> m_Groups;
        Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> m_Shapes;

        public EditPosForm()
        {
            InitializeComponent();

            m_Groups = new Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId>();
            m_Shapes = new Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId>();
        }

        public void SetPos(RebarPos pos, PosGroup group, Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> groups, PosShape shape, Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> shapes)
        {
            txtPosMarker.Text = pos.Pos;
            txtPosCount.Text = pos.Count;
            txtPosDiameter.Text = pos.Diameter;
            txtPosSpacing.Text = pos.Spacing;
            txtPosMultiplier.Text = pos.Multiplier.ToString();
            chkIncludePos.Checked = (pos.Multiplier > 0);
            txtPosMultiplier.Enabled = (pos.Multiplier > 0);
            txtPosNote.Text = pos.Note;

            m_Groups = groups;
            m_Group = pos.GroupId;
            foreach (string name in m_Groups.Keys)
            {
                cbGroup.Items.Add(name);
            }
            int precision = group.Precision;
            m_Shapes = shapes;
            m_Shape = pos.ShapeId;
            string shapename = "";
            foreach (KeyValuePair<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> pair in m_Shapes)
            {
                if (pair.Value == pos.ShapeId)
                {
                    shapename = pair.Key;
                    break;
                }
            }

            rbShowAll.Checked = (pos.Display == RebarPos.DisplayStyle.All);
            rbWithoutLength.Checked = (pos.Display == RebarPos.DisplayStyle.WithoutLength);
            rbMarkerOnly.Checked = (pos.Display == RebarPos.DisplayStyle.MarkerOnly);

            txtA.Text = pos.A;
            txtB.Text = pos.B;
            txtC.Text = pos.C;
            txtD.Text = pos.D;
            txtE.Text = pos.E;
            txtF.Text = pos.F;

            txtA.Enabled = btnSelectA.Enabled = btnMeasureA.Enabled = (shape.Fields >= 1);
            txtB.Enabled = btnSelectB.Enabled = btnMeasureB.Enabled = (shape.Fields >= 2);
            txtC.Enabled = btnSelectC.Enabled = btnMeasureC.Enabled = (shape.Fields >= 3);
            txtD.Enabled = btnSelectD.Enabled = btnMeasureD.Enabled = (shape.Fields >= 4);
            txtE.Enabled = btnSelectE.Enabled = btnMeasureE.Enabled = (shape.Fields >= 5);
            txtF.Enabled = btnSelectF.Enabled = btnMeasureF.Enabled = (shape.Fields >= 6);

            lblPosShape.Text = shapename;
            lblAverageLength.Text = ((pos.MinLength + pos.MaxLength) / 2.0).ToString("F" + precision.ToString());
            if (pos.IsVarLength)
                lblTotalLength.Text = pos.MinLength.ToString("F" + precision.ToString()) + "~" + pos.MaxLength.ToString("F" + precision.ToString());
            else
                lblTotalLength.Text = pos.MinLength.ToString("F" + precision.ToString());

            UpdateShapeView();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelectA_Click(object sender, EventArgs e)
        {
            Hide();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityResult per = ed.GetEntity("\nSelect entity: ");
            Show();
        }

        private void posShapeView_Click(object sender, EventArgs e)
        {
            SelectShapeForm form = new SelectShapeForm();
            form.SetShapes(m_Shape);
            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == System.Windows.Forms.DialogResult.OK)
            {
                m_Shape = form.Current;
                UpdateShapeView();
            }
        }

        private void UpdateShapeView()
        {
            posShapeView.Reset();

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosShape shape = tr.GetObject(m_Shape, OpenMode.ForRead) as PosShape;
                    if (shape == null)
                        return;
                    for (int i = 0; i < shape.Items.Count; i++)
                    {
                        PosShape.Shape sh = shape.Items[i];
                        Color color = sh.Color.ColorValue;
                        if (sh is PosShape.ShapeLine)
                        {
                            PosShape.ShapeLine line = sh as PosShape.ShapeLine;
                            posShapeView.AddLine(color, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2);
                        }
                        else if (sh is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc arc = sh as PosShape.ShapeArc;
                            posShapeView.AddArc(color, (float)arc.X, (float)arc.Y, (float)arc.R, (float)arc.StartAngle, (float)arc.EndAngle);
                        }
                        else if (sh is PosShape.ShapeText)
                        {
                            PosShape.ShapeText text = sh as PosShape.ShapeText;
                            posShapeView.AddText(color, (float)text.X, (float)text.Y, (float)text.Height, text.Text);
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
