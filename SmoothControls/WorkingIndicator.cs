using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WildMouse.SmoothControls
{
    public partial class WorkingIndicator : UserControl
    {
        private int m_iCurrentIndex;
        private const int C_IMAGE_START = 2;
        private const int C_IMAGE_FINISH = 32;
        private Bitmap[] m_abmpImages;

        public WorkingIndicator()
        {
            InitializeComponent();

            m_iCurrentIndex = C_IMAGE_START;
            this.Resize += new EventHandler(WorkingIndicator_Resize);
            pbAnimCanvas.Paint += new PaintEventHandler(pbAnimCanvas_Paint);

            m_abmpImages = new Bitmap[(C_IMAGE_FINISH - C_IMAGE_START) + 1];

            for (int i = 0; i < m_abmpImages.Length; i++)
                m_abmpImages[i] = null;
        }

        private void pbAnimCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (m_abmpImages[m_iCurrentIndex - C_IMAGE_START] == null)
                m_abmpImages[m_iCurrentIndex - C_IMAGE_START] = General.GetBitmap("process-working_" + m_iCurrentIndex.ToString() + ".png");

            e.Graphics.DrawImage((Image)m_abmpImages[m_iCurrentIndex - C_IMAGE_START], 0, 0);
        }

        private void WorkingIndicator_Resize(object sender, EventArgs e)
        {
            CaptionLbl.Width = this.Width - (CaptionLbl.Left + 10);

            CaptionLbl.Top = (this.Height / 2) - (CaptionLbl.Height / 2);
            pbAnimCanvas.Top = (this.Height / 2) - (pbAnimCanvas.Height / 2);
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            m_iCurrentIndex ++;

            if (m_iCurrentIndex > C_IMAGE_FINISH)
                m_iCurrentIndex = C_IMAGE_START;

            pbAnimCanvas.Invalidate();
        }

        public void AnimateOn()
        {
            AnimTimer.Enabled = true;
        }

        public void AnimateOff()
        {
            AnimTimer.Enabled = false;
        }

        public void Reset()
        {
            m_iCurrentIndex = 0;
            AnimTimer_Tick(this, EventArgs.Empty);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return CaptionLbl.Text; }
            set { CaptionLbl.Text = value; }
        }
    }
}
