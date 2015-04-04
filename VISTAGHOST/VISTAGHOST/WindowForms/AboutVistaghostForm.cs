using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class AboutVistaghostForm : Form
    {
        public AboutVistaghostForm()
        {
            InitializeComponent();
        }

        private void AboutVistaghostForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Vistaghost 2015\nVersion1.0.3\n\u00a9 2015 Vistaghost Studio\nAll right reserved";
        }

        public void GetLicense(RegisterData license)
        {
            txtLicense.Text = "Name : " + license.FullName + "\nAccount : " + license.Account + "\nRank : " + license.Rank +
                "\nWork at : " + license.WorkAt + "\nDate : " + license.Date;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutVistaghostForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
