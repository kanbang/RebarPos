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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lbSubItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnSelectSubItem = new System.Windows.Forms.Button();
            this.btnZoomSubItem = new System.Windows.Forms.Button();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.btnSelectAllSubItem = new System.Windows.Forms.Button();
            this.lblSelectHint = new System.Windows.Forms.Label();
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
            this.chCount});
            this.lbItems.FullRowSelect = true;
            this.lbItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lbItems.Location = new System.Drawing.Point(13, 12);
            this.lbItems.MultiSelect = false;
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(589, 273);
            this.lbItems.TabIndex = 2;
            this.lbItems.UseCompatibleStateImageBehavior = false;
            this.lbItems.View = System.Windows.Forms.View.Details;
            this.lbItems.SelectedIndexChanged += new System.EventHandler(this.lbItems_SelectedIndexChanged);
            this.lbItems.DoubleClick += new System.EventHandler(this.lbItems_DoubleClick);
            // 
            // chMarker
            // 
            this.chMarker.Text = "No.";
            // 
            // chError
            // 
            this.chError.Text = "Hata Mesajı";
            this.chError.Width = 400;
            // 
            // chCount
            // 
            this.chCount.Text = "Hatalı Poz Adedi";
            this.chCount.Width = 100;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(291, 486);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Kapat";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoom.Image = global::RebarPosCommands.Properties.Resources.zoomtolist;
            this.btnZoom.Location = new System.Drawing.Point(620, 12);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(24, 24);
            this.btnZoom.TabIndex = 3;
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Image = global::RebarPosCommands.Properties.Resources.selectlist;
            this.btnSelect.Location = new System.Drawing.Point(620, 42);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(24, 24);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.Image = global::RebarPosCommands.Properties.Resources.selectalllist;
            this.btnSelectAll.Location = new System.Drawing.Point(620, 72);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(24, 24);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lbSubItems
            // 
            this.lbSubItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSubItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.lbSubItems.FullRowSelect = true;
            this.lbSubItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lbSubItems.Location = new System.Drawing.Point(13, 291);
            this.lbSubItems.MultiSelect = false;
            this.lbSubItems.Name = "lbSubItems";
            this.lbSubItems.Size = new System.Drawing.Size(589, 189);
            this.lbSubItems.TabIndex = 7;
            this.lbSubItems.UseCompatibleStateImageBehavior = false;
            this.lbSubItems.View = System.Windows.Forms.View.Details;
            this.lbSubItems.SelectedIndexChanged += new System.EventHandler(this.lbSubItems_SelectedIndexChanged);
            this.lbSubItems.DoubleClick += new System.EventHandler(this.lbSubItems_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Çap";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Şekil";
            // 
            // btnSelectSubItem
            // 
            this.btnSelectSubItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSubItem.Image = global::RebarPosCommands.Properties.Resources.selectlist;
            this.btnSelectSubItem.Location = new System.Drawing.Point(620, 321);
            this.btnSelectSubItem.Name = "btnSelectSubItem";
            this.btnSelectSubItem.Size = new System.Drawing.Size(24, 24);
            this.btnSelectSubItem.TabIndex = 9;
            this.btnSelectSubItem.UseVisualStyleBackColor = true;
            this.btnSelectSubItem.Click += new System.EventHandler(this.btnSelectSubItem_Click);
            // 
            // btnZoomSubItem
            // 
            this.btnZoomSubItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomSubItem.Image = global::RebarPosCommands.Properties.Resources.zoomtolist;
            this.btnZoomSubItem.Location = new System.Drawing.Point(620, 291);
            this.btnZoomSubItem.Name = "btnZoomSubItem";
            this.btnZoomSubItem.Size = new System.Drawing.Size(24, 24);
            this.btnZoomSubItem.TabIndex = 8;
            this.btnZoomSubItem.UseVisualStyleBackColor = true;
            this.btnZoomSubItem.Click += new System.EventHandler(this.btnZoomSubItem_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "A";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "B";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "C";
            this.columnHeader6.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "D";
            this.columnHeader7.Width = 50;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "E";
            this.columnHeader8.Width = 50;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "F";
            this.columnHeader9.Width = 50;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Toplam Boy";
            this.columnHeader10.Width = 80;
            // 
            // btnSelectAllSubItem
            // 
            this.btnSelectAllSubItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAllSubItem.Image = global::RebarPosCommands.Properties.Resources.selectalllist;
            this.btnSelectAllSubItem.Location = new System.Drawing.Point(620, 351);
            this.btnSelectAllSubItem.Name = "btnSelectAllSubItem";
            this.btnSelectAllSubItem.Size = new System.Drawing.Size(24, 24);
            this.btnSelectAllSubItem.TabIndex = 10;
            this.btnSelectAllSubItem.UseVisualStyleBackColor = true;
            this.btnSelectAllSubItem.Click += new System.EventHandler(this.btnSelectAllSubItem_Click);
            // 
            // lblSelectHint
            // 
            this.lblSelectHint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSelectHint.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectHint.ForeColor = System.Drawing.Color.Red;
            this.lblSelectHint.Location = new System.Drawing.Point(131, 351);
            this.lblSelectHint.Name = "lblSelectHint";
            this.lblSelectHint.Size = new System.Drawing.Size(353, 38);
            this.lblSelectHint.TabIndex = 11;
            this.lblSelectHint.Text = "Kontrol detaylarını görüntülemek için lütfen yukarıdan bir satır seçin.";
            this.lblSelectHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(656, 521);
            this.Controls.Add(this.lblSelectHint);
            this.Controls.Add(this.btnSelectAllSubItem);
            this.Controls.Add(this.btnSelectSubItem);
            this.Controls.Add(this.btnZoomSubItem);
            this.Controls.Add(this.lbSubItems);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnZoom);
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
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ListView lbSubItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnSelectSubItem;
        private System.Windows.Forms.Button btnZoomSubItem;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Button btnSelectAllSubItem;
        private System.Windows.Forms.Label lblSelectHint;
    }
}