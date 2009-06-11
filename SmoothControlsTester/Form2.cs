using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.SmoothControls;
using System.IO;

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

            //SimpleListRow NewSimple = new SimpleListRow();
            //NewSimple.Text = "Cameron";
            //alphaListBox1.RowControls.Add(NewSimple);

            /*
            NewAlpha = new AlphaListRow();
            NewAlpha.Text = "Casey";
            alphaListBox1.RowControls.Add(NewAlpha);

            NewAlpha = new AlphaListRow();
            NewAlpha.Text = "Michelle";
            alphaListBox1.RowControls.Add(NewAlpha);
            */

            for (int i = 0; i < 10; i++)
            {
                alphaListBox1.Items.Add(((char)(i + 65)).ToString());
                alphaListBox1.Items.Add(((char)(i + 65)).ToString());
                alphaListBox1.Items.Add(((char)(i + 65)).ToString());
            }

            for (int i = 0; i < 10; i++)
                sequentialListBox1.Items.Add(i.ToString());

            /*
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
            */

            //LoadDummyWords();
        }

        private void LoadDummyWords()
        {
            StreamReader srReader = new StreamReader("C:/Cameron's Stuff/DotNET/Wallet/words.txt");
            string[] Words = srReader.ReadToEnd().Split(new char[1] { ' ' });

            for (int i = 0; i < Words.Length; i++)
                alphaListBox1.Items.Add(Words[i]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //segmentChooser1.SelectedIndex = 2;
            //ribbonButton1.ButtonEnabled = false;
            //listBox1.Items.Add("Cameron Rules!");
            //sequentialListBox1.Height += 10;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listViewRow1.ListInfo = new WildMouse.SmoothControls.ListItem("Cameron Rules");
        }
    }
}
