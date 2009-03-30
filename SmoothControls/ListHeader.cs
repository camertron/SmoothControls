using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    internal partial class ListHeader : UserControl
    {
        public static int CONTROL_HEIGHT = 18;
        private ListHeaderCollection pHeaders;

        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private Pen GradientPen;
        private Color SeparatorColor;
        private Pen SeparatorPen;
        private Color BorderColor;
        private Pen BorderPen;
        private SolidBrush TextBrush;
        private Font pFont;

        public delegate void ChangeHandler(object sender, int Index);
        public event ChangeHandler Changed;

        public ListHeader()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            pHeaders = new ListHeaderCollection();

            this.Paint += new PaintEventHandler(ListViewHeader_Paint);
            this.Resize += new EventHandler(ListViewHeader_Resize);

            SeparatorColor = Color.FromArgb(100, 100, 100);
            SeparatorPen = new Pen(SeparatorColor);

            GradientPen = new Pen(Color.Black);
            GradientStart = Color.FromArgb(210, 210, 210);
            GradientFinish = Color.FromArgb(239, 239, 239);
            UpdateGradients();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            TextBrush = new SolidBrush(Color.Black);

            HookUpHeaders();

            pFont = new Font("Arial", 9);
        }

        public override Font Font
        {
            get { return pFont; }
            set
            {
                pFont = value;
                MeasureLbl.Font = pFont;
                InvalidateParent();
            }
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void HookUpHeaders()
        {
            pHeaders.EntriesCleared += new ListHeaderCollection.EntryChangedHandler(pHeaders_EntryRearranged);
            pHeaders.EntryAdded += new ListHeaderCollection.EntryChangedHandler(pHeaders_EntryRearranged);
            pHeaders.EntryChanged += new ListHeaderCollection.EntryChangedHandler(pHeaders_EntryChanged);
            pHeaders.EntryRemoved += new ListHeaderCollection.EntryChangedHandler(pHeaders_EntryRearranged);
        }

        private void pHeaders_EntryRearranged(object sender, int Index)
        {
            this.Invalidate();
        }

        private void pHeaders_EntryChanged(object sender, int Index)
        {
            if (Changed != null)
                Changed(this, Index);

            this.Invalidate();
        }

        private void UpdateGradients()
        {
            GradientColors = WildMouse.Graphics.Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void ListViewHeader_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            UpdateGradients();
        }

        private void ListViewHeader_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < this.Height; i++)
            {
                GradientPen.Color = GradientColors[i];
                e.Graphics.DrawLine(GradientPen, 0, i, this.Width, i);
            }

            int X = 2;

            for (int i = 0; i < pHeaders.Count; i ++)
            {
                MeasureLbl.Text = pHeaders[i].Text;
                e.Graphics.DrawString(pHeaders[i].Text, pFont, TextBrush, X + ((pHeaders[i].Width / 2) - (MeasureLbl.Width / 2)), 2);
                X += pHeaders[i].Width + 1;

                e.Graphics.DrawLine(SeparatorPen, X, 0, X, this.Height);
            }

            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        public ListHeaderCollection Headers
        {
            get { return pHeaders; }
            set
            {
                pHeaders = value;
                HookUpHeaders();
                this.Invalidate();
            }
        }
    }
}
