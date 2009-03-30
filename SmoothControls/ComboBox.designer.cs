namespace WildMouse.SmoothControls
{
    partial class ComboBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComboBox));
            this.PopoutBtn = new System.Windows.Forms.PictureBox();
            this.MeasureLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PopoutBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // PopoutBtn
            // 
            this.PopoutBtn.Image = ((System.Drawing.Image)(resources.GetObject("PopoutBtn.Image")));
            this.PopoutBtn.Location = new System.Drawing.Point(78, 3);
            this.PopoutBtn.Name = "PopoutBtn";
            this.PopoutBtn.Size = new System.Drawing.Size(9, 18);
            this.PopoutBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PopoutBtn.TabIndex = 0;
            this.PopoutBtn.TabStop = false;
            this.PopoutBtn.Click += new System.EventHandler(this.PopoutBtn_Click);
            // 
            // MeasureLbl
            // 
            this.MeasureLbl.AutoSize = true;
            this.MeasureLbl.Location = new System.Drawing.Point(3, 3);
            this.MeasureLbl.Name = "MeasureLbl";
            this.MeasureLbl.Size = new System.Drawing.Size(35, 13);
            this.MeasureLbl.TabIndex = 1;
            this.MeasureLbl.Text = "label1";
            this.MeasureLbl.Visible = false;
            // 
            // ComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MeasureLbl);
            this.Controls.Add(this.PopoutBtn);
            this.Name = "ComboBox";
            this.Size = new System.Drawing.Size(261, 40);
            ((System.ComponentModel.ISupportInitialize)(this.PopoutBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PopoutBtn;
        private System.Windows.Forms.Label MeasureLbl;

    }
}
