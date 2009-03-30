using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WildMouse.SmoothControls
{
    public partial class SequentialListBox : UserControl
    {
        public event System.EventHandler SelectedIndexChanged;

        private const int SCROLL_LARGE_CHANGE = 10;
        private const int SCROLL_SMALL_CHANGE = 5;

        private ListRowCollection pRowControls;
        private Pen BorderPen;
        private int pSelectedIndex;

        public SequentialListBox()
        {
            InitializeComponent();

            this.Resize += new EventHandler(SequentialListBox_Resize);
            this.Paint += new PaintEventHandler(SequentialListBox_Paint);

            pRowControls = new ListRowCollection();

            pRowControls.EntryAdded += new EventHandler(pRowControls_EntryAdded);
            pRowControls.EntryRemoved += new EventHandler(pRowControls_EntryRemoved);
            pRowControls.EntryChanged += new ListRowCollection.EntryChangedHandler(pRowControls_EntryChanged);

            ListScroller.Scroll += new ScrollEventHandler(ListScroller_Scroll);

            BorderPen = new Pen(Color.FromArgb(152, 152, 152));

            pSelectedIndex = -1;

            this.BackColorChanged += new EventHandler(SequentialListBox_BackColorChanged);

            ListScroller.LargeChange = SCROLL_LARGE_CHANGE;
            ListScroller.SmallChange = SCROLL_SMALL_CHANGE;
            UpdateScrollBar();
        }

        private void SequentialListBox_BackColorChanged(object sender, EventArgs e)
        {
            ElementsPanel.BackColor = this.BackColor;

            for (int i = 0; i < pRowControls.Count; i ++)
                pRowControls[i].BackColor = this.BackColor;
        }

        public int SelectedIndex
        {
            get { return pSelectedIndex; }
            set
            {
                if (pSelectedIndex > -1)
                    pRowControls[pSelectedIndex].Selected = false;

                pSelectedIndex = value;

                if (pSelectedIndex > -1)
                    pRowControls[pSelectedIndex].Selected = true;

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        public Color BorderColor
        {
            get { return BorderPen.Color; }
            set { BorderPen.Color = value; }
        }

        public ListRowCollection RowControls
        {
            get { return pRowControls; }
        }

        private void pRowControls_EntryChanged(object sender, int EntryIndex)
        {
        }

        private void pRowControls_EntryRemoved(object sender, EventArgs e)
        {
        }

        private void pRowControls_EntryAdded(object sender, EventArgs e)
        {
            Control AddedCtrl = (Control)pRowControls[pRowControls.Count - 1];

            ElementsPanel.Controls.Add(AddedCtrl);
            AddedCtrl.Click += new EventHandler(ListEntry_Click);
            
            UpdateLayout();
            UpdateScrollBar();
        }

        private void ListEntry_Click(object sender, EventArgs e)
        {
            SelectedIndex = pRowControls.IndexOf((IListRow)sender);
        }

        private void ListScroller_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (pRowControls.Count > 0)
            {
                pRowControls[0].Top = -ListScroller.Value;
                pRowControls[0].Width = ElementsPanel.Width;

                for (int i = 1; i < pRowControls.Count; i++)
                {
                    pRowControls[i].Left = 0;
                    pRowControls[i].Top = pRowControls[i - 1].Bottom;
                    pRowControls[i].Width = ElementsPanel.Width;
                }
            }
        }

        private void SequentialListBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void SequentialListBox_Resize(object sender, EventArgs e)
        {
            int ScrollerWidth = 0;

            if (ListScroller.Visible)
                ScrollerWidth = ListScroller.Width;

            ElementsPanel.Top = 1;
            ElementsPanel.Left = 1;
            ElementsPanel.Width = this.Width - (ScrollerWidth + 1);
            ElementsPanel.Height = this.Height - 2;

            ListScroller.Left = this.Width - ListScroller.Width;
            ListScroller.Top = 0;
            ListScroller.Height = this.Height;

            UpdateScrollBar();
            UpdateLayout();
        }

        private void UpdateScrollBar()
        {
            int ControlHeight = 0;

            for (int i = 0; i < pRowControls.Count; i ++)
                ControlHeight += pRowControls[i].ControlHeight;

            int ScrollMax = ControlHeight - ElementsPanel.Height;

            if (ScrollMax >= 0)
            {
                ListScroller.Maximum = ScrollMax;
                ListScroller.LargeChange = SCROLL_LARGE_CHANGE;
                ListScroller.SmallChange = SCROLL_SMALL_CHANGE;
                ListScroller.Visible = true;
                ElementsPanel.Width = this.Width - (ListScroller.Width - 2);
            }
            else
            {
                ListScroller.Maximum = 0;
                ListScroller.Visible = false;
                ElementsPanel.Width = this.Width - 2;
            }

            ListScroller.BringToFront();
        }
    }

    public class ListRowCollection
    {
        private List<IListRow> Items;

        public delegate void EntryChangedHandler(object sender, int EntryIndex);
        public event System.EventHandler EntryAdded;
        public event System.EventHandler EntryRemoved;
        public event EntryChangedHandler EntryChanged;
        public event System.EventHandler EntriesCleared;

        public ListRowCollection() { Items = new List<IListRow>(); }

        public void Add(IListRow NewItem)
        {
            Items.Add(NewItem);

            if (EntryAdded != null)
                EntryAdded(this, EventArgs.Empty);
        }

        public void RemoveAt(int Index)
        {
            Items.RemoveAt(Index);

            if (EntryRemoved != null)
                EntryRemoved(this, EventArgs.Empty);
        }

        public IListRow this[int Index]
        {
            get { return Items[Index]; }
            set
            {
                Items[Index] = value;

                if (EntryChanged != null)
                    EntryChanged(this, Index);
            }
        }

        public void Clear()
        {
            Items.Clear();

            if (EntriesCleared != null)
                EntriesCleared(this, EventArgs.Empty);
        }

        public int Count { get { return Items.Count; } }

        public int IndexOf(IListRow RowToFind) { return Items.IndexOf(RowToFind); }
    }
}
