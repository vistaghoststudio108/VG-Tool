using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Vistaghost.VISTAGHOST.Lib;
using System.Xml;
using System.Xml.Linq;
namespace Vistaghost.VISTAGHOST.Helper
{
    class Logger
    {///<summary>
        ///Log a runtime error.
        ///</summary>
        public static void LogError(Exception e)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VGSettingConstants.VGFolder);
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
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VGSettingConstants.VGFolder);
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

        /// <summary>
        /// write history to file
        /// </summary>
        /// <param name="history"></param>
        public static void LogHistory(LogFileType type, string path, int line, ActionType mode, string find, string replace, int numPhrase)
        {
            try
            {
                var date = VGOperations.GetDateString(DateFormat.FullDate);
                var account = VGSetting.SettingData.CommentInfo.Account;
                var logPath = String.Empty;
                var dir = VGSetting.SettingData.HeaderInfo.LogPath;

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                switch (type)
                {
                    case LogFileType.TextFile:
                        {
                            logPath = Path.Combine(dir, VGSettingConstants.LogTextFile);

                            using (StreamWriter writer = new StreamWriter(logPath, true, new UTF8Encoding(false)))
                            {
                                writer.WriteLine(date + "   [" + account + "]    " + "[" + mode.ToString() + "] " + path + "(" + line.ToString() + ")");

                                if (numPhrase != -1)
                                {
                                    writer.WriteLine("-> Replace '" + find + "'" + " to '" + replace + "' succeeded (" + numPhrase.ToString() + " matched)");
                                }

                                /*end of log*/
                                writer.WriteLine("---------------------------------------------------------------");
                            }
                        }
                        break;
                    case LogFileType.Xml:
                        {
                            logPath = Path.Combine(dir, VGSettingConstants.LogXmlFile);
                            if (!File.Exists(logPath))
                            {
                                using (var stream = File.CreateText(logPath))
                                {
                                    /*Create new log file based on exists file*/
                                    stream.Write(Properties.Resources.Log);
                                }
                            }
                            XDocument Doc;
                            XElement RootNode;

                            if(File.Exists(logPath))
                            {
                                Doc = XDocument.Load(logPath, LoadOptions.SetBaseUri);

                                RootNode = Doc.Root.Element("VGHistory");
                                if (RootNode == null)
                                {
                                    RootNode = new XElement("VGHistory");
                                    Doc.Root.Add(RootNode);
                                }

                                XElement historyNode = new XElement("History");
                                XAttribute dateAttribute = new XAttribute("date", date);
                                historyNode.Add(dateAttribute);

                                XElement accountNode = new XElement("Account");
                                accountNode.Value = account;

                                XElement modeNode = new XElement("Mode");
                                modeNode.Value = mode.ToString();

                                XElement fileNode = new XElement("File");
                                fileNode.Value = path;

                                XElement lineNode = new XElement("Line");
                                lineNode.Value = line.ToString();

                                historyNode.Add(accountNode);
                                historyNode.Add(modeNode);
                                historyNode.Add(fileNode);
                                historyNode.Add(lineNode);

                                RootNode.Add(historyNode);

                                Doc.Save(logPath);
                            }
                        }
                        break;
                    case LogFileType.Excel:
                        {
                            logPath = Path.Combine(dir, VGSettingConstants.LogExcelFile);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }
}
