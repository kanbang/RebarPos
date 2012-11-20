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
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Interop;

namespace RebarPosCommands
{
    public partial class EditDetachedPosForm : Form
    {
        ObjectId m_Pos;
        RebarPos.HitTestResult hit;

        public EditDetachedPosForm()
        {
            InitializeComponent();

            m_Pos = ObjectId.Null;
        }

        public bool Init(ObjectId id, Point3d pt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    m_Pos = id;

                    RebarPos pos = tr.GetObject(m_Pos, OpenMode.ForRead) as RebarPos;
                    if (pos == null)
                    {
                        return false;
                    }


                    txtPosMarker.Text = pos.Pos;

                    hit = pos.HitTest(pt);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool haserror = false;
            if (!CheckPosMarker()) haserror = true;

            if (haserror)
            {
                MessageBox.Show("Lütfen hatalı değerleri düzeltin.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    RebarPos pos = tr.GetObject(m_Pos, OpenMode.ForWrite) as RebarPos;
                    if (pos == null) return;

                    pos.Pos = txtPosMarker.Text;

                    tr.Commit();

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditPosForm_Shown(object sender, EventArgs e)
        {
            switch (hit)
            {
                case RebarPos.HitTestResult.Pos:
                    txtPosMarker.Select();
                    txtPosMarker.SelectAll();
                    break;
                default:
                    txtPosMarker.Select();
                    txtPosMarker.SelectAll();
                    break;
            }
        }

        private void txtPosMarker_Validating(object sender, CancelEventArgs e)
        {
            CheckPosMarker();
        }

        private bool CheckPosMarker()
        {
            int val = 0;
            if (string.IsNullOrEmpty(txtPosMarker.Text) || int.TryParse(txtPosMarker.Text, out val))
            {
                errorProvider.SetError(txtPosMarker, "");
                return true;
            }
            else
            {
                errorProvider.SetError(txtPosMarker, "Poz numarası tam sayı olmalıdır.");
                errorProvider.SetIconAlignment(txtPosMarker, ErrorIconAlignment.MiddleLeft);
                return false;
            }
        }
    }
}
