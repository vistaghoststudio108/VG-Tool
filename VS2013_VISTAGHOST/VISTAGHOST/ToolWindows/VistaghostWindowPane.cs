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

            base.Content = vgWinControl;
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

            vgWinControl.AddString(Text);
        }

        public void Activate()
        {

        }

        /// <summary>
        /// Clear content of window
        /// </summary>
        public void Clear()
        {
            vgWinControl.Clear();
        }

        #endregion
    }
}
