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
    public partial class ListView : UserControl
    {
        private const int SCROLL_LARGE_CHANGE = 10;

        private ListViewItemCollection pListItems;
        private DoubleQueue<ListViewRow> ListRows;
        private Color BorderColor;
        private Pen BorderPen;
        private int MaxVisibleRows;
        private int pSelectedIndex;
        private Color RowSelectedColor;
        private Color RowColor1;
        private Color RowColor2;
        private PrivateFontCollection pfc;
        private int pFontSize;
        private SolidBrush TextBrush;
        private Color pTextColor;
        private Font pFont;

        public event System.EventHandler SelectedIndexChanged;

        public ListView()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            ListRows = new DoubleQueue<ListViewRow>();

            pListItems = new ListViewItemCollection();
            pListItems.EntriesCleared += new EventHandler(ListItems_EntriesCleared);
            pListItems.EntryAdded += new EventHandler(ListItems_EntryAdded);
            pListItems.EntryChanged += new ListViewItemCollection.EntryChangedHandler(ListItems_EntryChanged);
            pListItems.EntryRemoved += new EventHandler(ListItems_EntryRemoved);

            pListItems.SubEntryAdded += new ListViewItemCollection.EntryChangedHandler(pListItems_SubEntryAdded);
            pListItems.SubEntryChanged += new ListViewItemCollection.SubEntryChangedHandler(pListItems_SubEntryChanged);
            pListItems.SubEntryRemoved += new ListViewItemCollection.EntryChangedHandler(pListItems_SubEntryRemoved);
            pListItems.SubItemsCleared += new ListViewItemCollection.EntryChangedHandler(pListItems_SubItemsCleared);

            this.Paint += new PaintEventHandler(ListView_Paint);
            this.Resize += new EventHandler(ListView_Resize);
            ListScroller.Scroll += new ScrollEventHandler(ListScroller_Scroll);

            HeaderBar.Changed += new ListViewHeader.ChangeHandler(HeaderBar_Changed);

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            pSelectedIndex = -1;
            RowSelectedColor = Color.FromArgb(213, 218, 244);

            RowColor1 = Color.FromArgb(255, 255, 255);
            RowColor2 = Color.FromArgb(245, 245, 245);

            pFontSize = 9;
            MakeFont();
            TextBrush = new SolidBrush(Color.Black);
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void MakeFont()
        {
            if (pfc != null)
            {
                pfc.Families[0].Dispose();
                pfc.Dispose();
                pFont.Dispose();
            }

            pfc = General.PrepFont("MyriadPro-Regular.ttf");
            pFont = new Font(pfc.Families[0], pFontSize);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public int FontSize
        {
            get { return pFontSize; }
            set
            {
                pFontSize = value;
                MakeFont();
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public Color TextColor
        {
            get { return pTextColor; }
            set
            {
                pTextColor = value;
                TextBrush.Color = pTextColor;
                InvalidateParent();
            }
        }

        private void pListItems_SubItemsCleared(object sender, int EntryIndex)
        {
        }

        private void pListItems_SubEntryRemoved(object sender, int EntryIndex)
        {
        }

        private void pListItems_SubEntryChanged(object sender, int EntryIndex, int SubIndex)
        {
        }

        private void pListItems_SubEntryAdded(object sender, int EntryIndex)
        {
            for (int i = 0; i < pListItems[EntryIndex].SubItems.Count; i ++)
            {
                if (Headers.Count > i)
                    pListItems[EntryIndex].SubItems[i].Width = Headers[i].Width;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return pSelectedIndex;
            }
            set
            {
                //set index
                //highlight
                //scroll to location

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        public ListHeaderCollection Headers
        {
            get { return HeaderBar.Headers; }
            set
            {
                HeaderBar.Headers = value;

                ListComponent CurComponent;

                for (int i = 0; i < pListItems.Count; i ++)
                {
                    for (int c = 0; c < Headers.Count; c ++)
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

                HeaderBar.Invalidate();
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
                else if (pListItems[i].SubItems.Count > Index)
                    CurComponent = pListItems[i].SubItems[Index];

                if ((CurComponent != null) && (Headers.Count > Index))
                    CurComponent.Width = Headers[Index].Width;
            }
        }

        private void ListScroller_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                int Overhang = ListScroller.Value % ListViewRow.CONTROL_HEIGHT;

                if ((Overhang == 0) && (ListScroller.Value > 0))
                    ListRows.EnqueueBack(ListRows.DequeueFront());

                ListRows[0].Top = -Overhang;
                UpdateLayout();
            }
        }

        private void ListView_Resize(object sender, EventArgs e)
        {
            ListScroller.Height = this.Height;
            ListScroller.Left = this.Width - ListScroller.Width;
            ElementsPanel.Width = this.Width - (ListScroller.Width + 1);
            ElementsPanel.Height = this.Height - (ElementsPanel.Top + 1);
            ElementsPanel.Left = 1;

            HeaderBar.Width = this.Width;
        }

        public ListViewItemCollection Items
        {
            get { return pListItems; }
        }

        private void ListItems_EntryRemoved(object sender, EventArgs e)
        {
            UpdateScrollBar();

            if (pSelectedIndex >= pListItems.Count)
                pSelectedIndex = -1;

            UpdateLayout();
        }

        private void ListItems_EntryChanged(object sender, int EntryIndex)
        {
            UpdateLayout();
        }

        private void ListItems_EntryAdded(object sender, EventArgs e)
        {
            for (int i = 0; i < pListItems[pListItems.Count - 1].SubItems.Count + 1; i ++)
            {
                if (Headers.Count > i)
                {
                    if (i == 0)
                        pListItems[pListItems.Count - 1].Width = Headers[i].Width;
                    else
                        pListItems[pListItems.Count - 1].SubItems[i - 1].Width = Headers[i].Width;
                }
            }

            UpdateScrollBar();
            UpdateLayout();
        }

        private void ListItems_EntriesCleared(object sender, EventArgs e)
        {
            UpdateScrollBar();
            UpdateLayout();
        }

        private void UpdateScrollBar()
        {
            int ScrollMax = (pListItems.Count * ListViewRow.CONTROL_HEIGHT) - ElementsPanel.Height;

            if (ScrollMax >= 0)
            {
                ListScroller.Maximum = ScrollMax;
                ListScroller.LargeChange = SCROLL_LARGE_CHANGE;
            }
            else
                ListScroller.Maximum = 0;
        }

        private void ListView_Paint(object sender, PaintEventArgs e)
        {
            UpdateLayout();
            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void UpdateLayout()
        {
            MaxVisibleRows = (this.Height / ListViewRow.CONTROL_HEIGHT) + 2;
            HeaderBar.Font = pFont;

            if (ListRows.Count < MaxVisibleRows)
            {
                ListViewRow NewRow;

                //for (int i = 0; i < MaxVisibleRows - ListRows.Count; i ++)
                for (int i = 0; i < MaxVisibleRows; i++)
                {
                    NewRow = new ListViewRow();
                    NewRow.ArrowPressed += new ListViewRow.ArrowPressedHandler(Row_ArrowPressed);
                    NewRow.OnClick += new EventHandler(Row_OnClick);
                    NewRow.Width = this.Width;
                    NewRow.Left = 0;
                    ElementsPanel.Controls.Add(NewRow);
                    ListRows.EnqueueBack(NewRow);
                }
            }

            int ScrollIndex = (int)Math.Floor((double)ListScroller.Value / (double)ListViewRow.CONTROL_HEIGHT);

            for (int i = 0; i < ListRows.Count; i ++)
            {
                if ((ScrollIndex + i) >= pListItems.Count)
                    ListRows[i].ListInfo = null;
                else
                    ListRows[i].ListInfo = pListItems[ScrollIndex + i];

                ListRows[i].Top = (i * ListViewRow.CONTROL_HEIGHT) - (ListScroller.Value % ListViewRow.CONTROL_HEIGHT);
                ListRows[i].BackColor = GetRowColor(ScrollIndex + i);
                ListRows[i].Font = pFont;
                ListRows[i].Visible = true;
            }
        }

        private void Row_ArrowPressed(object sender, Keys Key)
        {
            if (Key == Keys.Down)
            {
                if ((pSelectedIndex + 1) < pListItems.Count)
                {
                    pSelectedIndex++;
                    UpdateLayout();

                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
            else if (Key == Keys.Up)
            {
                if ((pSelectedIndex - 1) >= 0)
                {
                    pSelectedIndex--;
                    UpdateLayout();

                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
        }

        private Color GetRowColor(int Index)
        {
            if (Index == pSelectedIndex)
                return RowSelectedColor;
            else if ((Index % 2) == 0)
                return RowColor1;
            else
                return RowColor2;
        }

        private void Row_OnClick(object sender, EventArgs e)
        {
            int Offset = (int)Math.Ceiling((double)(((ListViewRow)sender).Top / ListViewRow.CONTROL_HEIGHT));
            int ScrollIndex = (int)Math.Floor((double)ListScroller.Value / (double)ListViewRow.CONTROL_HEIGHT);

            if ((ScrollIndex + Offset) >= pListItems.Count)
                return;

            ((ListViewRow)sender).BackColor = RowSelectedColor;
            pSelectedIndex = ScrollIndex + Offset;

            UpdateLayout();

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }
    }
}
