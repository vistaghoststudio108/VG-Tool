using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vistaghost.VISTAGHOST.Helper;

namespace Vistaghost.VISTAGHOST.Editor
{
    class EditorManager
    {
        /// <summary>
        /// Get a IVsTextView interface that the ActiveDocument use
        /// </summary>
        /// <returns></returns>
        public static IVsTextView GetCurrentTextView()
        {
            var monitorSelection = (IVsMonitorSelection)VISTAGHOSTPackage.GetGlobalService(typeof(SVsShellMonitorSelection));
            if (monitorSelection == null)
            {
                return null;
            }
            object curDocument;
            if (ErrorHandler.Failed(monitorSelection.GetCurrentElementValue((uint)VSConstants.VSSELELEMID.SEID_DocumentFrame, out curDocument)))
            {
                // TODO: Report error
                return null;
            }

            IVsWindowFrame frame = curDocument as IVsWindowFrame;
            if (frame == null)
            {
                // TODO: Report error
                return null;
            }

            object docView = null;
            if (ErrorHandler.Failed(frame.GetProperty((int)__VSFPROPID.VSFPROPID_DocView, out docView)))
            {
                // TODO: Report error
                return null;
            }

            if (docView is IVsCodeWindow)
            {
                IVsTextView textView;
                if (ErrorHandler.Failed(((IVsCodeWindow)docView).GetPrimaryView(out textView)))
                {
                    // TODO: Report error
                    return null;
                }
                return textView;
            }
            return null;
        }

        /// <summary>
        /// Open file , set cursor position, select tokens and highlight tokens
        /// </summary>
        /// <param name="file">file to open.null means current document.</param>
        /// <param name="line">cursor line</param>
        /// <param name="column">cursor column</param>
        /// <param name="length">selection length</param>
        /// <param name="highlight">highlight the token cursor at or not</param>
        public static void GoTo(string file, int line, int column, int length, bool highlight)
        {
            IVsTextView view;
            if (file == null)
            {
                if (VISTAGHOSTPackage.DteHelper.DTE.ActiveDocument == null)
                {
                    Logger.LogMessage("EditorManager.Goto Fail : file is null.", false);
                    return;
                }
                else
                {
                    view = GetCurrentTextView();
                }
            }
            else
            {
                if (VISTAGHOSTPackage.DteHelper.DTE.ActiveDocument == null)
                {
                    view = PreviewDocument(file);
                }
                else if (VISTAGHOSTPackage.DteHelper.DTE.ActiveDocument.FullName != file)
                {
                    view = PreviewDocument(file);
                }
                else
                {
                    view = GetCurrentTextView();
                }
            }

            if (line != 0 || column != 0)
            {
                view.SetCaretPos(line, column);
                view.SetSelection(line, column, line, column + length);
                view.CenterLines(line, 1);
            }

            MarkLine(line);

            if (highlight) 
                HighlightPosition(line, column, length);
        }

        public static void MarkLine(int line)
        {
           // Editor.MarkPosTaggerProvider.CurrentTagger.ShowTag(line);
        }

        public static void HighlightPosition(int line, int column, int length)
        {
            //FindWordTaggerProvider.CurrentTagger.UpdateAtPosition(line, column, length);
        }

        public static IVsTextView PreviewDocument(string file)
        {
            IVsTextView view;
            //using (new NewDocumentStateScope(__VSNEWDOCUMENTSTATE.NDS_Provisional, Microsoft.VisualStudio.VSConstants.NewDocumentStateReason.Navigation))
            //{
            //    //BabePackage.DTEHelper.DTE.ItemOperations.OpenFile(file, EnvDTE.Constants.vsViewKindPrimary);
            //    view = OpenDocument(file, true, false);
            //}
            return null;
        }

        public static IVsTextView OpenDocument(string file, bool linkToProject, bool focus)
        {
            VISTAGHOSTPackage.DteHelper.DTE.ItemOperations.OpenFile(file, EnvDTE.Constants.vsViewKindPrimary);
            //IVsTextManager textMgr = (IVsTextManager)BabePackage.Current.GetService(typeof(SVsTextManager));

            //IVsUIShellOpenDocument uiShellOpenDocument = BabePackage.Current.GetService(typeof(SVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
            //IVsUIHierarchy hierarchy;
            //uint itemid;
            //IVsTextView viewAdapter;
            //IVsWindowFrame pWindowFrame;

            //VsShellUtilities.OpenDocument(
            //    BabePackage.Current,
            //    file,
            //    Guid.Empty,
            //    out hierarchy,
            //    out itemid,
            //    out pWindowFrame,
            //    out viewAdapter);

            //if (focus)
            //{
            //    ErrorHandler.ThrowOnFailure(pWindowFrame.Show());
            //}
            //else
            //{
            //    ErrorHandler.ThrowOnFailure(pWindowFrame.ShowNoActivate());
            //}

            var viewAdapter = GetCurrentTextView();
            return viewAdapter;
        }
    }
}
