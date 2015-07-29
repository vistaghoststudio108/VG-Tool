using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GhostExcel.WinForm
{
    public partial class ProgressForm : Form
    {
        public string Message
        {
            set { lblStatus.Text = value; }
        }

        public int ProgressValue
        {
            set { progressUpdate.Value = value; }
        }

        public event EventHandler<EventArgs> Canceled;

        public ProgressForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Create a copy of the event to work with
            EventHandler<EventArgs> ea = Canceled;
            /* If there are no subscribers, eh will be null so we need to check
             * to avoid a NullReferenceException. */
            if (ea != null)
                ea(this, e);
        }
    }
}
