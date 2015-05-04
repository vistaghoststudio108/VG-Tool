using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using System.Xml.Linq;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.User_Control;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    /// <summary>
    /// Interaction logic for VistaghostWindowControls.xaml
    /// </summary>
    public partial class VistaghostWindowControls : UserControl
    {
        BackgroundWorker bw;
        bool IsCanceled = false;
        SearchType searchType = SearchType.None;
        public VistaghostWindowControls Instance;
        private int numItem = 1;
        bool IsSearching = false;
        List<ObjectType> Results = new List<ObjectType>();
        List<FileContainer> FileList = new List<FileContainer>();
        int totalFileSearched = 0;

        public VistaghostWindowControls()
        {
            InitializeComponent();

            Combo_SearchType.SelectedIndex = 0;

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            this.Instance = this;
        }

        #region Custom methods

        public void AddString(string Text)
        {
            Dispatcher.Invoke(() =>
                {
                    SearchResultArea.AppendText(" " + numItem + ">" + Text + "\n");
                });

            numItem++;
        }

        public void Clear()
        {
            int sType = Combo_SearchType.SelectedIndex;
            switch (sType)
            {
                case 0:
                    SearchResultArea.Clear();
                    break;

                case 1:
                    WorkingHistoryArea.Clear();
                    break;

                case 2:
                    NotesArea.Clear();
                    break;

                default:
                    break;
            }

            numItem = 1;
        }

        #endregion

        private void TextViewArea_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Combo_SearchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            switch (combo.SelectedIndex)
            {
                case 0:// Changed items output
                    {
                        SearchResultArea.Visibility = System.Windows.Visibility.Visible;
                        WorkingHistoryArea.Visibility = System.Windows.Visibility.Hidden;
                        NotesArea.Visibility = System.Windows.Visibility.Hidden;

                        ChangedElementPanel.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;

                case 1:// Working history output
                    {
                        SearchResultArea.Visibility = System.Windows.Visibility.Hidden;
                        WorkingHistoryArea.Visibility = System.Windows.Visibility.Visible;
                        NotesArea.Visibility = System.Windows.Visibility.Hidden;

                        ChangedElementPanel.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    break;

                case 2:// Notes output
                    {
                        SearchResultArea.Visibility = System.Windows.Visibility.Hidden;
                        WorkingHistoryArea.Visibility = System.Windows.Visibility.Hidden;
                        NotesArea.Visibility = System.Windows.Visibility.Visible;

                        ChangedElementPanel.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    break;

                default:
                    break;
            }
        }

        private void Combo_BaseSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sourceType = Combo_BaseSource.SelectedIndex;
            switch (sourceType)
            {
                case 0: // from working history
                    {
                        //Combo_Keyword.IsEnabled = true;
                        //Combo_Keyword.Text = String.Empty;
                    }
                    break;

                case 1: // from result 1 or result 2
                case 2:
                    {
                        Combo_Keyword.Text = vgSetting.Instance.FindWhat;
                    }
                    break;

                default:
                    break;
            }
        }

        private void Combo_ElementType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            switch (combo.SelectedIndex)
            {
                case 0:// Function search
                    {
                        searchType = SearchType.AllFunction;
                    }
                    break;

                case 1:// Class search
                    {
                        searchType = SearchType.Class;
                    }
                    break;

                case 2:
                    {
                        searchType = SearchType.Enumerable;
                    }
                    break;

                case 3:
                    {
                        searchType = SearchType.Structure;
                    }
                    break;

                default:
                    break;
            }
        }

        string get_ElementType()
        {
            switch (searchType)
            {
                case SearchType.AllFunction:
                    return "functions";
                case SearchType.Class:
                    return "class";
                case SearchType.Enumerable:
                    return "enums";
                case SearchType.Structure:
                    return "structs";
                default:
                    return String.Empty;
            }
        }

        List<FileContainer> GetChangedItemsFromWorkHistory()
        {
            List<FileContainer> fList = new List<FileContainer>();
            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), vgSettingConstants.VGFolder,  vgSettingConstants.WorkHistoryFolder);
            var path = System.IO.Path.Combine(dir, vgSettingConstants.WorkHistoryFile);

            //if (!System.IO.File.Exists(path))
            //{
            //    using (var stream = System.IO.File.CreateText(path))
            //    {
            //        /*Create new log file based on exists file*/
            //        stream.Write(Properties.Resources.WorkHistory);
            //    }
            //}

            if (System.IO.File.Exists(path))
            {
                XDocument doc;
                doc = XDocument.Load(path, LoadOptions.SetBaseUri);
                foreach (var sNode in doc.Root.Elements())
                {
                    if (sNode.Attribute("id").Value == "1")
                    {
                        var fNode = sNode.Element("ChangedFile");
                        foreach (var fn in fNode.Elements())
                        {
                            if (fn.Attribute("action").Value == "mod" || fn.Attribute("action").Value == "add")
                            {
                                fList.Add(new FileContainer { FileName = fn.Value });
                            }
                        }

                        break;
                    }
                }
            }

            return fList;
        }

        bool CheckSource(out string message, out List<FileContainer> fileList)
        {
            bool bValid = true;
            message = String.Empty;
            fileList = null;

            switch (Combo_BaseSource.SelectedIndex)
            {
                case 0:
                    {
                        if (vgSetting.ProjectStatus.NotStarted)
                        {
                            message = "Project is not started";
                            bValid = false;
                        }
                        else
                        {
                            fileList = GetChangedItemsFromWorkHistory();
                            if (fileList.Count == 0)
                            {
                                bValid = false;
                                message = "There is no changed items in work history";
                            }
                        }
                    }
                    break;

                case 1:
                    {
                        fileList = vgSetting.Instance.FileList;
                        if (fileList.Count == 0)
                        {
                            bValid = false;
                            message = "Find Results 1 is empty";
                        }
                    }
                    break;

                case 2:
                    {
                        fileList = vgSetting.Instance.FileList;
                        if (fileList.Count == 0)
                        {
                            bValid = false;
                            message = "Find Results 2 is empty";
                        }
                    }
                    break;
            }

            return bValid;
        }

        private void BtnSearchElement_Click(object sender, RoutedEventArgs e)
        {
            //searching
            if (IsSearching)
                return;

            if(String.IsNullOrEmpty(Combo_Keyword.Text))
            {
                SearchResultArea.AppendText("Enter key word and try again.\n");
                return;
            }

            string message = String.Empty;

            if(!CheckSource(out message, out FileList))
            {
                SearchResultArea.AppendText(message + "\n");
                return;
            }

            IsCanceled = false;
            IsSearching = true;
            bw.RunWorkerAsync();

            this.Clear();
            SearchResultArea.AppendText("Find all \"" + get_ElementType() + "\", Source: \"" + Combo_BaseSource.Text + "\", Key word: \"" + Combo_Keyword.Text + "\"\n");
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {

            }
            else
            {
                // Finish searching
                IsSearching = false;
                SearchResultArea.AppendText("Results found: " + Results.Count + "    Total files searched: " + totalFileSearched);
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var _dte = Vistaghost.VISTAGHOST.VISTAGHOSTPackage.Current.DTE;
            try
            {
                Results = Vistaghost.VISTAGHOST.Lib.vgOperations.GetFunctionProtFromHistory(_dte, this.FileList, searchType, ref Instance, ref IsCanceled);

                if (IsCanceled)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                Logger.LogError(ex);
            }
        }

        private void BtnStopSearch_Click(object sender, RoutedEventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                IsCanceled = true;
                IsSearching = false;
                SearchResultArea.AppendText("Results found: " + Results.Count + "    Total files searched: " + totalFileSearched);
            }
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();
        }

        private void BtnCopyElement_Click(object sender, RoutedEventArgs e)
        {
            if (IsSearching)
                return;

            string copiedText = String.Empty;
            foreach (var item in Results)
            {
                switch (searchType)
                {
                    case SearchType.AllFunction:
                        {
                            copiedText += item.Prototype + "\n";
                        }
                        break;
                    case SearchType.Class:
                    case SearchType.Enumerable:
                    case SearchType.Structure:
                        {
                            copiedText += item.Name + "\n";
                        }
                        break;
                    default:
                        break;
                }
            }

            copiedText = copiedText.Remove(copiedText.Length - 1);

            Clipboard.SetText(copiedText, TextDataFormat.UnicodeText);
        }
    }
}
