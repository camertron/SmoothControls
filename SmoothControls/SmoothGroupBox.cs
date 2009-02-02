using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.ComponentModel.Design;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class SmoothGroupBox : UserControl
    {
        private PrivateFontCollection pfc;
        private Font pFont;
        private int pFontSize;

        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private Pen GradientPen;
        private Color BorderColor;
        private Pen BorderPen;
        private Color TextColor;
        private SolidBrush TextBrush;

        private float Diameter;
        private float Radius;

        private int LABEL_OFFSET = 8;
        private int LABEL_PADDING = 2;
        private int START_Y = 8;

        public SmoothGroupBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SmoothGroupBox_Paint);
            this.Resize += new EventHandler(SmoothGroupBox_Resize);

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;
            pFont = new Font(pfc.Families[0], pFontSize);
            MeasureLbl.Font = pFont;

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            Diameter = 10.0f;
            Radius = 5.0f;

            GradientStart = Color.FromArgb(251, 251, 251);
            GradientFinish = Color.FromArgb(251, 251, 251);

            GradientPen = new Pen(Color.Black);

            UpdateGradients();

            MeasureLbl.Text = "Shakespeare";

            TextColor = Color.Black;
            TextBrush = new SolidBrush(TextColor);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return MeasureLbl.Text; }
            set
            {
                MeasureLbl.Text = value;
                this.Invalidate();
            }
        }

        private void SmoothGroupBox_Resize(object sender, EventArgs e)
        {
            UpdateGradients();
        }

        private void UpdateGradients()
        {
            GradientColors = WildMouse.Graphics.Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void SmoothGroupBox_Paint(object sender, PaintEventArgs e)
        {
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            for (double i = 0; i < this.Height; i ++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = GradientColors[(int)i];
                e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i + START_Y, this.Width - ((Radius - ChordWidth) + 1), (float)i + START_Y);
            }

            //rounded corners
            e.Graphics.DrawArc(BorderPen, 0, START_Y, Diameter, Diameter, -90, -90);  //top left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), START_Y, Diameter, Diameter, -90, 90);  //top right
            e.Graphics.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //bottom left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //bottom right

            //border lines
            e.Graphics.DrawLine(BorderPen, Radius, START_Y, Radius + LABEL_OFFSET, START_Y);  //top (left section)
            e.Graphics.DrawLine(BorderPen, Radius + LABEL_OFFSET + MeasureLbl.Width + (LABEL_PADDING * 2), START_Y, this.Width - (Radius + 1), START_Y);  //top (right section)
            e.Graphics.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);  //bottom
            e.Graphics.DrawLine(BorderPen, 0, Radius + START_Y, 0, this.Height - (Radius + 1));  //left
            e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius + START_Y, this.Width - 1, this.Height - (Radius + 1));  //right

            e.Graphics.DrawString(MeasureLbl.Text, pFont, TextBrush, LABEL_OFFSET + (LABEL_PADDING * 2) + Radius + 3, 0);
        }
    }
}
