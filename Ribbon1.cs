using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;

namespace GhostExcelTest
{
    public partial class Ribbon1
    {
        Excel.Application app;
        const int NUM_COLUMN = 6;
        string[] header = { "No", "関数名", "関数を呼び出す手順", "検査条件", "判定基準", "TC No.", "備考" };
        int[] headerWidth = { 5, 25, 25, 50, 50, 25, 25 };
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            this.app = Globals.ThisAddIn.Application;
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Excel.Range beginCell = this.app.ActiveCell;
            Excel.Worksheet sheet = (Excel.Worksheet)this.app.ActiveWorkbook.ActiveSheet;

            Excel.Range endCell = sheet.Cells[beginCell.Row, beginCell.Column + NUM_COLUMN];

            Excel.Range finalRange = sheet.get_Range(beginCell, endCell);

            Excel.Style style = null;

            try
            {
                //Get style if it existed in workbook
                style = Globals.ThisAddIn.Application.ThisWorkbook.Styles["VVVVVVVV"];
            }
            catch (Exception ex)
            {
                //Otherwise, add new style to workbook
                style = Globals.ThisAddIn.Application.ThisWorkbook.Styles.Add("VVVVVVVV", Type.Missing);
            }
            style.Font.Name = "ＭＳ 明朝";
            style.Font.Size = 11;

            finalRange.Style = "New Style";

            Globals.ThisAddIn.AddBorder(finalRange);

            Excel.Range trackCell = beginCell;
            int index = 0;
            string address = String.Empty;

            while (trackCell.Address != endCell.Address)
            {
                trackCell.Value2 = header[index];
                trackCell.ColumnWidth = headerWidth[index];
                trackCell = trackCell.Next;
                index++;
            }

            trackCell.Value2 = header[index];
            trackCell.ColumnWidth = headerWidth[index];
        }
    }
}
