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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUserOnly = new System.Windows.Forms.CheckBox();
            this.lbStyles = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.lGroups = new System.Windows.Forms.ImageList(this.components);
            this.btnRename = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtColumns = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbColumns = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMultiplierHeadingLabel = new System.Windows.Forms.TextBox();
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
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTextStyle = new System.Windows.Forms.ComboBox();
            this.cbHeadingStyle = new System.Windows.Forms.ComboBox();
            this.cbFootingStyle = new System.Windows.Forms.ComboBox();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableView = new RebarPosCommands.TableStylePreview();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gbColumns.SuspendLayout();
            this.gbRows.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(1059, 593);
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
            this.btnCancel.Location = new System.Drawing.Point(1140, 593);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.chkUserOnly);
            this.groupBox2.Controls.Add(this.lbStyles);
            this.groupBox2.Controls.Add(this.btnRename);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 565);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tablo &Stilleri";
            // 
            // chkUserOnly
            // 
            this.chkUserOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUserOnly.AutoSize = true;
            this.chkUserOnly.Location = new System.Drawing.Point(15, 526);
            this.chkUserOnly.Name = "chkUserOnly";
            this.chkUserOnly.Size = new System.Drawing.Size(205, 17);
            this.chkUserOnly.TabIndex = 7;
            this.chkUserOnly.Text = "&Sadece Kullanıcı Tanımlı Stilleri Göster";
            this.chkUserOnly.UseVisualStyleBackColor = true;
            this.chkUserOnly.CheckedChanged += new System.EventHandler(this.chkUserOnly_CheckedChanged);
            // 
            // lbStyles
            // 
            this.lbStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStyles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lbStyles.FullRowSelect = true;
            this.lbStyles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lbStyles.HideSelection = false;
            this.lbStyles.LabelEdit = true;
            this.lbStyles.LabelWrap = false;
            this.lbStyles.LargeImageList = this.lGroups;
            this.lbStyles.Location = new System.Drawing.Point(15, 23);
            this.lbStyles.MultiSelect = false;
            this.lbStyles.Name = "lbStyles";
            this.lbStyles.Size = new System.Drawing.Size(225, 490);
            this.lbStyles.SmallImageList = this.lGroups;
            this.lbStyles.TabIndex = 0;
            this.lbStyles.UseCompatibleStateImageBehavior = false;
            this.lbStyles.View = System.Windows.Forms.View.Details;
            this.lbStyles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lbStyles_AfterLabelEdit);
            this.lbStyles.SelectedIndexChanged += new System.EventHandler(this.lbStyles_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Tablo Stili Adı";
            this.chName.Width = 200;
            // 
            // lGroups
            // 
            this.lGroups.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lGroups.ImageStream")));
            this.lGroups.TransparentColor = System.Drawing.Color.Magenta;
            this.lGroups.Images.SetKeyName(0, "bullet_black.png");
            this.lGroups.Images.SetKeyName(1, "bullet_red.png");
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
            // txtColumns
            // 
            this.txtColumns.Location = new System.Drawing.Point(149, 25);
            this.txtColumns.Name = "txtColumns";
            this.txtColumns.Size = new System.Drawing.Size(217, 20);
            this.txtColumns.TabIndex = 1;
            this.txtColumns.Validated += new System.EventHandler(this.txtColumns_Validated);
            this.txtColumns.Validating += new System.ComponentModel.CancelEventHandler(this.txtColumns_Validating);
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
            // 
            // gbColumns
            // 
            this.gbColumns.Controls.Add(this.label1);
            this.gbColumns.Controls.Add(this.label23);
            this.gbColumns.Controls.Add(this.label22);
            this.gbColumns.Controls.Add(this.label21);
            this.gbColumns.Controls.Add(this.label20);
            this.gbColumns.Controls.Add(this.label19);
            this.gbColumns.Controls.Add(this.label17);
            this.gbColumns.Controls.Add(this.label7);
            this.gbColumns.Controls.Add(this.label5);
            this.gbColumns.Controls.Add(this.txtMultiplierHeadingLabel);
            this.gbColumns.Controls.Add(this.txtDiameterListColumn);
            this.gbColumns.Controls.Add(this.txtTotalLengthColumn);
            this.gbColumns.Controls.Add(this.txtShapeColumn);
            this.gbColumns.Controls.Add(this.txtLengthColumn);
            this.gbColumns.Controls.Add(this.txtDiameterColumn);
            this.gbColumns.Controls.Add(this.txtCountCoumn);
            this.gbColumns.Controls.Add(this.txtPosColumn);
            this.gbColumns.Controls.Add(this.txtColumns);
            this.gbColumns.Location = new System.Drawing.Point(317, 13);
            this.gbColumns.Name = "gbColumns";
            this.gbColumns.Size = new System.Drawing.Size(389, 284);
            this.gbColumns.TabIndex = 2;
            this.gbColumns.TabStop = false;
            this.gbColumns.Text = "&Sütunlar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "&Tablo Çarpanı Başlığı";
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
            // txtMultiplierHeadingLabel
            // 
            this.txtMultiplierHeadingLabel.Location = new System.Drawing.Point(149, 246);
            this.txtMultiplierHeadingLabel.Name = "txtMultiplierHeadingLabel";
            this.txtMultiplierHeadingLabel.Size = new System.Drawing.Size(217, 20);
            this.txtMultiplierHeadingLabel.TabIndex = 15;
            this.txtMultiplierHeadingLabel.Validated += new System.EventHandler(this.txtMultiplierHeadingLabel_Validated);
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
            this.gbRows.Controls.Add(this.label27);
            this.gbRows.Controls.Add(this.label26);
            this.gbRows.Controls.Add(this.label25);
            this.gbRows.Controls.Add(this.label24);
            this.gbRows.Controls.Add(this.txtGrossWeightRow);
            this.gbRows.Controls.Add(this.txtWeightRow);
            this.gbRows.Controls.Add(this.txtUnitWeightRow);
            this.gbRows.Controls.Add(this.txtTotalLengthRow);
            this.gbRows.Location = new System.Drawing.Point(317, 303);
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
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "&Yazı Stili";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Ü&stbilgi Yazısı Stili";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "&Altbilgi Yazısı Stili";
            // 
            // cbTextStyle
            // 
            this.cbTextStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextStyle.FormattingEnabled = true;
            this.cbTextStyle.Location = new System.Drawing.Point(149, 26);
            this.cbTextStyle.Name = "cbTextStyle";
            this.cbTextStyle.Size = new System.Drawing.Size(217, 21);
            this.cbTextStyle.TabIndex = 15;
            this.cbTextStyle.SelectedIndexChanged += new System.EventHandler(this.cbTextStyle_SelectedIndexChanged);
            // 
            // cbHeadingStyle
            // 
            this.cbHeadingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeadingStyle.FormattingEnabled = true;
            this.cbHeadingStyle.Location = new System.Drawing.Point(149, 53);
            this.cbHeadingStyle.Name = "cbHeadingStyle";
            this.cbHeadingStyle.Size = new System.Drawing.Size(217, 21);
            this.cbHeadingStyle.TabIndex = 17;
            this.cbHeadingStyle.SelectedIndexChanged += new System.EventHandler(this.cbHeadingStyle_SelectedIndexChanged);
            // 
            // cbFootingStyle
            // 
            this.cbFootingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFootingStyle.FormattingEnabled = true;
            this.cbFootingStyle.Location = new System.Drawing.Point(149, 80);
            this.cbFootingStyle.Name = "cbFootingStyle";
            this.cbFootingStyle.Size = new System.Drawing.Size(217, 21);
            this.cbFootingStyle.TabIndex = 21;
            this.cbFootingStyle.SelectedIndexChanged += new System.EventHandler(this.cbFootingStyle_SelectedIndexChanged);
            // 
            // gbDisplay
            // 
            this.gbDisplay.Controls.Add(this.cbFootingStyle);
            this.gbDisplay.Controls.Add(this.cbHeadingStyle);
            this.gbDisplay.Controls.Add(this.cbTextStyle);
            this.gbDisplay.Controls.Add(this.label6);
            this.gbDisplay.Controls.Add(this.label15);
            this.gbDisplay.Controls.Add(this.label14);
            this.gbDisplay.Location = new System.Drawing.Point(317, 459);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(389, 118);
            this.gbDisplay.TabIndex = 1;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Görünüm &Ayarları";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableView);
            this.groupBox1.Location = new System.Drawing.Point(724, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 564);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ön İzleme";
            // 
            // tableView
            // 
            this.tableView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableView.AutoSize = true;
            this.tableView.BackColor = System.Drawing.Color.Black;
            this.tableView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableView.Columns = null;
            this.tableView.CountLabel = null;
            this.tableView.DiameterLabel = null;
            this.tableView.DiameterLengthLabel = null;
            this.tableView.DiameterListLabel = null;
            this.tableView.GrossWeightLabel = null;
            this.tableView.LengthLabel = null;
            this.tableView.Location = new System.Drawing.Point(21, 33);
            this.tableView.MultiplierHeadingLabel = null;
            this.tableView.Name = "tableView";
            this.tableView.PosLabel = null;
            this.tableView.ShapeLabel = null;
            this.tableView.Size = new System.Drawing.Size(451, 509);
            this.tableView.TabIndex = 0;
            this.tableView.TotalLengthLabel = null;
            this.tableView.UnitWeightLabel = null;
            this.tableView.WeightLabel = null;
            // 
            // TableStyleForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1227, 631);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbRows);
            this.Controls.Add(this.gbColumns);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableStyleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tablo Stilleri";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gbColumns.ResumeLayout(false);
            this.gbColumns.PerformLayout();
            this.gbRows.ResumeLayout(false);
            this.gbRows.PerformLayout();
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lbStyles;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ImageList lGroups;
        private System.Windows.Forms.TextBox txtColumns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider;
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
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.ComboBox cbFootingStyle;
        private System.Windows.Forms.ComboBox cbHeadingStyle;
        private System.Windows.Forms.ComboBox cbTextStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkUserOnly;
        private TableStylePreview tableView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMultiplierHeadingLabel;
    }
}