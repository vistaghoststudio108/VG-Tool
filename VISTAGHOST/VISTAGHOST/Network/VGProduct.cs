using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using Vistaghost.VISTAGHOST.Helper;

namespace Vistaghost.VISTAGHOST.Network
{
    public static class VGProduct
    {
        public static string UniqueAddress { get; set; }

        static VGProduct()
        {
            UniqueAddress = GetUniqueID();
        }

        static string GetUniqueID()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return cpuInfo;
        }

        public static bool RegisterProduct()
        {
            bool succeeded = false;
            try
            {
                if (String.IsNullOrEmpty(UniqueAddress))
                {
                    return false;
                }

                System.Net.WebRequest req = System.Net.WebRequest.Create(string.Format("http://localhost:5110/Vistaghost/Welcome?name={0}&age={1}", UniqueAddress, DateTime.Now.ToShortDateString()));

                req.Method = "POST";
                req.ContentLength = 0;
                req.Timeout = 1000;
                req.BeginGetResponse(null, null);

                succeeded = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return succeeded;
        }

        public static void UploadLog()
        {
            try
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VGSettingConstants.LogFolder);
                string path = Path.Combine(dir, VGSettingConstants.ErrorLogFile);

                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path, new UTF8Encoding(false));

                    byte[] dat = UTF8Encoding.UTF8.GetBytes("data=" + reader.ReadToEnd());

                    System.Net.WebRequest req = System.Net.HttpWebRequest.Create("http://vistaghost-product.com/");
                    req.Method = "POST";
                    req.ContentLength = dat.Length;
                    req.ContentType = "application/x-www-form-urlencoded";
                    var resstream = req.GetRequestStream();
                    resstream.Write(dat, 0, dat.Length);
                    resstream.Dispose();

                    req.Timeout = 3000;
                    req.BeginGetResponse((ar) =>
                    {
                        var resp = req.EndGetResponse(ar) as System.Net.HttpWebResponse;
                        byte[] buf = new byte[resp.ContentLength];
                        var respstream = resp.GetResponseStream();
                        respstream.Read(buf, 0, buf.Length);
                        respstream.Dispose();
                        if (buf.Length == 1 && buf[0] == 49)
                        {
                            //POST succeeded

                        }
                        resp.Close();
                    }, null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
    }
}
