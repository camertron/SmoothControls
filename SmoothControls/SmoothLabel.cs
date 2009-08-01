using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace WildMouse.SmoothControls
{
    public partial class SmoothLabel : UserControl
    {
        private Font pFont;
        private int pFontSize;
        //private PrivateFontCollection pfc;
        private Color pTextColor;
        private SolidBrush TextBrush;
        private string pText;
        private ContentAlignment pTextAlign;
        private bool bFontBold;
        private bool bFontItalic;

        public SmoothLabel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SmoothLabel_Paint);

            pTextColor = Color.Black;
            TextBrush = new SolidBrush(pTextColor);

            pFontSize = 10;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);

            pTextAlign = ContentAlignment.TopLeft;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Bold
        {
            get { return bFontBold; }
            set
            {
                bFontBold = value;
                RedoFont();
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Italic
        {
            get { return bFontItalic; }
            set
            {
                bFontItalic = value;
                RedoFont();
                this.Invalidate();
            }
        }

        private void RedoFont()
        {
            FontStyle FinalStyle;

            if (bFontItalic && bFontBold)
                FinalStyle = FontStyle.Bold & FontStyle.Italic;
            else if (bFontItalic)
                FinalStyle = FontStyle.Italic;
            else
                FinalStyle = FontStyle.Bold;

            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
            pFont = new Font(pFont.FontFamily, pFontSize, FinalStyle);
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
        public int FontSize
        {
            get { return pFontSize; }
            set
            {
                pFontSize = value;
                RedoFont();
                this.Invalidate();
            }
        }

        private void SmoothLabel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);

            float Top = 0;
            float Left = 0;

            SizeF sfStringSize = e.Graphics.MeasureString(pText, pFont);
            int LblHeight = (int)sfStringSize.Height;
            int LblWidth = (int)sfStringSize.Width;

            switch (pTextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    Top = this.Height - LblHeight;
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

            switch (pTextAlign)
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

            e.Graphics.DrawString(pText, pFont, TextBrush, Left, Top);
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
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public ContentAlignment TextAlign
        {
            get { return pTextAlign; }
            set
            {
                pTextAlign = value;
                this.Invalidate();
            }
        }
    }
}
