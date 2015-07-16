using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using GhostExcel.DataModel;

namespace GhostExcel
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Get address of ranges
        /// </summary>
        /// <param name="rng"></param>
        /// <returns>address string</returns>
        public string RangeAddress(Excel.Range rng)
        {
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1, missing, missing);
        }

        /// <summary>
        /// Get cell's address
        /// </summary>
        /// <param name="sheet">active sheet</param>
        /// <param name="row">cell's row</param>
        /// <param name="col">cell's column</param>
        /// <returns></returns>
        public string CellAddress(Excel.Worksheet sheet, int row, int col)
        {
            return RangeAddress(sheet.Cells[row, col]);
        }

        public void AddBorder(Excel.Range range, BorderType borderType)
        {

        }

        internal void FormatExcel(Excel.Worksheet worksheet)
        {
            Excel.Range range = this.Application.get_Range("A1", "D4");

            string address = RangeAddress(range);
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
