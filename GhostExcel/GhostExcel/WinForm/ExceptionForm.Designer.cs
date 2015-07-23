namespace GhostExcel.WinForm
{
    partial class ExceptionForm
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtApplication = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTargetSite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExceptionType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.lvExceptionDetail = new System.Windows.Forms.ListView();
            this.columnProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnInformation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeException = new System.Windows.Forms.TreeView();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 209);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(457, 63);
            this.txtMessage.TabIndex = 0;
            // 
            // btnSendMail
            // 
            this.btnSendMail.Location = new System.Drawing.Point(414, 324);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(75, 25);
            this.btnSendMail.TabIndex = 1;
            this.btnSendMail.Text = "Send";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(333, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(477, 306);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtDateTime);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtApplication);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtTargetSite);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtExceptionType);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(469, 278);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GhostExcel.Properties.Resources.Warning;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(14, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(372, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Please add a brief description of how we can reprocedure the error.";
            // 
            // txtDateTime
            // 
            this.txtDateTime.Location = new System.Drawing.Point(323, 152);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Size = new System.Drawing.Size(140, 21);
            this.txtDateTime.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Date/Time :";
            // 
            // txtApplication
            // 
            this.txtApplication.Location = new System.Drawing.Point(85, 152);
            this.txtApplication.Name = "txtApplication";
            this.txtApplication.Size = new System.Drawing.Size(149, 21);
            this.txtApplication.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Application :";
            // 
            // txtTargetSite
            // 
            this.txtTargetSite.Location = new System.Drawing.Point(85, 117);
            this.txtTargetSite.Name = "txtTargetSite";
            this.txtTargetSite.Size = new System.Drawing.Size(378, 21);
            this.txtTargetSite.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target Site :";
            // 
            // txtExceptionType
            // 
            this.txtExceptionType.Location = new System.Drawing.Point(108, 81);
            this.txtExceptionType.Name = "txtExceptionType";
            this.txtExceptionType.Size = new System.Drawing.Size(355, 21);
            this.txtExceptionType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Exception Type :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "The application has crashed and it will now be dismissed.\r\nClick Send to send a b" +
    "ug report to us.";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.lvExceptionDetail);
            this.tabPage2.Controls.Add(this.treeException);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(469, 278);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Exception";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(303, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Exception Details (double click on items to see details)";
            // 
            // lvExceptionDetail
            // 
            this.lvExceptionDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProperty,
            this.columnInformation});
            this.lvExceptionDetail.FullRowSelect = true;
            this.lvExceptionDetail.GridLines = true;
            this.lvExceptionDetail.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvExceptionDetail.Location = new System.Drawing.Point(6, 110);
            this.lvExceptionDetail.MultiSelect = false;
            this.lvExceptionDetail.Name = "lvExceptionDetail";
            this.lvExceptionDetail.ShowItemToolTips = true;
            this.lvExceptionDetail.Size = new System.Drawing.Size(457, 162);
            this.lvExceptionDetail.TabIndex = 2;
            this.lvExceptionDetail.UseCompatibleStateImageBehavior = false;
            this.lvExceptionDetail.View = System.Windows.Forms.View.Details;
            // 
            // columnProperty
            // 
            this.columnProperty.Text = "Property";
            this.columnProperty.Width = 100;
            // 
            // columnInformation
            // 
            this.columnInformation.Text = "Information";
            this.columnInformation.Width = 350;
            // 
            // treeException
            // 
            this.treeException.Location = new System.Drawing.Point(6, 21);
            this.treeException.Name = "treeException";
            this.treeException.Size = new System.Drawing.Size(457, 60);
            this.treeException.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Exception(s):";
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 355);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendMail);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ghost Excel Add-in Error";
            this.Load += new System.EventHandler(this.ExceptionForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FeedBackForm_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.Button btnCancel;
        //private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApplication;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTargetSite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExceptionType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView lvExceptionDetail;
        private System.Windows.Forms.TreeView treeException;
        private System.Windows.Forms.ColumnHeader columnProperty;
        private System.Windows.Forms.ColumnHeader columnInformation;
    }
}