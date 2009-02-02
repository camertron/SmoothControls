namespace WildMouse.SmoothControls
{
    partial class VerticalScrollBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerticalScrollBar));
            this.UpButton = new System.Windows.Forms.PictureBox();
            this.DownButton = new System.Windows.Forms.PictureBox();
            this.BackPic = new System.Windows.Forms.PictureBox();
            this.ScrollTimer = new System.Windows.Forms.Timer(this.components);
            this.Handle = new WildMouse.SmoothControls.VerticalScrollHandle();
            ((System.ComponentModel.ISupportInitialize)(this.UpButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackPic)).BeginInit();
            this.SuspendLayout();
            // 
            // UpButton
            // 
            this.UpButton.Image = ((System.Drawing.Image)(resources.GetObject("UpButton.Image")));
            this.UpButton.Location = new System.Drawing.Point(0, 0);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(17, 18);
            this.UpButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.UpButton.TabIndex = 1;
            this.UpButton.TabStop = false;
            // 
            // DownButton
            // 
            this.DownButton.Image = ((System.Drawing.Image)(resources.GetObject("DownButton.Image")));
            this.DownButton.Location = new System.Drawing.Point(0, 132);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(17, 18);
            this.DownButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DownButton.TabIndex = 2;
            this.DownButton.TabStop = false;
            // 
            // BackPic
            // 
            this.BackPic.Image = ((System.Drawing.Image)(resources.GetObject("BackPic.Image")));
            this.BackPic.Location = new System.Drawing.Point(0, 0);
            this.BackPic.Name = "BackPic";
            this.BackPic.Size = new System.Drawing.Size(17, 150);
            this.BackPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackPic.TabIndex = 3;
            this.BackPic.TabStop = false;
            // 
            // ScrollTimer
            // 
            this.ScrollTimer.Interval = 50;
            this.ScrollTimer.Tick += new System.EventHandler(this.ScrollTimer_Tick);
            // 
            // Handle
            // 
            this.Handle.Location = new System.Drawing.Point(1, 18);
            this.Handle.Name = "Handle";
            this.Handle.Size = new System.Drawing.Size(15, 51);
            this.Handle.TabIndex = 0;
            // 
            // VerticalScrollBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.Handle);
            this.Controls.Add(this.BackPic);
            this.DoubleBuffered = true;
            this.Name = "VerticalScrollBar";
            this.Size = new System.Drawing.Size(17, 150);
            ((System.ComponentModel.ISupportInitialize)(this.UpButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VerticalScrollHandle Handle;
        private System.Windows.Forms.PictureBox UpButton;
        private System.Windows.Forms.PictureBox DownButton;
        private System.Windows.Forms.PictureBox BackPic;
        private System.Windows.Forms.Timer ScrollTimer;
    }
}
