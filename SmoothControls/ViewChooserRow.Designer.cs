namespace WildMouse.SmoothControls
{
    partial class ViewChooserRow
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
            this.TextLbl = new WildMouse.SmoothControls.SmoothLabel();
            this.SuspendLayout();
            // 
            // TextLbl
            // 
            this.TextLbl.BackColor = System.Drawing.Color.Transparent;
            this.TextLbl.FontSize = 10;
            this.TextLbl.Location = new System.Drawing.Point(3, 4);
            this.TextLbl.Name = "TextLbl";
            this.TextLbl.Size = new System.Drawing.Size(105, 18);
            this.TextLbl.TabIndex = 0;
            this.TextLbl.Text = "ViewChooserRow";
            this.TextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViewChooserRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextLbl);
            this.Name = "ViewChooserRow";
            this.Size = new System.Drawing.Size(150, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothLabel TextLbl;
    }
}
