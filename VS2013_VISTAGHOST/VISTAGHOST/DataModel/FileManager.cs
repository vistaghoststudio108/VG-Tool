using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST.DataModel
{
    public class FileManager
    {
        static FileManager _instance;
        private string whPath = String.Empty; // work history path
        public static FileManager Instance
        {
            get
            {
                if (_instance == null) _instance = new FileManager();
                return _instance;
            }
        }

        public FileManager()
        {
            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                             vgSettingConstants.VGFolder,
                                             vgSettingConstants.WorkHistoryFolder);

            whPath = System.IO.Path.Combine(dir, vgSettingConstants.WorkHistoryFile);
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
                                case SearchType.AllFunction:
                                    {
                                        if(fn.Attribute("type").Value == "function")
                                        {
                                            switch (acType)
                                            {
                                                case ActionType.MODIFY:
                                                    {
                                                        if(fn.Attribute("action").Value == "mod")
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
    }
}
