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
    public partial class ListBox : UserControl
    {
        private const int SCROLL_LARGE_CHANGE = 10;

        protected ListViewItemCollection pListItems;
        protected DoubleQueue<ListRow> ListRows;
        protected Color BorderColor;
        protected Pen BorderPen;
        protected int MaxVisibleRows;
        protected int pSelectedIndex;
        protected Color RowSelectedColor;
        protected Color RowColor1;
        protected Color RowColor2;
        protected int pFontSize;
        protected SolidBrush TextBrush;
        protected Color pTextColor;
        protected Font pFont;
        protected bool bLayoutUpdating;
        protected bool bScrollbarUpdating;

        public event System.EventHandler SelectedIndexChanged;

        public ListBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Paint += new PaintEventHandler(List_Paint);
            this.Resize += new EventHandler(List_Resize);
            ListScroller.Scroll += new ScrollEventHandler(ListScroller_Scroll);

            BorderColor = Color.FromArgb(152, 152, 152);
            BorderPen = new Pen(BorderColor);

            pSelectedIndex = -1;
            RowSelectedColor = Color.FromArgb(213, 218, 244);

            RowColor2 = Color.FromArgb(255, 255, 255);
            RowColor1 = Color.FromArgb(245, 245, 245);

            pFontSize = 9;
            pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
            TextBrush = new SolidBrush(Color.Black);

            pListItems = new ListViewItemCollection();
            ListRows = new DoubleQueue<ListRow>();

            pListItems.EntriesCleared += new EventHandler(ListItems_EntriesCleared);
            pListItems.EntryAdded += new EventHandler(ListItems_EntryAdded);
            pListItems.EntryChanged += new ListViewItemCollection.EntryChangedHandler(ListItems_EntryChanged);
            pListItems.EntryRemoved += new EventHandler(ListItems_EntryRemoved);

            ListScroller.Visible = false;
            bLayoutUpdating = false;

            UpdateLayout();
        }

        protected void InvokeSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        protected virtual void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public virtual int FontSize
        {
            get { return pFontSize; }
            set
            {
                pFontSize = value;
                pFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, pFontSize);
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public virtual Color TextColor
        {
            get { return pTextColor; }
            set
            {
                pTextColor = value;
                TextBrush.Color = pTextColor;
                InvalidateParent();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public virtual int SelectedIndex
        {
            get
            {
                return pSelectedIndex;
            }
            set
            {
                pSelectedIndex = value;

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        public virtual ListViewItemCollection Items
        {
            get { return pListItems; }
        }

        protected virtual void ListItems_EntryRemoved(object sender, EventArgs e)
        {
            UpdateScrollBar();

            if (pSelectedIndex >= pListItems.Count)
                pSelectedIndex = -1;

            UpdateLayout();
        }

        protected virtual void ListItems_EntryChanged(object sender, int EntryIndex)
        {
            UpdateLayout();
        }

        protected virtual void ListItems_EntryAdded(object sender, EventArgs e)
        {
            pListItems[pListItems.Count - 1].Width = ElementsPanel.Width;

            UpdateScrollBar();
            List_Resize(this, EventArgs.Empty);
            UpdateLayout();
        }

        protected virtual void ListItems_EntriesCleared(object sender, EventArgs e)
        {
            SelectedIndex = -1;
            UpdateScrollBar();
            List_Resize(this, EventArgs.Empty);
            UpdateLayout();
        }

        private void ListScroller_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                int Overhang = ListScroller.Value % ListRow.CONTROL_HEIGHT;

                if ((Overhang == 0) && (ListScroller.Value > 0))
                    ListRows.EnqueueBack(ListRows.DequeueFront());

                ListRows[0].Top = -Overhang;
                UpdateLayout();
                this.Refresh();
            }
        }

        protected virtual void UpdateLayout()
        {
            if (! bLayoutUpdating)
            {
                bLayoutUpdating = true;

                MaxVisibleRows = (this.Height / ListRow.CONTROL_HEIGHT) + 2;

                if (ListRows.Count < MaxVisibleRows)
                {
                    ListRow NewRow;

                    //for (int i = 0; i < MaxVisibleRows - ListRows.Count; i ++)
                    for (int i = 0; i < MaxVisibleRows; i++)
                    {
                        NewRow = new ListRow();
                        NewRow.CmdKeyPressed += new CmdKeyPressedHandler(Row_ArrowPressed);
                        NewRow.OnClick += new EventHandler(Row_OnClick);
                        NewRow.Width = this.Width;
                        NewRow.Left = 0;
                        ElementsPanel.Controls.Add(NewRow);
                        ListRows.EnqueueBack(NewRow);
                    }
                }

                int ScrollIndex = (int)Math.Floor((double)ListScroller.Value / (double)ListRow.CONTROL_HEIGHT);

                for (int i = 0; i < ListRows.Count; i++)
                {
                    if ((ScrollIndex + i) >= pListItems.Count)
                        ListRows[i].ListInfo = null;
                    else
                        ListRows[i].ListInfo = pListItems[ScrollIndex + i];

                    ListRows[i].Top = (i * ListRow.CONTROL_HEIGHT) - (ListScroller.Value % ListRow.CONTROL_HEIGHT);
                    ListRows[i].BackColor = GetRowColor(ScrollIndex + i);
                    ListRows[i].Font = pFont;
                    ListRows[i].Visible = true;
                    ListRows[i].Width = ElementsPanel.Width;
                }

                bLayoutUpdating = false;
            }
        }

        private void Row_ArrowPressed(object sender, Keys Key)
        {
            if (Key == Keys.Down)
            {
                if ((pSelectedIndex + 1) < pListItems.Count)
                {
                    SelectedIndex++;
                    ListScroller.Value = ((SelectedIndex + 1) * ListRow.CONTROL_HEIGHT) - ElementsPanel.Height;
                    UpdateLayout();

                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
            else if (Key == Keys.Up)
            {
                if ((pSelectedIndex - 1) >= 0)
                {
                    SelectedIndex--;

                    if (CalcScrollIndex() >= SelectedIndex)
                        ListScroller.Value = (SelectedIndex * ListRow.CONTROL_HEIGHT);

                    UpdateLayout();

                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
        }

        protected virtual void UpdateScrollBar()
        {
            if (! bScrollbarUpdating)
            {
                bScrollbarUpdating = true;

                int ScrollMax = (pListItems.Count * ListRow.CONTROL_HEIGHT) - ElementsPanel.Height;

                if (ScrollMax >= 0)
                {
                    ListScroller.Maximum = ScrollMax;
                    ListScroller.LargeChange = SCROLL_LARGE_CHANGE;
                    ListScroller.Visible = true;
                    ListScroller.BringToFront();
                    ListScroller.Left = this.Width - ListScroller.Width;
                }
                else
                {
                    ListScroller.Maximum = 0;
                    ListScroller.Visible = false;
                }

                bScrollbarUpdating = false;
            }
        }

        protected virtual void List_Resize(object sender, EventArgs e)
        {
            int ScrollerWidth = 0;

            if (ListScroller.Visible)
                ScrollerWidth = ListScroller.Width;

            ListScroller.Height = this.Height;
            ListScroller.Left = this.Width - ScrollerWidth;
            ElementsPanel.Width = this.Width - (ScrollerWidth + 2);
            ElementsPanel.Height = this.Height - (ElementsPanel.Top + 1);
            ElementsPanel.Left = 1;

            for (int i = 0; i < ListRows.Count; i ++)
                ListRows[i].Width = ElementsPanel.Width;
        }

        protected virtual void List_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        protected virtual Color GetRowColor(int Index)
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
            ListRow Row = (ListRow)sender;
            int Offset = CalcOffset(Row);
            int ScrollIndex = CalcScrollIndex();

            if ((ScrollIndex + Offset) >= pListItems.Count)
                return;

            Row.BackColor = RowSelectedColor;
            pSelectedIndex = ScrollIndex + Offset;

            UpdateLayout();

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        protected virtual int CalcScrollIndex()
        {
            return (int)Math.Floor((double)ListScroller.Value / (double)ListRow.CONTROL_HEIGHT);
        }

        protected virtual int CalcOffset(ListRow Row)
        {
            int Offset = (Row.Top + Row.Height) / ListRow.CONTROL_HEIGHT;

            if (ListScroller.Value == 0)
                return Offset - 1;
            else
                return Offset;
        }
    }
}