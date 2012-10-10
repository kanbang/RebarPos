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
            this.txtDiameterList = new System.Windows.Forms.TextBox();
            this.txtMaxLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBending = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lGroups = new System.Windows.Forms.ImageList(this.components);
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.posStylePreview = new RebarPosCommands.PosStylePreview();
            this.btnPickCountColor = new System.Windows.Forms.Button();
            this.btnPickGroupColor = new System.Windows.Forms.Button();
            this.cbNoteStyle = new System.Windows.Forms.ComboBox();
            this.cbTextStyle = new System.Windows.Forms.ComboBox();
            this.btnPickMultiplierColor = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnPickNoteColor = new System.Windows.Forms.Button();
            this.btnPickCircleColor = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
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
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).BeginInit();
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
            this.gbOptions.Location = new System.Drawing.Point(12, 12);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(605, 244);
            this.gbOptions.TabIndex = 0;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "S&eçenekler";
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
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 178);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "&Standart Çap Listesi:";
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
            this.chkBending.Size = new System.Drawing.Size(284, 17);
            this.chkBending.TabIndex = 8;
            this.chkBending.Text = "&Toplam Boy Hesaplarken Demir Bükümlerini Dikkate Al";
            this.chkBending.UseVisualStyleBackColor = true;
            this.chkBending.CheckedChanged += new System.EventHandler(this.chkBending_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(461, 613);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(542, 613);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // gbDisplay
            // 
            this.gbDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDisplay.Controls.Add(this.posStylePreview);
            this.gbDisplay.Controls.Add(this.btnPickCountColor);
            this.gbDisplay.Controls.Add(this.btnPickGroupColor);
            this.gbDisplay.Controls.Add(this.cbNoteStyle);
            this.gbDisplay.Controls.Add(this.cbTextStyle);
            this.gbDisplay.Controls.Add(this.btnPickMultiplierColor);
            this.gbDisplay.Controls.Add(this.label13);
            this.gbDisplay.Controls.Add(this.label15);
            this.gbDisplay.Controls.Add(this.label12);
            this.gbDisplay.Controls.Add(this.label14);
            this.gbDisplay.Controls.Add(this.btnPickNoteColor);
            this.gbDisplay.Controls.Add(this.btnPickCircleColor);
            this.gbDisplay.Controls.Add(this.label11);
            this.gbDisplay.Controls.Add(this.label18);
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
            this.gbDisplay.Location = new System.Drawing.Point(12, 262);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(605, 327);
            this.gbDisplay.TabIndex = 1;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Görünüm &Ayarları";
            // 
            // posStylePreview
            // 
            this.posStylePreview.BackColor = System.Drawing.Color.Black;
            this.posStylePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.posStylePreview.CircleColor = System.Drawing.Color.Yellow;
            this.posStylePreview.CurrentGroupHighlightColor = System.Drawing.Color.Silver;
            this.posStylePreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.posStylePreview.Formula1 = "[M:C][N][\"T\":D][\"/\":S]";
            this.posStylePreview.Formula2 = "[\" L=\":L]";
            this.posStylePreview.Formula3 = "[M:C]";
            this.posStylePreview.GroupColor = System.Drawing.Color.Gray;
            this.posStylePreview.Location = new System.Drawing.Point(314, 192);
            this.posStylePreview.MultiplierColor = System.Drawing.Color.Gray;
            this.posStylePreview.Name = "posStylePreview";
            this.posStylePreview.NoteColor = System.Drawing.Color.Orange;
            this.posStylePreview.PosColor = System.Drawing.Color.Red;
            this.posStylePreview.Size = new System.Drawing.Size(268, 114);
            this.posStylePreview.TabIndex = 26;
            this.posStylePreview.TextColor = System.Drawing.Color.Red;
            // 
            // btnPickCountColor
            // 
            this.btnPickCountColor.BackColor = System.Drawing.Color.White;
            this.btnPickCountColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickCountColor.Location = new System.Drawing.Point(188, 285);
            this.btnPickCountColor.Name = "btnPickCountColor";
            this.btnPickCountColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickCountColor.TabIndex = 19;
            this.btnPickCountColor.UseVisualStyleBackColor = false;
            this.btnPickCountColor.Click += new System.EventHandler(this.btnPickCountColor_Click);
            // 
            // btnPickGroupColor
            // 
            this.btnPickGroupColor.BackColor = System.Drawing.Color.White;
            this.btnPickGroupColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickGroupColor.Location = new System.Drawing.Point(188, 256);
            this.btnPickGroupColor.Name = "btnPickGroupColor";
            this.btnPickGroupColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickGroupColor.TabIndex = 17;
            this.btnPickGroupColor.UseVisualStyleBackColor = false;
            this.btnPickGroupColor.Click += new System.EventHandler(this.btnPickGroupColor_Click);
            // 
            // cbNoteStyle
            // 
            this.cbNoteStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNoteStyle.FormattingEnabled = true;
            this.cbNoteStyle.Location = new System.Drawing.Point(433, 137);
            this.cbNoteStyle.Name = "cbNoteStyle";
            this.cbNoteStyle.Size = new System.Drawing.Size(149, 21);
            this.cbNoteStyle.TabIndex = 23;
            this.cbNoteStyle.SelectedIndexChanged += new System.EventHandler(this.cbNoteStyle_SelectedIndexChanged);
            // 
            // cbTextStyle
            // 
            this.cbTextStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextStyle.FormattingEnabled = true;
            this.cbTextStyle.Location = new System.Drawing.Point(433, 111);
            this.cbTextStyle.Name = "cbTextStyle";
            this.cbTextStyle.Size = new System.Drawing.Size(149, 21);
            this.cbTextStyle.TabIndex = 21;
            this.cbTextStyle.SelectedIndexChanged += new System.EventHandler(this.cbTextStyle_SelectedIndexChanged);
            // 
            // btnPickMultiplierColor
            // 
            this.btnPickMultiplierColor.BackColor = System.Drawing.Color.White;
            this.btnPickMultiplierColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickMultiplierColor.Location = new System.Drawing.Point(188, 227);
            this.btnPickMultiplierColor.Name = "btnPickMultiplierColor";
            this.btnPickMultiplierColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickMultiplierColor.TabIndex = 15;
            this.btnPickMultiplierColor.UseVisualStyleBackColor = false;
            this.btnPickMultiplierColor.Click += new System.EventHandler(this.btnPickMultiplierColor_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 290);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Metraj &Dışı Poz Rengi";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(311, 140);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "&Not Yazısı Stili";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 261);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Poz &Grubu Rengi";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(311, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "&Yazı Stili";
            // 
            // btnPickNoteColor
            // 
            this.btnPickNoteColor.BackColor = System.Drawing.Color.White;
            this.btnPickNoteColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickNoteColor.Location = new System.Drawing.Point(188, 198);
            this.btnPickNoteColor.Name = "btnPickNoteColor";
            this.btnPickNoteColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickNoteColor.TabIndex = 13;
            this.btnPickNoteColor.UseVisualStyleBackColor = false;
            this.btnPickNoteColor.Click += new System.EventHandler(this.btnPickNoteColor_Click);
            // 
            // btnPickCircleColor
            // 
            this.btnPickCircleColor.BackColor = System.Drawing.Color.White;
            this.btnPickCircleColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickCircleColor.Location = new System.Drawing.Point(188, 169);
            this.btnPickCircleColor.Name = "btnPickCircleColor";
            this.btnPickCircleColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickCircleColor.TabIndex = 11;
            this.btnPickCircleColor.UseVisualStyleBackColor = false;
            this.btnPickCircleColor.Click += new System.EventHandler(this.btnPickCircleColor_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Poz Ç&arpanı Rengi";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 203);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 13);
            this.label18.TabIndex = 12;
            this.label18.Text = "&Not Yazısı Rengi";
            // 
            // btnPickPosColor
            // 
            this.btnPickPosColor.BackColor = System.Drawing.Color.White;
            this.btnPickPosColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickPosColor.Location = new System.Drawing.Point(188, 140);
            this.btnPickPosColor.Name = "btnPickPosColor";
            this.btnPickPosColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickPosColor.TabIndex = 9;
            this.btnPickPosColor.UseVisualStyleBackColor = false;
            this.btnPickPosColor.Click += new System.EventHandler(this.btnPickPosColor_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Poz &Dairesi Rengi";
            // 
            // btnPickTextColor
            // 
            this.btnPickTextColor.BackColor = System.Drawing.Color.White;
            this.btnPickTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickTextColor.Location = new System.Drawing.Point(188, 111);
            this.btnPickTextColor.Name = "btnPickTextColor";
            this.btnPickTextColor.Size = new System.Drawing.Size(100, 23);
            this.btnPickTextColor.TabIndex = 7;
            this.btnPickTextColor.UseVisualStyleBackColor = false;
            this.btnPickTextColor.Click += new System.EventHandler(this.btnPickTextColor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "&Poz Numarası Rengi";
            // 
            // txtNoteScale
            // 
            this.txtNoteScale.Location = new System.Drawing.Point(433, 163);
            this.txtNoteScale.Name = "txtNoteScale";
            this.txtNoteScale.Size = new System.Drawing.Size(149, 20);
            this.txtNoteScale.TabIndex = 25;
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
            this.label16.Location = new System.Drawing.Point(311, 166);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 13);
            this.label16.TabIndex = 24;
            this.label16.Text = "Not Yazısı Ö&lçeği";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 116);
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
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Formül (&L Boyu)";
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
            // GroupForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(629, 651);
            this.Controls.Add(this.gbDisplay);
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
        private System.Windows.Forms.ImageList lGroups;
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
        private System.Windows.Forms.Button btnPickNoteColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnPickCountColor;
        private System.Windows.Forms.Label label13;
    }
}