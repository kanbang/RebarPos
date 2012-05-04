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
            this.cbDisplayUnit = new System.Windows.Forms.ComboBox();
            this.cbDrawingUnit = new System.Windows.Forms.ComboBox();
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
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.btnPickCurrentGroupColor = new System.Windows.Forms.Button();
            this.btnPickGroupColor = new System.Windows.Forms.Button();
            this.cbNoteStyle = new System.Windows.Forms.ComboBox();
            this.cbTextStyle = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnPickMultiplierColor = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnPickCircleColor = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnPickPosColor = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPickTextColor = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNoteScale = new System.Windows.Forms.TextBox();
            this.txtFormulaPosOnly = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFormulaWithoutLength = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.txtDiameterList = new System.Windows.Forms.TextBox();
            this.posStylePreview = new RebarPosCommands.PosStylePreview();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.udPrecision);
            this.gbOptions.Controls.Add(this.cbDisplayUnit);
            this.gbOptions.Controls.Add(this.cbDrawingUnit);
            this.gbOptions.Controls.Add(this.label4);
            this.gbOptions.Controls.Add(this.label3);
            this.gbOptions.Controls.Add(this.txtDiameterList);
            this.gbOptions.Controls.Add(this.txtMaxLength);
            this.gbOptions.Controls.Add(this.label2);
            this.gbOptions.Controls.Add(this.label17);
            this.gbOptions.Controls.Add(this.label1);
            this.gbOptions.Controls.Add(this.chkBending);
            this.gbOptions.Location = new System.Drawing.Point(306, 12);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(311, 246);
            this.gbOptions.TabIndex = 1;
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
            this.txtMaxLength.Location = new System.Drawing.Point(188, 115);
            this.txtMaxLength.Name = "txtMaxLength";
            this.txtMaxLength.Size = new System.Drawing.Size(100, 20);
            this.txtMaxLength.TabIndex = 7;
            this.txtMaxLength.Validated += new System.EventHandler(this.txtMaxLength_Validated);
            this.txtMaxLength.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaxLength_Validating);
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
            this.label1.Location = new System.Drawing.Point(17, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Maksimum &Demir Boyu (m)";
            // 
            // chkBending
            // 
            this.chkBending.AutoSize = true;
            this.chkBending.Location = new System.Drawing.Point(20, 141);
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
            this.btnOK.Location = new System.Drawing.Point(461, 621);
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
            this.btnCancel.Location = new System.Drawing.Point(542, 621);
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
            this.groupBox2.Size = new System.Drawing.Size(288, 246);
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
            this.lbGroups.Size = new System.Drawing.Size(227, 204);
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
            this.btnSetCurrent.Location = new System.Drawing.Point(248, 113);
            this.btnSetCurrent.Name = "btnSetCurrent";
            this.btnSetCurrent.Size = new System.Drawing.Size(24, 24);
            this.btnSetCurrent.TabIndex = 4;
            this.btnSetCurrent.UseVisualStyleBackColor = true;
            this.btnSetCurrent.Click += new System.EventHandler(this.btnSetCurrent_Click);
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnRename.Location = new System.Drawing.Point(248, 83);
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
            this.btnRemove.Location = new System.Drawing.Point(248, 53);
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
            this.btnAdd.Location = new System.Drawing.Point(248, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gbDisplay
            // 
            this.gbDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDisplay.Controls.Add(this.posStylePreview);
            this.gbDisplay.Controls.Add(this.btnPickCurrentGroupColor);
            this.gbDisplay.Controls.Add(this.btnPickGroupColor);
            this.gbDisplay.Controls.Add(this.cbNoteStyle);
            this.gbDisplay.Controls.Add(this.cbTextStyle);
            this.gbDisplay.Controls.Add(this.label13);
            this.gbDisplay.Controls.Add(this.btnPickMultiplierColor);
            this.gbDisplay.Controls.Add(this.label15);
            this.gbDisplay.Controls.Add(this.label12);
            this.gbDisplay.Controls.Add(this.label14);
            this.gbDisplay.Controls.Add(this.btnPickCircleColor);
            this.gbDisplay.Controls.Add(this.label11);
            this.gbDisplay.Controls.Add(this.btnPickPosColor);
            this.gbDisplay.Controls.Add(this.label10);
            this.gbDisplay.Controls.Add(this.btnPickTextColor);
            this.gbDisplay.Controls.Add(this.label9);
            this.gbDisplay.Controls.Add(this.txtNoteScale);
            this.gbDisplay.Controls.Add(this.txtFormulaPosOnly);
            this.gbDisplay.Controls.Add(this.label16);
            this.gbDisplay.Controls.Add(this.label8);
            this.gbDisplay.Controls.Add(this.label7);
            this.gbDisplay.Controls.Add(this.txtFormulaWithoutLength);
            this.gbDisplay.Controls.Add(this.label6);
            this.gbDisplay.Controls.Add(this.txtFormula);
            this.gbDisplay.Controls.Add(this.label5);
            this.gbDisplay.Location = new System.Drawing.Point(12, 265);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(605, 339);
            this.gbDisplay.TabIndex = 2;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Görünüm &Ayarları";
            // 
            // btnPickCurrentGroupColor
            // 
            this.btnPickCurrentGroupColor.BackColor = System.Drawing.Color.White;
            this.btnPickCurrentGroupColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickCurrentGroupColor.Location = new System.Drawing.Point(482, 167);
            this.btnPickCurrentGroupColor.Name = "btnPickCurrentGroupColor";
            this.btnPickCurrentGroupColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickCurrentGroupColor.TabIndex = 17;
            this.btnPickCurrentGroupColor.UseVisualStyleBackColor = false;
            this.btnPickCurrentGroupColor.Click += new System.EventHandler(this.btnPickCurrentGroupColor_Click);
            // 
            // btnPickGroupColor
            // 
            this.btnPickGroupColor.BackColor = System.Drawing.Color.White;
            this.btnPickGroupColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickGroupColor.Location = new System.Drawing.Point(482, 138);
            this.btnPickGroupColor.Name = "btnPickGroupColor";
            this.btnPickGroupColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickGroupColor.TabIndex = 15;
            this.btnPickGroupColor.UseVisualStyleBackColor = false;
            this.btnPickGroupColor.Click += new System.EventHandler(this.btnPickGroupColor_Click);
            // 
            // cbNoteStyle
            // 
            this.cbNoteStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNoteStyle.FormattingEnabled = true;
            this.cbNoteStyle.Location = new System.Drawing.Point(187, 233);
            this.cbNoteStyle.Name = "cbNoteStyle";
            this.cbNoteStyle.Size = new System.Drawing.Size(100, 21);
            this.cbNoteStyle.TabIndex = 21;
            this.cbNoteStyle.SelectedIndexChanged += new System.EventHandler(this.cbNoteStyle_SelectedIndexChanged);
            // 
            // cbTextStyle
            // 
            this.cbTextStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextStyle.FormattingEnabled = true;
            this.cbTextStyle.Location = new System.Drawing.Point(187, 207);
            this.cbTextStyle.Name = "cbTextStyle";
            this.cbTextStyle.Size = new System.Drawing.Size(100, 21);
            this.cbTextStyle.TabIndex = 19;
            this.cbTextStyle.SelectedIndexChanged += new System.EventHandler(this.cbTextStyle_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(311, 172);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "&Aktif Poz Grubu Rengi";
            // 
            // btnPickMultiplierColor
            // 
            this.btnPickMultiplierColor.BackColor = System.Drawing.Color.White;
            this.btnPickMultiplierColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickMultiplierColor.Location = new System.Drawing.Point(482, 109);
            this.btnPickMultiplierColor.Name = "btnPickMultiplierColor";
            this.btnPickMultiplierColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickMultiplierColor.TabIndex = 13;
            this.btnPickMultiplierColor.UseVisualStyleBackColor = false;
            this.btnPickMultiplierColor.Click += new System.EventHandler(this.btnPickMultiplierColor_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 236);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "&Not Yazısı Stili";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(311, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Poz &Grubu Rengi";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 210);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "&Yazı Stili";
            // 
            // btnPickCircleColor
            // 
            this.btnPickCircleColor.BackColor = System.Drawing.Color.White;
            this.btnPickCircleColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickCircleColor.Location = new System.Drawing.Point(187, 167);
            this.btnPickCircleColor.Name = "btnPickCircleColor";
            this.btnPickCircleColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickCircleColor.TabIndex = 11;
            this.btnPickCircleColor.UseVisualStyleBackColor = false;
            this.btnPickCircleColor.Click += new System.EventHandler(this.btnPickCircleColor_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(311, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Poz Ç&arpanı Rengi";
            // 
            // btnPickPosColor
            // 
            this.btnPickPosColor.BackColor = System.Drawing.Color.White;
            this.btnPickPosColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickPosColor.Location = new System.Drawing.Point(187, 138);
            this.btnPickPosColor.Name = "btnPickPosColor";
            this.btnPickPosColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickPosColor.TabIndex = 9;
            this.btnPickPosColor.UseVisualStyleBackColor = false;
            this.btnPickPosColor.Click += new System.EventHandler(this.btnPickPosColor_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Poz &Dairesi Rengi";
            // 
            // btnPickTextColor
            // 
            this.btnPickTextColor.BackColor = System.Drawing.Color.White;
            this.btnPickTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickTextColor.Location = new System.Drawing.Point(187, 109);
            this.btnPickTextColor.Name = "btnPickTextColor";
            this.btnPickTextColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickTextColor.TabIndex = 7;
            this.btnPickTextColor.UseVisualStyleBackColor = false;
            this.btnPickTextColor.Click += new System.EventHandler(this.btnPickTextColor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "&Poz Numarası Rengi";
            // 
            // txtNoteScale
            // 
            this.txtNoteScale.Location = new System.Drawing.Point(187, 259);
            this.txtNoteScale.Name = "txtNoteScale";
            this.txtNoteScale.Size = new System.Drawing.Size(100, 20);
            this.txtNoteScale.TabIndex = 23;
            this.txtNoteScale.Validated += new System.EventHandler(this.txtNoteScale_Validated);
            this.txtNoteScale.Validating += new System.ComponentModel.CancelEventHandler(this.txtNoteScale_Validating);
            // 
            // txtFormulaPosOnly
            // 
            this.txtFormulaPosOnly.Location = new System.Drawing.Point(187, 71);
            this.txtFormulaPosOnly.Name = "txtFormulaPosOnly";
            this.txtFormulaPosOnly.Size = new System.Drawing.Size(395, 20);
            this.txtFormulaPosOnly.TabIndex = 5;
            this.txtFormulaPosOnly.Validated += new System.EventHandler(this.txtFormulaPosOnly_Validated);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 262);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Not Yazısı Ö&lçeği";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "&Yazı Rengi";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Formül (Sadece &Poz Numarası)";
            // 
            // txtFormulaWithoutLength
            // 
            this.txtFormulaWithoutLength.Location = new System.Drawing.Point(187, 45);
            this.txtFormulaWithoutLength.Name = "txtFormulaWithoutLength";
            this.txtFormulaWithoutLength.Size = new System.Drawing.Size(395, 20);
            this.txtFormulaWithoutLength.TabIndex = 3;
            this.txtFormulaWithoutLength.Validated += new System.EventHandler(this.txtFormulaWithoutLength_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Formül (&L Boyu Gizli)";
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(187, 19);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(395, 20);
            this.txtFormula.TabIndex = 1;
            this.txtFormula.Validated += new System.EventHandler(this.txtFormula_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "&Formül";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 178);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "&Standart Çap Listesi:";
            // 
            // txtDiameterList
            // 
            this.txtDiameterList.Location = new System.Drawing.Point(20, 194);
            this.txtDiameterList.Multiline = true;
            this.txtDiameterList.Name = "txtDiameterList";
            this.txtDiameterList.Size = new System.Drawing.Size(268, 33);
            this.txtDiameterList.TabIndex = 10;
            this.txtDiameterList.Validated += new System.EventHandler(this.txtDiameterList_Validated);
            this.txtDiameterList.Validating += new System.ComponentModel.CancelEventHandler(this.txtDiameterList_Validating);
            // 
            // posStylePreview
            // 
            this.posStylePreview.BackColor = System.Drawing.Color.Black;
            this.posStylePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.posStylePreview.CircleColor = System.Drawing.Color.Yellow;
            this.posStylePreview.CurrentGroupHighlightColor = System.Drawing.Color.Silver;
            this.posStylePreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.posStylePreview.Formula1 = "[M:C][N][\"T\":D][\"/\":S][\" L=\":L]";
            this.posStylePreview.Formula2 = "[M:C][N][\"T\":D][\"/\":S]";
            this.posStylePreview.Formula3 = "[M:C]";
            this.posStylePreview.GroupColor = System.Drawing.Color.Gray;
            this.posStylePreview.Location = new System.Drawing.Point(314, 207);
            this.posStylePreview.MultiplierColor = System.Drawing.Color.Gray;
            this.posStylePreview.Name = "posStylePreview";
            this.posStylePreview.NoteColor = System.Drawing.Color.Orange;
            this.posStylePreview.PosColor = System.Drawing.Color.Red;
            this.posStylePreview.Size = new System.Drawing.Size(268, 114);
            this.posStylePreview.TabIndex = 24;
            this.posStylePreview.TextColor = System.Drawing.Color.Red;
            // 
            // GroupForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(629, 659);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poz Grupları";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
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
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.TextBox txtFormulaWithoutLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFormulaPosOnly;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPickTextColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPickPosColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPickCircleColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPickMultiplierColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnPickGroupColor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnPickCurrentGroupColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbTextStyle;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbNoteStyle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNoteScale;
        private System.Windows.Forms.Label label16;
        private PosStylePreview posStylePreview;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtDiameterList;
        private System.Windows.Forms.Label label17;
    }
}