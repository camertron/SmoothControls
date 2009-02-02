namespace SmoothControlsTester
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.viewChooser1 = new WildMouse.SmoothControls.ViewChooser();
            this.smoothRibbon1 = new WildMouse.SmoothControls.SmoothRibbon();
            this.ribbonButton4 = new WildMouse.SmoothControls.RibbonButton();
            this.ribbonDivider1 = new WildMouse.SmoothControls.RibbonDivider();
            this.ribbonButton3 = new WildMouse.SmoothControls.RibbonButton();
            this.ribbonButton1 = new WildMouse.SmoothControls.RibbonButton();
            this.ribbonButton2 = new WildMouse.SmoothControls.RibbonButton();
            this.smoothLabel2 = new WildMouse.SmoothControls.SmoothLabel();
            this.listViewRow1 = new WildMouse.SmoothControls.ListViewRow();
            this.segmentChooser1 = new WildMouse.SmoothControls.SegmentChooser();
            this.colorWell1 = new WildMouse.SmoothControls.ColorWell();
            this.smoothLabel1 = new WildMouse.SmoothControls.SmoothLabel();
            this.smoothRibbon1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(308, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // viewChooser1
            // 
            this.viewChooser1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.viewChooser1.Location = new System.Drawing.Point(448, 12);
            this.viewChooser1.Name = "viewChooser1";
            this.viewChooser1.SelectedIndex = 0;
            this.viewChooser1.Size = new System.Drawing.Size(125, 305);
            this.viewChooser1.TabIndex = 7;
            this.viewChooser1.Tabs = ((System.Collections.Specialized.StringCollection)(resources.GetObject("viewChooser1.Tabs")));
            // 
            // smoothRibbon1
            // 
            this.smoothRibbon1.Controls.Add(this.ribbonButton4);
            this.smoothRibbon1.Controls.Add(this.ribbonDivider1);
            this.smoothRibbon1.Controls.Add(this.ribbonButton3);
            this.smoothRibbon1.Controls.Add(this.ribbonButton1);
            this.smoothRibbon1.Controls.Add(this.ribbonButton2);
            this.smoothRibbon1.Expanded = true;
            this.smoothRibbon1.Location = new System.Drawing.Point(93, 156);
            this.smoothRibbon1.Name = "smoothRibbon1";
            this.smoothRibbon1.Size = new System.Drawing.Size(242, 105);
            this.smoothRibbon1.TabIndex = 6;
            this.smoothRibbon1.Text = "Paragraph";
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton4.ButtonEnabled = true;
            this.ribbonButton4.ButtonType = WildMouse.SmoothControls.RibbonButton.RibbonButtonType.Large;
            this.ribbonButton4.Image = ((System.Drawing.Bitmap)(resources.GetObject("ribbonButton4.Image")));
            this.ribbonButton4.Location = new System.Drawing.Point(81, 7);
            this.ribbonButton4.Name = "ribbonButton4";
            this.ribbonButton4.Size = new System.Drawing.Size(57, 71);
            this.ribbonButton4.TabIndex = 10;
            this.ribbonButton4.Text = "Battery";
            // 
            // ribbonDivider1
            // 
            this.ribbonDivider1.EndColor = System.Drawing.Color.Black;
            this.ribbonDivider1.Location = new System.Drawing.Point(75, 7);
            this.ribbonDivider1.Name = "ribbonDivider1";
            this.ribbonDivider1.Orientation = WildMouse.SmoothControls.RibbonDivider.DividerOrientation.Vertical;
            this.ribbonDivider1.Size = new System.Drawing.Size(1, 70);
            this.ribbonDivider1.StartColor = System.Drawing.Color.White;
            this.ribbonDivider1.TabIndex = 11;
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton3.ButtonEnabled = true;
            this.ribbonButton3.ButtonType = WildMouse.SmoothControls.RibbonButton.RibbonButtonType.Small;
            this.ribbonButton3.Image = ((System.Drawing.Bitmap)(resources.GetObject("ribbonButton3.Image")));
            this.ribbonButton3.Location = new System.Drawing.Point(136, 43);
            this.ribbonButton3.Name = "ribbonButton3";
            this.ribbonButton3.Size = new System.Drawing.Size(99, 30);
            this.ribbonButton3.TabIndex = 9;
            this.ribbonButton3.Text = "Internet";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton1.ButtonEnabled = true;
            this.ribbonButton1.ButtonType = WildMouse.SmoothControls.RibbonButton.RibbonButtonType.Large;
            this.ribbonButton1.Image = ((System.Drawing.Bitmap)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.Location = new System.Drawing.Point(7, 6);
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.Size = new System.Drawing.Size(57, 71);
            this.ribbonButton1.TabIndex = 7;
            this.ribbonButton1.Text = "Internet";
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton2.ButtonEnabled = true;
            this.ribbonButton2.ButtonType = WildMouse.SmoothControls.RibbonButton.RibbonButtonType.Small;
            this.ribbonButton2.Image = ((System.Drawing.Bitmap)(resources.GetObject("ribbonButton2.Image")));
            this.ribbonButton2.Location = new System.Drawing.Point(136, 7);
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.Size = new System.Drawing.Size(99, 30);
            this.ribbonButton2.TabIndex = 8;
            this.ribbonButton2.Text = "Battery";
            // 
            // smoothLabel2
            // 
            this.smoothLabel2.FontSize = 10;
            this.smoothLabel2.Location = new System.Drawing.Point(159, 98);
            this.smoothLabel2.Name = "smoothLabel2";
            this.smoothLabel2.Size = new System.Drawing.Size(269, 25);
            this.smoothLabel2.TabIndex = 5;
            this.smoothLabel2.Text = "smoothLabel2";
            this.smoothLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewRow1
            // 
            this.listViewRow1.ListInfo = null;
            this.listViewRow1.Location = new System.Drawing.Point(159, 118);
            this.listViewRow1.Name = "listViewRow1";
            this.listViewRow1.Size = new System.Drawing.Size(252, 17);
            this.listViewRow1.TabIndex = 4;
            this.listViewRow1.TextColor = System.Drawing.Color.Empty;
            // 
            // segmentChooser1
            // 
            this.segmentChooser1.Items = ((System.Collections.Specialized.StringCollection)(resources.GetObject("segmentChooser1.Items")));
            this.segmentChooser1.Location = new System.Drawing.Point(144, 59);
            this.segmentChooser1.Name = "segmentChooser1";
            this.segmentChooser1.SelectedIndex = 2;
            this.segmentChooser1.Size = new System.Drawing.Size(142, 23);
            this.segmentChooser1.TabIndex = 2;
            // 
            // colorWell1
            // 
            this.colorWell1.DisplayColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorWell1.Location = new System.Drawing.Point(159, 12);
            this.colorWell1.Name = "colorWell1";
            this.colorWell1.Size = new System.Drawing.Size(160, 29);
            this.colorWell1.TabIndex = 1;
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.FontSize = 10;
            this.smoothLabel1.Location = new System.Drawing.Point(23, 26);
            this.smoothLabel1.Name = "smoothLabel1";
            this.smoothLabel1.Size = new System.Drawing.Size(100, 29);
            this.smoothLabel1.TabIndex = 0;
            this.smoothLabel1.Text = "Cameron";
            this.smoothLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(658, 327);
            this.Controls.Add(this.viewChooser1);
            this.Controls.Add(this.smoothRibbon1);
            this.Controls.Add(this.smoothLabel2);
            this.Controls.Add(this.listViewRow1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.segmentChooser1);
            this.Controls.Add(this.colorWell1);
            this.Controls.Add(this.smoothLabel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.smoothRibbon1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WildMouse.SmoothControls.SmoothLabel smoothLabel1;
        private WildMouse.SmoothControls.ColorWell colorWell1;
        private WildMouse.SmoothControls.SegmentChooser segmentChooser1;
        private System.Windows.Forms.Button button1;
        private WildMouse.SmoothControls.ListViewRow listViewRow1;
        private WildMouse.SmoothControls.SmoothLabel smoothLabel2;
        private WildMouse.SmoothControls.SmoothRibbon smoothRibbon1;
        private WildMouse.SmoothControls.RibbonButton ribbonButton1;
        private WildMouse.SmoothControls.RibbonButton ribbonButton2;
        private WildMouse.SmoothControls.RibbonButton ribbonButton3;
        private WildMouse.SmoothControls.RibbonButton ribbonButton4;
        private WildMouse.SmoothControls.RibbonDivider ribbonDivider1;
        private WildMouse.SmoothControls.ViewChooser viewChooser1;
    }
}