namespace WildMouse.SmoothControls
{
    partial class ListViewHeader
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
            this.MeasureLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MeasureLbl
            // 
            this.MeasureLbl.AutoSize = true;
            this.MeasureLbl.Location = new System.Drawing.Point(3, 6);
            this.MeasureLbl.Name = "MeasureLbl";
            this.MeasureLbl.Size = new System.Drawing.Size(35, 13);
            this.MeasureLbl.TabIndex = 0;
            this.MeasureLbl.Text = "label1";
            this.MeasureLbl.Visible = false;
            // 
            // ListViewHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MeasureLbl);
            this.DoubleBuffered = true;
            this.Name = "ListViewHeader";
            this.Size = new System.Drawing.Size(179, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MeasureLbl;

    }
}
