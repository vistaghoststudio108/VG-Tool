using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using System.Collections.Generic;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using Vistaghost.VISTAGHOST.Lib;
using System.Windows.Forms;
using System.ComponentModel;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Packages;
using Vistaghost.VISTAGHOST.ToolWindows;

namespace Vistaghost.VISTAGHOST
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0.3", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVISTAGHOSTPkgString)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [Description("Vistaghost Tools Package")]
    [ProvideAutomationObject("Vistaghost")]
    [ProvideToolWindow(typeof(VistaghostWindowPane),
        Style = VsDockStyle.Linked,
        Orientation = ToolWindowOrientation.Left,
        Window = ToolWindowGuids80.Outputwindow
        )]
    public class VISTAGHOSTPackage : Package, IDisposable, IVsShellPropertyEvents
    {
        private DTE2 dte2;
        uint cookie;

        private VistaghostWindowPane vgwPane;
        private OutputWindowPane owP;
        private int recordNum = 1;

        private List<string> lines = new List<string>();
        private string[] notify = {
                              "Add -mod comments succeeded\n",
                              "Add -add comments succeeded\n",
                              "Add -del comments succeeded\n"
                          };

        OleComponent OleCom;

        public bool IsOpened { get; set; }

        public static VISTAGHOSTPackage Current { get; private set; }

        internal static vgSetting Setting = vgSetting.Instance;
        internal static DTEHelper DTEHelper { get; private set; }
        internal static WindowManager WindowManager { get; private set; }

        SingleForm sf = new SingleForm();
        Config cff = new Config();
        DeleteForm df = new DeleteForm();

        #region Properties
        public EnvDTE.DTE DTE
        {
            get
            {
                return DTEHelper.DTE;
            }
        }

        public DTE2 DTE2
        {
            get
            {
                return DTEHelper.DTE2;
            }
        }

        public VistaghostWindowPane VistaghostOutputWindow
        {
            get
            {
                return vgwPane;
            }

            set
            {
                vgwPane = value;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VISTAGHOSTPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
            Current = this;
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // set an eventlistener for shell property changes
            IVsShell shellService = GetService(typeof(SVsShell)) as IVsShell;

            if (shellService != null)
            {
                ErrorHandler.ThrowOnFailure(shellService.AdviseShellPropertyChanges(this, out cookie));
            }

            // initial datetime
            Setting.CurrentDate = vgOperations.GetDateString((DateFormat)vgSetting.SettingData.CommentInfo.DateFormat);

            sf.OnSendData = new AddCommentEventHandler(sf_OnSendData);
            cff.OnSendData = new ConfigEventHandler(cff_OnSendData);
            df.OnSendData = new DeleteEventHandler(df_OnSendData);
        }

        public int OnShellPropertyChange(int propid, object var)
        {
            // when zombie state changes to false, finish package initialization
            if ((int)__VSSPROPID.VSSPROPID_Zombie == propid)
            {
                if ((bool)var == false)
                {
                    // zombie state dependent code
                    DTEHelper = new DTEHelper(GetService(typeof(SDTE)) as DTE, (DTE2)GetService(typeof(DTE)));
                    dte2 = (DTE2)GetService(typeof(DTE));

                    WindowManager = new WindowManager();

                    RegisterOleComponent();

                    ShowToolBar();

                    // eventlistener no longer needed
                    IVsShell shellService = GetService(typeof(SVsShell)) as IVsShell;
                    if (shellService != null)
                        ErrorHandler.ThrowOnFailure(shellService.UnadviseShellPropertyChanges(this.cookie));

                    this.cookie = 0;
                }
            }
            return VSConstants.S_OK;
        }

        #endregion

        public void AddCommandBars()
        {
            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuAddID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidAddTag);
                MenuCommand menuAdd = new MenuCommand(AddTagCallback, menuAddID);
                mcs.AddCommand(menuAdd);

                CommandID menuModID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidModTag);
                MenuCommand menuMod = new MenuCommand(ModTagCallback, menuModID);
                mcs.AddCommand(menuMod);

                CommandID menuDelID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidDelTag);
                MenuCommand menuDel = new MenuCommand(DelTagCallback, menuDelID);
                mcs.AddCommand(menuDel);

                CommandID menuConfigID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidConfig);
                MenuCommand menuConfig = new MenuCommand(ConfigCallback, menuConfigID);
                mcs.AddCommand(menuConfig);

                CommandID menuDeleteID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidDelete);
                MenuCommand menuDelete = new MenuCommand(DeleteCallback, menuDeleteID);
                mcs.AddCommand(menuDelete);

                CommandID menuCountID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidCount);
                MenuCommand menuCount = new MenuCommand(CountCallback, menuCountID);
                mcs.AddCommand(menuCount);

                CommandID menuAboutID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidAbout);
                MenuCommand menuAbout = new MenuCommand(AboutCallback, menuAboutID);
                mcs.AddCommand(menuAbout);

                CommandID menuMakeHeaderID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidMakeHeader);
                MenuCommand menuHeader = new MenuCommand(MakeHeaderCallback, menuMakeHeaderID);
                mcs.AddCommand(menuHeader);

                CommandID menuExportSettingsID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidExportSettings);
                MenuCommand menuExportSettings = new MenuCommand(ExportSettingsCallback, menuExportSettingsID);
                mcs.AddCommand(menuExportSettings);

                CommandID menuExporHistoryID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidExportHistory);
                MenuCommand menuExportHistory = new MenuCommand(ExportHistoryCallback, menuExporHistoryID);
                mcs.AddCommand(menuExportHistory);

                CommandID menuChangeID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidChangeInfo);
                MenuCommand menuChangeInfo = new MenuCommand(ChangeInfoCallback, menuChangeID);
                mcs.AddCommand(menuChangeInfo);

                CommandID menuAddAllHeaderID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidCreateMultiHeader);
                MenuCommand menuAddAllHeader = new MenuCommand(AddAllHeaderCallback, menuAddAllHeaderID);
                mcs.AddCommand(menuAddAllHeader);

                CommandID menuCopyPrototypeID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidCopyPrototype);
                MenuCommand menuCopyPrototype = new MenuCommand(CopyPrototypeCallback, menuCopyPrototypeID);
                mcs.AddCommand(menuCopyPrototype);

                //CommandID menuExportFuncID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidExportFunc);
                //MenuCommand menuExportFunc = new MenuCommand(ExportFuncCallback, menuExportFuncID);
                //mcs.AddCommand(menuExportFunc);

                CommandID menuToolWindowID = new CommandID(GuidList.guidVISTAGHOSTCmdSet, (int)PkgCmdIDList.cmdidMyToolWindow);
                MenuCommand menuToolWindow = new MenuCommand(ShowToolWindowCallback, menuToolWindowID);
                mcs.AddCommand(menuToolWindow);

                OutputWindow ow = dte2.ToolWindows.OutputWindow;
                owP = ow.OutputWindowPanes.Add("Vistaghost");
                //owP = ow.OutputWindowPanes.Add("Vistaghost-Notifications");

                owP.OutputString(Setting.Title);

                VistaghostOutputWindow = (VistaghostWindowPane)this.FindToolWindow(typeof(VistaghostWindowPane), 0, true);
            }
        }

        private void RegisterOleComponent()
        {
            OleCom = new OleComponent();

            var ocm = this.GetService(typeof(SOleComponentManager)) as IOleComponentManager;

            if (ocm != null)
            {
                uint pwdID;
                OLECRINFO[] crinfo = new OLECRINFO[1];
                crinfo[0].cbSize = (uint)Marshal.SizeOf(typeof(OLECRINFO));

                crinfo[0].grfcrf = (uint)_OLECRF.olecrfNeedAllActiveNotifs;
                ocm.FRegisterComponent(OleCom, crinfo, out pwdID);
            }
        }

        public void ShowToolBar(bool show = false)
        {
            var cmdBars = (CommandBars)DTEHelper.DTE.CommandBars;
            var menu = cmdBars.ActiveMenuBar;

            menu.Controls["Vistaghost"].Visible = show;

            var lua = menu.Controls["Vistaghost"] as CommandBarPopup;
            lua.Controls["ShotKeys"].Visible = false;

            //if (show)
            //    WindowManager.ShowToolWindow();
        }

        public void EnableButton(int cmdidButton, bool enabled)
        {
            var svc = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            CommandID idBtn = new CommandID(GuidList.guidVISTAGHOSTCmdSet, cmdidButton);
            var cmd = svc.FindCommand(idBtn) as MenuCommand;

            if (cmd != null)
                cmd.Enabled = enabled;
        }

        #region Callback methods

        //private void ExportFuncCallback(object sender, EventArgs e)
        //{ }

        private void ShowToolWindowCallback(object sender, EventArgs e)
        {
            WindowManager.ShowToolWindow();
        }

        private void CopyPrototypeCallback(object sender, EventArgs e)
        {
            string prototype = vgOperations.GetFuncPrototype(DTEHelper.DTE2, PrototypeType.FullProt);

            if (!String.IsNullOrEmpty(prototype))
                Clipboard.SetText(prototype, TextDataFormat.UnicodeText);
        }

        private void AddAllHeaderCallback(object sender, EventArgs e)
        {
            ViewFunction vff = new ViewFunction(DTEHelper.DTE, DTEHelper.DTE2);
            vff.ShowDialog();
        }

        private void ChangeInfoCallback(object sender, EventArgs e)
        {
            sf.LoadData(vgSetting.SettingData, ActionType.CHANGE_INFO);
            sf.ShowDialog();
        }

        private void ExportSettingsCallback(object sender, EventArgs e)
        {
            ExportSettingsForm esf = new ExportSettingsForm();
            esf.OnExportResult += new ExportEventHandler(esf_OnExportResult);
            esf.ShowDialog();
        }

        private void ExportHistoryCallback(object sender, EventArgs e)
        {
            ExportHistoryForm ef = new ExportHistoryForm();
            ef.ShowDialog();
        }

        private void MakeHeaderCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            SingleHeader ihf = new SingleHeader(DTEHelper.DTE2);
            ihf.OnSendData += new AddHeaderEventHandler(ihf_OnSendData);
            ihf.ShowDialog();

        }

        private void AddTagCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            if (DTEHelper.DTE.ActiveDocument == null || DTEHelper.DTE.ActiveDocument.Selection == null)
                return;

            if (vgSetting.SettingData.CommentInfo.AutoShowInputDialog)
            {
                sf_OnSendData(vgSetting.SettingData.CommentInfo.Content,
                                vgSetting.SettingData.CommentInfo.Account,
                                vgSetting.SettingData.CommentInfo.DevID,
                                String.Empty,
                                String.Empty,
                                false,
                                ActionType.ADD,
                                false,
                                false);
                return;
            }
            sf.LoadData(vgSetting.SettingData, ActionType.ADD);
            sf.ShowDialog();
        }

        private void DelTagCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            if (DTEHelper.DTE.ActiveDocument == null || DTEHelper.DTE.ActiveDocument.Selection == null)
                return;

            if (vgSetting.SettingData.CommentInfo.AutoShowInputDialog)
            {
                sf_OnSendData(vgSetting.SettingData.CommentInfo.Content,
                                vgSetting.SettingData.CommentInfo.Account,
                                vgSetting.SettingData.CommentInfo.DevID,
                                String.Empty,
                                String.Empty,
                                false,
                                ActionType.DELETE,
                                false,
                                false);
                return;
            }
            sf.LoadData(vgSetting.SettingData, ActionType.DELETE);
            sf.ShowDialog();
        }

        protected override int QueryClose(out bool canClose)
        {
            canClose = true;
            return base.QueryClose(out canClose);
        }

        private void ModTagCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            if (DTEHelper.DTE.ActiveDocument == null || DTEHelper.DTE.ActiveDocument.Selection == null)
                return;

            if (vgSetting.SettingData.CommentInfo.AutoShowInputDialog)
            {
                sf_OnSendData(vgSetting.SettingData.CommentInfo.Content,
                                vgSetting.SettingData.CommentInfo.Account,
                                vgSetting.SettingData.CommentInfo.DevID,
                                String.Empty,
                                String.Empty,
                                false,
                                ActionType.MODIFY,
                                false,
                                false);
                return;
            }

            sf.LoadData(vgSetting.SettingData, ActionType.MODIFY);
            sf.ShowDialog();

        }

        private void ConfigCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            cff.LoadConfig(DTEHelper.DTE2, vgSetting.SettingData);
            cff.ShowDialog();
        }

        private void DeleteCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            df.ShowDialog();
        }

        private void CountCallback(object sender, EventArgs e)
        {
            if (!vgSetting.RegisterData.Registered)
            {
                RegisterForm rsf = new RegisterForm();
                rsf.ShowDialog();
                return;
            }

            if (DTEHelper.DTE.ActiveDocument == null || DTEHelper.DTE.ActiveDocument.Selection == null)
                return;

            string line = String.Empty;

            var selected = DTEHelper.DTE.ActiveDocument.Selection as TextSelection;

            if (!selected.IsEmpty)
            {
                var num = vgOperations.ProcessLinesForCount(selected.Text);
                MessageBox.Show("Number of LOC : " + num.ToString(),
                                "Count result",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void AboutCallback(object sender, EventArgs e)
        {
            AboutVistaghostForm abf = new AboutVistaghostForm();
            abf.GetLicense(vgSetting.RegisterData);
            abf.ShowDialog();
        }

        #endregion

        private void hvf_OnOpenFile(string file)
        {
            try
            {
                DTE.ItemOperations.OpenFile(file, EnvDTE.Constants.vsViewKindCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open this file!", "Open Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogError(ex);
            }
        }

        private void sf_OnSendData(string content,          /*content of comments*/
                            string account,         /*user account*/
                            string devid,           /*development ID*/
                            string find,            /*find string*/
                            string replace,         /*replace string*/
                            bool moreopt,           /*indicate that more options is expanded*/
                            ActionType mode,        /*type of action (modify, add, delete)*/
                            bool keep_comments,     /*keep old comments*/
                            bool contentchanged)
        {
            if (contentchanged)
            {
                vgSetting.SettingData.CommentInfo.Content = content;
                vgSetting.SettingData.CommentInfo.Account = account;
                vgSetting.SettingData.CommentInfo.DevID = devid;
                vgSetting.SettingData.CommentInfo.KeepComment = keep_comments;
            }

            if (mode == ActionType.CHANGE_INFO)
            {
                vgSetting.SaveSettings();
                return;
            }

            int numPh = -1;
            ActionInfo info = new ActionInfo();

            owP.Activate();

            if (vgOperations.ProcessTextForAddSingle(DTEHelper.DTE, Setting.CurrentDate, content, account, devid, mode, keep_comments, ref info))
            {
                if (vgSetting.SettingData.HistoryInfo.DisplayHistory)
                {
                    // Add a line of text to the new pane.
                    owP.OutputString(recordNum.ToString() + ">" + info.Path + "(" + info.Line + ")\n");
                    owP.OutputString(recordNum.ToString() + ">   [" + account + "] -> " + notify[(int)mode - 1]);
                    if (moreopt && (int)mode == 1)
                    {
                        numPh = vgOperations.Replace(DTEHelper.DTE, find, replace);
                        if (numPh != -1)
                        {
                            owP.OutputString(recordNum.ToString() + ">   [" + account + "] -> Replace '" + find + "'" + " to '" + replace + "' succeeded (" + numPh.ToString() + " matched)\n");
                        }
                        else
                            owP.OutputString(recordNum.ToString() + ">   [" + account + "] -> No matched phrases\n");
                    }

                    recordNum++;
                }

                /*Write log of history*/
                if (vgSetting.SettingData.HistoryInfo.WriteLogHistory)
                {
                    /*Get function*/
                    string prototype = vgOperations.GetFuncPrototype(DTEHelper.DTE2, PrototypeType.FuncName);

                    LogFileType type = LogFileType.Xml;

                    if (vgSetting.SettingData.HistoryInfo.LogExtension == ".txt")
                        type = LogFileType.TextFile;

                    Logger.LogHistory(type, info.Path, info.Line, mode, find, replace, numPh, prototype);
                }

                DTEHelper.DTE.StatusBar.Text = Properties.Resources.AddCommentSuccess;
            }
            else
                DTEHelper.DTE.StatusBar.Text = Properties.Resources.NoText;
        }

        private void cff_OnSendData(Settings data)
        {
            vgSetting.SettingData = data;
            vgSetting.SaveSettings();
            Setting.CurrentDate = vgOperations.GetDateString((DateFormat)data.CommentInfo.DateFormat);
        }

        private void ihf_OnSendData(ObjectType func)
        {
            int offsetLine = 0;
            if (vgOperations.ProcessStringForAddHeader(DTEHelper.DTE, func, out offsetLine))
            {
                DTEHelper.DTE.StatusBar.Text = Properties.Resources.MakeHeaderSuccess;
            }
            else
                DTEHelper.DTE.StatusBar.Text = Properties.Resources.MakeHeaderFaield;
        }

        private void df_OnSendData(bool delDS, bool delSS, bool delBL, bool smartFM)
        {
            vgDelCommentsType dType = vgDelCommentsType.vgNone;
            vgDelCommentsOptions dOpt = vgDelCommentsOptions.vgNone;

            if (delDS && !delSS)        /*only delete double-slash comments*/
                dType = vgDelCommentsType.vgDeleteDoubleSlash;
            else if (delSS && !delDS)   /*only delete slash-star comments*/
                dType = vgDelCommentsType.vgDeleteSlashStar;
            else if (delSS && delDS)
                dType = vgDelCommentsType.vgDeleteBoth;     /*delete both of them*/

            if (delBL)
                dOpt |= vgDelCommentsOptions.vgDeleteAllBreakLine;

            if (smartFM)
                dOpt |= vgDelCommentsOptions.vgSmartFormat;


            if (vgOperations.ClearCommentWithSelectedText(DTEHelper.DTE, dType, dOpt))
            {
                DTEHelper.DTE.StatusBar.Text = Properties.Resources.DeleteCommentSuccess;
                return;
            }
            DTEHelper.DTE.StatusBar.Text = Properties.Resources.NoText;
        }

        private void esf_OnExportResult(string message, bool success)
        {
            if (!success)
            {
                owP.Activate();
                owP.OutputString(message + "\n");
                DTEHelper.DTE.StatusBar.Text = Properties.Resources.ExportFailedMessage;
            }
            else
                DTEHelper.DTE.StatusBar.Text = message;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sf != null)
                    sf.Dispose();

                if (cff != null)
                    cff.Dispose();

                if (df != null)
                    df.Dispose();
            }
        }
    }
}
