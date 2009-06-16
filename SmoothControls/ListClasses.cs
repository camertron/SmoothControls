using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace WildMouse.SmoothControls
{
    public delegate void CmdKeyPressedHandler(object sender, Keys Key);

    public interface IListRow
    {
        int Width { get; set; }
        int Height { get; set; }
        int Top { get; set; }
        int Left { get; set; }
        int Bottom { get; }
        int Right { get; }

        int ControlHeight { get; }
        bool Selected { get; set; }
        Color BackColor { get; set; }
        string Text { get; set; }
        event System.EventHandler Click;
        event CmdKeyPressedHandler CmdKeyPressed;
        bool Visible { get; set; }
        System.Windows.Forms.BorderStyle BorderStyle { get; set; }
    }

    internal interface ListComponent
    {
        string Text { get; set; }
        int Width { get; set; }
        ContentAlignment TextAlign { get; set; }
    }

    public class ListItem : ListComponent
    {
        private string pText;
        private int pWidth;
        private bool pChecked;
        private ListSubItemCollection pSubItems;
        private ContentAlignment pTextAlign;

        public delegate void SubItemChangedHandler(object sender, int SubIndex);

        public event System.EventHandler Changed;
        public event System.EventHandler SubItemsCleared;
        public event System.EventHandler SubItemAdded;
        public event SubItemChangedHandler SubItemChanged;
        public event System.EventHandler SubItemRemoved;

        public ListItem()
        {
            pText = "";
            pWidth = 70;
            pChecked = false;
            pSubItems = new ListSubItemCollection();
            pTextAlign = ContentAlignment.MiddleCenter;

            HookUpSubItems();
        }

        public ListItem(string InitText)
        {
            Text = InitText;
            pWidth = 70;
            pSubItems = new ListSubItemCollection();
            pTextAlign = ContentAlignment.MiddleCenter;

            HookUpSubItems();
        }

        private void HookUpSubItems()
        {
            pSubItems.EntriesCleared += new EventHandler(pSubItems_EntriesCleared);
            pSubItems.EntryAdded += new EventHandler(pSubItems_EntryAdded);
            pSubItems.EntryChanged += new ListSubItemCollection.EntryChangedHandler(pSubItems_EntryChanged);
            pSubItems.EntryRemoved += new EventHandler(pSubItems_EntryRemoved);
        }

        public string Text
        {
            get { return pText; }
            set
            {
                pText = value;
                RaiseItemChanged();
            }
        }

        public int Width
        {
            get { return pWidth; }
            set
            {
                pWidth = value;
                RaiseItemChanged();
            }
        }

        public bool Checked
        {
            get { return pChecked; }
            set
            {
                pChecked = value;
                RaiseItemChanged();
            }
        }

        public ListSubItemCollection SubItems
        {
            get { return pSubItems; }
            set
            {
                pSubItems = value;
                HookUpSubItems();
                RaiseItemChanged();
            }
        }

        private void pSubItems_EntryRemoved(object sender, EventArgs e)
        {
            if (SubItemRemoved != null)
                SubItemRemoved(this, EventArgs.Empty);

            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        private void pSubItems_EntryAdded(object sender, EventArgs e)
        {
            if (SubItemAdded != null)
                SubItemAdded(this, EventArgs.Empty);

            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        private void pSubItems_EntriesCleared(object sender, EventArgs e)
        {
            if (SubItemsCleared != null)
                SubItemsCleared(this, EventArgs.Empty);

            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        private void pSubItems_EntryChanged(object sender, int EntryIndex)
        {
            if (SubItemChanged != null)
                SubItemChanged(this, EntryIndex);

            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public ContentAlignment TextAlign
        {
            get { return pTextAlign; }
            set
            {
                pTextAlign = value;
                RaiseItemChanged();
            } 
        }

        private void RaiseItemChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }

    public class ListViewItemCollection
    {
        private ArrayList Items;

        public delegate void EntryChangedHandler(object sender, int EntryIndex);
        public delegate void SubEntryChangedHandler(object sender, int EntryIndex, int SubIndex);

        public event System.EventHandler EntryAdded;
        public event System.EventHandler EntryRemoved;
        public event EntryChangedHandler EntryChanged;
        public event System.EventHandler EntriesCleared;

        public event EntryChangedHandler SubEntryAdded;
        public event EntryChangedHandler SubEntryRemoved;
        public event SubEntryChangedHandler SubEntryChanged;
        public event EntryChangedHandler SubItemsCleared;

        public ListViewItemCollection()
        {
            Items = new ArrayList();
        }

        public int IndexOf(object ToFind)
        {
            return Items.IndexOf(ToFind);
        }

        public void Add(ListItem NewItem)
        {
            Items.Add(NewItem);
            NewItem.Changed += new EventHandler(Item_Changed);
            NewItem.SubItemAdded += new EventHandler(NewItem_SubItemAdded);
            NewItem.SubItemChanged += new ListItem.SubItemChangedHandler(NewItem_SubItemChanged);
            NewItem.SubItemRemoved += new EventHandler(NewItem_SubItemRemoved);
            NewItem.SubItemsCleared += new EventHandler(NewItem_SubItemsCleared);

            if (EntryAdded != null)
                EntryAdded(this, EventArgs.Empty);
        }

        private void Item_Changed(object sender, EventArgs e)
        {
            if (EntryChanged != null)
                EntryChanged(this, Items.IndexOf(sender));
        }

        private void NewItem_SubItemsCleared(object sender, EventArgs e)
        {
            if (SubItemsCleared != null)
                SubItemsCleared(this, IndexOf(sender));
        }

        private void NewItem_SubItemRemoved(object sender, EventArgs e)
        {
            if (SubEntryRemoved != null)
                SubEntryRemoved(this, IndexOf(sender));
        }

        private void NewItem_SubItemChanged(object sender, int SubIndex)
        {
            if (SubEntryChanged != null)
                SubEntryChanged(this, IndexOf(sender), SubIndex);
        }

        private void NewItem_SubItemAdded(object sender, EventArgs e)
        {
            if (SubEntryAdded != null)
                SubEntryAdded(this, IndexOf(sender));
        }

        public void Add(string Text)
        {
            ListItem NewItem = new ListItem(Text);

            NewItem.Changed += new EventHandler(Item_Changed);
            NewItem.SubItemAdded += new EventHandler(NewItem_SubItemAdded);
            NewItem.SubItemChanged += new ListItem.SubItemChangedHandler(NewItem_SubItemChanged);
            NewItem.SubItemRemoved += new EventHandler(NewItem_SubItemRemoved);
            NewItem.SubItemsCleared += new EventHandler(NewItem_SubItemsCleared);

            Items.Add(NewItem);

            if (EntryAdded != null)
                EntryAdded(this, EventArgs.Empty);
        }

        public void RemoveAt(int Index)
        {
            //return;
            Items.RemoveAt(Index);

            if (EntryRemoved != null)
                EntryRemoved(this, EventArgs.Empty);
        }

        public ListItem this[int Index]
        {
            get { return (ListItem)Items[Index]; }
            set
            {
                Items[Index] = value;
                ((ListItem)Items[Index]).Changed += new EventHandler(Item_Changed);
                ((ListItem)Items[Index]).SubItemAdded += new EventHandler(NewItem_SubItemAdded);
                ((ListItem)Items[Index]).SubItemChanged += new ListItem.SubItemChangedHandler(NewItem_SubItemChanged);
                ((ListItem)Items[Index]).SubItemRemoved += new EventHandler(NewItem_SubItemRemoved);
                ((ListItem)Items[Index]).SubItemsCleared += new EventHandler(NewItem_SubItemsCleared);
                    
                if (EntryChanged != null)
                    EntryChanged(this, Index);
            }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public void Clear()
        {
            Items.Clear();

            if (EntriesCleared != null)
                EntriesCleared(this, EventArgs.Empty);
        }
    }

    public class ListSubItem : ListComponent
    {
        private string pText;
        private ContentAlignment pTextAlign;
        private int pWidth;

        public event System.EventHandler Changed;

        public ListSubItem()
        {
            pText = "";
            pTextAlign = ContentAlignment.MiddleCenter;
            pWidth = 0;
        }

        public ListSubItem(string InitText)
        {
            pText = InitText;
            pTextAlign = ContentAlignment.MiddleCenter;
            pWidth = 0;
        }

        public ListSubItem(string InitText, ContentAlignment InitTextAlign)
        {
            pText = InitText;
            pTextAlign = InitTextAlign;
            pWidth = 0;
        }

        public string Text
        {
            get { return pText; }
            set
            {
                pText = value;
                RaiseItemChanged();
            }
        }

        public ContentAlignment TextAlign
        {
            get { return pTextAlign; }
            set
            {
                pTextAlign = value;
                RaiseItemChanged();
            }
        }

        public int Width
        {
            get { return pWidth; }
            set
            {
                pWidth = value;
                RaiseItemChanged();
            }
        }

        private void RaiseItemChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }

    public class ListSubItemCollection
    {
        private ArrayList Items;

        public delegate void EntryChangedHandler(object sender, int EntryIndex);

        public event System.EventHandler EntryAdded;
        public event System.EventHandler EntryRemoved;
        public event EntryChangedHandler EntryChanged;
        public event System.EventHandler EntriesCleared;

        public ListSubItemCollection()
        {
            Items = new ArrayList();
        }

        public int IndexOf(object ToFind)
        {
            return Items.IndexOf(ToFind);
        }

        private void Item_Changed(object sender, EventArgs e)
        {
            if (EntryChanged != null)
                EntryChanged(this, Items.IndexOf(sender));
        }

        public void Add(ListSubItem NewItem)
        {
            Items.Add(NewItem);
            NewItem.Changed += new EventHandler(Item_Changed);

            if (EntryAdded != null)
                EntryAdded(this, EventArgs.Empty);
        }

        public void RemoveAt(int Index)
        {
            Items.RemoveAt(Index);

            if (EntryRemoved != null)
                EntryRemoved(this, EventArgs.Empty);
        }

        public ListSubItem this[int Index]
        {
            get { return (ListSubItem)Items[Index]; }
            set
            {
                Items[Index] = value;
                ((ListSubItem)Items[Index]).Changed += new EventHandler(Item_Changed);

                if (EntryChanged != null)
                    EntryChanged(this, Index);
            }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public void Clear()
        {
            Items.Clear();

            if (EntriesCleared != null)
                EntriesCleared(this, EventArgs.Empty);
        }
    }

    public class DoubleQueue<E>
    {
        private LinkedList<E> Items;

        public DoubleQueue()
        {
            Items = new LinkedList<E>();
        }

        public E this[int Index]
        {
            get { return Items.ElementAt(Index); }
        }

        public E PeekFront()
        {
            return Items.First.Value;
        }

        public E PeekBack()
        {
            return Items.Last.Value;
        }

        public void EnqueueFront(E NewItem)
        {
            Items.AddFirst(NewItem);
        }

        public E DequeueFront()
        {
            E Item = Items.First.Value;
            Items.RemoveFirst();

            return Item;
        }

        public void EnqueueBack(E NewItem)
        {
            Items.AddLast(NewItem);
        }

        public E DequeueBack()
        {
            E Item = Items.Last.Value;
            Items.RemoveLast();

            return Item;
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public LinkedList<E>.Enumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }

    public class ListHeaderClass : ListComponent
    {
        private string pText;
        private int pWidth;
        private ContentAlignment pTextAlign;

        public event System.EventHandler HeaderChanged;

        public ListHeaderClass()
        {
            pText = "";
            pWidth = 0;
            pTextAlign = ContentAlignment.MiddleCenter;
        }

        public ListHeaderClass(string InitText)
        {
            pText = InitText;
            pWidth = 0;
            pTextAlign = ContentAlignment.MiddleCenter;
        }

        public ListHeaderClass(string InitText, ContentAlignment InitTextAlign)
        {
            pText = InitText;
            pTextAlign = InitTextAlign;
            pWidth = 0;
        }

        public ListHeaderClass(string InitText, ContentAlignment InitTextAlign, int InitWidth)
        {
            pText = InitText;
            pTextAlign = InitTextAlign;
            pWidth = InitWidth;
        }

        public string Text
        {
            get { return pText; }
            set
            {
                pText = value;
                RaiseHeaderChanged();
            }
        }

        public int Width
        {
            get { return pWidth; }
            set
            {
                pWidth = value;
                RaiseHeaderChanged();
            }
        }

        public ContentAlignment TextAlign
        {
            get { return pTextAlign; }
            set
            {
                pTextAlign = value;
                RaiseHeaderChanged();
            }
        }

        private void RaiseHeaderChanged()
        {
            if (HeaderChanged != null)
                HeaderChanged(this, EventArgs.Empty);
        }
    }

    public class ListHeaderCollection
    {
        private ArrayList Items;

        public delegate void EntryChangedHandler(object sender, int Index);
        public event EntryChangedHandler EntryAdded;
        public event EntryChangedHandler EntryRemoved;
        public event EntryChangedHandler EntryChanged;
        public event EntryChangedHandler EntriesCleared;

        public ListHeaderCollection()
        {
            Items = new ArrayList();
        }

        public int IndexOf(object ToFind)
        {
            return Items.IndexOf(ToFind);
        }

        public void Add(ListHeaderClass NewItem)
        {
            Items.Add(NewItem);
            NewItem.HeaderChanged += new EventHandler(Item_HeaderChanged);

            if (EntryAdded != null)
                EntryAdded(this, Items.Count - 1);
        }

        private void Item_HeaderChanged(object sender, EventArgs e)
        {
            if (EntryChanged != null)
                EntryChanged(sender, Items.IndexOf(sender));
        }

        public void RemoveAt(int Index)
        {
            Items.RemoveAt(Index);

            if (EntryRemoved != null)
                EntryRemoved(this, Index);
        }

        public ListHeaderClass this[int Index]
        {
            get { return (ListHeaderClass)Items[Index]; }
            set
            {
                Items[Index] = value;
                ((ListHeaderClass)Items[Index]).HeaderChanged += new EventHandler(Item_HeaderChanged);

                if (EntryChanged != null)
                    EntryChanged(this, Index);
            }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public void Clear()
        {
            Items.Clear();

            if (EntriesCleared != null)
                EntriesCleared(this, -1);
        }
    }
}
