using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class HistoryViewer : Form
    {
        enum SearchType
        {
            All = 0, // search all
            Account, // search by account
            Date,    // search by date
            Type     // search by type
        }

        public HistoryViewerEventHandler OnOpenFile;

        SearchType sType = SearchType.All;

        public HistoryViewer()
        {
            InitializeComponent();
        }

        int getStringWidth(string text)
        {
            Size textSize = TextRenderer.MeasureText(text, this.Font);
            return textSize.Width;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtHistoryPath.Text = openFileDialog1.FileName;

                if (getStringWidth(txtHistoryPath.Text) > txtHistoryPath.Width)
                    toolTip1.SetToolTip(txtHistoryPath, txtHistoryPath.Text);
                else
                    toolTip1.SetToolTip(txtHistoryPath, "");
            }
        }

        private void HistoryViewer_Load(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = 0;
            listView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void HistoryViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            sType = (SearchType)cbSearchType.SelectedIndex;
            switch (sType)
            {
                case SearchType.All:
                    {

                    }
                    break;
                case SearchType.Account:
                    break;
                case SearchType.Date:
                    break;
                case SearchType.Type:
                    break;
                default:
                    break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var path = txtHistoryPath.Text;
            if (!File.Exists(path))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            listView1.Items.Clear();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;

            XmlNodeList nodeList = root.SelectSingleNode("VGHistory").SelectNodes("History");

            foreach (XmlNode xNode in nodeList)
            {
                ListViewItem item = new ListViewItem(new string[] { xNode.SelectSingleNode("Account").InnerText, xNode.SelectSingleNode("Mode").InnerText, xNode.SelectSingleNode("File").InnerText });
                listView1.Items.Add(item);
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewHitTestInfo lvhti = listView1.HitTest(e.X, e.Y);
                if (lvhti.Item == null)
                    contextMenuStrip1.Close();
            }
        }

        private void goToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = listView1.SelectedItems[0].SubItems[2].Text;
            if (OnOpenFile != null)
            {
                OnOpenFile(file);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
