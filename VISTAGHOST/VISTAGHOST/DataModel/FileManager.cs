using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vistaghost.VISTAGHOST.Lib;
using System.Xml.Linq;

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
            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VGSettingConstants.VGFolder);
            dir = System.IO.Path.Combine(dir, VGSettingConstants.WorkHistoryFolder);

            whPath = System.IO.Path.Combine(dir, VGSettingConstants.WorkHistoryFile);
        }

        public List<FileContainer> SearchFileFromWorkHistory()
        {
            List<FileContainer> fList = new List<FileContainer>();

            if (System.IO.File.Exists(whPath))
            {
                XDocument doc;
                doc = XDocument.Load(whPath, LoadOptions.SetBaseUri);
                foreach (var sNode in doc.Root.Elements())
                {
                    if (sNode.Attribute("id").Value == "1")
                    {
                        var fNode = sNode.Element("ChangedFile");
                        foreach (var fn in fNode.Elements())
                        {
                            if (fn.Attribute("action").Value == "mod" || fn.Attribute("action").Value == "add")
                            {
                                fList.Add(new FileContainer { FileName = fn.Value });
                            }
                        }

                        break;
                    }
                }
            }

            return fList;
        }

        public List<ObjectType> SearchElementFromWorkHistory()
        {
            return null;
        }
    }
}
