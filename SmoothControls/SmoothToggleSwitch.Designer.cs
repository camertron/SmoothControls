namespace WildMouse.SmoothControls
{
    partial class SmoothToggleSwitch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmoothToggleSwitch));
            this.Handle = new WildMouse.SmoothControls.SmoothToggle();
            this.AnimTmr = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Handle
            // 
            this.Handle.Icon = ((System.Drawing.Bitmap)(resources.GetObject("Handle.Icon")));
            this.Handle.Location = new System.Drawing.Point(0, 0);
            this.Handle.Name = "Handle";
            this.Handle.Size = new System.Drawing.Size(33, 22);
            this.Handle.TabIndex = 0;
            // 
            // AnimTmr
            // 
            this.AnimTmr.Interval = 1;
            this.AnimTmr.Tick += new System.EventHandler(this.AnimTmr_Tick);
            // 
            // SmoothToggleSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Handle);
            this.Name = "SmoothToggleSwitch";
            this.ResumeLayout(false);

        }

        #endregion

        private new WildMouse.SmoothControls.SmoothToggle Handle;
        private System.Windows.Forms.Timer AnimTmr;
    }
}
