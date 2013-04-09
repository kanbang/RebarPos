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
using Autodesk.AutoCAD.Geometry;

namespace RebarPosCommands
{
    public partial class PosShapesForm : Form
    {
        private List<PosShape> m_Copies;

        public bool ShowShapes { get { return chkShowShapes.Checked; } }

        public PosShapesForm()
        {
            InitializeComponent();

            m_Copies = new List<PosShape>();
            posShapeView.BackColor = DWGUtility.ModelBackgroundColor();
        }

        public bool Init(bool showShapes)
        {
            chkShowShapes.Checked = showShapes;

            List<string> shapes = PosShape.GetAllPosShapes();

            if (shapes.Count == 0)
            {
                return false;
            }

            try
            {
                foreach (string name in shapes)
                {
                    m_Copies.Add((PosShape)PosShape.GetPosShape(name).Clone());
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            PopulateList();

            return true;
        }

        private void PopulateList()
        {
            lbShapes.Items.Clear();
            foreach (PosShape copy in m_Copies)
            {
                if (!chkUserOnly.Checked || (chkUserOnly.Checked && !copy.IsBuiltIn))
                {
                    ListViewItem lv = new ListViewItem(copy.Name);
                    lv.SubItems.Add(copy.Fields.ToString());
                    lv.SubItems.Add(copy.Formula);
                    lv.SubItems.Add(copy.FormulaBending);
                    lbShapes.Items.Add(lv);
                }
            }

            if (lbShapes.Items.Count != 0)
                lbShapes.SelectedIndices.Add(0);

            UpdateItemImages();

            SetShape();
        }

        public void SetShape()
        {
            PosShape copy = GetSelected();
            if (copy == null) return;

            udFields.Value = copy.Fields;
            txtFormula.Text = copy.Formula;
            txtFormulaBending.Text = copy.FormulaBending;
            udPriority.Value = Math.Max(0, Math.Min(copy.Priority, 99));

            posShapeView.SuspendUpdate();
            posShapeView.ClearItems();
            posShapeView.AddItem(copy);
            posShapeView.ResumeUpdate();
        }

        private void lbShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbShapes.SelectedIndices.Count == 0)
            {
                gbOptions.Enabled = false;

                btnDelete.Enabled = false;
                btnRename.Enabled = false;
                btnDrawShapes.Enabled = false;
                btnSelectShapes.Enabled = false;
            }
            else
            {
                gbOptions.Enabled = true;

                PosShape copy = GetSelected();
                bool enable = (copy != null) && !copy.IsBuiltIn;

                btnDelete.Enabled = enable;
                btnRename.Enabled = enable;
                btnDrawShapes.Enabled = true;
                btnSelectShapes.Enabled = enable;

                udFields.ReadOnly = !enable;
                txtFormula.ReadOnly = !enable;
                txtFormulaBending.ReadOnly = !enable;
                udPriority.ReadOnly = !enable;

                lbShapes.LabelEdit = enable;

                SetShape();
            }
        }

        private void UpdateItemImages()
        {
            for (int i = 0; i < lbShapes.Items.Count; i++)
            {
                ListViewItem lv = lbShapes.Items[i];
                PosShape copy = m_Copies.Find(p => p.Name == lv.Text);
                if (copy.IsBuiltIn)
                    lv.ImageIndex = 0;
                else
                    lv.ImageIndex = 1;
            }
        }

        private PosShape GetSelected()
        {
            if (lbShapes.SelectedIndices.Count == 0) return null;
            return m_Copies.Find(p => p.Name == lbShapes.SelectedItems[0].Text);
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
            PosShape org = GetSelected();

            int i = 1;
            while (m_Copies.Exists(p => p.Name.ToUpperInvariant() == "SHAPE" + i.ToString()))
            {
                i++;
            }

            PosShape copy = new PosShape();

            copy.Name = "Shape" + i.ToString();

            copy.Fields = org != null ? org.Fields : 1;
            copy.Formula = org != null ? org.Formula : "A";
            copy.FormulaBending = org != null ? org.FormulaBending : "A";
            copy.Priority = org != null ? org.Priority : 0;

            if (org != null)
            {
                for (int k = 0; k < org.Items.Count; k++)
                {
                    PosShape.Shape draw = org.Items[k];
                    copy.Items.Add(draw.Clone());
                }
            }

            m_Copies.Add(copy);

            ListViewItem lv = new ListViewItem(copy.Name);
            lv.SubItems.Add(copy.Fields.ToString());
            lv.SubItems.Add(copy.Formula);
            lv.SubItems.Add(copy.FormulaBending);
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

            PosShape copy = GetSelected();
            if (copy == null) return;
            if (copy.IsBuiltIn) return;

            m_Copies.Remove(copy);
            lbShapes.Items.Remove(lbShapes.SelectedItems[0]);
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lbShapes.SelectedIndices.Count == 0) return;
            PosShape copy = GetSelected();
            if (copy.IsBuiltIn) return;

