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
    public partial class RegisterForm : Form
    {
        int index = 0;

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(529, 395);

            groupBox1.Size = new Size(517, 259);
            groupBox1.Location = new Point(-2, 56);
            groupBox1.BringToFront();

            groupBox2.Size = new Size(517, 259);
            groupBox2.Location = new Point(-2, 56);

            groupBox3.Size = new Size(517, 259);
            groupBox3.Location = new Point(-2, 56);
        }

        private void rdAccept_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = rdAccept.Checked;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            index++;
            if (index == 3)
            {
                RegisterData regData = new RegisterData
                {
                    Account = txtAccount.Text,
                    FullName = txtName.Text,
                    Rank = txtRank.Text,
                    WorkAt = txtWorkAt.Text,
                    Registered = true,
                    Date = DateTime.Now.ToLongDateString()
                };

                VGSetting.RegisterData = regData;

                VGSetting.SaveRegisterInfo();
                index = 2;
                this.Close();

                return;
            }

            if (index == 1)
            {
                label1.Text = "Your Information";
                label2.Text = "Provide accurate information about you. A license will be generated based\non this information for use this tool.";
                groupBox2.BringToFront();
            }
            else if (index == 2 &&
                !String.IsNullOrEmpty(txtAccount.Text) &&
                !String.IsNullOrEmpty(txtRank.Text) &&
                !String.IsNullOrEmpty(txtWorkAt.Text))
            {
                label1.Text = "Final Information";
                label2.Text = "Your final information to make a license.";
                lblName.Text = !String.IsNullOrEmpty(txtName.Text) ? txtName.Text : "Unknown";
                lblAccount.Text = !String.IsNullOrEmpty(txtAccount.Text) ? txtAccount.Text : "Unknown";
                lblRank.Text = !String.IsNullOrEmpty(txtRank.Text) ? txtRank.Text : "Unknown";
                lblWorkAt.Text = !String.IsNullOrEmpty(txtWorkAt.Text) ? txtWorkAt.Text : "Unknown";

                groupBox3.BringToFront();
            }
            else
            {
                index--;
                MessageBox.Show("Required fields must not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnBack.Enabled = (index > 0) ? true : false;
            btnNext.Text = (index == 2) ? "Finished" : "Next";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 0)
            {
                index = 0;
                return;
            }

            btnBack.Enabled = (index == 0) ? false : true;
            btnNext.Text = (index == 2) ? "Finished" : "Next";

            if (index == 1)
            {
                label1.Text = "Your Information";
                label2.Text = "Provide accurate information about you. A license will be generated based\non this information for use this tool.";
                groupBox2.BringToFront();
            }
            else if (index == 0)
            {
                label1.Text = "License Agreement";
                label2.Text = "Please read the following License Agreement. You must accept the term of\nthis agreement before continuing with the use.";
                groupBox1.BringToFront();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
