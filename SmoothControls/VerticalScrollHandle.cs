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
    public partial class VerticalScrollHandle : UserControl
    {
        private DBGraphics memGraphics;

        private Color BorderColor;
        private float Diameter;
        private float Radius;

        private Color[] GradientColors;
        private Color GradientStart;
        private Color GradientFinish;

        private Color BackgroundGradientStart;
        private Color BackgroundGradientFinish;

        private Pen GradientPen;
        private Pen BorderPen;

        private Bitmap m_bgImage;

        public VerticalScrollHandle()
        {
            InitializeComponent();

            GradientPen = new Pen(Color.Black);
            GradientStart = Color.FromArgb(220, 220, 220);
            GradientFinish = Color.FromArgb(180, 180, 180);

            BackgroundGradientStart = Color.FromArgb(244, 244, 244);
            BackgroundGradientFinish = Color.FromArgb(225, 225, 225);
            UpdateGradients();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            this.Paint += new PaintEventHandler(VerticalScrollHandle_Paint);
            this.Resize += new EventHandler(VerticalScrollHandle_Resize);
            this.MouseUp += new MouseEventHandler(VerticalScrollHandle_MouseUp);

            UpdateBG();
        }

        private void VerticalScrollHandle_MouseUp(object sender, MouseEventArgs e)
        {
            this.Invalidate();
        }

        private void VerticalScrollHandle_Resize(object sender, EventArgs e)
        {
            UpdateGradients();
            UpdateBG();

            InvalidateParent();
        }

        private void VerticalScrollHandle_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            FindDiameter();

            //Gradient.DrawGradient(memGraphics.g, Gradient.GradientDirection.Horizontal, BackgroundGradientStart, BackgroundGradientFinish, this.Width, this.Height);
            memGraphics.g.DrawImage((Image)m_bgImage, 0, 0);

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

         private void UpdateBG()
         {
             m_bgImage = new Bitmap(this.Width, this.Height);
             Color[] caBgColors = Gradient.ComputeGradient(BackgroundGradientStart, BackgroundGradientFinish, this.Width);

             for (int c = 0; c < this.Width; c++)
             {
                 for (int r = 0; r < this.Height; r++)
                     m_bgImage.SetPixel(c, r, caBgColors[c]);
             }
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
        
         /*
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
         */
    }
}
