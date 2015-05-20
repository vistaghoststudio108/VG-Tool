using EnvDTE;
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
        private static string whPath = String.Empty; // work history path
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
            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                             vgSettingConstants.VGFolder,
                                             vgSettingConstants.WorkHistoryFolder);

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            whPath = System.IO.Path.Combine(dir, vgSettingConstants.WorkHistoryFile);
            if(!File.Exists(whPath))
            {
                using (var stream = File.CreateText(whPath))
                {
                    /*Create new log file based on exists file*/
                    stream.Write(Properties.Resources.WorkHistory);
                }
            }

            SearchCanceled = false;
        }

        public List<FileContainer> SearchFileFromWorkHistory()
        {
            List<FileContainer> fileList = new List<FileContainer>();

            if (System.IO.File.Exists(whPath))
            {
                XDocument doc;
                doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                foreach (var sNode in doc.Root.Elements())
                {
                    if (sNode.Attribute("id").Value == vgSetting.ProjectStatus.ProjectID.ToString())
                    {
                        var fNode = sNode.Element("ChangedFile");
                        foreach (var fn in fNode.Elements())
                        {
                            if (fn.Attribute("action").Value == "mod" || fn.Attribute("action").Value == "add")
                            {
                                fileList.Add(new FileContainer { FileName = fn.Value });
                            }
                        }

                        break;
                    }
                }
            }

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
                    if (sNode.Attribute("id").Value == vgSetting.ProjectStatus.ProjectID.ToString())
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

        public static void UpdateWorkHistory(CodeElement Element, ActionType type)
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
                        catch (Exception ex)
                        {
                            Logger.LogError(ex);
                        }
                    }
                    break;
                case ActionType.DELETE:
                    {
                        var codeFunc = (CodeFunction)Element;
                        var doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                        var groupNode = doc.Root.Element("CodeElement").Element("Function");

                        XElement delNode = GetExistNode(codeFunc.Name, groupNode);

                        if(delNode != null)
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
    }
}
