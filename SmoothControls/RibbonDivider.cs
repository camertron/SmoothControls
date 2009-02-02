using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    public partial class RibbonDivider : UserControl
    {
        private Color pStartColor;
        private Color pEndColor;
        private DividerOrientation pOrientation;

        public enum DividerOrientation
        {
            Vertical = 1,
            Horizontal = 2
        }

        public RibbonDivider()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(RibbonDivider_Paint);
            this.Resize += new EventHandler(RibbonDivider_Resize);

            pStartColor = Color.FromArgb(240, 240, 240);
            pEndColor = Color.FromArgb(170, 170, 170);
            pOrientation = DividerOrientation.Vertical;
        }

        private void RibbonDivider_Resize(object sender, EventArgs e)
        {
            //return;
            switch (pOrientation)
            {
                case DividerOrientation.Vertical:
                    this.Width = 1; break;
                case DividerOrientation.Horizontal:
                    this.Height = 1; break;
            }
        }

        private void RibbonDivider_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            Gradient.GradientDirection Dir;
            Point DrawPoint = new Point();
            int GradientWidth;
            int GradientHeight;

            switch (pOrientation)
            {
                case DividerOrientation.Vertical:
                    Dir = Gradient.GradientDirection.Vertical;
                    DrawPoint.X = 0;
                    DrawPoint.Y = this.Height / 2;
                    GradientWidth = this.Width;
                    GradientHeight = this.Height / 2;
                    break;

                default:  //horizontal case
                    Dir = Gradient.GradientDirection.Horizontal;
                    DrawPoint.X = this.Width / 2;
                    DrawPoint.Y = 0;
                    GradientWidth = this.Width / 2;
                    GradientHeight = this.Height;
                    break;
            }

            WildMouse.Graphics.Gradient.DrawGradient(e.Graphics, Dir, pStartColor, pEndColor, GradientWidth, GradientHeight, new Point(0, 0));
            WildMouse.Graphics.Gradient.DrawGradient(e.Graphics, Dir, pEndColor, pStartColor, GradientWidth, GradientHeight, DrawPoint);
        }

        public Color StartColor
        {
            get { return pStartColor; }
            set { pStartColor = value; this.Invalidate(); }
        }

        public Color EndColor
        {
            get { return pEndColor; }
            set { pEndColor = value; this.Invalidate(); }
        }

        public DividerOrientation Orientation
        {
            get { return pOrientation; }
            set
            {
                if ((pOrientation == DividerOrientation.Vertical) && (value == DividerOrientation.Horizontal))
                    this.Width = this.Height;

                pOrientation = value;
                this.RibbonDivider_Resize(this, EventArgs.Empty);
                this.Invalidate();
            }
        }
    }
}
