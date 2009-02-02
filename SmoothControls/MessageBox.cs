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
    public partial class MessageBox : UserControl
    {
        private SolidBrush BackgroundBrush;

        public MessageBox()
        {
            InitializeComponent();

            BackgroundBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
            this.Paint += new PaintEventHandler(MessageBox_Paint);
            this.Resize += new EventHandler(MessageBox_Resize);
            this.Move += new EventHandler(MessageBox_Move);
        }

        public MessageBoxButtons Buttons
        {
            get { return MessageCtrl.Buttons; }
            set
            {
                MessageCtrl.Buttons = value;
            }
        }

        public MessageBoxIcon Icon
        {
            get { return MessageCtrl.Icon; }
            set
            {
                MessageCtrl.Icon = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return MessageCtrl.Text; }
            set
            {
                MessageCtrl.Text = value;
            }
        }

        public string Title
        {
            get { return MessageCtrl.Title; }
            set
            {
                MessageCtrl.Title = value;
            }
        }

        private void AdjustDimensions()
        {
            this.Top = 0;
            this.Left = 0;

            if (this.Parent != null)
                this.Size = this.Parent.ClientSize;
            else if (this.ParentForm != null)
                this.Size = this.ParentForm.ClientSize;
        }

        private void MessageBox_Move(object sender, EventArgs e)
        {
            AdjustDimensions();
        }

        private void MessageBox_Resize(object sender, EventArgs e)
        {
            AdjustDimensions();

            MessageCtrl.Top = (this.Height / 2) - (MessageCtrl.Height / 2);
            MessageCtrl.Left = (this.Width / 2) - (MessageCtrl.Width / 2);

            System.Drawing.Drawing2D.GraphicsPath FormRegion = new System.Drawing.Drawing2D.GraphicsPath();
            FormRegion.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            this.Region = new System.Drawing.Region(FormRegion);
        }

        private void MessageBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(BackgroundBrush, 0, 0, this.Width, this.Height);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do nothing!
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //turn the form transparent - sweet, eh?
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        public static void Show(Control Owner, string Text)
        {
            Show(Owner, Text, Owner.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static void Show(Control Owner, string Text, string Title)
        {
            Show(Owner, Text, Title, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static void Show(Control Owner, string Text, string Title, MessageBoxButtons Buttons)
        {
            Show(Owner, Text, Title, Buttons, MessageBoxIcon.None);
        }

        public static void Show(Control Owner, string Text, string Title, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        {
            WildMouse.SmoothControls.MessageBox Msg = new WildMouse.SmoothControls.MessageBox();

            Msg.Text = Text;
            Msg.Title = Title;
            Msg.Buttons = Buttons;
            Msg.Icon = Icon;

            Owner.Controls.Add(Msg);
            Msg.Visible = true;
            Msg.Invalidate();  //force it to draw itself

            Msg.AdjustDimensions();
            Msg.BringToFront();
        }
    }
}
