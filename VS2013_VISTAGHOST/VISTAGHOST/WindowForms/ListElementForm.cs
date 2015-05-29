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

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class ListElementForm : Form
    {
        public ListElementEventHandler OnResult;
        public ListElementForm()
        {
            InitializeComponent();
        }

        public void SetData(List<VISTAGHOST.DataModel.VGCodeElement> data)
        {
            foreach (var func in data)
            {
                dtElements.Rows.Add(true, func.Name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (OnResult != null)
            {
                OnResult(this, VGDialogResult.VG_OK);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (OnResult != null)
            {
                OnResult(this, VGDialogResult.VG_CANCEL);
            }

            this.Close();
        }
    }
}
