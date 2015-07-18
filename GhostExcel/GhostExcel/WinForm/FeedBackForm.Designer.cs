namespace GhostExcel.WinForm
{
    partial class FeedBackForm
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rdGmail = new System.Windows.Forms.RadioButton();
            this.rdOutlook = new System.Windows.Forms.RadioButton();
            this.lblMailServer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 12);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(456, 86);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // btnSendMail
            // 
            this.btnSendMail.Location = new System.Drawing.Point(393, 125);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(75, 25);
            this.btnSendMail.TabIndex = 1;
            this.btnSendMail.Text = "Send";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(13, 101);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(74, 15);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Attachments";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rdGmail
            // 
            this.rdGmail.AutoSize = true;
            this.rdGmail.Checked = true;
            this.rdGmail.Location = new System.Drawing.Point(92, 129);
            this.rdGmail.Name = "rdGmail";
            this.rdGmail.Size = new System.Drawing.Size(58, 19);
            this.rdGmail.TabIndex = 0;
            this.rdGmail.TabStop = true;
            this.rdGmail.Text = "Gmail";
            this.rdGmail.UseVisualStyleBackColor = true;
            // 
            // rdOutlook
            // 
            this.rdOutlook.AutoSize = true;
            this.rdOutlook.Location = new System.Drawing.Point(175, 129);
            this.rdOutlook.Name = "rdOutlook";
            this.rdOutlook.Size = new System.Drawing.Size(67, 19);
            this.rdOutlook.TabIndex = 0;
            this.rdOutlook.Text = "Outlook";
            this.rdOutlook.UseVisualStyleBackColor = true;
            // 
            // lblMailServer
            // 
            this.lblMailServer.AutoSize = true;
            this.lblMailServer.Location = new System.Drawing.Point(13, 130);
            this.lblMailServer.Name = "lblMailServer";
            this.lblMailServer.Size = new System.Drawing.Size(73, 15);
            this.lblMailServer.TabIndex = 5;
            this.lblMailServer.Text = "Mail server :";
            // 
            // FeedBackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 156);
            this.ControlBox = false;
            this.Controls.Add(this.lblMailServer);
            this.Controls.Add(this.rdOutlook);
            this.Controls.Add(this.rdGmail);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendMail);
            this.Controls.Add(this.txtMessage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeedBackForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Send Feedback";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FeedBackForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton rdOutlook;
        private System.Windows.Forms.RadioButton rdGmail;
        private System.Windows.Forms.Label lblMailServer;
    }
}