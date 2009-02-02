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
    internal partial class ComboPopoutItem : UserControl
    {
        private string pText;
        private bool pChecked;
        private Font pFont;
        private Color SelectedTextColor;
        private Color UnselectedTextColor;
        private SolidBrush TextBrush;
        private Color SelectedCheckColor;
        private Color UnselectedCheckColor;
        private Pen CheckPen;
        private bool pSelected;
        private Color[] SelectedGradientColors;
        private Color SelectedGradientStart;
        private Color SelectedGradientFinish;
        private Pen GradientPen;
        private Color SelectedBorderColor;
        private Pen SelectedBorderPen;

        public const int CONTROL_HEIGHT = 18;
        private const int TEXT_LEFT = 20;
        private const int CHECK_LEFT = 15;
        private const int TEXT_TOP = 2;
        private const int CHECK_PADDING = 5;

        public ComboPopoutItem()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            pText = "";
            pChecked = false;

            this.Paint += new PaintEventHandler(ComboPopoutItem_Paint);
            this.Resize += new EventHandler(ComboPopoutItem_Resize);

            pFont = new Font("Arial", 9);

            UnselectedTextColor = Color.Black;
            TextBrush = new SolidBrush(UnselectedTextColor);
            SelectedTextColor = Color.White;

            UnselectedCheckColor = Color.Black;
            CheckPen = new Pen(UnselectedCheckColor);
            CheckPen.Width = 2;
            SelectedCheckColor = Color.White;

            SelectedGradientStart = Color.FromArgb(81, 112, 246);
            SelectedGradientFinish = Color.FromArgb(26, 67, 243);
            GradientPen = new Pen(Color.Black);

            SelectedBorderColor = Color.FromArgb(14, 55, 231);
            SelectedBorderPen = new Pen(SelectedBorderColor);

            UpdateGradients();
        }

        private void UpdateGradients()
        {
            SelectedGradientColors = Gradient.ComputeGradient(SelectedGradientStart, SelectedGradientFinish, this.Height);
        }

        private void ComboPopoutItem_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            UpdateGradients();
        }

        private void ComboPopoutItem_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (pSelected)
            {
                TextBrush.Color = SelectedTextColor;
                CheckPen.Color = SelectedCheckColor;

                for (float i = 0; i < SelectedGradientColors.Length; i++)
                {
                    GradientPen.Color = SelectedGradientColors[(int)i];
                    e.Graphics.DrawLine(GradientPen, 0, i, this.Width - 1, i);
                }

                e.Graphics.DrawRectangle(SelectedBorderPen, 0, 0, this.Width - 1, this.Height - 1);
            }
            else
            {
                TextBrush.Color = UnselectedTextColor;
                CheckPen.Color = UnselectedCheckColor;
            }

            e.Graphics.DrawString(pText, pFont, TextBrush, TEXT_LEFT, TEXT_TOP);

            if (pChecked)
            {
                //draw checkmark
                e.Graphics.DrawLine(CheckPen, CHECK_PADDING, this.Height / 2, CHECK_LEFT / 2, this.Height - CHECK_PADDING);
                e.Graphics.DrawLine(CheckPen, CHECK_LEFT / 2, this.Height - CHECK_PADDING, CHECK_LEFT, CHECK_PADDING);
            }
        }

        public bool Selected
        {
            get { return pSelected; }
            set
            {
                pSelected = value;
                this.Invalidate();
            }
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

        public bool Checked
        {
            get { return pChecked; }
            set
            {
                pChecked = value;
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override Font Font
        {
            get { return pFont; }
            set
            {
                pFont = value;
                this.Invalidate();
            }
        }
    }
}
