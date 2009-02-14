using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GDIDB;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("Click")]
    public partial class RibbonButton : UserControl
    {
        private RibbonButtonType pButtonType;
        private Bitmap pImage;
        private Bitmap pDarkImage;
        private Bitmap pDisabledImage;
        private Pen BorderPen;
        private Color BorderColor;
        private Color BackgroundColor;
        private SolidBrush BackgroundBrush;
        private bool bEnabled;

        private float Diameter;
        private float Radius;

        private bool IsMouseOver;
        private bool IsMouseDown;
        private Keys kShortcutKey;

        //these must be differently named or the background won't appear when hovering
        public event MouseEventHandler CtrlMouseMove;
        public event System.EventHandler CtrlMouseEnter;
        public event System.EventHandler CtrlMouseLeave;
        public event System.EventHandler CtrlClick;

        private const int SMALL_IMAGE_MAX_WIDTH = 30;
        private const int SMALL_IMAGE_PADDING = 5;

        public enum RibbonButtonType
        {
            Large = 1,
            Small = 2
        }

        public RibbonButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(RibbonButton_Paint);
            this.Resize += new EventHandler(RibbonButton_Resize);

            Diameter = 6.0f;
            Radius = 3.0f;

            this.ButtonType = RibbonButtonType.Large;

            BorderColor = Color.Gray;
            BorderPen = new Pen(BorderColor);

            UpdateRegion();

            IsMouseOver = false;
            IsMouseDown = false;
            this.MouseMove += new MouseEventHandler(RibbonButton_MouseMove);
            this.MouseLeave += new EventHandler(RibbonButton_MouseLeave);
            this.MouseDown += new MouseEventHandler(RibbonButton_MouseDown);
            this.MouseUp += new MouseEventHandler(RibbonButton_MouseUp);
            this.Click += new EventHandler(RibbonButton_Click);
            CaptionLbl.MouseMove += new MouseEventHandler(CaptionLbl_MouseMove);
            CaptionLbl.MouseLeave += new EventHandler(CaptionLbl_MouseLeave);
            CaptionLbl.Click += new EventHandler(RibbonButton_Click);
            CaptionLbl.MouseDown += new MouseEventHandler(RibbonButton_MouseDown);
            CaptionLbl.MouseUp += new MouseEventHandler(RibbonButton_MouseUp);

            BackgroundColor = Color.FromArgb(220, 220, 220);
            BackgroundBrush = new SolidBrush(BackgroundColor);

            pImage = null;
            bEnabled = false;
        }

        /// <summary>
        /// Clicks on this control if the given key matches this button's shortcut key.
        /// </summary>
        /// <param name="kKey">The key that was pressed.</param>
        public void ReceiveKeys(Keys kKey)
        {
            if (kKey == kShortcutKey)
            {
                if (CtrlClick != null)
                    CtrlClick(this, EventArgs.Empty);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public Keys ShortcutKey
        {
            get { return kShortcutKey; }
            set { kShortcutKey = value; }
        }

        //overrides inherited member Enabled
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool ButtonEnabled
        {
            get { return bEnabled; }
            set
            {
                bEnabled = value;
                this.Invalidate();
            }
        }

        private Bitmap LightenImage(Bitmap OriginalImage)
        {
            return RecolorImage(OriginalImage, -30);
        }

        private Bitmap DarkenImage(Bitmap OriginalImage)
        {
            return RecolorImage(OriginalImage, 30);
        }

        private Bitmap RecolorImage(Bitmap OriginalImage, int iAdjustment)
        {
            Color CurColor;
            int iR = 0, iG = 0, iB = 0;
            Bitmap NewImage = new Bitmap(OriginalImage.Width, OriginalImage.Height);

            for (int c = 0; c < OriginalImage.Width; c++)
            {
                for (int r = 0; r < OriginalImage.Height; r++)
                {
                    CurColor = OriginalImage.GetPixel(c, r);

                    iR = FixAdjustment(iAdjustment, CurColor.R);
                    iG = FixAdjustment(iAdjustment, CurColor.G);
                    iB = FixAdjustment(iAdjustment, CurColor.B);

                    NewImage.SetPixel(c, r, Color.FromArgb(CurColor.A, iR, iG, iB));
                }
            }

            return NewImage;
        }

        private Bitmap FadeImage(Bitmap OriginalImage)
        {
            Bitmap NewImage = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            Color CurColor;

            for (int c = 0; c < OriginalImage.Width; c++)
            {
                for (int r = 0; r < OriginalImage.Height; r++)
                {
                    CurColor = OriginalImage.GetPixel(c, r);
                    NewImage.SetPixel(c, r, Color.FromArgb(FixAdjustment(100, CurColor.A), CurColor.R, CurColor.G, CurColor.B));
                }
            }

            return NewImage;
        }

        private int FixAdjustment(int iAdj, int iColorValue)
        {
            int CurAdj = iColorValue - iAdj;

            if (CurAdj >= 0)
            {
                if (CurAdj > 255)
                    return 255;
                else
                    return CurAdj;
            }
            else
                return 0;
        }

        private void RibbonButton_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            this.Invalidate();
        }

        private void RibbonButton_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            this.Invalidate();
        }

        private void CaptionLbl_MouseLeave(object sender, EventArgs e)
        {
            IsMouseOver = false;
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

        private void RibbonButton_Click(object sender, EventArgs e)
        {
            if (CtrlClick != null)
                CtrlClick(this, EventArgs.Empty);
        }

        private void CaptionLbl_MouseMove(object sender, MouseEventArgs e)
        {
            IsMouseOver = true;
        }

        private void RibbonButton_MouseLeave(object sender, EventArgs e)
        {
            IsMouseOver = false;
            this.Invalidate();

            if (CtrlMouseLeave != null)
                CtrlMouseLeave(this, EventArgs.Empty);
        }

        private void RibbonButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (! IsMouseOver)
            {
                IsMouseOver = true;
                this.Invalidate();

                if (CtrlMouseEnter != null)
                    CtrlMouseEnter(this, EventArgs.Empty);
            }
            
            if (CtrlMouseMove != null)
                CtrlMouseMove(this, e);
        }

        private void RibbonButton_Resize(object sender, EventArgs e)
        {
            switch (pButtonType)
            {
                case RibbonButtonType.Large:
                    CaptionLbl.Top = this.Height - (int)(CaptionLbl.Height + Radius);
                    CaptionLbl.Width = this.Width - (int)(Radius * 2);
                    break;

                case RibbonButtonType.Small:
                    CaptionLbl.Top = (this.Height / 2) - (CaptionLbl.Height / 2);
                    CaptionLbl.Width = this.Width - (SMALL_IMAGE_MAX_WIDTH + (int)(Radius * 2));
                    CaptionLbl.Left = SMALL_IMAGE_MAX_WIDTH + (int)Radius;
                    break;
            }

            UpdateRegion();
        }

        private void RibbonButton_Paint(object sender, PaintEventArgs e)
        {
            if (IsMouseOver)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //fill background
                e.Graphics.FillEllipse(BackgroundBrush, 1, 1, Diameter, Diameter);
                e.Graphics.FillEllipse(BackgroundBrush, 1, this.Height - (Diameter + 1), Diameter, Diameter);
                e.Graphics.FillEllipse(BackgroundBrush, this.Width - (Diameter + 1), 1, Diameter, Diameter);
                e.Graphics.FillEllipse(BackgroundBrush, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter);
                e.Graphics.FillRectangle(BackgroundBrush, 1, Radius, this.Width - 2, this.Height - Diameter);
                e.Graphics.FillRectangle(BackgroundBrush, Radius, 1, this.Width - Diameter, this.Height - 2);

                //rounded corners and border lines
                e.Graphics.DrawArc(BorderPen, 1, 1, Diameter, Diameter, -90, -90);  //top left
                e.Graphics.DrawLine(BorderPen, Radius, 1, this.Width - (Radius + 2), 1);  //top
                e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), 1, Diameter, Diameter, -90, 90);  //top right
                e.Graphics.DrawLine(BorderPen, this.Width - 1, Radius, this.Width - 1, this.Height - (Radius + 1)); //right
                e.Graphics.DrawArc(BorderPen, this.Width - (Diameter + 1), this.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //bottom right
                e.Graphics.DrawLine(BorderPen, Radius, this.Height - 1, this.Width - Radius, this.Height - 1);  //bottom
                e.Graphics.DrawArc(BorderPen, 1, this.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //bottom left
                e.Graphics.DrawLine(BorderPen, 1, Radius, 1, this.Height - (Radius + 1));  //left
            }

            Image UseImage;

            //determine image to use
            if (! bEnabled)
                UseImage = (Image)pDisabledImage;
            if (IsMouseDown)
                UseImage = (Image)pDarkImage;
            else
                UseImage = (Image)pImage;

            if (UseImage != null)
            {
                //determine positioning
                Point ImagePos = new Point();

                switch (pButtonType)
                {
                    case RibbonButtonType.Large:
                        ImagePos.X = (this.Width / 2) - (pImage.Width / 2);
                        ImagePos.Y = ((this.Height - CaptionLbl.Height) / 2) - (pImage.Height / 2);
                        break;

                    case RibbonButtonType.Small:
                        ImagePos.X = SMALL_IMAGE_PADDING;
                        ImagePos.Y = (this.Height / 2) - (UseImage.Height / 2);
                        break;
                }

                e.Graphics.DrawImage(UseImage, ImagePos);
            }
        }

        public RibbonButtonType ButtonType
        {
            get { return pButtonType; }
            set
            {
                pButtonType = value;

                switch (pButtonType)
                {
                    case RibbonButtonType.Large:
                        CaptionLbl.TextAlign = ContentAlignment.MiddleCenter; break;
                    case RibbonButtonType.Small:
                        CaptionLbl.TextAlign = ContentAlignment.MiddleLeft; break;
                }

                RibbonButton_Resize(this, EventArgs.Empty);
                this.Invalidate();
            }
        }

        public Bitmap Image
        {
            get { return pImage; }
            set
            {
                pImage = value;
                pDarkImage = DarkenImage(pImage);
                pDisabledImage = FadeImage(pImage);
                this.Invalidate();
            }
        }

        private void UpdateRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();

            //rounded corners
            FormRegion.AddArc(0, 0, Diameter, Diameter, 180, 90);  //top left
            FormRegion.AddLine(Radius, 0, this.Width - Radius, 0);  //top
            FormRegion.AddArc(this.Width - Diameter, 0, Diameter, Diameter, -90, 90);  //top right
            FormRegion.AddLine(this.Width, Radius, this.Width, this.Height - Radius); //right
            FormRegion.AddArc(this.Width - Diameter, this.Height - Diameter, Diameter, Diameter, 360, 90);  //bottom right
            FormRegion.AddLine(Radius, this.Height, this.Width - Radius, this.Height);  //bottom
            FormRegion.AddArc(0, this.Height - Diameter, Diameter, Diameter, 90, 90);  //bottom left
            FormRegion.AddLine(0, Radius, 0, this.Height - Radius);  //left

            FormRegion.CloseAllFigures();

            this.Region = new System.Drawing.Region(FormRegion);
        }
    }
}
