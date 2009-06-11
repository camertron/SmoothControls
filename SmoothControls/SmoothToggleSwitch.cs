using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using GDIDB;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("Switched")]
    public partial class SmoothToggleSwitch : UserControl
    {
        private event System.EventHandler Switched;

        private const int CONTROL_HEIGHT = 22;
        private const int CONTROL_WIDTH = 65;

        private Pen BorderPen;
        private Color BorderColor;

        private Color UpperGradientStart;
        private Color UpperGradientFinish;
        private Color LowerGradientStart;
        private Color LowerGradientFinish;
        private Color[] UpperGradient;
        private Color[] LowerGradient;
        private Color[] CurGradient;
        private const int UPPER_GRADIENT_HEIGHT = 4;
        private Pen GradientPen;
        private Point HoverPos;

        private float Diameter;
        private float Radius;

        private DBGraphics memGraphics;
        private bool MoveLeft;
        private bool m_bIsOn;

        public SmoothToggleSwitch()
        {
            InitializeComponent();
            
            this.Resize += new EventHandler(SmoothToggleSwitch_Resize);
            this.Paint += new PaintEventHandler(SmoothToggleSwitch_Paint);
            Handle.MouseMove += new MouseEventHandler(Handle_MouseMove);
            Handle.MouseDown += new MouseEventHandler(Handle_MouseDown);
            Handle.MouseUp += new MouseEventHandler(Handle_MouseUp);

            BorderColor = Color.FromArgb(81, 81, 81);
            BorderPen = new Pen(BorderColor);

            UpperGradientStart = Color.FromArgb(111, 111, 111);
            UpperGradientFinish = Color.FromArgb(142, 142, 142);
            LowerGradientStart = Color.FromArgb(142, 142, 142);
            LowerGradientFinish = Color.FromArgb(162, 162, 162);
            GradientPen = new Pen(Color.Black);

            Diameter = 7.0f;
            Radius = 3.5f;

            ComputeGradients();

            memGraphics = new DBGraphics();
            CreateDoubleBuffer();

            m_bIsOn = false;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool IsOn
        {
            get { return m_bIsOn; }
            set
            {
                m_bIsOn = value;
                MoveLeft = ! m_bIsOn;
                AnimTmr.Enabled = true;

                if (Switched != null)
                    Switched(this, EventArgs.Empty);
            }
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void CreateDoubleBuffer()
        {
            memGraphics.CreateDoubleBuffer(this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height);
        }

        private void Handle_MouseUp(object sender, MouseEventArgs e)
        {
            MoveLeft = (((Handle.Width / 2) + Handle.Left) < (this.Width / 2));
            m_bIsOn = ! MoveLeft;
            AnimTmr.Enabled = true;

            if (Switched != null)
                Switched(this, EventArgs.Empty);
        }

        private void Handle_MouseDown(object sender, MouseEventArgs e)
        {
            AnimTmr.Enabled = false;
        }

        private void AnimTmr_Tick(object sender, EventArgs e)
        {
            int NewLeft;

            if (MoveLeft)
                NewLeft = Handle.Left - 5;
            else
                NewLeft = Handle.Left + 5;

            if (NewLeft >= (this.Width - Handle.Width))
            {
                NewLeft = (this.Width - Handle.Width);
                AnimTmr.Enabled = false;
            }

            if (NewLeft <= 0)
            {
                NewLeft = 0;
                AnimTmr.Enabled = false;
            }

            Handle.Left = NewLeft;
        }

        private void Handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point CursorPos = this.PointToClient(Cursor.Position);
                int NewX = CursorPos.X - HoverPos.X;

                if (NewX <= (this.Width - Handle.Width))
                {
                    if (NewX > 0)
                        Handle.Left = NewX;
                    else
                        Handle.Left = 0;
                }
                else
                    Handle.Left = this.Width - Handle.Width;
            }
            else
                HoverPos = e.Location;
        }

        private void SmoothToggleSwitch_Paint(object sender, PaintEventArgs e)
        {
            float RadiusSquared = (float)Math.Pow(Radius, 2);
            float ChordWidth;
            float Height = (float)this.Height - 1;
            int Offset = 0;

            memGraphics.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            CurGradient = UpperGradient;

            for (double i = 0; i < this.Height; i++)
            {
                if (i <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(Radius - i, 2));
                else if ((Height - i) <= Radius)
                    ChordWidth = (float)Math.Sqrt(RadiusSquared - Math.Pow(i - (Height - Radius), 2));
                else
                    ChordWidth = Radius;

                if (i >= UPPER_GRADIENT_HEIGHT)
                {
                    CurGradient = LowerGradient;
                    Offset = UPPER_GRADIENT_HEIGHT;
                }

                GradientPen.Color = CurGradient[(int)i - Offset];
                memGraphics.g.DrawLine(GradientPen, Radius - ChordWidth, (float)i, this.Width - ((Radius - ChordWidth) + 1), (float)i);
            }

            //rounded corners
            memGraphics.g.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            memGraphics.g.DrawArc(BorderPen, this.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //top right
            memGraphics.g.DrawArc(BorderPen, 0, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //bottom left
            memGraphics.g.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //bottom right

            //border lines
            memGraphics.g.DrawLine(BorderPen, Radius, 0, this.Width - Radius, 0);  //top
            memGraphics.g.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);  //bottom
            memGraphics.g.DrawLine(BorderPen, 0, Radius, 0, this.Height - (Radius + 1));  //left
            memGraphics.g.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1));

            memGraphics.Render(e.Graphics);
            CreateDoubleBuffer();
        }

        private void ComputeGradients()
        {
            UpperGradient = Gradient.ComputeGradient(UpperGradientStart, UpperGradientFinish, UPPER_GRADIENT_HEIGHT);
            LowerGradient = Gradient.ComputeGradient(LowerGradientStart, LowerGradientFinish, this.Height - UPPER_GRADIENT_HEIGHT);
        }

        private void SmoothToggleSwitch_Resize(object sender, EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
            this.Width = CONTROL_WIDTH;

            ComputeGradients();
        }

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
    }
}
