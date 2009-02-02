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

        private ComboPopout Popout;

        public event System.EventHandler SelectedIndexChanged;

        public ComboBox()
        {
            InitializeComponent();

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

            Popout = new ComboPopout();
            Popout.SelectedIndexChanged += new EventHandler(Popout_SelectedIndexChanged);
            Popout.FinishedDisappearing += new EventHandler(Popout_FinishedDisappearing);

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;

            pFont = new Font(pfc.Families[0], pFontSize);
            MeasureLbl.Font = pFont;

            TextColor = Color.Black;
            TextBrush = new SolidBrush(TextColor);

            this.MouseUp += new MouseEventHandler(ComboBox_MouseUp);

            UpdateRegion();
        }

        public int SelectedIndex
        {
            get { return Popout.SelectedIndex; }
            set
            {
                Popout.SelectedIndex = value;
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

        private void Popout_FinishedDisappearing(object sender, EventArgs e)
        {
            this.Focus();
            Popout.Hide();
        }

        private void Popout_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invalidate();

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection Items
        {
            get { return Popout.Items; }
            set { Popout.Items = value; }
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

            if (Popout.SelectedIndex != -1)
                e.Graphics.DrawString(PrintString, pFont, TextBrush, PADDING, 1);
        }

        private void PopoutBtn_Click(object sender, EventArgs e)
        {
            ShowPopout();
        }

        private void ShowPopout()
        {
            if (Popout.Items.Count == 0)
                return;

            int MaxWidth = 0;

            for (int i = 0; i < Items.Count; i ++)
            {
                MeasureLbl.Text = Items[i];

                if (MeasureLbl.Width > MaxWidth)
                    MaxWidth = MeasureLbl.Width;
            }

            if (this.Width > MaxWidth)
                MaxWidth = this.Width;

            Popout.Opacity = 0;
            Popout.Show();
            Popout.UpdateLayout();

            Point Location = this.PointToScreen(this.Location);

            if (Popout.SelectedIndex > -1)
                Location.Y -= (ComboPopoutItem.CONTROL_HEIGHT * Popout.SelectedIndex);

            Popout.Width = MaxWidth + 10;
            Popout.Top = Location.Y - this.Top;
            Popout.Left = (Location.X - this.Left) - ((MaxWidth - this.Width) + 16);

            Popout.Activate();
        }
        
        //transparency stuff
        /*
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do nothing
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //turn the form transparent - sweet, eh?
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
        */
    }
}
