using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    interface ISearchWnd
    {
        void Search(string txt, bool AllFile, bool WholeWordMatch, bool CaseSensitive);

        void SetRelativePathEnable(bool enable);
    }

    class WindowManager
    {
        //ISearchWnd CurrentSearchWnd;
        public void WindowActivate(Window GotFocus, Window LostFocus)
        {

        }

        public void ShowToolWindow()
        {
            ToolWindowPane window = VISTAGHOSTPackage.Current.FindToolWindow(typeof(VistaghostWindowPane), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Properties.Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
