﻿namespace RebarPosCommands
{
    partial class EditBOQForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBOQForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.udMultiplier = new System.Windows.Forms.NumericUpDown();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTableMargin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTableRows = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTextHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbPrecision = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbDisplayUnit = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMultiplier)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbPrecision);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbDisplayUnit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.udMultiplier);
            this.groupBox1.Controls.Add(this.txtFooter);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtHeader);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Tablo Seçenekleri";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(159, 128);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(234, 20);
            this.txtNote.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "&Not";
            // 
            // udMultiplier
            // 
            this.udMultiplier.Location = new System.Drawing.Point(159, 90);
            this.udMultiplier.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.udMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udMultiplier.Name = "udMultiplier";
            this.udMultiplier.Size = new System.Drawing.Size(100, 20);
            this.udMultiplier.TabIndex = 5;
            this.udMultiplier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(159, 180);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(234, 20);
            this.txtFooter.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "&Altbilgi";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(159, 154);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(234, 20);
            this.txtHeader.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ü&stbilgi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Çarpa&n";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(357, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(276, 369);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTableMargin);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtTableRows);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTextHeight);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(13, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 121);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tablo &Yerleşimi";
            // 
            // txtTableMargin
            // 
            this.txtTableMargin.Location = new System.Drawing.Point(158, 83);
            this.txtTableMargin.Name = "txtTableMargin";
            this.txtTableMargin.Size = new System.Drawing.Size(100, 20);
            this.txtTableMargin.TabIndex = 5;
            this.txtTableMargin.Text = "50";
            this.txtTableMargin.Validating += new System.ComponentModel.CancelEventHandler(this.txtTableMargin_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Tablo &Aralığı";
            // 
            // txtTableRows
            // 
            this.txtTableRows.Location = new System.Drawing.Point(158, 57);
            this.txtTableRows.Name = "txtTableRows";
            this.txtTableRows.Size = new System.Drawing.Size(100, 20);
            this.txtTableRows.TabIndex = 3;
            this.txtTableRows.Text = "0";
            this.txtTableRows.Validating += new System.ComponentModel.CancelEventHandler(this.txtTableRows_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "&Satır Sayısı";
            // 
            // txtTextHeight
            // 
            this.txtTextHeight.Location = new System.Drawing.Point(158, 31);
            this.txtTextHeight.Name = "txtTextHeight";
            this.txtTextHeight.Size = new System.Drawing.Size(100, 20);
            this.txtTextHeight.TabIndex = 1;
            this.txtTextHeight.Text = "25";
            this.txtTextHeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtTextHeight_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "&Yazı Yüksekliği";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // cbPrecision
            // 
            this.cbPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrecision.FormattingEnabled = true;
            this.cbPrecision.Items.AddRange(new object[] {
            "0.",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000",
            "0.0000000",
            "0.00000000"});
            this.cbPrecision.Location = new System.Drawing.Point(159, 52);
            this.cbPrecision.Name = "cbPrecision";
            this.cbPrecision.Size = new System.Drawing.Size(100, 21);
            this.cbPrecision.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "&Basamak Sayısı";
            // 
            // cbDisplayUnit
            // 
            this.cbDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplayUnit.FormattingEnabled = true;
            this.cbDisplayUnit.Items.AddRange(new object[] {
            "Milimetre",
            "Santimetre"});
            this.cbDisplayUnit.Location = new System.Drawing.Point(159, 25);
            this.cbDisplayUnit.Name = "cbDisplayUnit";
            this.cbDisplayUnit.Size = new System.Drawing.Size(100, 21);
            this.cbDisplayUnit.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "&Görüntülenen Birim";
            // 
            // EditBOQForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(444, 404);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditBOQForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metraj Tablosu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMultiplier)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown udMultiplier;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTableMargin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTableRows;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTextHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbPrecision;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbDisplayUnit;
        private System.Windows.Forms.Label label9;
    }
}