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
    public partial class OptionButton : UserControl
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
        private int pOptionIndex;

        DBGraphics memGraphics;

        private const int CIRCLEDIM = 14;
        private const int CHECKED_CIRCLEDIM = 5;

        public event System.EventHandler CheckedChanged;

        public OptionButton()
        {
            InitializeComponent();

            pFontSize = 10;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);

            this.Paint += new PaintEventHandler(OptionButton_Paint);
            this.Resize += new EventHandler(OptionButton_Resize);

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

            this.MouseDown += new MouseEventHandler(OptionButton_MouseDown);
            this.MouseUp += new MouseEventHandler(OptionButton_MouseUp);
            this.Click += new EventHandler(OptionButton_Click);

            UpdateRegion();

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            Diameter = CIRCLEDIM;
            Radius = Diameter / 2.0f;

            OptionIndex = 0;
        }

        public int OptionIndex
        {
            get { return pOptionIndex; }
            set { pOptionIndex = value; }
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

        private void OptionButton_Click(object sender, EventArgs e)
        {
            if (pChecked != true)
            {
                pChecked = true;
                InvalidateParent();

                if (CheckedChanged != null)
                    CheckedChanged(this, EventArgs.Empty);
            }

            Control CurControl;
            
            if (this.Parent != null)
            {   
                for (int i = 0; i < this.Parent.Controls.Count; i ++)
                {
                    CurControl = this.Parent.Controls[i];

                    if (CurControl.GetType() == this.GetType())
                    {
                        if (((OptionButton)CurControl).OptionIndex == OptionIndex)
                        {
                            if (((OptionButton)CurControl) != this)
                            {
                                if (((OptionButton)CurControl).Checked)
                                    ((OptionButton)CurControl).Checked = false;
                            }
                        }
                    }
                }
            }
        }

        private void OptionButton_MouseUp(object sender, MouseEventArgs e)
        {
            GradientColors = NPGradientColors;
            InvalidateParent();
        }

        private void OptionButton_MouseDown(object sender, MouseEventArgs e)
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
            NPGradientColors = Gradient.ComputeGradient(NPGradientStart, NPGradientFinish, CIRCLEDIM);
            PGradientColors = Gradient.ComputeGradient(PGradientStart, PGradientFinish, CIRCLEDIM);
        }

        private void OptionButton_Resize(object sender, EventArgs e)
        {
            this.Height = CIRCLEDIM;
            UpdateGradients();
            UpdateRegion();
            CreateDoubleBuffer();
        }

        private void OptionButton_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = TextRenderingHint.AntiAlias;

            float RadiusSquared = (float)Math.Pow(Radius - 1, 2);
            float HalfHeight = (float)(this.Height - 2) / 2.0f;
            float ChordWidth;

            for (double i = 0; i < this.Height; i ++)
            {
                ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(HalfHeight - i, 2));
                GradientPen.Color = GradientColors[(int)i];
                memGraphics.g.DrawLine(GradientPen, Radius - ChordWidth, (float)i + 1, Radius + ChordWidth, (float)i + 1);
            }

            //Border
            memGraphics.g.DrawEllipse(BorderPen, 0, 0, Diameter - 1, Diameter - 1);
            
            if (pChecked)
            {
                memGraphics.g.FillEllipse(new SolidBrush(Color.Black), ((CIRCLEDIM / 2) - (CHECKED_CIRCLEDIM / 2)) - 1, ((CIRCLEDIM / 2) - (CHECKED_CIRCLEDIM / 2)) - 1, CHECKED_CIRCLEDIM, CHECKED_CIRCLEDIM);
            }

            memGraphics.g.DrawString(pText, pFont, TextBrush, Diameter + 5, -2);

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
