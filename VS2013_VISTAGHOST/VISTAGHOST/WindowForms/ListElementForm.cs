using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.DataModel;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class ListElementForm : Form
    {
        public ListElementEventHandler OnResult;
        public ListElementForm()
        {
            InitializeComponent();
        }

        public void SetData(List<VGCodeElement> data)
        {
            foreach (var func in data)
            {
                dtElements.Rows.Add(true, func.Name);
            }
        }

        public void AddItem(string item)
        {
            dtElements.Rows.Add(true, item);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<VGCodeElement> finalList = new List<VGCodeElement>();
            foreach (DataGridViewRow row in dtElements.Rows)
            {
                var chk = (bool)row.Cells[0].Value;
                if(chk)
                {
                    //finalList.Add()
                }
            }
            if (OnResult != null)
            {
                OnResult(null, VGDialogResult.VG_OK);
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
    }
}
