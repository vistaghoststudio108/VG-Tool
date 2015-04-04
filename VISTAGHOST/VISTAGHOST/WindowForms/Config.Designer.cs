namespace Vistaghost.VISTAGHOST.WindowForms
{
    partial class Config
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Comments");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Header");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Add", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Delete");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Data Management");
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCloseBeginTag = new System.Windows.Forms.TextBox();
            this.txtCloseEndTag = new System.Windows.Forms.TextBox();
            this.txtOpenBeginTag = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnSingleSetting = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpenEndTag = new System.Windows.Forms.TextBox();
            this.chDisHistory = new System.Windows.Forms.CheckBox();
            this.cbDateFormat = new System.Windows.Forms.ComboBox();
            this.chAutoAddWithoutDialog = new System.Windows.Forms.CheckBox();
            this.pnHeaderSetting = new System.Windows.Forms.Panel();
            this.cbHeaderStyle = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox9 = new System.Windows.Forms.TextBox();
            this.richTextBox8 = new System.Windows.Forms.TextBox();
            this.richTextBox7 = new System.Windows.Forms.TextBox();
            this.richTextBox6 = new System.Windows.Forms.TextBox();
            this.richTextBox5 = new System.Windows.Forms.TextBox();
            this.richTextBox4 = new System.Windows.Forms.TextBox();
            this.richTextBox3 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.TextBox();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pnDataSetting = new System.Windows.Forms.Panel();
            this.btnClearData = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.btnBrowseExternal = new System.Windows.Forms.Button();
            this.txtExternalLink = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.pnSingleSetting.SuspendLayout();
            this.pnHeaderSetting.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnDataSetting.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(536, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(442, 366);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 27);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point(8, 7);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "nodeComment";
            treeNode1.Text = "Comments";
            treeNode2.Name = "nodeHeader";
            treeNode2.Text = "Header";
            treeNode3.Name = "Node2";
            treeNode3.Text = "Add";
            treeNode4.Name = "nodeDelete";
            treeNode4.Text = "Delete";
            treeNode5.Name = "nodeDataManagement";
            treeNode5.Text = "Data Management";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(161, 355);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCloseBeginTag);
            this.groupBox1.Controls.Add(this.txtCloseEndTag);
            this.groupBox1.Controls.Add(this.txtOpenBeginTag);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDate2);
            this.groupBox1.Controls.Add(this.lblDate1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 97);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comment tags format";
            // 
            // txtCloseBeginTag
            // 
            this.txtCloseBeginTag.Location = new System.Drawing.Point(7, 60);
            this.txtCloseBeginTag.Name = "txtCloseBeginTag";
            this.txtCloseBeginTag.Size = new System.Drawing.Size(106, 23);
            this.txtCloseBeginTag.TabIndex = 6;
            this.txtCloseBeginTag.TextChanged += new System.EventHandler(this.CommentInput_TextChanged);
            // 
            // txtCloseEndTag
            // 
            this.txtCloseEndTag.Location = new System.Drawing.Point(333, 60);
            this.txtCloseEndTag.Name = "txtCloseEndTag";
            this.txtCloseEndTag.Size = new System.Drawing.Size(41, 23);
            this.txtCloseEndTag.TabIndex = 10;
            this.txtCloseEndTag.TextChanged += new System.EventHandler(this.CommentInput_TextChanged);
            // 
            // txtOpenBeginTag
            // 
            this.txtOpenBeginTag.Location = new System.Drawing.Point(7, 23);
            this.txtOpenBeginTag.Name = "txtOpenBeginTag";
            this.txtOpenBeginTag.Size = new System.Drawing.Size(106, 23);
            this.txtOpenBeginTag.TabIndex = 1;
            this.txtOpenBeginTag.TextChanged += new System.EventHandler(this.CommentInput_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(256, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "<Account>)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(255, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "<Account>) <Content>";
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(175, 63);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(83, 15);
            this.lblDate2.TabIndex = 8;
            this.lblDate2.Text = "<yyyymmdd>";
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(174, 27);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(83, 15);
            this.lblDate1.TabIndex = 3;
            this.lblDate1.Text = "<yyyymmdd>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "<DevID> (";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "<DevID> (";
            // 
            // pnSingleSetting
            // 
            this.pnSingleSetting.Controls.Add(this.label3);
            this.pnSingleSetting.Controls.Add(this.txtOpenEndTag);
            this.pnSingleSetting.Controls.Add(this.chDisHistory);
            this.pnSingleSetting.Controls.Add(this.cbDateFormat);
            this.pnSingleSetting.Controls.Add(this.chAutoAddWithoutDialog);
            this.pnSingleSetting.Controls.Add(this.groupBox1);
            this.pnSingleSetting.Location = new System.Drawing.Point(695, 12);
            this.pnSingleSetting.Name = "pnSingleSetting";
            this.pnSingleSetting.Size = new System.Drawing.Size(451, 355);
            this.pnSingleSetting.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Date format";
            // 
            // txtOpenEndTag
            // 
            this.txtOpenEndTag.Location = new System.Drawing.Point(394, 23);
            this.txtOpenEndTag.Name = "txtOpenEndTag";
            this.txtOpenEndTag.Size = new System.Drawing.Size(41, 23);
            this.txtOpenEndTag.TabIndex = 5;
            this.txtOpenEndTag.TextChanged += new System.EventHandler(this.CommentInput_TextChanged);
            // 
            // chDisHistory
            // 
            this.chDisHistory.AutoSize = true;
            this.chDisHistory.Location = new System.Drawing.Point(11, 172);
            this.chDisHistory.Name = "chDisHistory";
            this.chDisHistory.Size = new System.Drawing.Size(200, 19);
            this.chDisHistory.TabIndex = 13;
            this.chDisHistory.Text = "Display history in output window";
            this.chDisHistory.UseVisualStyleBackColor = true;
            this.chDisHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Options_MouseClick);
            // 
            // cbDateFormat
            // 
            this.cbDateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDateFormat.FormattingEnabled = true;
            this.cbDateFormat.Items.AddRange(new object[] {
            "YYYY/MM/DD",
            "YYYY/DD/MM",
            "DD/MM/YYYY",
            "MM/DD/YYYY"});
            this.cbDateFormat.Location = new System.Drawing.Point(84, 112);
            this.cbDateFormat.Name = "cbDateFormat";
            this.cbDateFormat.Size = new System.Drawing.Size(108, 23);
            this.cbDateFormat.TabIndex = 11;
            this.cbDateFormat.SelectedIndexChanged += new System.EventHandler(this.cbDateFormat_SelectedIndexChanged);
            // 
            // chAutoAddWithoutDialog
            // 
            this.chAutoAddWithoutDialog.AutoSize = true;
            this.chAutoAddWithoutDialog.Location = new System.Drawing.Point(11, 147);
            this.chAutoAddWithoutDialog.Name = "chAutoAddWithoutDialog";
            this.chAutoAddWithoutDialog.Size = new System.Drawing.Size(248, 19);
            this.chAutoAddWithoutDialog.TabIndex = 12;
            this.chAutoAddWithoutDialog.Text = "Add comment tags without display dialog";
            this.chAutoAddWithoutDialog.UseVisualStyleBackColor = true;
            this.chAutoAddWithoutDialog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Options_MouseClick);
            // 
            // pnHeaderSetting
            // 
            this.pnHeaderSetting.Controls.Add(this.checkBox10);
            this.pnHeaderSetting.Controls.Add(this.cbHeaderStyle);
            this.pnHeaderSetting.Controls.Add(this.groupBox3);
            this.pnHeaderSetting.Controls.Add(this.label4);
            this.pnHeaderSetting.Location = new System.Drawing.Point(175, 7);
            this.pnHeaderSetting.Name = "pnHeaderSetting";
            this.pnHeaderSetting.Size = new System.Drawing.Size(451, 355);
            this.pnHeaderSetting.TabIndex = 4;
            // 
            // cbHeaderStyle
            // 
            this.cbHeaderStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeaderStyle.FormattingEnabled = true;
            this.cbHeaderStyle.Items.AddRange(new object[] {
            "Aloka1",
            "Aloka2",
            "Aloka3"});
            this.cbHeaderStyle.Location = new System.Drawing.Point(346, 323);
            this.cbHeaderStyle.Name = "cbHeaderStyle";
            this.cbHeaderStyle.Size = new System.Drawing.Size(102, 23);
            this.cbHeaderStyle.TabIndex = 46;
            this.cbHeaderStyle.SelectedIndexChanged += new System.EventHandler(this.cbHeaderStyle_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBox9);
            this.groupBox3.Controls.Add(this.richTextBox8);
            this.groupBox3.Controls.Add(this.richTextBox7);
            this.groupBox3.Controls.Add(this.richTextBox6);
            this.groupBox3.Controls.Add(this.richTextBox5);
            this.groupBox3.Controls.Add(this.richTextBox4);
            this.groupBox3.Controls.Add(this.richTextBox3);
            this.groupBox3.Controls.Add(this.richTextBox2);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(this.txtHistory);
            this.groupBox3.Controls.Add(this.checkBox11);
            this.groupBox3.Controls.Add(this.checkBox9);
            this.groupBox3.Controls.Add(this.checkBox8);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.checkBox4);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(7, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 312);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Header format";
            // 
            // richTextBox9
            // 
            this.richTextBox9.Enabled = false;
            this.richTextBox9.Location = new System.Drawing.Point(11, 202);
            this.richTextBox9.Name = "richTextBox9";
            this.richTextBox9.Size = new System.Drawing.Size(162, 23);
            this.richTextBox9.TabIndex = 22;
            this.richTextBox9.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox8
            // 
            this.richTextBox8.Enabled = false;
            this.richTextBox8.Location = new System.Drawing.Point(11, 176);
            this.richTextBox8.Name = "richTextBox8";
            this.richTextBox8.Size = new System.Drawing.Size(162, 23);
            this.richTextBox8.TabIndex = 21;
            this.richTextBox8.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox7
            // 
            this.richTextBox7.Enabled = false;
            this.richTextBox7.Location = new System.Drawing.Point(11, 150);
            this.richTextBox7.Name = "richTextBox7";
            this.richTextBox7.Size = new System.Drawing.Size(162, 23);
            this.richTextBox7.TabIndex = 20;
            this.richTextBox7.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox6
            // 
            this.richTextBox6.Enabled = false;
            this.richTextBox6.Location = new System.Drawing.Point(11, 124);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.Size = new System.Drawing.Size(162, 23);
            this.richTextBox6.TabIndex = 19;
            this.richTextBox6.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox5
            // 
            this.richTextBox5.Enabled = false;
            this.richTextBox5.Location = new System.Drawing.Point(11, 98);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(162, 23);
            this.richTextBox5.TabIndex = 18;
            this.richTextBox5.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox4
            // 
            this.richTextBox4.Enabled = false;
            this.richTextBox4.Location = new System.Drawing.Point(11, 72);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(162, 23);
            this.richTextBox4.TabIndex = 17;
            this.richTextBox4.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Enabled = false;
            this.richTextBox3.Location = new System.Drawing.Point(11, 46);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(162, 23);
            this.richTextBox3.TabIndex = 16;
            this.richTextBox3.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(11, 279);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(424, 23);
            this.richTextBox2.TabIndex = 24;
            this.richTextBox2.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(11, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(424, 23);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // txtHistory
            // 
            this.txtHistory.Enabled = false;
            this.txtHistory.Location = new System.Drawing.Point(11, 228);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.Size = new System.Drawing.Size(424, 48);
            this.txtHistory.TabIndex = 23;
            this.txtHistory.TextChanged += new System.EventHandler(this.HeaderInput_TextChanged);
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point(179, 206);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(15, 14);
            this.checkBox11.TabIndex = 31;
            this.checkBox11.Tag = "6";
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox11.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(179, 180);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(15, 14);
            this.checkBox9.TabIndex = 30;
            this.checkBox9.Tag = "5";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox9.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(179, 154);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(15, 14);
            this.checkBox8.TabIndex = 29;
            this.checkBox8.Tag = "4";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox8.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(179, 128);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(15, 14);
            this.checkBox7.TabIndex = 28;
            this.checkBox7.Tag = "3";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox7.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(179, 102);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(15, 14);
            this.checkBox6.TabIndex = 27;
            this.checkBox6.Tag = "2";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox6.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(179, 76);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(15, 14);
            this.checkBox5.TabIndex = 26;
            this.checkBox5.Tag = "1";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox5.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(179, 50);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 25;
            this.checkBox4.Tag = "0";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox4.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(267, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Header Style";
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(18, 330);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(220, 19);
            this.checkBox10.TabIndex = 1;
            this.checkBox10.Tag = "7";
            this.checkBox10.Text = "Add break line between components";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponent_MouseClick);
            this.checkBox10.CheckedChanged += new System.EventHandler(this.AddComponent_CheckedChanged);
            // 
            // pnDataSetting
            // 
            this.pnDataSetting.Controls.Add(this.btnClearData);
            this.pnDataSetting.Controls.Add(this.groupBox4);
            this.pnDataSetting.Location = new System.Drawing.Point(25, 451);
            this.pnDataSetting.Name = "pnDataSetting";
            this.pnDataSetting.Size = new System.Drawing.Size(451, 355);
            this.pnDataSetting.TabIndex = 5;
            // 
            // btnClearData
            // 
            this.btnClearData.Location = new System.Drawing.Point(7, 109);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(111, 26);
            this.btnClearData.TabIndex = 39;
            this.btnClearData.Text = "Clear saved data";
            this.btnClearData.UseVisualStyleBackColor = true;
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton4);
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.btnBrowseExternal);
            this.groupBox4.Controls.Add(this.txtExternalLink);
            this.groupBox4.Location = new System.Drawing.Point(7, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(441, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(10, 50);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(174, 19);
            this.radioButton4.TabIndex = 36;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Save data at external storage";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.storage_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(10, 22);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(173, 19);
            this.radioButton3.TabIndex = 35;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Save data at internal storage";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.storage_CheckedChanged);
            // 
            // btnBrowseExternal
            // 
            this.btnBrowseExternal.Enabled = false;
            this.btnBrowseExternal.Location = new System.Drawing.Point(402, 47);
            this.btnBrowseExternal.Name = "btnBrowseExternal";
            this.btnBrowseExternal.Size = new System.Drawing.Size(33, 24);
            this.btnBrowseExternal.TabIndex = 38;
            this.btnBrowseExternal.Text = "...";
            this.btnBrowseExternal.UseVisualStyleBackColor = true;
            // 
            // txtExternalLink
            // 
            this.txtExternalLink.Enabled = false;
            this.txtExternalLink.Location = new System.Drawing.Point(190, 48);
            this.txtExternalLink.Name = "txtExternalLink";
            this.txtExternalLink.Size = new System.Drawing.Size(206, 23);
            this.txtExternalLink.TabIndex = 37;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 743);
            this.Controls.Add(this.pnSingleSetting);
            this.Controls.Add(this.pnDataSetting);
            this.Controls.Add(this.pnHeaderSetting);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Config";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vistaghost Configurations";
            this.Load += new System.EventHandler(this.Config_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Config_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnSingleSetting.ResumeLayout(false);
            this.pnSingleSetting.PerformLayout();
            this.pnHeaderSetting.ResumeLayout(false);
            this.pnHeaderSetting.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pnDataSetting.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnSingleSetting;
        private System.Windows.Forms.CheckBox chAutoAddWithoutDialog;
        private System.Windows.Forms.CheckBox chDisHistory;
        private System.Windows.Forms.Panel pnHeaderSetting;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDateFormat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel pnDataSetting;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnBrowseExternal;
        private System.Windows.Forms.TextBox txtExternalLink;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TextBox txtHistory;
        private System.Windows.Forms.TextBox richTextBox2;
        private System.Windows.Forms.TextBox richTextBox1;
        private System.Windows.Forms.TextBox richTextBox9;
        private System.Windows.Forms.TextBox richTextBox8;
        private System.Windows.Forms.TextBox richTextBox7;
        private System.Windows.Forms.TextBox richTextBox6;
        private System.Windows.Forms.TextBox richTextBox5;
        private System.Windows.Forms.TextBox richTextBox4;
        private System.Windows.Forms.TextBox richTextBox3;
        private System.Windows.Forms.TextBox txtCloseBeginTag;
        private System.Windows.Forms.TextBox txtOpenBeginTag;
        private System.Windows.Forms.TextBox txtOpenEndTag;
        private System.Windows.Forms.TextBox txtCloseEndTag;
        private System.Windows.Forms.ComboBox cbHeaderStyle;
        private System.Windows.Forms.Label label4;
    }
}