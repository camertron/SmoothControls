using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WildMouse.SmoothControls
{
    public partial class CompilationResultRow : UserControl, IListRow
    {
        private const int C_COLLAPSED_HEIGHT = 22;
        private const int C_PADDING = 10;
        private const int C_LINE_SPACING = 12;
        private const int C_INSTR_MARGIN = 23;

        private Point[] m_aptTriangleExpanded;
        private Point[] m_aptTriangleCollapsed;
        private int m_iTriangleSize = 10;
        private bool m_bExpanded;
        private List<CodeItem> m_lcItems;
        private SolidBrush m_bshTextBrush;
        private Pen m_pnDashPen;
        private bool m_bSelected;

        public event EventHandler ExpandedChanged;
        public event CmdKeyPressedHandler CmdKeyPressed;

        public CompilationResultRow()
        {
            InitializeComponent();

            Point ptFirst = new Point(2, 0);
            Point ptSecond = new Point(2, m_iTriangleSize);
            Point ptThird = new Point((m_iTriangleSize / 2) + 2, m_iTriangleSize / 2);
            m_aptTriangleCollapsed = new Point[3] { ptFirst, ptSecond, ptThird };

            ptFirst = new Point(0, 2);
            ptSecond = new Point(m_iTriangleSize, 2);
            ptThird = new Point((m_iTriangleSize / 2), (m_iTriangleSize / 2) + 2);
            m_aptTriangleExpanded = new Point[3] { ptFirst, ptSecond, ptThird };

            m_bExpanded = false;
            m_bshTextBrush = new SolidBrush(Color.Black);

            m_lcItems = new List<CodeItem>();

            this.Paint += new PaintEventHandler(CompilationResultRow_Paint);
            this.Resize += new EventHandler(CompilationResultRow_Resize);
            pbExpandArrow.Paint += new PaintEventHandler(pbExpandArrow_Paint);

            m_pnDashPen = new Pen(Color.FromArgb(185, 185, 185));
            m_pnDashPen.DashPattern = new float[2] { 5, 5 };
        }

        public string ProcName
        {
            get { return lblProcName.Text; }
            set { lblProcName.Text = value; }
        }

        public bool Selected
        {
            get { return m_bSelected; }
            set { m_bSelected = value; }
        }

        public int ControlHeight
        {
            get
            {
                if (m_bExpanded)
                {
                    int iHeight = (C_LINE_SPACING * m_lcItems.Count) + C_COLLAPSED_HEIGHT;

                    if (m_lcItems.Count > 0)
                        iHeight += C_PADDING;

                    return iHeight;
                }
                else
                    return C_COLLAPSED_HEIGHT;
            }
        }

        public void AddItem(CodeItem ciNewItem)
        {
            m_lcItems.Add(ciNewItem);
            this.Invalidate();
            CompilationResultRow_Resize(this, EventArgs.Empty);
        }

        public void RemoveItem(int iIndex)
        {
            m_lcItems.RemoveAt(iIndex);
            this.Invalidate();
            CompilationResultRow_Resize(this, EventArgs.Empty);
        }

        public CodeItem this[int iIindex]
        {
            get { return m_lcItems[iIindex]; }
            set { m_lcItems[iIindex] = value; this.Invalidate(); }
        }

        public int Count
        {
            get { return m_lcItems.Count; }
        }

        public void ClearItems()
        {
            m_lcItems.Clear();
        }

        public bool Expanded
        {
            get { return m_bExpanded; }
            set
            {
                m_bExpanded = value;
                OnExpandedChanged();
            }
        }

        private void OnExpandedChanged()
        {
            this.CompilationResultRow_Resize(this, EventArgs.Empty);
            this.Invalidate();

            if (ExpandedChanged != null)
                ExpandedChanged(this, EventArgs.Empty);
        }

        private void pbExpandArrow_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gpTriangle = new GraphicsPath();
            Point[] aptTriangle;

            if (m_bExpanded)
                aptTriangle = m_aptTriangleExpanded;
            else
                aptTriangle = m_aptTriangleCollapsed;

            gpTriangle.AddLine(aptTriangle[0], aptTriangle[1]);
            gpTriangle.AddLine(aptTriangle[1], aptTriangle[2]);
            gpTriangle.AddLine(aptTriangle[2], aptTriangle[0]);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(new SolidBrush(Color.Black), gpTriangle);
        }

        private void CompilationResultRow_Paint(object sender, PaintEventArgs e)
        {
            Font fntCourier = new Font("Courier New", 10);
            string sCurEntry;
            int iCurY = C_COLLAPSED_HEIGHT;

            for (int i = 0; i < m_lcItems.Count; i++)
            {
                sCurEntry = m_lcItems[i].Address + "  " + m_lcItems[i].InstructionCode + "  " + m_lcItems[i].CodeSnippet;
                e.Graphics.DrawString(sCurEntry, fntCourier, m_bshTextBrush, C_INSTR_MARGIN, iCurY);

                iCurY += C_LINE_SPACING;
            }

            CompilationResultRow_Resize(this, EventArgs.Empty);
            e.Graphics.DrawLine(m_pnDashPen, 0, this.Height - 1, this.Width, this.Height - 1);
        }

        private void CompilationResultRow_Resize(object sender, EventArgs e)
        {
            lblProcName.Width = this.Width - lblProcName.Left;
            this.Height = ControlHeight;
        }

        private void pbExpandArrow_Click(object sender, EventArgs e)
        {
            m_bExpanded = ! m_bExpanded;
            pbExpandArrow.Invalidate();
            OnExpandedChanged();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, Keys keyData)
        {
            if (CmdKeyPressed != null)
                CmdKeyPressed(this, keyData);

            return true;
        }
    }

    public class CodeItem
    {
        private string m_sInstructionCode;
        private string m_sAddress;
        private string m_sCodeSnippet;

        public string CodeSnippet
        {
            get { return m_sCodeSnippet; }
            set { m_sCodeSnippet = value; }
        }

        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        public string InstructionCode
        {
            get { return m_sInstructionCode; }
            set { m_sInstructionCode = value; }
        }
    }
}
