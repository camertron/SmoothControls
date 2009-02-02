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
    [DefaultEvent("Scroll")]
    public partial class VerticalScrollBar : UserControl
    {
        private const int PADDING = 18;
        private const int CONTROL_WIDTH = 17;
        private const int MIN_HANDLE_SIZE = 20;

        private int pValue;
        private int pMax;
        private int pMin;
        private int pLargeChange;
        private int pSmallChange;

        private bool ScrollingUp;
        private int ElapsedWaitTime;
        private const int WAIT_TIME = 300;

        public new event ScrollEventHandler Scroll;

        public VerticalScrollBar()
        {
            InitializeComponent();

            Handle.MouseMove += new MouseEventHandler(Handle_MouseMove);

            this.Resize += new EventHandler(VerticalScrollBar_Resize);
            UpButton.MouseDown += new MouseEventHandler(UpButton_MouseDown);
            UpButton.MouseUp += new MouseEventHandler(UpButton_MouseUp);
            DownButton.MouseDown += new MouseEventHandler(DownButton_MouseDown);
            DownButton.MouseUp += new MouseEventHandler(DownButton_MouseUp);
            DownButton.Click += new EventHandler(DownButton_Click);
            UpButton.Click += new EventHandler(UpButton_Click);

            pMax = 100;
            pMin = 0;
            pLargeChange = 50;
            pSmallChange = 1;
            pValue = 0;

            ScrollingUp = false;
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            ScrollTimer.Enabled = false;
            ElapsedWaitTime = WAIT_TIME;
            ScrollTimer_Tick(this, EventArgs.Empty);
            ElapsedWaitTime = 0;
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            ScrollTimer.Enabled = false;
            ElapsedWaitTime = WAIT_TIME;
            ScrollTimer_Tick(this, EventArgs.Empty);
            ElapsedWaitTime = 0;
        }

        private void DownButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ElapsedWaitTime = 0;
                ScrollingUp = false;
                ScrollTimer.Enabled = false;
            }
        }

        private void DownButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ElapsedWaitTime = 0;
                ScrollingUp = false;
                ScrollTimer.Enabled = true;
            }
        }

        private void UpButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ElapsedWaitTime = 0;
                ScrollTimer.Enabled = false;
            }
        }

        private void UpButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ElapsedWaitTime = 0;
                ScrollingUp = true;
                ScrollTimer.Enabled = true;
            }
        }

        private void VerticalScrollBar_Resize(object sender, EventArgs e)
        {
            this.Width = CONTROL_WIDTH;
            BackPic.Height = this.Height;
            DownButton.Top = this.Height - DownButton.Height;

            RepositionHandle(pValue);
        }

        private void RepositionHandle(int SetValue)
        {
            float Space = (float)(this.Height - (PADDING * 2));
            float Distance = (float)pMax - (float)pMin;

            float Percent;

            if ((pLargeChange == 0) || (Distance == 0))
            {
                Percent = 1.0f;
                Handle.Height = (int)Space;
            }
            else
            {
                int HandleHeight = (int)(((float)pLargeChange / Distance) * Space);

                if (HandleHeight < MIN_HANDLE_SIZE)
                    Handle.Height = MIN_HANDLE_SIZE;
                else
                    Handle.Height = HandleHeight;

                Percent = ((float)SetValue - (float)pMin) / Distance;
            }
            
            Handle.Top = (int)((Space - Handle.Height) * Percent) + PADDING;

            this.Invalidate();
        }

        private void InvalidateParent()
        {
            if (this.Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(rc, true);
        }

        private void RecalculateValue()
        {
            float Space = (float)(this.Height - ((PADDING * 2) + Handle.Height));
            float Distance = (float)pMax - (float)pMin;

            if (Space == 0)
                pValue = 0;
            else
                pValue = (int)(((Handle.Top - PADDING) / Space) * Distance);
        }

        private void Handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point Pos = this.PointToClient(Cursor.Position);
                ScrollEventArgs Args = null;
                int OldValue = pValue;

                if ((Pos.Y - (Handle.Height / 2)) <= PADDING)
                {
                    Handle.Top = PADDING;
                    RecalculateValue();
                    Args = new ScrollEventArgs(ScrollEventType.First, OldValue, pMin, ScrollOrientation.VerticalScroll);
                }
                else if ((Pos.Y + (Handle.Height / 2)) >= (this.Height - PADDING))
                {
                    Handle.Top = this.Height - (Handle.Height + PADDING);
                    RecalculateValue();
                    Args = new ScrollEventArgs(ScrollEventType.Last, OldValue, pMax, ScrollOrientation.VerticalScroll);
                }
                else
                {
                    Handle.Top = Pos.Y - (Handle.Height / 2);
                    RecalculateValue();

                    if (OldValue > pValue)
                        Args = new ScrollEventArgs(ScrollEventType.SmallDecrement, OldValue, pValue, ScrollOrientation.VerticalScroll);
                    else
                        Args = new ScrollEventArgs(ScrollEventType.SmallIncrement, OldValue, pValue, ScrollOrientation.VerticalScroll);
                }

                if (Scroll != null)
                    Scroll(this, Args);

                this.Invalidate();
            }
        }

        public int Value
        {
            get { return pValue; }
            set
            {
                if (value > pMax)
                    pValue = pMax;
                else if (value < pMin)
                    pValue = pMin;
                else
                    pValue = value;

                RepositionHandle(pValue);
            }
        }

        public int Maximum
        {
            get { return pMax; }
            set
            {
                if (pLargeChange > value)
                    pLargeChange = value;
                else if (value < pMin)
                {
                    pMin = value;
                    pMax = value;
                }
                else
                    pMax = value;

                RepositionHandle(pValue);
            }
        }

        public int Minimum
        {
            get { return pMin; }
            set
            {
                if (value > pMax)
                    pMax = value;

                pMin = value;

                RepositionHandle(pValue);
            }
        }

        public int LargeChange
        {
            get { return pLargeChange; }
            set
            {
                if (value > pMax)
                    pLargeChange = pMax;
                else
                    pLargeChange = value;

                RepositionHandle(pValue);
            }
        }

        public int SmallChange
        {
            get { return pSmallChange; }
            set
            {
                pSmallChange = value;
                RepositionHandle(pValue);
            }
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            int NewValue;
            int OldValue;

            ElapsedWaitTime += ScrollTimer.Interval;

            if (ElapsedWaitTime >= WAIT_TIME)
            {
                OldValue = pValue;

                if (ScrollingUp)
                    NewValue = pValue - pSmallChange;
                else
                    NewValue = pValue + pSmallChange;

                if (NewValue > pMax)
                {
                    NewValue = pMax;
                    ScrollTimer.Enabled = false;
                }
                else if (NewValue < pMin)
                {
                    NewValue = pMin;
                    ScrollTimer.Enabled = false;
                }

                RepositionHandle(NewValue);

                pValue = NewValue;

                if (Scroll != null)
                    Scroll(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, OldValue, NewValue, ScrollOrientation.VerticalScroll));
            }
        }
    }
}
