using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.Network;

namespace Vistaghost.VISTAGHOST.Helper
{
    class DTEHelper
    {
        public DTE DTE { get; private set; }
        public DTE2 DTE2 { get; set; }

        DocumentEvents docEvents;
        SolutionEvents solEvents;
        WindowEvents wndEvents;
        DTEEvents dteEvents;
        FindEvents findEvents;
        CommandEvents findinfilesEvent;
        SelectionEvents selEvent;
        CodeModelEvents codeModelEvent;

        public DTEHelper(DTE dte, DTE2 dte2)
        {
            this.DTE = dte;
            this.DTE2 = dte2;

            // Document events
            docEvents = DTE.Events.DocumentEvents;
            docEvents.DocumentOpening += docEvents_DocumentOpening;
            docEvents.DocumentClosing += docEvents_DocumentClosing;
            docEvents.DocumentOpened += docEvents_DocumentOpened;
            docEvents.DocumentSaved += docEvents_DocumentSaved;

            // Solution events
            solEvents = DTE.Events.SolutionEvents;
            solEvents.Opened += solEvents_Opened;

            // Window events
            wndEvents = DTE.Events.WindowEvents;
            wndEvents.WindowActivated += wndEvents_WindowActivated;

            // DTE events
            dteEvents = DTE.Events.DTEEvents;
            dteEvents.OnStartupComplete += dteEvents_OnStartupComplete;
            dteEvents.OnBeginShutdown += dteEvents_OnBeginShutdown;

            // Find events
            findEvents = DTE.Events.FindEvents;
            findEvents.FindDone += findEvents_FindDone;

            findinfilesEvent = dte2.Events.get_CommandEvents("{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 277);
            findinfilesEvent.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler(findinfilesEvent_BeforeExecute);
            findinfilesEvent.AfterExecute += new _dispCommandEvents_AfterExecuteEventHandler(findinfilesEvent_AfterExecute);

            //Code model event
            codeModelEvent = ((EnvDTE80.Events2)dte.Events).get_CodeModelEvents();
            codeModelEvent.ElementAdded += codeModelEvent_ElementAdded;
            codeModelEvent.ElementChanged += codeModelEvent_ElementChanged;
            codeModelEvent.ElementDeleted += codeModelEvent_ElementDeleted;
        }

        void codeModelEvent_ElementDeleted(object Parent, CodeElement Element)
        {
            
        }

        void codeModelEvent_ElementChanged(CodeElement Element, vsCMChangeKind Change)
        {
            
        }

        void codeModelEvent_ElementAdded(CodeElement Element)
        {
            
        }

        void docEvents_DocumentSaved(Document Document)
        {
            var fcm = Document.ProjectItem.FileCodeModel;
            var cf = fcm.CodeElements.OfType<CodeFunction>();
        }

        void docEvents_DocumentOpened(Document Document)
        {
            
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
            vgSetting.Instance.FileList.Clear();

            switch (Result)
            {
                case vsFindResult.vsFindResultFound:
                    vgSetting.Instance.FileList = Vistaghost.VISTAGHOST.Lib.vgOperations.GetFileFromResultWindow(this.DTE, FileFilter.ffSource);
                    break;
                case vsFindResult.vsFindResultNotFound:
                    vgSetting.Instance.FindWhat = String.Empty;
                    break;
                default:
                    break;
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
            DTE.StatusBar.Text = text;
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
            return DTE.StatusBar;
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
            if (!vgSetting.SettingData.DataInfo.RegisteredOnWeb)
            {
                VGProduct.RegisterProduct();
            }
        }

        public void ExecuteCmd(string cmd, string args = null)
        {
            if (DTE == null) return;
            DTE.ExecuteCommand(cmd, args);
        }
    }
}
