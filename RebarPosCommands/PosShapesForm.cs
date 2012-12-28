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
            public bool builtin;

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
                    copy.builtin = item.Value.IsBuiltIn;

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

            PopulateList();

            return true;
        }

        private void PopulateList()
        {
            lbShapes.Items.Clear();
            foreach (ShapeCopy copy in m_Copies)
            {
                if (!chkUserOnly.Checked || (chkUserOnly.Checked && !copy.builtin))
                {
                    ListViewItem lv = new ListViewItem(copy.name);
                    lv.SubItems.Add(copy.fields.ToString());
                    lv.SubItems.Add(copy.formula);
                    lv.SubItems.Add(copy.formulaBending);
                    lbShapes.Items.Add(lv);
                }
            }
            if (lbShapes.Items.Count != 0)
                lbShapes.SelectedIndices.Add(0);
            UpdateItemImages();

            SetShape();
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
            ShapeCopy copy = GetSelected();
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
                gbDisplay.Enabled = false;

                btnDelete.Enabled = false;
                btnRename.Enabled = false;
                btnSelectShapes.Enabled = false;
            }
            else
            {
                gbOptions.Enabled = true;
                gbDisplay.Enabled = true;

                ShapeCopy copy = GetSelected();
                bool enable = (copy != null) && !copy.builtin;
                btnDelete.Enabled = enable;
                btnRename.Enabled = enable;
                btnSelectShapes.Enabled = enable;

                udFields.ReadOnly = !enable;
                txtFormula.ReadOnly = !enable;
                txtFormulaBending.ReadOnly = !enable;
                udPriority.ReadOnly = !enable;

                SetShape();
            }
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < lbShapes.Items.Count; i++)
            {
                ListViewItem lv = lbShapes.Items[i];
                ShapeCopy copy = m_Copies.Find(p => p.name == lv.Text);
                if (copy.builtin)
                    lv.ImageIndex = 0;
                else
                    lv.ImageIndex = 1;
            }
        }

        private ShapeCopy GetSelected()
        {
            if (lbShapes.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);
        }

        private bool ShowEditMessage()
        {
            return ConfimMessageBox.Show("Poz açılımlarını değiştirmek bu açılımların kullanıldığı çizimlerde hatalara neden olabilir. Devam etmek istiyor musunuz?", "RebarPos", "Bu uyarıyı bir daha gösterme.", @"SOFTWARE\SahinEng\RebarPos\SuppressShapeEditDialog", MessageBoxIcon.Warning, MessageBoxButtons.YesNo, DialogResult.Yes) == DialogResult.Yes;
        }

        private void chkUserOnly_CheckedChanged(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShapeCopy org = GetSelected();

            int i = 1;
            while (m_Copies.Exists(p => p.name.ToUpperInvariant() == "SHAPE" + i.ToString()))
            {
                i++;
            }

            ShapeCopy copy = new ShapeCopy();

            copy.name = "Shape" + i.ToString();

            copy.fields = org != null ? org.fields : 1;
            copy.formula = org != null ? org.formula : "A";
            copy.formulaBending = org != null ? org.formulaBending : "A";
            copy.priority = org != null ? org.priority : 0;

            copy.builtin = false;

            if (org != null)
            {
                foreach (PosShape.Shape draw in org.shapes)
                {
                    copy.shapes.Add(draw);
                }
            }

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.name);
            lv.SubItems.Add(copy.fields.ToString());
            lv.SubItems.Add(copy.formula);
            lv.SubItems.Add(copy.formulaBending);
            lv.ImageIndex = 1;
            lbShapes.Items.Add(lv);
            lbShapes.SelectedIndices.Clear();
            lbShapes.SelectedIndices.Add(lbShapes.Items.Count - 1);
            lv.BeginEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ShowEditMessage()) return;

            if (lbShapes.SelectedIndices.Count == 0) return;

            ShapeCopy copy = GetSelected();
            if (copy == null) return;
            if (copy.builtin) return;

            m_Copies.Remove(copy);
            lbShapes.Items.Remove(lbShapes.SelectedItems[0]);
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (!ShowEditMessage()) return;
            if (lbShapes.SelectedIndices.Count == 0) return;
            lbShapes.SelectedItems[0].BeginEdit();
        }

        private void lbShapes_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }
            if (lbShapes.SelectedIndices.Count == 0)
            {
                e.CancelEdit = true;
                return;
            }
            foreach (ListViewItem item in lbShapes.Items)
            {
                if (item.Index != lbShapes.SelectedIndices[0] && item.Text == e.Label)
                {
                    MessageBox.Show("Bu isim zaten mevcut. Lütfen farklı bir isim seçin.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.CancelEdit = true;
                    return;
                }
            }
            ShapeCopy copy = m_Copies.Find(p => p.name == lbShapes.Items[e.Item].Text);
            if (copy == null)
            {
                e.CancelEdit = true;
                return;
            }
            copy.name = e.Label;
        }

        private void btnSelectShapes_Click(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;

            // Select shapes
            Hide();
            TypedValue[] tvs = new TypedValue[] {
                new TypedValue((int)DxfCode.Operator, "<OR"),
                new TypedValue((int)DxfCode.Start, "LINE"),
                new TypedValue((int)DxfCode.Start, "ARC"),
                new TypedValue((int)DxfCode.Start, "TEXT"),
                new TypedValue((int)DxfCode.Operator, "OR>")
            };
            SelectionFilter filter = new SelectionFilter(tvs);
            PromptSelectionResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(filter);
            Show();
            if (result.Status != PromptStatus.OK || result.Value.Count == 0) return;

            int fieldCount = 1;
            copy.shapes.Clear();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (SelectedObject sel in result.Value)
                    {
                        DBObject obj = tr.GetObject(sel.ObjectId, OpenMode.ForRead);

                        bool visible = true;
                        Entity en = obj as Entity;
                        if (en != null)
                        {
                            LayerTableRecord ltr = (LayerTableRecord)tr.GetObject(en.LayerId, OpenMode.ForRead);

                            if (ltr != null)
                            {
                                visible = ltr.IsPlottable;
                            }
                        }

                        if (obj is Line)
                        {
                            Line line = obj as Line;

                            copy.shapes.Add(new PosShape.ShapeLine(line.Color, line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y, visible));
                        }
                        else if (obj is Arc)
                        {
                            Arc arc = obj as Arc;
                            copy.shapes.Add(new PosShape.ShapeArc(arc.Color, arc.Center.X, arc.Center.Y, arc.Radius, arc.StartAngle, arc.EndAngle, visible));
                        }
                        else if (obj is DBText)
                        {
                            DBText text = obj as DBText;
                            if (text.TextString == "A" && fieldCount < 1) fieldCount = 1;
                            if (text.TextString == "B" && fieldCount < 2) fieldCount = 2;
                            if (text.TextString == "C" && fieldCount < 3) fieldCount = 3;
                            if (text.TextString == "D" && fieldCount < 4) fieldCount = 4;
                            if (text.TextString == "E" && fieldCount < 5) fieldCount = 5;
                            if (text.TextString == "F" && fieldCount < 6) fieldCount = 6;
                            copy.shapes.Add(new PosShape.ShapeText(text.Color, text.Position.X, text.Position.Y, text.Height, text.TextString, text.HorizontalMode, text.VerticalMode, visible));
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            udFields.Value = fieldCount;
            SetShape();
        }

        private void udFields_ValueChanged(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;
            if (copy.builtin) return;
            copy.fields = (int)udFields.Value;
        }

        private void txtFormula_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text) || string.IsNullOrEmpty(txtFormula.Text.Trim()))
            {
                errorProvider.SetError(txtFormula, "Lütfen poz formülünü girin.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtFormula, "");
            }
        }

        private void txtFormula_Validated(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;
            if (copy.builtin) return;
            copy.formula = txtFormula.Text;
        }

        private void txtFormulaBending_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormulaBending.Text) || string.IsNullOrEmpty(txtFormulaBending.Text.Trim()))
            {
                errorProvider.SetError(txtFormulaBending, "Lütfen bükümlü poz formülünü girin.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtFormulaBending, "");
            }
        }

        private void txtFormulaBending_Validated(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;
            if (copy.builtin) return;
            copy.formulaBending = txtFormulaBending.Text;
        }

        private void udPriority_ValueChanged(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;
            if (copy.builtin) return;
            copy.priority = (int)udPriority.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RebarPos");
            string userShapesFile = System.IO.Path.Combine(userFolder, "ShapeList.txt");
            string newShapesFile = System.IO.Path.Combine(userFolder, "ShapeList.new");

            PosShape.ClearPosShapes(false, true);
            foreach (ShapeCopy copy in m_Copies)
            {
                if (!copy.builtin)
                {
                    PosShape shape = new PosShape();
                    shape.Name = copy.name;
                    shape.Fields = copy.fields;
                    shape.Formula = copy.formula;
                    shape.FormulaBending = copy.formulaBending;
                    shape.Priority = copy.priority;
                    shape.IsBuiltIn = copy.builtin;
                    foreach (PosShape.Shape sh in copy.shapes)
                    {
                        shape.Items.Add(sh);
                    }
                    PosShape.AddPosShape(shape);
                }
            }

            try
            {
                PosShape.SavePosShapesToFile(newShapesFile, false, true);
                System.IO.File.Delete(userShapesFile);
                System.IO.File.Move(newShapesFile, userShapesFile);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            PosShape.ReadPosShapesFromFile(userShapesFile, false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
