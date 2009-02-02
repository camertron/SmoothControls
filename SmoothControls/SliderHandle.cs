using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using GDIDB;

namespace WildMouse.SmoothControls
{
    internal partial class SliderHandle : UserControl
    {
        private float Radius;
        private float Diameter;

        private Pen GradientPen;
        private Color[] GradientColors;
        private Color GradientStart;
        private Color GradientFinish;
        private Pen BorderPen;
        private Color BorderColor;

        private DBGraphics memGraphics;

        public SliderHandle()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(SliderHandle_Paint);
            this.Resize += new EventHandler(SliderHandle_Resize);

            GradientStart = Color.FromArgb(252, 252, 252);
            GradientFinish = Color.FromArgb(223, 223, 223);

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            GradientPen = new Pen(Color.Black);
            UpdateGradients();
            UpdateRegion();

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();
        }

        private void CreateDoubleBuffer()
        {
            memGraphics.CreateDoubleBuffer(this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height);
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void SliderHandle_Resize(object sender, EventArgs e)
        {
            this.Width = this.Height;
            UpdateGradients();
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            FindDiameter();

            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();

            FormRegion.AddEllipse(0, 0, Diameter, Diameter);
            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void FindDiameter()
        {
            Diameter = this.Width;
            Radius = Diameter / 2.0f;
        }

        private void SliderHandle_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            FindDiameter();

            float RadiusSquared = (float)Math.Pow(Radius - 1, 2);
            float HalfHeight = (float)(this.Height - 2) / 2.0f;
            float ChordWidth;

            for (double i = 0; i < this.Height; i++)
            {
                ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(HalfHeight - i, 2));
                GradientPen.Color = GradientColors[(int)i];
                memGraphics.g.DrawLine(GradientPen, Radius - ChordWidth, (float)i + 1, this.Width - ((Radius - ChordWidth) + 2), (float)i + 1);
            }

            memGraphics.g.DrawEllipse(BorderPen, 1, 1, Diameter - 3, Diameter - 3);

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
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
