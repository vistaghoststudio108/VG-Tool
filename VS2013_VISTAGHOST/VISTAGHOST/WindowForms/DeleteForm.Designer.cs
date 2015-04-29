namespace Vistaghost.VISTAGHOST
{
    partial class DeleteForm
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
            this.cbDeleteDoubleSlash = new System.Windows.Forms.CheckBox();
            this.cbDeleteAllBreakLines = new System.Windows.Forms.CheckBox();
            this.cbDeleteSlashStar = new System.Windows.Forms.CheckBox();
            this.cbSmartFormat = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDeleteDoubleSlash
            // 
            this.cbDeleteDoubleSlash.AutoSize = true;
            this.cbDeleteDoubleSlash.Location = new System.Drawing.Point(17, 10);
            this.cbDeleteDoubleSlash.Name = "cbDeleteDoubleSlash";
            this.cbDeleteDoubleSlash.Size = new System.Drawing.Size(155, 19);
            this.cbDeleteDoubleSlash.TabIndex = 5;
            this.cbDeleteDoubleSlash.Text = "Delete all (//) comments";
            this.cbDeleteDoubleSlash.UseVisualStyleBackColor = true;
            // 
            // cbDeleteAllBreakLines
            // 
            this.cbDeleteAllBreakLines.AutoSize = true;
            this.cbDeleteAllBreakLines.Location = new System.Drawing.Point(223, 10);
            this.cbDeleteAllBreakLines.Name = "cbDeleteAllBreakLines";
            this.cbDeleteAllBreakLines.Size = new System.Drawing.Size(133, 19);
            this.cbDeleteAllBreakLines.TabIndex = 6;
            this.cbDeleteAllBreakLines.Text = "Delete all break lines";
            this.cbDeleteAllBreakLines.UseVisualStyleBackColor = true;
            // 
            // cbDeleteSlashStar
            // 
            this.cbDeleteSlashStar.AutoSize = true;
            this.cbDeleteSlashStar.Location = new System.Drawing.Point(17, 35);
            this.cbDeleteSlashStar.Name = "cbDeleteSlashStar";
            this.cbDeleteSlashStar.Size = new System.Drawing.Size(165, 19);
            this.cbDeleteSlashStar.TabIndex = 5;
            this.cbDeleteSlashStar.Text = "Delete all (/**/) comments";
            this.cbDeleteSlashStar.UseVisualStyleBackColor = true;
            // 
            // cbSmartFormat
            // 
            this.cbSmartFormat.AutoSize = true;
            this.cbSmartFormat.Location = new System.Drawing.Point(223, 35);
            this.cbSmartFormat.Name = "cbSmartFormat";
            this.cbSmartFormat.Size = new System.Drawing.Size(96, 19);
            this.cbSmartFormat.TabIndex = 6;
            this.cbSmartFormat.Text = "Smart format";
            this.cbSmartFormat.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(150, 62);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 30);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // DeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 101);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbSmartFormat);
            this.Controls.Add(this.cbDeleteAllBreakLines);
            this.Controls.Add(this.cbDeleteSlashStar);
            this.Controls.Add(this.cbDeleteDoubleSlash);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DeleteForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeleteForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbDeleteDoubleSlash;
        private System.Windows.Forms.CheckBox cbDeleteAllBreakLines;
        private System.Windows.Forms.CheckBox cbDeleteSlashStar;
        private System.Windows.Forms.CheckBox cbSmartFormat;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnApply;
    }
}