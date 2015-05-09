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
    public partial class SingleForm : Form
    {
        public AddCommentEventHandler OnSendData;
        bool ChangedContent = false;
        bool MoreOptions = false;
        ActionType vmode = ActionType.NONE;

        public SingleForm()
        {
            InitializeComponent();
        }

        public void LoadData(Settings settings, ActionType vmode)
        {
            txtContent.Text = settings.CommentInfo.Content;
            txtAccount.Text = settings.CommentInfo.Account;
            txtDevID.Text = settings.CommentInfo.DevID;
            chKeepComments.Checked = settings.CommentInfo.KeepComment;

            btnAdd.Enabled = true;
            ChangedContent = false;

            this.vmode = vmode;

            switch (vmode)
            {
                case ActionType.MODIFY:
                    {
                        if (expMore.IsExpanded)
                            expMore.IsExpanded = false;
                        this.Size = new Size(484, 172);
                        chKeepComments.Location = new Point(15, 99);
                        chKeepComments.Visible = true;
                        btnAdd.Location = new Point(369, 99);
                        expMore.Visible = true;
                        btnAdd.Text = "Add";
                    }
                    break;
                case ActionType.ADD:
                case ActionType.DELETE:
                    {
                        this.Size = new Size(484, 150);
                        expMore.Visible = false;
                        chKeepComments.Visible = false;
                        btnAdd.Location = new Point(369, 75);
                        btnAdd.Text = "Add";
                    }
                    break;

                case ActionType.CHANGE_INFO:
                    {
                        this.Size = new Size(484, 150);
                        expMore.Visible = false;
                        chKeepComments.Visible = false;
                        btnAdd.Location = new Point(369, 75);
                        btnAdd.Text = "Save";
                    }
                    break;

                case ActionType.NONE:
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("No Content!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty(txtAccount.Text))
            {
                MessageBox.Show("No Account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty(txtDevID.Text))
            {
                MessageBox.Show("No DevID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnAdd.Enabled = false;
            this.Close();

            if (OnSendData != null)
                OnSendData( txtContent.Text, 
                            txtAccount.Text, 
                            txtDevID.Text, 
                            txtFindWhat.Text, 
                            txtReplaceWith.Text, 
                            MoreOptions, 
                            vmode, 
                            chKeepComments.Checked, 
                            ChangedContent);
        }

        private void SingleForm_Load(object sender, EventArgs e)
        {
        }

        private void txtString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, EventArgs.Empty);
                return;
            }

            ChangedContent = true;
        }

        private void expMore_ExpandCollapse(object sender, MakarovDev.ExpandCollapsePanel.ExpandCollapseEventArgs e)
        {
            if (e.IsExpanded)
            {
                this.Size = new Size(this.Size.Width, this.Size.Height + 93);
                btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y + 93);
                chKeepComments.Location = new Point(chKeepComments.Location.X, chKeepComments.Location.Y + 93);

                txtFindWhat.Enabled = true;
                txtFindWhat.Focus();
                txtReplaceWith.Enabled = true;
                MoreOptions = true;
            }
            else
            {
                txtFindWhat.Enabled = false;
                txtReplaceWith.Enabled = false;

                btnAdd.Focus();
                this.Size = new Size(this.Size.Width, this.Size.Height - 93);
                btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y - 93);
                chKeepComments.Location = new Point(chKeepComments.Location.X, chKeepComments.Location.Y - 93);

                MoreOptions = false;
            }
        }

        private void chKeepComments_CheckedChanged(object sender, EventArgs e)
        {
            ChangedContent = true;
        }

        private void SingleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
