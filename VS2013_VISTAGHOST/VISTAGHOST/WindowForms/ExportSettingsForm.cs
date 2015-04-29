using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST
{
    public partial class ExportSettingsForm : Form
    {
        public ExportEventHandler OnExportResult;
        int MaxPanel = 1;
        int _index = 0;
        string ExpFileName = ""; // export default file name
        string CurFileName = ""; // current setting file name
        string FilePath = "";
        bool saveCurSettings = true;
        List<FileNameInfo> FileNames;
        ExImType eiType = ExImType.vgEXPORT;

        public ExportSettingsForm()
        {
            InitializeComponent();
        }

        private void ExportSettingsForm_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(531, 377);

            pnOptionChoice.Location = new Point(1, 10);
            pnOptionChoice.Size = new Size(512, 289);
            pnOptionChoice.BringToFront();

            pnSettingNamed.Location = new Point(1, 10);
            pnSettingNamed.Size = new Size(512, 289);

            panel1.Location = new Point(1, 10);
            panel1.Size = new Size(512, 289);

            panel2.Location = new Point(1, 10);
            panel2.Size = new Size(512, 289);

            ExpFileName = vgSetting.DefaultFileName(ExImType.vgEXPORT);
            CurFileName = vgSetting.DefaultFileName(ExImType.vgIMPORT);

            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder);

            FileNames = vgSetting.GetAllSettingFile(FilePath);

            if (FileNames.Count > 0)
            {
                lstSettings.Items.Clear();
                foreach (var item in FileNames)
                {
                    lstSettings.Items.Add(item.Name);
                }
            }

            vgSetting.GetUniqueFileName(FilePath, ExpFileName, out ExpFileName);
            vgSetting.GetUniqueFileName(FilePath, CurFileName, out CurFileName);

            cbFilePath.Text = FilePath;
            textBox2.Text = FilePath;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _index++;
            if (_index > MaxPanel)
            {
                _index = MaxPanel;
                return;
            }
            switch (eiType)
            {
                case ExImType.vgEXPORT:
                    btnFinish.Enabled = (_index == MaxPanel);
                    break;
                case ExImType.vgIMPORT:
                    btnFinish.Enabled = ((_index == MaxPanel) ? ((lstSettings.SelectedIndex != -1) ? true : false) : false);
                    break;
            }

            btnPrevious.Enabled = !(_index == 0);

            btnNext.Enabled = !(_index == MaxPanel);

            ShowPane(_index);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _index--;
            if (_index < 0)
            {
                _index = 0;
                return;
            }
            btnNext.Enabled = (_index != MaxPanel);

            btnPrevious.Enabled = !(_index == 0);

            btnFinish.Enabled = (_index == MaxPanel);

            ShowPane(_index);
        }

        void ShowPane(int iPane)
        {
            if (iPane == 0)
                pnOptionChoice.BringToFront();
            else if (iPane == 1)
            {
                switch (eiType)
                {
                    case ExImType.vgEXPORT:
                        {
                            txtFileName.Text = ExpFileName + vgSettingConstants.FileExtenstion;
                            pnSettingNamed.BringToFront();
                        }
                        break;
                    case ExImType.vgIMPORT:
                        {
                            textBox1.Text = CurFileName + vgSettingConstants.FileExtenstion;
                            panel1.BringToFront();
                        }
                        break;
                    case ExImType.vgRESET_ALL:
                        break;
                    default:
                        break;
                }
            }
            else if (iPane == 2) // only for import options
            {
                panel2.BringToFront();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = txtFileName.Text;
            saveFileDialog1.DefaultExt = "vgconfig";
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.InitialDirectory = cbFilePath.Text;

            saveFileDialog1.Filter = "Settings Files (.vgconfig)|*.vgconfig";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cbFilePath.Text = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                txtFileName.Text = System.IO.Path.GetFileName(saveFileDialog1.FileName);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            string path = String.Empty;
            string message = String.Empty;
            bool success = false;

            if (!Directory.Exists(cbFilePath.Text) && saveCurSettings)
            {
                Directory.CreateDirectory(cbFilePath.Text);
            }

            switch (eiType)
            {
                case ExImType.vgEXPORT:
                    {
                        path = Path.Combine(cbFilePath.Text, txtFileName.Text);
                        vgSetting.SaveCurrentSetting(path, out message, out success);

                        if (OnExportResult != null)
                        {
                            OnExportResult(message, success);
                        }
                    }
                    break;
                case ExImType.vgIMPORT:
                    {
                        int index = lstSettings.SelectedIndex;
                        if (index != -1)
                        {
                            if (saveCurSettings)
                            {
                                path = Path.Combine(textBox2.Text, textBox1.Text);
                                vgSetting.SaveCurrentSetting(path, out message, out success);
                            }


                            //load settings
                            vgSetting.LoadNewSetting(FileNames[index].FullPath, out message, out success);
                            vgSetting.SaveSettings();
                        }
                    }
                    break;
                case ExImType.vgRESET_ALL:
                    break;
                default:
                    break;
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = textBox1.Text;
            saveFileDialog1.DefaultExt = "vgconfig";
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.InitialDirectory = textBox2.Text;

            saveFileDialog1.Filter = "Settings Files (.vgconfig)|*.vgconfig";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cbFilePath.Text = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                txtFileName.Text = System.IO.Path.GetFileName(saveFileDialog1.FileName);
            }
        }

        private void rdYes_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = rdYes.Checked;
            textBox2.Enabled = rdYes.Checked;
            button1.Enabled = rdYes.Checked;

            saveCurSettings = rdYes.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                eiType = ExImType.vgEXPORT;
                MaxPanel = 1;

                if (!btnNext.Enabled)
                    btnNext.Enabled = true;
            }
            else if (radioButton2.Checked)
            {
                eiType = ExImType.vgIMPORT;
                MaxPanel = 2;

                if (!btnNext.Enabled)
                    btnNext.Enabled = true;
            }
            else if (radioButton3.Checked)
            {
                eiType = ExImType.vgRESET_ALL;
                btnNext.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "*.vgconfig";
            openFileDialog1.DefaultExt = "vgconfig";
            openFileDialog1.ValidateNames = true;
            openFileDialog1.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder);

            openFileDialog1.Filter = "Settings Files (.vgconfig)|*.vgconfig";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var nfile = new FileNameInfo
                {
                    FullPath = openFileDialog1.FileName,
                    Name = System.IO.Path.GetFileName(openFileDialog1.FileName)
                };

                lstSettings.Items.Add(nfile.Name);
                FileNames.Add(nfile);
            }
        }

        private void lstSettings_DoubleClick(object sender, EventArgs e)
        {
            if (lstSettings.SelectedIndex != -1)
                btnFinish_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void lstSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSettings.SelectedIndex != -1)
            {
                btnFinish.Enabled = true;
            }
            else
                btnFinish.Enabled = false;
        }
    }
}
