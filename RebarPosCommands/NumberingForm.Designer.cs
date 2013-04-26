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
            this.chOldMarker = new System.Windows.Forms.ColumnHeader();
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
            this.gbAutoNumber = new System.Windows.Forms.GroupBox();
            this.rbNumberAll = new System.Windows.Forms.RadioButton();
            this.rbKeepExisting = new System.Windows.Forms.RadioButton();
            this.cbOrder4 = new System.Windows.Forms.ComboBox();
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
            this.rbNumberVarLength = new System.Windows.Forms.RadioButton();
            this.gbManualNumber = new System.Windows.Forms.GroupBox();
            this.btnApplyNumber = new System.Windows.Forms.Button();
            this.btnDeletePos = new System.Windows.Forms.Button();
            this.btnAddPos = new System.Windows.Forms.Button();
            this.btnIncrementNumber = new System.Windows.Forms.Button();
            this.btnDecrementNumber = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbGroupVarLength = new System.Windows.Forms.RadioButton();
            this.gbAutoNumber.SuspendLayout();
            this.gbManualNumber.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbItems
            // 
            this.lbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMarker,
            this.chOldMarker,
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
            this.lbItems.FullRowSelect = true;
            this.lbItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lbItems.HideSelection = false;
            this.lbItems.Location = new System.Drawing.Point(13, 59);
            this.lbItems.MultiSelect = false;
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(571, 423);
            this.lbItems.TabIndex = 1;
            this.lbItems.UseCompatibleStateImageBehavior = false;
            this.lbItems.View = System.Windows.Forms.View.Details;
            this.lbItems.SelectedIndexChanged += new System.EventHandler(this.lbItems_SelectedIndexChanged);
            // 
            // chMarker
            // 
            this.chMarker.Text = "No.";
            // 
            // chOldMarker
            // 
            this.chOldMarker.Text = "Eski No.";
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
            this.chA.Width = 30;
            // 
            // chB
            // 
            this.chB.Text = "B";
            this.chB.Width = 30;
            // 
            // chC
            // 
            this.chC.Text = "C";
            this.chC.Width = 30;
            // 
            // chD
            // 
            this.chD.Text = "D";
            this.chD.Width = 30;
            // 
            // chE
            // 
            this.chE.Text = "E";
            this.chE.Width = 30;
            // 
            // chF
            // 
            this.chF.Text = "F";
            this.chF.Width = 30;
            // 
            // gbAutoNumber
            // 
            this.gbAutoNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAutoNumber.Controls.Add(this.rbNumberAll);
            this.gbAutoNumber.Controls.Add(this.rbKeepExisting);
            this.gbAutoNumber.Controls.Add(this.cbOrder4);
            this.gbAutoNumber.Controls.Add(this.cbOrder3);
            this.gbAutoNumber.Controls.Add(this.label5);
            this.gbAutoNumber.Controls.Add(this.cbOrder2);
            this.gbAutoNumber.Controls.Add(this.label4);
            this.gbAutoNumber.Controls.Add(this.cbOrder1);
            this.gbAutoNumber.Controls.Add(this.label3);
            this.gbAutoNumber.Controls.Add(this.btnAutoNumber);
            this.gbAutoNumber.Controls.Add(this.label2);
            this.gbAutoNumber.Controls.Add(this.label1);
            this.gbAutoNumber.Controls.Add(this.txtStartNum);
            this.gbAutoNumber.Location = new System.Drawing.Point(602, 13);
            this.gbAutoNumber.Name = "gbAutoNumber";
            this.gbAutoNumber.Size = new System.Drawing.Size(262, 314);
            this.gbAutoNumber.TabIndex = 2;
            this.gbAutoNumber.TabStop = false;
            this.gbAutoNumber.Text = "&Otomatik Numaralandırma";
            // 
            // rbNumberAll
            // 
            this.rbNumberAll.AutoSize = true;
            this.rbNumberAll.Location = new System.Drawing.Point(21, 178);
            this.rbNumberAll.Name = "rbNumberAll";
            this.rbNumberAll.Size = new System.Drawing.Size(187, 17);
            this.rbNumberAll.TabIndex = 9;
            this.rbNumberAll.TabStop = true;
            this.rbNumberAll.Text = "&Tüm Pozları Yeniden Numaralandır";
            this.rbNumberAll.UseVisualStyleBackColor = true;
            this.rbNumberAll.CheckedChanged += new System.EventHandler(this.rbNumberAll_CheckedChanged);
            // 
            // rbKeepExisting
            // 
            this.rbKeepExisting.AutoSize = true;
            this.rbKeepExisting.Location = new System.Drawing.Point(21, 155);
            this.rbKeepExisting.Name = "rbKeepExisting";
            this.rbKeepExisting.Size = new System.Drawing.Size(139, 17);
            this.rbKeepExisting.TabIndex = 8;
            this.rbKeepExisting.TabStop = true;
            this.rbKeepExisting.Text = "&Mevcut Numaraları Koru";
            this.rbKeepExisting.UseVisualStyleBackColor = true;
            this.rbKeepExisting.CheckedChanged += new System.EventHandler(this.rbKeepExisting_CheckedChanged);
            // 
            // cbOrder4
            // 
            this.cbOrder4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder4.FormattingEnabled = true;
            this.cbOrder4.Items.AddRange(new object[] {
            "",
            "Çizimdeki Yer",
            "Şekil",
            "Çap",
            "Toplam Boy"});
            this.cbOrder4.Location = new System.Drawing.Point(135, 110);
            this.cbOrder4.Name = "cbOrder4";
            this.cbOrder4.Size = new System.Drawing.Size(100, 21);
            this.cbOrder4.TabIndex = 7;
            // 
            // cbOrder3
            // 
            this.cbOrder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder3.FormattingEnabled = true;
            this.cbOrder3.Items.AddRange(new object[] {
            "",
            "Çizimdeki Yer",
            "Şekil",
            "Çap",
            "Toplam Boy"});
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
            "",
            "Çizimdeki Yer",
            "Şekil",
            "Çap",
            "Toplam Boy"});
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
            "",
            "Çizimdeki Yer",
            "Şekil",
            "Çap",
            "Toplam Boy"});
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
            this.btnAutoNumber.Location = new System.Drawing.Point(25, 266);
            this.btnAutoNumber.Name = "btnAutoNumber";
            this.btnAutoNumber.Size = new System.Drawing.Size(211, 23);
            this.btnAutoNumber.TabIndex = 13;
            this.btnAutoNumber.Text = "Otomatik &Numaralandır";
            this.btnAutoNumber.UseVisualStyleBackColor = true;
            this.btnAutoNumber.Click += new System.EventHandler(this.btnAutoNumber_Click);
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
            this.label1.Location = new System.Drawing.Point(20, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "&Başlangıç Numarası";
            // 
            // txtStartNum
            // 
            this.txtStartNum.Location = new System.Drawing.Point(136, 223);
            this.txtStartNum.Name = "txtStartNum";
            this.txtStartNum.Size = new System.Drawing.Size(100, 20);
            this.txtStartNum.TabIndex = 12;
            this.txtStartNum.Text = "1";
            // 
            // rbNumberVarLength
            // 
            this.rbNumberVarLength.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbNumberVarLength.AutoSize = true;
            this.rbNumberVarLength.Checked = true;
            this.rbNumberVarLength.Image = global::RebarPosCommands.Properties.Resources.NumVarLength1;
            this.rbNumberVarLength.Location = new System.Drawing.Point(3, 3);
            this.rbNumberVarLength.Name = "rbNumberVarLength";
            this.rbNumberVarLength.Size = new System.Drawing.Size(250, 27);
            this.rbNumberVarLength.TabIndex = 0;
            this.rbNumberVarLength.TabStop = true;
            this.rbNumberVarLength.Text = "&Değişken Boylu Pozlara Ayrı Numara Ver";
            this.rbNumberVarLength.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbNumberVarLength.UseVisualStyleBackColor = true;
            this.rbNumberVarLength.CheckedChanged += new System.EventHandler(this.rbNumberVarLength_CheckedChanged);
            // 
            // gbManualNumber
            // 
            this.gbManualNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbManualNumber.Controls.Add(this.btnApplyNumber);
            this.gbManualNumber.Controls.Add(this.btnDeletePos);
            this.gbManualNumber.Controls.Add(this.btnAddPos);
            this.gbManualNumber.Controls.Add(this.btnIncrementNumber);
            this.gbManualNumber.Controls.Add(this.btnDecrementNumber);
            this.gbManualNumber.Controls.Add(this.txtNumber);
            this.gbManualNumber.Controls.Add(this.lblNumber);
            this.gbManualNumber.Location = new System.Drawing.Point(603, 333);
            this.gbManualNumber.Name = "gbManualNumber";
            this.gbManualNumber.Size = new System.Drawing.Size(262, 149);
            this.gbManualNumber.TabIndex = 3;
            this.gbManualNumber.TabStop = false;
            this.gbManualNumber.Text = "&Elle Numaralandırma";
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
            this.btnApplyNumber.Click += new System.EventHandler(this.btnApplyNumber_Click);
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
            this.btnDeletePos.Click += new System.EventHandler(this.btnDeletePos_Click);
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
            this.btnAddPos.Click += new System.EventHandler(this.btnAddPos_Click);
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
            this.btnIncrementNumber.Click += new System.EventHandler(this.btnIncrementNumber_Click);
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
            this.btnDecrementNumber.Click += new System.EventHandler(this.btnDecrementNumber_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(82, 102);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(123, 20);
            this.txtNumber.TabIndex = 5;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(21, 105);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 13);
            this.lblNumber.TabIndex = 4;
            this.lblNumber.Text = "&Numara";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(709, 496);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(790, 496);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbGroupVarLength);
            this.panel1.Controls.Add(this.rbNumberVarLength);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 40);
            this.panel1.TabIndex = 0;
            // 
            // rbGroupVarLength
            // 
            this.rbGroupVarLength.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbGroupVarLength.AutoSize = true;
            this.rbGroupVarLength.Image = global::RebarPosCommands.Properties.Resources.NumVarLength2;
            this.rbGroupVarLength.Location = new System.Drawing.Point(277, 3);
            this.rbGroupVarLength.Name = "rbGroupVarLength";
            this.rbGroupVarLength.Size = new System.Drawing.Size(218, 27);
            this.rbGroupVarLength.TabIndex = 1;
            this.rbGroupVarLength.Text = "Değişken Boylu Pozları &Gruplandır";
            this.rbGroupVarLength.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbGroupVarLength.UseVisualStyleBackColor = true;
            this.rbGroupVarLength.CheckedChanged += new System.EventHandler(this.rbGroupVarLength_CheckedChanged);
            // 
            // NumberingForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(877, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbManualNumber);
            this.Controls.Add(this.gbAutoNumber);
            this.Controls.Add(this.lbItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumberingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Numaralandırma";
            this.gbAutoNumber.ResumeLayout(false);
            this.gbAutoNumber.PerformLayout();
            this.gbManualNumber.ResumeLayout(false);
            this.gbManualNumber.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lbItems;
        private System.Windows.Forms.ColumnHeader chMarker;
        private System.Windows.Forms.ColumnHeader chOldMarker;
        private System.Windows.Forms.ColumnHeader chDiameter;
        private System.Windows.Forms.ColumnHeader chShape;
        private System.Windows.Forms.ColumnHeader chA;
        private System.Windows.Forms.ColumnHeader chB;
        private System.Windows.Forms.ColumnHeader chC;
        private System.Windows.Forms.ColumnHeader chD;
        private System.Windows.Forms.ColumnHeader chE;
        private System.Windows.Forms.ColumnHeader chF;
        private System.Windows.Forms.ColumnHeader chPriority;
        private System.Windows.Forms.GroupBox gbAutoNumber;
        private System.Windows.Forms.ComboBox cbOrder1;
        private System.Windows.Forms.Button btnAutoNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartNum;
        private System.Windows.Forms.ComboBox cbOrder4;
        private System.Windows.Forms.ComboBox cbOrder3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOrder2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbManualNumber;
        private System.Windows.Forms.Button btnDeletePos;
        private System.Windows.Forms.Button btnAddPos;
        private System.Windows.Forms.Button btnIncrementNumber;
        private System.Windows.Forms.Button btnDecrementNumber;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Button btnApplyNumber;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader chLength;
        private System.Windows.Forms.RadioButton rbNumberAll;
        private System.Windows.Forms.RadioButton rbKeepExisting;
        private System.Windows.Forms.RadioButton rbNumberVarLength;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbGroupVarLength;
    }
}