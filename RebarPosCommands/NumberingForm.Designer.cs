namespace RebarPosCommands
{
    partial class NumberingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbItems = new System.Windows.Forms.ListView();
            this.chMarker = new System.Windows.Forms.ColumnHeader();
            this.chNewMarker = new System.Windows.Forms.ColumnHeader();
            this.chPriority = new System.Windows.Forms.ColumnHeader();
            this.chDiameter = new System.Windows.Forms.ColumnHeader();
            this.chShape = new System.Windows.Forms.ColumnHeader();
            this.chLength = new System.Windows.Forms.ColumnHeader();
            this.chA = new System.Windows.Forms.ColumnHeader();
            this.chB = new System.Windows.Forms.ColumnHeader();
            this.chC = new System.Windows.Forms.ColumnHeader();
            this.chD = new System.Windows.Forms.ColumnHeader();
            this.chE = new System.Windows.Forms.ColumnHeader();
            this.chF = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbOrder5 = new System.Windows.Forms.ComboBox();
            this.cbOrder4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbOrder3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOrder2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOrder1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAutoNumber = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartNum = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnApplyNumber = new System.Windows.Forms.Button();
            this.btnDeletePos = new System.Windows.Forms.Button();
            this.btnAddPos = new System.Windows.Forms.Button();
            this.btnIncrementNumber = new System.Windows.Forms.Button();
            this.btnDecrementNumber = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbItems
            // 
            this.lbItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMarker,
            this.chNewMarker,
            this.chPriority,
            this.chDiameter,
            this.chShape,
            this.chLength,
            this.chA,
            this.chB,
            this.chC,
            this.chD,
            this.chE,
            this.chF});
            this.lbItems.Location = new System.Drawing.Point(13, 57);
            this.lbItems.MultiSelect = false;
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(559, 382);
            this.lbItems.TabIndex = 2;
            this.lbItems.UseCompatibleStateImageBehavior = false;
            this.lbItems.View = System.Windows.Forms.View.Details;
            // 
            // chMarker
            // 
            this.chMarker.Text = "No.";
            // 
            // chNewMarker
            // 
            this.chNewMarker.Text = "Yeni No.";
            // 
            // chPriority
            // 
            this.chPriority.Text = "Öncelik";
            // 
            // chDiameter
            // 
            this.chDiameter.Text = "Çap";
            this.chDiameter.Width = 37;
            // 
            // chShape
            // 
            this.chShape.Text = "Şekil";
            this.chShape.Width = 70;
            // 
            // chLength
            // 
            this.chLength.Text = "Toplam Boy";
            this.chLength.Width = 79;
            // 
            // chA
            // 
            this.chA.Text = "A";
            this.chA.Width = 25;
            // 
            // chB
            // 
            this.chB.Text = "B";
            this.chB.Width = 25;
            // 
            // chC
            // 
            this.chC.Text = "C";
            this.chC.Width = 25;
            // 
            // chD
            // 
            this.chD.Text = "D";
            this.chD.Width = 25;
            // 
            // chE
            // 
            this.chE.Text = "E";
            this.chE.Width = 25;
            // 
            // chF
            // 
            this.chF.Text = "F";
            this.chF.Width = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbOrder5);
            this.groupBox1.Controls.Add(this.cbOrder4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbOrder3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbOrder2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbOrder1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnAutoNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtStartNum);
            this.groupBox1.Location = new System.Drawing.Point(590, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 270);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Otomatik Numaralandırma";
            // 
            // cbOrder5
            // 
            this.cbOrder5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder5.FormattingEnabled = true;
            this.cbOrder5.Items.AddRange(new object[] {
            "Çizimdeki Yer",
            "Öncelik",
            "Şekil",
            "Çap",
            "Toplam Boy",
            ""});
            this.cbOrder5.Location = new System.Drawing.Point(135, 137);
            this.cbOrder5.Name = "cbOrder5";
            this.cbOrder5.Size = new System.Drawing.Size(100, 21);
            this.cbOrder5.TabIndex = 9;
            // 
            // cbOrder4
            // 
            this.cbOrder4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder4.FormattingEnabled = true;
            this.cbOrder4.Items.AddRange(new object[] {
            "Çizimdeki Yer",
            "Öncelik",
            "Şekil",
            "Çap",
            "Toplam Boy",
            ""});
            this.cbOrder4.Location = new System.Drawing.Point(135, 110);
            this.cbOrder4.Name = "cbOrder4";
            this.cbOrder4.Size = new System.Drawing.Size(100, 21);
            this.cbOrder4.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "&5. Sıralama Kriteri";
            // 
            // cbOrder3
            // 
            this.cbOrder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder3.FormattingEnabled = true;
            this.cbOrder3.Items.AddRange(new object[] {
            "Çizimdeki Yer",
            "Öncelik",
            "Şekil",
            "Çap",
            "Toplam Boy",
            ""});
            this.cbOrder3.Location = new System.Drawing.Point(135, 83);
            this.cbOrder3.Name = "cbOrder3";
            this.cbOrder3.Size = new System.Drawing.Size(100, 21);
            this.cbOrder3.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "&4. Sıralama Kriteri";
            // 
            // cbOrder2
            // 
            this.cbOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder2.FormattingEnabled = true;
            this.cbOrder2.Items.AddRange(new object[] {
            "Çizimdeki Yer",
            "Öncelik",
            "Şekil",
            "Çap",
            "Toplam Boy",
            ""});
            this.cbOrder2.Location = new System.Drawing.Point(135, 56);
            this.cbOrder2.Name = "cbOrder2";
            this.cbOrder2.Size = new System.Drawing.Size(100, 21);
            this.cbOrder2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "&3. Sıralama Kriteri";
            // 
            // cbOrder1
            // 
            this.cbOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder1.FormattingEnabled = true;
            this.cbOrder1.Items.AddRange(new object[] {
            "Çizimdeki Yer",
            "Öncelik",
            "Şekil",
            "Çap",
            "Toplam Boy",
            ""});
            this.cbOrder1.Location = new System.Drawing.Point(135, 29);
            this.cbOrder1.Name = "cbOrder1";
            this.cbOrder1.Size = new System.Drawing.Size(100, 21);
            this.cbOrder1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "&2. Sıralama Kriteri";
            // 
            // btnAutoNumber
            // 
            this.btnAutoNumber.Location = new System.Drawing.Point(24, 230);
            this.btnAutoNumber.Name = "btnAutoNumber";
            this.btnAutoNumber.Size = new System.Drawing.Size(211, 23);
            this.btnAutoNumber.TabIndex = 12;
            this.btnAutoNumber.Text = "Otomatik &Numaralandır";
            this.btnAutoNumber.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "&1. Sıralama Kriteri";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "&Başlangıç Numarası";
            // 
            // txtStartNum
            // 
            this.txtStartNum.Location = new System.Drawing.Point(135, 185);
            this.txtStartNum.Name = "txtStartNum";
            this.txtStartNum.Size = new System.Drawing.Size(100, 20);
            this.txtStartNum.TabIndex = 11;
            this.txtStartNum.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnApplyNumber);
            this.groupBox2.Controls.Add(this.btnDeletePos);
            this.groupBox2.Controls.Add(this.btnAddPos);
            this.groupBox2.Controls.Add(this.btnIncrementNumber);
            this.groupBox2.Controls.Add(this.btnDecrementNumber);
            this.groupBox2.Controls.Add(this.txtNumber);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(590, 290);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 149);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Elle Numaralandırma";
            // 
            // btnApplyNumber
            // 
            this.btnApplyNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyNumber.Image = global::RebarPosCommands.Properties.Resources.tick;
            this.btnApplyNumber.Location = new System.Drawing.Point(211, 99);
            this.btnApplyNumber.Name = "btnApplyNumber";
            this.btnApplyNumber.Size = new System.Drawing.Size(24, 24);
            this.btnApplyNumber.TabIndex = 6;
            this.btnApplyNumber.UseVisualStyleBackColor = true;
            // 
            // btnDeletePos
            // 
            this.btnDeletePos.Image = global::RebarPosCommands.Properties.Resources.table_row_delete;
            this.btnDeletePos.Location = new System.Drawing.Point(135, 61);
            this.btnDeletePos.Name = "btnDeletePos";
            this.btnDeletePos.Size = new System.Drawing.Size(100, 23);
            this.btnDeletePos.TabIndex = 3;
            this.btnDeletePos.Text = "Satır &Sil";
            this.btnDeletePos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeletePos.UseVisualStyleBackColor = true;
            // 
            // btnAddPos
            // 
            this.btnAddPos.Image = global::RebarPosCommands.Properties.Resources.table_row_insert;
            this.btnAddPos.Location = new System.Drawing.Point(24, 61);
            this.btnAddPos.Name = "btnAddPos";
            this.btnAddPos.Size = new System.Drawing.Size(100, 23);
            this.btnAddPos.TabIndex = 2;
            this.btnAddPos.Text = "Satır E&kle";
            this.btnAddPos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddPos.UseVisualStyleBackColor = true;
            // 
            // btnIncrementNumber
            // 
            this.btnIncrementNumber.Image = global::RebarPosCommands.Properties.Resources.arrow_down;
            this.btnIncrementNumber.Location = new System.Drawing.Point(135, 32);
            this.btnIncrementNumber.Name = "btnIncrementNumber";
            this.btnIncrementNumber.Size = new System.Drawing.Size(100, 23);
            this.btnIncrementNumber.TabIndex = 1;
            this.btnIncrementNumber.Text = "&Aşağı Taşı";
            this.btnIncrementNumber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIncrementNumber.UseVisualStyleBackColor = true;
            // 
            // btnDecrementNumber
            // 
            this.btnDecrementNumber.Image = global::RebarPosCommands.Properties.Resources.arrow_up;
            this.btnDecrementNumber.Location = new System.Drawing.Point(24, 32);
            this.btnDecrementNumber.Name = "btnDecrementNumber";
            this.btnDecrementNumber.Size = new System.Drawing.Size(100, 23);
            this.btnDecrementNumber.TabIndex = 0;
            this.btnDecrementNumber.Text = "Yukarı &Taşı";
            this.btnDecrementNumber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDecrementNumber.UseVisualStyleBackColor = true;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(82, 102);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(123, 20);
            this.txtNumber.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "&Numara";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(696, 458);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(777, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Numaralandırılacak Poz &Grupu";
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(168, 19);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(100, 21);
            this.cbGroup.TabIndex = 1;
            // 
            // NumberingForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(865, 496);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbItems);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbGroup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumberingForm";
            this.ShowInTaskbar = false;
            this.Text = "Numaralandırma";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lbItems;
        private System.Windows.Forms.ColumnHeader chMarker;
        private System.Windows.Forms.ColumnHeader chNewMarker;
        private System.Windows.Forms.ColumnHeader chDiameter;
        private System.Windows.Forms.ColumnHeader chShape;
        private System.Windows.Forms.ColumnHeader chA;
        private System.Windows.Forms.ColumnHeader chB;
        private System.Windows.Forms.ColumnHeader chC;
        private System.Windows.Forms.ColumnHeader chD;
        private System.Windows.Forms.ColumnHeader chE;
        private System.Windows.Forms.ColumnHeader chF;
        private System.Windows.Forms.ColumnHeader chPriority;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbOrder1;
        private System.Windows.Forms.Button btnAutoNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartNum;
        private System.Windows.Forms.ComboBox cbOrder5;
        private System.Windows.Forms.ComboBox cbOrder4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbOrder3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOrder2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeletePos;
        private System.Windows.Forms.Button btnAddPos;
        private System.Windows.Forms.Button btnIncrementNumber;
        private System.Windows.Forms.Button btnDecrementNumber;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnApplyNumber;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader chLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbGroup;
    }
}