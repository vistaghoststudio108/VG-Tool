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

namespace Vistaghost.VISTAGHOST.Helper
{
    class DTEHelper
    {
        public DTE Dte { get; private set; }

        public DTE2 Dte2 { get; set; }

        string type = String.Empty;

        //DocumentEvents docEvents;
        SolutionEvents solEvents;
        //WindowEvents wndEvents;
        DTEEvents dteEvents;
        FindEvents findEvents;

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

            //wndEvents = DTE.Events.WindowEvents;
            //wndEvents.WindowActivated += wndEvents_WindowActivated;

            dteEvents = Dte.Events.DTEEvents;
            dteEvents.OnStartupComplete += dteEvents_OnStartupComplete;
            dteEvents.OnBeginShutdown += dteEvents_OnBeginShutdown;
        }

        void findEvents_FindDone(vsFindResult Result, bool Cancelled)
        {
            if (Result == vsFindResult.vsFindResultNotFound)
            {
                var files = VGOperations.GetFileFromResultWindow(this.Dte);
            }
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
