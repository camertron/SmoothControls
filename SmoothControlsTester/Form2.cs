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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            //viewChooser1.Tabs.Add("Cameron");
            //viewChooser1.Tabs.Add("Casey");
            //viewChooser1.Tabs.Add("Michelle");

            IconListRow NewRow;

            NewRow = new IconListRow();
            NewRow.Text = "Serial Numbers";
            NewRow.Icon = (Bitmap)Bitmap.FromFile("C:\\Cameron's Stuff\\DotNET\\Wallet\\Icons\\applications.png");
            //NewRow.Selected = true;
            sequentialListBox1.RowControls.Add(NewRow);

            NewRow = new IconListRow();
            NewRow.Text = "Web Passwords";
            NewRow.Icon = (Bitmap)Bitmap.FromFile("C:\\Cameron's Stuff\\DotNET\\Wallet\\Icons\\safari.png");
            //NewRow.Selected = true;
            sequentialListBox1.RowControls.Add(NewRow);

            NewRow = new IconListRow();
            NewRow.Text = "Credit Cards";
            NewRow.Icon = (Bitmap)Bitmap.FromFile("C:\\Cameron's Stuff\\DotNET\\Wallet\\Icons\\contact.png");
            //NewRow.Selected = true;
            sequentialListBox1.RowControls.Add(NewRow);

            NewRow = new IconListRow();
            NewRow.Text = "Domains";
            NewRow.Icon = (Bitmap)Bitmap.FromFile("C:\\Cameron's Stuff\\DotNET\\Wallet\\Icons\\address_book.png");
            //NewRow.Selected = true;
            sequentialListBox1.RowControls.Add(NewRow);

            NewRow = new IconListRow();
            NewRow.Text = "Screen Names";
            NewRow.Icon = (Bitmap)Bitmap.FromFile("C:\\Cameron's Stuff\\DotNET\\Wallet\\Icons\\face-smile.png");
            //NewRow.Selected = true;
            sequentialListBox1.RowControls.Add(NewRow);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //segmentChooser1.SelectedIndex = 2;
            //ribbonButton1.ButtonEnabled = false;
            //listBox1.Items.Add("Cameron Rules!");
            sequentialListBox1.Height += 10;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listViewRow1.ListInfo = new WildMouse.SmoothControls.ListItem("Cameron Rules");
        }
    }
}
