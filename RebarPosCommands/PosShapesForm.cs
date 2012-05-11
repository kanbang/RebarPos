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
            public ObjectId id;
            public string name;

            public bool isNew;
            public bool isUsed;
            public bool isDeleted;

            public int fields;
            public string formula;
            public string formulaBending;
            public int priority;

            public List<PosShape.Shape> shapes;

            public ShapeCopy()
            {
                shapes = new List<PosShape.Shape>();
            }
            public void Add(PosShape.Shape shape)
            {
                shapes.Add(shape);
            }
            public void AddLine(Color color, double x1, double y1, double x2, double y2)
            {
                shapes.Add(new PosShape.ShapeLine(Autodesk.AutoCAD.Colors.Color.FromColor(color), x1, y1, x2, y2));
            }
            public void AddArc(Color color, double x, double y, double r, double startAngle, double endAngle)
            {
                shapes.Add(new PosShape.ShapeArc(Autodesk.AutoCAD.Colors.Color.FromColor(color), x, y, r, startAngle, endAngle));
            }
            public void AddText(Color color, double x, double y, double height, string text)
            {
                shapes.Add(new PosShape.ShapeText(Autodesk.AutoCAD.Colors.Color.FromColor(color), x, y, height, text));
            }
        }

        List<ShapeCopy> m_Copies;
        Dictionary<string, ObjectId> m_Shapes;

        public bool ShowShapes { get { return chkShowShapes.Checked; } }

        public PosShapesForm()
        {
            InitializeComponent();

            m_Copies = new List<ShapeCopy>();
            m_Shapes = new Dictionary<string, ObjectId>();
            AcadPreferences pref = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as AcadPreferences;
            uint indexColor = pref.Display.GraphicsWinModelBackgrndColor;
            posShapeView.BackColor = ColorTranslator.FromOle((int)indexColor);
        }

        public bool Init(bool showShapes)
        {
            chkShowShapes.Checked = showShapes;

            m_Shapes = DWGUtility.GetShapes();

            if (m_Shapes.Count == 0)
            {
                return false;
            }

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (KeyValuePair<string, ObjectId> item in m_Shapes)
                    {
                        PosShape shape = tr.GetObject(item.Value, OpenMode.ForRead) as PosShape;
                        if (shape == null) continue;

                        ShapeCopy copy = new ShapeCopy();

                        copy.name = item.Key;

                        copy.id = item.Value;
                        copy.isNew = false;
                        copy.isDeleted = false;
                        copy.isUsed = false;

                        copy.fields = shape.Fields;
                        copy.formula = shape.Formula;
                        copy.formulaBending = shape.FormulaBending;
                        copy.priority = shape.Priority;

                        for (int j = 0; j < shape.Items.Count; j++)
                        {
                            copy.Add(shape.Items[j]);
                        }

                        m_Copies.Add(copy);
                    }

                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                    using (BlockTableRecordEnumerator it = btr.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            RebarPos pos = tr.GetObject(it.Current, OpenMode.ForRead) as RebarPos;
                            if (pos != null)
                            {
                                ShapeCopy copy = m_Copies.Find(p => p.id == pos.ShapeId);
                                if (copy != null)
                                {
                                    copy.isUsed = true;
                                    continue;
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

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

        public void SetShape()
        {
            if (lbShapes.SelectedIndices.Count == 0) return;

            ShapeCopy copy = m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);
            if (copy == null)
                return;

            btnRemove.Enabled = !copy.isUsed;
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
                    posShapeView.AddLine(line.Color.ColorValue, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2);
                }
                else if (draw is PosShape.ShapeArc)
                {
                    PosShape.ShapeArc arc = draw as PosShape.ShapeArc;
                    posShapeView.AddArc(arc.Color.ColorValue, (float)arc.X, (float)arc.Y, (float)arc.R, (float)arc.StartAngle, (float)arc.EndAngle);
                }
                else if (draw is PosShape.ShapeText)
                {
                    PosShape.ShapeText text = draw as PosShape.ShapeText;
                    posShapeView.AddText(text.Color.ColorValue, (float)text.X, (float)text.Y, (float)text.Height, text.Text);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShapeCopy org = m_Copies[0];
            if (lbShapes.SelectedIndices.Count != 0)
                org = m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);

            int i = 1;
            while (m_Copies.Exists(p => p.name.ToUpperInvariant() == "SHAPE" + i.ToString()))
            {
                i++;
            }

            ShapeCopy copy = new ShapeCopy();

            copy.name = "Shape" + i.ToString();

            copy.id = ObjectId.Null;
            copy.isNew = true;
            copy.isDeleted = false;
            copy.isUsed = false;

            copy.fields = org.fields;
            copy.formula = org.formula;
            copy.formulaBending = org.formulaBending;
            copy.priority = org.priority;

            foreach (PosShape.Shape draw in org.shapes)
            {
                copy.Add(draw);
            }

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.name);
            lv.ImageIndex = 2;
            lbShapes.Items.Add(lv);
            lbShapes.SelectedIndices.Clear();
            lbShapes.SelectedIndices.Add(lbShapes.Items.Count - 1);
            lv.BeginEdit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbShapes.SelectedIndices.Count == 0) return;

            ShapeCopy copy = m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);
            if (copy == null)
            {
                return;
            }
            copy.isDeleted = true;
            UpdateItemImages();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lbShapes.SelectedIndices.Count == 0) return;
            lbShapes.SelectedItems[0].BeginEdit();
        }

        private void lbShapes_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
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

        private void lbShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbShapes.SelectedIndices.Count == 0)
            {
                btnRemove.Enabled = false;
                btnRename.Enabled = false;
                gbOptions.Enabled = false;
                return;
            }
            else
            {
                btnRemove.Enabled = true;
                btnRename.Enabled = true;
                gbOptions.Enabled = true;
                SetShape();
            }
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < m_Copies.Count; i++)
            {
                ShapeCopy copy = m_Copies[i];
                ListViewItem lv = lbShapes.Items[i];
                if (copy.isUsed)
                    lv.ImageIndex = 0;
                else if (copy.isDeleted)
                    lv.ImageIndex = 3;
                else if (copy.isNew)
                    lv.ImageIndex = 2;
                else
                    lv.ImageIndex = 1;
            }
        }

        private ShapeCopy GetSelected()
        {
            if (lbShapes.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.name == lbShapes.SelectedItems[0].Text);
        }

        private void udFields_ValueChanged(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;
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
            copy.formulaBending = txtFormulaBending.Text;
        }

        private void udPriority_ValueChanged(object sender, EventArgs e)
        {
            ShapeCopy copy = GetSelected();
            if (copy == null) return;
            copy.priority = (int)udPriority.Value;
        }

        private void btnSelectShape_Click(object sender, EventArgs e)
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

            copy.shapes.Clear();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (SelectedObject sel in result.Value)
                    {
                        DBObject obj = tr.GetObject(sel.ObjectId, OpenMode.ForRead);

                        if (obj is Line)
                        {
                            Line line = obj as Line;
                            copy.AddLine(line.Color.ColorValue, line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y);
                        }
                        else if (obj is Arc)
                        {
                            Arc arc = obj as Arc;
                            copy.AddArc(arc.Color.ColorValue, arc.Center.X, arc.Center.Y, arc.Radius, arc.StartAngle, arc.EndAngle);
                        }
                        else if (obj is DBText)
                        {
                            DBText text = obj as DBText;
                            copy.AddText(text.Color.ColorValue, text.Position.X, text.Position.Y, text.Height, text.TextString);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            SetShape();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Apply changes
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosShape.TableName), OpenMode.ForWrite);
                    foreach (ShapeCopy copy in m_Copies)
                    {
                        if (copy.isDeleted)
                        {
                            if (!copy.id.IsNull)
                                dict.Remove(copy.id);
                        }
                        else if (copy.isNew)
                        {
                            PosShape shape = new PosShape();

                            shape.Name = copy.name;

                            shape.Fields = copy.fields;
                            shape.Formula = copy.formula;
                            shape.FormulaBending = copy.formulaBending;
                            shape.Priority = copy.priority;

                            shape.Items.Clear();
                            foreach (PosShape.Shape draw in copy.shapes)
                            {
                                if (draw is PosShape.ShapeLine)
                                {
                                    PosShape.ShapeLine line = draw as PosShape.ShapeLine;
                                    shape.Items.AddLine(line.X1, line.Y1, line.X2, line.Y2, line.Color);
                                }
                                else if (draw is PosShape.ShapeArc)
                                {
                                    PosShape.ShapeArc arc = draw as PosShape.ShapeArc;
                                    shape.Items.AddArc(arc.X, arc.Y, arc.R, arc.StartAngle, arc.EndAngle, arc.Color);
                                }
                                else if (draw is PosShape.ShapeText)
                                {
                                    PosShape.ShapeText text = draw as PosShape.ShapeText;
                                    shape.Items.AddText(text.X, text.Y, text.Height, text.Text, text.Color);
                                }
                            }

                            copy.id = dict.SetAt("*", shape);
                            tr.AddNewlyCreatedDBObject(shape, true);
                        }
                        else
                        {
                            PosShape shape = tr.GetObject(copy.id, OpenMode.ForWrite) as PosShape;

                            shape.Name = copy.name;

                            shape.Fields = copy.fields;
                            shape.Formula = copy.formula;
                            shape.FormulaBending = copy.formulaBending;
                            shape.Priority = copy.priority;

                            shape.Items.Clear();
                            foreach (PosShape.Shape draw in copy.shapes)
                            {
                                if (draw is PosShape.ShapeLine)
                                {
                                    PosShape.ShapeLine line = draw as PosShape.ShapeLine;
                                    shape.Items.AddLine(line.X1, line.Y1, line.X2, line.Y2, line.Color);
                                }
                                else if (draw is PosShape.ShapeArc)
                                {
                                    PosShape.ShapeArc arc = draw as PosShape.ShapeArc;
                                    shape.Items.AddArc(arc.X, arc.Y, arc.R, arc.StartAngle, arc.EndAngle, arc.Color);
                                }
                                else if (draw is PosShape.ShapeText)
                                {
                                    PosShape.ShapeText text = draw as PosShape.ShapeText;
                                    shape.Items.AddText(text.X, text.Y, text.Height, text.Text, text.Color);
                                }
                            }

                            DWGUtility.RefreshPosWithShape(copy.id);
                        }
                    }
                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
