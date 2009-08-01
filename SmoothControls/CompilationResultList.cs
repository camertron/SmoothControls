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
    public partial class CompilationResultList : SequentialListBox
    {
        public CompilationResultList() : base()
        {
            InitializeComponent();

            RowColor1 = Color.FromArgb(234, 255, 231);
            RowColor2 = Color.FromArgb(224, 231, 254);
            RowSelectedColor = Color.FromArgb(255, 204, 204);
        }

        public void AddRow(CompilationResultRow crrNewRow)
        {
            base.RowControls.Add(crrNewRow);
            crrNewRow.ExpandedChanged += new EventHandler(Row_ExpandedChanged);
            base.UpdateLayout();
            base.UpdateScrollBar();
        }

        private void Row_ExpandedChanged(object sender, EventArgs e)
        {
            base.UpdateLayout();
            base.UpdateScrollBar();
            base.OnResize(EventArgs.Empty);
        }

        public void RemoveRow(int iIndex)
        {
            base.RowControls.RemoveAt(iIndex);
            base.UpdateLayout();
        }

        public CompilationResultRow this[int iIndex]
        {
            get { return (CompilationResultRow)base.RowControls[iIndex]; }
            set { base.RowControls[iIndex] = value; base.UpdateLayout(); }
        }

        public int RowCount
        {
            get { return base.RowControls.Count; }
        }

        public void ClearRows()
        {
            base.RowControls.Clear();
            base.UpdateLayout();
        }
    }
}
