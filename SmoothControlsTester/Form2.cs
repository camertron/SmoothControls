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

            lvMedusa.Headers.Add(new ListHeaderClass("Addr", ContentAlignment.MiddleCenter, 40));
            lvMedusa.Headers.Add(new ListHeaderClass("Instr", ContentAlignment.MiddleCenter, 140));
            lvMedusa.Headers.Add(new ListHeaderClass("Code Snippet", ContentAlignment.MiddleCenter, 180));
            lvMedusa.Headers.Add(new ListHeaderClass("Proc", ContentAlignment.MiddleCenter, 50));

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

            /*
            for (int i = 0; i < 10; i++)
            {
                alphaListBox1.Items.Add(((char)(i + 65)).ToString());
                alphaListBox1.Items.Add(((char)(i + 65)).ToString());
                alphaListBox1.Items.Add(((char)(i + 65)).ToString());
            }*/

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

            LoadDummyWords();
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
            CompilationResultRow compilationResultRow1 = new CompilationResultRow();

            CodeItem ciNew = new CodeItem();
            ciNew.Address = "000";
            ciNew.InstructionCode = "45 0A 00";
            ciNew.CodeSnippet = "ACLo = ACHi + 1";

            compilationResultRow1.AddItem(ciNew);

            ciNew = new CodeItem();
            ciNew.Address = "001";
            ciNew.InstructionCode = "FF 03 1C";
            ciNew.CodeSnippet = "jump ifzero forward 3";

            compilationResultRow1.AddItem(ciNew);

            ciNew = new CodeItem();
            ciNew.Address = "002";
            ciNew.InstructionCode = "78 3D 29";
            ciNew.CodeSnippet = "return";

            compilationResultRow1.AddItem(ciNew);

            compilationResultRow1.Expanded = true;
            compilationResultList1.AddRow(compilationResultRow1);

            return;

            //segmentChooser1.SelectedIndex = 2;
            //ribbonButton1.ButtonEnabled = false;
            //listBox1.Items.Add("Cameron Rules!");
            //sequentialListBox1.Height += 10;

            lvMedusa.Items.Add("000");
            lvMedusa.Items[0].SubItems.Add(new ListSubItem("F6 0F 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[0].SubItems.Add(new ListSubItem("jump opcodein", ContentAlignment.MiddleCenter));
            lvMedusa.Items[0].SubItems.Add(new ListSubItem("ADBY", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("001");
            lvMedusa.Items[1].SubItems.Add(new ListSubItem("FF 02 0A", ContentAlignment.MiddleCenter));
            lvMedusa.Items[1].SubItems.Add(new ListSubItem("call NextBytesToMAR", ContentAlignment.MiddleCenter));
            lvMedusa.Items[1].SubItems.Add(new ListSubItem("ADBY", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("002");
            lvMedusa.Items[2].SubItems.Add(new ListSubItem("6B 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[2].SubItems.Add(new ListSubItem("ACLo = ACLo + MemAtMAR", ContentAlignment.MiddleCenter));
            lvMedusa.Items[2].SubItems.Add(new ListSubItem("ADBY", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("003");
            lvMedusa.Items[3].SubItems.Add(new ListSubItem("B6 80 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[3].SubItems.Add(new ListSubItem("ACLo = ACLo + MemAtMAR", ContentAlignment.MiddleCenter));
            lvMedusa.Items[3].SubItems.Add(new ListSubItem("ADBY", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("004");
            lvMedusa.Items[4].SubItems.Add(new ListSubItem("F6 0F 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[4].SubItems.Add(new ListSubItem("jump opcodein", ContentAlignment.MiddleCenter));
            lvMedusa.Items[4].SubItems.Add(new ListSubItem("ADBY", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("005");
            lvMedusa.Items[5].SubItems.Add(new ListSubItem("F2 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[5].SubItems.Add(new ListSubItem("MARAndPC ++", ContentAlignment.MiddleCenter));
            lvMedusa.Items[5].SubItems.Add(new ListSubItem("BYCO", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("006");
            lvMedusa.Items[6].SubItems.Add(new ListSubItem("E6 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[6].SubItems.Add(new ListSubItem("ACHi = MemAtMAR", ContentAlignment.MiddleCenter));
            lvMedusa.Items[6].SubItems.Add(new ListSubItem("BYCO", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("007");
            lvMedusa.Items[7].SubItems.Add(new ListSubItem("F2 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[7].SubItems.Add(new ListSubItem("MARAndPC ++", ContentAlignment.MiddleCenter));
            lvMedusa.Items[7].SubItems.Add(new ListSubItem("BYCO", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("008");
            lvMedusa.Items[8].SubItems.Add(new ListSubItem("F6 0F 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[8].SubItems.Add(new ListSubItem("jump opcodein", ContentAlignment.MiddleCenter));
            lvMedusa.Items[8].SubItems.Add(new ListSubItem("BYCO", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("009");
            lvMedusa.Items[9].SubItems.Add(new ListSubItem("FF 03 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[9].SubItems.Add(new ListSubItem("return", ContentAlignment.MiddleCenter));
            lvMedusa.Items[9].SubItems.Add(new ListSubItem("BYCO", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("00A");
            lvMedusa.Items[10].SubItems.Add(new ListSubItem("F2 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[10].SubItems.Add(new ListSubItem("MARAndPC ++", ContentAlignment.MiddleCenter));
            lvMedusa.Items[10].SubItems.Add(new ListSubItem("NextBytesToMAR", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("00B");
            lvMedusa.Items[11].SubItems.Add(new ListSubItem("D6 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[11].SubItems.Add(new ListSubItem("BRLo = MemAtMAR", ContentAlignment.MiddleCenter));
            lvMedusa.Items[11].SubItems.Add(new ListSubItem("NextBytesToMAR", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("00C");
            lvMedusa.Items[12].SubItems.Add(new ListSubItem("F2 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[12].SubItems.Add(new ListSubItem("MARAndPC ++", ContentAlignment.MiddleCenter));
            lvMedusa.Items[12].SubItems.Add(new ListSubItem("NextBytesToMAR", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("00D");
            lvMedusa.Items[13].SubItems.Add(new ListSubItem("C6 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[13].SubItems.Add(new ListSubItem("BRHi = MemAtMAR", ContentAlignment.MiddleCenter));
            lvMedusa.Items[13].SubItems.Add(new ListSubItem("NextBytesToMAR", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("00E");
            lvMedusa.Items[14].SubItems.Add(new ListSubItem("AD 00 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[14].SubItems.Add(new ListSubItem("MARLo = BRLo", ContentAlignment.MiddleCenter));
            lvMedusa.Items[14].SubItems.Add(new ListSubItem("NextBytesToMAR", ContentAlignment.MiddleCenter));

            lvMedusa.Items.Add("00F");
            lvMedusa.Items[15].SubItems.Add(new ListSubItem("9C 03 00", ContentAlignment.MiddleCenter));
            lvMedusa.Items[15].SubItems.Add(new ListSubItem("MARHi = BRHi  return", ContentAlignment.MiddleCenter));
            lvMedusa.Items[15].SubItems.Add(new ListSubItem("NextBytesToMAR", ContentAlignment.MiddleCenter));
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listViewRow1.ListInfo = new WildMouse.SmoothControls.ListItem("Cameron Rules");
        }
    }
}
