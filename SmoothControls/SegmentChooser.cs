using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using System.Drawing.Text;
using System.Collections.Specialized;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class SegmentChooser : UserControl
    {
        private Color BorderColor;
        private Pen BorderPen;
        private Color PGradientStart;
        private Color PGradientFinish;
        private Color NPGradientStart;
        private Color NPGradientFinish;
        private Color HoldGradientStart;
        private Color HoldGradientFinish;
        private Color[] HoldGradientColors;
        private Color[] PGradientColors;
        private Color[] NPGradientColors;
        private Color[] GradientColors;
        private Pen GradientPen;
        private float Diameter;
        private float Radius;
        private PrivateFontCollection pfc;
        private int pFontSize;
        private Font pFont;
        private StringCollection pItems;
        private int[] Widths;
        private Color TextColor;
        private SolidBrush TextBrush;
        private Color SeparatorColor;
        private Pen SeparatorPen;

        private int pSelectedIndex;

        private const int CONTROL_HEIGHT = 23;
        private const int PADDING = 5;

        public event System.EventHandler SelectedIndexChanged;

        public SegmentChooser()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SegmentChooser_Paint);
            this.Resize += new EventHandler(SegmentChooser_Resize);
            this.MouseUp += new MouseEventHandler(SegmentChooser_MouseUp);
            this.MouseDown += new MouseEventHandler(SegmentChooser_MouseDown);

            Diameter = 12.0f;
            Radius = 6.0f;

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            NPGradientStart = Color.FromArgb(252, 252, 252);
            NPGradientFinish = Color.FromArgb(223, 223, 223);
            PGradientFinish = Color.FromArgb(194, 194, 194);
            PGradientStart = Color.FromArgb(239, 239, 239);
            HoldGradientStart = Color.FromArgb(219, 219, 219);
            HoldGradientFinish = Color.FromArgb(174, 174, 174);

            GradientPen = new Pen(Color.Gray);

            UpdateGradients();
            GradientColors = PGradientColors;

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;

            pFont = new Font(pfc.Families[0], pFontSize);
            MeasureLbl.Font = pFont;

            pItems = new StringCollection();

            TextColor = Color.Black;
            TextBrush = new SolidBrush(TextColor);

            SeparatorColor = Color.FromArgb(170, 170, 170);
            SeparatorPen = new Pen(SeparatorColor);

            pSelectedIndex = -1;
        }

        public int SelectedIndex
        {
            get { return pSelectedIndex; }
            set
            {
                GradientColors = PGradientColors;
                pSelectedIndex = value;
                this.Invalidate();
            }
        }

        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection Items
        {
            get { return pItems; }
            set
            {
                pItems = value;
                this.Invalidate();
            }
        }

        private void SegmentChooser_MouseDown(object sender, MouseEventArgs e)
        {
            int X = 0;

            for (int i = 0; i < pItems.Count; i ++)
            {
                if ((e.X >= X) && (e.X <= (X + Widths[i])))
                    pSelectedIndex = i;

                X += Widths[i] + (PADDING * 2);
            }

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);

            GradientColors = HoldGradientColors;

            this.Invalidate();
        }

        private void SegmentChooser_MouseUp(object sender, MouseEventArgs e)
        {
            GradientColors = PGradientColors;
            this.Invalidate();
        }

        private void SegmentChooser_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            UpdateGradients();
        }

        private void UpdateGradients()
        {
            NPGradientColors = Gradient.ComputeGradient(NPGradientStart, NPGradientFinish, this.Height);
            PGradientColors = Gradient.ComputeGradient(PGradientStart, PGradientFinish, this.Height);
            HoldGradientColors = Gradient.ComputeGradient(HoldGradientStart, HoldGradientFinish, this.Height);
        }

        private void SegmentChooser_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;

            for (double i = 0; i < this.Height; i++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = NPGradientColors[(int)i];
                e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            Widths = new int[pItems.Count];
            int X = 0;
            int CurWidth = 0;

            for (int i = 0; i < pItems.Count; i ++)
            {
                MeasureLbl.Text = pItems[i];
                Widths[i] = MeasureLbl.Width;

                CurWidth = Widths[i] + (PADDING * 2);

                if (i == pSelectedIndex)
                {
                    for (double g = 0; g < this.Height; g ++)
                    {
                        if (g <= Radius)
                            ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - g, 2));
                        else if ((Height - g) <= Radius)
                            ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(g - (Height - Radius), 2));
                        else
                            ChordWidth = Radius;

                        GradientPen.Color = GradientColors[(int)g];

                        if ((i == 0) && (pItems.Count == 1))
                            e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)g, this.Width - ((Radius - ChordWidth) + 1), (float)g);
                        else if (i == 0)
                            e.Graphics.DrawLine(GradientPen, X + (Radius - ChordWidth), (float)g, X + CurWidth, (float)g);
                        else if (i == (pItems.Count - 1))
                            e.Graphics.DrawLine(GradientPen, X, (float)g, this.Width - ((Radius - ChordWidth) + 1), (float)g);
                        else
                            e.Graphics.DrawLine(GradientPen, X, (float)g, X + CurWidth, (float)g);
                    }
                }

                e.Graphics.DrawString(pItems[i], pFont, TextBrush, X + PADDING + 3, 2);

                if (i > 0)
                    e.Graphics.DrawLine(SeparatorPen, X, 1, X, this.Height - 2);

                X += Widths[i] + (PADDING * 2);
            }

            e.Graphics.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //upper right
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //lower right
            e.Graphics.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //lower left

            e.Graphics.DrawLine(BorderPen, Radius + 1, 0, this.Width - (Radius + 2), 0);  //top
            e.Graphics.DrawLine(BorderPen, Radius + 1, this.Height - 1, this.Width - (Radius + 2), this.Height - 1);  //bottom
            e.Graphics.DrawLine(BorderPen, 0, Radius, 0, this.Height - Radius);  //left
            e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));  //right
        }
    }
}
