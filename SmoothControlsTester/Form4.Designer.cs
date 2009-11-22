namespace SmoothControlsTester
{
    partial class Form4
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
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new WildMouse.SmoothControls.CheckBox();
            this.rectButton1 = new WildMouse.SmoothControls.RectButton();
            this.roundButton2 = new WildMouse.SmoothControls.RoundButton();
            this.roundButton1 = new WildMouse.SmoothControls.RoundButton();
            this.stTable = new WildMouse.SmoothControls.SmoothTable();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.Location = new System.Drawing.Point(349, 310);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 14);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Alphabetize";
            this.checkBox1.TextColor = System.Drawing.Color.Black;
            // 
            // rectButton1
            // 
            this.rectButton1.Icon = null;
            this.rectButton1.Location = new System.Drawing.Point(253, 330);
            this.rectButton1.Name = "rectButton1";
            this.rectButton1.Size = new System.Drawing.Size(75, 22);
            this.rectButton1.TabIndex = 4;
            this.rectButton1.Text = "Update";
            // 
            // roundButton2
            // 
            this.roundButton2.BackColor = System.Drawing.Color.Transparent;
            this.roundButton2.FontSize = 9;
            this.roundButton2.Icon = null;
            this.roundButton2.Location = new System.Drawing.Point(334, 266);
            this.roundButton2.Name = "roundButton2";
            this.roundButton2.Size = new System.Drawing.Size(113, 17);
            this.roundButton2.State = WildMouse.SmoothControls.RoundButton.ButtonState.NotPressed;
            this.roundButton2.Sticky = false;
            this.roundButton2.TabIndex = 3;
            this.roundButton2.Text = "Remove Citation";
            this.roundButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.roundButton2.TextColor = System.Drawing.Color.Black;
            // 
            // roundButton1
            // 
            this.roundButton1.BackColor = System.Drawing.Color.Transparent;
            this.roundButton1.FontSize = 9;
            this.roundButton1.Icon = null;
            this.roundButton1.Location = new System.Drawing.Point(215, 266);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.Size = new System.Drawing.Size(113, 17);
            this.roundButton1.State = WildMouse.SmoothControls.RoundButton.ButtonState.NotPressed;
            this.roundButton1.Sticky = false;
            this.roundButton1.TabIndex = 2;
            this.roundButton1.Text = "Add Citation";
            this.roundButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.roundButton1.TextColor = System.Drawing.Color.Black;
            // 
            // stTable
            // 
            this.stTable.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.stTable.Location = new System.Drawing.Point(65, 49);
            this.stTable.Name = "stTable";
            this.stTable.Size = new System.Drawing.Size(51, 21);
            this.stTable.TabIndex = 0;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 515);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.rectButton1);
            this.Controls.Add(this.roundButton2);
            this.Controls.Add(this.roundButton1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.stTable);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothTable stTable;
        private System.Windows.Forms.Button button2;
        private WildMouse.SmoothControls.RoundButton roundButton1;
        private WildMouse.SmoothControls.RoundButton roundButton2;
        private WildMouse.SmoothControls.RectButton rectButton1;
        private WildMouse.SmoothControls.CheckBox checkBox1;
    }
}