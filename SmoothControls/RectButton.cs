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
    [DefaultEvent("Click")]
    public partial class RectButton : UserControl
    {
        private Color GradientStart;
        private Color GradientFinish;
        private Color PressedGradientStart;
        private Color PressedGradientFinish;
        private Color[] GradientColors;
        private Color[] PressedGradientColors;
        private Pen GradientPen;
        private Color BorderColor;
        private Pen BorderPen;
        private Bitmap pIcon;

        private float Radius;
        private float Diameter;

        private DBGraphics memGraphics;
        private bool bPressed;

        public RectButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SmoothToggle_Paint);
            this.Resize += new EventHandler(SmoothToggle_Resize);
            this.MouseDown += new MouseEventHandler(RectButton_MouseDown);
            this.MouseUp += new MouseEventHandler(RectButton_MouseUp);

            Diameter = 7.0f;
            Radius = 3.5f;

            GradientStart = Color.FromArgb(249, 249, 249);
            GradientFinish = Color.FromArgb(188, 188, 188);
            PressedGradientFinish = Color.FromArgb(188, 188, 188);
            PressedGradientStart = Color.FromArgb(100, 100, 100);
            GradientPen = new Pen(Color.Black);

            ComputeGradients();

            BorderColor = Color.FromArgb(81, 81, 81);
            BorderPen = new Pen(BorderColor);

            pIcon = null;

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();
        }

        private void RectButton_MouseUp(object sender, MouseEventArgs e)
        {
            bPressed = false;
            InvalidateParent();
        }

        private void RectButton_MouseDown(object sender, MouseEventArgs e)
        {
            bPressed = true;
            InvalidateParent();
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

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public Bitmap Icon
        {
            get { return pIcon; }
            set { pIcon = value; InvalidateParent(); }
        }

        private void ComputeGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
            PressedGradientColors = Gradient.ComputeGradient(PressedGradientStart, PressedGradientFinish, this.Height);
        }

        private void SmoothToggle_Resize(object sender, EventArgs e)
        {
            ComputeGradients();
        }

        private void SmoothToggle_Paint(object sender, PaintEventArgs e)
        {
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;
            Color[] GradientToUse;

            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (bPressed)
                GradientToUse = PressedGradientColors;
            else
                GradientToUse = GradientColors;

            for (double i = 0; i < this.Height; i++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = GradientToUse[(int)i];
                memGraphics.g.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            //rounded corners
            memGraphics.g.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            memGraphics.g.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //top right
            memGraphics.g.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //bottom left
            memGraphics.g.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //bottom right

            //border lines
            memGraphics.g.DrawLine(BorderPen, Radius, 0, this.Width - Radius, 0);  //top
            memGraphics.g.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);  //bottom
            memGraphics.g.DrawLine(BorderPen, 0, Radius, 0, this.Height - (Radius + 1));  //left
            memGraphics.g.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));

            if (pIcon != null)
                memGraphics.g.DrawImage((Image)pIcon, (this.Width / 2) - (pIcon.Width / 2), (this.Height / 2) - (pIcon.Height / 2));

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
        }
    }
}
