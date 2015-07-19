namespace GhostExcel.UserControls
{
    partial class GhostPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFilterFast = new System.Windows.Forms.TextBox();
            this.cbFilterType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objListViewFast = new BrightIdeasSoftware.FastObjectListView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objListViewFast)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFilterFast
            // 
            this.textBoxFilterFast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBoxFilterFast.Location = new System.Drawing.Point(9, 27);
            this.textBoxFilterFast.Name = "textBoxFilterFast";
            this.textBoxFilterFast.Size = new System.Drawing.Size(252, 20);
            this.textBoxFilterFast.TabIndex = 0;
            this.textBoxFilterFast.TextChanged += new System.EventHandler(this.textBoxFilterFast_TextChanged);
            // 
            // cbFilterType
            // 
            this.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbFilterType.FormattingEnabled = true;
            this.cbFilterType.Items.AddRange(new object[] {
            "Any text",
            "Prefix",
            "Regex"});
            this.cbFilterType.Location = new System.Drawing.Point(267, 26);
            this.cbFilterType.Name = "cbFilterType";
            this.cbFilterType.Size = new System.Drawing.Size(101, 21);
            this.cbFilterType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 447);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(378, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.olvColumn1.Text = "Function Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 100;
            // 
            // objListViewFast
            // 
            this.objListViewFast.AllColumns.Add(this.olvColumn1);
            this.objListViewFast.AllowColumnReorder = true;
            this.objListViewFast.AllowDrop = true;
            this.objListViewFast.AlternateRowBackColor = System.Drawing.Color.White;
            this.objListViewFast.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.objListViewFast.CellEditEnterChangesRows = true;
            this.objListViewFast.CellEditTabChangesRows = true;
            this.objListViewFast.CheckBoxes = true;
            this.objListViewFast.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.objListViewFast.Cursor = System.Windows.Forms.Cursors.Default;
            this.objListViewFast.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.objListViewFast.EmptyListMsg = "No results found";
            this.objListViewFast.FullRowSelect = true;
            this.objListViewFast.GridLines = true;
            this.objListViewFast.HideSelection = false;
            this.objListViewFast.Location = new System.Drawing.Point(0, 71);
            this.objListViewFast.Name = "objListViewFast";
            this.objListViewFast.OwnerDraw = true;
            this.objListViewFast.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.objListViewFast.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.objListViewFast.ShowCommandMenuOnRightClick = true;
            this.objListViewFast.ShowGroups = false;
            this.objListViewFast.ShowImagesOnSubItems = true;
            this.objListViewFast.ShowItemToolTips = true;
            this.objListViewFast.Size = new System.Drawing.Size(378, 376);
            this.objListViewFast.SpaceBetweenGroups = 20;
            this.objListViewFast.TabIndex = 2;
            this.objListViewFast.TintSortColumn = true;
            this.objListViewFast.TriStateCheckBoxes = true;
            this.objListViewFast.UseAlternatingBackColors = true;
            this.objListViewFast.UseCompatibleStateImageBehavior = false;
            this.objListViewFast.UseFilterIndicator = true;
            this.objListViewFast.UseFiltering = true;
            this.objListViewFast.UseHyperlinks = true;
            this.objListViewFast.View = System.Windows.Forms.View.Details;
            this.objListViewFast.VirtualMode = true;
            // 
            // GhostPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterType);
            this.Controls.Add(this.textBoxFilterFast);
            this.Controls.Add(this.objListViewFast);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "GhostPane";
            this.Size = new System.Drawing.Size(378, 469);
            this.Load += new System.EventHandler(this.GhostPane_Load);
            this.SizeChanged += new System.EventHandler(this.GhostPane_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objListViewFast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFilterFast;
        private System.Windows.Forms.ComboBox cbFilterType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.FastObjectListView objListViewFast;

    }
}
