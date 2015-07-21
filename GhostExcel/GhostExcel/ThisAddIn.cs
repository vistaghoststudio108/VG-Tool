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
