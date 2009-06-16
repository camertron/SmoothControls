using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WildMouse.SmoothControls
{
    public partial class AlphaListBox : SequentialListBox
    {
        private const int ALPHABET_COUNT = 26;
        private bool m_bUpdating;

        private AlphaListRowGroup[] m_lrGroups;

        public AlphaListBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            m_lrGroups = new AlphaListRowGroup[ALPHABET_COUNT];

            for (int i = 0; i < m_lrGroups.Length; i++)
                m_lrGroups[i] = null;

            m_bUpdating = false;
        }

        protected override void pRowControls_EntryAdded(object sender, EventArgs e)
        {
            RowEntryAdded();
        }

        protected override void pRowControls_EntryChanged(object sender, int EntryIndex, string OldValue)
        {
        }

        protected override void pRowControls_EntryRemoved(object sender, int RemovedIndex)
        {
        }

        protected override void StringItemRemoved(int iIndex, string sItemText)
        {
            int iGroupIndex = TranslateChar(sItemText[0]);

            for (int i = 0; i < m_lrGroups[iGroupIndex].Items.Count; i++)
            {
                if (m_lrGroups[iGroupIndex].Items[i].Text == sItemText)
                    m_lrGroups[iGroupIndex].Items.RemoveAt(i);
            }
        }

        protected override void StringItemChanged(int iChangedIndex)
        {
        }

        protected override void StringItemAdded(string sNewStr)
        {
            SimpleListRow slrNewRow = new SimpleListRow();

            slrNewRow.Text = sNewStr;
            pRowControls.Add(slrNewRow);
        }

        protected override void RowEntryAdded()
        {
            if (! m_bUpdating)
            {
                Control CurCtrl = ControlAtIndex(base.pRowControls.Count - 1);

                if (CurCtrl.GetType() != typeof(AlphaListRow))
                {
                    PlaceControl(CurCtrl);
                    base.HookUpRow(CurCtrl);
                }

                UpdateLayout();
            }
        }

        private void PlaceControl(Control ctrlToPlace)
        {
            if (ctrlToPlace.Text != "")
            {
                int iIndex = TranslateChar(ctrlToPlace.Text[0]);
                AlphaListRowGroup lrgCurGroup;

                if (m_lrGroups[iIndex] == null)
                {
                    lrgCurGroup = new AlphaListRowGroup(TranslateIndex(iIndex).ToString());
                    m_lrGroups[iIndex] = lrgCurGroup;
                }
                else
                    lrgCurGroup = m_lrGroups[iIndex];

                lrgCurGroup.Items.Add((IListRow)ctrlToPlace);
            }
        }

        protected override void RowEntryChanged(int ChangedIndex, string OldValue)
        {
            if (! m_bUpdating)
            {
                IListRow lrCurRow = (IListRow)ControlAtIndex(ChangedIndex);
                int iIndex = TranslateChar(lrCurRow.Text[0]);

                //change only if text has changed
                if (!m_lrGroups[iIndex].Items.Contains(lrCurRow))
                {
                    m_lrGroups[TranslateChar(OldValue[0])].Items.Remove(lrCurRow);
                    PlaceControl((Control)lrCurRow);
                }

                base.RowEntryChanged(ChangedIndex, OldValue);
            }
        }

        private Control ControlAtIndex(int iIndex)
        {
            return (Control)base.pRowControls[iIndex];
        }

        private int TranslateChar(char cToTrans)
        {
            cToTrans = cToTrans.ToString().ToUpper()[0];
            return ((int)cToTrans) - 65;
        }

        private char TranslateIndex(int iIndex)
        {
            return (char)(iIndex + 65);
        }

        protected override void UpdateLayout()
        {
            m_bUpdating = true;

            base.pRowControls.Clear();

            for (int i = 0; i < m_lrGroups.Length; i++)
            {
                //sort
                if (m_lrGroups[i] != null)
                {
                    if (m_lrGroups[i].Items.Count > 0)
                    {
                        m_lrGroups[i].SortGroup();
                        base.pRowControls.Add(m_lrGroups[i].AlphaRow);

                        //add controls to base list
                        for (int j = 0; j < m_lrGroups[i].Items.Count; j++)
                            base.pRowControls.Add(m_lrGroups[i].Items[j]);
                    }
                    else
                    {
                        if (this.Controls.Contains(m_lrGroups[i].AlphaRow))
                            this.Controls.Remove(m_lrGroups[i].AlphaRow);
                    }
                }
            }

            base.UpdateLayout();
            base.UpdateScrollBar();
            m_bUpdating = false;
        }

        protected override Color GetRowColor(int Index)
        {
            if (m_bUseRowColoring)
            {
                int iGroup = TranslateChar(ControlAtIndex(Index).Text[0]);
                int iPos = m_lrGroups[iGroup].Items.IndexOf((IListRow)ControlAtIndex(Index));

                if (Index == pSelectedIndex)
                    return RowSelectedColor;
                else if ((iPos % 2) == 0)
                    return RowColor2;
                else
                    return RowColor1;
            }
            else
                return this.BackColor;
        }
    }

    public class AlphaListRowGroup
    {
        private ListRowCollection lrcItems;
        private string sKey;
        private AlphaListRow m_arRow;

        public AlphaListRowGroup()
        {
            Construct(new ListRowCollection(), "");
        }

        public AlphaListRowGroup(string sInitKey)
        {
            Construct(new ListRowCollection(), sInitKey);
        }

        public AlphaListRowGroup(ListRowCollection lrColl, string sInitKey)
        {
            Construct(lrColl, sInitKey);
        }

        private void Construct(ListRowCollection lrcInitColl, string sInitKey)
        {
            lrcItems = lrcInitColl;
            sKey = sInitKey;
            m_arRow = new AlphaListRow();
            m_arRow.Text = sInitKey;
        }

        public ListRowCollection Items
        {
            get { return lrcItems; }
        }

        public string Key
        {
            get { return sKey; }
            set { sKey = value; m_arRow.Text = value; }
        }

        public void SortGroup()
        {
            lrcItems.Sort();
        }

        public AlphaListRow AlphaRow
        {
            get { return m_arRow; }
        }
    }
}
