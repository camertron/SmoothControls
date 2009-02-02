using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using WildMouse.Graphics;
using System.Drawing.Text;

namespace WildMouse.SmoothControls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [DefaultEvent("TabClicked")]
    public partial class TabPanel : UserControl
    {
        private StringCollection pTabs;
        private Color TabBorderColor;
        private Pen TabBorderPen;
        private Color BorderColor;
        private Pen BorderPen;
        private Color GradientColor;
        private Pen GradientPen;
        private Color TextColor;
        private SolidBrush TextBrush;
        private Font pFont;
        private int pFontSize;
        private Color BodyFillColor;
        private SolidBrush BodyFillBrush;
        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private PrivateFontCollection pfc;

        private float TabDiameter;
        private float TabRadius;
        private int pTabHeight;

        //private const int TAB_HEIGHT = 15;
        private const int PADDING = 5;

        private int pSelectedTab;

        public delegate void TabClickedHandler(object sender, int Index);
        public event TabClickedHandler TabClicked;

        public TabPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
            
            this.Paint += new PaintEventHandler(TabPanel_Paint);
            this.MouseUp += new MouseEventHandler(TabPanel_MouseUp);
            this.Resize += new EventHandler(TabPanel_Resize);

            pTabHeight = 25;
            pTabs = new StringCollection();

            TabBorderColor = Color.FromArgb(200, 200, 200);
            TabBorderPen = new Pen(TabBorderColor);

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            GradientColor = Color.Black;
            GradientPen = new Pen(GradientColor);

            TextColor = Color.Black;
            TextBrush = new SolidBrush(TextColor);

            TabDiameter = 10.0f;
            TabRadius = 5.0f;

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;
            pFont = new Font(pfc.Families[0], pFontSize);
            MeasureLbl.Font = pFont;

            BodyFillColor = Color.White;
            BodyFillBrush = new SolidBrush(BodyFillColor);

            GradientStart = Color.FromArgb(240, 240, 240);
            GradientFinish = BodyFillColor;

            UpdateGradients();

            pSelectedTab = 1;
        }

        private void TabPanel_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public int TabHeight
        {
            get { return pTabHeight; }
            set { pTabHeight = value; this.Invalidate(); }
        }

        private void TabPanel_MouseUp(object sender, MouseEventArgs e)
        {
            float X = TabDiameter + 1.0f;
            int PanelKey;

            if (e.Y <= pTabHeight)
            {
                for (int i = 0; i < pTabs.Count; i++)
                {
                    MeasureLbl.Text = pTabs[i];

                    if ((e.X > X) && (e.X < (X + MeasureLbl.Width + TabDiameter + (PADDING * 2))))
                    {
                        SelectedTab = i;
                        this.Invalidate();

                        if (TabClicked != null)
                            TabClicked(this, i);

                        PanelKey = this.Controls.IndexOfKey(pTabs[i].Replace(" ", "") + "Panel");

                        if (PanelKey > -1)
                            this.Controls[PanelKey].BringToFront();

                        return;
                    }

                    X += MeasureLbl.Width + TabDiameter + (PADDING * 2);
                }
            }
        }

        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection Tabs
        {
            get { return pTabs; }
            set
            {
                pTabs = value;
                this.Invalidate();
            }
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, pTabHeight + (int)TabRadius + 1);
        }

        public int SelectedTab
        {
            get { return pSelectedTab; }
            set
            {
                pSelectedTab = value;
                this.Invalidate();
            }
        }

        private void TabPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            float X = TabDiameter + 1;
            float RadiusSquared = (float)Math.Pow(TabRadius, 2);
            float ChordWidth;

            //fill background color
            e.Graphics.FillEllipse(BodyFillBrush, 0, pTabHeight + TabRadius, TabDiameter, TabDiameter);  //top left
            e.Graphics.FillEllipse(BodyFillBrush, this.Width - (TabDiameter + 1), pTabHeight + TabRadius, TabDiameter, TabDiameter);  //top right
            e.Graphics.FillEllipse(BodyFillBrush, 0, this.Height - (TabDiameter + 1), TabDiameter, TabDiameter);  // bottom left
            e.Graphics.FillEllipse(BodyFillBrush, this.Width - (TabDiameter + 1), this.Height - (TabDiameter + 1), TabDiameter, TabDiameter);  //bottom right

            e.Graphics.FillRectangle(BodyFillBrush, TabRadius, pTabHeight + TabRadius, this.Width - TabDiameter, this.Height - 1);
            e.Graphics.FillRectangle(BodyFillBrush, 0, pTabHeight + TabDiameter + 1, this.Width - 1, this.Height - (pTabHeight + TabDiameter + TabRadius + 1));

            for (int i = 0; i < pTabs.Count; i++)
            {
                MeasureLbl.Text = pTabs[i];

                e.Graphics.FillEllipse(BodyFillBrush, X, 0, TabDiameter, TabDiameter);
                e.Graphics.FillEllipse(BodyFillBrush, X + MeasureLbl.Width + (PADDING * 2), 0, TabDiameter, TabDiameter);
                e.Graphics.FillRectangle(BodyFillBrush, X + TabRadius, 0, MeasureLbl.Width + (PADDING * 2), pTabHeight + TabRadius);
                e.Graphics.FillRectangle(BodyFillBrush, X, TabRadius, MeasureLbl.Width + TabDiameter + (PADDING * 2), pTabHeight);

                X += MeasureLbl.Width + TabDiameter + (PADDING * 2);
            }

            X = TabDiameter + 1;

            for (int i = 0; i < pTabs.Count; i ++)
            {
                MeasureLbl.Text = pTabs[i];

                if (i == pSelectedTab)
                {
                    //draw gradient
                    for (double g = 0; g < pTabHeight + TabRadius + 1; g ++)
                    {
                        //GradientPen.Color = Color.White;
                        if (g <= TabRadius)
                            ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(TabRadius - g, 2));
                        else if (((pTabHeight + TabRadius) - g) <= TabRadius)
                        {
                            ChordWidth = (float)Math.Sqrt(RadiusSquared + Math.Pow(g - pTabHeight, 2));
                            //GradientPen.Color = Color.Black;
                        }
                        else
                            ChordWidth = TabRadius;

                        GradientPen.Color = GradientColors[(int)g];
                        e.Graphics.DrawLine(GradientPen, X + (TabRadius - ChordWidth) + 1, (float)g, X + ((MeasureLbl.Width + TabDiameter + (PADDING * 2)) - ((TabRadius - ChordWidth) + 1)), (float)g);
                    }

                    e.Graphics.DrawArc(TabBorderPen, X - TabDiameter, pTabHeight - TabRadius, TabDiameter, TabDiameter, 0, 90);  //bottom left
                    e.Graphics.DrawArc(TabBorderPen, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), pTabHeight - TabRadius, TabDiameter, TabDiameter, 180, -90);  //bottom right

                    e.Graphics.DrawLine(TabBorderPen, PADDING, pTabHeight + TabRadius, X - TabRadius, pTabHeight + TabRadius);  //bottom left
                    e.Graphics.DrawLine(TabBorderPen, X + MeasureLbl.Width + TabDiameter + (PADDING * 2) + TabRadius, pTabHeight + TabRadius, this.Width - TabRadius, pTabHeight + TabRadius);  //bottom right

                    e.Graphics.DrawLine(TabBorderPen, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), TabRadius, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), pTabHeight);  //right

                    if (i == 0)
                        e.Graphics.DrawLine(TabBorderPen, X, TabRadius, X, pTabHeight);  //left
                }

                //draw borders and such
                e.Graphics.DrawArc(TabBorderPen, X, 0, TabDiameter, TabDiameter, -90, -90);  //top left
                e.Graphics.DrawArc(TabBorderPen, X + MeasureLbl.Width + (PADDING * 2), 0, TabDiameter, TabDiameter, -90, 90);  //top right

                e.Graphics.DrawLine(TabBorderPen, X + TabRadius, 0, X + MeasureLbl.Width + TabRadius + (PADDING * 2), 0);  //top

                //e.Graphics.DrawString(pTabs[i], pFont, TextBrush, (X + 3) + ((((PADDING * 2) + TabDiameter + MeasureLbl.Width) / 2) - (MeasureLbl.Width / 2)), 6);
                e.Graphics.DrawString(pTabs[i], pFont, TextBrush, (X + 3) + ((((PADDING * 2) + TabDiameter + MeasureLbl.Width) / 2) - (MeasureLbl.Width / 2)), (pTabHeight / 2) - ((MeasureLbl.Height - 6) / 2));

                if ((i == 0) && (i != pSelectedTab))
                    e.Graphics.DrawLine(TabBorderPen, X, TabRadius, X, pTabHeight + TabRadius);  //left

                if ((i + 1) == pSelectedTab)
                    e.Graphics.DrawLine(TabBorderPen, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), TabRadius, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), pTabHeight);  //right
                else if (i != pSelectedTab)
                    e.Graphics.DrawLine(TabBorderPen, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), TabRadius, X + MeasureLbl.Width + TabDiameter + (PADDING * 2), pTabHeight + TabRadius);  //right

                e.Graphics.DrawArc(TabBorderPen, 0, pTabHeight + TabRadius, TabDiameter, TabDiameter, -90, -90);  //top left
                e.Graphics.DrawArc(TabBorderPen, this.Width - (TabDiameter + 1), pTabHeight + TabRadius, TabDiameter, TabDiameter, -90, 90);  //top right
                e.Graphics.DrawArc(TabBorderPen, 0, this.Height - (TabDiameter + 1), TabDiameter, TabDiameter, 180, -90);  //bottom left
                e.Graphics.DrawArc(TabBorderPen, this.Width - (TabDiameter + 1), this.Height - (TabDiameter + 1), TabDiameter, TabDiameter, 0, 90);  //bottom right

                e.Graphics.DrawLine(TabBorderPen, 0, pTabHeight + TabDiameter, 0, this.Height - (TabRadius + 1));  //left
                e.Graphics.DrawLine(TabBorderPen, this.Width - 1, pTabHeight + TabDiameter, this.Width - 1, this.Height - (TabRadius + 1));  //right
                e.Graphics.DrawLine(TabBorderPen, TabRadius, this.Height - 1, this.Width - (TabRadius + 1), this.Height - 1);  //bottom

                X += MeasureLbl.Width + TabDiameter + (PADDING * 2);
            }
        }
    }
}
