namespace WildMouse.SmoothControls
{
    partial class RibbonButton
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
            this.CaptionLbl = new WildMouse.SmoothControls.SmoothLabel();
            this.SuspendLayout();
            // 
            // CaptionLbl
            // 
            this.CaptionLbl.Bold = false;
            this.CaptionLbl.FontSize = 9;
            this.CaptionLbl.Italic = false;
            this.CaptionLbl.Location = new System.Drawing.Point(3, 77);
            this.CaptionLbl.Name = "CaptionLbl";
            this.CaptionLbl.Size = new System.Drawing.Size(78, 14);
            this.CaptionLbl.TabIndex = 0;
            this.CaptionLbl.Text = "Hello";
            this.CaptionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CaptionLbl.TextColor = System.Drawing.Color.Black;
            // 
            // RibbonButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.CaptionLbl);
            this.Name = "RibbonButton";
            this.Size = new System.Drawing.Size(93, 103);
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothLabel CaptionLbl;
    }
}
