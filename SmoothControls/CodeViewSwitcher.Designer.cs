namespace WildMouse.SmoothControls
{
    partial class CodeViewSwitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeViewSwitcher));
            this.pbSwitchIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSwitchIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSwitchIcon
            // 
            this.pbSwitchIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbSwitchIcon.Image")));
            this.pbSwitchIcon.Location = new System.Drawing.Point(138, 1);
            this.pbSwitchIcon.Name = "pbSwitchIcon";
            this.pbSwitchIcon.Size = new System.Drawing.Size(37, 21);
            this.pbSwitchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSwitchIcon.TabIndex = 0;
            this.pbSwitchIcon.TabStop = false;
            // 
            // CodeViewSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbSwitchIcon);
            this.Name = "CodeViewSwitcher";
            this.Size = new System.Drawing.Size(246, 71);
            ((System.ComponentModel.ISupportInitialize)(this.pbSwitchIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSwitchIcon;
    }
}
