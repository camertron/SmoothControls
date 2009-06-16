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
    public partial class ListRow : UserControl, IListRow
    {
        public static int CONTROL_HEIGHT = 17;

        private ListItem pListInfo;
        private ArrayList DisplayLabels;
        private Color SeparatorColor;
        private Pen SeparatorPen;
        private Color pTextColor;

        public new event System.EventHandler OnClick;
        public event CmdKeyPressedHandler CmdKeyPressed;

        public ListRow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            pListInfo = null;
            DisplayLabels = new ArrayList();

            this.Paint += new PaintEventHandler(ListViewRow_Paint);
            this.Resize += new System.EventHandler(ListViewRow_Resize);

            SeparatorColor = Color.FromArgb(100, 100, 100);
            SeparatorPen = new Pen(SeparatorColor);

            this.Click += new System.EventHandler(ListViewRow_Click);

            pTextColor = Color.Black;
        }

        //this property should be tied to the background color of the usercontrol,
        //but currently ListView sets that when necessary
        public bool Selected
        {
            get { return false; }
            set { }
        }

        public int ControlHeight
        {
            get { return CONTROL_HEIGHT; }
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        public Color TextColor
        {
            get { return pTextColor; }
            set
            {
                pTextColor = value;
                InvalidateParent();
            }
        }

        private void ListViewRow_Click(object sender, System.EventArgs e)
        {
            if (OnClick != null)
                OnClick(this, System.EventArgs.Empty);
        }

        private void DisplayLbl_Click(object sender, System.EventArgs e)
        {
            if (OnClick != null)
                OnClick(this, System.EventArgs.Empty);

            this.Focus();
        }

        private void ListViewRow_Resize(object sender, System.EventArgs e)
        {
            this.Height = CONTROL_HEIGHT;
        }

        private void ListViewRow_Paint(object sender, PaintEventArgs e)
        {
            UpdateLayout();

            if (pListInfo == null)
                return;

            for (int i = 0; i <= pListInfo.SubItems.Count; i ++)
                e.Graphics.DrawLine(SeparatorPen, ((SmoothLabel)DisplayLabels[i]).Right + 1, 0, ((SmoothLabel)DisplayLabels[i]).Right + 1, this.Height);
        }

        public ListItem ListInfo
        {
            get { return pListInfo; }
            set
            {
                pListInfo = value;
                this.Invalidate();
            }
        }

        public void UpdateLayout()
        {
            if (pListInfo == null)
            {
                for (int i = 0; i < DisplayLabels.Count; i ++)
                    ((SmoothLabel)DisplayLabels[i]).Text = "";

                return;
            }

            SmoothLabel CurLbl;
            ListComponent CurComponent;
            int CurLeft = 1;

            if (DisplayLabels.Count > (pListInfo.SubItems.Count + 1))
            { 
                for (int i = 0; i <= DisplayLabels.Count - (pListInfo.SubItems.Count + 1); i ++)
                {
                    this.Controls.RemoveAt(0);
                    DisplayLabels.RemoveAt(0);
                }
            }
            else if (DisplayLabels.Count < (pListInfo.SubItems.Count + 1))
            {
                int LabelCount = DisplayLabels.Count;

                for (int i = 0; i <= (pListInfo.SubItems.Count) - LabelCount; i ++)
                {
                    CurLbl = new SmoothLabel();
                    CurLbl.AutoSize = false;
                    this.Controls.Add(CurLbl);
                    DisplayLabels.Add(CurLbl);
                }
            }

            for (int i = 0; i < pListInfo.SubItems.Count + 1; i ++)
            {
                if (i == 0)
                    CurComponent = pListInfo;
                else
                    CurComponent = pListInfo.SubItems[i - 1];

                CurLbl = (SmoothLabel)DisplayLabels[i];
                CurLbl.ForeColor = pTextColor;
                CurLbl.Click += new System.EventHandler(DisplayLbl_Click);
                CurLbl.Text = CurComponent.Text;
                CurLbl.TextAlign = CurComponent.TextAlign;
                CurLbl.Height = CONTROL_HEIGHT;
                CurLbl.Width = CurComponent.Width - 1;
                CurLbl.Top = -1;
                CurLbl.Left = CurLeft + 1;

                CurLeft += CurComponent.Width + 1;
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, Keys keyData)
        {
            if (CmdKeyPressed != null)
                CmdKeyPressed(this, keyData);

            return true;
        }
    }
}
