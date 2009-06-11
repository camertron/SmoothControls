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

        protected const int SCROLL_LARGE_CHANGE = 10;
        protected const int SCROLL_SMALL_CHANGE = 5;

        protected ListRowCollection pRowControls;
        protected Pen BorderPen;
        protected int pSelectedIndex;
        protected StringCollectionWithEvents m_scItems;
        protected Color RowSelectedColor;
        protected Color RowColor1;
        protected Color RowColor2;
        protected bool m_bUseRowColoring;

        public SequentialListBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            m_scItems = new StringCollectionWithEvents();
            m_scItems.ItemAdded += new StringCollectionWithEvents.ItemAddedEventHandler(m_scItems_ItemAdded);
            m_scItems.ItemChanged += new StringCollectionWithEvents.ItemChangedEventHandler(m_scItems_ItemChanged);
            m_scItems.ItemRemoved += new StringCollectionWithEvents.ItemRemovedEventHandler(m_scItems_ItemRemoved);

            this.Resize += new EventHandler(SequentialListBox_Resize);
            this.Paint += new PaintEventHandler(SequentialListBox_Paint);

            pRowControls = new ListRowCollection();

            pRowControls.EntryAdded += new EventHandler(pRowControls_EntryAdded);
            pRowControls.EntryRemoved += new ListRowCollection.EntryRemovedHandler(pRowControls_EntryRemoved);
            pRowControls.EntryChanged += new ListRowCollection.EntryChangedHandler(pRowControls_EntryChanged);

            ListScroller.Scroll += new ScrollEventHandler(ListScroller_Scroll);

            BorderPen = new Pen(Color.FromArgb(152, 152, 152));

            pSelectedIndex = -1;

            this.BackColorChanged += new EventHandler(SequentialListBox_BackColorChanged);

            ListScroller.LargeChange = SCROLL_LARGE_CHANGE;
            ListScroller.SmallChange = SCROLL_SMALL_CHANGE;
            UpdateScrollBar();

            RowSelectedColor = Color.FromArgb(213, 218, 244);
            RowColor2 = Color.FromArgb(255, 255, 255);
            RowColor1 = Color.FromArgb(245, 245, 245);
        }

        public bool UseRowColoring
        {
            get { return m_bUseRowColoring; }
            set { m_bUseRowColoring = value; }
        }

        public StringCollectionWithEvents Items
        {
            get { return m_scItems; }
        }

        protected virtual void StringItemRemoved(int iIndex, string sItemText)
        {
            pRowControls.RemoveAt(iIndex);
        }

        protected virtual void StringItemChanged(int iChangedIndex)
        {
            pRowControls[iChangedIndex].Text = m_scItems[iChangedIndex];
        }

        protected virtual void StringItemAdded(string sNewStr)
        {
            SimpleListRow slrNewRow = new SimpleListRow();

            slrNewRow.Text = sNewStr;
            pRowControls.Add(slrNewRow);
        }

        private void m_scItems_ItemRemoved(object sender, int iIndex, string sItemRemoved)
        {
            StringItemRemoved(iIndex, sItemRemoved);
        }

        private void m_scItems_ItemChanged(object sender, int iChangedIndex)
        {
            StringItemChanged(iChangedIndex);
        }

        private void m_scItems_ItemAdded(object sender, string sNewStr)
        {
            StringItemAdded(sNewStr);
        }

        private void SequentialListBox_BackColorChanged(object sender, EventArgs e)
        {
            ElementsPanel.BackColor = this.BackColor;

            for (int i = 0; i < pRowControls.Count; i ++)
                pRowControls[i].BackColor = this.BackColor;
        }

        public virtual int SelectedIndex
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

        public virtual Color BorderColor
        {
            get { return BorderPen.Color; }
            set { BorderPen.Color = value; }
        }

        public virtual ListRowCollection RowControls
        {
            get { return pRowControls; }
        }

        private void pRowControls_EntryChanged(object sender, int EntryIndex, string OldValue)
        {
            RowEntryChanged(EntryIndex, OldValue);
        }

        private void pRowControls_EntryAdded(object sender, EventArgs e)
        {
            RowEntryAdded();
        }

        private void pRowControls_EntryRemoved(object sender, int RemovedIndex)
        {
            RowEntryRemoved(RemovedIndex);
        }

        protected virtual void RowEntryChanged(int ChangedIndex, string OldValue)
        {
            UpdateLayout();
            UpdateScrollBar();
        }

        protected virtual void RowEntryRemoved(int RemovedIndex)
        {
            pRowControls.RemoveAt(RemovedIndex);

            UpdateLayout();
            UpdateScrollBar();
        }

        protected virtual void RowEntryAdded()
        {
            Control AddedCtrl = (Control)pRowControls[pRowControls.Count - 1];

            HookUpRow(AddedCtrl);

            UpdateLayout();
            UpdateScrollBar();
        }

        protected void HookUpRow(Control ToHookUp)
        {
            ElementsPanel.Controls.Add(ToHookUp);
            ToHookUp.Click += new EventHandler(ListEntry_Click);
        }

        private void ListEntry_Click(object sender, EventArgs e)
        {
            SelectedIndex = pRowControls.IndexOf((IListRow)sender);
        }

        private void ListScroller_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                UpdateLayout();
                this.Refresh();
            }
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        protected virtual void UpdateLayout()
        {
            if (pRowControls.Count > 0)
            {
                CheckControl(pRowControls[0]);
                pRowControls[0].Top = (-ListScroller.Value - 1);
                pRowControls[0].Width = ElementsPanel.Width;
                pRowControls[0].BackColor = GetRowColor(0);

                for (int i = 1; i < pRowControls.Count; i++)
                {
                    CheckControl(pRowControls[i]);
                    pRowControls[i].Left = 0;
                    pRowControls[i].Top = pRowControls[i - 1].Bottom;
                    pRowControls[i].Width = ElementsPanel.Width;
                    pRowControls[i].BackColor = GetRowColor(i);
                }
            }
        }

        private void CheckControl(IListRow ToCheck)
        {
            if (! ElementsPanel.Controls.Contains((Control)ToCheck))
            {
                ElementsPanel.Controls.Add((Control)ToCheck);
                ((Control)ToCheck).Visible = true;
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

        protected virtual void UpdateScrollBar()
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

        protected virtual Color GetRowColor(int Index)
        {
            if (m_bUseRowColoring)
            {
                if (Index == pSelectedIndex)
                    return RowSelectedColor;
                else if ((Index % 2) == 0)
                    return RowColor1;
                else
                    return RowColor2;
            }
            else
                return this.BackColor;
        }
    }

    public class ListRowCollection
    {
        private List<IListRow> Items;

        public delegate void EntryChangedHandler(object sender, int EntryIndex, string OldText);
        public delegate void EntryRemovedHandler(object sender, int RemovedIndex);
        public event System.EventHandler EntryAdded;
        public event EntryRemovedHandler EntryRemoved;
        public event EntryChangedHandler EntryChanged;
        public event System.EventHandler EntriesCleared;

        public ListRowCollection() { Items = new List<IListRow>(); }

        private int CompareListRows(IListRow First, IListRow Second)
        {
            int FirstChar, SecondChar;

            for (int i = 0; i < First.Text.Length; i++)
            {
                if (i >= Second.Text.Length)
                    return 0;
                else
                {
                    FirstChar = (int)First.Text[i];
                    SecondChar = (int)Second.Text[i];

                    if (FirstChar > SecondChar)
                        return 1;
                    else if (FirstChar < SecondChar)
                        return -1;
                }
            }

            return 0;
        }

        public void Sort()
        {
            Items.Sort(CompareListRows);
        }

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
                EntryRemoved(this, Index);
        }

        public void Remove(IListRow ToRemove)
        {
            Items.Remove(ToRemove);
        }

        public IListRow this[int Index]
        {
            get { return Items[Index]; }
            set
            {
                string OldStr = Items[Index].Text;
                Items[Index] = value;

                if (EntryChanged != null)
                    EntryChanged(this, Index, OldStr);
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

        public bool Contains(IListRow RowToFind) { return Items.Contains(RowToFind); }
    }
}
