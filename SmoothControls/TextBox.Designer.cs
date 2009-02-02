namespace WildMouse.SmoothControls
{
    partial class TextBox
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
            this.TextField = new System.Windows.Forms.TextBox();
            this.InflateTmr = new System.Windows.Forms.Timer(this.components);
            this.DeflateTmr = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TextField
            // 
            this.TextField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextField.Location = new System.Drawing.Point(3, 3);
            this.TextField.Name = "TextField";
            this.TextField.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextField.Size = new System.Drawing.Size(100, 13);
            this.TextField.TabIndex = 0;
            // 
            // InflateTmr
            // 
            this.InflateTmr.Interval = 5;
            this.InflateTmr.Tick += new System.EventHandler(this.InflateTmr_Tick);
            // 
            // DeflateTmr
            // 
            this.DeflateTmr.Interval = 5;
            this.DeflateTmr.Tick += new System.EventHandler(this.DeflateTmr_Tick);
            // 
            // TextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextField);
            this.Name = "TextBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextField;
        private System.Windows.Forms.Timer InflateTmr;
        private System.Windows.Forms.Timer DeflateTmr;
    }
}
