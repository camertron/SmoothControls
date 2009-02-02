namespace WildMouse.SmoothControls
{
    partial class MessageBox
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
            this.MessageCtrl = new WildMouse.SmoothControls.Message();
            this.SuspendLayout();
            // 
            // MessageCtrl
            // 
            this.MessageCtrl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.MessageCtrl.Buttons = System.Windows.Forms.MessageBoxButtons.OK;
            this.MessageCtrl.ForeColor = System.Drawing.Color.Black;
            this.MessageCtrl.Icon = System.Windows.Forms.MessageBoxIcon.None;
            this.MessageCtrl.Location = new System.Drawing.Point(96, 64);
            this.MessageCtrl.Name = "MessageCtrl";
            this.MessageCtrl.Size = new System.Drawing.Size(400, 150);
            this.MessageCtrl.TabIndex = 0;
            this.MessageCtrl.Text = "messageBox1";
            this.MessageCtrl.TextColor = System.Drawing.Color.Black;
            this.MessageCtrl.Title = null;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MessageCtrl);
            this.Name = "MessageBox";
            this.Size = new System.Drawing.Size(655, 323);
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.Message MessageCtrl;

    }
}
