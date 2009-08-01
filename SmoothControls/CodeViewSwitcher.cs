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
    public partial class CodeViewSwitcher : UserControl
    {
        public enum SelectedView
        {
            Microcode = 1,
            MainProgram = 2
        }

        private Color m_cSelectedColor;
        private Color m_cDeselectedColor;
        private SolidBrush m_bshFillBrush;
        private SelectedView m_svCurView;
        private int m_iRadius;
        private int m_iButtonWidth;
        private Font m_fntFont;
        private int m_iFontSize;
        private SolidBrush m_bshTextBrush;

        public delegate void SelectionChangedEventHandler(object sender, SelectedView svNewView);
        public event SelectionChangedEventHandler SelectionChanged;

        public CodeViewSwitcher()
        {
            InitializeComponent();

            m_cSelectedColor = Color.FromArgb(85, 85, 85);
            m_cDeselectedColor = Color.FromArgb(163, 163, 163);
            m_bshFillBrush = new SolidBrush(m_cSelectedColor);

            m_bshTextBrush = new SolidBrush(Color.White);

            m_svCurView = SelectedView.Microcode;
            m_iRadius = 11;
            m_iButtonWidth = 110;

            m_iFontSize = 11;
            m_fntFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, m_iFontSize);

            this.Paint += new PaintEventHandler(CodeViewSwitcher_Paint);
            this.Resize += new EventHandler(CodeViewSwitcher_Resize);
            this.Click += new EventHandler(CodeViewSwitcher_Click);
        }

        public SelectedView View
        {
            get { return m_svCurView; }
            set { m_svCurView = value; this.Invalidate(); }
        }

        private void CodeViewSwitcher_Click(object sender, EventArgs e)
        {
            Point ptCursorLoc = this.PointToClient(Cursor.Position);

            if ((ptCursorLoc.X >= 0) && (ptCursorLoc.X <= (m_iButtonWidth + (m_iRadius * 2))))
                m_svCurView = SelectedView.Microcode;
            else if ((ptCursorLoc.X > pbSwitchIcon.Right) && (ptCursorLoc.X <= this.Width))
                m_svCurView = SelectedView.MainProgram;

            if (SelectionChanged != null)
                SelectionChanged(this, m_svCurView);

            this.Invalidate();
        }

        private void CodeViewSwitcher_Resize(object sender, EventArgs e)
        {
            this.Height = m_iRadius * 2;
        }

        private void CodeViewSwitcher_Paint(object sender, PaintEventArgs e)
        {
            SizeF sfStringMeasure;
            int iX = 0, iY = 0;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // *** FIRST BUTTON ***

            if (m_svCurView == SelectedView.Microcode)
                m_bshFillBrush.Color = m_cSelectedColor;
            else
                m_bshFillBrush.Color = m_cDeselectedColor;

            e.Graphics.FillEllipse(m_bshFillBrush, 0, 0, m_iRadius * 2, m_iRadius * 2);
            e.Graphics.FillEllipse(m_bshFillBrush, m_iButtonWidth, 0, m_iRadius * 2, m_iRadius * 2);
            e.Graphics.FillRectangle(m_bshFillBrush, m_iRadius, 0, m_iButtonWidth, m_iRadius * 2);

            sfStringMeasure = e.Graphics.MeasureString("Microcode", m_fntFont);
            iX = ((m_iButtonWidth / 2) - ((int)sfStringMeasure.Width / 2)) + m_iRadius;
            iY = (this.Height / 2) - ((int)sfStringMeasure.Height / 2);

            e.Graphics.DrawString("Microcode", m_fntFont, m_bshTextBrush, iX, iY);

            // *** SECOND BUTTON ***

            if (m_svCurView == SelectedView.MainProgram)
                m_bshFillBrush.Color = m_cSelectedColor;
            else
                m_bshFillBrush.Color = m_cDeselectedColor;

            e.Graphics.FillEllipse(m_bshFillBrush, pbSwitchIcon.Right + 6, 0, m_iRadius * 2, m_iRadius * 2);
            e.Graphics.FillEllipse(m_bshFillBrush, pbSwitchIcon.Right + 6 + m_iButtonWidth, 0, m_iRadius * 2, m_iRadius * 2);
            e.Graphics.FillRectangle(m_bshFillBrush, pbSwitchIcon.Right + 6 + m_iRadius, 0, m_iButtonWidth, m_iRadius * 2);

            sfStringMeasure = e.Graphics.MeasureString("Main Program", m_fntFont);
            iX = ((m_iButtonWidth / 2) - ((int)sfStringMeasure.Width / 2)) + m_iRadius;
            iY = (this.Height / 2) - ((int)sfStringMeasure.Height / 2);

            e.Graphics.DrawString("Main Program", m_fntFont, m_bshTextBrush, pbSwitchIcon.Right + 6 + iX, iY);
        }
    }
}
