using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    public partial class VistaghostWindowControl : UserControl
    {
        public Vistaghost.VISTAGHOST.Lib.ToolWindowPaneEventHandler OnClicked;

        public VistaghostWindowControl()
        {
            InitializeComponent();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox combo = (ToolStripComboBox)sender;

            switch (combo.SelectedIndex)
            {
                case 0:// Function output
                    {
                        txtFunctionOutput.BringToFront();
                    }
                    break;

                case 1:// History output
                    {
                        txtHistoryOutput.BringToFront();
                    }
                    break;

                case 2:// Error List output
                    {
                        txtErrorListOutput.BringToFront();
                    }
                    break;

                default:
                    break;
            }
        }

        private void VistaghostWindowControl_Load(object sender, EventArgs e)
        {
            //txtFunctionOutput.Text = "Function window";
            //txtHistoryOutput.Text = "History window";
            //txtErrorListOutput.Text = "Error List window";
        }

        #region Common methods

        public void Clear()
        {
 
        }

        public void AddString(string content)
        {
            txtFunctionOutput.AppendText(content + "\n");
        }

        #endregion

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            if (OnClicked != null)
                OnClicked(1);
        }
    }
}
