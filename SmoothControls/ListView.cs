using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Text;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class ListView : ListBox
    {
        private const int SCROLL_LARGE_CHANGE = 10;

        public ListView()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            pListItems = new ListViewItemCollection();
            pListItems.EntriesCleared += new EventHandler(ListItems_EntriesCleared);
            pListItems.EntryAdded += new EventHandler(ListItems_EntryAdded);
            pListItems.EntryRemoved += new EventHandler(ListItems_EntryRemoved);
            //pListItems.EntryChanged += new ListViewItemCollection.EntryChangedHandler(pListItems_EntryChanged);

            pListItems.SubEntryAdded += new ListViewItemCollection.EntryChangedHandler(pListItems_SubEntryAdded);
            pListItems.SubEntryRemoved += new ListViewItemCollection.EntryChangedHandler(pListItems_SubEntryRemoved);
            pListItems.SubItemsCleared += new ListViewItemCollection.EntryChangedHandler(pListItems_SubItemsCleared);

            this.Paint += new PaintEventHandler(List_Paint);
            this.Resize += new EventHandler(List_Resize);

            HeaderBar.Changed += new ListHeader.ChangeHandler(HeaderBar_Changed);

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            pSelectedIndex = -1;
            RowSelectedColor = Color.FromArgb(213, 218, 244);

            RowColor1 = Color.FromArgb(255, 255, 255);
            RowColor2 = Color.FromArgb(245, 245, 245);

            pFontSize = 9;
            MakeFont();
            TextBrush = new SolidBrush(Color.Black);

            UpdateLayout();
            base.List_Resize(this, EventArgs.Empty);
        }

        //private void pListItems_EntryChanged(object sender, int EntryIndex)
        //{
            //for (int i = 0; i < ListRows.Count; i++)
                //ListRows[i].UpdateLayout();
        //}

        private void pListItems_SubItemsCleared(object sender, int EntryIndex)
        {
            UpdateLayout();
        }

        private void pListItems_SubEntryRemoved(object sender, int EntryIndex)
        {
            UpdateLayout();
            base.UpdateScrollBar();
            List_Resize(this, EventArgs.Empty);
        }

        private void pListItems_SubEntryAdded(object sender, int EntryIndex)
        {
            UpdateLayout();
            UpdateRowWidths();
            base.UpdateScrollBar();
            List_Resize(this, EventArgs.Empty);
        }

        public ListHeaderCollection Headers
        {
            get { return HeaderBar.Headers; }
            set
            {
                HeaderBar.Headers = value;

                UpdateRowWidths();

                HeaderBar.Invalidate();
            }
        }

        private void UpdateRowWidths()
        {
            ListComponent CurComponent;

            for (int i = 0; i < pListItems.Count; i++)
            {
                for (int c = 0; c < Headers.Count; c++)
                {
                    if (c == 0)
                        CurComponent = pListItems[i];
                    else
                    {
                        if (pListItems[i].SubItems.Count > (c - 1))
                            CurComponent = pListItems[i].SubItems[c - 1];
                        else
                            CurComponent = null;
                    }

                    if (CurComponent != null)
                        CurComponent.Width = Headers[c].Width;
                }
            }
        }

        private void HeaderBar_Changed(object sender, int Index)
        {
            ListComponent CurComponent;

            for (int i = 0; i < pListItems.Count; i++)
            {
                CurComponent = null;

                if (Index == 0)
                    CurComponent = pListItems[i];
                else if (pListItems[i].SubItems.Count >= Index)
                    CurComponent = pListItems[i].SubItems[Index - 1];

                if ((CurComponent != null) && (Headers.Count > Index))
                    CurComponent.Width = Headers[Index].Width;
            }

            if (pListItems.Count > Index)
            {
                for (int s = 0; s < pListItems[Index].SubItems.Count; s++)
                {
                    if (Headers.Count > (s + 1))
                        pListItems[Index].SubItems[s].Width = Headers[s + 1].Width;
                }
            }

            UpdateLayout();
        }

        protected override void List_Resize(object sender, EventArgs e)
        {
            int ScrollerWidth = 0;

            if (ListScroller.Visible)
                ScrollerWidth = ListScroller.Width;

            ListScroller.Height = this.Height;
            ListScroller.Left = this.Width - ScrollerWidth;
            ElementsPanel.Top = HeaderBar.Top + HeaderBar.Height;
            ElementsPanel.Width = this.Width - (ScrollerWidth + 2);
            ElementsPanel.Height = this.Height - (ElementsPanel.Top + 1);
            ElementsPanel.Left = 1;

            HeaderBar.Width = this.Width;

            base.List_Resize(sender, e);

            ListScroller.BringToFront();
        }

        protected override void ListItems_EntryRemoved(object sender, EventArgs e)
        {
            base.UpdateScrollBar();

            if (pSelectedIndex >= pListItems.Count)
                pSelectedIndex = -1;

            UpdateLayout();
        }

        protected override void ListItems_EntryAdded(object sender, EventArgs e)
        {
            UpdateLayout();
            UpdateRowWidths();
            base.UpdateScrollBar();
            List_Resize(this, EventArgs.Empty);
        }

        protected override void ListItems_EntriesCleared(object sender, EventArgs e)
        {
            base.UpdateScrollBar();
            UpdateLayout();
        }

        protected override void List_Paint(object sender, PaintEventArgs e)
        {
            UpdateLayout();
            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        protected override void UpdateLayout()
        {
            if (! base.bLayoutUpdating)
            {
                base.UpdateLayout();

                if (HeaderBar != null)
                    HeaderBar.Font = pFont;
            }
        }
    }
}
