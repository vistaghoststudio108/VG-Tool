using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class DeleteForm : Form
    {
        public DeleteEventHandler OnSendData;

        public DeleteForm()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Close();

            if (OnSendData != null)
            {
                OnSendData(cbDeleteDoubleSlash.Checked, cbDeleteSlashStar.Checked, cbDeleteAllBreakLines.Checked, cbSmartFormat.Checked);
            }
        }

        private void DeleteForm_Load(object sender, EventArgs e)
        {
            btnApply.Focus();
        }

        private void DeleteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
