using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostExcel.DataModel
{
    public static class GhostConstants
    {
        public const string Version = "1.0.0";
        public const string VGFolder = "Vistaghost";
        public const string ErrorLogFile = "ExcelErrorLog.txt";
        public const string DefaultStyleName = "GhostExcelStyle";

        //Mail
        public const string EmailTo = "phamvanthuankhmt05uit@gmail.com";
        public const string EmailFrom = "thuanpv.uit@gmail.com";
        public const string AttachFileName = "error_file.txt";
        public const string ContentTypeString = "application/vnd.ms-excel";
        public const int SMTPPort = 587;
        public const string SMTPGmailHost = "smtp.gmail.com";
        public const string SMTPOutlookHost = "smtp.outlook.com";
        public const string MailSubject = "Error about GhostExcel Add-in";
        public const long MaxAttachFileSize = 5000000; //5MB
        public const string MailPassword = "phamvanthuan";
        public const string MailUserToken = "test message 1";

    }
}
