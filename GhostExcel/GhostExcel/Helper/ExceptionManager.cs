using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GhostExcel.WinForm;
using GhostExcel.DataModel;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Mail;
using System.Threading;

namespace GhostExcel
{
    public class ExceptionManager
    {
        static ExceptionManager _instance = null;

        public static ExceptionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExceptionManager();
                }

                return _instance;
            }
        }

        private ExceptionManager()
        {
        }

        private void exForm_OnSendMail(object sender, string message, object attachments, MailServer mailSvr)
        {
            Thread mThread = new Thread(delegate()
                {
                    bool succeed = ExcelUtilities.SendFeedBackEmail(GhostConstants.MailSubject,
                                                    message,
                                                    attachments as List<Attachment>,
                                                    mailSvr);

                });

            try
            {
                mThread.Start();
            }
            catch (ThreadStartException ex)
            {
                ExcelLogger.LogError(ex);
            }
        }

        private void exForm_OnCancelMail(object sender, string message, object attachments, MailServer mailSvr)
        {
            //Do something here
        }

        public void ThrowExceptionReport(Exception baseEx)
        {
            ExceptionForm exForm = new ExceptionForm();
            exForm.SetExInformation(baseEx);
            exForm.OnSendMail += new UCEventHandler(exForm_OnSendMail);
            exForm.OnCancelMail += new UCEventHandler(exForm_OnCancelMail);
            exForm.ShowDialog();
        }
    }
}
