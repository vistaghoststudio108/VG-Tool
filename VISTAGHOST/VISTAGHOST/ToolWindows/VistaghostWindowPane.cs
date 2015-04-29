using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    [Guid(GuidList.guidVISTAGHOSTWindow)]
    public class VistaghostWindowPane : ToolWindowPane
    {
        public static VistaghostWindowPane Current;
        private VistaghostWindowControl vgControl;

        public VistaghostWindowPane() :
            base(null)
        {
            this.Caption = Properties.Resources.VistaghostWindowTitle;
            
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            //this.Frame = DTEHelper.Current.DTE.MainWindow.LinkedWindowFrame;
            
            Current = this;

            vgControl = new VistaghostWindowControl();
            vgControl.OnClicked += new Vistaghost.VISTAGHOST.Lib.ToolWindowPaneEventHandler(vgControl_OnClicked);
        }

        public override IWin32Window Window
        {
            get 
            {
                return (IWin32Window)vgControl;
            }
        }

        #region event handler

        private void vgControl_OnClicked(int swCode)
        {
            vgControl.AddString("This is test by vistaghost studio, lead by ThuanPV3");
        }

        #endregion

        #region overide methods

        public override void OnToolWindowCreated()
        {
            base.OnToolWindowCreated();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region Common methods

        public void AddString(string strToAdd)
        {
            if (vgControl != null)
            {
                vgControl.AddString(strToAdd);
            }
        }

        public void ClearContent()
        {
 
        }

        #endregion
    }
}
