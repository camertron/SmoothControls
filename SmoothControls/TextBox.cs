using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("TextChanged")]
    public partial class TextBox : UserControl
    {
        private PrivateFontCollection pfc;
        private Color StartColor;
        private Color FinishColor;
        private Color[] BorderColors;
        private Pen BorderPen;
        private int pFontSize;
        private Font pFont;
        private int ColorCounter;

        private const int PADDING = 3;
        private const int GRADIENT_DISTANCE = 25;

        public event System.EventHandler TextChanged;

        public TextBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(TextBox_Paint);
            this.Resize += new EventHandler(TextBox_Resize);
            this.GotFocus += new EventHandler(TextBox_GotFocus);
            this.LostFocus += new EventHandler(TextBox_LostFocus);
            TextField.GotFocus += new EventHandler(TextField_GotFocus);
            TextField.LostFocus += new EventHandler(TextField_LostFocus);
            TextField.TextChanged += new EventHandler(TextField_TextChanged);

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;

            pFont = new Font(pfc.Families[0], pFontSize);
            TextField.Font = pFont;

            StartColor = Color.FromArgb(230, 230, 230);
            FinishColor = Color.FromArgb(190, 190, 190);
            BorderColors = Gradient.ComputeGradient(StartColor, FinishColor, GRADIENT_DISTANCE);

            BorderPen = new Pen(Color.Black);
            ColorCounter = 0;

            this.BackColor = Color.White;
        }

        private void TextField_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public int MaxLength
        {
            get { return TextField.MaxLength; }
            set { TextField.MaxLength = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Multiline
        {
            get { return TextField.Multiline; }
            set { TextField.Multiline = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return TextField.Text; }
            set { TextField.Text = value; }
        }

        private void TextField_LostFocus(object sender, EventArgs e)
        {
            ControlLostFocus();
        }

        private void TextField_GotFocus(object sender, EventArgs e)
        {
            ControlGotFocus();
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            ControlLostFocus();
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            ControlGotFocus();
        }

        private void ControlLostFocus()
        {
            if (InflateTmr.Enabled)
                InflateTmr.Enabled = false;

            ColorCounter = GRADIENT_DISTANCE - 1;
            DeflateTmr.Enabled = true;
        }

        private void ControlGotFocus()
        {
            if (DeflateTmr.Enabled)
                DeflateTmr.Enabled = false;

            ColorCounter = 0;
            InflateTmr.Enabled = true;
        }

        private void TextBox_Resize(object sender, EventArgs e)
        {
            if (TextField.Multiline)
                TextField.Height = this.Height - (PADDING * 2);
            else
                this.Height = TextField.Height + (PADDING * 2);

            TextField.Width = this.Width - (PADDING * 2);
        }

        private void TextBox_Paint(object sender, PaintEventArgs e)
        {
            if ((ColorCounter >= GRADIENT_DISTANCE) || (ColorCounter < 0))  //just in case
                return;

            BorderPen.Color = BorderColors[ColorCounter];
            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void InflateTmr_Tick(object sender, EventArgs e)
        {
            if ((ColorCounter + 1) >= GRADIENT_DISTANCE)
            {
                InflateTmr.Enabled = false;
                ColorCounter = GRADIENT_DISTANCE - 1;
                return;
            }

            this.Invalidate();
            ColorCounter ++;
        }

        private void DeflateTmr_Tick(object sender, EventArgs e)
        {
            if ((ColorCounter - 1) < 0)
            {
                DeflateTmr.Enabled = false;
                ColorCounter = 0;
                return;
            }

            this.Invalidate();
            ColorCounter --;
        }
    }
}
