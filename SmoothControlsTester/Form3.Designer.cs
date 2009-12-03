namespace SmoothControlsTester
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.pbTestCanvas = new System.Windows.Forms.PictureBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.iconListRow1 = new WildMouse.SmoothControls.IconListRow();
            this.alphaListBox1 = new WildMouse.SmoothControls.AlphaListBox();
            this.smoothToggleSwitch1 = new WildMouse.SmoothControls.SmoothToggleSwitch();
            this.smoothLabel1 = new WildMouse.SmoothControls.SmoothLabel();
            this.sequentialListBox1 = new WildMouse.SmoothControls.SequentialListBox();
            this.iconListRow2 = new WildMouse.SmoothControls.IconListRow();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTestCanvas
            // 
            this.pbTestCanvas.Location = new System.Drawing.Point(44, 74);
            this.pbTestCanvas.Name = "pbTestCanvas";
            this.pbTestCanvas.Size = new System.Drawing.Size(127, 94);
            this.pbTestCanvas.TabIndex = 3;
            this.pbTestCanvas.TabStop = false;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(44, 184);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(127, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Draw!";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // iconListRow1
            // 
            this.iconListRow1.FontSize = 10;
            this.iconListRow1.Icon = ((System.Drawing.Bitmap)(resources.GetObject("iconListRow1.Icon")));
            this.iconListRow1.Location = new System.Drawing.Point(250, 26);
            this.iconListRow1.Name = "iconListRow1";
            this.iconListRow1.Selected = false;
            this.iconListRow1.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.iconListRow1.Size = new System.Drawing.Size(166, 35);
            this.iconListRow1.TabIndex = 2;
            this.iconListRow1.Text = "Antarctica";
            this.iconListRow1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.iconListRow1.TextColor = System.Drawing.Color.Black;
            this.iconListRow1.TextLeft = 60;
            // 
            // alphaListBox1
            // 
            this.alphaListBox1.BackColor = System.Drawing.Color.White;
            this.alphaListBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.alphaListBox1.Location = new System.Drawing.Point(320, 104);
            this.alphaListBox1.Name = "alphaListBox1";
            this.alphaListBox1.SelectedIndex = -1;
            this.alphaListBox1.Size = new System.Drawing.Size(337, 434);
            this.alphaListBox1.TabIndex = 1;
            this.alphaListBox1.UseRowColoring = true;
            // 
            // smoothToggleSwitch1
            // 
            this.smoothToggleSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.smoothToggleSwitch1.IsOn = false;
            this.smoothToggleSwitch1.Location = new System.Drawing.Point(30, 26);
            this.smoothToggleSwitch1.Name = "smoothToggleSwitch1";
            this.smoothToggleSwitch1.Size = new System.Drawing.Size(65, 22);
            this.smoothToggleSwitch1.TabIndex = 0;
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.Bold = false;
            this.smoothLabel1.FontSize = 10;
            this.smoothLabel1.Italic = false;
            this.smoothLabel1.Location = new System.Drawing.Point(141, 113);
            this.smoothLabel1.Name = "smoothLabel1";
            this.smoothLabel1.Size = new System.Drawing.Size(150, 18);
            this.smoothLabel1.TabIndex = 5;
            this.smoothLabel1.Text = "Antarctica";
            this.smoothLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.smoothLabel1.TextColor = System.Drawing.Color.Black;
            // 
            // sequentialListBox1
            // 
            this.sequentialListBox1.BackColor = System.Drawing.Color.White;
            this.sequentialListBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.sequentialListBox1.Location = new System.Drawing.Point(44, 233);
            this.sequentialListBox1.Name = "sequentialListBox1";
            this.sequentialListBox1.SelectedIndex = -1;
            this.sequentialListBox1.Size = new System.Drawing.Size(235, 218);
            this.sequentialListBox1.TabIndex = 6;
            this.sequentialListBox1.UseRowColoring = false;
            // 
            // iconListRow2
            // 
            this.iconListRow2.FontSize = 10;
            this.iconListRow2.Icon = ((System.Drawing.Bitmap)(resources.GetObject("iconListRow2.Icon")));
            this.iconListRow2.Location = new System.Drawing.Point(44, 457);
            this.iconListRow2.Name = "iconListRow2";
            this.iconListRow2.Selected = false;
            this.iconListRow2.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.iconListRow2.Size = new System.Drawing.Size(235, 23);
            this.iconListRow2.TabIndex = 17;
            this.iconListRow2.Text = "iconListRow2";
            this.iconListRow2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.iconListRow2.TextColor = System.Drawing.Color.Black;
            this.iconListRow2.TextLeft = 25;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 586);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.iconListRow2);
            this.Controls.Add(this.sequentialListBox1);
            this.Controls.Add(this.smoothLabel1);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pbTestCanvas);
            this.Controls.Add(this.iconListRow1);
            this.Controls.Add(this.alphaListBox1);
            this.Controls.Add(this.smoothToggleSwitch1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothToggleSwitch smoothToggleSwitch1;
        private WildMouse.SmoothControls.AlphaListBox alphaListBox1;
        private WildMouse.SmoothControls.IconListRow iconListRow1;
        private System.Windows.Forms.PictureBox pbTestCanvas;
        private System.Windows.Forms.Button btnDraw;
        private WildMouse.SmoothControls.SmoothLabel smoothLabel1;
        private WildMouse.SmoothControls.SequentialListBox sequentialListBox1;
        private WildMouse.SmoothControls.IconListRow iconListRow2;
        private System.Windows.Forms.Button button1;

    }
}