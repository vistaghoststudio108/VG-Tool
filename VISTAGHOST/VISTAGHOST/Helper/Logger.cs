using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Vistaghost.VISTAGHOST.Lib;
namespace Vistaghost.VISTAGHOST.Helper
{
    class Logger
    {///<summary>
        ///Log a runtime error.
        ///</summary>
        public static void LogError(Exception e)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VGSettingConstants.ExportFolder);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            try
            {
                string path = Path.Combine(dir, VGSettingConstants.ErrorLogFile);

                Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
                json["Version"] = VGSettingConstants.Version;
                json["Type"] = e.GetType().FullName;
                json["Time"] = DateTime.Now.ToString();
                json["Position"] = string.Format("{0}--->{1}", e.Source, e.TargetSite);
                json["Message"] = e.Message;
                json["StackTrace"] = e.StackTrace;

                using (StreamWriter writer = new StreamWriter(path, true, new UTF8Encoding(false)))
                {
                    writer.WriteLine(json.ToString());
                    writer.WriteLine();
                }
            }
            catch { }
        }

        ///<summary>
        ///Log a runtime error message.
        ///</summary>
        public static void LogMessage(string message)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VGSettingConstants.ExportFolder);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            try
            {
                string path = Path.Combine(dir, VGSettingConstants.ErrorLogFile);

                Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
                json["Version"] = VGSettingConstants.Version;
                json["Type"] = "LogMessage";
                json["Time"] = DateTime.Now.ToString();
                json["Message"] = message;

                using (StreamWriter writer = new StreamWriter(path, true, new UTF8Encoding(false)))
                {
                    writer.WriteLine(json.ToString());
                    writer.WriteLine();
                }
            }
            catch { }
        }
    }
}
