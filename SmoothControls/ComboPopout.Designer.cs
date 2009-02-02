namespace WildMouse.SmoothControls
{
    partial class ComboPopout
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
            this.FadeTmr = new System.Windows.Forms.Timer(this.components);
            this.BrightenTmr = new System.Windows.Forms.Timer(this.components);
            this.FlashTmr = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // FadeTmr
            // 
            this.FadeTmr.Interval = 10;
            this.FadeTmr.Tick += new System.EventHandler(this.FadeTmr_Tick);
            // 
            // BrightenTmr
            // 
            this.BrightenTmr.Interval = 10;
            this.BrightenTmr.Tick += new System.EventHandler(this.BrightenTmr_Tick);
            // 
            // FlashTmr
            // 
            this.FlashTmr.Interval = 40;
            this.FlashTmr.Tick += new System.EventHandler(this.FlashTmr_Tick);
            // 
            // ComboPopout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(153, 152);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComboPopout";
            this.Opacity = 0;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ComboPopout";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer FadeTmr;
        private System.Windows.Forms.Timer BrightenTmr;
        private System.Windows.Forms.Timer FlashTmr;
    }
}