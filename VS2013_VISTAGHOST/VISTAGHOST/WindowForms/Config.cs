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
using System.Security.AccessControl;

namespace Vistaghost.VISTAGHOST
{
    public partial class Config : Form
    {
        bool AddCommentChanged = false;
        bool AddHeaderChanged = false;
        bool AddHistoryChanged = false;

        Settings settings;
        public ConfigEventHandler OnSendData;

        private HeaderStyle headerStyle = HeaderStyle.Aloka1;
        DTE2 dte2;

        private int parentIndex = -1;
        private int curIndex = -1;
        private int childIndex = -1;

        private string curHotKey = String.Empty;

        private List<VGCommand> commands = new List<VGCommand>() { 
            new VGCommand(0, Properties.Resources.AddComment, "[add]Tags", "Add", "None"),
            new VGCommand(0, Properties.Resources.AddComment, "[delete]Tags", "Delete", "None"),
            new VGCommand(0, Properties.Resources.AddComment, "[modify]Tags", "Modify", "None"),
            new VGCommand(1, Properties.Resources.DeleteComment, "", "Delete Comments", "None"),
            new VGCommand(2, Properties.Resources.CopyPrototype, "", "Copy Function Prototype", "None"),
            new VGCommand(3, Properties.Resources.SingleHeader, "", "Single Header", "None"),
            new VGCommand(4, Properties.Resources.CountLOC, "", "Count LOC", "None"),
            new VGCommand(5, Properties.Resources.ChangeInfo, "", "Change Comments Info", "None"),
            new VGCommand(6, Properties.Resources.Config, "", "Configurations", "None"),
            new VGCommand(7, Properties.Resources.MultiHeader, "", "Multi Header", "None"),
            new VGCommand(8, Properties.Resources.ImportExport, "", "Import/Export Settings", "None"),
            new VGCommand(9, Properties.Resources.About, "", "About Tool", "None")
        };

        public Config()
        {
            InitializeComponent();
            settings = new Settings();
            pnCommentSetting.Visible = true;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(new Point(e.X, e.Y)))
            {
                if (e.Node.Text == "Comments")
                {
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = false;
                    pnCommentSetting.Visible = true;
                    pnHistory.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "Header")
                {
                    pnHeaderSetting.Visible = true;
                    pnDataSetting.Visible = false;
                    pnCommentSetting.Visible = false;
                    pnHistory.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "Project")
                {
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = true;
                    pnCommentSetting.Visible = false;
                    pnHistory.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "History")
                {
                    pnHistory.Visible = true;
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = false;
                    pnCommentSetting.Visible = false;
                    pnKeyboard.Visible = false;
                }
                else if (e.Node.Text == "Keyboard")
                {
                    pnHistory.Visible = false;
                    pnHeaderSetting.Visible = false;
                    pnDataSetting.Visible = false;
                    pnCommentSetting.Visible = false;
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
            numerEmptyLine.Value = data.CommentInfo.EmptyLineNum;
            chAutoAddWithoutDialog.Checked = data.CommentInfo.AutoShowInputDialog;
            chkJustOneLine.Checked = data.CommentInfo.JustOneLine;

            // update header config
            richTextBox1.Text = data.HeaderInfo.BeginHeader;
            richTextBox2.Text = data.HeaderInfo.EndHeader;
            checkBox10.Checked = data.HeaderInfo.AddBreakLine;
            txtHistory.Text = data.HeaderInfo.History;
            cbHeaderStyle.SelectedIndex = data.HeaderInfo.Style;
            cbXAModel.SelectedIndex = cbXAModel.FindString(data.HeaderInfo.XAModel);

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
                cbXAModel.Enabled = checkBox11.Checked;
            }

            chDisHistory.Checked = data.HistoryInfo.DisplayHistory;
            chkLogHistory.Checked = data.HistoryInfo.WriteLogHistory;
            txtLogPath.Text = data.HistoryInfo.LogPath;

            rdTxtFile.Checked = (data.HistoryInfo.LogExtension == ".txt");
            rdXmlFile.Checked = (data.HistoryInfo.LogExtension == ".xml");

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
                    break;
                }
            }

