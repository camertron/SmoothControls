using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GDIDB;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    internal partial class VerticalScrollHandle : UserControl
    {
        private DBGraphics memGraphics;

        private Color BorderColor;
        private float Diameter;
        private float Radius;
        private Color[] GradientColors;
        private Color GradientStart;
        private Color GradientFinish;
        private Pen GradientPen;
        private Pen BorderPen;

        public VerticalScrollHandle()
        {
            InitializeComponent();

            GradientPen = new Pen(Color.Black);
            //GradientStart = Color.FromArgb(188, 193, 232);
            //GradientFinish = Color.FromArgb(135, 144, 210);
            GradientStart = Color.FromArgb(220, 220, 220);
            GradientFinish = Color.FromArgb(180, 180, 180);
            UpdateGradients();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            this.Paint += new PaintEventHandler(VerticalScrollHandle_Paint);
            this.Resize += new EventHandler(VerticalScrollHandle_Resize);
            this.MouseUp += new MouseEventHandler(VerticalScrollHandle_MouseUp);

            UpdateRegion();
        }

        private void VerticalScrollHandle_MouseUp(object sender, MouseEventArgs e)
        {
            InvalidateParent();
        }

        private void VerticalScrollHandle_Resize(object sender, EventArgs e)
        {
            UpdateRegion();
            UpdateGradients();
            InvalidateParent();
        }

        private void VerticalScrollHandle_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            FindDiameter();

            //draw gradient
            float RadiusSquared = (float)Math.Pow(Radius - 1, 2);
            float HalfWidth = (float)(this.Width - 2) / 2.0f;
            float ChordWidth;

            for (double i = 0; i < this.Width; i++)
            {
                ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(HalfWidth - i, 2));
                GradientPen.Color = GradientColors[(int)i];
                memGraphics.g.DrawLine(GradientPen, (float)i + 1, Radius - ChordWidth, (float)i + 1, this.Height - ((Radius - ChordWidth) + 2));
            }

            //draw visible border
            memGraphics.g.DrawArc(BorderPen, 1, 1, Diameter - 2, Diameter - 2, 0, -180);
            memGraphics.g.DrawArc(BorderPen, 1, this.Height - (Diameter + 1), Diameter - 2, Diameter - 2, 0, 180);
            memGraphics.g.DrawLine(BorderPen, 1, Radius, 1, this.Height - (Radius + 2));
            memGraphics.g.DrawLine(BorderPen, this.Width - 2, Radius + 2, this.Width - 2, this.Height - (Radius + 2));

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void CreateDoubleBuffer()
        {
            memGraphics.CreateDoubleBuffer(this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height);
        }

        private void UpdateRegion()
        {
            FindDiameter();

            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();

            //define region
            FormRegion.AddArc(0, 0, Diameter, Diameter, 0, -180);
            FormRegion.AddLine(this.Width - 1, Radius, this.Width - 1, this.Height - Radius);
            FormRegion.AddArc(0, this.Height - (Diameter + 1), Diameter, Diameter, 0, 180);
            FormRegion.AddLine(0, this.Height - (Radius + 1), 0, Radius);

            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Width);
        }

        private void FindDiameter()
        {
            if (this.Height <= this.Width)
                Diameter = this.Height - 1;
            else
                Diameter = this.Width - 1;

            Radius = Diameter / 2.0f;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do nothing
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //turn the form transparent - sweet, eh?
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
}
