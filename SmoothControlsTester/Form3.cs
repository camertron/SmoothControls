using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.SmoothControls;

namespace SmoothControlsTester
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 50; i++)
                //alphaListBox1.Items.Add(GenRandomString(i));

            IconListRow ilrCurRow = new IconListRow();
            ilrCurRow.Text = "Argentina";
            ilrCurRow.Icon = (Bitmap)Bitmap.FromFile("C:/Cameron's Stuff/DotNET/FlagGrabber/FlagGrabber/Flags/Tiny JPG/Argentina.jpg");

            alphaListBox1.RowControls.Add(ilrCurRow);

            ilrCurRow = new IconListRow();
            ilrCurRow.Text = "Hong Kong";
            ilrCurRow.Icon = (Bitmap)Bitmap.FromFile("C:/Cameron's Stuff/DotNET/FlagGrabber/FlagGrabber/Flags/Tiny JPG/Hong Kong.jpg");

            alphaListBox1.RowControls.Add(ilrCurRow);
        }

        private string GenRandomString(int iSeed)
        {
            Random rnd = new Random(iSeed + (int)DateTime.Now.Ticks);
            int iTimes = rnd.Next(4, 15);
            StringBuilder sbFinal = new StringBuilder();

            for (int i = 0; i < iTimes; i ++)
                sbFinal.Append((char)rnd.Next(65, 90));

            return sbFinal.ToString();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Bitmap bmpImage = (Bitmap)Bitmap.FromFile("C:/Cameron's Stuff/DotNET/FlagGrabber/FlagGrabber/Flags/Tiny JPG/Antarctica.jpg");
            System.Drawing.Graphics gCanvas = pbTestCanvas.CreateGraphics();
            Rectangle rImageArea = new Rectangle((pbTestCanvas.Width / 2) - (bmpImage.Width / 2), (pbTestCanvas.Height / 2) - (bmpImage.Height / 2), bmpImage.Width, bmpImage.Height);

            gCanvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gCanvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            gCanvas.DrawImage((Image)bmpImage, rImageArea);

            //WildMouse.SmoothControls.General.DrawDropShadow(gCanvas, rImageArea, pbTestCanvas.BackColor, Color.FromArgb(100, 100, 100), 5);

            gCanvas.DrawRectangle(new Pen(Color.FromArgb(100, 100, 100)), rImageArea);
        }
    }
}
