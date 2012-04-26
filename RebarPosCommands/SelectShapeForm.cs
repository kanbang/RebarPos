using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class SelectShapeForm : Form
    {
        Autodesk.AutoCAD.DatabaseServices.ObjectId m_Current;

        public SelectShapeForm()
        {
            InitializeComponent();

            m_Current = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;
        }

        public Autodesk.AutoCAD.DatabaseServices.ObjectId Current { get { return m_Current; } }

        public void SetShapes(Autodesk.AutoCAD.DatabaseServices.ObjectId current)
        {
            m_Current = current;

            layoutPanel.Controls.Clear();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                    if (!namedDict.Contains(PosShape.TableName))
                        return;
                    DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosShape.TableName), OpenMode.ForRead);
                    DbDictionaryEnumerator it = dict.GetEnumerator();
                    while (it.MoveNext())
                    {
                        string name = it.Key;
                        Autodesk.AutoCAD.DatabaseServices.ObjectId id = it.Value;
                        PosShape shape = tr.GetObject(id, OpenMode.ForRead) as PosShape;
                        if (shape != null)
                        {
                            PosShapeView posShapeView = new PosShapeView();
                            posShapeView.ShapeName = name;
                            posShapeView.ShowName = true;
                            posShapeView.Selected = (id == m_Current);
                            posShapeView.Visible = true;
                            posShapeView.Size = new Size(200, 100);
                            posShapeView.Tag = id;
                            posShapeView.Click += new EventHandler(posShapeView_Click);

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

        private void posShapeView_Click(object sender, EventArgs e)
        {
            PosShapeView ctrl = sender as PosShapeView;
            if (ctrl != null)
            {
                m_Current = (Autodesk.AutoCAD.DatabaseServices.ObjectId)ctrl.Tag;
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
