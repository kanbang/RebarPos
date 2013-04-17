namespace RebarPosCommands
{
    partial class SelectShapeForm
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
            this.shapesView = new RebarPosCommands.MultiPosShapeView();
            this.SuspendLayout();
            // 
            // shapesView
            // 
            this.shapesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shapesView.CellBackColor = System.Drawing.Color.Empty;
            this.shapesView.CellSize = new System.Drawing.Size(300, 150);
            this.shapesView.Location = new System.Drawing.Point(12, 12);
            this.shapesView.Name = "shapesView";
            this.shapesView.SelectedShape = "";
            this.shapesView.ShowShapeNames = false;
            this.shapesView.Size = new System.Drawing.Size(1215, 588);
            this.shapesView.TabIndex = 0;
            // 
            // SelectShapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 612);
            this.Controls.Add(this.shapesView);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectShapeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poz Şekli";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectShapeForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private MultiPosShapeView shapesView;



    }
}