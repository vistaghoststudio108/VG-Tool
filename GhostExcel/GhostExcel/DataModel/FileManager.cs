using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GhostExcel.DataModel
{
    public class FileManager
    {
        private static FileManager _instance;

        public static FileManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FileManager();

                return _instance;
            }
        }

        private FileManager()
        {

        }

        public List<Function> SearchInFile()
        {
            string shPath = String.Empty;
            shPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                            GhostConstants.VGFolder,
                                            GhostConstants.SharedFolder,
                                            GhostConstants.SharedFile);

            List<Function> funcList = new List<Function>();

            if(!System.IO.File.Exists(shPath))
            {
                return funcList;
            }
            
            try
            {
                var doc = XDocument.Load(shPath, LoadOptions.SetBaseUri);
                var funcNode = doc.Root.Element("Function");
                if (funcNode != null)
                {
                    foreach (var item in funcNode.Elements("Func"))
                    {
                        var func = new Function
                        {
                            Name = item.Element("Name").Value,
                            Location = item.Element("Location").Value,
                        };

                        funcList.Add(func);
                    }
                }
            }
            catch (Exception ex)
            {
                ExcelLogger.LogError(ex);
                return funcList;
            }

            return funcList;
        }
    }
}
