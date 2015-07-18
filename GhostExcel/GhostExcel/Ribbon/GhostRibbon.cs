using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using GhostExcel.WinForm;
using GhostExcel.DataModel;
using System.Net.Mail;
using System.Windows.Forms;

namespace GhostExcel
{
    public partial class GhostRibbon
    {
        Excel.Application _application;

        private void GhostRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this._application = Globals.ThisAddIn.Application;

            mailThread.DoWork += backgroundWorker_DoWork;
            mailThread.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.SendMailSucceed, "Notifications", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //btnUpdate.Enabled = true;
            ////Show my custom pane
            //if(!Globals.ThisAddIn.MyCustomTaskPane.Visible)
            //    Globals.ThisAddIn.MyCustomTaskPane.Visible = true;
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MailData mailData = (MailData)e.Argument;

            ExcelUtilities.SendFeedBackEmail(GhostConstants.MailSubject, 
                                             mailData.Message, 
                                             mailData.Attachments as List<Attachment>, 
                                             mailData.MailSvr);
        }

        private void btnUpdate_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, RibbonControlEventArgs e)
        {
            SettingForm sf = new SettingForm();
            sf.ShowDialog();
        }

        private void fbf_OnSendMail(object sender, string message, object attachments, MailServer mailSvr)
        {
            if(!mailThread.IsBusy)
            {
                MailData mailData = new MailData 
                { 
                    MailSvr = mailSvr, 
                    Message = message,
                    Attachments = attachments 
                };

                mailThread.RunWorkerAsync(mailData);
            }
        }

        private void btnFeedBack_Click(object sender, RibbonControlEventArgs e)
        {
            FeedBackForm fbf = new FeedBackForm();
            fbf.OnSendMail += new UCEventHandler(fbf_OnSendMail);
            fbf.ShowDialog();
        }
    }
}
