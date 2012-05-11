namespace RebarPosCommands
{
    partial class PosShapesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PosShapesForm));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.udPriority = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.udFields = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFormulaBending = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbShapes = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.lShapes = new System.Windows.Forms.ImageList(this.components);
            this.btnRename = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.btnSelectShape = new System.Windows.Forms.Button();
            this.posShapeView = new RebarPosCommands.PosShapeView();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkShowShapes = new System.Windows.Forms.CheckBox();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFields)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.udPriority);
            this.gbOptions.Controls.Add(this.label3);
            this.gbOptions.Controls.Add(this.udFields);
            this.gbOptions.Controls.Add(this.label4);
            this.gbOptions.Controls.Add(this.txtFormulaBending);
            this.gbOptions.Controls.Add(this.label2);
            this.gbOptions.Controls.Add(this.txtFormula);
            this.gbOptions.Controls.Add(this.label1);
            this.gbOptions.Location = new System.Drawing.Point(306, 12);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(311, 194);
            this.gbOptions.TabIndex = 1;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "S&eçenekler";
            // 
            // udPriority
            // 
            this.udPriority.Location = new System.Drawing.Point(122, 147);
            this.udPriority.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.udPriority.Name = "udPriority";
            this.udPriority.Size = new System.Drawing.Size(166, 20);
            this.udPriority.TabIndex = 7;
            this.udPriority.ValueChanged += new System.EventHandler(this.udPriority_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ö&ncelik";
            // 
            // udFields
            // 
            this.udFields.Location = new System.Drawing.Point(122, 27);
            this.udFields.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.udFields.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udFields.Name = "udFields";
            this.udFields.Size = new System.Drawing.Size(166, 20);
            this.udFields.TabIndex = 1;
            this.udFields.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udFields.ValueChanged += new System.EventHandler(this.udFields_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Parça &Sayısı";
            // 
            // txtFormulaBending
            // 
            this.txtFormulaBending.Location = new System.Drawing.Point(122, 100);
            this.txtFormulaBending.Name = "txtFormulaBending";
            this.txtFormulaBending.Size = new System.Drawing.Size(166, 20);
            this.txtFormulaBending.TabIndex = 5;
            this.txtFormulaBending.Validated += new System.EventHandler(this.txtFormulaBending_Validated);
            this.txtFormulaBending.Validating += new System.ComponentModel.CancelEventHandler(this.txtFormulaBending_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "&Bükümlü Formül";
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(122, 74);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(166, 20);
            this.txtFormula.TabIndex = 3;
            this.txtFormula.Validated += new System.EventHandler(this.txtFormula_Validated);
            this.txtFormula.Validating += new System.ComponentModel.CancelEventHandler(this.txtFormula_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Formül";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(461, 437);
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
            this.btnCancel.Location = new System.Drawing.Point(542, 437);
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
            this.groupBox2.Controls.Add(this.lbShapes);
            this.groupBox2.Controls.Add(this.btnRename);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 405);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Donatı Açılımları";
            // 
            // lbShapes
            // 
            this.lbShapes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbShapes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lbShapes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lbShapes.LabelEdit = true;
            this.lbShapes.LabelWrap = false;
            this.lbShapes.LargeImageList = this.lShapes;
            this.lbShapes.Location = new System.Drawing.Point(15, 23);
            this.lbShapes.MultiSelect = false;
            this.lbShapes.Name = "lbShapes";
            this.lbShapes.Size = new System.Drawing.Size(227, 363);
            this.lbShapes.SmallImageList = this.lShapes;
            this.lbShapes.TabIndex = 0;
            this.lbShapes.UseCompatibleStateImageBehavior = false;
            this.lbShapes.View = System.Windows.Forms.View.Details;
            this.lbShapes.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lbShapes_AfterLabelEdit);
            this.lbShapes.SelectedIndexChanged += new System.EventHandler(this.lbShapes_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 130;
            // 
            // lShapes
            // 
            this.lShapes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lShapes.ImageStream")));
            this.lShapes.TransparentColor = System.Drawing.Color.Transparent;
            this.lShapes.Images.SetKeyName(0, "page.png");
            this.lShapes.Images.SetKeyName(1, "page_white.png");
            this.lShapes.Images.SetKeyName(2, "page_white_add.png");
            this.lShapes.Images.SetKeyName(3, "page_white_delete.png");
            this.lShapes.Images.SetKeyName(4, "page_go.png");
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
            this.gbDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDisplay.Controls.Add(this.btnSelectShape);
            this.gbDisplay.Controls.Add(this.posShapeView);
            this.gbDisplay.Location = new System.Drawing.Point(306, 212);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(311, 205);
            this.gbDisplay.TabIndex = 2;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "&Görünüm";
            // 
            // btnSelectShape
            // 
            this.btnSelectShape.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectShape.Location = new System.Drawing.Point(20, 158);
            this.btnSelectShape.Name = "btnSelectShape";
            this.btnSelectShape.Size = new System.Drawing.Size(268, 23);
            this.btnSelectShape.TabIndex = 1;
            this.btnSelectShape.Text = "Çizimden &Al";
            this.btnSelectShape.UseVisualStyleBackColor = true;
            this.btnSelectShape.Click += new System.EventHandler(this.btnSelectShape_Click);
            // 
            // posShapeView
            // 
            this.posShapeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.posShapeView.BackColor = System.Drawing.Color.Black;
            this.posShapeView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.posShapeView.ForeColor = System.Drawing.Color.White;
            this.posShapeView.Location = new System.Drawing.Point(20, 31);
            this.posShapeView.Name = "posShapeView";
            this.posShapeView.Size = new System.Drawing.Size(268, 121);
            this.posShapeView.TabIndex = 0;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // chkShowShapes
            // 
            this.chkShowShapes.AutoSize = true;
            this.chkShowShapes.Location = new System.Drawing.Point(12, 441);
            this.chkShowShapes.Name = "chkShowShapes";
            this.chkShowShapes.Size = new System.Drawing.Size(172, 17);
            this.chkShowShapes.TabIndex = 5;
            this.chkShowShapes.Text = "Poz Açılımlarını Ekranda Göster";
            this.chkShowShapes.UseVisualStyleBackColor = true;
            // 
            // PosShapesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(629, 475);
            this.Controls.Add(this.chkShowShapes);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PosShapesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Donatı Açılımları";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFields)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.gbDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udFields;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lbShapes;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ImageList lShapes;
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.NumericUpDown udPriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFormulaBending;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectShape;
        private PosShapeView posShapeView;
        private System.Windows.Forms.CheckBox chkShowShapes;
    }
}