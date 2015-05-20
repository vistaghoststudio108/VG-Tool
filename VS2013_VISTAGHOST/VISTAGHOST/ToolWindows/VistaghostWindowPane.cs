using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    [Guid(GuidList.guidVISTAGHOSTWindow)]
    public class VistaghostWindowPane : ToolWindowPane
    {
        private VistaghostWindowControls wnd;
        public static VistaghostWindowPane Current;

        public VistaghostWindowPane()
            : base(null)
        {
            this.Caption = Properties.Resources.VistaghostWindowTitle;

            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            wnd = new VistaghostWindowControls();

            base.Content = wnd;
            Current = this;
        }

        #region event handler

        #endregion

        #region Common methods
        /// <summary>
        /// Add text to tool window
        /// </summary>
        /// <param name="Text">string need to add</param>
        public void OutputString(string Text)
        {
            if (String.IsNullOrEmpty(Text))
                return;

            wnd.Dispatcher.Invoke(() =>
                {
                    wnd.SearchResultArea.AppendText(Text);
                });
        }

        public void Activate()
        {

        }

        /// <summary>
        /// Clear content of window
        /// </summary>
        public override void ClearSearch()
        {
            switch (wnd.Combo_SearchType.SelectedIndex)
            {
                case 0:
                    wnd.SearchResultArea.Document.Blocks.Clear();
                    break;

                case 1:
                    wnd.WorkingHistoryArea.Document.Blocks.Clear();
                    break;

                case 2:
                    wnd.Clear();
                    break;

                default:
                    break;
            }

            base.ClearSearch();
        }

        #endregion
    }
}
