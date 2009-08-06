using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    public partial class SmoothTable : UserControl
    {
        private const int C_DEFAULT_FONT_SIZE = 10;
        private const int C_DEFAULT_COLUMN_WIDTH = 50;
        private const int C_DEFAULT_COLUMN_HEIGHT = 20;
        private const int C_DEFAULT_ROW_HEIGHT = 20;
        private const int C_DEFAULT_ROW_WIDTH = 50;

        private List<TableRowLabel> m_lsRowLabels;
        private List<TableColumnLabel> m_lsColumnLabels;
        private Dictionary<int, Dictionary<int, string>> m_dRows;
        private int m_iColumnHeight = C_DEFAULT_COLUMN_HEIGHT;
        private int m_iRowWidth = C_DEFAULT_ROW_WIDTH;

        private Pen m_pGridPen;
        private Pen m_pLabelPen;
        private SolidBrush m_bshLabelTextBrush;
        private SolidBrush m_bshValueTextBrush;
        private Font m_fntLabelFont;
        private Font m_fntValueFont;
        private Color m_cGradientStart;
        private Color m_cGradientFinish;
        private int m_iTotalWidth = 100; //the width of the drawn table (not the width of the entire usercontrol)
        private int m_iTotalHeight = 100; //the height of the drawn table

        public SmoothTable()
        {
            InitializeComponent();

            m_dRows = new Dictionary<int, Dictionary<int, string>>();
            m_lsRowLabels = new List<TableRowLabel>();
            m_lsColumnLabels = new List<TableColumnLabel>();

            m_pGridPen = new Pen(Color.FromArgb(180, 180, 180));
            m_pLabelPen = new Pen(Color.Black);
            m_bshLabelTextBrush = new SolidBrush(Color.Black);
            m_bshValueTextBrush = new SolidBrush(Color.Black);
            m_fntLabelFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, C_DEFAULT_FONT_SIZE);
            m_fntValueFont = FontVault.GetFontVault().GetFont(FontVault.AvailableFonts.MyriadPro, C_DEFAULT_FONT_SIZE);

            m_cGradientStart = Color.FromArgb(230, 230, 230);
            m_cGradientFinish = Color.FromArgb(180, 180, 180);

            this.Paint += new PaintEventHandler(SmoothTable_Paint);
            this.Resize += new EventHandler(SmoothTable_Resize);
        }

        private void SmoothTable_Resize(object sender, EventArgs e)
        {
            this.Width = m_iTotalWidth + 1;
            this.Height = m_iTotalHeight + 1;
        }

        private void SmoothTable_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Point ptDrawStart = new Point(0, m_iColumnHeight);
            System.Drawing.Point ptDrawFinish = new Point(0, 0);
            System.Drawing.Point ptLabelPoint = new Point();
            System.Drawing.SizeF sfLabelSize;
            Region m_rgOrigRegion = e.Graphics.Clip;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            m_iTotalHeight = 0;
            m_iTotalWidth = 0;

            //draw rows, but not row lines - we don't know how wide the table is yet
            //while drawing, keep track of total height
            //also draw row labels
            for (int i = 0; i < m_lsRowLabels.Count; i ++)
            {
                //draw background gradient of row label
                Gradient.DrawGradient(e.Graphics, Gradient.GradientDirection.Vertical, m_cGradientStart, m_cGradientFinish, m_iRowWidth, m_lsRowLabels[i].Height, ptDrawStart);

                //draw row label
                m_lsRowLabels[i].Region = new Rectangle(ptDrawStart.X, ptDrawStart.Y, m_iRowWidth, m_lsRowLabels[i].Height);
                sfLabelSize = e.Graphics.MeasureString(m_lsRowLabels[i].Text, m_fntLabelFont);
                ptLabelPoint.X = ptDrawStart.X + ((m_lsRowLabels[i].Region.Width / 2) - ((int)sfLabelSize.Width / 2));
                ptLabelPoint.Y = ptDrawStart.Y + ((m_lsRowLabels[i].Region.Height / 2) - ((int)sfLabelSize.Height / 2));

                e.Graphics.Clip = new Region(m_lsRowLabels[i].Region);
                e.Graphics.DrawString(m_lsRowLabels[i].Text, m_fntLabelFont, m_bshLabelTextBrush, ptLabelPoint);
                e.Graphics.Clip = m_rgOrigRegion;

                ptDrawStart.Y += m_lsRowLabels[i].Height;
            }

            //prepare start and end points
            m_iTotalHeight = ptDrawStart.Y;
            ptDrawFinish.Y = m_iTotalHeight;
            ptDrawStart.Y = 0;
            ptDrawStart.X = m_iRowWidth;

            //draw columns and column lines, build total width while you're at it
            for (int i = 0; i < m_lsColumnLabels.Count; i++)
            {
                Gradient.DrawGradient(e.Graphics, Gradient.GradientDirection.Vertical, m_cGradientStart, m_cGradientFinish, m_lsColumnLabels[i].Width, m_iColumnHeight, ptDrawStart);

                //draw column label
                m_lsColumnLabels[i].Region = new Rectangle(ptDrawStart.X, ptDrawStart.Y, m_lsColumnLabels[i].Width, m_iColumnHeight);
                sfLabelSize = e.Graphics.MeasureString(m_lsColumnLabels[i].Text, m_fntLabelFont);
                ptLabelPoint.X = ptDrawStart.X + ((m_lsColumnLabels[i].Region.Width / 2) - ((int)sfLabelSize.Width / 2));
                ptLabelPoint.Y = ptDrawStart.Y + ((m_lsColumnLabels[i].Region.Height / 2) - ((int)sfLabelSize.Height / 2));

                e.Graphics.Clip = new Region(m_lsColumnLabels[i].Region);
                e.Graphics.DrawString(m_lsColumnLabels[i].Text, m_fntLabelFont, m_bshLabelTextBrush, ptLabelPoint);
                e.Graphics.Clip = m_rgOrigRegion;

                //draw grid line
                ptDrawFinish.X = ptDrawStart.X;
                e.Graphics.DrawLine(m_pGridPen, ptDrawStart, ptDrawFinish);
 
                ptDrawStart.X += m_lsColumnLabels[i].Width;
            }

            //prepare start and end points.
            //start point will be all the way to the right of the table,
            //end point will be on the left
            m_iTotalWidth = ptDrawStart.X;
            ptDrawStart.Y = m_iColumnHeight;
            ptDrawFinish.X = 0;
            ptDrawFinish.Y = ptDrawStart.Y;

            //draw final row lines now that total width has been calculated
            for (int i = 0; i < m_lsRowLabels.Count; i ++)
            {
                e.Graphics.DrawLine(m_pGridPen, ptDrawStart, ptDrawFinish);
                ptDrawStart.Y += m_lsRowLabels[i].Height;
                ptDrawFinish.Y = ptDrawStart.Y;
            }

            //final border around everything
            e.Graphics.DrawRectangle(m_pGridPen, 0, 0, m_iTotalWidth, m_iTotalHeight);

            //final resize to match size of drawn table
            this.SmoothTable_Resize(this, EventArgs.Empty);

            Fill(e.Graphics);
        }

        private void Fill(System.Drawing.Graphics gCanvas)
        {
            Region rOrigClip = gCanvas.Clip;
            Dictionary<int, Dictionary<int, string>>.Enumerator edRowEnum = m_dRows.GetEnumerator();
            Dictionary<int, string>.Enumerator edColEnum;
            Rectangle rValueRect;
            string sCurValue;
            SizeF sfStringSize;
            Point ptValuePoint = new Point();

            while (edRowEnum.MoveNext())
            {
                edColEnum = edRowEnum.Current.Value.GetEnumerator();
                
                while (edColEnum.MoveNext())
                {
                    rValueRect = CalcTableRect(edRowEnum.Current.Key, edColEnum.Current.Key);
                    sCurValue = GetValue(edRowEnum.Current.Key, edColEnum.Current.Key);

                    sfStringSize = gCanvas.MeasureString(sCurValue, m_fntValueFont);
                    ptValuePoint.X = rValueRect.X + ((rValueRect.Width / 2) - ((int)sfStringSize.Width / 2));
                    ptValuePoint.Y = rValueRect.Y + ((rValueRect.Height / 2) - ((int)sfStringSize.Height / 2));

                    gCanvas.Clip = new Region(rValueRect);
                    gCanvas.DrawString(sCurValue, m_fntValueFont, m_bshValueTextBrush, ptValuePoint);
                    gCanvas.Clip = rOrigClip;
                }
            }
        }

        private Rectangle CalcTableRect(int iRow, int iCol)
        {
            int iY = m_lsRowLabels[iRow].Region.Top;
            int iHeight = m_lsRowLabels[iRow].Height;
            int iX = m_lsColumnLabels[iCol].Region.Left;
            int iWidth = m_lsColumnLabels[iCol].Width;

            return new Rectangle(iX, iY, iWidth, iHeight);
        }

        private void EnsureExists(int iRow, int iCol)
        {
            if (! m_dRows.ContainsKey(iRow))
                m_dRows.Add(iRow, new Dictionary<int, string>());
            
            if (! m_dRows[iRow].ContainsKey(iCol))
                m_dRows[iRow].Add(iCol, "");
        }

        public string GetValue(int iRow, int iCol)
        {
            EnsureExists(iRow, iCol);
            return m_dRows[iRow][iCol];
        }

        public void SetValue(int iRow, int iCol, string sNewValue)
        {
            EnsureExists(iRow, iCol);
            m_dRows[iRow][iCol] = sNewValue;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public List<TableRowLabel> RowLabels
        {
            get { return m_lsRowLabels; }
            set { m_lsRowLabels = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public List<TableColumnLabel> ColumnLabels
        {
            get { return m_lsColumnLabels; }
            set { m_lsColumnLabels = value; }
        }
       
        public void AddRowLabel(string sLabelText, int iRowHeight)
        {
            m_lsRowLabels.Add(new TableRowLabel(sLabelText, iRowHeight));
        }

        public void AddRowLabel(string sLabelText)
        {
            m_lsRowLabels.Add(new TableRowLabel(sLabelText, C_DEFAULT_ROW_HEIGHT));
        }

        public void AddColumnLabel(string sLabelText, int iColumnWidth)
        {
            m_lsColumnLabels.Add(new TableColumnLabel(sLabelText, iColumnWidth));
        }

        public void AddColumnLabel(string sLabelText)
        {
            m_lsColumnLabels.Add(new TableColumnLabel(sLabelText, C_DEFAULT_COLUMN_WIDTH));
        }
    }

    public class TableRowLabel
    {
        private string m_sText;
        private int m_iHeight;
        private Rectangle m_rRegion;

        public TableRowLabel(string sInitText, int iInitHeight)
        {
            m_sText = sInitText;
            m_iHeight = iInitHeight;
            m_rRegion = new Rectangle();
        }

        public Rectangle Region
        {
            get { return m_rRegion; }
            set { m_rRegion = value; }
        }

        public int Height
        {
            get { return m_iHeight; }
            set { m_iHeight = value; }
        }

        public string Text
        {
            get { return m_sText; }
            set { m_sText = value; }
        }
    }

    public class TableColumnLabel
    {
        private string m_sText;
        private int m_iWidth;
        private Rectangle m_rRegion;

        public TableColumnLabel(string sInitText, int iInitWidth)
        {
            m_sText = sInitText;
            m_iWidth = iInitWidth;
            m_rRegion = new Rectangle();
        }

        public Rectangle Region
        {
            get { return m_rRegion; }
            set { m_rRegion = value; }
        }

        public int Width
        {
            get { return m_iWidth; }
            set { m_iWidth = value; }
        }

        public string Text
        {
            get { return m_sText; }
            set { m_sText = value; }
        }
    }
}
