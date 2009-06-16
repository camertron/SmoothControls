namespace SmoothControlsTester
{
    partial class Form3
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
            this.alphaListBox1 = new WildMouse.SmoothControls.AlphaListBox();
            this.smoothToggleSwitch1 = new WildMouse.SmoothControls.SmoothToggleSwitch();
            this.SuspendLayout();
            // 
            // alphaListBox1
            // 
            this.alphaListBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.alphaListBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.alphaListBox1.Location = new System.Drawing.Point(250, 108);
            this.alphaListBox1.Name = "alphaListBox1";
            this.alphaListBox1.SelectedIndex = -1;
            this.alphaListBox1.Size = new System.Drawing.Size(195, 337);
            this.alphaListBox1.TabIndex = 1;
            this.alphaListBox1.UseRowColoring = true;
            // 
            // smoothToggleSwitch1
            // 
            this.smoothToggleSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.smoothToggleSwitch1.IsOn = false;
            this.smoothToggleSwitch1.Location = new System.Drawing.Point(30, 26);
            this.smoothToggleSwitch1.Name = "smoothToggleSwitch1";
            this.smoothToggleSwitch1.Size = new System.Drawing.Size(65, 22);
            this.smoothToggleSwitch1.TabIndex = 0;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 586);
            this.Controls.Add(this.alphaListBox1);
            this.Controls.Add(this.smoothToggleSwitch1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothToggleSwitch smoothToggleSwitch1;
        private WildMouse.SmoothControls.AlphaListBox alphaListBox1;

    }
}