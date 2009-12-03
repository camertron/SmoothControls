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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            List<string> hello = new List<string>();
            hello.Add("Cameron");
            hello.Add("Casey");
            hello.Add("Michelle");
            hello.Add("Terry");
            hello.Add("Siri");
            hello.Add("Cheerio");
            hello.Add("Franja");
            bulletedLabel1.Items = hello;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stTable.AddRowLabel("0x000");
            stTable.AddRowLabel("0x010");
            stTable.AddRowLabel("0x020");
            stTable.AddRowLabel("0x030");

            stTable.AddColumnLabel("Col 1");
            stTable.AddColumnLabel("Col 2");
            stTable.AddColumnLabel("Col 3");
            stTable.AddColumnLabel("Col 4");

            stTable.SetValue(0, 0, "Hello!");
            stTable.SetValue(0, 1, "Cam");
            stTable.SetValue(1, 1, "Case");

            stTable.Invalidate();
        }
    }
}
