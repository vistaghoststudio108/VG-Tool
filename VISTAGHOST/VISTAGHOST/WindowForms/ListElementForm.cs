using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.DataModel;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class ListElementForm : Form
    {
        public ListElementEventHandler OnResult;
        private List<VGCodeElement> ceList = new List<VGCodeElement>();

        public ListElementForm()
        {
            InitializeComponent();
        }
        public void AddItem(VGCodeElement element)
        {
            dtElements.Rows.Add(true, element.Name);
            ceList.Add(element);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<VGCodeElement> finalList = new List<VGCodeElement>();

            for (int i = 0; i < dtElements.Rows.Count; i++)
            {
                var chk = (bool)dtElements.Rows[i].Cells[0].Value;
                if (chk)
                {
                    finalList.Add(ceList[i]);
                }
            }

            if (OnResult != null)
            {
                OnResult(finalList, VGDialogResult.VG_OK);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (OnResult != null)
            {
                OnResult(null, VGDialogResult.VG_CANCEL);
            }

            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtElements.Rows)
            {
                row.Cells[0].Value = chkSelectAll.Checked;
            }
        }

        private void ListElementForm_Load(object sender, EventArgs e)
        {
            chkSelectAll.Checked = true;
        }

    }
}
