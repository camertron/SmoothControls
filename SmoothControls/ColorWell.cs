using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("DisplayColorChanged")]
    public partial class ColorWell : UserControl
    {
        private Color pDisplayColor;
        private Color BorderColor;
        private Pen BorderPen;
        private SolidBrush FillBrush;

        private float Diameter;
        private float Radius;

        private const int CONTROL_HEIGHT = 29;
        private const int CONTROL_WIDTH = 160;

        public event System.EventHandler DisplayColorChanged;

        public ColorWell()
        {
            InitializeComponent();

            this.Resize += new EventHandler(ColorWell_Resize);
            ColorPanel.Paint += new PaintEventHandler(ColorPanel_Paint);
            ColorPanel.DoubleClick += new EventHandler(ColorPanel_DoubleClick);
            ColorTxt.TextChanged += new EventHandler(ColorTxt_TextChanged);

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            FillBrush = new SolidBrush(Color.Black);

            Diameter = 10.0f;
            Radius = 5.0f;
        }

        private void ColorTxt_TextChanged(object sender, EventArgs e)
        {
            ColorFromHexString(ColorTxt.Text);

            if (DisplayColorChanged != null)
                DisplayColorChanged(this, EventArgs.Empty);
        }

        private void ColorFromHexString(string Hex)
        {
            if (Hex.Length == 6)
            {
                if (IsValidHex(Hex))
                {
                    byte r = System.Convert.ToByte(Hex.Substring(0, 2), 16);
                    byte g = System.Convert.ToByte(Hex.Substring(2, 2), 16);
                    byte b = System.Convert.ToByte(Hex.Substring(4, 2), 16);

                    DisplayColor = Color.FromArgb(r, g, b);
                }
            }
        }

        private bool IsValidHex(string Str)
        {
            Str = Str.ToUpper();
            int Code;

            for (int i = 0; i < Str.Length; i ++)
            {
                Code = (int)Str[i];

                if (! (((Code >= 65) && (Code <= 70)) || ((Code >= 48) && (Code <= 57))))
                    return false;
            }

            return true;
        }

        private void ColorPanel_DoubleClick(object sender, EventArgs e)
        {
            if (ColorPickerDlg.ShowDialog() == DialogResult.OK)
                this.DisplayColor = ColorPickerDlg.Color;

            if (DisplayColorChanged != null)
                DisplayColorChanged(this, EventArgs.Empty);
        }

        private string PadHex(string Hex)
        {
            if (Hex.Length == 1)
                return "0" + Hex;
            else
                return Hex;
        }

        public Color DisplayColor
        {
            get { return pDisplayColor; }
            set
            {
                pDisplayColor = value;

                FillBrush.Color = pDisplayColor;
                ColorTxt.Text = PadHex(pDisplayColor.R.ToString("x").ToUpper()) + PadHex(pDisplayColor.G.ToString("x").ToUpper()) + PadHex(pDisplayColor.B.ToString("x").ToUpper());
                ColorPanel.Invalidate();

                if (DisplayColorChanged != null)
                    DisplayColorChanged(this, EventArgs.Empty);
            }
        }

        private void ColorWell_Resize(object sender, EventArgs e)
        {
            this.Width = CONTROL_WIDTH;
            this.Height = CONTROL_HEIGHT;
        }

        private void ColorPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            e.Graphics.FillEllipse(FillBrush, 0, 0, Diameter, Diameter);  //upper left
            e.Graphics.FillEllipse(FillBrush, 0, ColorPanel.Height - (Diameter + 1), Diameter, Diameter);  //lower left
            e.Graphics.FillEllipse(FillBrush, ColorPanel.Width - (Diameter + 1), 0, Diameter, Diameter);  //upper right
            e.Graphics.FillEllipse(FillBrush, ColorPanel.Width - (Diameter + 1), ColorPanel.Height - (Diameter + 1), Diameter, Diameter);  //lower right

            e.Graphics.FillRectangle(FillBrush, Radius, 0, ColorPanel.Width - Diameter, ColorPanel.Height);  //height-based rectangle
            e.Graphics.FillRectangle(FillBrush, 0, Radius, ColorPanel.Width, ColorPanel.Height - Diameter);  //width-based rectangle

            e.Graphics.DrawArc(BorderPen, 0, 0, Diameter, Diameter, -90, -90);  //top left
            e.Graphics.DrawArc(BorderPen, ColorPanel.Width - (Diameter + 1), 0, Diameter, Diameter, -90, 90);  //upper right
            e.Graphics.DrawArc(BorderPen, ColorPanel.Width - (Diameter + 1), ColorPanel.Height - (Diameter + 1), Diameter, Diameter, 90, -90);  //lower right
            e.Graphics.DrawArc(BorderPen, 0, ColorPanel.Height - (Diameter + 1), Diameter, Diameter, 90, 90);  //lower left

            e.Graphics.DrawLine(BorderPen, Radius + 1, 0, ColorPanel.Width - (Radius + 2), 0);  //top
            e.Graphics.DrawLine(BorderPen, Radius + 1, ColorPanel.Height - 1, ColorPanel.Width - (Radius + 2), ColorPanel.Height - 1);  //bottom
            e.Graphics.DrawLine(BorderPen, 0, Radius, 0, ColorPanel.Height - Radius);  //left
            e.Graphics.DrawLine(BorderPen, ColorPanel.Width - 1, Radius, ColorPanel.Width - 1, ColorPanel.Height - (Radius + 1));  //right
        }
    }
}
