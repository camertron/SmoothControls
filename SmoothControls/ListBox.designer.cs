namespace WildMouse.SmoothControls
{
    partial class ListBox
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
            this.ElementsPanel = new System.Windows.Forms.Panel();
            this.ListScroller = new WildMouse.SmoothControls.VerticalScrollBar();
            this.SuspendLayout();
            // 
            // ElementsPanel
            // 
            this.ElementsPanel.Location = new System.Drawing.Point(3, 3);
            this.ElementsPanel.Name = "ElementsPanel";
            this.ElementsPanel.Size = new System.Drawing.Size(148, 100);
            this.ElementsPanel.TabIndex = 2;
            // 
            // ListScroller
            // 
            this.ListScroller.LargeChange = 0;
            this.ListScroller.Location = new System.Drawing.Point(157, 0);
            this.ListScroller.Maximum = 180;
            this.ListScroller.Minimum = 0;
            this.ListScroller.Name = "ListScroller";
            this.ListScroller.Size = new System.Drawing.Size(17, 132);
            this.ListScroller.SmallChange = 15;
            this.ListScroller.TabIndex = 0;
            this.ListScroller.Value = 0;
            // 
            // ListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.ListScroller);
            this.Controls.Add(this.ElementsPanel);
            this.DoubleBuffered = true;
            this.Name = "ListBox";
            this.Size = new System.Drawing.Size(174, 132);
            this.ResumeLayout(false);

        }

        #endregion

        protected VerticalScrollBar ListScroller;
        protected System.Windows.Forms.Panel ElementsPanel;
    }
}
