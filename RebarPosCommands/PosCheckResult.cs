using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using Autodesk.AutoCAD.DatabaseServices;

using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    #region DiameterCheck
    public class DiameterCheck : PosCheckResult
    {
        private class CompareByDiameter : IComparer<string>
        {
            public int Compare(string e1, string e2)
            {
                int p1 = 0;
                int p2 = 0;
                int.TryParse(e1, out p1);
                int.TryParse(e2, out p2);

                return (p1 == p2 ? 0 : (p1 < p2 ? -1 : 1));
            }
        }

        public override string Key { get { return "diameter"; } }
        public override string Description { get { return "Çap Hatası"; } }

        public List<string> Diameters { get; private set; }

        public override string ErrorMessage
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Diameters.Count; i++)
                {
                    sb.Append(Diameters[i]);
                    if (i != Diameters.Count - 1) sb.Append(", ");
                }
                return sb.ToString();
            }
        }

        public DiameterCheck(string pos, IEnumerable<string> diameters)
            : base(pos)
        {
            Diameters = new List<string>();
            foreach (string dia in diameters)
                Diameters.Add(dia);
            Diameters.Sort(new CompareByDiameter());
        }

        public override bool Fix()
        {
            using (System.Windows.Forms.Form frmEdit = new System.Windows.Forms.Form())
            {
                System.Windows.Forms.Label lblDiameter;
                System.Windows.Forms.ComboBox cbDiameter;
                System.Windows.Forms.Button btnCancel;
                System.Windows.Forms.Button btnOK;
                System.Windows.Forms.GroupBox groupBox;

                lblDiameter = new System.Windows.Forms.Label();
                cbDiameter = new System.Windows.Forms.ComboBox();
                btnCancel = new System.Windows.Forms.Button();
                btnOK = new System.Windows.Forms.Button();
                groupBox = new System.Windows.Forms.GroupBox();

                groupBox.SuspendLayout();
                frmEdit.SuspendLayout();
                // 
                // lblDiameter
                // 
                lblDiameter.AutoSize = true;
                lblDiameter.Location = new System.Drawing.Point(17, 22);
                lblDiameter.Name = "lblDiameter";
                lblDiameter.Size = new System.Drawing.Size(50, 13);
                lblDiameter.TabIndex = 0;
                lblDiameter.Text = "Yeni Çap";
                // 
                // comboBox1
                // 
                cbDiameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                cbDiameter.FormattingEnabled = true;
                cbDiameter.Location = new System.Drawing.Point(105, 19);
                cbDiameter.Name = "cbDiameter";
                cbDiameter.Size = new System.Drawing.Size(121, 21);
                cbDiameter.TabIndex = 1;
                // 
                // btnCancel
                // 
                btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                btnCancel.Location = new System.Drawing.Point(179, 72);
                btnCancel.Name = "btnCancel";
                btnCancel.Size = new System.Drawing.Size(75, 23);
                btnCancel.TabIndex = 3;
                btnCancel.Text = "İptal";
                btnCancel.UseVisualStyleBackColor = true;
                // 
                // btnOK
                // 
                btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                btnOK.Location = new System.Drawing.Point(98, 72);
                btnOK.Name = "btnOK";
                btnOK.Size = new System.Drawing.Size(75, 23);
                btnOK.TabIndex = 2;
                btnOK.Text = "Tamam";
                btnOK.UseVisualStyleBackColor = true;
                // 
                // groupBox
                // 
                groupBox.Controls.Add(lblDiameter);
                groupBox.Controls.Add(cbDiameter);
                groupBox.Location = new System.Drawing.Point(12, 8);
                groupBox.Name = "groupBox";
                groupBox.Size = new System.Drawing.Size(242, 58);
                groupBox.TabIndex = 0;
                groupBox.TabStop = false;
                // 
                // frmEdit
                // 
                frmEdit.AcceptButton = btnOK;
                frmEdit.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                frmEdit.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                frmEdit.CancelButton = btnCancel;
                frmEdit.ClientSize = new System.Drawing.Size(265, 110);
                frmEdit.Controls.Add(groupBox);
                frmEdit.Controls.Add(btnCancel);
                frmEdit.Controls.Add(btnOK);
                frmEdit.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                frmEdit.StartPosition = FormStartPosition.CenterParent;
                frmEdit.MaximizeBox = false;
                frmEdit.MinimizeBox = false;
                frmEdit.Name = "frmEdit";
                frmEdit.Text = "Çap Seçimi";

                groupBox.ResumeLayout(false);
                groupBox.PerformLayout();
                frmEdit.ResumeLayout(false);

                // Show dialog
                foreach (string diameter in Diameters)
                    cbDiameter.Items.Add(diameter);
                cbDiameter.SelectedIndex = 0;

                if (frmEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string diameter = (string)cbDiameter.SelectedItem;

                    Database db = HostApplicationServices.WorkingDatabase;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        try
                        {
                            foreach (ObjectId id in Items)
                            {
                                RebarPos pos = tr.GetObject(id, OpenMode.ForWrite) as RebarPos;
                                if (pos == null) continue;

                                pos.Diameter = diameter;
                            }
                            tr.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    return true;
                }

                return false;
            }
        }
    }
    #endregion

    #region PieceLengthCheck
    public class PieceLengthCheck : PosCheckResult
    {
        #region ShapeDefiniton
        public class ShapeDefiniton
        {
            public string Shape;
            public int FieldCount;
            public string A;
            public string B;
            public string C;
            public string D;
            public string E;
            public string F;

            public ShapeDefiniton(string shape, string a, string b, string c, string d, string e, string f)
            {
                Shape = shape;

                if (string.IsNullOrEmpty(a))
                    FieldCount = 0;
                else if (string.IsNullOrEmpty(b))
                    FieldCount = 1;
                else if (string.IsNullOrEmpty(c))
                    FieldCount = 2;
                else if (string.IsNullOrEmpty(d))
                    FieldCount = 3;
                else if (string.IsNullOrEmpty(e))
                    FieldCount = 4;
                else if (string.IsNullOrEmpty(f))
                    FieldCount = 5;
                else
                    FieldCount = 6;

                A = a; B = b; C = c; D = d; E = e; F = f;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                sb.Append(Shape);
                sb.Append("] ");
                if (FieldCount > 0)
                {
                    sb.Append(A);
                }
                if (FieldCount > 1)
                {
                    sb.Append(", ");
                    sb.Append(B);
                }
                if (FieldCount > 2)
                {
                    sb.Append(", ");
                    sb.Append(C);
                }
                if (FieldCount > 3)
                {
                    sb.Append(", ");
                    sb.Append(D);
                }
                if (FieldCount > 4)
                {
                    sb.Append(", ");
                    sb.Append(E);
                }
                if (FieldCount > 5)
                {
                    sb.Append(", ");
                    sb.Append(F);
                }
                return sb.ToString();
            }

            public override bool Equals(object obj)
            {
                if (object.ReferenceEquals(this, obj)) return true;
                if (!(obj is ShapeDefiniton)) return false;
                ShapeDefiniton o = (ShapeDefiniton)obj;
                if (string.Compare(Shape, o.Shape, StringComparison.OrdinalIgnoreCase) != 0) return false;
                if (string.Compare(A, o.A, StringComparison.OrdinalIgnoreCase) != 0) return false;
                if (string.Compare(B, o.B, StringComparison.OrdinalIgnoreCase) != 0) return false;
                if (string.Compare(C, o.C, StringComparison.OrdinalIgnoreCase) != 0) return false;
                if (string.Compare(D, o.D, StringComparison.OrdinalIgnoreCase) != 0) return false;
                if (string.Compare(E, o.E, StringComparison.OrdinalIgnoreCase) != 0) return false;
                if (string.Compare(F, o.F, StringComparison.OrdinalIgnoreCase) != 0) return false;

                return true;
            }

            public override int GetHashCode()
            {
                int hash = 13;
                hash = (hash * 7) + Shape.GetHashCode();
                hash = (hash * 7) + A.GetHashCode();
                hash = (hash * 7) + B.GetHashCode();
                hash = (hash * 7) + C.GetHashCode();
                hash = (hash * 7) + D.GetHashCode();
                hash = (hash * 7) + E.GetHashCode();
                hash = (hash * 7) + F.GetHashCode();
                return hash;
            }
        }
        #endregion

        public override string Key { get { return "length"; } }
        public override string Description { get { return "Açılım Hatası"; } }

        public List<ShapeDefiniton> Shapes { get; private set; }

        public override string ErrorMessage
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Shapes.Count; i++)
                {
                    sb.Append(Shapes[i].ToString());
                    if (i != Shapes.Count - 1) sb.Append(" / ");
                }
                return sb.ToString();
            }
        }

        public PieceLengthCheck(string pos)
            : base(pos)
        {
            Shapes = new List<ShapeDefiniton>();
        }

        public override bool Fix()
        {
            SelectShapeForm frmEdit = new SelectShapeForm();
            List<SelectShapeForm.ShapeDisplay> list = new List<SelectShapeForm.ShapeDisplay>();
            foreach (ShapeDefiniton def in Shapes)
                list.Add(new SelectShapeForm.ShapeDisplay(def.Shape, def.A, def.B, def.C, def.D, def.E, def.F));
            frmEdit.SetShapes(Shapes[0].Shape, list);

            if (frmEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShapeDefiniton shape = Shapes.Find(p => p.Shape == frmEdit.Current);
                if (shape == null) return false;

                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        foreach (ObjectId id in Items)
                        {
                            RebarPos pos = tr.GetObject(id, OpenMode.ForWrite) as RebarPos;
                            if (pos == null) continue;

                            pos.Shape = shape.Shape;
                            pos.A = shape.FieldCount > 0 ? shape.A : "";
                            pos.B = shape.FieldCount > 1 ? shape.B : "";
                            pos.C = shape.FieldCount > 2 ? shape.C : "";
                            pos.D = shape.FieldCount > 3 ? shape.D : "";
                            pos.E = shape.FieldCount > 4 ? shape.E : "";
                            pos.F = shape.FieldCount > 5 ? shape.F : "";
                        }
                        tr.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return true;
            }

            return false;
        }
    }
    #endregion

    #region StandardDiameterCheck
    public class StandardDiameterCheck : PosCheckResult
    {
        public override string Key { get { return "standarddiameter"; } }
        public override string Description { get { return "Standart Olmayan Çap Hatası"; } }

        public string CurrentDiameter { get; private set; }
        public List<int> Diameters { get; private set; }

        public override string ErrorMessage
        {
            get
            {
                return CurrentDiameter;
            }
        }

        public StandardDiameterCheck(string pos, string currentDiameter, IEnumerable<int> diameters)
            : base(pos)
        {
            CurrentDiameter = currentDiameter;
            Diameters = new List<int>();
            foreach (int dia in diameters)
                Diameters.Add(dia);
        }

        public override bool Fix()
        {
            using (System.Windows.Forms.Form frmEdit = new System.Windows.Forms.Form())
            {
                System.Windows.Forms.Label lblDiameter;
                System.Windows.Forms.ComboBox cbDiameter;
                System.Windows.Forms.Button btnCancel;
                System.Windows.Forms.Button btnOK;
                System.Windows.Forms.GroupBox groupBox;

                lblDiameter = new System.Windows.Forms.Label();
                cbDiameter = new System.Windows.Forms.ComboBox();
                btnCancel = new System.Windows.Forms.Button();
                btnOK = new System.Windows.Forms.Button();
                groupBox = new System.Windows.Forms.GroupBox();

                groupBox.SuspendLayout();
                frmEdit.SuspendLayout();
                // 
                // lblDiameter
                // 
                lblDiameter.AutoSize = true;
                lblDiameter.Location = new System.Drawing.Point(17, 22);
                lblDiameter.Name = "lblDiameter";
                lblDiameter.Size = new System.Drawing.Size(50, 13);
                lblDiameter.TabIndex = 0;
                lblDiameter.Text = "Yeni Çap";
                // 
                // comboBox1
                // 
                cbDiameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                cbDiameter.FormattingEnabled = true;
                cbDiameter.Location = new System.Drawing.Point(105, 19);
                cbDiameter.Name = "cbDiameter";
                cbDiameter.Size = new System.Drawing.Size(121, 21);
                cbDiameter.TabIndex = 1;
                // 
                // btnCancel
                // 
                btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                btnCancel.Location = new System.Drawing.Point(179, 72);
                btnCancel.Name = "btnCancel";
                btnCancel.Size = new System.Drawing.Size(75, 23);
                btnCancel.TabIndex = 3;
                btnCancel.Text = "İptal";
                btnCancel.UseVisualStyleBackColor = true;
                // 
                // btnOK
                // 
                btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                btnOK.Location = new System.Drawing.Point(98, 72);
                btnOK.Name = "btnOK";
                btnOK.Size = new System.Drawing.Size(75, 23);
                btnOK.TabIndex = 2;
                btnOK.Text = "Tamam";
                btnOK.UseVisualStyleBackColor = true;
                // 
                // groupBox
                // 
                groupBox.Controls.Add(lblDiameter);
                groupBox.Controls.Add(cbDiameter);
                groupBox.Location = new System.Drawing.Point(12, 8);
                groupBox.Name = "groupBox";
                groupBox.Size = new System.Drawing.Size(242, 58);
                groupBox.TabIndex = 0;
                groupBox.TabStop = false;
                // 
                // frmEdit
                // 
                frmEdit.AcceptButton = btnOK;
                frmEdit.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                frmEdit.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                frmEdit.CancelButton = btnCancel;
                frmEdit.ClientSize = new System.Drawing.Size(265, 110);
                frmEdit.Controls.Add(groupBox);
                frmEdit.Controls.Add(btnCancel);
                frmEdit.Controls.Add(btnOK);
                frmEdit.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                frmEdit.StartPosition = FormStartPosition.CenterParent;
                frmEdit.MaximizeBox = false;
                frmEdit.MinimizeBox = false;
                frmEdit.Name = "frmEdit";
                frmEdit.Text = "Çap Seçimi";

                groupBox.ResumeLayout(false);
                groupBox.PerformLayout();
                frmEdit.ResumeLayout(false);

                // Show dialog
                foreach (int diameter in Diameters)
                    cbDiameter.Items.Add(diameter);
                cbDiameter.SelectedIndex = 0;

                if (frmEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int diameter = (int)cbDiameter.SelectedItem;

                    Database db = HostApplicationServices.WorkingDatabase;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        try
                        {
                            foreach (ObjectId id in Items)
                            {
                                RebarPos pos = tr.GetObject(id, OpenMode.ForWrite) as RebarPos;
                                if (pos == null) continue;

                                pos.Diameter = diameter.ToString();
                            }
                            tr.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    return true;
                }

                return false;
            }
        }
    }
    #endregion

    #region MaximumLengthCheck
    public class MaximumLengthCheck : PosCheckResult
    {
        public override string Key { get { return "maxlength"; } }
        public override string Description { get { return "Maksimum Boy Hatası"; } }

        public double Length1 { get; private set; }
        public double Length2 { get; private set; }
        public bool IsVarLength { get; private set; }
        public double MaxLength { get; private set; }
        public int FieldCount { get; private set; }
        public string A { get; private set; }
        public string B { get; private set; }
        public string C { get; private set; }
        public string D { get; private set; }
        public string E { get; private set; }
        public string F { get; private set; }

        public override string ErrorMessage
        {
            get
            {
                if (IsVarLength)
                    return "(" + Length1.ToString("F2") + " m ~ " + Length2.ToString("F2") + " m) > " + MaxLength.ToString("F2") + " m";
                else
                    return Length1.ToString("F2") + " m > " + MaxLength.ToString("F2") + " m";
            }
        }

        public MaximumLengthCheck(string pos, double length1, double length2, bool isVarLength, double maxlength, string a, string b, string c, string d, string e, string f)
            : base(pos)
        {
            Length1 = length1;
            Length2 = length2;
            IsVarLength = isVarLength;
            MaxLength = maxlength;

            if (string.IsNullOrEmpty(a))
                FieldCount = 0;
            else if (string.IsNullOrEmpty(b))
                FieldCount = 1;
            else if (string.IsNullOrEmpty(c))
                FieldCount = 2;
            else if (string.IsNullOrEmpty(d))
                FieldCount = 3;
            else if (string.IsNullOrEmpty(e))
                FieldCount = 4;
            else if (string.IsNullOrEmpty(f))
                FieldCount = 5;
            else
                FieldCount = 6;

            A = a; B = b; C = c; D = d; E = e; F = f;
        }

        public override bool Fix()
        {
            using (System.Windows.Forms.Form frmEdit = new System.Windows.Forms.Form())
            {
                System.Windows.Forms.Button btnCancel;
                System.Windows.Forms.Button btnOK;
                System.Windows.Forms.GroupBox groupBox;
                System.Windows.Forms.TextBox txtF;
                System.Windows.Forms.Label label10;
                System.Windows.Forms.TextBox txtE;
                System.Windows.Forms.Label label9;
                System.Windows.Forms.TextBox txtD;
                System.Windows.Forms.Label label8;
                System.Windows.Forms.TextBox txtC;
                System.Windows.Forms.Label label7;
                System.Windows.Forms.TextBox txtB;
                System.Windows.Forms.Label label6;
                System.Windows.Forms.TextBox txtA;
                System.Windows.Forms.Label label5;

                btnCancel = new System.Windows.Forms.Button();
                btnOK = new System.Windows.Forms.Button();
                groupBox = new System.Windows.Forms.GroupBox();
                txtF = new System.Windows.Forms.TextBox();
                label10 = new System.Windows.Forms.Label();
                txtE = new System.Windows.Forms.TextBox();
                label9 = new System.Windows.Forms.Label();
                txtD = new System.Windows.Forms.TextBox();
                label8 = new System.Windows.Forms.Label();
                txtC = new System.Windows.Forms.TextBox();
                label7 = new System.Windows.Forms.Label();
                txtB = new System.Windows.Forms.TextBox();
                label6 = new System.Windows.Forms.Label();
                txtA = new System.Windows.Forms.TextBox();
                label5 = new System.Windows.Forms.Label();

                groupBox.SuspendLayout();
                frmEdit.SuspendLayout();
                // 
                // btnCancel
                // 
                btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                btnCancel.Location = new System.Drawing.Point(227, 128);
                btnCancel.Name = "btnCancel";
                btnCancel.Size = new System.Drawing.Size(75, 23);
                btnCancel.TabIndex = 2;
                btnCancel.Text = "İptal";
                btnCancel.UseVisualStyleBackColor = true;
                // 
                // btnOK
                // 
                btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                btnOK.Location = new System.Drawing.Point(146, 128);
                btnOK.Name = "btnOK";
                btnOK.Size = new System.Drawing.Size(75, 23);
                btnOK.TabIndex = 1;
                btnOK.Text = "Tamam";
                btnOK.UseVisualStyleBackColor = true;
                // 
                // groupBox
                // 
                groupBox.Controls.Add(txtF);
                groupBox.Controls.Add(label10);
                groupBox.Controls.Add(txtE);
                groupBox.Controls.Add(label9);
                groupBox.Controls.Add(txtD);
                groupBox.Controls.Add(label8);
                groupBox.Controls.Add(txtC);
                groupBox.Controls.Add(label7);
                groupBox.Controls.Add(txtB);
                groupBox.Controls.Add(label6);
                groupBox.Controls.Add(txtA);
                groupBox.Controls.Add(label5);
                groupBox.Location = new System.Drawing.Point(12, 8);
                groupBox.Name = "groupBox";
                groupBox.Size = new System.Drawing.Size(290, 114);
                groupBox.TabIndex = 0;
                groupBox.TabStop = false;
                // 
                // txtF
                // 
                txtF.Location = new System.Drawing.Point(193, 71);
                txtF.Name = "txtF";
                txtF.Size = new System.Drawing.Size(70, 20);
                txtF.TabIndex = 11;
                // 
                // label10
                // 
                label10.AutoSize = true;
                label10.Location = new System.Drawing.Point(160, 74);
                label10.Name = "label10";
                label10.Size = new System.Drawing.Size(13, 13);
                label10.TabIndex = 10;
                label10.Text = "&F";
                // 
                // txtE
                // 
                txtE.Location = new System.Drawing.Point(193, 45);
                txtE.Name = "txtE";
                txtE.Size = new System.Drawing.Size(70, 20);
                txtE.TabIndex = 9;
                // 
                // label9
                // 
                label9.AutoSize = true;
                label9.Location = new System.Drawing.Point(160, 48);
                label9.Name = "label9";
                label9.Size = new System.Drawing.Size(14, 13);
                label9.TabIndex = 8;
                label9.Text = "&E";
                // 
                // txtD
                // 
                txtD.Location = new System.Drawing.Point(193, 19);
                txtD.Name = "txtD";
                txtD.Size = new System.Drawing.Size(70, 20);
                txtD.TabIndex = 7;
                // 
                // label8
                // 
                label8.AutoSize = true;
                label8.Location = new System.Drawing.Point(160, 22);
                label8.Name = "label8";
                label8.Size = new System.Drawing.Size(15, 13);
                label8.TabIndex = 6;
                label8.Text = "&D";
                // 
                // txtC
                // 
                txtC.Location = new System.Drawing.Point(57, 71);
                txtC.Name = "txtC";
                txtC.Size = new System.Drawing.Size(70, 20);
                txtC.TabIndex = 5;
                // 
                // label7
                // 
                label7.AutoSize = true;
                label7.Location = new System.Drawing.Point(24, 74);
                label7.Name = "label7";
                label7.Size = new System.Drawing.Size(14, 13);
                label7.TabIndex = 4;
                label7.Text = "&C";
                // 
                // txtB
                // 
                txtB.Location = new System.Drawing.Point(57, 45);
                txtB.Name = "txtB";
                txtB.Size = new System.Drawing.Size(70, 20);
                txtB.TabIndex = 3;
                // 
                // label6
                // 
                label6.AutoSize = true;
                label6.Location = new System.Drawing.Point(24, 48);
                label6.Name = "label6";
                label6.Size = new System.Drawing.Size(14, 13);
                label6.TabIndex = 2;
                label6.Text = "&B";
                // 
                // txtA
                // 
                txtA.Location = new System.Drawing.Point(57, 19);
                txtA.Name = "txtA";
                txtA.Size = new System.Drawing.Size(70, 20);
                txtA.TabIndex = 1;
                // 
                // label5
                // 
                label5.AutoSize = true;
                label5.Location = new System.Drawing.Point(24, 22);
                label5.Name = "label5";
                label5.Size = new System.Drawing.Size(14, 13);
                label5.TabIndex = 0;
                label5.Text = "&A";
                // 
                // frmEdit
                // 
                frmEdit.AcceptButton = btnOK;
                frmEdit.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                frmEdit.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                frmEdit.CancelButton = btnCancel;
                frmEdit.ClientSize = new System.Drawing.Size(315, 162);
                frmEdit.Controls.Add(groupBox);
                frmEdit.Controls.Add(btnCancel);
                frmEdit.Controls.Add(btnOK);
                frmEdit.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                frmEdit.MaximizeBox = false;
                frmEdit.MinimizeBox = false;
                frmEdit.Name = "frmEdit";
                frmEdit.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                frmEdit.Text = "Çap Seçimi";

                groupBox.ResumeLayout(false);
                groupBox.PerformLayout();
                frmEdit.ResumeLayout(false);

                // Show dialog
                txtA.Text = A;
                txtB.Text = B;
                txtC.Text = C;
                txtD.Text = D;
                txtE.Text = E;
                txtF.Text = F;
                txtA.Enabled = (FieldCount > 0);
                txtB.Enabled = (FieldCount > 1);
                txtC.Enabled = (FieldCount > 2);
                txtD.Enabled = (FieldCount > 3);
                txtE.Enabled = (FieldCount > 4);
                txtF.Enabled = (FieldCount > 5);

                if (frmEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Database db = HostApplicationServices.WorkingDatabase;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        try
                        {
                            foreach (ObjectId id in Items)
                            {
                                RebarPos pos = tr.GetObject(id, OpenMode.ForWrite) as RebarPos;
                                if (pos == null) continue;

                                pos.A = txtA.Text;
                                pos.B = txtB.Text;
                                pos.C = txtC.Text;
                                pos.D = txtD.Text;
                                pos.E = txtE.Text;
                                pos.F = txtF.Text;
                            }
                            tr.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    return true;
                }

                return false;
            }
        }
    }
    #endregion

    #region SamePosKeyCheck
    public class SamePosKeyCheck : PosCheckResult
    {
        private class CompareByPos : IComparer<string>
        {
            public int Compare(string e1, string e2)
            {
                int p1 = 0;
                int p2 = 0;
                int.TryParse(e1, out p1);
                int.TryParse(e2, out p2);

                return (p1 == p2 ? 0 : (p1 < p2 ? -1 : 1));
            }
        }

        public override string Key { get { return "sameposkey"; } }
        public override string Description { get { return "Poz Çapı ve Açılımı Aynı"; } }

        public List<string> OtherPos { get; private set; }

        public override string ErrorMessage
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Pos);
                sb.Append(" = ");
                for (int i = 0; i < OtherPos.Count; i++)
                {
                    sb.Append(OtherPos[i]);
                    if (i != OtherPos.Count - 1) sb.Append(" : ");
                }
                return sb.ToString();
            }
        }

        public SamePosKeyCheck(IEnumerable<string> otherPos)
            : base()
        {
            OtherPos = new List<string>();
            foreach (string p in otherPos)
                OtherPos.Add(p);
            OtherPos.Sort(new CompareByPos());
            Pos = OtherPos[0];
            OtherPos.RemoveAt(0);
        }

        public override bool Fix()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OtherPos.Count; i++)
            {
                sb.Append(OtherPos[i]);
                if (i != OtherPos.Count - 1) sb.Append(", ");
            }
            sb.Append(" nolu pozların numaraları ");
            sb.Append(Pos);
            sb.Append(" olarak değiştirilecek.");
            sb.AppendLine();
            sb.AppendLine("Devam etmek istiyor musunuz?");
            if (MessageBox.Show(sb.ToString(), "RebarPos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        foreach (ObjectId id in Items)
                        {
                            RebarPos pos = tr.GetObject(id, OpenMode.ForWrite) as RebarPos;
                            if (pos == null) continue;

                            pos.Pos = Pos;
                        }
                        tr.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return true;
            }

            return false;
        }
    }
    #endregion

    public abstract class PosCheckResult
    {
        public string Pos { get; protected set; }
        public List<ObjectId> Items { get; private set; }
        public abstract string ErrorMessage { get; }
        public abstract string Key { get; }
        public abstract string Description { get; }

        public abstract bool Fix();

        public PosCheckResult()
            : this(string.Empty)
        {
            ;
        }

        public PosCheckResult(string pos)
        {
            Pos = pos;
            Items = new List<ObjectId>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n");
            sb.Append(Pos);
            sb.Append(" Pozu ");
            sb.Append(Description);
            sb.Append(": ");
            sb.Append(ErrorMessage);
            sb.Append(" (");
            sb.Append(Items.Count);
            sb.Append(" adet poz)");

            return sb.ToString();
        }

        public static List<PosCheckResult> CheckAllInSelection(IEnumerable<ObjectId> items, bool checkErrors, bool checkWarnings)
        {
            List<PosCheckResult> results = new List<PosCheckResult>();

            // Read all pos objects
            List<PosCopy> pliste = PosCopy.ReadAllInSelection(items, PosCopy.PosGrouping.None);

            if (checkErrors)
            {
                // Read standard diameter list
                List<int> standardDiameters = DWGUtility.GetStandardDiameters();

                // Maximum bar length
                double maxLength = DWGUtility.GetMaximumBarLength();

                // Check if standard diameter
                Dictionary<string, List<PosCopy>> sdcheck = new Dictionary<string, List<PosCopy>>();
                foreach (PosCopy x in pliste)
                {
                    int dia = 0;
                    if (!int.TryParse(x.diameter, out dia) || !standardDiameters.Contains(dia))
                    {
                        string key = x.pos + dia.ToString();
                        if (sdcheck.ContainsKey(key))
                            sdcheck[key].Add(x);
                        else
                            sdcheck[key] = new List<PosCopy>() { x };
                    }
                }
                foreach (List<PosCopy> list in sdcheck.Values)
                {
                    StandardDiameterCheck check = new StandardDiameterCheck(list[0].pos, list[0].diameter, standardDiameters);
                    foreach (PosCopy copy in list)
                        check.Items.Add(copy.list[0]);
                    results.Add(check);
                }

                // Check maximum bar length
                Dictionary<string, List<PosCopy>> maxcheck = new Dictionary<string, List<PosCopy>>();
                foreach (PosCopy copy in pliste)
                {
                    if (copy.length1 / 1000.0 > maxLength || copy.length2 / 1000.0 > maxLength)
                    {
                        string key = copy.pos + copy.key;
                        if (maxcheck.ContainsKey(key))
                            maxcheck[key].Add(copy);
                        else
                            maxcheck[key] = new List<PosCopy>() { copy };
                    }
                }
                foreach (List<PosCopy> list in maxcheck.Values)
                {
                    MaximumLengthCheck check = new MaximumLengthCheck(list[0].pos, list[0].length1 / 1000.0, list[0].length2 / 1000.0, list[0].isVarLength, maxLength, list[0].a, list[0].b, list[0].c, list[0].d, list[0].e, list[0].f);
                    foreach (PosCopy copy in list)
                        check.Items.Add(copy.list[0]);
                    results.Add(check);
                }

                // Group by pos
                Dictionary<string, List<PosCopy>> poscheck = new Dictionary<string, List<PosCopy>>();
                foreach (PosCopy copy in pliste)
                {
                    string key = copy.pos;
                    if (poscheck.ContainsKey(key))
                        poscheck[key].Add(copy);
                    else
                        poscheck[key] = new List<PosCopy>() { copy };
                }

                foreach (List<PosCopy> list in poscheck.Values)
                {
                    // Check if diameters different in same pos
                    Dictionary<string, List<PosCopy>> dcheck = new Dictionary<string, List<PosCopy>>();
                    foreach (PosCopy copy in list)
                    {
                        if (dcheck.ContainsKey(copy.diameter))
                            dcheck[copy.diameter].Add(copy);
                        else
                            dcheck[copy.diameter] = new List<PosCopy>() { copy };
                    }

                    if (dcheck.Count > 1)
                    {
                        DiameterCheck check = new DiameterCheck(list[0].pos, dcheck.Keys);
                        foreach (List<PosCopy> checklist in dcheck.Values)
                        {
                            foreach (PosCopy copy in checklist)
                                check.Items.Add(copy.list[0]);
                        }
                        results.Add(check);
                    }

                    // Check if shapes and piece lengths different in same pos
                    Dictionary<string, List<PosCopy>> lcheck = new Dictionary<string, List<PosCopy>>();
                    foreach (PosCopy copy in list)
                    {
                        string key = copy.shapename + copy.a + copy.b + copy.c + copy.d + copy.e + copy.f;
                        if (lcheck.ContainsKey(key))
                            lcheck[key].Add(copy);
                        else
                            lcheck[key] = new List<PosCopy>() { copy };
                    }

                    if (lcheck.Count > 1)
                    {
                        PieceLengthCheck check = new PieceLengthCheck(list[0].pos);
                        foreach (List<PosCopy> checklist in lcheck.Values)
                        {
                            foreach (PosCopy copy in checklist)
                            {
                                PieceLengthCheck.ShapeDefiniton shape = new PieceLengthCheck.ShapeDefiniton(copy.shapename, copy.a, copy.b, copy.c, copy.d, copy.e, copy.f);
                                if (!check.Shapes.Contains(shape))
                                    check.Shapes.Add(shape);
                            }

                            foreach (PosCopy copy in checklist)
                                check.Items.Add(copy.list[0]);
                        }
                        results.Add(check);
                    }
                }
            }

            if (checkWarnings)
            {
                // Group by pos key
                Dictionary<string, List<PosCopy>> poscheck = new Dictionary<string, List<PosCopy>>();
                foreach (PosCopy copy in pliste)
                {
                    if (poscheck.ContainsKey(copy.key))
                        poscheck[copy.key].Add(copy);
                    else
                        poscheck[copy.key] = new List<PosCopy>() { copy };
                }

                foreach (List<PosCopy> list in poscheck.Values)
                {
                    // Check if pos numbers are different with same pos key
                    Dictionary<string, List<PosCopy>> scheck = new Dictionary<string, List<PosCopy>>();
                    foreach (PosCopy copy in list)
                    {
                        if (scheck.ContainsKey(copy.pos))
                            scheck[copy.pos].Add(copy);
                        else
                            scheck[copy.pos] = new List<PosCopy>() { copy };
                    }

                    if (scheck.Count > 1)
                    {
                        SamePosKeyCheck check = new SamePosKeyCheck(scheck.Keys);
                        foreach (List<PosCopy> checklist in scheck.Values)
                        {
                            foreach (PosCopy copy in checklist)
                            {
                                check.Items.Add(copy.list[0]);
                            }
                        }
                        results.Add(check);
                    }
                }
            }

            results.Sort(new CompareByPosNumber());

            return results;
        }

        private class CompareByPosNumber : IComparer<PosCheckResult>
        {
            public int Compare(PosCheckResult e1, PosCheckResult e2)
            {
                int p1 = 0;
                int p2 = 0;
                int.TryParse(e1.Pos, out p1);
                int.TryParse(e2.Pos, out p2);

                return (p1 == p2 ? 0 : (p1 < p2 ? -1 : 1));
            }
        }

        public static void ConsoleOut(List<PosCheckResult> list)
        {
            if (list.Count != 0)
            {
                Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\nPozlandirmada asagidaki hatalar bulundu:");
                ed.WriteMessage("\n----------------------------------------");
                StringBuilder sb = new StringBuilder();
                foreach (PosCheckResult result in list)
                {
                    sb.Append(result.ToString());
                }
                ed.WriteMessage(sb.ToString());
            }
        }
    }
}
