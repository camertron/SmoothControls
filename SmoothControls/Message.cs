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
using System.Collections;

namespace WildMouse.SmoothControls
{
    internal partial class Message : UserControl
    {
        private const int LETTERCOUNT_MAX = 50;
        private const int LINECOUNT_MAX = 7;
        private const int LINE_HEIGHT = 16;
        private const int HEIGHT_MIN = 100;

        private Color BorderColor;
        private float Diameter;
        private float Radius;
        private Color[] GradientColors;
        private Color GradientStart;
        private Color GradientFinish;
        private Pen GradientPen;
        private Pen BorderPen;
        private Color pTextColor;
        private SolidBrush TextBrush;
        private PrivateFontCollection pfc1;
        private PrivateFontCollection pfc2;
        private string[] TextLines;
        private string[] TextSplitters;

        private Font pFont;
        private Font pBoldFont;
        private int pFontSize;
        private string pText;
        private string pTitle;
        private int pHeight;
        private int pWidth;
        private MessageBoxButtons pButtons;
        private MessageBoxIcon pIcon;

        public Message()
        {
            InitializeComponent();

            pfc1 = General.PrepFont("MyriadPro-Regular.ttf");
            pfc2 = General.PrepFont("MyriadPro-Bold.ttf");
            pFontSize = 10;

	        //pFont = new Font("Arial", pFontSize);
            pFont = new Font(pfc1.Families[0], pFontSize);
            pBoldFont = new Font(pfc2.Families[0], pFontSize, FontStyle.Bold);

            this.Paint += new PaintEventHandler(Message_Paint);
            this.Resize += new EventHandler(Message_Resize);

            Diameter = 20.0f;
            Radius = Diameter / 2.0f;

            GradientPen = new Pen(Color.Black);
            GradientStart = Color.FromArgb(252, 252, 252);
            GradientFinish = Color.FromArgb(223, 223, 223);
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
            UpdateGradients();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            pTextColor = Color.Black;
            TextBrush = new SolidBrush(pTextColor);

            pHeight = 150;
            pWidth = 450;

            TextLines = null;
            TextSplitters = new string[1] { " " };
        }

        ~Message()
        {
            pfc1.Families[0].Dispose();
            pfc2.Families[0].Dispose();

            pfc1.Dispose();
            pfc2.Dispose();

            pFont.Dispose();
            pBoldFont.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public MessageBoxButtons Buttons
        {
            get { return pButtons; }
            set
            {
                pButtons = value;
                FixButtons();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public MessageBoxIcon Icon
        {
            get { return pIcon; }
            set
            {
                pIcon = value;
                this.Invalidate();
            }
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
                this.Invalidate();
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
                ParseText();
                pHeight = HEIGHT_MIN + (LINE_HEIGHT * TextLines.Length);
                this.Message_Resize(this, EventArgs.Empty);
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string Title
        {
            get { return pTitle; }
            set
            {
                pTitle = value;
                this.Invalidate();
            }
        }

        private void Message_Resize(object sender, EventArgs e)
        {
            this.Height = pHeight;
            this.Width = pWidth;

            Button1.Top = this.Height - (Button1.Height + 15);
            Button2.Top = Button1.Top;
            Button3.Top = Button1.Top;

            Button1.Left = this.Width - (Button1.Width + 15);
            Button2.Left = Button1.Left - (Button2.Width + 5);
            Button3.Left = Button2.Left - (Button3.Width + 5);

            UpdateGradients();
        }

        private void FixButtons()
        {
            switch (pButtons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    Button1.Text = "Ignore";
                    Button1.Visible = true;
                    Button2.Text = "Retry";
                    Button2.Visible = true;
                    Button3.Text = "Abort";
                    Button3.Visible = true;
                    break;
                case MessageBoxButtons.OK:
                    Button1.Text = "Ok";
                    Button1.Visible = true;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    break;
                case MessageBoxButtons.OKCancel:
                    Button1.Text = "Cancel";
                    Button1.Visible = true;
                    Button2.Text = "Ok";
                    Button2.Visible = true;
                    Button3.Visible = false;
                    break;
                case MessageBoxButtons.RetryCancel:
                    Button1.Text = "Cancel";
                    Button1.Visible = true;
                    Button2.Text = "Retry";
                    Button2.Visible = true;
                    Button3.Visible = false;
                    break;
                case MessageBoxButtons.YesNo:
                    Button1.Text = "No";
                    Button1.Visible = true;
                    Button2.Text = "Yes";
                    Button2.Visible = true;
                    Button3.Visible = false;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    Button1.Text = "Cancel";
                    Button1.Visible = true;
                    Button2.Text = "No";
                    Button2.Visible = true;
                    Button3.Text = "Yes";
                    Button3.Visible = true;
                    break;
            }
        }

        private void Message_Paint(object sender, PaintEventArgs e)
        {
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1.0f;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            for (double i = 0; i < this.Height; i ++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = GradientColors[(int)i];
                e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            //rounded corners
            e.Graphics.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);
            e.Graphics.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);

            //border lines
            e.Graphics.DrawLine(BorderPen, Radius, 0, this.Width - Radius, 0);
            e.Graphics.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);
            e.Graphics.DrawLine(BorderPen, 0, Radius, 0, this.Height - (Radius + 1));
            e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));

            switch (pIcon)
            {
                case MessageBoxIcon.Information:
                    e.Graphics.DrawImage((Image)General.GetBitmap("information.png"), 20, 20);
                    break;
                case MessageBoxIcon.Error:
                    e.Graphics.DrawImage((Image)General.GetBitmap("error.png"), 20, 20);
                    break;
                case MessageBoxIcon.Exclamation:
                    e.Graphics.DrawImage((Image)General.GetBitmap("exclamation.png"), 20, 20);
                    break;
                case MessageBoxIcon.Question:
                    e.Graphics.DrawImage((Image)General.GetBitmap("question.png"), 20, 20);
                    break;
            }

            int X = 100;
            int Y = 50;

            e.Graphics.DrawString(pTitle, pBoldFont, TextBrush, 100, 20);

            if (TextLines != null)
            {
                for (int i = 0; i < TextLines.Length; i++)
                {
                    e.Graphics.DrawString(TextLines[i], pFont, TextBrush, X, Y);
                    Y += LINE_HEIGHT;
                }
            }
        }

        private void ParseText()
        {
            ArrayList Final = new ArrayList();
            string[] Words = pText.Split(TextSplitters, StringSplitOptions.RemoveEmptyEntries);
            string CurLine = "";

            for (int i = 0; i < Words.Length; i ++)
            {
                if ((Words[i].Length + CurLine.Length) > LETTERCOUNT_MAX)
                {
                    Final.Add(CurLine);
                    CurLine = "";
                }

                CurLine += Words[i] + " ";
            }

            if (CurLine.Length > 0)
                Final.Add(CurLine);

            TextLines = new string[Final.Count];

            for (int i = 0; i < Final.Count; i ++)
                TextLines[i] = Final[i].ToString();
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