            if (!ShowEditMessage()) return;

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
            PosShape copy = m_Copies.Find(p => p.Name == lbShapes.Items[e.Item].Text);
            if (copy == null || copy.IsBuiltIn)
            {
                e.CancelEdit = true;
                return;
            }
            copy.Name = e.Label;
        }

        private void btnSelectShapes_Click(object sender, EventArgs e)
        {
            PosShape copy = GetSelected();
            if (copy == null || copy.IsBuiltIn) return;

            // Select shapes
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            using (EditorUserInteraction UI = ed.StartUserInteraction(this))
            {
                TypedValue[] tvs = new TypedValue[] {
                    new TypedValue((int)DxfCode.Operator, "<OR"),
                    new TypedValue((int)DxfCode.Start, "LINE"),
                    new TypedValue((int)DxfCode.Start, "ARC"),
                    new TypedValue((int)DxfCode.Start, "TEXT"),
                    new TypedValue((int)DxfCode.Operator, "OR>")
                };
                SelectionFilter filter = new SelectionFilter(tvs);
                PromptSelectionResult result = ed.GetSelection(filter);

                if (result.Status != PromptStatus.OK || result.Value.Count == 0) return;

                int fieldCount = 1;
                copy.Items.Clear();
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

                                copy.Items.Add(new PosShape.ShapeLine(line.Color, line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y, visible));
                            }
                            else if (obj is Arc)
                            {
                                Arc arc = obj as Arc;
                                copy.Items.Add(new PosShape.ShapeArc(arc.Color, arc.Center.X, arc.Center.Y, arc.Radius, arc.StartAngle, arc.EndAngle, visible));
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
                                double x = text.Position.X;
                                double y = text.Position.Y;
                                if (text.AlignmentPoint.X != 0.0 || text.AlignmentPoint.Y != 0.0)
                                {
                                    x = text.AlignmentPoint.X;
                                    y = text.AlignmentPoint.Y;
                                }
                                copy.Items.Add(new PosShape.ShapeText(text.Color, x, y, text.Height, text.WidthFactor, text.TextString, "romans.shx", text.HorizontalMode, text.VerticalMode, visible));
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }

                udFields.Value = fieldCount;
            }

