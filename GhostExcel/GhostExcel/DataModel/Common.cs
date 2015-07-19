using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace GhostExcel.DataModel
{
    /// <summary>
    /// Border's type enum
    /// </summary>
    public enum BorderType
    {
        Around = 0,
        Inside,
        All
    }

    public class StyleInfo
    {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public int FontColor { get; set; }
        public int InteriorColor { get; set; }
        public Excel.XlPattern InteriorPattern { get; set; }
        public string StyleName { get; set; }

        /// <summary>
        /// Constructor for initialize all properties's value
        /// </summary>
        public StyleInfo()
        {
            StyleName = GhostConstants.DefaultStyleName;
            InteriorPattern = Excel.XlPattern.xlPatternSolid;
            FontName = String.Empty;
            FontSize = -1;
            FontColor = -1;
            InteriorColor = -1;
        }

        /// <summary>
        /// Check the validate of style's information
        /// </summary>
        /// <returns>true - if valid; false - if other</returns>
        public bool IsValidStyle()
        {
            if(String.IsNullOrEmpty(FontName) ||
               FontSize == -1 ||
               FontColor == -1 ||
               InteriorColor == -1)
            {
                return false;
            }

            return true;
        }
    }

    public class MailData
    {
        public MailServer MailSvr { get; set; }
        public string Message { get; set; }
        public object Attachments { get; set; }

        public MailData()
        {
            MailSvr = MailServer.Gmail;
            Message = String.Empty;
            Attachments = null;
        }
    }

    public enum MailServer
    {
        Gmail = 0,
        Outlook,
        Yahoo,
        Facebook,
        MicrosoftLive,
        None
    }

    public class Function
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool? Status { get; set; }
        public Function(string name, string location, bool status)
        {
            this.Name = name;
            this.Location = location;
            this.Status = status;
        }
    }
}
