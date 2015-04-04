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
using System.Xml.Serialization;
using System.Xml;
using Vistaghost.VISTAGHOST.Helper;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class Config : Form
    {
        bool AddCommentChanged = false;
        bool AddHeaderChanged = false;

        Settings settings;
        public ConfigEventHandler OnSendData;
        private HeaderStyle headerStyle = HeaderStyle.Aloka1;

        public Config()
        {
            InitializeComponent();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(new Point(e.X, e.Y)))
            {
                if (e.Node.Text == "Comments")
                {
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = false;
                    pnSingleSetting.Visible = true;
                }
                else if (e.Node.Text == "Header")
                {
                    pnHeaderSetting.Visible = true;
                    pnDataSetting.Visible = false;
                    pnSingleSetting.Visible = false;
                }
                else if (e.Node.Text == "Data Management")
                {
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = true;
                    pnSingleSetting.Visible = false;
                }
            }
        }

        public void LoadConfig(Settings data)
        {
            this.settings = data;
            // update comment config
            txtOpenBeginTag.Text = data.CommentInfo.OpenTagBegin;
            txtOpenEndTag.Text = data.CommentInfo.OpenTagEnd;
            txtCloseBeginTag.Text = data.CommentInfo.CloseTagBegin;
            txtCloseEndTag.Text = data.CommentInfo.CloseTagEnd;

            cbDateFormat.SelectedIndex = data.CommentInfo.DateFormat;
            chAutoAddWithoutDialog.Checked = data.CommentInfo.AutoShowInputDialog;
            chDisHistory.Checked = data.CommentInfo.DisplayHistory;

            // update header config
            richTextBox1.Text = data.HeaderInfo.BeginHeader;
            richTextBox2.Text = data.HeaderInfo.EndHeader;
            checkBox10.Checked = data.HeaderInfo.AddBreakLine;
            txtHistory.Text = data.HeaderInfo.History;
            cbHeaderStyle.SelectedIndex = data.HeaderInfo.Style;

            if (data.HeaderInfo.HeaderComponents != null && data.HeaderInfo.HeaderComponents.Count != 0)
            {
                richTextBox3.Text = data.HeaderInfo.HeaderComponents[0].Name;
                richTextBox4.Text = data.HeaderInfo.HeaderComponents[1].Name;
                richTextBox5.Text = data.HeaderInfo.HeaderComponents[2].Name;
                richTextBox6.Text = data.HeaderInfo.HeaderComponents[3].Name;
                richTextBox7.Text = data.HeaderInfo.HeaderComponents[4].Name;
                richTextBox8.Text = data.HeaderInfo.HeaderComponents[5].Name;
                richTextBox9.Text = data.HeaderInfo.HeaderComponents[6].Name;

                checkBox4.Checked = data.HeaderInfo.HeaderComponents[0].Checked;
                checkBox5.Checked = data.HeaderInfo.HeaderComponents[1].Checked;
                checkBox6.Checked = data.HeaderInfo.HeaderComponents[2].Checked;
                checkBox7.Checked = data.HeaderInfo.HeaderComponents[3].Checked;
                checkBox8.Checked = data.HeaderInfo.HeaderComponents[4].Checked;
                checkBox9.Checked = data.HeaderInfo.HeaderComponents[5].Checked;
                checkBox11.Checked = data.HeaderInfo.HeaderComponents[6].Checked;

                txtHistory.Enabled = checkBox11.Checked;
            }
        }

        private void Config_Load(object sender, EventArgs e)
        {
            AddCommentChanged = AddHeaderChanged = false;

            this.Size = new Size(636, 430);

            pnSingleSetting.Location = new Point(175, 7);
            pnSingleSetting.Size = new Size(451, 355);
            pnHeaderSetting.Location = new Point(175, 7);
            pnHeaderSetting.Size = new Size(451, 355);
            pnDataSetting.Location = new Point(175, 7);
            pnDataSetting.Size = new Size(451, 355);

            toolTip1.SetToolTip(txtOpenBeginTag, txtOpenBeginTag.Text);
            toolTip1.SetToolTip(txtCloseBeginTag, txtCloseBeginTag.Text);
            treeView1.ExpandAll();
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnClearData.Enabled = true;

            if (AddCommentChanged)
            {
                //update data
                settings.CommentInfo.OpenTagBegin = txtOpenBeginTag.Text;
                settings.CommentInfo.OpenTagEnd = txtOpenEndTag.Text;
                settings.CommentInfo.CloseTagBegin = txtCloseBeginTag.Text;
                settings.CommentInfo.CloseTagEnd = txtCloseEndTag.Text;

                settings.CommentInfo.DateFormat = cbDateFormat.SelectedIndex;
                settings.CommentInfo.AutoShowInputDialog = chAutoAddWithoutDialog.Checked;
                settings.CommentInfo.DisplayHistory = chDisHistory.Checked;
            }

            if (AddHeaderChanged)
            {
                settings.HeaderInfo.BeginHeader = richTextBox1.Text;
                if (settings.HeaderInfo.HeaderComponents == null || settings.HeaderInfo.HeaderComponents.Count == 0)
                {
                    settings.HeaderInfo.HeaderComponents = new List<ComponentInfo>(7);
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox3.Text, Checked = checkBox4.Checked });
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox4.Text, Checked = checkBox5.Checked });
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox5.Text, Checked = checkBox6.Checked });
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox6.Text, Checked = checkBox7.Checked });
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox7.Text, Checked = checkBox8.Checked });
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox8.Text, Checked = checkBox9.Checked });
                    settings.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Name = richTextBox9.Text, Checked = checkBox11.Checked });
                }
                else
                {
                    settings.HeaderInfo.HeaderComponents[0] = new ComponentInfo { Name = richTextBox3.Text, Checked = checkBox4.Checked };
                    settings.HeaderInfo.HeaderComponents[1] = new ComponentInfo { Name = richTextBox4.Text, Checked = checkBox5.Checked };
                    settings.HeaderInfo.HeaderComponents[2] = new ComponentInfo { Name = richTextBox5.Text, Checked = checkBox6.Checked };
                    settings.HeaderInfo.HeaderComponents[3] = new ComponentInfo { Name = richTextBox6.Text, Checked = checkBox7.Checked };
                    settings.HeaderInfo.HeaderComponents[4] = new ComponentInfo { Name = richTextBox7.Text, Checked = checkBox8.Checked };
                    settings.HeaderInfo.HeaderComponents[5] = new ComponentInfo { Name = richTextBox8.Text, Checked = checkBox9.Checked };
                    settings.HeaderInfo.HeaderComponents[6] = new ComponentInfo { Name = richTextBox9.Text, Checked = checkBox11.Checked };
                }

                settings.HeaderInfo.EndHeader = richTextBox2.Text;
                settings.HeaderInfo.AddBreakLine = checkBox10.Checked;
                settings.HeaderInfo.History = txtHistory.Text;
                settings.HeaderInfo.Style = (int)headerStyle;
            }

            if (OnSendData != null && (AddCommentChanged | AddHeaderChanged))
                OnSendData(settings);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// single_config.vsc, single_info.vsc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you really want to delete saved data ?", "Clear data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

                if (isoStore.GetFileNames(VGSettingConstants.SettingFile).Length > 0)
                {
                    try
                    {
                        isoStore.DeleteFile(VGSettingConstants.SettingFile);
                        isoStore.Close();

                        VGSetting.SettingData = new Settings();

                        VGSetting.SaveSettings();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        return;
                    }

                    // reset to original status
                    txtOpenBeginTag.Text = txtCloseBeginTag.Text = "//<Not set>";
                    txtOpenEndTag.Text = txtCloseEndTag.Text = "/";

                    cbDateFormat.SelectedIndex = 0;
                    chAutoAddWithoutDialog.Checked = true;
                    chDisHistory.Checked = false;

                    richTextBox3.Text = richTextBox4.Text = richTextBox5.Text = "";
                    richTextBox6.Text = richTextBox7.Text = richTextBox8.Text = richTextBox9.Text = txtHistory.Text = "";
                    richTextBox1.Text = "/*<Not set>";
                    richTextBox2.Text = "<Not set>*/";

                    richTextBox3.Enabled = richTextBox4.Enabled = richTextBox5.Enabled = false;
                    richTextBox6.Enabled = richTextBox7.Enabled = richTextBox8.Enabled = richTextBox9.Enabled = false;

                    checkBox4.Checked = checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = checkBox8.Checked = false;
                    checkBox9.Checked = checkBox10.Checked = checkBox11.Checked = false;

                    txtHistory.Enabled = false;
                    btnSave.Enabled = false;

                    MessageBox.Show("All data have been deleted.", "Clear data result", MessageBoxButtons.OK, MessageBoxIcon.None);

                    btnClearData.Enabled = false;
                    return;
                }

                btnClearData.Enabled = true;
            }
        }

        private void storage_CheckedChanged(object sender, EventArgs e)
        {
            txtExternalLink.Enabled = radioButton4.Checked;
            btnBrowseExternal.Enabled = radioButton4.Checked;
        }

        private void AddComponent_MouseClick(object sender, MouseEventArgs e)
        {
            AddHeaderChanged = true;
            btnSave.Enabled = true;
        }

        private void Options_MouseClick(object sender, MouseEventArgs e)
        {
            AddCommentChanged = true;
            btnSave.Enabled = true;
        }

        private void AddComponent_CheckedChanged(object sender, EventArgs e)
        {
            var check = ((CheckBox)sender);
            var tag = int.Parse(((CheckBox)sender).Tag.ToString());

            if (tag == 0)
                richTextBox3.Enabled = check.Checked;
            else if (tag == 1)
                richTextBox4.Enabled = check.Checked;
            else if (tag == 2)
                richTextBox5.Enabled = check.Checked;
            else if (tag == 3)
                richTextBox6.Enabled = check.Checked;
            else if (tag == 4)
                richTextBox7.Enabled = check.Checked;
            else if (tag == 5)
                richTextBox8.Enabled = check.Checked;
            else if (tag == 6)
            {
                richTextBox9.Enabled = check.Checked;
                txtHistory.Enabled = check.Checked;
            }
        }

        private void cbDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddCommentChanged = true;
            btnSave.Enabled = true;

            var index = (DateFormat)((ComboBox)sender).SelectedIndex;
            switch (index)
            {
                case DateFormat.yyyymmdd:
                    lblDate1.Text = lblDate2.Text = "<yyyymmdd>";
                    break;
                case DateFormat.yyyyddmm:
                    lblDate1.Text = lblDate2.Text = "<yyyyddmm>";
                    break;
                case DateFormat.ddmmyyyy:
                    lblDate1.Text = lblDate2.Text = "<ddmmyyyy>";
                    break;
                case DateFormat.mmddyyyy:
                    lblDate1.Text = lblDate2.Text = "<mmddyyyy>";
                    break;
                default:
                    break;
            }
        }

        private void Config_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void HeaderInput_TextChanged(object sender, EventArgs e)
        {
            if (!btnSave.Enabled)
                btnSave.Enabled = true;

            if (!AddHeaderChanged)
                AddHeaderChanged = true;
        }

        private void CommentInput_TextChanged(object sender, EventArgs e)
        {
            if (!AddCommentChanged)
                AddCommentChanged = true;

            if (!btnSave.Enabled)
                btnSave.Enabled = true;
        }

        private void cbHeaderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curStyle = headerStyle;
            headerStyle = (HeaderStyle)cbHeaderStyle.SelectedIndex;

            if (curStyle != headerStyle)
            {
                AddHeaderChanged = true;

                if (!btnSave.Enabled)
                    btnSave.Enabled = true;
            }
        }
    }
}
