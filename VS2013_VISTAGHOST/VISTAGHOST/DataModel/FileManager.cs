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

        }

        public List<FileContainer> SearchFileFromWorkHistory()
        {
            List<FileContainer> fList = new List<FileContainer>();
            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                             vgSettingConstants.VGFolder,
                                             vgSettingConstants.WorkHistoryFolder);

            var path = System.IO.Path.Combine(dir, vgSettingConstants.WorkHistoryFile);

            //if (!System.IO.File.Exists(path))
            //{
            //    using (var stream = System.IO.File.CreateText(path))
            //    {
            //        /*Create new log file based on exists file*/
            //        stream.Write(Properties.Resources.WorkHistory);
            //    }
            //}

            if (System.IO.File.Exists(path))
            {
                XDocument doc;
                doc = XDocument.Load(path, LoadOptions.SetBaseUri);
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
    }
}
