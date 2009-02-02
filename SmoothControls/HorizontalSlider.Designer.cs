namespace WildMouse.SmoothControls
{
    partial class HorizontalSlider
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
            this.Handle = new WildMouse.SmoothControls.SliderHandle();
            this.SuspendLayout();
            // 
            // Handle
            // 
            this.Handle.Location = new System.Drawing.Point(116, 3);
            this.Handle.Name = "Handle";
            this.Handle.Size = new System.Drawing.Size(19, 19);
            this.Handle.TabIndex = 0;
            // 
            // HorizontalSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Handle);
            this.Name = "HorizontalSlider";
            this.Size = new System.Drawing.Size(261, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private SliderHandle Handle;
    }
}
