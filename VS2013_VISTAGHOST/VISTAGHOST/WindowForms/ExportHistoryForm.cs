using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST
{
    public partial class ExportHistoryForm : Form
    {
        public ExportEventHandler OnExportResult;
        public ExportHistoryForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtLocation.Text = saveFileDialog1.FileName;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtLocation.Text))
            {
                MessageBox.Show("File path is invalidated.", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ExportFileTo(txtLocation.Text))
            {
                if (OnExportResult != null)
                {
                    OnExportResult("Export file successed", true);
                }

                return;
            }

            if (OnExportResult != null)
            {
                OnExportResult("Export file failed", false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool ExportFileTo(string location)
        {
            var data = vgSetting.LoadSettings();

            return false;
        }
    }
}
