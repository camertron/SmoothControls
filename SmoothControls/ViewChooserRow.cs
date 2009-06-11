using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using System.Collections;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("Click")]
    public partial class ViewChooserRow : UserControl
    {
        public event System.EventHandler CtrlClick;

        private const int CONTROL_HEIGHT = 22;

        private Color GradientStart;
        private Color GradientFinish;
        private Color SelectedGradientStart;
        private Color SelectedGradientFinish;
        private Color NotSelectedGradientStart;
        private Color NotSelectedGradientFinish;
        private Color HoverGradientStart;
        private Color HoverGradientFinish;

        private Color BorderColor;
        private Pen BorderPen;

        private bool MouseIsOver;
        private bool pSelected;

        public ViewChooserRow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            SelectedGradientStart = Color.FromArgb(230, 230, 230);
            SelectedGradientFinish = Color.FromArgb(180, 180, 180);

            HoverGradientStart = Color.FromArgb(220, 220, 220);
            HoverGradientFinish = Color.FromArgb(220, 220, 220);
            //HoverGradientStart = Color.FromArgb(240, 240, 240);
            //HoverGradientFinish = Color.FromArgb(200, 200, 200);

            NotSelectedGradientStart = this.BackColor;
            NotSelectedGradientFinish = this.BackColor;

            this.Paint += new PaintEventHandler(ViewChooserRow_Paint);
            this.Resize += new EventHandler(ViewChooserRow_Resize);
            this.MouseEnter += new EventHandler(ViewChooserRow_MouseEnter);
            this.MouseLeave += new EventHandler(ViewChooserRow_MouseLeave);
            this.Click += new EventHandler(ViewChooserRow_Click);
            this.BackColorChanged += new EventHandler(ViewChooserRow_BackColorChanged);

            TextLbl.MouseEnter += new EventHandler(ViewChooserRow_MouseEnter);
            TextLbl.MouseLeave += new EventHandler(ViewChooserRow_MouseLeave);
            TextLbl.Click += new EventHandler(ViewChooserRow_Click);

            MouseIsOver = false;
            pSelected = false;
            UpdateGradientColors();

            BorderColor = Color.Silver;
            BorderPen = new Pen(BorderColor);
        }

        private void ViewChooserRow_Click(object sender, EventArgs e)
        {
            if (CtrlClick != null)
                CtrlClick(this, e);
        }

        private void ViewChooserRow_BackColorChanged(object sender, EventArgs e)
        {
            NotSelectedGradientStart = this.BackColor;
            NotSelectedGradientFinish = this.BackColor;
        }

        private void UpdateGradientColors()
        {
            if (MouseIsOver)
            {
                if (pSelected)
                {
                    GradientStart = SelectedGradientStart;
                    GradientFinish = SelectedGradientFinish;
                }
                else
                {
                    GradientStart = HoverGradientStart;
                    GradientFinish = HoverGradientFinish;
                }
            }
            else
            {
                if (pSelected)
                {
                    GradientStart = SelectedGradientStart;
                    GradientFinish = SelectedGradientFinish;
                }
                else
                {
                    GradientStart = NotSelectedGradientStart;
                    GradientFinish = NotSelectedGradientFinish;
                }
            }
        }

        private void ViewChooserRow_MouseLeave(object sender, EventArgs e)
        {
            MouseIsOver = false;
            UpdateGradientColors();
            this.Invalidate();
        }

        private void ViewChooserRow_MouseEnter(object sender, EventArgs e)
        {
            MouseIsOver = true;
            UpdateGradientColors();
            this.Invalidate();
        }

        private void ViewChooserRow_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            TextLbl.Width = this.Width - 10;
            TextLbl.Top = (this.Height / 2) - (TextLbl.Height / 2);
            TextLbl.Left = (this.Width / 2) - (TextLbl.Width / 2);
        }

        private void ViewChooserRow_Paint(object sender, PaintEventArgs e)
        {
            WildMouse.Graphics.Gradient.DrawGradient(e.Graphics, Gradient.GradientDirection.Vertical, GradientStart, GradientFinish, this.Width, this.Height);

            e.Graphics.DrawLine(BorderPen, 0, 0, this.Width, 0);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Selected
        {
            get { return pSelected; }
            set { pSelected = value; UpdateGradientColors(); this.Invalidate(); }
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
    }

    public class ViewChooserRowCollection : ArrayList
    {
        public void Add(ViewChooserRow NewItem)
        {
            base.Add(NewItem);
        }

        public new ViewChooserRow this[int Index]
        {
            get { return (ViewChooserRow)base[Index]; }
            set { base[Index] = value; }
        }
    }
}
