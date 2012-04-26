namespace RebarPosCommands
{
    partial class GroupForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupForm));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.udPrecision = new System.Windows.Forms.NumericUpDown();
            this.cbStyle = new System.Windows.Forms.ComboBox();
            this.cbDisplayUnit = new System.Windows.Forms.ComboBox();
            this.cbDrawingUnit = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaxLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBending = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbGroups = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.lGroups = new System.Windows.Forms.ImageList(this.components);
            this.btnSetCurrent = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.udPrecision);
            this.gbOptions.Controls.Add(this.cbStyle);
            this.gbOptions.Controls.Add(this.cbDisplayUnit);
            this.gbOptions.Controls.Add(this.cbDrawingUnit);
            this.gbOptions.Controls.Add(this.label5);
            this.gbOptions.Controls.Add(this.label4);
            this.gbOptions.Controls.Add(this.label3);
            this.gbOptions.Controls.Add(this.txtMaxLength);
            this.gbOptions.Controls.Add(this.label2);
            this.gbOptions.Controls.Add(this.label1);
            this.gbOptions.Controls.Add(this.chkBending);
            this.gbOptions.Location = new System.Drawing.Point(254, 12);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(311, 268);
            this.gbOptions.TabIndex = 2;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Grup S&eçenekleri";
            // 
            // udPrecision
            // 
            this.udPrecision.Location = new System.Drawing.Point(188, 74);
            this.udPrecision.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.udPrecision.Name = "udPrecision";
            this.udPrecision.Size = new System.Drawing.Size(100, 20);
            this.udPrecision.TabIndex = 5;
            this.udPrecision.ValueChanged += new System.EventHandler(this.udPrecision_ValueChanged);
            // 
            // cbStyle
            // 
            this.cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStyle.FormattingEnabled = true;
            this.cbStyle.Location = new System.Drawing.Point(188, 227);
            this.cbStyle.Name = "cbStyle";
            this.cbStyle.Size = new System.Drawing.Size(100, 21);
            this.cbStyle.TabIndex = 10;
            this.cbStyle.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
            // 
            // cbDisplayUnit
            // 
            this.cbDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplayUnit.FormattingEnabled = true;
            this.cbDisplayUnit.Items.AddRange(new object[] {
            "Milimetre",
            "Santimetre"});
            this.cbDisplayUnit.Location = new System.Drawing.Point(188, 46);
            this.cbDisplayUnit.Name = "cbDisplayUnit";
            this.cbDisplayUnit.Size = new System.Drawing.Size(100, 21);
            this.cbDisplayUnit.TabIndex = 3;
            this.cbDisplayUnit.SelectedIndexChanged += new System.EventHandler(this.cbDisplayUnit_SelectedIndexChanged);
            // 
            // cbDrawingUnit
            // 
            this.cbDrawingUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDrawingUnit.FormattingEnabled = true;
            this.cbDrawingUnit.Items.AddRange(new object[] {
            "Milimetre",
            "Santimetre"});
            this.cbDrawingUnit.Location = new System.Drawing.Point(188, 19);
            this.cbDrawingUnit.Name = "cbDrawingUnit";
            this.cbDrawingUnit.Size = new System.Drawing.Size(100, 21);
            this.cbDrawingUnit.TabIndex = 1;
            this.cbDrawingUnit.SelectedIndexChanged += new System.EventHandler(this.cbDrawingUnit_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "&Poz Stili";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Basamak &Sayısı";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "&Görüntülenen Birim";
            // 
            // txtMaxLength
            // 
            this.txtMaxLength.Location = new System.Drawing.Point(188, 126);
            this.txtMaxLength.Name = "txtMaxLength";
            this.txtMaxLength.Size = new System.Drawing.Size(100, 20);
            this.txtMaxLength.TabIndex = 7;
            this.txtMaxLength.TextChanged += new System.EventHandler(this.txtMaxLength_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Çizim &Birimi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Maksimum &Demir Boyu (m)";
            // 
            // chkBending
            // 
            this.chkBending.AutoSize = true;
            this.chkBending.Location = new System.Drawing.Point(20, 178);
            this.chkBending.Name = "chkBending";
            this.chkBending.Size = new System.Drawing.Size(209, 17);
            this.chkBending.TabIndex = 8;
            this.chkBending.Text = "&Toplam Boydan Demir Bükümlerini Düş";
            this.chkBending.UseVisualStyleBackColor = true;
            this.chkBending.CheckedChanged += new System.EventHandler(this.chkBending_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(409, 297);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(490, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbGroups);
            this.groupBox2.Controls.Add(this.btnSetCurrent);
            this.groupBox2.Controls.Add(this.btnRename);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 268);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Poz Grupları";
            // 
            // lbGroups
            // 
            this.lbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lbGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lbGroups.LabelEdit = true;
            this.lbGroups.LabelWrap = false;
            this.lbGroups.LargeImageList = this.lGroups;
            this.lbGroups.Location = new System.Drawing.Point(15, 23);
            this.lbGroups.MultiSelect = false;
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.Size = new System.Drawing.Size(175, 225);
            this.lbGroups.SmallImageList = this.lGroups;
            this.lbGroups.TabIndex = 0;
            this.lbGroups.UseCompatibleStateImageBehavior = false;
            this.lbGroups.View = System.Windows.Forms.View.Details;
            this.lbGroups.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lbGroups_AfterLabelEdit);
            this.lbGroups.SelectedIndexChanged += new System.EventHandler(this.lbGroups_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 130;
            // 
            // lGroups
            // 
            this.lGroups.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lGroups.ImageStream")));
            this.lGroups.TransparentColor = System.Drawing.Color.Transparent;
            this.lGroups.Images.SetKeyName(0, "page.png");
            this.lGroups.Images.SetKeyName(1, "page_white.png");
            this.lGroups.Images.SetKeyName(2, "page_white_add.png");
            this.lGroups.Images.SetKeyName(3, "page_white_delete.png");
            this.lGroups.Images.SetKeyName(4, "page_go.png");
            // 
            // btnSetCurrent
            // 
            this.btnSetCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetCurrent.Image = global::RebarPosCommands.Properties.Resources.tick;
            this.btnSetCurrent.Location = new System.Drawing.Point(196, 113);
            this.btnSetCurrent.Name = "btnSetCurrent";
            this.btnSetCurrent.Size = new System.Drawing.Size(24, 24);
            this.btnSetCurrent.TabIndex = 3;
            this.btnSetCurrent.UseVisualStyleBackColor = true;
            this.btnSetCurrent.Click += new System.EventHandler(this.btnSetCurrent_Click);
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnRename.Location = new System.Drawing.Point(196, 83);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(24, 24);
            this.btnRename.TabIndex = 3;
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Image = global::RebarPosCommands.Properties.Resources.delete;
            this.btnRemove.Location = new System.Drawing.Point(196, 53);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::RebarPosCommands.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(196, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // GroupForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(577, 335);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupForm";
            this.ShowInTaskbar = false;
            this.Text = "Poz Grupları";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chkBending;
        private System.Windows.Forms.TextBox txtMaxLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDisplayUnit;
        private System.Windows.Forms.ComboBox cbDrawingUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udPrecision;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lbGroups;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ImageList lGroups;
        private System.Windows.Forms.Button btnSetCurrent;
    }
}