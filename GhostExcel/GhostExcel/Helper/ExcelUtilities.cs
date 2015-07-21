using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Outlook = Microsoft.Office.Interop.Outlook;
using GhostExcel.DataModel;
using System.Drawing;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Net;

namespace GhostExcel
{
    public static class ExcelUtilities
    {
        static ExcelUtilities()
        {

        }

        /// <summary>
        /// Get range's address
        /// </summary>
        /// <param name="rng"></param>
        /// <returns>address string</returns>
        public static string RangeAddress(Excel.Range rng)
        {
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Get cell's address
        /// </summary>
        /// <param name="sheet">active sheet</param>
        /// <param name="row">cell's row</param>
        /// <param name="col">cell's column</param>
        /// <returns></returns>
        public static string CellAddress(Excel.Worksheet sheet, int row, int col)
        {
            return RangeAddress(sheet.Cells[row, col]);
        }

        /// <summary>
        /// Add border for cells in excel
        /// </summary>
        /// <param name="range">range of cells</param>
        /// <param name="color">border's color</param>
        public static void AddBorder(Excel.Range range, 
                                     Color color,
                                     BorderType type = BorderType.All)
        {
            Excel.Borders borders = range.Borders;

            switch (type)
            {
                //Border to all of range
                case BorderType.All:
                    {
                        borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                        borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    break;
                //Just border outside of range
                case BorderType.Around:
                    {
                        borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    break;

                case BorderType.Inside:
                    break;

                default:
                    break;
            }

            //Set border's color
            borders.Color = ColorTranslator.ToOle(color);

            // Release all objects
            ExcelCleaner.releaseObject(borders);
        }

        public static bool Find(Excel.Range range, string strFind)
        {
            bool found = false;
            Excel.Range curFind = null;

            curFind = range.Find(strFind, Type.Missing,
                                      Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                                      Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                                      Type.Missing, Type.Missing);

            if (curFind != null)
                found = true;

            return found;
        }

        /// <summary>
        /// Find string in active excel's sheet
        /// </summary>
        /// <param name="workSheet">current sheet</param>
        /// <param name="strFind">string to find</param>
        /// <param name="resultColor">result's color</param>
        /// <param name="boldResult">bold result or not</param>
        public static bool Find(Excel.Range range, string strFind, Color resultColor, bool boldResult = true)
        {
            Excel.Range currentFind = null;
            Excel.Range firstFind = null;
            bool found = false;

            // You should specify all these parameters every time you call this method, 
            // since they can be overridden in the user interface. 
            currentFind = range.Find(strFind, Type.Missing,
                                      Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                                      Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                                      Type.Missing, Type.Missing);

            while (currentFind != null)
            {
                // Keep track of the first range you find.  
                if (firstFind == null)
                {
                    firstFind = currentFind;
                }

                // If you didn't move to a new range, you are done. 
                else if (currentFind.get_Address(Excel.XlReferenceStyle.xlA1)
                    == firstFind.get_Address(Excel.XlReferenceStyle.xlA1))
                {
                    break;
                }

                currentFind.Font.Color = ColorTranslator.ToOle(resultColor);
                currentFind.Font.Bold = boldResult;

                currentFind = range.FindNext(currentFind);
            }

            //Release all COM objects
            ExcelCleaner.releaseObject(firstFind);
            ExcelCleaner.releaseObject(currentFind);

            return found;
        }

        /// <summary>
        /// Apply style to range in excel sheet
        /// </summary>
        /// <param name="range">range of cell</param>
        /// <param name="styleInfo">style's information</param>
        public static void DoApplyStyle(Excel.Range range, StyleInfo styleInfo)
        {
            if (styleInfo == null || !styleInfo.IsValidStyle())
                return;

            Excel.Style style = null;

            try
            {
                //Get style if it existed in workbook
                style = Globals.ThisAddIn.Application.ThisWorkbook.Styles[styleInfo.StyleName];
            }
            catch (Exception ex)
            {
                //Otherwise, add new style to workbook
                style = Globals.ThisAddIn.Application.ThisWorkbook.Styles.Add(styleInfo.StyleName, Type.Missing);
                ExcelLogger.LogError(ex);
            }

            //Set style's information
            style.Font.Name = styleInfo.FontName;
            style.Font.Size = styleInfo.FontSize;
            style.Font.Color = styleInfo.FontColor;
            style.Interior.Color = styleInfo.InteriorColor;
            style.Interior.Pattern = styleInfo.InteriorPattern;

            //Apply  to ranges
            range.Style = styleInfo.StyleName;

            //Release COM objects
            ExcelCleaner.releaseObject(style);
        }

        private static SmtpClient CreateSMTPClient(MailServer mailSvr)
        {
            SmtpClient smtpClient = null;

            smtpClient = new SmtpClient();

            switch (mailSvr)
            {
                case MailServer.Outlook:
                    smtpClient.Host = GhostConstants.SMTPOutlookHost;
                    break;
                case MailServer.Gmail:
                default:
                    smtpClient.Host = GhostConstants.SMTPGmailHost;
                    break;
            }

            smtpClient.Timeout = 30000; // 30000 miniseconds (30 seconds)
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Port = GhostConstants.SMTPPort;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(GhostConstants.EmailFrom, GhostConstants.MailPassword);

            return smtpClient;
        }

        /// <summary>
        /// Send feedback's mail
        /// </summary>
        /// <param name="subject">mail's subject</param>
        /// <param name="message">mail's content</param>
        /// <param name="attachmentList">attach files</param>
        /// <param name="mailSvr">mail's server</param>
        /// <returns>true - if successed; false - if failed</returns>
        public static bool SendFeedBackEmail(string subject, string message,
                                             List<Attachment> attachmentList,
                                             out string errorMsg,
                                             MailServer mailSvr = MailServer.Gmail)
        {
            MailMessage msg = null;
            bool succeed = false;
            errorMsg = String.Empty;

            try
            {
                msg = new MailMessage();
                msg.Subject = subject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = message;
                msg.BodyEncoding = Encoding.UTF8;
                msg.To.Add(GhostConstants.EmailTo);
                msg.From = new MailAddress(GhostConstants.EmailFrom);
                msg.IsBodyHtml = true;

                SmtpClient smtpClient = CreateSMTPClient(mailSvr);

                if (attachmentList != null)
                {
                    foreach (var item in attachmentList)
                    {
                        msg.Attachments.Add(item);
                    }
                }
                else
                {
                    //Get default error file
                    var attachment = GetAttachment();

                    if(attachment != null)
                        msg.Attachments.Add(attachment);
                }

                smtpClient.Send(msg);
                succeed = true;
            }
            catch (SmtpException ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message + "\n");
#endif
                ExcelLogger.LogError(ex);
                succeed = false;
                errorMsg = ex.Message;
            }

            if (msg != null)
                msg.Dispose();

            return succeed;
        }

        /// <summary>
        /// Get error file which created if existed an error while running
        /// </summary>
        /// <returns>error path</returns>
        private static string GetErrorFile()
        {
             var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GhostConstants.VGFolder);
             string errorFile = Path.Combine(dir, GhostConstants.ErrorLogFile);

             if (!File.Exists(errorFile))
                 return String.Empty;

             return errorFile;
        }

        /// <summary>
        /// Write file to memory and create stream
        /// </summary>
        /// <param name="filePath">file's path</param>
        /// <returns>stream to the file in memory</returns>
        public static Stream WriteFileToMemory(string filePath)
        {
            var memStream = new MemoryStream();
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                memStream.Write(bytes, 0, (int)file.Length);
                file.Close();
            }
            memStream.Position = 0;
            return memStream;
        }

        /// <summary>
        /// Get attach file when send email
        /// </summary>
        /// <returns></returns>
        private static Attachment GetAttachment()
        {
            Attachment attachment = null;
            string strErrorFile = String.Empty;

            strErrorFile = GetErrorFile();

            if (String.IsNullOrEmpty(strErrorFile))
                return null;

            try
            {
                var stream = WriteFileToMemory(strErrorFile);
                if (stream != null)
                {
                    attachment = new Attachment(stream, GhostConstants.AttachFileName);
                    attachment.ContentType = new ContentType(GhostConstants.ContentTypeString);
                }
            }
            catch (Exception ex)
            {
                ExcelLogger.LogError(ex);
                return null;
            }

            return attachment;
        }

        public static void FormatExcel(Excel.Worksheet worksheet)
        {
            Excel.Range range = worksheet.get_Range("B3", "D5");
            range.Value2 = "GhostVG";
            AddBorder(range, Color.Black);
            range.Columns.AutoFit();
        }
    }
}
