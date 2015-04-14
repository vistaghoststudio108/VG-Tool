using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using Vistaghost.VISTAGHOST.Helper;
using System.Net;
using System.Threading;
using System.ComponentModel;

namespace Vistaghost.VISTAGHOST.Network
{
    public class RequestState
    {
        // This class stores the State of the request. 
        const int BUFFER_SIZE = 512;
        public StringBuilder requestData;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;
        public RequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
            request = null;
            streamResponse = null;
        }
    }

    public static class VGProduct
    {
        public static string UniqueAddress { get; set; }
        static bool Succeeded;
        const int BUFFER_SIZE = 512;

        static VGProduct()
        {
            UniqueAddress = GetUniqueID();
            Succeeded = false;
        }

        static string GetUniqueID()
        {
            string cpuInfo = string.Empty;
            try
            {
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
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return cpuInfo;
        }

        public static void RegisterProduct()
        {
            try
            {
                if (String.IsNullOrEmpty(UniqueAddress))
                {
                    return;
                }

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.RunWorkerAsync();   
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a HttpWebrequest object to the desired URL. 
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://vistaghost-product.somee.com/Register?id={0}&date={1}", UniqueAddress, DateTime.Now.ToShortDateString()));

            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.ContentLength = 0;
            myHttpWebRequest.Timeout = 5000;

            RequestState myRequestState = new RequestState();
            myRequestState.request = myHttpWebRequest;


            // Start the asynchronous request.
            IAsyncResult result = (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);
        }


        private static void ReadCallBack(IAsyncResult asyncResult)
        {
            try
            {
                RequestState myRequestState = (RequestState)asyncResult.AsyncState;
                Stream responseStream = myRequestState.streamResponse;
                int read = responseStream.EndRead(asyncResult);
                // Read the HTML page and then print it to the console. 
                if (read > 0)
                {
                    // POST succeeded. Do everthing to store this info
                    VGSetting.SettingData.DataInfo.ProcessorID = UniqueAddress;
                    VGSetting.SettingData.DataInfo.BeginDate = DateTime.Now.ToShortDateString();
                    VGSetting.SettingData.DataInfo.RegisteredOnWeb = true;

                    VGSetting.SaveSettings();
                }

                responseStream.Close();

            }
            catch (WebException wEx)
            {
                Logger.LogError(wEx);
            }
        }

        private static void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                // State of request is asynchronous.
                RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest myHttpWebRequest = myRequestState.request;
                myRequestState.response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

                // Read the response into a Stream object.
                Stream responseStream = myRequestState.response.GetResponseStream();
                myRequestState.streamResponse = responseStream;

                // Begin the Reading of the contents of the HTML page and print it to the console.
                IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);

                // Release the HttpWebResponse resource.
                myRequestState.response.Close();
                return;
            }
            catch (WebException wEx)
            {
                Logger.LogMessage(wEx.Message);
            }
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

                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://vistaghost-product.com/");
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
