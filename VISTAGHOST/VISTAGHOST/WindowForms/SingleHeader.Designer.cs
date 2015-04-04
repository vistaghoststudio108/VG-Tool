namespace Vistaghost.VISTAGHOST.WindowForms
{
    partial class SingleHeader
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dtParamView = new System.Windows.Forms.DataGridView();
            this.txtParaList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chInput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblParaNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtParamView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(156, 119);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dtParamView
            // 
            this.dtParamView.AllowUserToAddRows = false;
            this.dtParamView.AllowUserToDeleteRows = false;
            this.dtParamView.AllowUserToOrderColumns = true;
            this.dtParamView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtParamView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtParamView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtParamView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dtParamView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtParamView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dtParamView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtParamView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtParaList,
            this.chInput,
            this.chOutput});
            this.dtParamView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtParamView.EnableHeadersVisualStyles = false;
            this.dtParamView.Location = new System.Drawing.Point(0, 0);
            this.dtParamView.MultiSelect = false;
            this.dtParamView.Name = "dtParamView";
            this.dtParamView.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtParamView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtParamView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtParamView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtParamView.Size = new System.Drawing.Size(385, 115);
            this.dtParamView.TabIndex = 3;
            // 
            // txtParaList
            // 
            this.txtParaList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.txtParaList.DefaultCellStyle = dataGridViewCellStyle2;
            this.txtParaList.FillWeight = 35F;
            this.txtParaList.HeaderText = "Arguments";
            this.txtParaList.Name = "txtParaList";
            this.txtParaList.ReadOnly = true;
            this.txtParaList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtParaList.Width = 254;
            // 
            // chInput
            // 
            this.chInput.FillWeight = 7.614227F;
            this.chInput.HeaderText = "Input";
            this.chInput.Name = "chInput";
            // 
            // chOutput
            // 
            this.chOutput.FillWeight = 7.614227F;
            this.chOutput.HeaderText = "Output";
            this.chOutput.Name = "chOutput";
            // 
            // lblParaNumber
            // 
            this.lblParaNumber.AutoSize = true;
            this.lblParaNumber.Location = new System.Drawing.Point(7, 125);
            this.lblParaNumber.Name = "lblParaNumber";
            this.lblParaNumber.Size = new System.Drawing.Size(0, 15);
            this.lblParaNumber.TabIndex = 4;
            // 
            // SingleHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(385, 153);
            this.Controls.Add(this.lblParaNumber);
            this.Controls.Add(this.dtParamView);
            this.Controls.Add(this.btnAdd);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleHeader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Header Info";
            this.Load += new System.EventHandler(this.InputHeader_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SingleHeader_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dtParamView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dtParamView;
        private System.Windows.Forms.Label lblParaNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtParaList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chInput;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chOutput;
    }
}