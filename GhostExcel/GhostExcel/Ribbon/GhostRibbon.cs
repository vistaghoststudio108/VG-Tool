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

namespace GhostExcel
{
    public partial class GhostRibbon
    {
        Excel.Application _application;

        private void GhostRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this._application = Globals.ThisAddIn.Application;

            //mailThread.DoWork += mailThread_DoWork;
            //mailThread.RunWorkerCompleted += mailThread_RunWorkerCompleted;

            searchThread.DoWork += searchThread_DoWork;
            searchThread.RunWorkerCompleted += searchThread_RunWorkerCompleted;
            searchThread.ProgressChanged += searchThread_ProgressChanged;
        }

        void searchThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pf.SetProgressPercent(e.ProgressPercentage);
        }

        void searchThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        void searchThread_DoWork(object sender, DoWorkEventArgs e)
        {
            ProgressForm progress = (ProgressForm)e.Argument;
            int i = 1;
            while (i < 100)
            {
                progress.UpdateProgress(i);
                i++;
            }
            progress.Close();
        }

        private void btnUpdate_Click(object sender, RibbonControlEventArgs e)
        {
            //if (!searchThread.IsBusy)
            //{
            //    ProgressForm pf = new ProgressForm();
            //    searchThread.RunWorkerAsync(pf);
            //    pf.ShowDialog();
            //}

            Excel.Worksheet ws = (Excel.Worksheet)this._application.ActiveWorkbook.ActiveSheet;

            Excel.Range range = ws.get_Range("A1", "D4");

            if(ExcelUtilities.Find(range, "vistaghost"))
            {

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
