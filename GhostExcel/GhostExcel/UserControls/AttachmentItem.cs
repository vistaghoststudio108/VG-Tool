using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GhostExcel.UserControls
{
    public partial class AttachmentItem : UserControl
    {
        public EventHandler OnClosed;

        public int Index { get; set; }

        public override string Text
        {
            get
            {
                return this.lblFilePath.Text;
            }
            set
            {
                this.lblFilePath.Text = value;
            }
        }

        public AttachmentItem()
        {
            InitializeComponent();
        }

        private void AttachmentItem_Load(object sender, EventArgs e)
        {
            this.Height = this.lblFilePath.Height;

            this.btnClose.Height = this.Height;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (OnClosed != null)
                OnClosed(this, EventArgs.Empty);

            this.Dispose();
        }

        private void AttachmentItem_SizeChanged(object sender, EventArgs e)
        {
            this.btnClose.Location = new Point(this.Width - this.btnClose.Width - 2, this.btnClose.Location.Y);
        }
    }
}
