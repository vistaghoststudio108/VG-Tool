using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GhostExcel
{
    public class ExcelCleaner
    {
        /// <summary>
        /// Release resource after use
        /// </summary>
        /// <param name="obj"> object need to release </param>
        public static void releaseObject(object obj)
        {
            try
            {
                if(Marshal.IsComObject(obj))
                    Marshal.ReleaseComObject(obj);

                obj = null;
            }
            catch (Exception ex)
            {
                ExcelLogger.LogError(ex);
                obj = null;
            }

            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static void releaseObject(ref object obj)
        {
            if(Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }

            obj = null;
        }
    }
}
