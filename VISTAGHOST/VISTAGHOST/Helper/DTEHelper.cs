﻿using EnvDTE;
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
using Vistaghost.VISTAGHOST.DataModel;
using Vistaghost.VISTAGHOST.WindowForms;

namespace Vistaghost.VISTAGHOST.Helper
{
    class DTEHelper
    {
        public DTE DTE { get; private set; }

        public DTE2 DTE2 { get; set; }

        private bool SavedAll = false;
        private ListElementForm lef;

        DocumentEvents docEvents;
        SolutionEvents solEvents;
        ProjectItemsEvents proitemEvents;
        DTEEvents dteEvents;
        FindEvents findEvents;
        CommandEvents commandEvents;
        CodeModelEvents codeModelEvent;
        SelectionEvents selEvent;

        public DTEHelper(DTE dte, DTE2 dte2)
        {
            this.DTE = dte;
            this.DTE2 = dte2;

            docEvents = DTE.Events.get_DocumentEvents(DTE.ActiveDocument);
            docEvents.DocumentOpening += docEvents_DocumentOpening;
            docEvents.DocumentClosing += docEvents_DocumentClosing;
            docEvents.DocumentOpened += docEvents_DocumentOpened;
            docEvents.DocumentSaved += docEvents_DocumentSaved;

            findEvents = DTE.Events.FindEvents;
            findEvents.FindDone += new _dispFindEvents_FindDoneEventHandler(findEvents_FindDone);

            solEvents = DTE.Events.SolutionEvents;
            solEvents.Opened += solEvents_Opened;

            proitemEvents = ((EnvDTE80.Events2)DTE.Events).ProjectItemsEvents;
            proitemEvents.ItemAdded += new _dispProjectItemsEvents_ItemAddedEventHandler(proitemEvents_ItemAdded);
            proitemEvents.ItemRemoved += new _dispProjectItemsEvents_ItemRemovedEventHandler(proitemEvents_ItemRemoved);
            proitemEvents.ItemRenamed += new _dispProjectItemsEvents_ItemRenamedEventHandler(proitemEvents_ItemRenamed);

            dteEvents = DTE.Events.DTEEvents;
            dteEvents.OnStartupComplete += dteEvents_OnStartupComplete;
            dteEvents.OnBeginShutdown += dteEvents_OnBeginShutdown;

            commandEvents = DTE2.Events.get_CommandEvents("{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 277);

            commandEvents.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler(commandEvents_BeforeExecute);
            commandEvents.AfterExecute += new _dispCommandEvents_AfterExecuteEventHandler(commandEvents_AfterExecute);

            //Code model event
            codeModelEvent = ((EnvDTE80.Events2)DTE.Events).get_CodeModelEvents(null);
            codeModelEvent.ElementAdded += new _dispCodeModelEvents_ElementAddedEventHandler(codeModelEvent_ElementAdded);
            codeModelEvent.ElementChanged += new _dispCodeModelEvents_ElementChangedEventHandler(codeModelEvent_ElementChanged);
            codeModelEvent.ElementDeleted += new _dispCodeModelEvents_ElementDeletedEventHandler(codeModelEvent_ElementDeleted);

            // Selection event
            selEvent = DTE.Events.SelectionEvents;
            selEvent.OnChange += new _dispSelectionEvents_OnChangeEventHandler(selEvent_OnChange);
        }

        void proitemEvents_ItemRenamed(ProjectItem ProjectItem, string OldName)
        {

        }

        void proitemEvents_ItemRemoved(ProjectItem ProjectItem)
        {

        }

        void proitemEvents_ItemAdded(ProjectItem ProjectItem)
        {

        }

        void selEvent_OnChange()
        {
            
        }

        void codeModelEvent_ElementDeleted(object Parent, CodeElement Element)
        {
            if (!VGSetting.ProjectStatus.Started)
                return;

            switch (Element.Kind)
            {
                //delete function
                case vsCMElement.vsCMElementFunction:
                    {
                        var codeFunc = (CodeFunction)Element;
                        FileManager.Instance.RemoveElement(codeFunc.Name);
                    }
                    break;

                default:
                    break;
            }
        }

        void codeModelEvent_ElementChanged(CodeElement Element, vsCMChangeKind Change)
        {
        }

        void codeModelEvent_ElementAdded(CodeElement Element)
        {
            if (!VGSetting.ProjectStatus.Started)
                return;

            switch (Element.Kind)
            {
                //add function
                case vsCMElement.vsCMElementFunction:
                    {
                        var codeFunc = (CodeFunction)Element;
                        string prot = codeFunc.get_Prototype((int)((vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType | vsCMPrototype.vsCMPrototypeFullname)));
                        var element = new VGCodeElement(codeFunc.ProjectItem.Document.FullName, prot);

                        if (lef == null)
                        {
                            lef = new ListElementForm();
                            lef.OnResult += new ListElementEventHandler(lef_OnResult);
                            lef.AddItem(element);
                            lef.Show();
                        }
                        else
                        {
                            lef.AddItem(element);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void lef_OnResult(List<VGCodeElement> finalElementList, VGDialogResult dlgResult)
        {
            switch (dlgResult)
            {
                case VGDialogResult.VG_OK:
                    {
                        FileManager.Instance.SaveNewElements(finalElementList);
                    }
                    break;
                case VGDialogResult.VG_CANCEL:
                    break;
                default:
                    break;
            }

            if (lef != null)
            {
                lef.OnResult -= new ListElementEventHandler(lef_OnResult);
                lef = null;
            }
        }

        void docEvents_DocumentSaved(Document Document)
        {
            if (SavedAll)
            {
                FileManager.Instance.SaveFileChanged(Document.FullName);
            }
        }

        void commandEvents_AfterExecute(string Guid, int ID, object CustomIn, object CustomOut)
        {
            if (SavedAll)
                SavedAll = false;
        }

        void commandEvents_BeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {
            if (ID == 331 && VGSetting.ProjectStatus.Started)
            {
                // "Save" invoked
                var activeDoc = this.DTE.ActiveDocument;
                if (!activeDoc.Saved)
                    FileManager.Instance.SaveFileChanged(activeDoc.FullName);
            }
            else if (ID == 224 && VGSetting.ProjectStatus.Started)
            {
                // "Save all" invoked
                SavedAll = true;
            }
        }

        void findEvents_FindDone(vsFindResult Result, bool Cancelled)
        {
            VGSetting.Instance.FileList.Clear();

            switch (Result)
            {
                case vsFindResult.vsFindResultFound:
                    {
                        //TODO what
                    }
                    break;
                case vsFindResult.vsFindResultNotFound:
                    VGSetting.Instance.FindWhat = String.Empty;
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

        void docEvents_DocumentOpened(Document Document)
        {
            
        }

        void dteEvents_OnBeginShutdown()
        {
        }

        void wndEvents_WindowActivated(Window GotFocus, Window LostFocus)
        {
            VISTAGHOSTPackage.WindowManager.WindowActivate(GotFocus, LostFocus);
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
            if (!VGSetting.SettingData.DataInfo.RegisteredOnWeb)
            {
                VGProduct.RegisterProduct();
            }
        }

        public void ExecuteCmd(string cmd, string args)
        {
            if (DTE == null) return;
            DTE.ExecuteCommand(cmd, args);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (lef != null)
                    lef.Dispose();
            }
        }
    }
}
