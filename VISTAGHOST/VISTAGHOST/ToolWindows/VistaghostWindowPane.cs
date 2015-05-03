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
            
            Current = this;

            vgControl = new VistaghostWindowControl();
        }

        public override IWin32Window Window
        {
            get 
            {
                return (IWin32Window)vgControl;
            }
        }

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

        public void AddString(string Text)
        {
            vgControl.AddString(Text);
        }

        public void Clear()
        {
            vgControl.Clear();
        }

        #endregion
    }
}
