namespace WildMouse.SmoothControls
{
    partial class WorkingIndicator
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
            this.components = new System.ComponentModel.Container();
            this.AnimTimer = new System.Windows.Forms.Timer(this.components);
            this.pbAnimCanvas = new System.Windows.Forms.PictureBox();
            this.CaptionLbl = new WildMouse.SmoothControls.SmoothLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // AnimTimer
            // 
            this.AnimTimer.Interval = 30;
            this.AnimTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
            // 
            // pbAnimCanvas
            // 
            this.pbAnimCanvas.Location = new System.Drawing.Point(3, 3);
            this.pbAnimCanvas.Name = "pbAnimCanvas";
            this.pbAnimCanvas.Size = new System.Drawing.Size(32, 32);
            this.pbAnimCanvas.TabIndex = 32;
            this.pbAnimCanvas.TabStop = false;
            // 
            // CaptionLbl
            // 
            this.CaptionLbl.Bold = false;
            this.CaptionLbl.FontSize = 10;
            this.CaptionLbl.Italic = false;
            this.CaptionLbl.Location = new System.Drawing.Point(41, 10);
            this.CaptionLbl.Name = "CaptionLbl";
            this.CaptionLbl.Size = new System.Drawing.Size(150, 19);
            this.CaptionLbl.TabIndex = 33;
            this.CaptionLbl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CaptionLbl.TextColor = System.Drawing.Color.Black;
            // 
            // WorkingIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CaptionLbl);
            this.Controls.Add(this.pbAnimCanvas);
            this.Name = "WorkingIndicator";
            this.Size = new System.Drawing.Size(241, 39);
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer AnimTimer;
        private System.Windows.Forms.PictureBox pbAnimCanvas;
        private SmoothLabel CaptionLbl;
    }
}
