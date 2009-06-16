using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            for (int i = 0; i < 50; i++)
                alphaListBox1.Items.Add(GenRandomString(i));
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
    }
}
