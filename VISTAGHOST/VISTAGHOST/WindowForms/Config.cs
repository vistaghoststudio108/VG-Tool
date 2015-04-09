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
using EnvDTE80;
using EnvDTE;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class Config : Form
    {
        bool AddCommentChanged = false;
        bool AddHeaderChanged = false;

        Settings settings;
        public ConfigEventHandler OnSendData;
        private HeaderStyle headerStyle = HeaderStyle.Aloka1;
        DTE2 dte2;

        private int parentIndex = 0;
        private int childIndex = 0;

        private string curHotKey = String.Empty;

        private List<VGCommand> commands = new List<VGCommand>() { 
            new VGCommand(0, "EditorContextMenus.CodeWindow.AddComments", "[add]Tags", "Add", "None"),
            new VGCommand(0, "EditorContextMenus.CodeWindow.AddComments", "[delete]Tags", "Delete", "None"),
            new VGCommand(0, "EditorContextMenus.CodeWindow.AddComments", "[modify]Tags", "Modify", "None"),
            new VGCommand(1, "EditorContextMenus.CodeWindow.DeleteComments", "", "Delete Comments", "None"),
            new VGCommand(2, "EditorContextMenus.CodeWindow.MakeFunctionHeader", "", "Single Header", "None"),
            new VGCommand(3, "EditorContextMenus.CodeWindow.CountLinesofCode", "", "Count LOC", "None"),
            new VGCommand(4, "Vistaghost.ChangeInfo", "", "Change Comments Info", "None"),
            new VGCommand(5, "Vistaghost.HistoryViewer", "", "History Viewer", "None"),
            new VGCommand(6, "Vistaghost.Configurations", "", "Configurations", "None"),
            new VGCommand(7, "Vistaghost.CreateMultiHeader", "", "Multi Header", "None"),
            new VGCommand(8, "Vistaghost.ImportandExportSettings", "", "Import/Export Settings", "None"),
            new VGCommand(9, "Vistaghost.About", "", "About Tool", "None")
        };

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
                    pnHistory.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "Header")
                {
                    pnHeaderSetting.Visible = true;
                    pnDataSetting.Visible = false;
                    pnSingleSetting.Visible = false;
                    pnHistory.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "Data Management")
                {
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = true;
                    pnSingleSetting.Visible = false;
                    pnHistory.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "History")
                {
                    pnHistory.Visible = true;
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = false;
                    pnSingleSetting.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "Keyboard")
                {
                    pnHistory.Visible = false;
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = false;
                    pnSingleSetting.Visible = false;
                    pnKeyboard.Visible = true;
                }
            }
        }

        public void LoadConfig(DTE2 dte2, Settings data)
        {
            this.settings = data;
            this.dte2 = dte2;
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

            /*Load keybinding*/
            InitKeyBinding();
        }

        void InitKeyBinding()
        {
            object[] bindings;
            var appendCommand = String.Empty;
            foreach (var c in commands)
            {
                if (c.HotKeys == txtHotKey.Text)
                {
                    c.HotKeys = "None";
                }

                if (String.IsNullOrEmpty(c.MissName))
                {
                    appendCommand = c.KeyName;
                }
                else
                {
                    appendCommand = c.KeyName + "." + c.MissName;
                }

                bindings = (object[])this.dte2.Commands.Item(appendCommand, 0).Bindings;
                foreach (object b in bindings)
                {
                    c.HotKeys = ((string)b).Remove(0, 8);
                }
            }

            listGroupCommand_SelectedIndexChanged(null, EventArgs.Empty);
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

            pnHistory.Location = new Point(175, 7);
            pnHistory.Size = new Size(451, 355);

            pnKeyboard.Location = new Point(175, 7);
            pnKeyboard.Size = new Size(451, 355);

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

        private void chkLogHistory_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = chkLogHistory.Checked;
        }

        private void btnCustomForColor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void btnCustomBackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {

            }
        }

        private void cbForgroundColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPreview.ForeColor = cbForgroundColor.SelectedItem.Color;
        }

        private void cbBackgroundColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPreview.BackColor = cbBackgroundColor.SelectedItem.Color;
        }

        private void listGroupCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            parentIndex = listGroupCommand.SelectedIndex;
            if (parentIndex == -1)
                return;

            lvDetailKeys.Items.Clear();
            curHotKey = String.Empty;
            childIndex = 0;

            foreach (var c in commands)
            {
                if (c.GroupID == parentIndex)
                {
                    ListViewItem item = new ListViewItem(new string[] { c.DisName, c.HotKeys });
                    lvDetailKeys.Items.Add(item);
                }
            }
        }

        private void lvDetailKeys_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;

            childIndex = e.ItemIndex;

            txtHotKey.Text = lvDetailKeys.SelectedItems[0].SubItems[1].Text;
            curHotKey = txtHotKey.Text;
        }

        string GetCommandString()
        {
            var strCmd = String.Empty;

            foreach (var c in commands)
            {
                if (c.GroupID == parentIndex)
                {
                    if (String.IsNullOrEmpty(c.MissName))
                    {
                        strCmd = c.KeyName;
                    }
                    else
                    {
                        strCmd = c.KeyName + "." + commands[childIndex].MissName;
                    }
                    break;
                }
            }

            return strCmd;
        }

        private void btnAssignHotKey_Click(object sender, EventArgs e)
        {
            if (curHotKey == txtHotKey.Text || curHotKey == "")
            {
                return;
            }

            var strCmd = String.Empty;
            Commands cmds;
            Command cmd;

            strCmd = GetCommandString();

            try
            {
                // Set references to the Commands collection and the 
                cmds = dte2.Commands;
                cmd = cmds.Item(strCmd, 1);

                // Assigns the command key
                cmd.Bindings = "Global::" + txtHotKey.Text;

                /*update info*/
                InitKeyBinding();

                txtHotKey.Text = String.Empty;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        private void btnRemoveHotkey_Click(object sender, EventArgs e)
        {
            if (curHotKey == "None" || curHotKey == "")
            {
                return;
            }

            var strCmd = String.Empty;

            strCmd = GetCommandString();

            try
            {
                this.dte2.Commands.Item(strCmd, 0).Bindings = new object[] { };

                InitKeyBinding();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        private void listGroupCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && listGroupCommand.SelectedIndex != -1)
            {
                lvDetailKeys.Focus();
                lvDetailKeys.Items[childIndex].Selected = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lvDetailKeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                listGroupCommand.Focus();
                e.SuppressKeyPress = true;
            }
        }
    }
}
