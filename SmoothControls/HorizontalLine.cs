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
    public partial class HorizontalLine : UserControl
    {
        private int pThickness;
        private Color pLineColor;
        private SolidBrush LineBrush;

        public HorizontalLine()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(HorizontalLine_Paint);
            this.Resize += new EventHandler(HorizontalLine_Resize);

            LineBrush = new SolidBrush(Color.Black);

            pThickness = 1;
        }

        private void HorizontalLine_Resize(object sender, EventArgs e)
        {
            this.Height = pThickness + 1;
        }

        private void HorizontalLine_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(LineBrush, 0, 0, this.Width - 1, this.Height - 1);
        }

        public int Thickness
        {
            get { return pThickness; }
            set
            {
                pThickness = value;
                this.HorizontalLine_Resize(this, EventArgs.Empty);
            }
        }

        public Color LineColor
        {
            get { return pLineColor; }
            set
            {
                pLineColor = value;
                LineBrush.Color = pLineColor;
                this.Invalidate();
            }
        }
    }
}
