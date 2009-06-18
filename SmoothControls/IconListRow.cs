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
        public const int CONTROL_HEIGHT = 35;
        private const int ICON_WIDTH = 50;

        private DBGraphics memGraphics;

        private Color BorderColor;
        private Color[] GradientColors;
        private Color GradientStart;
        private Color GradientFinish;
        private Pen GradientPen;
        private SolidBrush TextBrush;
        private Bitmap pIcon;
        private bool bSelected;
        private SolidBrush BackBrush;
        private Pen m_pnSeparatorPen;
        private Color m_clrSeparatorColor;

        private Font pFont;
        private int pFontSize;

        public event CmdKeyPressedHandler CmdKeyPressed;

        public IconListRow()
        {
            InitializeComponent();

            pIcon = null;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Resize += new EventHandler(FatListViewRow_Resize);
            this.Paint += new PaintEventHandler(FatListViewRow_Paint);

            //figure out gradient stuff
            BorderColor = Color.Silver;
            GradientStart = Color.FromArgb(215, 215, 215);
            GradientFinish = Color.FromArgb(183, 183, 183);
            GradientPen = new Pen(Color.Black);  //black as placeholder
            UpdateGradients();
            
            //figure out font stuff
            pFontSize = 10;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
            SizeLbl.Font = pFont;
            SizeLbl.Text = "FatListViewRow";
            SizeLbl.TextAlign = ContentAlignment.MiddleCenter;
            SizeLbl.ForeColor = Color.Black;
            TextBrush = new SolidBrush(SizeLbl.ForeColor);

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            BackBrush = new SolidBrush(this.BackColor);
            this.BackColorChanged += new EventHandler(IconListRow_BackColorChanged);

            m_clrSeparatorColor = Color.FromArgb(220, 220, 220);
            m_pnSeparatorPen = new Pen(m_clrSeparatorColor);
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
            get { return CONTROL_HEIGHT; }
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

        private void FatListViewRow_Paint(object sender, PaintEventArgs e)
        {
            memGraphics.g.TextRenderingHint = TextRenderingHint.AntiAlias;
            memGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;

            if (bSelected)
            {
                for (int i = 0; i < this.Height; i++)
                {
                    GradientPen.Color = GradientColors[i];
                    memGraphics.g.DrawLine(GradientPen, 0, i, this.Width, i);
                }
            }
            else
                memGraphics.g.FillRectangle(BackBrush, -1, -1, this.Width + 1, this.Height + 1);

            if (pIcon != null)
            {
                memGraphics.g.DrawImage((Image)pIcon, (ICON_WIDTH / 2) - (pIcon.Width / 2), (this.Height / 2) - (pIcon.Height / 2), pIcon.Width, pIcon.Height);
                memGraphics.g.DrawString(SizeLbl.Text, pFont, TextBrush, ICON_WIDTH, ((this.Height / 2) - (SizeLbl.Height / 2)) - 1);
            }
            else
                memGraphics.g.DrawString(SizeLbl.Text, pFont, TextBrush, ((this.Width / 2) - (SizeLbl.Width / 2)) + 3, ((this.Height / 2) - (SizeLbl.Height / 2)) - 1);

            //memGraphics.g.DrawLine(new Pen(Color.White), 0, this.Height - 1, this.Width, this.Height - 1);
            memGraphics.g.DrawLine(m_pnSeparatorPen, 0, this.Height - 1, this.Width, this.Height - 1);

            memGraphics.Render(e.Graphics);
        }

        private void FatListViewRow_Resize(object sender, EventArgs e)
        {
            CreateDoubleBuffer();

            this.Height = CONTROL_HEIGHT;
            UpdateGradients();
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
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
                TextBrush.Color = SizeLbl.ForeColor;
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
