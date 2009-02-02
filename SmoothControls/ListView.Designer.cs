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
            WildMouse.SmoothControls.ListHeaderCollection listHeaderCollection1 = new WildMouse.SmoothControls.ListHeaderCollection();
            this.ListScroller = new WildMouse.SmoothControls.VerticalScrollBar();
            this.HeaderBar = new WildMouse.SmoothControls.ListViewHeader();
            this.ElementsPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
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
            // HeaderBar
            // 
            this.HeaderBar.Headers = listHeaderCollection1;
            this.HeaderBar.Location = new System.Drawing.Point(0, 0);
            this.HeaderBar.Name = "HeaderBar";
            this.HeaderBar.Size = new System.Drawing.Size(160, 18);
            this.HeaderBar.TabIndex = 1;
            // 
            // ElementsPanel
            // 
            this.ElementsPanel.Location = new System.Drawing.Point(3, 18);
            this.ElementsPanel.Name = "ElementsPanel";
            this.ElementsPanel.Size = new System.Drawing.Size(148, 100);
            this.ElementsPanel.TabIndex = 2;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.ElementsPanel);
            this.Controls.Add(this.ListScroller);
            this.Controls.Add(this.HeaderBar);
            this.DoubleBuffered = true;
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(174, 132);
            this.ResumeLayout(false);

        }

        #endregion

        private VerticalScrollBar ListScroller;
        private ListViewHeader HeaderBar;
        private System.Windows.Forms.Panel ElementsPanel;
    }
}
