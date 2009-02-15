﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace WildMouse.SmoothControls
{
    public partial class SmoothLabel : UserControl
    {
        private Font pFont;
        private int pFontSize;
        private PrivateFontCollection pfc;
        private Color TextColor;
        private SolidBrush TextBrush;
        private string pText;
        private ContentAlignment pTextAlign;

        public SmoothLabel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(SmoothLabel_Paint);

            TextColor = Color.Black;
            TextBrush = new SolidBrush(TextColor);

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFontSize = 10;
            pFont = new Font(pfc.Families[0], pFontSize);
            MeasureLbl.Font = pFont;

            pTextAlign = ContentAlignment.TopLeft;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override Color ForeColor
        {
            get { return TextColor; }
            set
            {
                TextColor = value;
                TextBrush.Color = TextColor;
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public int FontSize
        {
            get { return pFontSize; }
            set
            {
                pFontSize = value;
                pFont = new Font(pfc.Families[0], pFontSize);
                MeasureLbl.Font = pFont;
                this.Invalidate();
            }
        }

        private void SmoothLabel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            float Top = 0;
            float Left = 0;
            int LblHeight = MeasureLbl.Height + 1;  //a little tweaking...
            int LblWidth = MeasureLbl.Width - 5;

            switch (pTextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    Top = this.Height - LblHeight;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    Top = (this.Height / 2) - (LblHeight / 2);
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    Top = 0;
                    break;
            }

            switch (pTextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    Left = (this.Width / 2) - (LblWidth / 2);
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    Left = 0;
                    break;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    Left = this.Width - LblWidth;
                    break;
            }

            e.Graphics.DrawString(pText, pFont, TextBrush, Left, Top);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return pText; }
            set
            {
                pText = value;
                MeasureLbl.Text = pText;
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public ContentAlignment TextAlign
        {
            get { return pTextAlign; }
            set
            {
                pTextAlign = value;
                this.Invalidate();
            }
        }
    }
}