            curIndex = -1;
            listGroupCommand_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void Config_Load(object sender, EventArgs e)
        {
            AddCommentChanged = AddHeaderChanged = false;

            this.Size = new System.Drawing.Size(647, 438);

            pnCommentSetting.Location = new Point(175, 7);
            pnCommentSetting.Size = new Size(451, 355);

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

            if(String.IsNullOrEmpty(settings.HistoryInfo.LogPath))
            {
                string logpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder);
                logpath = Path.Combine(logpath, vgSettingConstants.LogFolder);

                txtLogPath.Text = logpath;
            }
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
                settings.CommentInfo.EmptyLineNum = (int)numerEmptyLine.Value;
                settings.CommentInfo.AutoShowInputDialog = chAutoAddWithoutDialog.Checked;
                settings.CommentInfo.JustOneLine = chkJustOneLine.Checked;
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
                settings.HeaderInfo.XAModel = cbXAModel.Text;
            }

            if (AddHistoryChanged)
            {
                settings.HistoryInfo.DisplayHistory = chDisHistory.Checked;
                settings.HistoryInfo.WriteLogHistory = chkLogHistory.Checked;
                settings.HistoryInfo.LogPath = txtLogPath.Text;
                settings.HistoryInfo.LogExtension = (rdTxtFile.Checked) ? ".txt" : ".xml";
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

                if (isoStore.FileExists(vgSettingConstants.SettingFile))
                {
                    try
                    {
                        isoStore.DeleteFile(vgSettingConstants.SettingFile);
                        isoStore.Close();

                        vgSetting.SettingData = new Settings();

                        vgSetting.SettingData.HeaderInfo.HeaderComponents = new List<ComponentInfo>();
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Module Name:" });
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Calling Sequence:" });
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Function:" });
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Arguments:" });
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Return Value:" });
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Note:" });
                        vgSetting.SettingData.HeaderInfo.HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "History:" });

                        vgSetting.SaveSettings();
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

                    richTextBox3.Text = "Module Name:";
                    richTextBox4.Text = "Calling Sequence:";
                    richTextBox5.Text = "Function:";
                    richTextBox6.Text = "Arguments:";
                    richTextBox7.Text = "Return Value:";
                    richTextBox8.Text = "Note:";
                    richTextBox9.Text = "History:";

                    txtHistory.Text = "";
                    cbXAModel.SelectedIndex = 4;
                    cbHeaderStyle.SelectedIndex = 0;

                    richTextBox1.Text = "/*=============================================================Aloka===========";
                    richTextBox2.Text = "==============================================================Aloka==========*/";

                    richTextBox3.Enabled = richTextBox4.Enabled = richTextBox5.Enabled = false;
                    richTextBox6.Enabled = richTextBox7.Enabled = richTextBox8.Enabled = richTextBox9.Enabled = false;

                    checkBox4.Checked = checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = checkBox8.Checked = false;
                    checkBox9.Checked = checkBox10.Checked = checkBox11.Checked = false;

                    /*Reset history*/
                    chDisHistory.Checked = false;
                    chkLogHistory.Checked = false;
                    txtLogPath.Text = String.Empty;

                    txtHistory.Enabled = false;
                    btnSave.Enabled = false;

                    MessageBox.Show("All data have been deleted.", "Clear data result", MessageBoxButtons.OK, MessageBoxIcon.None);

                    btnClearData.Enabled = false;
                    return;
                }

                btnClearData.Enabled = true;
            }
        }

        private void AddComponent_CheckedChanged(object sender, EventArgs e)
        {
            var check = ((CheckBox)sender);
            var tag = int.Parse(((CheckBox)sender).Tag.ToString());

            if (tag == 0)
            {
                richTextBox3.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox3.Focus();
            }
            else if (tag == 1)
            {
                richTextBox4.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox4.Focus();
            }
            else if (tag == 2)
            {
                richTextBox5.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox5.Focus();
            }
            else if (tag == 3)
            {
                richTextBox6.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox6.Focus();
            }
            else if (tag == 4)
            {
                richTextBox7.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox7.Focus();
            }
            else if (tag == 5)
            {
                richTextBox8.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox8.Focus();
            }
            else if (tag == 6)
            {
                richTextBox9.Enabled = check.Checked;
                if (check.Checked)
                    richTextBox9.Focus();

                txtHistory.Enabled = check.Checked;
                cbXAModel.Enabled = check.Checked;
            }
        }

        private void cbDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddCommentChanged = true;
            btnSave.Enabled = true;

            var index = (DateFormat)((ComboBox)sender).SelectedIndex;
            switch (index)
            {
                case DateFormat.YYYYMMDD:
                    lblDate1.Text = lblDate2.Text = "<yyyymmdd>";
                    break;
                case DateFormat.YYYYDDMM:
                    lblDate1.Text = lblDate2.Text = "<yyyyddmm>";
                    break;
                case DateFormat.DDMMYYYY:
                    lblDate1.Text = lblDate2.Text = "<ddmmyyyy>";
                    break;
                case DateFormat.MMDDYYYY:
                    lblDate1.Text = lblDate2.Text = "<mmddyyyy>";
                    break;
                default:
                    break;
            }
        }

        private void Config_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void HeaderInput_TextChanged(object sender, EventArgs e)
        {
            if(!btnSave.Enabled)
                btnSave.Enabled = true;

            if(!AddHeaderChanged)
                AddHeaderChanged = true;
        }

        private void CommentInput_TextChanged(object sender, EventArgs e)
        {
            if(!AddCommentChanged)
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
            AddHistoryChanged = true;
            btnSave.Enabled = true;
        }

        private void listGroupCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            parentIndex = listGroupCommand.SelectedIndex;
            if (parentIndex == -1 || parentIndex == curIndex)
                return;

            curIndex = parentIndex;
            lvDetailKeys.Items.Clear();
            curHotKey = String.Empty;
            childIndex = 0;
            btnAssignHotKey.Enabled = false;
            btnRemoveHotkey.Enabled = false;

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
            {
                txtHotKey.Text = String.Empty;
                btnAssignHotKey.Enabled = false;
                btnRemoveHotkey.Enabled = false;
                return;
            }

            btnAssignHotKey.Enabled = true;
            btnRemoveHotkey.Enabled = true;
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
                MessageBox.Show(this, "Can not assign shortcut key. Check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                /*set empty hot key*/
                this.dte2.Commands.Item(strCmd, 0).Bindings = new object[] { };

                InitKeyBinding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Can not remove shortcut key. Check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void numerEmptyLine_ValueChanged(object sender, EventArgs e)
        {
            AddCommentChanged = true;
            btnSave.Enabled = true;
        }

        private void cmCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            AddCommentChanged = true;
            btnSave.Enabled = true;
        }

        private void cbXAModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curIndex = cbXAModel.SelectedIndex;
            int savedIndex = cbXAModel.FindString(settings.HeaderInfo.XAModel);

            if (curIndex == savedIndex)
            {
                return;
            }

            AddHeaderChanged = true;
            btnSave.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogPath.Text = folderBrowserDialog1.SelectedPath;

                AddHistoryChanged = true;
                btnSave.Enabled = true;
            }
        }

        private void typeFile_CheckedChanged(object sender, EventArgs e)
        {
            AddHistoryChanged = true;
            btnSave.Enabled = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Action == TreeViewAction.ByMouse)
            {
                return;
            }

            if (e.Node.Text == "Comments")
            {
                pnHeaderSetting.Visible = false;
                pnDataSetting.Visible = false;
                pnCommentSetting.Visible = true;
                pnHistory.Visible = false;
                pnKeyboard.Visible = false;
            }
            else if (e.Node.Text == "Header")
            {
                pnHeaderSetting.Visible = true;
                pnDataSetting.Visible = false;
                pnCommentSetting.Visible = false;
                pnHistory.Visible = false;
                pnKeyboard.Visible = false;
            }
            else if (e.Node.Text == "Project")
            {
                pnHeaderSetting.Visible = false;
                pnDataSetting.Visible = true;
                pnCommentSetting.Visible = false;
                pnHistory.Visible = false;
                pnKeyboard.Visible = false;
            }
            else if (e.Node.Text == "History")
            {
                pnHistory.Visible = true;
                pnHeaderSetting.Visible = false;
                pnDataSetting.Visible = false;
                pnCommentSetting.Visible = false;
                pnKeyboard.Visible = false;
            }
            else if (e.Node.Text == "Keyboard")
            {
                pnHistory.Visible = false;
                pnHeaderSetting.Visible = false;
                pnDataSetting.Visible = false;
                pnCommentSetting.Visible = false;
                pnKeyboard.Visible = true;
            }
        }

        private void btnStartProject_Click(object sender, EventArgs e)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                    vgSettingConstants.VGFolder,
                                    vgSettingConstants.WorkHistoryFolder);

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var path = Path.Combine(dir, vgSettingConstants.WorkHistoryFile);

            if(!File.Exists(path))
            {
                using (var stream = File.CreateText(path))
                {
                    /*Create new log file based on exists file*/
                    stream.Write(Properties.Resources.WorkHistory);
                }
            }

            File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.Encrypted);
        }
    }
}
