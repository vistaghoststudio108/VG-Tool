namespace Vistaghost.VISTAGHOST.ToolWindows
{
    partial class VistaghostWindowControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearchAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtFunctionOutput = new System.Windows.Forms.TextBox();
            this.txtHistoryOutput = new System.Windows.Forms.TextBox();
            this.txtErrorListOutput = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImage = global::Vistaghost.VISTAGHOST.Properties.Resources.bg;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.toolStripSeparator1,
            this.btnSearchAll,
            this.toolStripButton2,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(702, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(107, 22);
            this.toolStripLabel1.Text = "Show output from:";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.toolStripComboBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Functions",
            "History",
            "Error List"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(150, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearchAll
            // 
            this.btnSearchAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearchAll.Image = global::Vistaghost.VISTAGHOST.Properties.Resources.search;
            this.btnSearchAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearchAll.Name = "btnSearchAll";
            this.btnSearchAll.Size = new System.Drawing.Size(23, 22);
            this.btnSearchAll.Text = "toolStripButton1";
            this.btnSearchAll.Click += new System.EventHandler(this.btnSearchAll_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Vistaghost.VISTAGHOST.Properties.Resources.search2;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // txtFunctionOutput
            // 
            this.txtFunctionOutput.BackColor = System.Drawing.Color.White;
            this.txtFunctionOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFunctionOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFunctionOutput.Location = new System.Drawing.Point(0, 25);
            this.txtFunctionOutput.Margin = new System.Windows.Forms.Padding(0);
            this.txtFunctionOutput.Multiline = true;
            this.txtFunctionOutput.Name = "txtFunctionOutput";
            this.txtFunctionOutput.ReadOnly = true;
            this.txtFunctionOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFunctionOutput.Size = new System.Drawing.Size(702, 221);
            this.txtFunctionOutput.TabIndex = 2;
            // 
            // txtHistoryOutput
            // 
            this.txtHistoryOutput.BackColor = System.Drawing.Color.White;
            this.txtHistoryOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHistoryOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistoryOutput.Location = new System.Drawing.Point(0, 25);
            this.txtHistoryOutput.Margin = new System.Windows.Forms.Padding(0);
            this.txtHistoryOutput.Multiline = true;
            this.txtHistoryOutput.Name = "txtHistoryOutput";
            this.txtHistoryOutput.ReadOnly = true;
            this.txtHistoryOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHistoryOutput.Size = new System.Drawing.Size(702, 221);
            this.txtHistoryOutput.TabIndex = 3;
            // 
            // txtErrorListOutput
            // 
            this.txtErrorListOutput.BackColor = System.Drawing.Color.White;
            this.txtErrorListOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrorListOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrorListOutput.Location = new System.Drawing.Point(0, 25);
            this.txtErrorListOutput.Margin = new System.Windows.Forms.Padding(0);
            this.txtErrorListOutput.Multiline = true;
            this.txtErrorListOutput.Name = "txtErrorListOutput";
            this.txtErrorListOutput.ReadOnly = true;
            this.txtErrorListOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrorListOutput.Size = new System.Drawing.Size(702, 221);
            this.txtErrorListOutput.TabIndex = 4;
            // 
            // VistaghostWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtErrorListOutput);
            this.Controls.Add(this.txtHistoryOutput);
            this.Controls.Add(this.txtFunctionOutput);
            this.Controls.Add(this.toolStrip1);
            this.Name = "VistaghostWindowControl";
            this.Size = new System.Drawing.Size(702, 246);
            this.Load += new System.EventHandler(this.VistaghostWindowControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSearchAll;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtFunctionOutput;
        private System.Windows.Forms.TextBox txtHistoryOutput;
        private System.Windows.Forms.TextBox txtErrorListOutput;


    }
}
