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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            //viewChooser1.Tabs.Add("Cameron");
            //viewChooser1.Tabs.Add("Casey");
            //viewChooser1.Tabs.Add("Michelle");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //segmentChooser1.SelectedIndex = 2;
            ribbonButton1.ButtonEnabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listViewRow1.ListInfo = new WildMouse.SmoothControls.ListViewItem("Cameron Rules");
        }
    }
}
