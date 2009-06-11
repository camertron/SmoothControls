namespace WildMouse.SmoothControls
{
    partial class ListView
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
            WildMouse.SmoothControls.ListHeaderCollection listHeaderCollection13 = new WildMouse.SmoothControls.ListHeaderCollection();
            this.HeaderBar = new WildMouse.SmoothControls.ListHeader();
            this.SuspendLayout();
            // 
            // ListScroller
            // 
            this.ListScroller.Location = new System.Drawing.Point(165, 0);
            this.ListScroller.Size = new System.Drawing.Size(17, 178);
            // 
            // ElementsPanel
            // 
            this.ElementsPanel.Location = new System.Drawing.Point(1, 24);
            this.ElementsPanel.Size = new System.Drawing.Size(163, 153);
            // 
            // HeaderBar
            // 
            this.HeaderBar.Headers = listHeaderCollection13;
            this.HeaderBar.Location = new System.Drawing.Point(0, 0);
            this.HeaderBar.Name = "HeaderBar";
            this.HeaderBar.Size = new System.Drawing.Size(160, 18);
            this.HeaderBar.TabIndex = 1;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.HeaderBar);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(182, 178);
            this.Controls.SetChildIndex(this.ElementsPanel, 0);
            this.Controls.SetChildIndex(this.ListScroller, 0);
            this.Controls.SetChildIndex(this.HeaderBar, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private ListHeader HeaderBar;
    }
}
