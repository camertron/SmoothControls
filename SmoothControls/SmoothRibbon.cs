using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using System.ComponentModel.Design;
using System.Collections;
using GDIDB;

namespace WildMouse.SmoothControls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class SmoothRibbon : UserControl
    {
        private const int RIBBON_COLLAPSED_HEIGHT = 18;

        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private Pen GradientPen;
        private Color BorderColor;
        private Pen BorderPen;
        private Color DividerColor;
        private Pen DividerPen;
        private Color BottomFillColor;

        private float Diameter;
        private float Radius;
        private float DividerPadding;

        private bool pExpanded;
        private int pExpandedHeight;

        public delegate void ExpandedEventHandler(object sender, bool Expanded);

        //events
        public event System.EventHandler CtrlDoubleClick;
        public event System.EventHandler BaseDoubleClick;  //when the bottom part of the control is double-clicked
        public event ExpandedEventHandler ExpandedChanged;

        private ArrayList pVisibleControls;

        public SmoothRibbon()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SmoothRibbon_Paint);
            this.Resize += new EventHandler(SmoothRibbon_Resize);

            BottomFillColor = Color.FromArgb(245, 245, 245);
            TextLbl.BackColor = BottomFillColor;

            DividerColor = Color.FromArgb(200, 200, 200);
            DividerPen = new Pen(DividerColor);
            DividerPen.Width = 1;

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            Diameter = 7.0f;
            Radius = 3.5f;
            DividerPadding = 18;

            GradientFinish = Color.FromArgb(223, 223, 223);
            GradientStart = Color.FromArgb(240, 240, 240);

            GradientPen = new Pen(Color.Black);

            UpdateGradients();

            TextLbl.DoubleClick += new EventHandler(TextLbl_DoubleClick);
            this.DoubleClick += new EventHandler(SmoothRibbon_DoubleClick);

            pExpanded = true;
            pExpandedHeight = this.Height;
            pVisibleControls = new ArrayList();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Expanded
        {
            get { return pExpanded; }
            set
            {
                //don't allow the property to be set with the same value (true to true, false to false)
                //it could mess things up!
                if (value == pExpanded)
                    return;

                pExpanded = value;

                if (pExpanded)
                {
                    for (int i = 0; i < pVisibleControls.Count; i++)
                        ((Control)pVisibleControls[i]).Visible = true;

                    pVisibleControls.Clear();
                    this.Height = pExpandedHeight;
                }
                else
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i].Name != "TextLbl")
                        {
                            if (this.Controls[i].Visible)
                            {
                                pVisibleControls.Add(this.Controls[i]);
                                this.Controls[i].Visible = false;
                            }
                        }
                    }

                    pExpandedHeight = this.Height;
                }

                SmoothRibbon_Resize(this, EventArgs.Empty);
                this.Invalidate();

                if (ExpandedChanged != null)
                    ExpandedChanged(this, pExpanded);
            }
        }

        private void SmoothRibbon_DoubleClick(object sender, EventArgs e)
        {
            if (CtrlDoubleClick != null)
                CtrlDoubleClick(this, EventArgs.Empty);
        }

        private void TextLbl_DoubleClick(object sender, EventArgs e)
        {
            if (CtrlDoubleClick != null)
                CtrlDoubleClick(this, EventArgs.Empty);

            if (BaseDoubleClick != null)
                BaseDoubleClick(this, EventArgs.Empty);

            this.Expanded = ! this.Expanded;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return TextLbl.Text; }
            set { TextLbl.Text = value; }
        }

        private void SmoothRibbon_Resize(object sender, EventArgs e)
        {
            if (!pExpanded)
                this.Height = RIBBON_COLLAPSED_HEIGHT;

            UpdateGradients();

            TextLbl.Top = this.Height - ((int)DividerPadding - 1);
            TextLbl.Left = 3;
            TextLbl.Width = this.Width - 6;
        }

        private void UpdateGradients()
        {
            GradientColors = WildMouse.Graphics.Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void SmoothRibbon_Paint(object sender, PaintEventArgs e)
        {
            //draw gradient
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            for (double i = 0; i < this.Height; i++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                if (i > (this.Height - DividerPadding))
                    GradientPen.Color = BottomFillColor;
                else
                    GradientPen.Color = GradientColors[(int)i];

                e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            //rounded corners
            e.Graphics.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //top right
            e.Graphics.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //bottom left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //bottom right

            //border lines
            e.Graphics.DrawLine(BorderPen, Radius, 0, this.Width - Radius, 0);  //top
            e.Graphics.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);  //bottom
            e.Graphics.DrawLine(BorderPen, 0, Radius, 0, this.Height - (Radius + 1));  //left
            e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));

            //draw divider line
            e.Graphics.DrawLine(DividerPen, 1, this.Height - DividerPadding, this.Width - 2, this.Height - DividerPadding);
        }
    }
}
