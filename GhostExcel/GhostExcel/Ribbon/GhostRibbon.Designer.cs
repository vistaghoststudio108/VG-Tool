namespace GhostExcel
{
    partial class GhostRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public GhostRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnUpdate = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnSetting = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.btnFeedBack = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.mailThread = new System.ComponentModel.BackgroundWorker();
            this.searchThread = new System.ComponentModel.BackgroundWorker();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group3.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Label = "VISTAGHOST";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnUpdate);
            this.group1.Label = "Data";
            this.group1.Name = "group1";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Label = "Update";
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUpdate_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnSetting);
            this.group2.Label = "Configuration";
            this.group2.Name = "group2";
            // 
            // btnSetting
            // 
            this.btnSetting.Label = "Setting";
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSetting_Click);
            // 
            // group3
            // 
            this.group3.Items.Add(this.btnFeedBack);
            this.group3.Items.Add(this.button2);
            this.group3.Label = "Help";
            this.group3.Name = "group3";
            // 
            // btnFeedBack
            // 
            this.btnFeedBack.Label = "FeedBack";
            this.btnFeedBack.Name = "btnFeedBack";
            this.btnFeedBack.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFeedBack_Click);
            // 
            // button2
            // 
            this.button2.Label = "About";
            this.button2.Name = "button2";
            // 
            // searchThread
            // 
            this.searchThread.WorkerReportsProgress = true;
            // 
            // GhostRibbon
            // 
            this.Name = "GhostRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.GhostRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdate;
        private System.ComponentModel.BackgroundWorker mailThread;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSetting;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFeedBack;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        private System.ComponentModel.BackgroundWorker searchThread;
    }

    partial class ThisRibbonCollection
    {
        internal GhostRibbon GhostRibbon
        {
            get { return this.GetRibbon<GhostRibbon>(); }
        }
    }
}
