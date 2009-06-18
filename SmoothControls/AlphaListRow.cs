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
using System.Drawing.Text;

namespace WildMouse.SmoothControls
{
    public partial class AlphaListRow : UserControl, IListRow
    {
        private DBGraphics memGraphics;
        private const int GRADIENT_HEIGHT = 15;
        public static int CONTROL_HEIGHT = 18;
        private Color GradientStart;
        private Color GradientFinish;
        private Color[] Gradient;
        private Pen GradientPen;
        private Pen BorderPen;
        private string pText;

        private Font pFont;
        private int pFontSize;
        private SolidBrush TextBrush;

        public event CmdKeyPressedHandler CmdKeyPressed;

        public AlphaListRow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            this.Paint += new PaintEventHandler(AlphaListRow_Paint);
            this.Resize += new EventHandler(AlphaListRow_Resize);

            GradientPen = new Pen(Color.Black);
            BorderPen = new Pen(Color.FromArgb(180, 180, 180));

            GradientStart = Color.FromArgb(252, 252, 252);
            GradientFinish = Color.FromArgb(160, 160, 160);

            pFontSize = 10;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
            TextBrush = new SolidBrush(Color.Black);

            UpdateGradients();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return pText; }
            set { pText = value; InvalidateParent(); }
        }

        public bool Selected
        {
            get { return false; }
            set { }
        }

        public int ControlHeight
        {
            get { return CONTROL_HEIGHT; }
        }

        private void UpdateGradients()
        {
            Gradient = Graphics.Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void AlphaListRow_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
        }

        private void AlphaListRow_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //memGraphics.g.FillRectangle(new SolidBrush(Color.White), 0, 0, this.Width, this.Height);

            for (int i = 0; i < GRADIENT_HEIGHT; i++)
            {
                GradientPen.Color = Gradient[i];
                memGraphics.g.DrawLine(GradientPen, 0, i + 1, this.Width, i + 1);
            }

            memGraphics.g.DrawLine(BorderPen, 0, 0, this.Width, 0);
            memGraphics.g.DrawLine(BorderPen, 0, GRADIENT_HEIGHT + 2, this.Width, GRADIENT_HEIGHT + 2);

            memGraphics.g.DrawString(pText, pFont, TextBrush, 5, 0);

            memGraphics.Render(e.Graphics);
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

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, Keys keyData)
        {
            if (CmdKeyPressed != null)
                CmdKeyPressed(this, keyData);

            return true;
        }
    }
}
