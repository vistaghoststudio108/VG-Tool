namespace GreatTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOldBmpSource = new System.Windows.Forms.TextBox();
            this.btnBrowseOldBmpSource = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnLoadBMP = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblImageSize1 = new System.Windows.Forms.Label();
            this.lblImageNum1 = new System.Windows.Forms.Label();
            this.btnDrawText = new System.Windows.Forms.Button();
            this.txtPosSource = new System.Windows.Forms.TextBox();
            this.btnBrowsePos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblMousePos1 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.cbInputPos = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sdf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblImageSize2 = new System.Windows.Forms.Label();
            this.lblImageNum2 = new System.Windows.Forms.Label();
            this.lblMousePos2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNewBmpSource = new System.Windows.Forms.TextBox();
            this.btnBrowseNewBmpSource = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnInvert = new System.Windows.Forms.Button();
            this.txtInvertResult = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Segment16 = new System.Windows.Forms.RadioButton();
            this.Segment17 = new System.Windows.Forms.RadioButton();
            this.checkDot = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkHide = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOldBmpSource
            // 
            this.txtOldBmpSource.Location = new System.Drawing.Point(121, 16);
            this.txtOldBmpSource.Name = "txtOldBmpSource";
            this.txtOldBmpSource.ReadOnly = true;
            this.txtOldBmpSource.Size = new System.Drawing.Size(394, 23);
            this.txtOldBmpSource.TabIndex = 1;
            this.txtOldBmpSource.Text = "C:\\Users\\thuanpv3\\Desktop\\Compare image\\New";
            // 
            // btnBrowseOldBmpSource
            // 
            this.btnBrowseOldBmpSource.Location = new System.Drawing.Point(522, 15);
            this.btnBrowseOldBmpSource.Name = "btnBrowseOldBmpSource";
            this.btnBrowseOldBmpSource.Size = new System.Drawing.Size(87, 25);
            this.btnBrowseOldBmpSource.TabIndex = 2;
            this.btnBrowseOldBmpSource.Text = "Browse";
            this.btnBrowseOldBmpSource.UseVisualStyleBackColor = true;
            this.btnBrowseOldBmpSource.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLoadBMP
            // 
            this.btnLoadBMP.Location = new System.Drawing.Point(14, 125);
            this.btnLoadBMP.Name = "btnLoadBMP";
            this.btnLoadBMP.Size = new System.Drawing.Size(75, 23);
            this.btnLoadBMP.TabIndex = 3;
            this.btnLoadBMP.Text = "Load Bmp";
            this.btnLoadBMP.UseVisualStyleBackColor = true;
            this.btnLoadBMP.Click += new System.EventHandler(this.btnLoadBMP_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(309, 165);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // lblImageSize1
            // 
            this.lblImageSize1.AutoSize = true;
            this.lblImageSize1.Location = new System.Drawing.Point(306, 147);
            this.lblImageSize1.Name = "lblImageSize1";
            this.lblImageSize1.Size = new System.Drawing.Size(69, 15);
            this.lblImageSize1.TabIndex = 8;
            this.lblImageSize1.Text = "Image Size :";
            // 
            // lblImageNum1
            // 
            this.lblImageNum1.AutoSize = true;
            this.lblImageNum1.Location = new System.Drawing.Point(425, 147);
            this.lblImageNum1.Name = "lblImageNum1";
            this.lblImageNum1.Size = new System.Drawing.Size(76, 15);
            this.lblImageNum1.TabIndex = 8;
            this.lblImageNum1.Text = "Image Num :";
            // 
            // btnDrawText
            // 
            this.btnDrawText.Location = new System.Drawing.Point(14, 427);
            this.btnDrawText.Name = "btnDrawText";
            this.btnDrawText.Size = new System.Drawing.Size(75, 24);
            this.btnDrawText.TabIndex = 9;
            this.btnDrawText.Text = "Draw ID";
            this.btnDrawText.UseVisualStyleBackColor = true;
            this.btnDrawText.Click += new System.EventHandler(this.btnDrawText_Click);
            // 
            // txtPosSource
            // 
            this.txtPosSource.Location = new System.Drawing.Point(121, 74);
            this.txtPosSource.Name = "txtPosSource";
            this.txtPosSource.ReadOnly = true;
            this.txtPosSource.Size = new System.Drawing.Size(394, 23);
            this.txtPosSource.TabIndex = 1;
            this.txtPosSource.Text = "C:\\Users\\thuanpv3\\Desktop\\pos.txt";
            // 
            // btnBrowsePos
            // 
            this.btnBrowsePos.Location = new System.Drawing.Point(522, 73);
            this.btnBrowsePos.Name = "btnBrowsePos";
            this.btnBrowsePos.Size = new System.Drawing.Size(87, 25);
            this.btnBrowsePos.TabIndex = 2;
            this.btnBrowsePos.Text = "Browse";
            this.btnBrowsePos.UseVisualStyleBackColor = true;
            this.btnBrowsePos.Click += new System.EventHandler(this.btnBrowsePos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Old Bmp Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Position Source";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblMousePos1
            // 
            this.lblMousePos1.AutoSize = true;
            this.lblMousePos1.Location = new System.Drawing.Point(306, 424);
            this.lblMousePos1.Name = "lblMousePos1";
            this.lblMousePos1.Size = new System.Drawing.Size(71, 15);
            this.lblMousePos1.TabIndex = 8;
            this.lblMousePos1.Text = "Mouse Pos :";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(15, 457);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(811, 23);
            this.txtResult.TabIndex = 12;
            // 
            // cbInputPos
            // 
            this.cbInputPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputPos.FormattingEnabled = true;
            this.cbInputPos.Location = new System.Drawing.Point(107, 125);
            this.cbInputPos.Name = "cbInputPos";
            this.cbInputPos.Size = new System.Drawing.Size(123, 23);
            this.cbInputPos.TabIndex = 13;
            this.cbInputPos.SelectedIndexChanged += new System.EventHandler(this.cbInputPos_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sdf});
            this.dataGridView1.Location = new System.Drawing.Point(15, 165);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(141, 256);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // sdf
            // 
            this.sdf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sdf.HeaderText = "Column1";
            this.sdf.Name = "sdf";
            this.sdf.ReadOnly = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(571, 165);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // lblImageSize2
            // 
            this.lblImageSize2.AutoSize = true;
            this.lblImageSize2.Location = new System.Drawing.Point(568, 147);
            this.lblImageSize2.Name = "lblImageSize2";
            this.lblImageSize2.Size = new System.Drawing.Size(69, 15);
            this.lblImageSize2.TabIndex = 8;
            this.lblImageSize2.Text = "Image Size :";
            // 
            // lblImageNum2
            // 
            this.lblImageNum2.AutoSize = true;
            this.lblImageNum2.Location = new System.Drawing.Point(687, 147);
            this.lblImageNum2.Name = "lblImageNum2";
            this.lblImageNum2.Size = new System.Drawing.Size(76, 15);
            this.lblImageNum2.TabIndex = 8;
            this.lblImageNum2.Text = "Image Num :";
            // 
            // lblMousePos2
            // 
            this.lblMousePos2.AutoSize = true;
            this.lblMousePos2.Location = new System.Drawing.Point(568, 424);
            this.lblMousePos2.Name = "lblMousePos2";
            this.lblMousePos2.Size = new System.Drawing.Size(71, 15);
            this.lblMousePos2.TabIndex = 8;
            this.lblMousePos2.Text = "Mouse Pos :";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dataGridView2.Location = new System.Drawing.Point(162, 165);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(141, 256);
            this.dataGridView2.TabIndex = 14;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // txtNewBmpSource
            // 
            this.txtNewBmpSource.Location = new System.Drawing.Point(121, 45);
            this.txtNewBmpSource.Name = "txtNewBmpSource";
            this.txtNewBmpSource.ReadOnly = true;
            this.txtNewBmpSource.Size = new System.Drawing.Size(394, 23);
            this.txtNewBmpSource.TabIndex = 1;
            this.txtNewBmpSource.Text = "C:\\Users\\thuanpv3\\Desktop\\Compare image\\Old";
            // 
            // btnBrowseNewBmpSource
            // 
            this.btnBrowseNewBmpSource.Location = new System.Drawing.Point(522, 44);
            this.btnBrowseNewBmpSource.Name = "btnBrowseNewBmpSource";
            this.btnBrowseNewBmpSource.Size = new System.Drawing.Size(87, 25);
            this.btnBrowseNewBmpSource.TabIndex = 2;
            this.btnBrowseNewBmpSource.Text = "Browse";
            this.btnBrowseNewBmpSource.UseVisualStyleBackColor = true;
            this.btnBrowseNewBmpSource.Click += new System.EventHandler(this.btnBrowseNewBmpSource_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "New Bmp Source";
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(15, 486);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(75, 23);
            this.btnInvert.TabIndex = 15;
            this.btnInvert.Text = "Invert";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            // 
            // txtInvertResult
            // 
            this.txtInvertResult.Location = new System.Drawing.Point(12, 515);
            this.txtInvertResult.Name = "txtInvertResult";
            this.txtInvertResult.ReadOnly = true;
            this.txtInvertResult.Size = new System.Drawing.Size(811, 23);
            this.txtInvertResult.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Segment17);
            this.groupBox1.Controls.Add(this.Segment16);
            this.groupBox1.Location = new System.Drawing.Point(627, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 86);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Segment Type";
            // 
            // Segment16
            // 
            this.Segment16.AutoSize = true;
            this.Segment16.Checked = true;
            this.Segment16.Location = new System.Drawing.Point(22, 22);
            this.Segment16.Name = "Segment16";
            this.Segment16.Size = new System.Drawing.Size(87, 19);
            this.Segment16.TabIndex = 0;
            this.Segment16.TabStop = true;
            this.Segment16.Text = "16 Segment";
            this.Segment16.UseVisualStyleBackColor = true;
            this.Segment16.CheckedChanged += new System.EventHandler(this.Segment16_CheckedChanged);
            // 
            // Segment17
            // 
            this.Segment17.AutoSize = true;
            this.Segment17.Location = new System.Drawing.Point(22, 47);
            this.Segment17.Name = "Segment17";
            this.Segment17.Size = new System.Drawing.Size(87, 19);
            this.Segment17.TabIndex = 0;
            this.Segment17.Text = "17 Segment";
            this.Segment17.UseVisualStyleBackColor = true;
            // 
            // checkDot
            // 
            this.checkDot.AutoSize = true;
            this.checkDot.Location = new System.Drawing.Point(121, 431);
            this.checkDot.Name = "checkDot";
            this.checkDot.Size = new System.Drawing.Size(45, 19);
            this.checkDot.TabIndex = 17;
            this.checkDot.Text = "Dot";
            this.checkDot.UseVisualStyleBackColor = true;
            this.checkDot.CheckedChanged += new System.EventHandler(this.checkDot_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Location = new System.Drawing.Point(262, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseClick);
            // 
            // checkHide
            // 
            this.checkHide.AutoSize = true;
            this.checkHide.Location = new System.Drawing.Point(172, 431);
            this.checkHide.Name = "checkHide";
            this.checkHide.Size = new System.Drawing.Size(51, 19);
            this.checkHide.TabIndex = 17;
            this.checkHide.Text = "Hide";
            this.checkHide.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 571);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkHide);
            this.Controls.Add(this.checkDot);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnInvert);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbInputPos);
            this.Controls.Add(this.txtInvertResult);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDrawText);
            this.Controls.Add(this.lblMousePos2);
            this.Controls.Add(this.lblMousePos1);
            this.Controls.Add(this.lblImageNum2);
            this.Controls.Add(this.lblImageNum1);
            this.Controls.Add(this.lblImageSize2);
            this.Controls.Add(this.lblImageSize1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLoadBMP);
            this.Controls.Add(this.btnBrowsePos);
            this.Controls.Add(this.btnBrowseNewBmpSource);
            this.Controls.Add(this.btnBrowseOldBmpSource);
            this.Controls.Add(this.txtPosSource);
            this.Controls.Add(this.txtNewBmpSource);
            this.Controls.Add(this.txtOldBmpSource);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOldBmpSource;
        private System.Windows.Forms.Button btnBrowseOldBmpSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnLoadBMP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblImageSize1;
        private System.Windows.Forms.Label lblImageNum1;
        private System.Windows.Forms.Button btnDrawText;
        private System.Windows.Forms.TextBox txtPosSource;
        private System.Windows.Forms.Button btnBrowsePos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblMousePos1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ComboBox cbInputPos;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdf;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblImageSize2;
        private System.Windows.Forms.Label lblImageNum2;
        private System.Windows.Forms.Label lblMousePos2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TextBox txtNewBmpSource;
        private System.Windows.Forms.Button btnBrowseNewBmpSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.TextBox txtInvertResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Segment17;
        private System.Windows.Forms.RadioButton Segment16;
        private System.Windows.Forms.CheckBox checkDot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckBox checkHide;
    }
}

