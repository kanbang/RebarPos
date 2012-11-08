namespace RebarPosCommands
{
    partial class CheckForm
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
            this.chError = new System.Windows.Forms.ColumnHeader();
            this.chCount = new System.Windows.Forms.ColumnHeader();
            this.chCorrect = new System.Windows.Forms.ColumnHeader();
            this.chZoom = new System.Windows.Forms.ColumnHeader();
            this.chSelect = new System.Windows.Forms.ColumnHeader();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbItems
            // 
            this.lbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMarker,
            this.chError,
            this.chCount,
            this.chCorrect,
            this.chZoom,
            this.chSelect});
            this.lbItems.FullRowSelect = true;
            this.lbItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lbItems.Location = new System.Drawing.Point(13, 12);
            this.lbItems.MultiSelect = false;
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(527, 468);
            this.lbItems.TabIndex = 2;
            this.lbItems.UseCompatibleStateImageBehavior = false;
            this.lbItems.View = System.Windows.Forms.View.Details;
            this.lbItems.DoubleClick += new System.EventHandler(this.lbItems_DoubleClick);
            // 
            // chMarker
            // 
            this.chMarker.Text = "Poz No.";
            // 
            // chError
            // 
            this.chError.Text = "Hata Mesajı";
            this.chError.Width = 240;
            // 
            // chCount
            // 
            this.chCount.Text = "Hatalı Poz Adedi";
            this.chCount.Width = 120;
            // 
            // chCorrect
            // 
            this.chCorrect.Text = "";
            this.chCorrect.Width = 26;
            // 
            // chZoom
            // 
            this.chZoom.Text = "";
            this.chZoom.Width = 26;
            // 
            // chSelect
            // 
            this.chSelect.Text = "";
            this.chSelect.Width = 26;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(239, 486);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Kapat";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // CheckForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(552, 521);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poz Kontrol";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lbItems;
        private System.Windows.Forms.ColumnHeader chMarker;
        private System.Windows.Forms.ColumnHeader chError;
        private System.Windows.Forms.ColumnHeader chCount;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader chCorrect;
        private System.Windows.Forms.ColumnHeader chZoom;
        private System.Windows.Forms.ColumnHeader chSelect;
    }
}