using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing.Text;

namespace WildMouse.SmoothControls
{
    internal partial class ComboPopout : Form
    {
        private ArrayList PopoutItems;
        private StringCollection pItems;
        private int pSelectedIndex;
        private Font pFont;

        private Color BorderColor;
        private Pen BorderPen;

        private int HoverIndex;
        private int FlashCount;

        private const int FLASH_MAX = 1;

        public event System.EventHandler SelectedIndexChanged;
        public event System.EventHandler FinishedDisappearing;

        public ComboPopout()
        {
            InitializeComponent();

            pItems = new StringCollection();
            pSelectedIndex = -1;  //nothing selected

            this.Paint += new PaintEventHandler(ComboPopout_Paint);
            this.Resize += new EventHandler(ComboPopout_Resize);
            this.Deactivate += new EventHandler(ComboPopout_Deactivate);
            this.Activated += new EventHandler(ComboPopout_Activated);

            BorderColor = Color.FromArgb(200, 200, 200);
            BorderPen = new Pen(BorderColor);

            PopoutItems = new ArrayList();

            HoverIndex = 0;
            FlashCount = 0;
        }

        public override Font Font
        {
            get { return pFont; }
            set
            {
                pFont = value;

                for (int i = 0; i < PopoutItems.Count; i ++)
                    ((ComboPopoutItem)PopoutItems[i]).Font = pFont;
            }
        }

        private void ComboPopout_Activated(object sender, EventArgs e)
        {
            BrightenTmr.Enabled = true;
        }

        private void ComboPopout_Deactivate(object sender, EventArgs e)
        {
            FadeTmr.Enabled = true;
        }

        private void ComboPopout_Resize(object sender, EventArgs e)
        {
            this.Height = (pItems.Count * ComboPopoutItem.CONTROL_HEIGHT) + 2;
            UpdateLayout();
        }

        private void ComboPopout_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        public StringCollection Items
        {
            get { return pItems; }
            set
            {
                pItems = value;
                UpdateLayout();
            }
        }

        public int SelectedIndex
        {
            get { return pSelectedIndex; }
            set
            {
                pSelectedIndex = value;
                UpdateLayout();
            }
        }

        public void UpdateLayout()
        {
            this.Height = (pItems.Count * ComboPopoutItem.CONTROL_HEIGHT) + 2;

            ComboPopoutItem CurItem;
            int X;

            if (pItems.Count > PopoutItems.Count)
            {
                int Count = pItems.Count - PopoutItems.Count;

                for (int i = 0; i < Count; i ++)
                {
                    CurItem = new ComboPopoutItem();
                    CurItem.MouseMove += new MouseEventHandler(PopupItem_MouseMove);
                    CurItem.Click += new EventHandler(PopupItem_Click);
                    CurItem.Visible = true;
                    PopoutItems.Add(CurItem);
                    this.Controls.Add(CurItem);
                }
            }

            if (pItems.Count < PopoutItems.Count)
            {
                for (int i = 0; i < PopoutItems.Count - pItems.Count; i ++)
                    PopoutItems.RemoveAt(0);
            }

            X = 1;

            for (int i = 0; i < pItems.Count; i ++)
            {
                CurItem = (ComboPopoutItem)PopoutItems[i];

                CurItem.Text = pItems[i];
                CurItem.Top = X;
                CurItem.Left = 1;
                CurItem.Width = this.Width - 2;

                if (i == pSelectedIndex)
                    CurItem.Checked = true;
                else
                    CurItem.Checked = false;

                X += CurItem.Height;
            }
        }

        private void PopupItem_Click(object sender, EventArgs e)
        {
            FlashCount = 0;
            FlashTmr.Enabled = true;
            pSelectedIndex = HoverIndex;

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void PopupItem_MouseMove(object sender, MouseEventArgs e)
        {
            ((ComboPopoutItem)PopoutItems[HoverIndex]).Selected = false;
            HoverIndex = PopoutItems.IndexOf(sender);
            ((ComboPopoutItem)PopoutItems[HoverIndex]).Selected = true;
        }

        private void FadeTmr_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;

            if (this.Opacity == 0)
            {
                FadeTmr.Enabled = false;

                if (FinishedDisappearing != null)
                    FinishedDisappearing(this, EventArgs.Empty);
            }
        }

        private void BrightenTmr_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;

            if (this.Opacity == 1)
                BrightenTmr.Enabled = false;
        }

        private void FlashTmr_Tick(object sender, EventArgs e)
        {
            if (FlashCount <= FLASH_MAX)
            {
                ((ComboPopoutItem)PopoutItems[HoverIndex]).Selected = !((ComboPopoutItem)PopoutItems[HoverIndex]).Selected;
                FlashCount++;
            }
            else
            {
                FlashCount = 0;
                FlashTmr.Enabled = false;
                FadeTmr.Enabled = true;
            }
        }
    }
}
