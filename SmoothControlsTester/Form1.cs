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
    public partial class Form1 : Form
    {
        private int Counter = 0;

        public Form1()
        {
            InitializeComponent();

            verticalScrollBar1.Scroll += new ScrollEventHandler(verticalScrollBar1_Scroll);

            comboBox1.Items.Add("Supercalifragilisticexpialidotious");
            for (int i = 0; i < 10; i++)
                comboBox1.Items.Add(i.ToString());
        }

        private void verticalScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label1.Text = e.NewValue.ToString();
        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            //WildMouse.SmoothControls.MessageBox.Show(this, "Cameron Rules", "Important Message", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);
            //horizontalSlider1.Value = 25;

            WildMouse.SmoothControls.ListHeaderClass CurHeader;
            WildMouse.SmoothControls.ListHeaderCollection Headers = new WildMouse.SmoothControls.ListHeaderCollection();

            for (int i = 0; i < 3; i++)
            {
                CurHeader = new WildMouse.SmoothControls.ListHeaderClass();
                CurHeader.Text = "Header " + i.ToString();
                CurHeader.Width = 70;
                CurHeader.TextAlign = ContentAlignment.MiddleCenter;
                Headers.Add(CurHeader);
            }

            //listViewHeader1.Headers = Headers;

            listView1.Headers = Headers;

            WildMouse.SmoothControls.ListItem Item;
            WildMouse.SmoothControls.ListSubItem SubItem;

            //for (int i = 0; i < 20; i++)
            //{
                Item = new WildMouse.SmoothControls.ListItem();
                SubItem = new WildMouse.SmoothControls.ListSubItem();

                Item.Text = "cameron " + Counter.ToString();
                SubItem.Text = "Casey " + Counter.ToString();

                Item.SubItems.Add(SubItem);
                Item.SubItems.Add(new WildMouse.SmoothControls.ListSubItem("Michelle"));

                listView1.Items.Add(Item);
                //listViewRow1.ListInfo = Item;

                Counter ++;
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            workingIndicator1.AnimateOn();
            //smoothLabel2.FontSize = 12;
            //listView1.Width += 10;
            //listView1.Headers[0].Width = 100;
            //listView1.Headers[0].Text = "Ender!";

            //listView1.Items[2].SubItems.Add(new WildMouse.SmoothControls.ListSubItem("Wahoo!"));

            /*
            TestForm NewForm = new TestForm();
            Point Location = this.PointToScreen(button1.Location);

            NewForm.Show();
            NewForm.Top = Location.Y + button1.Height;
            NewForm.Left = Location.X;
            NewForm.Opacity = 1;
            */

            //listView1.Items.RemoveAt(listView1.SelectedIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void smoothLabel1_Load(object sender, EventArgs e)
        {

        }

        private void horizontalSlider1_ValueChanged(object sender, int NewValue)
        {
            label3.Text = horizontalSlider1.Value.ToString();
        }
    }
}
