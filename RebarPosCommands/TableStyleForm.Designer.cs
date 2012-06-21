namespace RebarPosCommands
{
    partial class TableStyleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableStyleForm));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.udPrecision = new System.Windows.Forms.NumericUpDown();
            this.cbDisplayUnit = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbStyles = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.lGroups = new System.Windows.Forms.ImageList(this.components);
            this.btnRename = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.btnPickFootingColor = new System.Windows.Forms.Button();
            this.btnPickHeadingColor = new System.Windows.Forms.Button();
            this.cbFootingStyle = new System.Windows.Forms.ComboBox();
            this.cbHeadingStyle = new System.Windows.Forms.ComboBox();
            this.cbTextStyle = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnPickBorderColor = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnPickSeparatorColor = new System.Windows.Forms.Button();
            this.btnPickLineColor = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnPickPosColor = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPickTextColor = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRowSpacing = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFootingScale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHeadingScale = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtColumns = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbColumns = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDiameterListColumn = new System.Windows.Forms.TextBox();
            this.txtTotalLengthColumn = new System.Windows.Forms.TextBox();
            this.txtShapeColumn = new System.Windows.Forms.TextBox();
            this.txtLengthColumn = new System.Windows.Forms.TextBox();
            this.txtDiameterColumn = new System.Windows.Forms.TextBox();
            this.txtCountCoumn = new System.Windows.Forms.TextBox();
            this.txtPosColumn = new System.Windows.Forms.TextBox();
            this.gbRows = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtGrossWeightRow = new System.Windows.Forms.TextBox();
            this.txtWeightRow = new System.Windows.Forms.TextBox();
            this.txtUnitWeightRow = new System.Windows.Forms.TextBox();
            this.txtTotalLengthRow = new System.Windows.Forms.TextBox();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gbColumns.SuspendLayout();
            this.gbRows.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.udPrecision);
            this.gbOptions.Controls.Add(this.cbDisplayUnit);
            this.gbOptions.Controls.Add(this.label4);
            this.gbOptions.Controls.Add(this.label3);
            this.gbOptions.Location = new System.Drawing.Point(409, 423);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(389, 92);
            this.gbOptions.TabIndex = 4;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Tablo S&eçenekleri";
            // 
            // udPrecision
            // 
            this.udPrecision.Location = new System.Drawing.Point(149, 54);
            this.udPrecision.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.udPrecision.Name = "udPrecision";
            this.udPrecision.Size = new System.Drawing.Size(100, 20);
            this.udPrecision.TabIndex = 3;
            this.udPrecision.ValueChanged += new System.EventHandler(this.udPrecision_ValueChanged);
            // 
            // cbDisplayUnit
            // 
            this.cbDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplayUnit.FormattingEnabled = true;
            this.cbDisplayUnit.Items.AddRange(new object[] {
            "Milimetre",
            "Santimetre"});
            this.cbDisplayUnit.Location = new System.Drawing.Point(149, 26);
            this.cbDisplayUnit.Name = "cbDisplayUnit";
            this.cbDisplayUnit.Size = new System.Drawing.Size(100, 21);
            this.cbDisplayUnit.TabIndex = 1;
            this.cbDisplayUnit.SelectedIndexChanged += new System.EventHandler(this.cbDisplayUnit_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Basamak &Sayısı";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "&Görüntülenen Birim";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(644, 534);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(725, 534);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbStyles);
            this.groupBox2.Controls.Add(this.btnRename);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 248);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tablo &Stilleri";
            // 
            // lbStyles
            // 
            this.lbStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStyles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lbStyles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lbStyles.LabelEdit = true;
            this.lbStyles.LabelWrap = false;
            this.lbStyles.LargeImageList = this.lGroups;
            this.lbStyles.Location = new System.Drawing.Point(15, 23);
            this.lbStyles.MultiSelect = false;
            this.lbStyles.Name = "lbStyles";
            this.lbStyles.Size = new System.Drawing.Size(225, 206);
            this.lbStyles.SmallImageList = this.lGroups;
            this.lbStyles.TabIndex = 0;
            this.lbStyles.UseCompatibleStateImageBehavior = false;
            this.lbStyles.View = System.Windows.Forms.View.Details;
            this.lbStyles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lbStyles_AfterLabelEdit);
            this.lbStyles.SelectedIndexChanged += new System.EventHandler(this.lbStyles_SelectedIndexChanged);
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
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnRename.Location = new System.Drawing.Point(246, 83);
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
            this.btnRemove.Location = new System.Drawing.Point(246, 53);
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
            this.btnAdd.Location = new System.Drawing.Point(246, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gbDisplay
            // 
            this.gbDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDisplay.Controls.Add(this.btnPickFootingColor);
            this.gbDisplay.Controls.Add(this.btnPickHeadingColor);
            this.gbDisplay.Controls.Add(this.cbFootingStyle);
            this.gbDisplay.Controls.Add(this.cbHeadingStyle);
            this.gbDisplay.Controls.Add(this.cbTextStyle);
            this.gbDisplay.Controls.Add(this.label13);
            this.gbDisplay.Controls.Add(this.btnPickBorderColor);
            this.gbDisplay.Controls.Add(this.label6);
            this.gbDisplay.Controls.Add(this.label15);
            this.gbDisplay.Controls.Add(this.label12);
            this.gbDisplay.Controls.Add(this.label14);
            this.gbDisplay.Controls.Add(this.btnPickSeparatorColor);
            this.gbDisplay.Controls.Add(this.btnPickLineColor);
            this.gbDisplay.Controls.Add(this.label11);
            this.gbDisplay.Controls.Add(this.label18);
            this.gbDisplay.Controls.Add(this.btnPickPosColor);
            this.gbDisplay.Controls.Add(this.label10);
            this.gbDisplay.Controls.Add(this.btnPickTextColor);
            this.gbDisplay.Controls.Add(this.label9);
            this.gbDisplay.Controls.Add(this.txtRowSpacing);
            this.gbDisplay.Controls.Add(this.label1);
            this.gbDisplay.Controls.Add(this.txtFootingScale);
            this.gbDisplay.Controls.Add(this.label2);
            this.gbDisplay.Controls.Add(this.txtHeadingScale);
            this.gbDisplay.Controls.Add(this.label16);
            this.gbDisplay.Controls.Add(this.label8);
            this.gbDisplay.Location = new System.Drawing.Point(307, 12);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(491, 249);
            this.gbDisplay.TabIndex = 1;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Görünüm &Ayarları";
            // 
            // btnPickFootingColor
            // 
            this.btnPickFootingColor.BackColor = System.Drawing.Color.White;
            this.btnPickFootingColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickFootingColor.Location = new System.Drawing.Point(104, 203);
            this.btnPickFootingColor.Name = "btnPickFootingColor";
            this.btnPickFootingColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickFootingColor.TabIndex = 13;
            this.btnPickFootingColor.UseVisualStyleBackColor = false;
            this.btnPickFootingColor.Click += new System.EventHandler(this.btnPickFootingColor_Click);
            // 
            // btnPickHeadingColor
            // 
            this.btnPickHeadingColor.BackColor = System.Drawing.Color.White;
            this.btnPickHeadingColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickHeadingColor.Location = new System.Drawing.Point(104, 174);
            this.btnPickHeadingColor.Name = "btnPickHeadingColor";
            this.btnPickHeadingColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickHeadingColor.TabIndex = 11;
            this.btnPickHeadingColor.UseVisualStyleBackColor = false;
            this.btnPickHeadingColor.Click += new System.EventHandler(this.btnPickHeadingColor_Click);
            // 
            // cbFootingStyle
            // 
            this.cbFootingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFootingStyle.FormattingEnabled = true;
            this.cbFootingStyle.Location = new System.Drawing.Point(370, 121);
            this.cbFootingStyle.Name = "cbFootingStyle";
            this.cbFootingStyle.Size = new System.Drawing.Size(100, 21);
            this.cbFootingStyle.TabIndex = 21;
            this.cbFootingStyle.SelectedIndexChanged += new System.EventHandler(this.cbFootingStyle_SelectedIndexChanged);
            // 
            // cbHeadingStyle
            // 
            this.cbHeadingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeadingStyle.FormattingEnabled = true;
            this.cbHeadingStyle.Location = new System.Drawing.Point(370, 69);
            this.cbHeadingStyle.Name = "cbHeadingStyle";
            this.cbHeadingStyle.Size = new System.Drawing.Size(100, 21);
            this.cbHeadingStyle.TabIndex = 17;
            this.cbHeadingStyle.SelectedIndexChanged += new System.EventHandler(this.cbHeadingStyle_SelectedIndexChanged);
            // 
            // cbTextStyle
            // 
            this.cbTextStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextStyle.FormattingEnabled = true;
            this.cbTextStyle.Location = new System.Drawing.Point(370, 26);
            this.cbTextStyle.Name = "cbTextStyle";
            this.cbTextStyle.Size = new System.Drawing.Size(100, 21);
            this.cbTextStyle.TabIndex = 15;
            this.cbTextStyle.SelectedIndexChanged += new System.EventHandler(this.cbTextStyle_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 208);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "&Altbilgi Rengi";
            // 
            // btnPickBorderColor
            // 
            this.btnPickBorderColor.BackColor = System.Drawing.Color.White;
            this.btnPickBorderColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickBorderColor.Location = new System.Drawing.Point(104, 145);
            this.btnPickBorderColor.Name = "btnPickBorderColor";
            this.btnPickBorderColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickBorderColor.TabIndex = 9;
            this.btnPickBorderColor.UseVisualStyleBackColor = false;
            this.btnPickBorderColor.Click += new System.EventHandler(this.btnPickBorderColor_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "&Altbilgi Yazısı Stili";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(248, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Ü&stbilgi Yazısı Stili";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 179);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Ü&stbilgi Rengi";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(248, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "&Yazı Stili";
            // 
            // btnPickSeparatorColor
            // 
            this.btnPickSeparatorColor.BackColor = System.Drawing.Color.White;
            this.btnPickSeparatorColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickSeparatorColor.Location = new System.Drawing.Point(104, 116);
            this.btnPickSeparatorColor.Name = "btnPickSeparatorColor";
            this.btnPickSeparatorColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickSeparatorColor.TabIndex = 7;
            this.btnPickSeparatorColor.UseVisualStyleBackColor = false;
            this.btnPickSeparatorColor.Click += new System.EventHandler(this.btnPickSeparatorColor_Click);
            // 
            // btnPickLineColor
            // 
            this.btnPickLineColor.BackColor = System.Drawing.Color.White;
            this.btnPickLineColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickLineColor.Location = new System.Drawing.Point(104, 87);
            this.btnPickLineColor.Name = "btnPickLineColor";
            this.btnPickLineColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickLineColor.TabIndex = 5;
            this.btnPickLineColor.UseVisualStyleBackColor = false;
            this.btnPickLineColor.Click += new System.EventHandler(this.btnPickLineColor_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Ç&erçeve Rengi";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 121);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Tali Ç&izgi Rengi";
            // 
            // btnPickPosColor
            // 
            this.btnPickPosColor.BackColor = System.Drawing.Color.White;
            this.btnPickPosColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickPosColor.Location = new System.Drawing.Point(104, 58);
            this.btnPickPosColor.Name = "btnPickPosColor";
            this.btnPickPosColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickPosColor.TabIndex = 3;
            this.btnPickPosColor.UseVisualStyleBackColor = false;
            this.btnPickPosColor.Click += new System.EventHandler(this.btnPickPosColor_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "A&na Çizgi Rengi";
            // 
            // btnPickTextColor
            // 
            this.btnPickTextColor.BackColor = System.Drawing.Color.White;
            this.btnPickTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickTextColor.Location = new System.Drawing.Point(104, 29);
            this.btnPickTextColor.Name = "btnPickTextColor";
            this.btnPickTextColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickTextColor.TabIndex = 1;
            this.btnPickTextColor.UseVisualStyleBackColor = false;
            this.btnPickTextColor.Click += new System.EventHandler(this.btnPickTextColor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "&Sütun Rengi";
            // 
            // txtRowSpacing
            // 
            this.txtRowSpacing.Location = new System.Drawing.Point(370, 205);
            this.txtRowSpacing.Name = "txtRowSpacing";
            this.txtRowSpacing.Size = new System.Drawing.Size(100, 20);
            this.txtRowSpacing.TabIndex = 25;
            this.txtRowSpacing.Validated += new System.EventHandler(this.txtRowSpacing_Validated);
            this.txtRowSpacing.Validating += new System.ComponentModel.CancelEventHandler(this.txtRowSpacing_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "&Satır Aralığı";
            // 
            // txtFootingScale
            // 
            this.txtFootingScale.Location = new System.Drawing.Point(370, 147);
            this.txtFootingScale.Name = "txtFootingScale";
            this.txtFootingScale.Size = new System.Drawing.Size(100, 20);
            this.txtFootingScale.TabIndex = 23;
            this.txtFootingScale.Validated += new System.EventHandler(this.txtFootingScale_Validated);
            this.txtFootingScale.Validating += new System.ComponentModel.CancelEventHandler(this.txtFootingScale_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Altbilgi &Yazısı Ölçeği";
            // 
            // txtHeadingScale
            // 
            this.txtHeadingScale.Location = new System.Drawing.Point(370, 95);
            this.txtHeadingScale.Name = "txtHeadingScale";
            this.txtHeadingScale.Size = new System.Drawing.Size(100, 20);
            this.txtHeadingScale.TabIndex = 19;
            this.txtHeadingScale.Validated += new System.EventHandler(this.txtHeadingScale_Validated);
            this.txtHeadingScale.Validating += new System.ComponentModel.CancelEventHandler(this.txtHeadingScale_Validating);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(248, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Üstbilgi Yazısı Ö&lçeği";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "&Yazı Rengi";
            // 
            // txtColumns
            // 
            this.txtColumns.Location = new System.Drawing.Point(149, 25);
            this.txtColumns.Name = "txtColumns";
            this.txtColumns.Size = new System.Drawing.Size(217, 20);
            this.txtColumns.TabIndex = 1;
            this.txtColumns.Validated += new System.EventHandler(this.txtColumns_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "&Sütunlar";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // gbColumns
            // 
            this.gbColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbColumns.Controls.Add(this.label23);
            this.gbColumns.Controls.Add(this.label22);
            this.gbColumns.Controls.Add(this.label21);
            this.gbColumns.Controls.Add(this.label20);
            this.gbColumns.Controls.Add(this.label19);
            this.gbColumns.Controls.Add(this.label17);
            this.gbColumns.Controls.Add(this.label7);
            this.gbColumns.Controls.Add(this.label5);
            this.gbColumns.Controls.Add(this.txtDiameterListColumn);
            this.gbColumns.Controls.Add(this.txtTotalLengthColumn);
            this.gbColumns.Controls.Add(this.txtShapeColumn);
            this.gbColumns.Controls.Add(this.txtLengthColumn);
            this.gbColumns.Controls.Add(this.txtDiameterColumn);
            this.gbColumns.Controls.Add(this.txtCountCoumn);
            this.gbColumns.Controls.Add(this.txtPosColumn);
            this.gbColumns.Controls.Add(this.txtColumns);
            this.gbColumns.Location = new System.Drawing.Point(14, 267);
            this.gbColumns.Name = "gbColumns";
            this.gbColumns.Size = new System.Drawing.Size(389, 248);
            this.gbColumns.TabIndex = 2;
            this.gbColumns.TabStop = false;
            this.gbColumns.Text = "&Sütunlar";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(18, 211);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 13);
            this.label23.TabIndex = 14;
            this.label23.Text = "Çap &Listesi";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(18, 185);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(63, 13);
            this.label22.TabIndex = 12;
            this.label22.Text = "&Toplam Boy";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 159);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(30, 13);
            this.label21.TabIndex = 10;
            this.label21.Text = "Ş&ekil";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(18, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(25, 13);
            this.label20.TabIndex = 8;
            this.label20.Text = "&Boy";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 107);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(26, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Ç&ap";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 81);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "&Adet";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "&Poz";
            // 
            // txtDiameterListColumn
            // 
            this.txtDiameterListColumn.Location = new System.Drawing.Point(149, 208);
            this.txtDiameterListColumn.Name = "txtDiameterListColumn";
            this.txtDiameterListColumn.Size = new System.Drawing.Size(217, 20);
            this.txtDiameterListColumn.TabIndex = 15;
            this.txtDiameterListColumn.Validated += new System.EventHandler(this.txtDiameterListColumn_Validated);
            // 
            // txtTotalLengthColumn
            // 
            this.txtTotalLengthColumn.Location = new System.Drawing.Point(149, 182);
            this.txtTotalLengthColumn.Name = "txtTotalLengthColumn";
            this.txtTotalLengthColumn.Size = new System.Drawing.Size(217, 20);
            this.txtTotalLengthColumn.TabIndex = 13;
            this.txtTotalLengthColumn.Validated += new System.EventHandler(this.txtTotalLengthColumn_Validated);
            // 
            // txtShapeColumn
            // 
            this.txtShapeColumn.Location = new System.Drawing.Point(149, 156);
            this.txtShapeColumn.Name = "txtShapeColumn";
            this.txtShapeColumn.Size = new System.Drawing.Size(217, 20);
            this.txtShapeColumn.TabIndex = 11;
            this.txtShapeColumn.Validated += new System.EventHandler(this.txtShapeColumn_Validated);
            // 
            // txtLengthColumn
            // 
            this.txtLengthColumn.Location = new System.Drawing.Point(149, 130);
            this.txtLengthColumn.Name = "txtLengthColumn";
            this.txtLengthColumn.Size = new System.Drawing.Size(217, 20);
            this.txtLengthColumn.TabIndex = 9;
            this.txtLengthColumn.Validated += new System.EventHandler(this.txtLengthColumn_Validated);
            // 
            // txtDiameterColumn
            // 
            this.txtDiameterColumn.Location = new System.Drawing.Point(149, 104);
            this.txtDiameterColumn.Name = "txtDiameterColumn";
            this.txtDiameterColumn.Size = new System.Drawing.Size(217, 20);
            this.txtDiameterColumn.TabIndex = 7;
            this.txtDiameterColumn.Validated += new System.EventHandler(this.txtDiameterColumn_Validated);
            // 
            // txtCountCoumn
            // 
            this.txtCountCoumn.Location = new System.Drawing.Point(149, 78);
            this.txtCountCoumn.Name = "txtCountCoumn";
            this.txtCountCoumn.Size = new System.Drawing.Size(217, 20);
            this.txtCountCoumn.TabIndex = 5;
            this.txtCountCoumn.Validated += new System.EventHandler(this.txtCountCoumn_Validated);
            // 
            // txtPosColumn
            // 
            this.txtPosColumn.Location = new System.Drawing.Point(149, 52);
            this.txtPosColumn.Name = "txtPosColumn";
            this.txtPosColumn.Size = new System.Drawing.Size(217, 20);
            this.txtPosColumn.TabIndex = 3;
            this.txtPosColumn.Validated += new System.EventHandler(this.txtPosColumn_Validated);
            // 
            // gbRows
            // 
            this.gbRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRows.Controls.Add(this.label27);
            this.gbRows.Controls.Add(this.label26);
            this.gbRows.Controls.Add(this.label25);
            this.gbRows.Controls.Add(this.label24);
            this.gbRows.Controls.Add(this.txtGrossWeightRow);
            this.gbRows.Controls.Add(this.txtWeightRow);
            this.gbRows.Controls.Add(this.txtUnitWeightRow);
            this.gbRows.Controls.Add(this.txtTotalLengthRow);
            this.gbRows.Location = new System.Drawing.Point(409, 267);
            this.gbRows.Name = "gbRows";
            this.gbRows.Size = new System.Drawing.Size(389, 150);
            this.gbRows.TabIndex = 3;
            this.gbRows.TabStop = false;
            this.gbRows.Text = "&Toplam Satırları";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 111);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 13);
            this.label27.TabIndex = 6;
            this.label27.Text = "&Toplam Ağırlık";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(18, 85);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(35, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "&Ağırlık";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 59);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 13);
            this.label25.TabIndex = 2;
            this.label25.Text = "&Birim Ağırlık";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(18, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(84, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "Toplam &Uzunluk";
            // 
            // txtGrossWeightRow
            // 
            this.txtGrossWeightRow.Location = new System.Drawing.Point(149, 108);
            this.txtGrossWeightRow.Name = "txtGrossWeightRow";
            this.txtGrossWeightRow.Size = new System.Drawing.Size(217, 20);
            this.txtGrossWeightRow.TabIndex = 7;
            this.txtGrossWeightRow.Validated += new System.EventHandler(this.txtGrossWeightRow_Validated);
            // 
            // txtWeightRow
            // 
            this.txtWeightRow.Location = new System.Drawing.Point(149, 82);
            this.txtWeightRow.Name = "txtWeightRow";
            this.txtWeightRow.Size = new System.Drawing.Size(217, 20);
            this.txtWeightRow.TabIndex = 5;
            this.txtWeightRow.Validated += new System.EventHandler(this.txtWeightRow_Validated);
            // 
            // txtUnitWeightRow
            // 
            this.txtUnitWeightRow.Location = new System.Drawing.Point(149, 56);
            this.txtUnitWeightRow.Name = "txtUnitWeightRow";
            this.txtUnitWeightRow.Size = new System.Drawing.Size(217, 20);
            this.txtUnitWeightRow.TabIndex = 3;
            this.txtUnitWeightRow.Validated += new System.EventHandler(this.txtUnitWeightRow_Validated);
            // 
            // txtTotalLengthRow
            // 
            this.txtTotalLengthRow.Location = new System.Drawing.Point(149, 30);
            this.txtTotalLengthRow.Name = "txtTotalLengthRow";
            this.txtTotalLengthRow.Size = new System.Drawing.Size(217, 20);
            this.txtTotalLengthRow.TabIndex = 1;
            this.txtTotalLengthRow.Validated += new System.EventHandler(this.txtTotalLengthRow_Validated);
            // 
            // TableStyleForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(812, 572);
            this.Controls.Add(this.gbRows);
            this.Controls.Add(this.gbColumns);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(818, 600);
            this.Name = "TableStyleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tablo Stilleri";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gbColumns.ResumeLayout(false);
            this.gbColumns.PerformLayout();
            this.gbRows.ResumeLayout(false);
            this.gbRows.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.ComboBox cbDisplayUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown udPrecision;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lbStyles;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ImageList lGroups;
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.TextBox txtColumns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPickTextColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPickPosColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPickLineColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPickBorderColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnPickHeadingColor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnPickFootingColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbTextStyle;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbHeadingStyle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtHeadingScale;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnPickSeparatorColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbFootingStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFootingScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRowSpacing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbColumns;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPosColumn;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDiameterColumn;
        private System.Windows.Forms.TextBox txtCountCoumn;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtLengthColumn;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtShapeColumn;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtTotalLengthColumn;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtDiameterListColumn;
        private System.Windows.Forms.GroupBox gbRows;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtTotalLengthRow;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtUnitWeightRow;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtWeightRow;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtGrossWeightRow;
    }
}