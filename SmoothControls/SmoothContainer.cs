using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using System.ComponentModel.Design;

namespace WildMouse.SmoothControls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class SmoothContainer : UserControl
    {
        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private Pen GradientPen;
        private Color BorderColor;
        private Pen BorderPen;

        private float Diameter;
        private float Radius;

        public SmoothContainer()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SmoothContainer_Paint);
            this.Resize += new EventHandler(SmoothContainer_Resize);

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            Diameter = 10.0f;
            Radius = 5.0f;

            GradientStart = Color.FromArgb(251, 251, 251);
            GradientFinish = Color.FromArgb(251, 251, 251);

            GradientPen = new Pen(Color.Black);

            UpdateGradients();

            Diameter = 10.0f;
            Radius = 5.0f;
        }

        private void SmoothContainer_Resize(object sender, EventArgs e)
        {
            UpdateGradients();
        }

        private void UpdateGradients()
        {
            GradientColors = WildMouse.Graphics.Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void SmoothContainer_Paint(object sender, PaintEventArgs e)
        {
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            for (double i = 0; i < this.Height; i++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = GradientColors[(int)i];
                e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            //rounded corners
            e.Graphics.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //top right
            e.Graphics.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //bottom left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //bottom right

            //border lines
            e.Graphics.DrawLine(BorderPen, Radius, 0, this.Width - Radius, 0);  //top
            e.Graphics.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);  //bottom
            e.Graphics.DrawLine(BorderPen, 0, Radius, 0, this.Height - (Radius + 1));  //left
            e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));
        }
    }
}
