using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using GhostExcel.DataModel;
using GhostExcel.UserControls;
using Tools = Microsoft.Office.Tools;

namespace GhostExcel
{
    public partial class ThisAddIn
    {
        #region Private variables
        Tools.CustomTaskPane _myCustomTaskPane;
        GhostPane _ghostPane;

        private Office.CommandBarPopup command;
        private Excel.Range selectedCells;

        #endregion

        #region Properties

        public Tools.CustomTaskPane MyCustomTaskPane
        {
            get
            {
                return _myCustomTaskPane;
            }
            set
            {
                _myCustomTaskPane = value;
            }
        }

        public GhostPane GhostPane
        {
            get
            {
                return _ghostPane;
            }
            set
            {
                _ghostPane = value;
            }
        }

        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //Init my custom pane
            _ghostPane = new GhostPane();
            _myCustomTaskPane = this.CustomTaskPanes.Add(_ghostPane, "Update Results");
            _myCustomTaskPane.Width = 250;
            _myCustomTaskPane.Visible = true;

            InitContextMenu();
            Application.SheetBeforeRightClick += new Excel.AppEvents_SheetBeforeRightClickEventHandler(Application_SheetBeforeRightClick);
            //command.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(command_Click);
        }

        void command_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                System.DateTime currentDateTime = System.DateTime.Now;
                string dateStamp = currentDateTime.ToString("dMMMMyyyy_hh.mm.ss");

                string fileName =
                    System.Environment.GetFolderPath
                    (Environment.SpecialFolder.MyDocuments) + "\\\\" + dateStamp + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);

                foreach (Excel.Range cell in selectedCells.Cells)
                {
                    if (cell.Value2 != null)
                    {
                        sw.WriteLine(cell.Value2.ToString());
                    }
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        void Application_SheetBeforeRightClick(object Sh, Excel.Range Target, ref bool Cancel)
        {
            selectedCells = Target;
        }

        private void InitContextMenu()
        {
            Office.CommandBar menuContext;
            Office.CommandBarPopup mainSubMenu;

            menuContext = Application.CommandBars["Cell"];
            mainSubMenu = (Office.CommandBarPopup)menuContext.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, true);
            mainSubMenu.Caption = "Ghost";

            var subItem1 = (Office.CommandBarButton)mainSubMenu.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            subItem1.Caption = "Get function";

            var subItem2 = (Office.CommandBarButton)mainSubMenu.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            subItem2.Caption = "Sub menu 2";

            var subItem3 = (Office.CommandBarButton)mainSubMenu.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            subItem3.Caption = "Sub menu 3";

            var subItem4 = (Office.CommandBarButton)mainSubMenu.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            subItem4.Caption = "Sub menu 4";
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            //Release all resources
            this.Dispose(true);
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

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ghostPane != null)
                    _ghostPane.Dispose();

                if (_myCustomTaskPane != null)
                    _myCustomTaskPane.Dispose();
            }
        }
    }
}
