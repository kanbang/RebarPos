namespace RebarPosCommands
{
    partial class EditPosForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPosForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbAlignLengthRight = new System.Windows.Forms.RadioButton();
            this.rbAlignLengthBottom = new System.Windows.Forms.RadioButton();
            this.rbAlignLengthTop = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAlignNoteRight = new System.Windows.Forms.RadioButton();
            this.rbAlignNoteBottom = new System.Windows.Forms.RadioButton();
            this.rbAlignNoteTop = new System.Windows.Forms.RadioButton();
            this.chkShowLength = new System.Windows.Forms.CheckBox();
            this.cbPosDiameter = new System.Windows.Forms.ComboBox();
            this.chkIncludePos = new System.Windows.Forms.CheckBox();
            this.btnPickSpacing = new System.Windows.Forms.Button();
            this.btnPickNumber = new System.Windows.Forms.Button();
            this.txtPosMultiplier = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPosNote = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPosSpacing = new RebarPosCommands.SpacingTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPosCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPosMarker = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.posShapeView = new RebarPosCommands.PosShapeView();
            this.btnMeasureF = new System.Windows.Forms.Button();
            this.btnMeasureE = new System.Windows.Forms.Button();
            this.btnMeasureD = new System.Windows.Forms.Button();
            this.btnMeasureC = new System.Windows.Forms.Button();
            this.btnMeasureB = new System.Windows.Forms.Button();
            this.btnMeasureA = new System.Windows.Forms.Button();
            this.btnSelectF = new System.Windows.Forms.Button();
            this.btnSelectE = new System.Windows.Forms.Button();
            this.btnSelectD = new System.Windows.Forms.Button();
            this.btnSelectC = new System.Windows.Forms.Button();
            this.btnSelectB = new System.Windows.Forms.Button();
            this.btnSelectA = new System.Windows.Forms.Button();
            this.txtF = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtE = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtD = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtC = new System.Windows.Forms.TextBox();
            this.lblAverageLengthCaption = new System.Windows.Forms.Label();
            this.lblTotalLengthCaption = new System.Windows.Forms.Label();
            this.lblTotalLength = new System.Windows.Forms.Label();
            this.lblAverageLength = new System.Windows.Forms.Label();
            this.lblPosShape = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnDetach = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.chkShowPieceLengths = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.chkShowLength);
            this.groupBox1.Controls.Add(this.cbPosDiameter);
            this.groupBox1.Controls.Add(this.chkIncludePos);
            this.groupBox1.Controls.Add(this.btnPickSpacing);
            this.groupBox1.Controls.Add(this.btnPickNumber);
            this.groupBox1.Controls.Add(this.txtPosMultiplier);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtPosNote);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtPosSpacing);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPosCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPosMarker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 365);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbAlignLengthRight);
            this.panel2.Controls.Add(this.rbAlignLengthBottom);
            this.panel2.Controls.Add(this.rbAlignLengthTop);
            this.panel2.Location = new System.Drawing.Point(182, 224);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(52, 50);
            this.panel2.TabIndex = 14;
            // 
            // rbAlignLengthRight
            // 
            this.rbAlignLengthRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAlignLengthRight.Image = global::RebarPosCommands.Properties.Resources.arrow_right;
            this.rbAlignLengthRight.Location = new System.Drawing.Point(24, 10);
            this.rbAlignLengthRight.Name = "rbAlignLengthRight";
            this.rbAlignLengthRight.Size = new System.Drawing.Size(24, 24);
            this.rbAlignLengthRight.TabIndex = 2;
            this.rbAlignLengthRight.UseVisualStyleBackColor = true;
            // 
            // rbAlignLengthBottom
            // 
            this.rbAlignLengthBottom.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAlignLengthBottom.Image = global::RebarPosCommands.Properties.Resources.arrow_down;
            this.rbAlignLengthBottom.Location = new System.Drawing.Point(0, 24);
            this.rbAlignLengthBottom.Name = "rbAlignLengthBottom";
            this.rbAlignLengthBottom.Size = new System.Drawing.Size(24, 24);
            this.rbAlignLengthBottom.TabIndex = 1;
            this.rbAlignLengthBottom.UseVisualStyleBackColor = true;
            // 
            // rbAlignLengthTop
            // 
            this.rbAlignLengthTop.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAlignLengthTop.Image = global::RebarPosCommands.Properties.Resources.arrow_up;
            this.rbAlignLengthTop.Location = new System.Drawing.Point(0, 0);
            this.rbAlignLengthTop.Name = "rbAlignLengthTop";
            this.rbAlignLengthTop.Size = new System.Drawing.Size(24, 24);
            this.rbAlignLengthTop.TabIndex = 0;
            this.rbAlignLengthTop.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbAlignNoteRight);
            this.panel1.Controls.Add(this.rbAlignNoteBottom);
            this.panel1.Controls.Add(this.rbAlignNoteTop);
            this.panel1.Location = new System.Drawing.Point(182, 278);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(52, 50);
            this.panel1.TabIndex = 17;
            // 
            // rbAlignNoteRight
            // 
            this.rbAlignNoteRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAlignNoteRight.Image = global::RebarPosCommands.Properties.Resources.arrow_right;
            this.rbAlignNoteRight.Location = new System.Drawing.Point(24, 10);
            this.rbAlignNoteRight.Name = "rbAlignNoteRight";
            this.rbAlignNoteRight.Size = new System.Drawing.Size(24, 24);
            this.rbAlignNoteRight.TabIndex = 2;
            this.rbAlignNoteRight.UseVisualStyleBackColor = true;
            // 
            // rbAlignNoteBottom
            // 
            this.rbAlignNoteBottom.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAlignNoteBottom.Image = global::RebarPosCommands.Properties.Resources.arrow_down;
            this.rbAlignNoteBottom.Location = new System.Drawing.Point(0, 24);
            this.rbAlignNoteBottom.Name = "rbAlignNoteBottom";
            this.rbAlignNoteBottom.Size = new System.Drawing.Size(24, 24);
            this.rbAlignNoteBottom.TabIndex = 1;
            this.rbAlignNoteBottom.UseVisualStyleBackColor = true;
            // 
            // rbAlignNoteTop
            // 
            this.rbAlignNoteTop.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAlignNoteTop.Image = global::RebarPosCommands.Properties.Resources.arrow_up;
            this.rbAlignNoteTop.Location = new System.Drawing.Point(0, 0);
            this.rbAlignNoteTop.Name = "rbAlignNoteTop";
            this.rbAlignNoteTop.Size = new System.Drawing.Size(24, 24);
            this.rbAlignNoteTop.TabIndex = 0;
            this.rbAlignNoteTop.UseVisualStyleBackColor = true;
            // 
            // chkShowLength
            // 
            this.chkShowLength.AutoSize = true;
            this.chkShowLength.Location = new System.Drawing.Point(19, 241);
            this.chkShowLength.Name = "chkShowLength";
            this.chkShowLength.Size = new System.Drawing.Size(122, 17);
            this.chkShowLength.TabIndex = 13;
            this.chkShowLength.Text = "Toplam Boyu Göster";
            this.chkShowLength.UseVisualStyleBackColor = true;
            // 
            // cbPosDiameter
            // 
            this.cbPosDiameter.FormattingEnabled = true;
            this.cbPosDiameter.Location = new System.Drawing.Point(97, 71);
            this.cbPosDiameter.Name = "cbPosDiameter";
            this.cbPosDiameter.Size = new System.Drawing.Size(100, 21);
            this.cbPosDiameter.TabIndex = 6;
            this.cbPosDiameter.Validating += new System.ComponentModel.CancelEventHandler(this.cbPosDiameter_Validating);
            // 
            // chkIncludePos
            // 
            this.chkIncludePos.AutoSize = true;
            this.chkIncludePos.Location = new System.Drawing.Point(19, 157);
            this.chkIncludePos.Name = "chkIncludePos";
            this.chkIncludePos.Size = new System.Drawing.Size(88, 17);
            this.chkIncludePos.TabIndex = 10;
            this.chkIncludePos.Text = "&Metraja Dahil";
            this.chkIncludePos.UseVisualStyleBackColor = true;
            this.chkIncludePos.CheckedChanged += new System.EventHandler(this.chkIncludePos_CheckedChanged);
            // 
            // btnPickSpacing
            // 
            this.btnPickSpacing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickSpacing.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnPickSpacing.Location = new System.Drawing.Point(202, 95);
            this.btnPickSpacing.Name = "btnPickSpacing";
            this.btnPickSpacing.Size = new System.Drawing.Size(24, 24);
            this.btnPickSpacing.TabIndex = 9;
            this.btnPickSpacing.UseVisualStyleBackColor = true;
            this.btnPickSpacing.Click += new System.EventHandler(this.btnPickSpacing_Click);
            // 
            // btnPickNumber
            // 
            this.btnPickNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickNumber.Image = global::RebarPosCommands.Properties.Resources.cursor;
            this.btnPickNumber.Location = new System.Drawing.Point(202, 43);
            this.btnPickNumber.Name = "btnPickNumber";
            this.btnPickNumber.Size = new System.Drawing.Size(24, 24);
            this.btnPickNumber.TabIndex = 4;
            this.btnPickNumber.UseVisualStyleBackColor = true;
            this.btnPickNumber.Click += new System.EventHandler(this.btnPickNumber_Click);
            // 
            // txtPosMultiplier
            // 
            this.txtPosMultiplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosMultiplier.Location = new System.Drawing.Point(97, 180);
            this.txtPosMultiplier.Name = "txtPosMultiplier";
            this.txtPosMultiplier.Size = new System.Drawing.Size(100, 20);
            this.txtPosMultiplier.TabIndex = 12;
            this.txtPosMultiplier.Validating += new System.ComponentModel.CancelEventHandler(this.txtPosMultiplier_Validating);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 183);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Ç&arpan";
            // 
            // txtPosNote
            // 
            this.txtPosNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosNote.Location = new System.Drawing.Point(43, 292);
            this.txtPosNote.Name = "txtPosNote";
            this.txtPosNote.Size = new System.Drawing.Size(129, 20);
            this.txtPosNote.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 295);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "&Not";
            // 
            // txtPosSpacing
            // 
            this.txtPosSpacing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosSpacing.Location = new System.Drawing.Point(97, 98);
            this.txtPosSpacing.Name = "txtPosSpacing";
            this.txtPosSpacing.Size = new System.Drawing.Size(100, 20);
            this.txtPosSpacing.TabIndex = 8;
            this.txtPosSpacing.Validating += new System.ComponentModel.CancelEventHandler(this.txtPosSpacing_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "A&ralık";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ça&p";
            // 
            // txtPosCount
            // 
            this.txtPosCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosCount.Location = new System.Drawing.Point(97, 46);
            this.txtPosCount.Name = "txtPosCount";
            this.txtPosCount.Size = new System.Drawing.Size(100, 20);
            this.txtPosCount.TabIndex = 3;
            this.txtPosCount.Validating += new System.ComponentModel.CancelEventHandler(this.txtPosCount_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "A&det";
            // 
            // txtPosMarker
            // 
            this.txtPosMarker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosMarker.Location = new System.Drawing.Point(97, 20);
            this.txtPosMarker.Name = "txtPosMarker";
            this.txtPosMarker.Size = new System.Drawing.Size(100, 20);
            this.txtPosMarker.TabIndex = 1;
            this.txtPosMarker.Validating += new System.ComponentModel.CancelEventHandler(this.txtPosMarker_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Poz &No";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.posShapeView);
            this.groupBox2.Controls.Add(this.chkShowPieceLengths);
            this.groupBox2.Controls.Add(this.btnMeasureF);
            this.groupBox2.Controls.Add(this.btnMeasureE);
            this.groupBox2.Controls.Add(this.btnMeasureD);
            this.groupBox2.Controls.Add(this.btnMeasureC);
            this.groupBox2.Controls.Add(this.btnMeasureB);
            this.groupBox2.Controls.Add(this.btnMeasureA);
            this.groupBox2.Controls.Add(this.btnSelectF);
            this.groupBox2.Controls.Add(this.btnSelectE);
            this.groupBox2.Controls.Add(this.btnSelectD);
            this.groupBox2.Controls.Add(this.btnSelectC);
            this.groupBox2.Controls.Add(this.btnSelectB);
            this.groupBox2.Controls.Add(this.btnSelectA);
            this.groupBox2.Controls.Add(this.txtF);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtE);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtD);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtC);
            this.groupBox2.Controls.Add(this.lblAverageLengthCaption);
            this.groupBox2.Controls.Add(this.lblTotalLengthCaption);
            this.groupBox2.Controls.Add(this.lblTotalLength);
            this.groupBox2.Controls.Add(this.lblAverageLength);
            this.groupBox2.Controls.Add(this.lblPosShape);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtB);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtA);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(260, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 365);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // posShapeView
            // 
            this.posShapeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.posShapeView.BackColor = System.Drawing.Color.Black;
            this.posShapeView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.posShapeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.posShapeView.ForeColor = System.Drawing.Color.White;
            this.posShapeView.Location = new System.Drawing.Point(22, 19);
            this.posShapeView.Margin = new System.Windows.Forms.Padding(6);
            this.posShapeView.Name = "posShapeView";
            this.posShapeView.Size = new System.Drawing.Size(340, 137);
            this.posShapeView.TabIndex = 0;
            this.posShapeView.Click += new System.EventHandler(this.posShapeView_Click);
            // 
            // btnMeasureF
            // 
            this.btnMeasureF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMeasureF.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnMeasureF.Location = new System.Drawing.Point(338, 243);
            this.btnMeasureF.Name = "btnMeasureF";
            this.btnMeasureF.Size = new System.Drawing.Size(24, 24);
            this.btnMeasureF.TabIndex = 24;
            this.btnMeasureF.UseVisualStyleBackColor = true;
            this.btnMeasureF.Click += new System.EventHandler(this.btnMeasureF_Click);
            // 
            // btnMeasureE
            // 
            this.btnMeasureE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMeasureE.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnMeasureE.Location = new System.Drawing.Point(338, 217);
            this.btnMeasureE.Name = "btnMeasureE";
            this.btnMeasureE.Size = new System.Drawing.Size(24, 24);
            this.btnMeasureE.TabIndex = 20;
            this.btnMeasureE.UseVisualStyleBackColor = true;
            this.btnMeasureE.Click += new System.EventHandler(this.btnMeasureE_Click);
            // 
            // btnMeasureD
            // 
            this.btnMeasureD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMeasureD.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnMeasureD.Location = new System.Drawing.Point(338, 191);
            this.btnMeasureD.Name = "btnMeasureD";
            this.btnMeasureD.Size = new System.Drawing.Size(24, 24);
            this.btnMeasureD.TabIndex = 16;
            this.btnMeasureD.UseVisualStyleBackColor = true;
            this.btnMeasureD.Click += new System.EventHandler(this.btnMeasureD_Click);
            // 
            // btnMeasureC
            // 
            this.btnMeasureC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMeasureC.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnMeasureC.Location = new System.Drawing.Point(158, 243);
            this.btnMeasureC.Name = "btnMeasureC";
            this.btnMeasureC.Size = new System.Drawing.Size(24, 24);
            this.btnMeasureC.TabIndex = 12;
            this.btnMeasureC.UseVisualStyleBackColor = true;
            this.btnMeasureC.Click += new System.EventHandler(this.btnMeasureC_Click);
            // 
            // btnMeasureB
            // 
            this.btnMeasureB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMeasureB.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnMeasureB.Location = new System.Drawing.Point(158, 217);
            this.btnMeasureB.Name = "btnMeasureB";
            this.btnMeasureB.Size = new System.Drawing.Size(24, 24);
            this.btnMeasureB.TabIndex = 8;
            this.btnMeasureB.UseVisualStyleBackColor = true;
            this.btnMeasureB.Click += new System.EventHandler(this.btnMeasureB_Click);
            // 
            // btnMeasureA
            // 
            this.btnMeasureA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMeasureA.Image = global::RebarPosCommands.Properties.Resources.dimension;
            this.btnMeasureA.Location = new System.Drawing.Point(158, 191);
            this.btnMeasureA.Name = "btnMeasureA";
            this.btnMeasureA.Size = new System.Drawing.Size(24, 24);
            this.btnMeasureA.TabIndex = 4;
            this.btnMeasureA.UseVisualStyleBackColor = true;
            this.btnMeasureA.Click += new System.EventHandler(this.btnMeasureA_Click);
            // 
            // btnSelectF
            // 
            this.btnSelectF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectF.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnSelectF.Location = new System.Drawing.Point(308, 243);
            this.btnSelectF.Name = "btnSelectF";
            this.btnSelectF.Size = new System.Drawing.Size(24, 24);
            this.btnSelectF.TabIndex = 23;
            this.btnSelectF.UseVisualStyleBackColor = true;
            this.btnSelectF.Click += new System.EventHandler(this.btnSelectF_Click);
            // 
            // btnSelectE
            // 
            this.btnSelectE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectE.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnSelectE.Location = new System.Drawing.Point(308, 217);
            this.btnSelectE.Name = "btnSelectE";
            this.btnSelectE.Size = new System.Drawing.Size(24, 24);
            this.btnSelectE.TabIndex = 19;
            this.btnSelectE.UseVisualStyleBackColor = true;
            this.btnSelectE.Click += new System.EventHandler(this.btnSelectE_Click);
            // 
            // btnSelectD
            // 
            this.btnSelectD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectD.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnSelectD.Location = new System.Drawing.Point(308, 191);
            this.btnSelectD.Name = "btnSelectD";
            this.btnSelectD.Size = new System.Drawing.Size(24, 24);
            this.btnSelectD.TabIndex = 15;
            this.btnSelectD.UseVisualStyleBackColor = true;
            this.btnSelectD.Click += new System.EventHandler(this.btnSelectD_Click);
            // 
            // btnSelectC
            // 
            this.btnSelectC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectC.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnSelectC.Location = new System.Drawing.Point(128, 243);
            this.btnSelectC.Name = "btnSelectC";
            this.btnSelectC.Size = new System.Drawing.Size(24, 24);
            this.btnSelectC.TabIndex = 11;
            this.btnSelectC.UseVisualStyleBackColor = true;
            this.btnSelectC.Click += new System.EventHandler(this.btnSelectC_Click);
            // 
            // btnSelectB
            // 
            this.btnSelectB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectB.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnSelectB.Location = new System.Drawing.Point(128, 217);
            this.btnSelectB.Name = "btnSelectB";
            this.btnSelectB.Size = new System.Drawing.Size(24, 24);
            this.btnSelectB.TabIndex = 7;
            this.btnSelectB.UseVisualStyleBackColor = true;
            this.btnSelectB.Click += new System.EventHandler(this.btnSelectB_Click);
            // 
            // btnSelectA
            // 
            this.btnSelectA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectA.Image = global::RebarPosCommands.Properties.Resources.textfield;
            this.btnSelectA.Location = new System.Drawing.Point(128, 191);
            this.btnSelectA.Name = "btnSelectA";
            this.btnSelectA.Size = new System.Drawing.Size(24, 24);
            this.btnSelectA.TabIndex = 3;
            this.btnSelectA.UseVisualStyleBackColor = true;
            this.btnSelectA.Click += new System.EventHandler(this.btnSelectA_Click);
            // 
            // txtF
            // 
            this.txtF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtF.Location = new System.Drawing.Point(232, 246);
            this.txtF.Name = "txtF";
            this.txtF.Size = new System.Drawing.Size(70, 20);
            this.txtF.TabIndex = 22;
            this.txtF.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(199, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "&F";
            // 
            // txtE
            // 
            this.txtE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtE.Location = new System.Drawing.Point(232, 220);
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(70, 20);
            this.txtE.TabIndex = 18;
            this.txtE.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "&E";
            // 
            // txtD
            // 
            this.txtD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtD.Location = new System.Drawing.Point(232, 194);
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(70, 20);
            this.txtD.TabIndex = 14;
            this.txtD.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "&D";
            // 
            // txtC
            // 
            this.txtC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtC.Location = new System.Drawing.Point(52, 246);
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(70, 20);
            this.txtC.TabIndex = 10;
            this.txtC.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // lblAverageLengthCaption
            // 
            this.lblAverageLengthCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAverageLengthCaption.AutoSize = true;
            this.lblAverageLengthCaption.Location = new System.Drawing.Point(19, 336);
            this.lblAverageLengthCaption.Name = "lblAverageLengthCaption";
            this.lblAverageLengthCaption.Size = new System.Drawing.Size(73, 13);
            this.lblAverageLengthCaption.TabIndex = 0;
            this.lblAverageLengthCaption.Text = "Ortalama Boy:";
            // 
            // lblTotalLengthCaption
            // 
            this.lblTotalLengthCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalLengthCaption.AutoSize = true;
            this.lblTotalLengthCaption.Location = new System.Drawing.Point(19, 310);
            this.lblTotalLengthCaption.Name = "lblTotalLengthCaption";
            this.lblTotalLengthCaption.Size = new System.Drawing.Size(66, 13);
            this.lblTotalLengthCaption.TabIndex = 0;
            this.lblTotalLengthCaption.Text = "Toplam Boy:";
            // 
            // lblTotalLength
            // 
            this.lblTotalLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalLength.AutoSize = true;
            this.lblTotalLength.Location = new System.Drawing.Point(125, 310);
            this.lblTotalLength.Name = "lblTotalLength";
            this.lblTotalLength.Size = new System.Drawing.Size(14, 13);
            this.lblTotalLength.TabIndex = 0;
            this.lblTotalLength.Text = "#";
            // 
            // lblAverageLength
            // 
            this.lblAverageLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAverageLength.AutoSize = true;
            this.lblAverageLength.Location = new System.Drawing.Point(125, 336);
            this.lblAverageLength.Name = "lblAverageLength";
            this.lblAverageLength.Size = new System.Drawing.Size(14, 13);
            this.lblAverageLength.TabIndex = 0;
            this.lblAverageLength.Text = "#";
            // 
            // lblPosShape
            // 
            this.lblPosShape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPosShape.AutoSize = true;
            this.lblPosShape.Location = new System.Drawing.Point(125, 284);
            this.lblPosShape.Name = "lblPosShape";
            this.lblPosShape.Size = new System.Drawing.Size(14, 13);
            this.lblPosShape.TabIndex = 0;
            this.lblPosShape.Text = "#";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Poz Şekli:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "&C";
            // 
            // txtB
            // 
            this.txtB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtB.Location = new System.Drawing.Point(52, 220);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(70, 20);
            this.txtB.TabIndex = 6;
            this.txtB.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "&B";
            // 
            // txtA
            // 
            this.txtA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtA.Location = new System.Drawing.Point(52, 194);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(70, 20);
            this.txtA.TabIndex = 2;
            this.txtA.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "&A";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(483, 393);
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
            this.btnCancel.Location = new System.Drawing.Point(564, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // btnDetach
            // 
            this.btnDetach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDetach.Location = new System.Drawing.Point(12, 393);
            this.btnDetach.Name = "btnDetach";
            this.btnDetach.Size = new System.Drawing.Size(133, 23);
            this.btnDetach.TabIndex = 5;
            this.btnDetach.Text = "Boş Poz Bloğuna Çevir";
            this.btnDetach.UseVisualStyleBackColor = true;
            this.btnDetach.Click += new System.EventHandler(this.btnDetach_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlign.Location = new System.Drawing.Point(151, 393);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(75, 23);
            this.btnAlign.TabIndex = 3;
            this.btnAlign.Text = "Hizala";
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // chkShowPieceLengths
            // 
            this.chkShowPieceLengths.AutoSize = true;
            this.chkShowPieceLengths.Location = new System.Drawing.Point(22, 168);
            this.chkShowPieceLengths.Name = "chkShowPieceLengths";
            this.chkShowPieceLengths.Size = new System.Drawing.Size(142, 17);
            this.chkShowPieceLengths.TabIndex = 25;
            this.chkShowPieceLengths.Text = "&Parça boylarını görüntüle";
            this.chkShowPieceLengths.UseVisualStyleBackColor = true;
            this.chkShowPieceLengths.CheckedChanged += new System.EventHandler(this.chkShowPieceLengths_CheckedChanged);
            // 
            // EditPosForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(653, 428);
            this.Controls.Add(this.btnDetach);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAlign);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditPosForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poz Edit";
            this.Shown += new System.EventHandler(this.EditPosForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPickNumber;
        private SpacingTextBox txtPosSpacing;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPosCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPosMarker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPickSpacing;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectA;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkIncludePos;
        private System.Windows.Forms.TextBox txtPosMultiplier;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnMeasureF;
        private System.Windows.Forms.Button btnMeasureE;
        private System.Windows.Forms.Button btnMeasureD;
        private System.Windows.Forms.Button btnMeasureC;
        private System.Windows.Forms.Button btnMeasureB;
        private System.Windows.Forms.Button btnMeasureA;
        private System.Windows.Forms.Button btnSelectF;
        private System.Windows.Forms.Button btnSelectE;
        private System.Windows.Forms.Button btnSelectD;
        private System.Windows.Forms.Button btnSelectC;
        private System.Windows.Forms.Button btnSelectB;
        private System.Windows.Forms.TextBox txtF;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtE;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.Label lblAverageLengthCaption;
        private System.Windows.Forms.Label lblTotalLengthCaption;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPosNote;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private PosShapeView posShapeView;
        private System.Windows.Forms.Label lblTotalLength;
        private System.Windows.Forms.Label lblAverageLength;
        private System.Windows.Forms.Label lblPosShape;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox cbPosDiameter;
        private System.Windows.Forms.Button btnDetach;
        private System.Windows.Forms.CheckBox chkShowLength;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbAlignNoteRight;
        private System.Windows.Forms.RadioButton rbAlignNoteBottom;
        private System.Windows.Forms.RadioButton rbAlignNoteTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbAlignLengthRight;
        private System.Windows.Forms.RadioButton rbAlignLengthBottom;
        private System.Windows.Forms.RadioButton rbAlignLengthTop;
        private System.Windows.Forms.Button btnAlign;
        private System.Windows.Forms.CheckBox chkShowPieceLengths;
    }
}