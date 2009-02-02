using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace WildMouse.SmoothControls
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class ViewChooser : UserControl
    {
        public event System.EventHandler SelectedIndexChanged;

        private StringCollection pTabs;
        private ViewChooserRowCollection pRows;

        private Color BorderColor;
        private Pen BorderPen;

        private int pSelectedIndex;

        public ViewChooser()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            pTabs = new StringCollection();
            pRows = new ViewChooserRowCollection();

            BorderColor = Color.Silver;
            BorderPen = new Pen(BorderColor);

            this.Paint += new PaintEventHandler(ViewChooser_Paint);
            this.Resize += new EventHandler(ViewChooser_Resize);

            UpdateTabs();
        }

        private void ViewChooser_Resize(object sender, EventArgs e)
        {
            int ImageTop;

            if (pRows.Count > 0)
            {
                pRows[0].Top = 0;
                pRows[0].Width = this.Width - 2;
                pRows[0].Left = 1;

                for (int i = 1; i < pRows.Count; i ++)
                {
                    pRows[i].Top = pRows[i - 1].Bottom;
                    pRows[i].Width = this.Width - 2;
                    pRows[i].Left = 1;
                }

                ImageTop = pRows[pRows.Count - 1].Bottom + 5;
            }
            else
                ImageTop = 5;

            ImageBox.Top = ImageTop;
            ImageBox.Left = (this.Width / 2) - (ImageBox.Width / 2);

            this.Invalidate();
        }

        private void ViewChooser_Paint(object sender, PaintEventArgs e)
        {
            if (pRows.Count > 0)
            {
                int Y = pRows[pRows.Count - 1].Bottom;

                UpdateTabs();
                e.Graphics.DrawRectangle(BorderPen, 0, 0, this.Width - 1, this.Height - 1);
                e.Graphics.DrawLine(BorderPen, 0, Y, this.Width - 1, Y);
            }
        }

        private void UpdateTabs()
        {
            ViewChooserRow CurRow;
            int i;

            for (i = 0; i < pTabs.Count; i++)
            {
                if (i >= pRows.Count)
                {
                    CurRow = new ViewChooserRow();
                    CurRow.CtrlClick += new EventHandler(Row_CtrlClick);
                    this.Controls.Add(CurRow);
                    pRows.Add(CurRow);
                }
                else
                    CurRow = pRows[i];

                CurRow.Text = pTabs[i];
                CurRow.Visible = true;
                CurRow.BackColor = this.BackColor;
            }

            for (int j = i; j < pRows.Count; j++)
                pRows.RemoveAt(pRows.Count - 1);
        }

        private void Row_CtrlClick(object sender, EventArgs e)
        {
            UpdateSelected(sender);

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void UpdateSelected(object SelectedControl)
        {
            for (int i = 0; i < pRows.Count; i++)
            {
                if (pRows[i] == SelectedControl)
                {
                    pSelectedIndex = i;                    
                    pRows[i].Selected = true;
                }
                else
                    pRows[i].Selected = false;
            }
        }

        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public StringCollection Tabs
        {
            get { return pTabs; }
            set
            {
                pTabs = value;
                UpdateTabs();
                ViewChooser_Resize(this, EventArgs.Empty);
                UpdateSelected(pRows[pSelectedIndex]);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public int SelectedIndex
        {
            get { return pSelectedIndex; }
            set
            {
                pSelectedIndex = value;

                if (value < pTabs.Count)
                    UpdateSelected(pTabs[value]);

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, EventArgs.Empty);
            }
        }
    }
}