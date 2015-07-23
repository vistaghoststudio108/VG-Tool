using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GhostExcel.UserControls;
using GhostExcel.DataModel;
using System.Net.Mail;
using System.Threading;
using GhostExcel.Properties;
using System.Net.Mime;

namespace GhostExcel.WinForm
{
    public delegate void UCEventHandler(object sender, string message, object attachments, MailServer mailSvr);
    public partial class ExceptionForm : Form
    {
        //List<AttachmentItem> attachItems = new List<AttachmentItem>();
        //int numItem = 0;
        //const int DEFAULT_X = 13;
        //const int DEFAULT_Y = 120;
        public UCEventHandler OnSendMail;
        public UCEventHandler OnCancelMail;
        //const int MaxAttachItem = 5;
        public ExceptionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check size of attached files
        /// </summary>
        /// <returns>true - if size > 5MB; false - if other</returns>
        //private bool IsFileSizeTooLarge()
        //{
        //    long size = 0;

        //    try
        //    {
        //        foreach (var item in attachItems)
        //        {
        //            size += new System.IO.FileInfo(item.RealText).Length;
        //        }

        //        if (size >= GhostConstants.MaxAttachFileSize)
        //            return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExcelLogger.LogError(ex);
        //    }

        //    return false;
        //}

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            ////Check mail's body
            //if (String.IsNullOrEmpty(this.txtMessage.Text))
            //{
            //    MessageBox.Show(this, Resources.MailContentEmpty, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            ////Check size of attached files
            //if (IsFileSizeTooLarge())
            //{
            //    if (MessageBox.Show(Resources.FileSizeTooLarge, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            //    {
            //        this.Close();
            //        return;
            //    }
            //}

            //List<Attachment> attachments = new List<Attachment>();
            ////Create attachments file
            //foreach (var item in attachItems)
            //{
            //    var stream = ExcelUtilities.WriteFileToMemory(item.Text);
            //    var filename = System.IO.Path.GetFileName(item.Text);

            //    var attachfile = new Attachment(stream, filename);
            //    attachfile.ContentType = new ContentType(GhostConstants.ContentTypeString);
            //    attachments.Add(attachfile);
            //}

            //Get server
            MailServer mailSvr = MailServer.Gmail;

            //Send mail
            if (OnSendMail != null)
                OnSendMail(this, txtMessage.Text, null, mailSvr);

            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (OnCancelMail != null)
                OnCancelMail(this, String.Empty, null, MailServer.None);

            base.OnClosed(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Update control's position after add/remove an attach file
        /// </summary>
        /// <param name="offset">offset value to move up/down</param>
        /// <param name="index">index of removed file</param>
        //private void UpdateControlsPosition(int offset, int index)
        //{
        //    //Update the rest items in list
        //    if (index != -1)
        //    {
        //        for (int idx = index; idx < attachItems.Count; idx++)
        //        {
        //            attachItems[idx].Location = new Point(DEFAULT_X, attachItems[idx].Location.Y + offset);
        //        }
        //    }

        //    // Update other controls
        //    this.Height += offset;
        //    this.btnCancel.Location = new Point(this.btnCancel.Location.X, this.btnCancel.Location.Y + offset);
        //    this.btnSendMail.Location = new Point(this.btnSendMail.Location.X, this.btnSendMail.Location.Y + offset);
        //}

        /// <summary>
        /// Check the existing of file
        /// </summary>
        /// <param name="fileName">file need to check</param>
        /// <returns>true - if file is existed; false - if other</returns>
        //private bool IsFileExisted(string fileName)
        //{
        //    foreach (var item in attachItems)
        //    {
        //        if (string.Compare(fileName, item.RealText) == 0)
        //            return true;
        //    }

        //    return false;
        //}

        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    if (this.numItem == MaxAttachItem)
        //    {
        //        MessageBox.Show(this, Properties.Resources.MaximumAttachFile, "Maximum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    openFileDialog1.Multiselect = false;
        //    openFileDialog1.Title = "Attach Files";
        //    openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //    if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
        //    {
        //        if (!IsFileExisted(openFileDialog1.FileName))
        //        {
        //            AttachmentItem item = new AttachmentItem();
        //            item.Index = this.numItem;
        //            item.Width = this.txtMessage.Width - 1;
        //            item.Text = openFileDialog1.FileName;
        //            item.OnClosed += new EventHandler(item_OnClosed);
        //            item.Location = new Point(DEFAULT_X, DEFAULT_Y + this.numItem * (item.Height + 1));

        //            attachItems.Add(item);
        //            this.Controls.Add(item);

        //            this.numItem++;

        //            UpdateControlsPosition(item.Height + 1, -1);
        //        }
        //    }
        //}

        //private void item_OnClosed(object sender, EventArgs e)
        //{
        //    this.numItem--;
        //    var item = (AttachmentItem)sender;
        //    int index = attachItems.IndexOf(item);
        //    attachItems.Remove(item);

        //    UpdateControlsPosition(-item.Height - 1, index);
        //}

        private void FeedBackForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ShowExceptionDetails(List<string> info)
        {
            string[] property = { "Exception", "Message", "Target Site", "Source", "Stack Trace" };
            for (int index = 0; index < info.Count; index++)
            {
                ListViewItem lvItem = new ListViewItem(new string[] { property[index], info[index] });
                this.lvExceptionDetail.Items.Add(lvItem);
            }
        }

        public void SetExInformation(Exception ex)
        {
            txtExceptionType.Text = ex.GetType().FullName;
            txtTargetSite.Text = ex.TargetSite.ToString();
            txtDateTime.Text = string.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            txtApplication.Text = System.Reflection.Assembly.GetExecutingAssembly().FullName;

            treeException.Nodes.Add(ex.GetType().Name);

            List<string> info = new List<string>();
            info.Add(ex.GetType().Name);
            info.Add(ex.Message);
            info.Add(ex.TargetSite.ToString());
            info.Add(ex.Source);
            info.Add(ex.StackTrace);

            ShowExceptionDetails(info);
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
