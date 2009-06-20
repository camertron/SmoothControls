using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using WildMouse.Graphics;
using GDIDB;

namespace WildMouse.SmoothControls
{
    public partial class IconListRow : UserControl, IListRow
    {
        public const int C_CONTROL_HEIGHT = 35;
        private const int C_DEFAULT_TEXT_LEFT = 50;

        private DBGraphics m_dbgMemGraphics;

        private Color m_cBorderColor;
        private Color[] m_acGradientColors;
        private Color m_cGradientStart;
        private Color m_cGradientFinish;
        private Pen m_pGradientPen;
        private SolidBrush m_sbTextBrush;
        private Bitmap pIcon;
        private bool bSelected;
        private SolidBrush BackBrush;
        private Pen m_pnSeparatorPen;
        private Color m_clrSeparatorColor;
        private int m_iTextLeft;

        private Font pFont;
        private int pFontSize;

        public event CmdKeyPressedHandler CmdKeyPressed;

        public IconListRow()
        {
            Init();
        }

        public void Init()
        {
            InitializeComponent();

            pIcon = null;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Resize += new EventHandler(IconListRow_Resize);
            this.Paint += new PaintEventHandler(IconListRow_Paint);

            //figure out gradient stuff
            m_cBorderColor = Color.Silver;
            m_cGradientStart = Color.FromArgb(215, 215, 215);
            m_cGradientFinish = Color.FromArgb(183, 183, 183);
            m_pGradientPen = new Pen(Color.Black);  //black as placeholder
            UpdateGradients();
            
            //figure out font stuff
            pFontSize = 10;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
            SizeLbl.Font = pFont;
            SizeLbl.Text = "FatListViewRow";
            SizeLbl.TextAlign = ContentAlignment.MiddleCenter;
            SizeLbl.ForeColor = Color.Black;
            m_sbTextBrush = new SolidBrush(SizeLbl.ForeColor);

            m_dbgMemGraphics = new DBGraphics();
            CreateDoubleBuffer();

            BackBrush = new SolidBrush(this.BackColor);
            this.BackColorChanged += new EventHandler(IconListRow_BackColorChanged);

            m_clrSeparatorColor = Color.FromArgb(220, 220, 220);
            m_pnSeparatorPen = new Pen(m_clrSeparatorColor);

            m_iTextLeft = C_DEFAULT_TEXT_LEFT;
        }

        public IconListRow(string sInitText, Bitmap bmpInitIcon)
        {
            Init();
            SizeLbl.Text = sInitText;
            pIcon = bmpInitIcon;

            this.Invalidate();
        }

        public Color SeparatorColor
        {
            get { return m_clrSeparatorColor; }
            set
            {
                m_clrSeparatorColor = value;
                m_pnSeparatorPen.Color = value;
                InvalidateParent();
            }
        }

        private void IconListRow_BackColorChanged(object sender, EventArgs e)
        {
            BackBrush.Color = this.BackColor;
        }  

        public bool Selected
        {
            get { return bSelected; }
            set { bSelected = value; InvalidateParent(); }
        }

        public int ControlHeight
        {
            get { return C_CONTROL_HEIGHT; }
        }

        public int TextLeft
        {
            get { return m_iTextLeft; }
            set { m_iTextLeft = value; this.Invalidate(); }
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
            m_dbgMemGraphics.CreateDoubleBuffer(this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height);
        }

        private void IconListRow_Paint(object sender, PaintEventArgs e)
        {
            m_dbgMemGraphics.g.TextRenderingHint = TextRenderingHint.AntiAlias;
            m_dbgMemGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;

            if (bSelected)
            {
                for (int i = 0; i < this.Height; i++)
                {
                    m_pGradientPen.Color = m_acGradientColors[i];
                    m_dbgMemGraphics.g.DrawLine(m_pGradientPen, 0, i, this.Width, i);
                }
            }
            else
                m_dbgMemGraphics.g.FillRectangle(BackBrush, -1, -1, this.Width + 1, this.Height + 1);

            if (pIcon != null)
            {
                m_dbgMemGraphics.g.DrawImage((Image)pIcon, (m_iTextLeft / 2) - (pIcon.Width / 2), (this.Height / 2) - (pIcon.Height / 2), pIcon.Width, pIcon.Height);
                m_dbgMemGraphics.g.DrawString(SizeLbl.Text, pFont, m_sbTextBrush, m_iTextLeft, ((this.Height / 2) - (SizeLbl.Height / 2)) - 1);
            }
            else
                m_dbgMemGraphics.g.DrawString(SizeLbl.Text, pFont, m_sbTextBrush, ((this.Width / 2) - (SizeLbl.Width / 2)) + 3, ((this.Height / 2) - (SizeLbl.Height / 2)) - 1);

            //memGraphics.g.DrawLine(new Pen(Color.White), 0, this.Height - 1, this.Width, this.Height - 1);
            m_dbgMemGraphics.g.DrawLine(m_pnSeparatorPen, 0, this.Height - 1, this.Width, this.Height - 1);

            m_dbgMemGraphics.Render(e.Graphics);
        }

        private void IconListRow_Resize(object sender, EventArgs e)
        {
            CreateDoubleBuffer();

            this.Height = C_CONTROL_HEIGHT;
            UpdateGradients();
        }

        private void UpdateGradients()
        {
            m_acGradientColors = Gradient.ComputeGradient(m_cGradientStart, m_cGradientFinish, this.Height);
        }

        public Bitmap Icon
        {
            get { return pIcon; }
            set { pIcon = value; InvalidateParent(); }
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
                pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
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
                m_sbTextBrush.Color = SizeLbl.ForeColor;
                InvalidateParent();
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, Keys keyData)
        {
            if (CmdKeyPressed != null)
                CmdKeyPressed(this, keyData);

            return true;
        }
    }
}
