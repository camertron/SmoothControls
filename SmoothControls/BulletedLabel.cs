using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WildMouse.SmoothControls
{
    public partial class BulletedLabel : UserControl
    {
        private List<string> m_lsItems;

        private const int C_DEFAULT_MARGIN = 5;
        private const int C_DEFAULT_BULLET_SIZE = 6;
        private const int C_DEFAULT_DIVIDER_WIDTH = 20;
        private const int C_DEFAULT_FONT_SIZE = 10;

        private Font m_fntDrawFont;
        private int m_iFontSize;
        private SolidBrush m_bshBulletBrush;
        private SolidBrush m_bshTextBrush;
        private int m_iBulletSize;
        private int m_iDividerWidth;
        private int m_iMargin;

        public BulletedLabel()
        {
            InitializeComponent();

            m_iBulletSize = C_DEFAULT_BULLET_SIZE;
            m_iDividerWidth = C_DEFAULT_DIVIDER_WIDTH;
            m_iMargin = C_DEFAULT_MARGIN;
            m_iFontSize = C_DEFAULT_FONT_SIZE;

            this.Paint += new PaintEventHandler(BulletedLabel_Paint);
            m_fntDrawFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, m_iFontSize);
            m_bshBulletBrush = new SolidBrush(Color.Black);
            m_bshTextBrush = new SolidBrush(Color.Black);

            m_lsItems = new List<string>();
        }

        public Color BulletColor
        {
            get { return m_bshBulletBrush.Color; }
            set { m_bshBulletBrush.Color = value; }
        }

        public Color TextColor
        {
            get { return m_bshTextBrush.Color; }
            set { m_bshTextBrush.Color = value; }
        }

        public int BulletSize
        {
            get { return m_iBulletSize; }
            set { m_iBulletSize = value; this.Invalidate(); }
        }

        public int DividerWidth
        {
            get { return m_iDividerWidth; }
            set { m_iDividerWidth = value; this.Invalidate(); }
        }

        public int FontSize
        {
            get { return m_iFontSize; }
            set
            {
                m_iFontSize = value;
                m_fntDrawFont = new Font(m_fntDrawFont.FontFamily, m_iFontSize);
            }
        }

        private void BulletedLabel_Paint(object sender, PaintEventArgs e)
        {
            int iX = m_iMargin, iY = m_iMargin;
            int iLargestWidth = 0;
            SizeF sfStrSize;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            for (int i = 0; i < m_lsItems.Count; i++)
            {
                sfStrSize = General.MeasureDisplayStringWidth(e.Graphics, m_lsItems[i], m_fntDrawFont);
                e.Graphics.FillEllipse(m_bshBulletBrush, iX, iY, m_iBulletSize, m_iBulletSize);
                e.Graphics.DrawString(m_lsItems[i], m_fntDrawFont, m_bshTextBrush, iX + m_iBulletSize + 2, iY + ((m_iBulletSize / 2) - (int)(sfStrSize.Height / 2)));
                iY += (int)sfStrSize.Height;

                if ((sfStrSize.Width + m_iBulletSize + 2) > iLargestWidth)
                    iLargestWidth = (int)(sfStrSize.Width + m_iBulletSize + 2);

                if ((iY + (int)sfStrSize.Height) > this.Height)
                {
                    iY = m_iMargin;
                    iX += iLargestWidth + m_iDividerWidth;
                    iLargestWidth = 0;
                }
            }
        }

        public List<string> Items
        {
            get { return m_lsItems; }
            set { m_lsItems = value; this.Invalidate(); }
        }
    }
}
