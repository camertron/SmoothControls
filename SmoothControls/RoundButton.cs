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
using System.Drawing.Drawing2D;
using GDIDB;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("Click")]
    public partial class RoundButton : UserControl
    {
        private DBGraphics memGraphics;

        private Color BorderColor;
        private float Diameter;
        private float Radius;
        private Color[] GradientColors;
        private Color[] PressedColors;
        private Color[] NotPressedColors;
        private Color NPGradientStart;
        private Color NPGradientFinish;
        private Color PGradientStart;
        private Color PGradientFinish;
        private Pen GradientPen;
        private Pen BorderPen;
        private Pen HighlightPen;
        private SolidBrush TextBrush;
        private PrivateFontCollection pfc;
        private Pen FocusDashedLine;

        private Font pFont;
        private int pFontSize;
        private bool pSticky;
        private ButtonState pState;

        public new event System.EventHandler Click;
        public new event System.Windows.Forms.MouseEventHandler MouseUp;
        public new event System.Windows.Forms.MouseEventHandler MouseDown;

        public enum ButtonState
        {
            Pressed = 1,
            NotPressed = 2
        }

        /*
        ~RoundButton()
        {
            pfc.Families[0].Dispose();
            pfc.Dispose();
            pFont.Dispose();
        }
        */

        public RoundButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);
            HighlightPen = new Pen(Color.FromArgb(100, 255, 255, 255));

            NPGradientStart = Color.FromArgb(252, 252, 252);
            NPGradientFinish = Color.FromArgb(223, 223, 223);
            PGradientStart = Color.FromArgb(194, 194, 194);
            PGradientFinish = Color.FromArgb(239, 239, 239);
            UpdateGradients();
            GradientColors = NotPressedColors;

            GradientPen = new Pen(Color.Black);
            Radius = 0;
            Diameter = 0;

            pFontSize = 10;
            MakeFont();
            SizeLbl.Font = pFont;
            SizeLbl.Text = "Round Button";
            SizeLbl.TextAlign = ContentAlignment.MiddleCenter;
            SizeLbl.ForeColor = Color.Black;
            TextBrush = new SolidBrush(SizeLbl.ForeColor);

            this.Paint += new PaintEventHandler(RoundButton_Paint);
            this.Resize += new EventHandler(RoundButton_Resize);
            ((System.Windows.Forms.UserControl)(this)).MouseDown += new MouseEventHandler(RoundButton_MouseDown);
            ((System.Windows.Forms.UserControl)(this)).MouseUp += new MouseEventHandler(RoundButton_MouseUp);
            ((System.Windows.Forms.UserControl)(this)).Click += new EventHandler(RoundButton_Click);
            this.KeyUp += new KeyEventHandler(RoundButton_KeyUp);
            this.KeyDown += new KeyEventHandler(RoundButton_KeyDown);
            this.GotFocus += new EventHandler(RoundButton_GotFocus);
            this.LostFocus += new EventHandler(RoundButton_LostFocus);

            pState = ButtonState.NotPressed;
            pSticky = false;

            //define form's region
            UpdateRegion();

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            FocusDashedLine = new Pen(Color.FromArgb(120, 120, 120), 1);
            FocusDashedLine.DashPattern = new float[2] { 2, 2 };
        }

        private void RoundButton_LostFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void RoundButton_GotFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        //handle the user pressing the enter key
        private void RoundButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RoundButton_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void RoundButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RoundButton_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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

            FormRegion.AddArc(0, 0, Diameter, Diameter, -90, -180);
            FormRegion.AddLine(Radius, this.Height - 1, this.Width - Diameter, this.Height - 1);
            FormRegion.AddArc(this.Width, 0, Diameter, Diameter, 90, -180);
            FormRegion.AddLine(Radius, 0, this.Width - Diameter, 0);

            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void MakeFont()
        {
            /*
            if (pfc != null)
            {
                pfc.Families[0].Dispose();
                pfc.Dispose();
                pFont.Dispose();
            }
            */

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFont = new Font(pfc.Families[0], pFontSize);
        }

        private void RoundButton_Click(object sender, EventArgs e)
        {
            //if (Click != null)
                //Click(this, EventArgs.Empty);
        }

        private void UpdateGradients()
        {
            PressedColors = Gradient.ComputeGradient(PGradientStart, PGradientFinish, this.Height);
            NotPressedColors = Gradient.ComputeGradient(NPGradientStart, NPGradientFinish, this.Height);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public int FontSize
        {
            get { return pFontSize; }
            set
            {
                pFontSize = value;
                MakeFont();
                SizeLbl.Font = pFont;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
           get { return SizeLbl.Text; }
           set
           {
                SizeLbl.Text = value;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public ContentAlignment TextAlign
        {
            get { return SizeLbl.TextAlign; }
            set
            {
                SizeLbl.TextAlign = value;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public Color TextColor
        {
            get { return SizeLbl.ForeColor; }
            set
            {
                SizeLbl.ForeColor = value;
                TextBrush.Color = SizeLbl.ForeColor;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Sticky
        {
            get { return pSticky; }
            set { pSticky = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public ButtonState State
        {
            get { return pState; }
            set
            {
                pState = value;
                InvalidateParent();
            }
        }

        private void RoundButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (pState == ButtonState.NotPressed)
            {
                pState = ButtonState.Pressed;
                InvalidateParent();
            }
            else
                pState = ButtonState.NotPressed;  //don't invalidate just yet!

            if (MouseDown != null)
                MouseDown(this, e);

            if (Click != null)
                Click(this, EventArgs.Empty);
        }

        private void RoundButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (Sticky)
                this.Invalidate();
            else
            {
                pState = ButtonState.NotPressed;
                InvalidateParent();
            }

            if (MouseUp != null)
                MouseUp(this, e);
        }

        private void RoundButton_Resize(object sender, EventArgs e)
        {
            //in case the height is adjusted, re-compute the gradient
            UpdateGradients();
            UpdateRegion();
            CreateDoubleBuffer();
        }

        private void FindDiameter()
        {
            if (this.Height <= this.Width)
                Diameter = this.Height - 1;
            else
                Diameter = this.Width - 1;

            Radius = Diameter / 2.0f;
        }

        private void RoundButton_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = TextRenderingHint.AntiAlias;

            switch (pState)
            {
                case ButtonState.NotPressed:
                    GradientColors = NotPressedColors;
                    break;
                case ButtonState.Pressed:
                    GradientColors = PressedColors;
                    break;
            }

            FindDiameter();

            //draw gradient
            float RadiusSquared = (float)Math.Pow(Radius - 1, 2);
            float HalfHeight = (float)(this.Height - 2) / 2.0f;
            float ChordWidth;

            for (double i = 0; i < this.Height; i ++)
            {
                ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(HalfHeight - i, 2));
                GradientPen.Color = GradientColors[(int)i];
                memGraphics.g.DrawLine(GradientPen, Radius - ChordWidth, (float)i + 1, this.Width - ((Radius - ChordWidth) + 2), (float)i + 1);
            }

            //draw visible border
            memGraphics.g.DrawArc(BorderPen, 1, 1, Diameter - 2, Diameter - 2, 90, 180);
            memGraphics.g.DrawArc(BorderPen, this.Width - (Diameter + 1), 1, Diameter - 2, Diameter - 2, 90, -180);
            memGraphics.g.DrawLine(BorderPen, Radius, 1, this.Width - (Radius + 2), 1);
            memGraphics.g.DrawLine(BorderPen, Radius, this.Height - 2, this.Width - (Radius + 1), this.Height - 2);

            float Top = 0;
            float Left = 0;
            int LblHeight = SizeLbl.Height + 1;  //a little tweaking...
            int LblWidth = SizeLbl.Width - 5;

            switch (SizeLbl.TextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    Top = this.Height - Height;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    Top = (this.Height / 2) - (LblHeight / 2);
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    Top = 0;
                    break;
            }

            switch (SizeLbl.TextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    Left = (this.Width / 2) - (LblWidth / 2);
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    Left = 0;
                    break;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    Left = this.Width - LblWidth;
                    break;
            }

            memGraphics.g.DrawString(SizeLbl.Text, pFont, TextBrush, Left, Top);
            SizeF TextSize = memGraphics.g.MeasureString(SizeLbl.Text, pFont);
            
            if (this.Focused)
                memGraphics.g.DrawLine(FocusDashedLine, Left + 3, (Top + TextSize.Height) - 3, (Left + TextSize.Width) - 4, (Top + TextSize.Height) - 3);

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
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
