using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("ValueChanged")]
    public partial class HorizontalSlider : UserControl
    {
        public delegate void ValueChangedEventHandler(object sender, int NewValue);
        public event ValueChangedEventHandler ValueChanged;

        private Color BorderColor;
        private Pen BorderPen;
        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private Pen GradientPen;

        private float Radius;
        private float Diameter;

        private int pMax;
        private int pMin;
        private int pValue;

        private const int CONTROL_HEIGHT = 17;
        private const int PADDING = 15;

        private new Bitmap BackgroundImage;

        public HorizontalSlider()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            this.Paint += new PaintEventHandler(HorizontalSlider_Paint);
            this.Resize += new EventHandler(HorizontalSlider_Resize);

            Radius = 3.5f;
            Diameter = 7.0f;

            GradientPen = new Pen(Color.Black);

            GradientStart = Color.FromArgb(185, 183, 183);
            GradientFinish = Color.FromArgb(127, 125, 125);
            UpdateGradients();

            Handle.MouseMove += new MouseEventHandler(Handle_MouseMove);
            Handle.MouseUp += new MouseEventHandler(Handle_MouseUp);

            UpdateRegion();
            MakeBackgroundBitmap();
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        public int Maximum
        {
            get { return pMax; }
            set
            {
                pMax = value;
                RepositionHandle();
            }
        }

        public int Minimum
        {
            get { return pMin; }
            set
            {
                pMin = value;
                RepositionHandle();
            }
        }

        public int Value
        {
            get { return pValue; }
            set
            {
                pValue = value;
                RepositionHandle();
            }
        }

        private void RepositionHandle()
        {
            float Percent = ((float)pValue - (float)pMin) / ((float)pMax - (float)pMin);
            int RawLeft = (int)((this.Width - (PADDING * 2)) * Percent);

            Handle.Left = (RawLeft - (Handle.Width / 2)) + PADDING;

            InvalidateParent();
        }

        private void CalculateValue()
        {
            //these are algebraic reversals of the formulas above in RepositionHandle()
            float Percent = (float)Handle.Left / ((float)this.Width - (((float)PADDING * 2.0f) - ((float)Handle.Width / 2.0f)));

            //if (Percent < 0.05)
                //Percent = 0;
            //else if (Percent > 0.98)
                //Percent = 1.0f;
            float CalcValue = Percent * ((float)pMax - (float)pMin) + (float)pMin;

            pValue = (int)CalcValue;
        }

        private void UpdateRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();

            FormRegion.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void Handle_MouseUp(object sender, MouseEventArgs e)
        {
            InvalidateParent();
        }

        private void Handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point Pos = this.PointToClient(Cursor.Position);

                if (Pos.X <= PADDING)
                    Handle.Left = PADDING - (Handle.Width / 2);
                else if (Pos.X >= (this.Width - PADDING))
                    Handle.Left = this.Width - (PADDING + (Handle.Width / 2));
                else
                {
                    Handle.Left = Pos.X - (Handle.Width / 2);
                    InvalidateParent();
                }

                CalculateValue();

                if (ValueChanged != null)
                    ValueChanged(this, pValue);
            }
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, (int)Diameter);
        }

        private void HorizontalSlider_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            UpdateGradients();

            Handle.Left = (this.Width / 2) - (Handle.Width / 2);
            Handle.Top = (this.Height / 2) - (Handle.Height / 2);

            UpdateRegion();
            MakeBackgroundBitmap();
        }

        private void HorizontalSlider_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(BackgroundImage, 0, (this.Height / 2) - (Diameter + 1));
        }

        private void MakeBackgroundBitmap()
        {
            BackgroundImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            System.Drawing.Graphics TheGraphics = System.Drawing.Graphics.FromImage((Image)BackgroundImage);

            TheGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //draw gradient
            float RadiusSquared = (float)Math.Pow(Radius - 1, 2);
            float HalfHeight = (float)(this.Height - 2) / 2.0f;
            float ChordWidth;
            float HeightMiddle = ((float)this.Height / 2.0f) - (Radius);

            for (double i = 0; i < this.Height; i++)
            {
                ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(HalfHeight - i, 2));

                if ((i >= HeightMiddle) && (i < this.Height - HeightMiddle))
                {
                    GradientPen.Color = GradientColors[(int)i - (int)HeightMiddle];
                    TheGraphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i + 1, this.Width - ((Radius - ChordWidth) + 1), (float)i + 1);
                }
            }

            TheGraphics.DrawArc(BorderPen, 1, HeightMiddle, Diameter, Diameter, -90, -180);
            TheGraphics.DrawArc(BorderPen, this.Width - (Diameter + 2), HeightMiddle, Diameter, Diameter, -90, 180);
            TheGraphics.DrawLine(BorderPen, Radius + 1, HeightMiddle, this.Width - (Radius + 2), HeightMiddle);
            TheGraphics.DrawLine(BorderPen, Radius + 1, HeightMiddle + Diameter, this.Width - (Radius + 2), HeightMiddle + Diameter);
        }
        
        /*
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
        */
    }
}
