namespace Vistaghost.VISTAGHOST.WindowForms
{
    partial class ViewFunction
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtParamView = new System.Windows.Forms.DataGridView();
            this.txtParaList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chInput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.dtFunctions = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onlyViewThisFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkAdded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtParamView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFunctions)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(12, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(98, 29);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnFindAll_Click);
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
            this.chOutput,
            this.colValue});
            this.dtParamView.EnableHeadersVisualStyles = false;
            this.dtParamView.Location = new System.Drawing.Point(358, 47);
            this.dtParamView.Name = "dtParamView";
            this.dtParamView.ReadOnly = true;
            this.dtParamView.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtParamView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtParamView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtParamView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtParamView.Size = new System.Drawing.Size(336, 292);
            this.dtParamView.TabIndex = 4;
            // 
            // txtParaList
            // 
            this.txtParaList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.txtParaList.DefaultCellStyle = dataGridViewCellStyle2;
            this.txtParaList.FillWeight = 35F;
            this.txtParaList.HeaderText = "Arguments";
            this.txtParaList.Name = "txtParaList";
            this.txtParaList.ReadOnly = true;
            this.txtParaList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chInput
            // 
            this.chInput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.chInput.FillWeight = 5F;
            this.chInput.HeaderText = "Input";
            this.chInput.Name = "chInput";
            this.chInput.ReadOnly = true;
            this.chInput.Width = 41;
            // 
            // chOutput
            // 
            this.chOutput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.chOutput.FillWeight = 4.478957F;
            this.chOutput.HeaderText = "Output";
            this.chOutput.Name = "chOutput";
            this.chOutput.ReadOnly = true;
            this.chOutput.Width = 51;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            this.colValue.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(705, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSearchType
            // 
            this.cbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "All Function",
            "No-Header Function",
            "Class",
            "Enum",
            "Structure"});
            this.cbSearchType.Location = new System.Drawing.Point(194, 16);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(154, 23);
            this.cbSearchType.TabIndex = 8;
            // 
            // dtFunctions
            // 
            this.dtFunctions.AllowUserToAddRows = false;
            this.dtFunctions.AllowUserToDeleteRows = false;
            this.dtFunctions.AllowUserToResizeColumns = false;
            this.dtFunctions.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtFunctions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtFunctions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtFunctions.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dtFunctions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtFunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtFunctions.ColumnHeadersVisible = false;
            this.dtFunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkAdded,
            this.dataGridViewTextBoxColumn1});
            this.dtFunctions.EnableHeadersVisualStyles = false;
            this.dtFunctions.Location = new System.Drawing.Point(12, 47);
            this.dtFunctions.Name = "dtFunctions";
            this.dtFunctions.RowHeadersVisible = false;
            this.dtFunctions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtFunctions.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtFunctions.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtFunctions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtFunctions.Size = new System.Drawing.Size(336, 292);
            this.dtFunctions.TabIndex = 4;
            this.dtFunctions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtFunctions_KeyDown);
            this.dtFunctions.SelectionChanged += new System.EventHandler(this.dtFunctions_SelectionChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlyViewThisFunctionToolStripMenuItem,
            this.edToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(203, 70);
            // 
            // onlyViewThisFunctionToolStripMenuItem
            // 
            this.onlyViewThisFunctionToolStripMenuItem.Name = "onlyViewThisFunctionToolStripMenuItem";
            this.onlyViewThisFunctionToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.onlyViewThisFunctionToolStripMenuItem.Text = "Only View This Function";
            // 
            // edToolStripMenuItem
            // 
            this.edToolStripMenuItem.Name = "edToolStripMenuItem";
            this.edToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.edToolStripMenuItem.Text = "Edit Name";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // chkAdded
            // 
            this.chkAdded.FillWeight = 10F;
            this.chkAdded.HeaderText = "Added";
            this.chkAdded.Name = "chkAdded";
            this.chkAdded.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.FillWeight = 100.736F;
            this.dataGridViewTextBoxColumn1.HeaderText = "List of parameters";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ViewFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 368);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dtFunctions);
            this.Controls.Add(this.cbSearchType);
            this.Controls.Add(this.dtParamView);
            this.Controls.Add(this.btnSearch);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewFunction";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Function Viewer";
            this.Load += new System.EventHandler(this.ViewFunction_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewFunction_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dtParamView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFunctions)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dtParamView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.DataGridView dtFunctions;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtParaList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chInput;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem onlyViewThisFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkAdded;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}