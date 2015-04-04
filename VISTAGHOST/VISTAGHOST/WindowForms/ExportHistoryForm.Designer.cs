namespace Vistaghost.VISTAGHOST.WindowForms
{
    partial class ExportHistoryForm
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdXMLFile = new System.Windows.Forms.RadioButton();
            this.rdTextFile = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chOpenFile = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBrowse.Location = new System.Drawing.Point(365, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdXMLFile);
            this.groupBox1.Controls.Add(this.rdTextFile);
            this.groupBox1.Location = new System.Drawing.Point(15, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File type";
            // 
            // rdXMLFile
            // 
            this.rdXMLFile.AutoSize = true;
            this.rdXMLFile.Location = new System.Drawing.Point(121, 21);
            this.rdXMLFile.Name = "rdXMLFile";
            this.rdXMLFile.Size = new System.Drawing.Size(52, 19);
            this.rdXMLFile.TabIndex = 4;
            this.rdXMLFile.Text = "*.xml";
            this.rdXMLFile.UseVisualStyleBackColor = true;
            // 
            // rdTextFile
            // 
            this.rdTextFile.AutoSize = true;
            this.rdTextFile.Checked = true;
            this.rdTextFile.Location = new System.Drawing.Point(32, 21);
            this.rdTextFile.Name = "rdTextFile";
            this.rdTextFile.Size = new System.Drawing.Size(46, 19);
            this.rdTextFile.TabIndex = 3;
            this.rdTextFile.TabStop = true;
            this.rdTextFile.Text = "*.txt";
            this.rdTextFile.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(322, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(241, 98);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(71, 12);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(288, 23);
            this.txtLocation.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location:";
            // 
            // chOpenFile
            // 
            this.chOpenFile.AutoSize = true;
            this.chOpenFile.Checked = true;
            this.chOpenFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chOpenFile.Location = new System.Drawing.Point(11, 102);
            this.chOpenFile.Name = "chOpenFile";
            this.chOpenFile.Size = new System.Drawing.Size(159, 19);
            this.chOpenFile.TabIndex = 7;
            this.chOpenFile.Text = "Open file when complete";
            this.chOpenFile.UseVisualStyleBackColor = true;
            // 
            // ExportHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 129);
            this.Controls.Add(this.chOpenFile);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnBrowse);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportHistoryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export History";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdXMLFile;
        private System.Windows.Forms.RadioButton rdTextFile;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chOpenFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}