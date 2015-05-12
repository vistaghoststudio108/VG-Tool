using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreatTool
{
    public partial class Form1 : Form
    {
        List<string> fullName1 = new List<string>();
        List<string> fullName2 = new List<string>();
        List<string> posList = new List<string>();
        int maxNum = 0;
        int curIndex1 = -1;
        int curIndex2 = -1;
        bool IsDraw = false;
        int curComboIndex = -1;
        int preComboIndex = -1;

        int nDotSize = 2;

        Font newFont;
        bool Available = true;
        bool isDotDisplay = false;
        //int numseg = 16;
        Color curColor = Color.Red;

        List<Point> tempPos = new List<Point>(17) { 
            Point.Empty, Point.Empty, Point.Empty, Point.Empty, Point.Empty,
            Point.Empty, Point.Empty, Point.Empty, Point.Empty,
            Point.Empty, Point.Empty, Point.Empty, Point.Empty,
            Point.Empty, Point.Empty, Point.Empty, Point.Empty, };

        List<List<Point>> pointList = new List<List<Point>>();
        List<string> readList = new List<string>();

        string[] segid = {
                                  //"MAS",
                                  //"BAS",
                                  //"MP",	
                                  //"BP",	
                                  //"MS",	
                                  //"MA",	
                                  //"MI",
                                  //"ML",	
                                  //"ASS",
                                  //"AL",	
                                  //"BL",	
                                  //"BSM",
                                  //"AI",	
                                  //"AA",	
                                  //"BA",	
                                  //"BI",	
                                  //"TA",
                                  "BA",
                                "BAS",
                                "BIS",
                                "BI",
                                "BIL",
                                "BAL",
                                "MA",
                                "MAS",
                                "MIS",
                                "MI",
                                "MIL",
                                "MAL",
                                "AA",
                                "AS",
                                "AI",
                                "AL",
                                "APEX"
                               };

        string[] invertid = {
                                "BA",
                                "BAS",
                                "BIS",
                                "BI",
                                "BIL",
                                "BAL",
                                "MA",
                                "MAS",
                                "MIS",
                                "MI",
                                "MIL",
                                "MAL",
                                "AA",
                                "AS",
                                "AI",
                                "AL",
                                "APEX"
                            };
        public Form1()
        {
            InitializeComponent();
            newFont = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtOldBmpSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        Point GetPosFromString(string input)
        {
            var couple = input.Split(new char[] { ',' });

            var xstr = couple[0].Substring(1, couple[0].Length - 1);
            var ystr = couple[1].Substring(0, couple[1].Length - 1);

            int x = int.Parse(xstr);
            int y = int.Parse(ystr);

            return new Point(x, y);
        }

        void GetListPosFromString(string input)
        {
            List<Point> lp = new List<Point>();

            while (!string.IsNullOrEmpty(input))
            {
                int pos = input.IndexOf("}");
                var p = GetPosFromString(input.Substring(0, pos + 1));
                lp.Add(p);

                try
                {
                    input = input.Substring(pos + 2);
                }
                catch
                {
                    input = String.Empty;
                }
            }
            pointList.Add(lp);
        }

        void LoadPos()
        {
            try
            {
                using (StreamReader rd = new StreamReader(txtPosSource.Text))
                {
                    while (rd.Peek() > 0)
                    {
                        var line = rd.ReadLine();
                        var pos = line.IndexOf(']');
                        //string type = line.Substring(0, pos + 1);
                        line = line.Substring(pos + 1);
                        line = DelWhiteSpace(line);

                        //cbInputPos.Items.Add(type);
                        readList.Add(line);
                        GetListPosFromString(line);
                    }
                }
            }
            catch
            {
            }
        }

        void LoadOldBmp()
        {
            if (Directory.Exists(txtOldBmpSource.Text))
            {
                DirectoryInfo dir = new DirectoryInfo(txtOldBmpSource.Text);
                FileInfo[] bmpFiles = dir.GetFiles("*.bmp");

                foreach (var bmp in bmpFiles)
                {
                    dataGridView1.Rows.Add(bmp.Name);
                    fullName1.Add(bmp.FullName);
                }
            }
        }

        void LoadNewBmp()
        {
            if (Directory.Exists(txtNewBmpSource.Text))
            {
                DirectoryInfo dir = new DirectoryInfo(txtNewBmpSource.Text);
                FileInfo[] bmpFiles = dir.GetFiles("*.bmp");

                foreach (var bmp in bmpFiles)
                {
                    fullName2.Add(bmp.FullName);
                }
            }
        }

        private void btnLoadBMP_Click(object sender, EventArgs e)
        {
            if (txtOldBmpSource.Text == String.Empty)
                return;

            Available = false;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            lblImageSize1.Text = "Image1 Size : ";
            lblImageSize2.Text = "Image2 Size : ";
            fullName1.Clear();
            fullName2.Clear();
            //cbInputPos.Items.Clear();
            curIndex1 = curIndex2 = -1;
            txtResult.Text = String.Empty;

            LoadOldBmp();
            LoadNewBmp();

            lblImageNum1.Text = "Image1 Num : " + fullName1.Count.ToString();
            lblImageNum2.Text = "Image2 Num : " + fullName2.Count.ToString();
            LoadPos();

            Available = true;
            checkHide.CheckedChanged -= checkHide_CheckedChanged;
            checkHide.Checked = false;
            dataGridView1_SelectionChanged(sender, e);
            //dataGridView2_SelectionChanged(sender, e);

            checkHide.CheckedChanged += checkHide_CheckedChanged;
        }

        private void btnBrowsePos_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtPosSource.Text = openFileDialog1.FileName;
            }
        }

        string DelWhiteSpace(string input)
        {
            while (input.Contains(" "))
            {
                int pos = input.IndexOf(" ");
                input = input.Remove(pos, 1);
            }

            return input;
        }

        /// <summary>
        /// {100,100}
        /// </summary>
        /// <returns></returns>
        Point GetPosFromIndex(int index)
        {
            var couple = posList[index].Split(new char[] { ',' });

            int x = int.Parse(couple[0].Substring(1));
            int y = int.Parse(couple[1].Substring(0, couple[1].Length - 1));

            return new Point(x, y);
        }

        private void btnDrawText_Click(object sender, EventArgs e)
        {
            if (txtPosSource.Text == String.Empty ||
                cbInputPos.Text == String.Empty ||
                curComboIndex == -1 ||
                checkHide.Checked)
                return;

            IsDraw = true;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!IsDraw)
                return;

            for (int i = 0; i < pointList[curComboIndex].Count; i++)
            {
                if (pointList[curComboIndex][i] != Point.Empty)
                {
                    if (isDotDisplay)
                    {
                        if (curIndex2 == i)
                        {
                            nDotSize = 4;
                        }
                        else
                            nDotSize = 2;

                        e.Graphics.FillRectangle(new SolidBrush(curColor), pointList[curComboIndex][i].X - nDotSize / 2, pointList[curComboIndex][i].Y - nDotSize / 2, nDotSize, nDotSize);
                    }
                    else
                    {
                        int sw = (int)e.Graphics.MeasureString(segid[i], newFont).Width;
                        int sh = (int)e.Graphics.MeasureString(segid[i], newFont).Height;
                        e.Graphics.DrawString(segid[i], newFont, new SolidBrush(curColor), pointList[curComboIndex][i].X - sw / 2, pointList[curComboIndex][i].Y - sh / 2);
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = new Point(e.X, e.Y);

            lblMousePos1.Text = "Mouse Pos: (" + mousePos.X + "," + mousePos.Y + ")";
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (maxNum == 0)
            {
                txtResult.Text = String.Empty;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right && maxNum != 17)
            {
                maxNum++;
                txtResult.Text += "{" + e.X + "," + e.Y + "}, ";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!Available)
                return;

            if(dataGridView1.SelectedRows.Count == 0)
            {
                pictureBox1.Image = pictureBox2.Image = null;
                curIndex1 = -1;
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;
            if (index != curIndex1)
            {
                curIndex1 = index;
                //dataGridView2.Rows[curIndex1].Selected = true;
                //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView2.FirstDisplayedScrollingRowIndex;
                Bitmap bmp = new Bitmap(fullName1[index]);
                lblImageSize1.Text = "Image Size : " + bmp.Size.Width + "x" + bmp.Size.Height;
                pictureBox1.Image = bmp;

                bmp = new Bitmap(fullName2[index]);
                lblImageSize2.Text = "Image Size : " + bmp.Size.Width + "x" + bmp.Size.Height;
                pictureBox2.Image = bmp;
                maxNum = 0;
                IsDraw = false;
            }
        }

        private void btnBrowseNewBmpSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtNewBmpSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (maxNum == 0)
            {
                txtResult.Text = String.Empty;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right && maxNum != 17)
            {
                maxNum++;
                txtResult.Text += "{" + e.X + "," + e.Y + "}, ";
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = new Point(e.X, e.Y);

            lblMousePos2.Text = "Mouse Pos: (" + mousePos.X + "," + mousePos.Y + ")";
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (!Available)
                return;

            if(dataGridView2.SelectedRows.Count == 0)
            {
                curIndex2 = -1;
                nDotSize = 2;
                pictureBox1.Refresh();
                return;
            }

            int index = dataGridView2.SelectedRows[0].Index;
            if (index != curIndex2)
            {
                curIndex2 = index;
                pictureBox1.Refresh();
                //dataGridView1.Rows[curIndex2].Selected = true;
                //dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkHide.CheckedChanged += checkHide_CheckedChanged;
            dataGridView2.CellValueChanged +=dataGridView2_CellValueChanged;
        }

        int GetIndex(string idstr)
        {
            for (int i = 0; i < invertid.Length; i++)
            {
                if (invertid[i] == idstr)
                {
                    return i;
                }
            }

            return -1;
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            int index = cbInputPos.SelectedIndex;
            if (index == -1)
                return;

            var tempP = tempPos;

            for (int i = 0; i < pointList[index].Count; i++)
            {
                if (pointList[index][i] != Point.Empty)
                {
                    var idstr = segid[i];
                    var idx = GetIndex(idstr);

                    if (idx != -1)
                    {
                        tempP[idx] = pointList[index][i];
                    }
                }
            }

            if (tempP.Count > 0)
            {

            }
        }

        private void Segment16_CheckedChanged(object sender, EventArgs e)
        {
            if (IsDraw)
                return;

            //if (Segment16.Checked)
            //    numseg = 16;
            //else
            //    numseg = 17;
        }

        private void checkDot_CheckedChanged(object sender, EventArgs e)
        {
            isDotDisplay = checkDot.Checked;
            pictureBox1.Refresh();
        }

        private void cbInputPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            curComboIndex = cbInputPos.SelectedIndex;
            if (curComboIndex != preComboIndex)
            {
                preComboIndex = curComboIndex;
                if (pointList.Count == 0 || curComboIndex > pointList.Count - 1)
                    return;

                //txtResult.Text = readList[curComboIndex];
                foreach (var lPos in pointList[curComboIndex])
                {
                    dataGridView2.Rows.Add(lPos.X.ToString(), lPos.Y.ToString());
                }
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if(colorDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                label1.BackColor = colorDialog1.Color;
                curColor = colorDialog1.Color;
                pictureBox1.Refresh();
            }
        }

        private void checkHide_CheckedChanged(object sender, EventArgs e)
        {
            IsDraw = !checkHide.Checked;
            pictureBox1.Refresh();
        }

        private void checkStretch_CheckedChanged(object sender, EventArgs e)
        {
            if(checkStretch.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtResult.Text = String.Empty;
                try
                {
                    var c1 = (string)dataGridView2.Rows[e.RowIndex].Cells[0].Value;
                    var c2 = (string)dataGridView2.Rows[e.RowIndex].Cells[1].Value;
                    int nx = int.Parse(c1);
                    int ny = int.Parse(c2);

                    pointList[curComboIndex][e.RowIndex] = new Point(nx, ny);

                    pictureBox1.Refresh();
                }
                catch
                {
                    dataGridView2.CellValueChanged -= dataGridView2_CellValueChanged;
                    dataGridView2.Rows[e.RowIndex].Cells[0].Value = pointList[curComboIndex][e.RowIndex].X.ToString();
                    dataGridView2.Rows[e.RowIndex].Cells[1].Value = pointList[curComboIndex][e.RowIndex].Y.ToString();
                    dataGridView2.CellValueChanged += dataGridView2_CellValueChanged;
                    return;
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    txtResult.Text += "{" + (string)row.Cells[0].Value + "," + (string)row.Cells[1].Value + "}, ";
                }
            }
        }
    }
}
