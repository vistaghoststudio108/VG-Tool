﻿using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST.DataModel
{
    public class FileManager
    {
        static FileManager _instance;
        private string whPath = String.Empty; // work history path
        private static readonly object fsLock = new object();
        public static FileManager Instance
        {
            get
            {
                if (_instance == null) _instance = new FileManager();
                return _instance;
            }
        }

        public bool SearchCanceled { get; set; }

        public FileManager()
        {
            CheckFile(Properties.Resources.WorkHistory);
            SearchCanceled = false;
        }

        /// <summary>
        /// Check the exist of the hisory file
        /// </summary>
        bool CheckFile(string resourceFile, string desFolder = vgSettingConstants.WorkHistoryFolder,
                       string desFile = vgSettingConstants.WorkHistoryFile, bool needStarted = true)
        {
            if (needStarted && !vgSetting.ProjectStatus.Started)
                return false;

            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                             vgSettingConstants.VGFolder,
                                             desFolder);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            this.whPath = System.IO.Path.Combine(dir, desFile);
            if (!File.Exists(whPath))
            {
                using (var stream = File.CreateText(whPath))
                {
                    stream.Write(resourceFile);
                    //File.SetAttributes(whPath, FileAttributes.ReadOnly | FileAttributes.Encrypted | FileAttributes.System);
                }
            }

            return true;
        }

        public void FixCorruptedFiles()
        {
            if (!CheckFile(Properties.Resources.WorkHistory))
                 return;

            XDocument doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
            if (doc != null)
            {
            }
        }

        public void UpdateStatus()
        {
            if (!CheckFile(Properties.Resources.WorkHistory))
                return;

            XDocument doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
            if (doc != null)
            {
                doc.Root.Attribute("id").Value = vgSetting.ProjectStatus.ProjectID;
                doc.Root.Attribute("from").Value = DateTime.Now.ToShortDateString();
                doc.Root.Attribute("project").Value = vgSetting.ProjectStatus.ProjectName;
                doc.Root.Attribute("author").Value = Environment.MachineName;

                // Delete all old datas
                doc.Root.Element("ChangedFile").RemoveAll();
                doc.Root.Element("CodeElement").Element("Function").RemoveAll();
                doc.Root.Element("CodeElement").Element("Class").RemoveAll();
                doc.Root.Element("CodeElement").Element("Enum").RemoveAll();
                doc.Root.Element("CodeElement").Element("Struct").RemoveAll();
                doc.Root.Element("Comment").RemoveAll();

                doc.Save(whPath);
            }
        }

        public List<FileContainer> SearchFileFromWorkHistory(out string message)
        {
            List<FileContainer> fileList = new List<FileContainer>();
            message = String.Empty;

            if (File.Exists(whPath))
            {
                XDocument doc;
                try
                {
                    doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                    if (doc.Root.Attribute("id").Value == vgSetting.ProjectStatus.ProjectID)
                    {
                        var fNode = doc.Root.Element("ChangedFile");
                        foreach (var fn in fNode.Elements())
                        {
                            fileList.Add(new FileContainer { FileName = fn.Value });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }

                if (fileList.Count == 0)
                    message = "There is no changed items in work history";
            }
            else
                message = "File not found";

            return fileList;
        }

        public List<ObjectType> SearchElementFromWorkHistory(SearchType sType, ActionType acType)
        {
            List<ObjectType> funcList = new List<ObjectType>();

            if (System.IO.File.Exists(whPath))
            {
                XDocument doc;
                doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                foreach (var sNode in doc.Root.Elements())
                {
                    if (sNode.Attribute("id").Value == vgSetting.ProjectStatus.ProjectID)
                    {
                        var fNode = sNode.Element("CodeElement");
                        foreach (var fn in fNode.Elements())
                        {
                            switch (sType)
                            {
                                case SearchType.Function:
                                    {
                                        if (fn.Attribute("type").Value == "function")
                                        {
                                            switch (acType)
                                            {
                                                case ActionType.MODIFY:
                                                    {
                                                        if (fn.Attribute("action").Value == "mod")
                                                        {
                                                            var f = new ObjectType();
                                                            f.Name = fn.Element("name").Value;
                                                            f.Path = fn.Element("path").Value;
                                                            funcList.Add(f);
                                                        }
                                                    }
                                                    break;
                                                case ActionType.ADD:
                                                    {
                                                        if (fn.Attribute("action").Value == "add")
                                                        {
                                                            var f = new ObjectType();
                                                            f.Name = fn.Element("name").Value;
                                                            f.Path = fn.Element("path").Value;
                                                            funcList.Add(f);
                                                        }
                                                    }
                                                    break;
                                                case ActionType.DELETE:
                                                    {
                                                        if (fn.Attribute("action").Value == "del")
                                                        {
                                                            var f = new ObjectType();
                                                            f.Name = fn.Element("name").Value;
                                                            f.Path = fn.Element("path").Value;
                                                            funcList.Add(f);
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case SearchType.Class:
                                    break;
                                case SearchType.Enumerable:
                                    break;
                                case SearchType.Structure:
                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    }
                }
            }

            return funcList;
        }

        #region Search method

        List<VGCodeElement> GetElementRange(EnvDTE.DTE dte, string path)
        {
            Document doc;
            FileCodeModel fcm;
            bool bOpen = false;
            List<VGCodeElement> eRanges = new List<VGCodeElement>();

            if (dte.ItemOperations.IsFileOpen(path, Constants.vsViewKindCode))
            {
                doc = dte.Documents.Item(path);
            }
            else
            {
                doc = dte.ItemOperations.OpenFile(path, Constants.vsViewKindCode).Document;
                bOpen = true;
            }

            if (doc == null || doc.ProjectItem == null || doc.ProjectItem.FileCodeModel == null)
            {
                return eRanges;
            }

            fcm = doc.ProjectItem.FileCodeModel;
            var codeFuncs = fcm.CodeElements.OfType<CodeFunction>();
            if (codeFuncs.Count() == 0)
            {
                return eRanges;
            }

            foreach (var codeFunc in codeFuncs)
            {
                var ce = new VGCodeElement(path,
                    codeFunc.get_Prototype((int)((vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType | vsCMPrototype.vsCMPrototypeFullname))),
                    codeFunc.StartPoint.Line,
                    codeFunc.EndPoint.Line);

                eRanges.Add(ce);
            }

            if (bOpen)
            {
                bOpen = false;
                // dte.Documents.Item(path).Close(vsSaveChanges.vsSaveChangesYes);
            }

            return eRanges;
        }

        public IEnumerable<VGCodeElement> SearchInFile(DTE dte, string file, string keyword, bool CaseSensitive)
        {
            List<VGCodeElement> codeElements = new List<VGCodeElement>();
            file = Path.GetFullPath(file);

            if (File.Exists(file))
            {
                var options = RegexOptions.None;
                if (!CaseSensitive)
                    options = RegexOptions.IgnoreCase;

                Regex reg = new Regex(Regex.Escape(keyword), options);

                List<string> blocks = new List<string>();
                List<string> lines = new List<string>();
                List<VGCodeElement> ceList = new List<VGCodeElement>();

                ceList = GetElementRange(dte, file);
                if (ceList.Count == 0)
                {
                    yield break;
                }

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (sr.Peek() > 0)
                        {
                            lines.Add(sr.ReadLine());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    yield break;
                }

                foreach (var r in ceList)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = r.BeginLine - 1; i <= r.EndLine - 1; i++)
                    {
                        sb.AppendLine(lines[i]);
                    }

                    blocks.Add(sb.ToString());
                }

                for (int i = 0; i < blocks.Count; i++)
                {
                    var match = reg.Match(blocks[i]);
                    if (match.Success)
                    {
                        var lm = new VGCodeElement(file, ceList[i].Name, ceList[i].BeginLine, ceList[i].EndLine);
                        lm.Preview = blocks[i];
                        yield return lm;
                    }

                    if (this.SearchCanceled)
                    {
                        this.SearchCanceled = false;
                        yield break;
                    }
                }
            }
        }

        static XElement GetExistNode(string name, XElement parent)
        {
            if (parent == null)
                return null;

            foreach (var xnode in parent.Elements())
            {
                if (xnode.Element("Name").Value.Contains(name))
                    return xnode;
            }

            return null;
        }

        public void UpdateWorkHistory(CodeElement Element, ActionType type)
        {
            switch (type)
            {
                case ActionType.MODIFY:
                    break;
                case ActionType.ADD:
                    {
                        try
                        {
                            var codeFunc = (CodeFunction)Element;
                            var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                            var groupNode = doc.Root.Element("CodeElement").Element("Function");
                            if (groupNode != null)
                            {
                                var eNode = new XElement("Func");
                                XElement nameNode = new XElement("Name");
                                nameNode.Value = codeFunc.FullName;//codeFunc.get_Prototype((int)((vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType | vsCMPrototype.vsCMPrototypeFullname)));

                                XElement typeNode = new XElement("Type");
                                typeNode.Value = "Add";

                                XElement fileNode = new XElement("File");
                                fileNode.Value = codeFunc.ProjectItem.Document.FullName;

                                XElement lineNode = new XElement("Line");
                                lineNode.Value = codeFunc.StartPoint.Line.ToString();

                                eNode.Add(nameNode);
                                eNode.Add(typeNode);
                                eNode.Add(fileNode);
                                eNode.Add(lineNode);

                                groupNode.Add(eNode);
                                doc.Save(whPath);
                            }
                        }
                        catch
                        {
                        }
                    }
                    break;
                case ActionType.DELETE:
                    {
                        var codeFunc = (CodeFunction)Element;
                        var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                        var groupNode = doc.Root.Element("CodeElement").Element("Function");

                        XElement delNode = GetExistNode(codeFunc.Name, groupNode);

                        if (delNode != null)
                        {
                            delNode.Remove();
                            doc.Save(whPath);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        public void SaveNewElements(List<VGCodeElement> codeElements)
        {
            if (!CheckFile(Properties.Resources.WorkHistory))
                return;

            try
            {
                var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                var groupNode = doc.Root.Element("CodeElement").Element("Function");
                if (groupNode != null)
                {
                    foreach (var ce in codeElements)
                    {
                        var eNode = new XElement("Func");
                        XAttribute dateAttr = new XAttribute("date", DateTime.Now.ToLongDateString());
                        eNode.Add(dateAttr);

                        XElement nameNode = new XElement("Name");
                        nameNode.Value = ce.Name;

                        XElement fileNode = new XElement("File");
                        fileNode.Value = ce.File;

                        eNode.Add(nameNode);
                        eNode.Add(fileNode);

                        groupNode.Add(eNode);
                    }

                    doc.Save(whPath);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        bool ExistNode(string value, XElement parent)
        {
            if (parent == null)
                return false;

            foreach (var node in parent.Elements())
            {
                if (node.Value == value)
                    return true;
            }

            return false;
        }

        public void SaveFileChanged(string filename)
        {
            if (!CheckFile(Properties.Resources.WorkHistory))
                return;

            try
            {
                var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                var groupNode = doc.Root.Element("ChangedFile");
                if (groupNode != null)
                {
                    var eNode = new XElement("File");
                    eNode.Value = filename;

                    XAttribute dateAttr = new XAttribute("date", DateTime.Now.ToShortDateString());
                    eNode.Add(dateAttr);

                    groupNode.Add(eNode);
                    doc.Save(whPath);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        public void RemoveElement(string eName)
        {
            if (!CheckFile(Properties.Resources.WorkHistory))
                return;

            try
            {
                var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                var groupNode = doc.Root.Element("CodeElement").Element("Function");
                if (groupNode != null)
                {
                    XElement delNode = GetExistNode(eName, groupNode);

                    if (delNode != null)
                    {
                        delNode.Remove();
                        doc.Save(whPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        public void SaveFoundFiles(List<VGCodeElement> fileList)
        {
            if (!CheckFile(Properties.Resources.SharedFile, vgSettingConstants.SharedFolder, vgSettingConstants.SharedFile, false))
                return;

            try
            {
                var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                var funcNode = doc.Root.Element("Function");

                if (funcNode != null)
                {
                    funcNode.RemoveAll();

                    foreach (var file in fileList)
                    {
                        var fNode = new XElement("Func");

                        var nameNode = new XElement("Name");
                        nameNode.Value = file.Name;

                        var locationNode = new XElement("Location");
                        locationNode.Value = file.File;

                        fNode.Add(nameNode);
                        fNode.Add(locationNode);

                        funcNode.Add(fNode);
                    }

                    doc.Save(whPath);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
    }
}
