namespace WildMouse.SmoothControls
{
    partial class CompilationResultRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbExpandArrow = new System.Windows.Forms.PictureBox();
            this.lblProcName = new WildMouse.SmoothControls.SmoothLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpandArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // pbExpandArrow
            // 
            this.pbExpandArrow.Location = new System.Drawing.Point(9, 6);
            this.pbExpandArrow.Name = "pbExpandArrow";
            this.pbExpandArrow.Size = new System.Drawing.Size(10, 12);
            this.pbExpandArrow.TabIndex = 0;
            this.pbExpandArrow.TabStop = false;
            this.pbExpandArrow.Click += new System.EventHandler(this.pbExpandArrow_Click);
            // 
            // lblProcName
            // 
            this.lblProcName.Bold = false;
            this.lblProcName.FontSize = 10;
            this.lblProcName.Italic = false;
            this.lblProcName.Location = new System.Drawing.Point(22, 3);
            this.lblProcName.Name = "lblProcName";
            this.lblProcName.Size = new System.Drawing.Size(120, 16);
            this.lblProcName.TabIndex = 1;
            this.lblProcName.Text = "ADBY";
            this.lblProcName.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblProcName.TextColor = System.Drawing.Color.Black;
            // 
            // CompilationResultRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblProcName);
            this.Controls.Add(this.pbExpandArrow);
            this.Name = "CompilationResultRow";
            ((System.ComponentModel.ISupportInitialize)(this.pbExpandArrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbExpandArrow;
        private SmoothLabel lblProcName;
    }
}
