using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;

namespace GhostExcel
{
    public partial class GhostRibbon
    {
        Excel.Application _application;

        private void GhostRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this._application = Globals.ThisAddIn.Application;
        }

        private void btnUpdate_Click(object sender, RibbonControlEventArgs e)
        {
            Excel.Worksheet _workSheet = (Excel.Worksheet)this._application.ActiveWorkbook.ActiveSheet;

            Globals.ThisAddIn.FormatExcel(_workSheet);
        }
    }
}
