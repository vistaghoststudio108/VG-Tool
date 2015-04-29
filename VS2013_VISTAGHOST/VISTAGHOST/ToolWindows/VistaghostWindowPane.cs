using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    [Guid(GuidList.guidVISTAGHOSTWindow)]
    public class VistaghostWindowPane : ToolWindowPane
    {
        private VistaghostWindowControls vgWinControl;

        public VistaghostWindowPane()
            : base(null)
        {
            this.Caption = Properties.Resources.VistaghostWindowTitle;

            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            vgWinControl = new VistaghostWindowControls();
            vgWinControl.OnClicked += new Lib.ToolWindowPaneEventHandler(vgWinControl_OnClicked);

            base.Content = vgWinControl;
        }

        #region event handler

        private void vgWinControl_OnClicked(int swCode)
        {
 
        }

        #endregion

        public override void ClearSearch()
        {
            base.ClearSearch();
        }
    }
}
