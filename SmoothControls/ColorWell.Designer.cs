namespace WildMouse.SmoothControls
{
    partial class ColorWell
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
            this.ColorPanel = new System.Windows.Forms.Panel();
            this.ColorTxt = new WildMouse.SmoothControls.TextBox();
            this.ColorPickerDlg = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // ColorPanel
            // 
            this.ColorPanel.Location = new System.Drawing.Point(3, 3);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(77, 23);
            this.ColorPanel.TabIndex = 0;
            // 
            // ColorTxt
            // 
            this.ColorTxt.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ColorTxt.Location = new System.Drawing.Point(85, 3);
            this.ColorTxt.MaxLength = 6;
            this.ColorTxt.Multiline = false;
            this.ColorTxt.Name = "ColorTxt";
            this.ColorTxt.Size = new System.Drawing.Size(70, 23);
            this.ColorTxt.TabIndex = 1;
            // 
            // ColorWell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColorTxt);
            this.Controls.Add(this.ColorPanel);
            this.Name = "ColorWell";
            this.Size = new System.Drawing.Size(160, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ColorPanel;
        private WildMouse.SmoothControls.TextBox ColorTxt;
        private System.Windows.Forms.ColorDialog ColorPickerDlg;
    }
}
