namespace WildMouse.SmoothControls
{
    partial class Message
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
            this.Button1 = new WildMouse.SmoothControls.RoundButton();
            this.Button2 = new WildMouse.SmoothControls.RoundButton();
            this.Button3 = new WildMouse.SmoothControls.RoundButton();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.Transparent;
            this.Button1.FontSize = 10;
            this.Button1.Location = new System.Drawing.Point(286, 95);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(104, 18);
            this.Button1.State = WildMouse.SmoothControls.RoundButton.ButtonState.NotPressed;
            this.Button1.Sticky = false;
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Button1";
            this.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Button1.TextColor = System.Drawing.Color.Black;
            // 
            // Button2
            // 
            this.Button2.BackColor = System.Drawing.Color.Transparent;
            this.Button2.FontSize = 10;
            this.Button2.Location = new System.Drawing.Point(176, 95);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(104, 18);
            this.Button2.State = WildMouse.SmoothControls.RoundButton.ButtonState.NotPressed;
            this.Button2.Sticky = false;
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Button2";
            this.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Button2.TextColor = System.Drawing.Color.Black;
            // 
            // Button3
            // 
            this.Button3.BackColor = System.Drawing.Color.Transparent;
            this.Button3.FontSize = 10;
            this.Button3.Location = new System.Drawing.Point(66, 95);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(104, 18);
            this.Button3.State = WildMouse.SmoothControls.RoundButton.ButtonState.NotPressed;
            this.Button3.Sticky = false;
            this.Button3.TabIndex = 2;
            this.Button3.Text = "Button3";
            this.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Button3.TextColor = System.Drawing.Color.Black;
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Message";
            this.Size = new System.Drawing.Size(402, 126);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundButton Button1;
        private RoundButton Button2;
        private RoundButton Button3;
    }
}
