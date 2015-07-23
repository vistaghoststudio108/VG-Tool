using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vistaghost.VISTAGHOST.Helper
{
    public class ComManager
    {
        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, ref COPYDATASTRUCT lParam);
    }
}
