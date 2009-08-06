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
            this.stTable = new WildMouse.SmoothControls.SmoothTable();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stTable
            // 
            this.stTable.Location = new System.Drawing.Point(65, 49);
            this.stTable.Name = "stTable";
            this.stTable.Size = new System.Drawing.Size(478, 392);
            this.stTable.TabIndex = 0;
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
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 515);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.stTable);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothTable stTable;
        private System.Windows.Forms.Button button2;
    }
}