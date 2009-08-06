using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    public partial class MedusaViewSwitcher : UserControl
    {
        private const int C_BUTTON_WIDTH = 76;

        private Color m_cOnStart;
        private Color m_cOnFinish;
        private Color m_cOffStart;
        private Color m_cOffFinish;
        private Color[] m_acOnGradient;
        private Color[] m_acOffGradient;

        private List<string> m_lsButtons;
        private int m_iSelectedIndex;
        private int m_iFontSize;
        private Font m_fntFont;
        private SolidBrush m_bshTextBrush;
        private Pen m_pDividerPen;

        public event EventHandler SelectedIndexChanged;

        public MedusaViewSwitcher()
        {
            InitializeComponent();

            m_cOnStart = Color.FromArgb(183, 183, 183);
            m_cOnFinish = Color.FromArgb(137, 137, 137);
            m_cOffStart = Color.FromArgb(221, 221, 221);
            m_cOffFinish = Color.FromArgb(187, 187, 187);

            CalculateGradients();

            m_iFontSize = 10;
            m_fntFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, m_iFontSize);
            m_bshTextBrush = new SolidBrush(Color.Black);
            m_lsButtons = new List<string>();
            m_pDividerPen = new Pen(Color.FromArgb(128, 128, 128));

            this.Paint += new PaintEventHandler(MedusaViewSwitcher_Paint);
            this.Resize += new EventHandler(MedusaViewSwitcher_Resize);
            this.Click += new EventHandler(MedusaViewSwitcher_Click);
        }

        private void MedusaViewSwitcher_Click(object sender, EventArgs e)
        {
            Point ptMouseLoc = this.PointToClient(Cursor.Position);
            m_iSelectedIndex = ptMouseLoc.X / C_BUTTON_WIDTH;

            this.Invalidate();

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void MedusaViewSwitcher_Resize(object sender, EventArgs e)
        {
            CalculateGradients();
            this.Invalidate();
        }

        private void CalculateGradients()
        {
            m_acOnGradient = Gradient.ComputeGradient(m_cOnStart, m_cOnFinish, this.Height);
            m_acOffGradient = Gradient.ComputeGradient(m_cOffStart, m_cOffFinish, this.Height);
        }

        private void MedusaViewSwitcher_Paint(object sender, PaintEventArgs e)
        {
            Pen pDrawPen = new Pen(Color.Black);
            SizeF sfCurStringMeasure;
            int iX = 0, iY = 0;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            for (int i = 0; i < this.Height; i++)
            {
                pDrawPen.Color = m_acOffGradient[i];
                e.Graphics.DrawLine(pDrawPen, 0, i, this.Width, i);

                //for selected one
                pDrawPen.Color = m_acOnGradient[i];
                e.Graphics.DrawLine(pDrawPen, m_iSelectedIndex * C_BUTTON_WIDTH, i, (m_iSelectedIndex + 1) * C_BUTTON_WIDTH, i);
            }

            for (int i = 0; i < m_lsButtons.Count; i++)
            {
                sfCurStringMeasure = e.Graphics.MeasureString(m_lsButtons[i], m_fntFont);

                iX = (i * C_BUTTON_WIDTH) + ((C_BUTTON_WIDTH / 2) - ((int)sfCurStringMeasure.Width / 2));
                iY = (this.Height / 2) - ((int)sfCurStringMeasure.Height / 2);

                e.Graphics.DrawString(m_lsButtons[i], m_fntFont, m_bshTextBrush, iX, iY);

                //draw divider
                e.Graphics.DrawLine(m_pDividerPen, i * C_BUTTON_WIDTH, 0, i * C_BUTTON_WIDTH, this.Height);
            }

            //draw last divider on the end
            e.Graphics.DrawLine(m_pDividerPen, (m_lsButtons.Count * C_BUTTON_WIDTH) + 1, 0, (m_lsButtons.Count * C_BUTTON_WIDTH) + 1, this.Height);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> Buttons
        {
            get { return m_lsButtons; }
            set { m_lsButtons = value; this.Invalidate(); }
        }

        public int SelectedIndex
        {
            get { return m_iSelectedIndex; }
            set { m_iSelectedIndex = value; this.Invalidate(); }
        }
    }
}
