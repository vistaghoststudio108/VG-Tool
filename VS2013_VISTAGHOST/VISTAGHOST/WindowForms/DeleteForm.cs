using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST
{
    public partial class DeleteForm : Form
    {
        public DeleteEventHandler OnSendData;
        bool Changed = false;

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
            Changed = false;
            btnApply.Enabled = false;
        }

        private void DeleteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cbDeleteDoubleSlash_CheckedChanged(object sender, EventArgs e)
        {
            if (!Changed)
            {
                btnApply.Enabled = true;
                Changed = true;
            }
        }
    }
}