            SetShape();
        }

        private void udFields_ValueChanged(object sender, EventArgs e)
        {
            PosShape copy = GetSelected();
            if (copy == null) return;
            if (copy.IsBuiltIn) return;
            copy.Fields = (int)udFields.Value;
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
            PosShape copy = GetSelected();
            if (copy == null) return;
            if (copy.IsBuiltIn) return;
            copy.Formula = txtFormula.Text;
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
            PosShape copy = GetSelected();
            if (copy == null) return;
            if (copy.IsBuiltIn) return;
            copy.FormulaBending = txtFormulaBending.Text;
        }

        private void udPriority_ValueChanged(object sender, EventArgs e)
        {
            PosShape copy = GetSelected();
            if (copy == null) return;
            if (copy.IsBuiltIn) return;
            copy.Priority = (int)udPriority.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyChanges()
        {
            string userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RebarPos");
            string userShapesFile = System.IO.Path.Combine(userFolder, "ShapeList.txt");
            string newShapesFile = System.IO.Path.Combine(userFolder, "ShapeList.new");

            foreach (PosShape copy in m_Copies)
            {
                if (!copy.IsBuiltIn)
                {
                    if (PosShape.HasPosShape(copy.Name))
                    {
                        PosShape org = PosShape.GetPosShape(copy.Name);
                        org.Fields = copy.Fields;
                        org.Formula = copy.Formula;
                        org.FormulaBending = copy.FormulaBending;
                        org.Priority = copy.Priority;
                        org.Items.Clear();
                        for (int i=0;i<copy.Items.Count;i++)
                        {
                            org.Items.Add(copy.Items[i].Clone());
                        }
                    }
                    else
                    {
                        PosShape.AddPosShape(copy);
                    }
                }
            }

            try
            {
                PosShape.SavePosShapesToFile(newShapesFile);
                System.IO.File.Delete(userShapesFile);
                System.IO.File.Move(newShapesFile, userShapesFile);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            PosShape.ReadPosShapesFromFile(userShapesFile);
        }

        private void btnDrawShapes_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointResult result;
            using (EditorUserInteraction UI = ed.StartUserInteraction(this))
            {
                result = ed.GetPoint("Açılım şablonun çizileceği yer: ");
            }
            if (result.Status == PromptStatus.OK)
            {
                Point3d inspt = result.Value;

                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                        Point3dCollection vertices = new Point3dCollection(new Point3d[]{
                            new Point3d(0, 0, 0),
                            new Point3d(475, 0, 0),
                            new Point3d(475, 75, 0),
                            new Point3d(0, 75, 0),
                        });
                        Polyline2d rec = new Polyline2d(Poly2dType.SimplePoly, vertices, 0, true, 0, 0, null);
                        rec.ColorIndex = 256;
                        rec.TransformBy(Matrix3d.Displacement(inspt.GetAsVector()));
                        rec.SetDatabaseDefaults(db);
                        btr.AppendEntity(rec);
                        tr.AddNewlyCreatedDBObject(rec, true);

                        PosShape shape = GetSelected();
                        Point3d ext = shape.Bounds.Value.MinPoint;
                        Matrix3d offset = Matrix3d.Displacement(new Vector3d(-ext.X, -ext.Y, 0));
                        for (int i = 0; i < shape.Items.Count; i++)
                        {
                            PosShape.Shape item = shape.Items[i];
                            int col = item.Color.ColorIndex;
                            if (!item.Visible) col = 8;
                            ObjectId hiddenLayer = PosUtility.DefpointsLayer;
                            if (item is PosShape.ShapeLine)
                            {
                                PosShape.ShapeLine line = (PosShape.ShapeLine)item;
                                Line dbobj = new Line(new Point3d(line.X1, line.Y1, 0), new Point3d(line.X2, line.Y2, 0));
                                dbobj.ColorIndex = col;
                                if (!item.Visible) dbobj.LayerId = hiddenLayer;
                                dbobj.TransformBy(offset);
                                dbobj.TransformBy(Matrix3d.Displacement(inspt.GetAsVector()));

                                dbobj.SetDatabaseDefaults(db);
                                btr.AppendEntity(dbobj);
                                tr.AddNewlyCreatedDBObject(dbobj, true);
                            }
                            if (item is PosShape.ShapeArc)
                            {
                                PosShape.ShapeArc arc = (PosShape.ShapeArc)item;
                                Arc dbobj = new Arc(new Point3d(arc.X, arc.Y, 0), arc.R, arc.StartAngle, arc.EndAngle);
                                dbobj.ColorIndex = col;
                                if (!item.Visible) dbobj.LayerId = hiddenLayer;
                                dbobj.TransformBy(offset);
                                dbobj.TransformBy(Matrix3d.Displacement(inspt.GetAsVector()));

                                dbobj.SetDatabaseDefaults(db);
                                btr.AppendEntity(dbobj);
                                tr.AddNewlyCreatedDBObject(dbobj, true);
                            }
                            if (item is PosShape.ShapeText)
                            {
                                PosShape.ShapeText text = (PosShape.ShapeText)item;
                                DBText dbobj = new DBText();
                                dbobj.Position = new Point3d(text.X, text.Y, 0);
                                dbobj.TextStyleId = PosUtility.CreateTextStyle("", text.Font, text.Width);
                                dbobj.Height = text.Height;
                                dbobj.WidthFactor = text.Width;
                                dbobj.HorizontalMode = text.HorizontalAlignment;
                                dbobj.VerticalMode = text.VerticalAlignment;
                                dbobj.AlignmentPoint = new Point3d(text.X, text.Y, 0);
                                dbobj.TextString = text.Text;
                                dbobj.ColorIndex = col;
                                if (!item.Visible) dbobj.LayerId = hiddenLayer;
                                dbobj.TransformBy(offset);
                                dbobj.TransformBy(Matrix3d.Displacement(inspt.GetAsVector()));

                                dbobj.SetDatabaseDefaults(db);
                                btr.AppendEntity(dbobj);
                                tr.AddNewlyCreatedDBObject(dbobj, true);
                            }
                        }

                        tr.Commit();
                    }
                    catch (System.Exception)
                    {
                        ;
                    }
                }

                MessageBox.Show("Açılımı şablon çerçevesi içine çizin ve tekrar bu diyalog kutusuna dönüp çizimden al düğmesini tıklayın.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
