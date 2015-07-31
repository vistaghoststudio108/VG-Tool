using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using GhostExcel.WinForm;
using GhostExcel.DataModel;
using System.Net.Mail;
using System.Windows.Forms;
using System.Diagnostics;

namespace GhostExcel
{
    public partial class GhostRibbon
    {
        Excel.Application _application;
        //ProgressForm progress;

        private void GhostRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this._application = Globals.ThisAddIn.Application;

            searchThread.DoWork += searchThread_DoWork;
            searchThread.RunWorkerCompleted += searchThread_RunWorkerCompleted;
            //searchThread.ProgressChanged += searchThread_ProgressChanged;
        }

        void searchThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progress.Message = "In progress, please wait... " + e.ProgressPercentage.ToString() + "%";
            //progress.ProgressValue = e.ProgressPercentage;
        }

        void searchThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                
            }
            else if (e.Error != null)
            {
                
            }
            else
            {

            }

            //Enable Update button
            btnUpdate.Enabled = true;
        }

        void searchThread_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Worksheet ws = (Excel.Worksheet)this._application.ActiveWorkbook.ActiveSheet;
            Excel.Range range = ws.UsedRange;
            List<Excel.Range> listAreas;
            ExcelUtilities.SplitUsedRange(ws.UsedRange, out listAreas);

            int numOfFunc = FileManager.Instance.SearchInFile().Count;

            if(numOfFunc == 0)
            {
                this._application.StatusBar = "Function not found";
                return;
            }

            Globals.ThisAddIn.GhostPane.ClearList();

            foreach (var func in FileManager.Instance.SearchInFile())
            {
                if(!ExcelUtilities.Find(range, func.Name))
                {
                    Globals.ThisAddIn.GhostPane.AddObj(func);
                    this._application.StatusBar = "Found " + func.Name;
                }
            }

            ExcelCleaner.releaseObject(ws);
            ExcelCleaner.releaseObject(range);
        }

        private void btnUpdate_Click(object sender, RibbonControlEventArgs e)
        {
            if (!searchThread.IsBusy)
            {
                btnUpdate.Enabled = false;
                // Start the asynchronous operation.
                searchThread.RunWorkerAsync();
            }
        }

        private void btnSetting_Click(object sender, RibbonControlEventArgs e)
        {
            SettingForm sf = new SettingForm();
            sf.ShowDialog();
        }


        private void btnFeedBack_Click(object sender, RibbonControlEventArgs e)
        {
            SettingForm sf = null;
            try
            {
                sf.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ThrowExceptionReport(ex);
            }
        }

        private void btnNew_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}
