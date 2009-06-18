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
using GDIDB;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("CheckedChanged")]
    public partial class CheckBox : UserControl
    {
        private Color BorderColor;
        private float Diameter;
        private float Radius;
        private Color[] GradientColors;
        private Color[] NPGradientColors;
        private Color[] PGradientColors;
        private Color NPGradientStart;
        private Color NPGradientFinish;
        private Color PGradientStart;
        private Color PGradientFinish;
        private Pen GradientPen;
        private Pen BorderPen;
        private Color pTextColor;
        private SolidBrush TextBrush;
        private Color CheckColor;
        private Pen CheckPen;

        private Font pFont;
        private int pFontSize;
        private string pText;
        private bool pChecked;

        private DBGraphics memGraphics;

        private const int BOXDIM = 14;

        public event System.EventHandler CheckedChanged;

        public CheckBox()
        {
            InitializeComponent();

            pFontSize = 10;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);

            this.Paint += new PaintEventHandler(CheckBox_Paint);
            this.Resize += new EventHandler(CheckBox_Resize);

            Diameter = 6.0f;
            Radius = 3.0f;

            GradientPen = new Pen(Color.Black);
            NPGradientStart = Color.FromArgb(252, 252, 252);
            NPGradientFinish = Color.FromArgb(223, 223, 223);
            PGradientStart = Color.FromArgb(194, 194, 194);
            PGradientFinish = Color.FromArgb(239, 239, 239);
            UpdateGradients();

            GradientColors = NPGradientColors;

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            pTextColor = Color.Black;
            TextBrush = new SolidBrush(pTextColor);

            CheckColor = Color.FromArgb(70, 70, 70);
            CheckPen = new Pen(CheckColor);
            CheckPen.Width = 2f;

            this.MouseDown += new MouseEventHandler(CheckBox_MouseDown);
            this.MouseUp += new MouseEventHandler(CheckBox_MouseUp);
            this.Click += new EventHandler(CheckBox_Click);

            UpdateRegion();

            memGraphics = new DBGraphics();
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

        private void CheckBox_Click(object sender, EventArgs e)
        {
            pChecked = (! pChecked);
            InvalidateParent();

            if (CheckedChanged != null)
                CheckedChanged(this, EventArgs.Empty);
        }

        private void CheckBox_MouseUp(object sender, MouseEventArgs e)
        {
            GradientColors = NPGradientColors;
            InvalidateParent();
        }

        private void CheckBox_MouseDown(object sender, MouseEventArgs e)
        {
            GradientColors = PGradientColors;
            InvalidateParent();
        }

        private void UpdateRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();

            FormRegion.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void UpdateGradients()
        {
            NPGradientColors = Gradient.ComputeGradient(NPGradientStart, NPGradientFinish, BOXDIM);
            PGradientColors = Gradient.ComputeGradient(PGradientStart, PGradientFinish, BOXDIM);
        }

        private void CheckBox_Resize(object sender, EventArgs e)
        {
            this.Height = BOXDIM;
            UpdateGradients();
            UpdateRegion();
            CreateDoubleBuffer();
        }

        private void CheckBox_Paint(object sender, PaintEventArgs e)
        {
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;

            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = TextRenderingHint.AntiAlias;

            for (double i = 0; i < this.Height; i ++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = GradientColors[(int)i];
                memGraphics.g.DrawLine(GradientPen, Radius - ChordWidth, (float)i, BOXDIM - ((Radius - ChordWidth) + 1), (float)i);
            }

            //rounded corners
            memGraphics.g.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);
            memGraphics.g.DrawArc(BorderPen, BOXDIM - Diameter, 0, Diameter, Diameter, -90, 90);
            memGraphics.g.DrawArc(BorderPen, 0, BOXDIM - (Diameter + 1), Diameter, Diameter, 90, 90);
            memGraphics.g.DrawArc(BorderPen, BOXDIM - Diameter, BOXDIM - (Diameter + 1), Diameter, Diameter, 90, -90);
            
            //border lines
            memGraphics.g.DrawLine(BorderPen, Radius, 0, BOXDIM - Radius, 0);
            memGraphics.g.DrawLine(BorderPen, Radius, this.Height - 1, BOXDIM - Radius, this.Height - 1); //bottom
            memGraphics.g.DrawLine(BorderPen, 0, Radius, 0, this.Height - (Radius + 1));
            memGraphics.g.DrawLine(BorderPen, BOXDIM, Radius, BOXDIM, this.Height - (Radius + 1));

            if (pChecked)
            {
                memGraphics.g.DrawLine(CheckPen, 2, 2, BOXDIM - 2, BOXDIM - 3);
                memGraphics.g.DrawLine(CheckPen, BOXDIM - 2, 2, 2, BOXDIM - 3);
                //memGraphics.g.DrawLine(CheckPen, 2, BOXDIM / 3, BOXDIM / 2, BOXDIM - 3);
                //memGraphics.g.DrawLine(CheckPen, BOXDIM / 2, BOXDIM - 3, BOXDIM + 2, 0);
            }

            memGraphics.g.DrawString(pText, pFont, TextBrush, BOXDIM + 5, -2);

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public Color TextColor
        {
            get { return pTextColor; }
            set
            {
                pTextColor = value;
                TextBrush.Color = pTextColor;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return pText; }
            set
            {
                pText = value;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Checked
        {
            get { return pChecked; }
            set
            {
                pChecked = value;
                InvalidateParent();

                if (CheckedChanged != null)
                    CheckedChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing
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
