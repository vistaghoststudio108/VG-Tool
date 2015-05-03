using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    public partial class VistaghostWindowControl : UserControl
    {
        public VistaghostWindowControl()
        {
            InitializeComponent();
        }

        public void AddString(string Text)
        {
            ucVistaghostWindow1.AddString(Text);
        }

        public void Clear()
        {
            ucVistaghostWindow1.Clear();
        }
    }
}
