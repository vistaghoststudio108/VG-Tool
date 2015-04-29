using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.Network;
using System.ComponentModel;

namespace Vistaghost.VISTAGHOST.Helper
{
    class DTEHelper
    {
        public DTE Dte { get; private set; }

        public DTE2 Dte2 { get; set; }

        string type = String.Empty;

        //DocumentEvents docEvents;
        SolutionEvents solEvents;
        DTEEvents dteEvents;
        FindEvents findEvents;
        CommandEvents findinfilesEvent;

        public DTEHelper(DTE dte, DTE2 dte2)
        {
            this.Dte = dte;
            this.Dte2 = dte2;

            //docEvents = DTE.Events.DocumentEvents;
            //docEvents.DocumentOpening += docEvents_DocumentOpening;
            //docEvents.DocumentClosing += docEvents_DocumentClosing;

            findEvents = Dte.Events.FindEvents;
            findEvents.FindDone += new _dispFindEvents_FindDoneEventHandler(findEvents_FindDone);

            solEvents = Dte.Events.SolutionEvents;
            solEvents.Opened += solEvents_Opened;

            dteEvents = Dte.Events.DTEEvents;
            dteEvents.OnStartupComplete += dteEvents_OnStartupComplete;
            dteEvents.OnBeginShutdown += dteEvents_OnBeginShutdown;

            findinfilesEvent = dte2.Events.get_CommandEvents("{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 277);

            findinfilesEvent.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler(findinfilesEvent_BeforeExecute);
            findinfilesEvent.AfterExecute += new _dispCommandEvents_AfterExecuteEventHandler(findinfilesEvent_AfterExecute);
        }

        void findinfilesEvent_AfterExecute(string Guid, int ID, object CustomIn, object CustomOut)
        {
        }

        void findinfilesEvent_BeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {
            // Cancel Find in Files event by setting CancelDefault = true
            //CancelDefault = true;
        }

        void findEvents_FindDone(vsFindResult Result, bool Cancelled)
        {
            if (Result == vsFindResult.vsFindResultFound)
            {
                VGSetting.Instance.FileList = Vistaghost.VISTAGHOST.Lib.VGOperations.GetFileFromResultWindow(this.Dte, FileFilter.ffSource);

                //BackgroundWorker bw = new BackgroundWorker();
                //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                //bw.RunWorkerAsync();
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        void docEvents_DocumentClosing(Document Document)
        {
        }

        void docEvents_DocumentOpening(string DocumentPath, bool ReadOnly)
        {
        }

        void dteEvents_OnBeginShutdown()
        {
        }

        void wndEvents_WindowActivated(Window GotFocus, Window LostFocus)
        {
        }

        void dteEvents_OnStartupComplete()
        {
        }


        public void SetStatusBarText(string text)
        {
            Dte.StatusBar.Text = text;
        }

        public void AddErrorToErrorListWindow(string error)
        {
            ErrorListProvider errorProvider = new ErrorListProvider(VISTAGHOSTPackage.Current);
            Microsoft.VisualStudio.Shell.Task newError = new Microsoft.VisualStudio.Shell.Task();
            newError.Category = TaskCategory.BuildCompile;
            newError.Text = error;
            errorProvider.Tasks.Add(newError);
        }

        public void OutputWindowWriteLine(string text)
        {
            const string DEBUG_OUTPUT_PANE_GUID = "{FC076020-078A-11D1-A7DF-00A0C9110051}";
            EnvDTE.Window window = (EnvDTE.Window)VISTAGHOSTPackage.Current.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            window.Visible = true;
            EnvDTE.OutputWindow outputWindow = (EnvDTE.OutputWindow)window.Object;
            foreach (EnvDTE.OutputWindowPane outputWindowPane in outputWindow.OutputWindowPanes)
            {
                if (outputWindowPane.Guid.ToUpper() == DEBUG_OUTPUT_PANE_GUID)
                {
                    outputWindowPane.OutputString(text + "\r\n");
                }
            }
        }

        public StatusBar GetStatusBar()
        {
            return Dte.StatusBar;
        }

        void solEvents_Opened()
        {
            if (!VISTAGHOSTPackage.Current.IsOpened)
            {
                VISTAGHOSTPackage.Current.AddCommandBars();
                VISTAGHOSTPackage.Current.ShowToolBar(true);

                VISTAGHOSTPackage.Current.IsOpened = true;
            }

            /*If not register, send request for register to website*/
            if (!VGSetting.SettingData.DataInfo.RegisteredOnWeb)
            {
                VGProduct.RegisterProduct();
            }
        }

        public void ExecuteCmd(string cmd, string args)
        {
            if (Dte == null) return;
            Dte.ExecuteCommand(cmd, args);
        }
    }
}
