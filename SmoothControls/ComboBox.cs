using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using System.Drawing.Text;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class ComboBox : UserControl
    {
        private Color BorderColor;
        private Pen BorderPen;
        private Color GradientStart;
        private Color GradientFinish;
        private Color[] GradientColors;
        private Pen GradientPen;
        private PrivateFontCollection pfc;
        private Font pFont;
        private int pFontSize;
        private Color TextColor;
        private SolidBrush TextBrush;

        private float Radius;
        private float Diameter;

        private const int CONTROL_HEIGHT = 20;
        private const int POPOUT_PADDING = 7;
        private const int PADDING = 10;

        private int m_iSelectedIndex;
        private StringCollectionWithEvents m_scItems;

        private List<ComboPopoutItem> m_clListControls;
        private ToolStripDropDown m_tsDropDown;
        private ToolStripControlHost m_tsHost;
        private Panel m_Panel;
        private ComboPopoutItem m_cpiLastSelected;

        public event System.EventHandler SelectedIndexChanged;

        public ComboBox()
        {
            InitializeComponent();

            m_cpiLastSelected = null;

            m_clListControls = new List<ComboPopoutItem>();

            m_Panel = new Panel();
            m_tsHost = new ToolStripControlHost(m_Panel);

            m_tsDropDown = new ToolStripDropDown();
            m_tsDropDown.AutoSize = false;
            m_tsDropDown.Items.Add(m_tsHost);

            m_scItems = new StringCollectionWithEvents();
            m_scItems.ItemAdded += new StringCollectionWithEvents.ItemAddedEventHandler(m_scItems_ItemAdded);
            m_scItems.ItemRemoved += new StringCollectionWithEvents.ItemRemovedEventHandler(m_scItems_ItemRemoved);
            m_iSelectedIndex = -1;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            this.Paint += new PaintEventHandler(ComboBox_Paint);
            this.Resize += new EventHandler(ComboBox_Resize);

            Radius = 5.0f;
            Diameter = 10.0f;

            GradientPen = new Pen(Color.Black);

            GradientStart = Color.FromArgb(252, 252, 252);
            GradientFinish = Color.FromArgb(223, 223, 223);
            UpdateGradients();

            UpdateRegion();

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;

            pFont = new Font(pfc.Families[0], pFontSize);
            MeasureLbl.Font = pFont;

            TextColor = Color.Black;
            TextBrush = new SolidBrush(TextColor);

            this.MouseUp += new MouseEventHandler(ComboBox_MouseUp);

            UpdateRegion();
        }

        private void m_scItems_ItemRemoved(object sender, int iIndex)
        {
            m_Panel.Controls.Remove(m_clListControls[iIndex]);
            m_clListControls.RemoveAt(iIndex);
        }

        private void m_scItems_ItemAdded(object sender, string sNewStr)
        {
            ComboPopoutItem cpiNewItem = new ComboPopoutItem();

            cpiNewItem.Text = sNewStr;

            cpiNewItem.Click += new EventHandler(ListItem_Click);
            cpiNewItem.MouseEnter += new EventHandler(ListItem_MouseEnter);

            m_clListControls.Add(cpiNewItem);
            m_Panel.Controls.Add(cpiNewItem);
        }

        private void ListItem_MouseEnter(object sender, EventArgs e)
        {
            ((ComboPopoutItem)sender).Selected = true;

            if (m_cpiLastSelected != null)
                m_cpiLastSelected.Selected = false;

            m_cpiLastSelected = (ComboPopoutItem)sender;
        }

        private void ListItem_Click(object sender, EventArgs e)
        {
            if (m_iSelectedIndex > -1)
                m_clListControls[m_iSelectedIndex].Checked = false;

            m_iSelectedIndex = m_clListControls.IndexOf((ComboPopoutItem)sender);

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);

            m_tsDropDown.Hide();
            m_cpiLastSelected = null;
            m_clListControls[m_iSelectedIndex].Checked = true;

            this.Invalidate();
        }

        public int SelectedIndex
        {
            get { return m_iSelectedIndex; }
            set
            {
                m_iSelectedIndex = value;
                this.Invalidate();

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void ComboBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X > (PopoutBtn.Left - POPOUT_PADDING))
                ShowPopout();
        }

        public StringCollectionWithEvents Items
        {
            get { return m_scItems; }
            set { m_scItems = value; }
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void UpdateRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();

            FormRegion.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void UpdateGradients()
        {
            GradientColors = Gradient.ComputeGradient(GradientStart, GradientFinish, this.Height);
        }

        private void ComboBox_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            PopoutBtn.Top = 1;
            PopoutBtn.Left = this.Width - (PopoutBtn.Width + POPOUT_PADDING);

            UpdateGradients();
            UpdateRegion();
        }

        private void ComboBox_Paint(object sender, PaintEventArgs e)
        {
            string PrintString = "";

            if (SelectedIndex > -1)
            {
                for (int i = 0; i < Items[SelectedIndex].Length; i++)
                {
                    MeasureLbl.Text = Items[SelectedIndex].Substring(0, i) + "...";

                    if (MeasureLbl.Width > (this.Width - ((PADDING * 2) + 5)))
                    {
                        PrintString = Items[SelectedIndex].Substring(0, i - 1) + "...";
                        break;
                    }
                }

                if (PrintString == "")
                    PrintString = Items[SelectedIndex];
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;

            for (double i = 0; i < this.Height; i++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                GradientPen.Color = GradientColors[(int)i];
                e.Graphics.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            e.Graphics.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //upper right
            e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //lower right
            e.Graphics.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //lower left

            e.Graphics.DrawLine(BorderPen, Radius + 1, 0, this.Width - (Radius + 2), 0);  //top
            e.Graphics.DrawLine(BorderPen, Radius + 1, this.Height - 1, this.Width - (Radius + 2), this.Height - 1);  //bottom
            e.Graphics.DrawLine(BorderPen, 0, Radius, 0, this.Height - Radius);  //left
            e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));  //right

            int SeparatorX = this.Width - (PopoutBtn.Width + (POPOUT_PADDING * 2));
            e.Graphics.DrawLine(BorderPen, SeparatorX, 0, SeparatorX, this.Height - 1);

            if (m_iSelectedIndex != -1)
                e.Graphics.DrawString(PrintString, pFont, TextBrush, PADDING, 1);
        }

        private void PopoutBtn_Click(object sender, EventArgs e)
        {
            ShowPopout();
        }

        private void ShowPopout()
        {
            if (m_scItems.Count == 0)
                return;

            int MaxWidth = 0;
            int iTop = 0;

            for (int i = 0; i < m_scItems.Count; i ++)
            {
                MeasureLbl.Text = m_scItems[i];

                if (MeasureLbl.Width > MaxWidth)
                    MaxWidth = MeasureLbl.Width;

                m_clListControls[i].Top = iTop;
                m_clListControls[i].Left = 2;

                iTop += ComboPopoutItem.CONTROL_HEIGHT;
            }

            if (this.Width > MaxWidth)
                MaxWidth = this.Width - 8;

            for (int i = 0; i < m_scItems.Count; i++)
                m_clListControls[i].Width = MaxWidth;

            Point Location = new Point(0, this.Height);

            if (m_iSelectedIndex > -1)
                Location.Y -= (ComboPopoutItem.CONTROL_HEIGHT * (m_iSelectedIndex + 1));

            m_tsDropDown.BackColor = Color.White;
            m_tsDropDown.Width = MaxWidth + 7;
            m_tsDropDown.Height = (ComboPopoutItem.CONTROL_HEIGHT * m_scItems.Count) + 7;

            m_Panel.Height = m_tsDropDown.Height;
            m_Panel.Width = m_tsDropDown.Width;

            m_tsDropDown.Show(this, Location, ToolStripDropDownDirection.BelowRight);
        }
    }
}
