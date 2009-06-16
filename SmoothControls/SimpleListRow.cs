using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using GDIDB;

namespace WildMouse.SmoothControls
{
    public partial class SimpleListRow : UserControl, IListRow
    {
        private bool m_bSelected;
        private const int CONTROL_HEIGHT = 17;

        private SolidBrush TextBrush;
        private PrivateFontCollection pfc;
        private Font pFont;
        private int pFontSize;
        private DBGraphics memGraphics;
        public event CmdKeyPressedHandler CmdKeyPressed;

        public SimpleListRow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Resize += new EventHandler(SimpleListRow_Resize);
            this.Paint += new PaintEventHandler(SimpleListRow_Paint);

            pFontSize = 10;
            MakeFont();
            SizeLbl.Font = pFont;
            SizeLbl.Text = "Round Button";
            SizeLbl.TextAlign = ContentAlignment.MiddleCenter;
            SizeLbl.ForeColor = Color.Black;
            TextBrush = new SolidBrush(SizeLbl.ForeColor);

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

        private void MakeFont()
        {
            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFont = new Font(pfc.Families[0], pFontSize);
        }

        private void SimpleListRow_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = TextRenderingHint.AntiAlias;

            memGraphics.g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
            memGraphics.g.DrawString(SizeLbl.Text, pFont, TextBrush, 3, (this.Height / 2) - (SizeLbl.Height / 2));

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
        }

        private void SimpleListRow_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
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

        public bool Selected
        {
            get { return m_bSelected; }
            set { m_bSelected = value; }
        }

        public int ControlHeight
        {
            get { return CONTROL_HEIGHT; }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, Keys keyData)
        {
            if (CmdKeyPressed != null)
                CmdKeyPressed(this, keyData);

            return true;
        }
    }
}
