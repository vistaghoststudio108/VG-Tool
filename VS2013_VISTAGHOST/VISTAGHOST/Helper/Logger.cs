﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vistaghost.VISTAGHOST.Lib;
using System.Xml;
using System.Xml.Linq;

namespace Vistaghost.VISTAGHOST.Helper
{
    class Logger
    {
        ///<summary>
        ///Log a runtime error.
        ///</summary>
        public static void LogError(Exception e, bool emptyfunciton = false)
        {
            if (emptyfunciton)
                return;

            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            try
            {
                string path = Path.Combine(dir, vgSettingConstants.ErrorLogFile);

                Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
                json["Version"] = vgSettingConstants.Version;
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
        public static void LogMessage(string message, bool emptyfunction = false)
        {
            if (emptyfunction)
                return;

            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            try
            {
                string path = Path.Combine(dir, vgSettingConstants.ErrorLogFile);

                Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
                json["Version"] = vgSettingConstants.Version;
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

        public static void LogHistory(LogFileType type, string path, int line, ActionType mode, string find, string replace, int numPhrase, string prototype)
        {
            try
            {
                var date = vgOperations.GetDateString(DateFormat.FullDate);
                var account = vgSetting.SettingData.CommentInfo.Account;
                var logPath = String.Empty;
                var dir = vgSetting.SettingData.HistoryInfo.LogPath;

                if (String.IsNullOrEmpty(dir))
                {
                    dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder);
                    dir = Path.Combine(dir, vgSettingConstants.LogFolder);
                }

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                switch (type)
                {
                    case LogFileType.TextFile:
                        {
                            logPath = Path.Combine(dir, vgSettingConstants.LogTextFile);

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
                            logPath = Path.Combine(dir, vgSettingConstants.LogXmlFile);
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

                            if (File.Exists(logPath))
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

                                XElement funcNode = new XElement("Func");
                                funcNode.Value = prototype;

                                XElement lineNode = new XElement("Line");
                                lineNode.Value = line.ToString();

                                historyNode.Add(accountNode);
                                historyNode.Add(modeNode);
                                historyNode.Add(fileNode);
                                historyNode.Add(funcNode);
                                historyNode.Add(lineNode);

                                RootNode.Add(historyNode);

                                Doc.Save(logPath);
                            }
                        }
                        break;
                    case LogFileType.Excel:
                        {
                            logPath = Path.Combine(dir, vgSettingConstants.LogExcelFile);
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
