using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GhostExcel
{
    public delegate int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    public class InterceptKeys
    {
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        //Declare the mouse hook constant.
        //For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.            

        private const int WH_KEYBOARD = 2;
        private const int HC_ACTION = 0;

        public static void SetHook()
        {
            _hookID = NativeMethods.SetWindowsHookEx(WH_KEYBOARD, _proc, IntPtr.Zero, (uint)NativeMethods.GetCurrentThreadId());
        }

        public static void ReleaseHook()
        {
            NativeMethods.UnhookWindowsHookEx(_hookID);
        }

        private static int HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int PreviousStateBit = 31;
            bool KeyWasAlreadyPressed = false;

            Int64 bitmask = (Int64)Math.Pow(2, (PreviousStateBit - 1));

            try
            {

                if (nCode < 0)
                {
                    return (int)NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
                else
                {
                    if (nCode == HC_ACTION)
                    {
                        Keys keyData = (Keys)wParam;
                        KeyWasAlreadyPressed = ((Int64)lParam & bitmask) > 0;

                        //Alt+1
                        if (Functions.IsKeyDown(Keys.Menu) && keyData == Keys.D2 && KeyWasAlreadyPressed == false)
                        {

                        }
                    }
                    return (int)NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
            }
            catch (Exception ex)
            {
                ExcelLogger.LogError(ex);
                return (int)NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
        }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        internal static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MessageBeep(int uType);
    }

    public class Functions
    {
        public static bool IsKeyDown(Keys keys)
        {
            //0x8000 : pressed, not toggled
            return (NativeMethods.GetKeyState((int)keys) & 0x8000) == 0x8000;
        }
    }
}
