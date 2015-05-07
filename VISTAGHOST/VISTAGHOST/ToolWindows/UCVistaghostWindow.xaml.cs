using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.DataModel;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    /// <summary>
    /// Interaction logic for UCVistaghostWindow.xaml
    /// </summary>
    public partial class UCVistaghostWindow : UserControl
    {
        BackgroundWorker bw;
        SearchType searchType = SearchType.None;
        public UCVistaghostWindow Instance;
        bool IsCanceled = false;
        private int numItem = 1;
        bool IsSearching = false;
        List<ObjectType> Results = new List<ObjectType>();
        List<FileContainer> FileList = new List<FileContainer>();
        int totalFileSearched = 0;

        public UCVistaghostWindow()
        {
            InitializeComponent();

            Combo_SearchType.SelectedIndex = 0;
            Combo_ElementType.SelectedIndex = 0;
            Combo_BaseSource.SelectedIndex = 1;

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            this.Instance = this;
        }

        public void AddString(string Text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SearchResultArea.AppendText(Text + "\n");
            }), null);
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
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                SearchResultArea.AppendText("Results found: " + Results.Count + "    Total files searched: " + totalFileSearched + "    Canceled\n");
            }
            else
            {
                SearchResultArea.AppendText(" Results found: " + Results.Count + "    Total files searched: " + totalFileSearched + "\n");
            }

            /*Enable some buttons*/
            BtnSearchElement.IsEnabled = true;
            BtnCopyElement.IsEnabled = true;
            BtnStopSearch.IsEnabled = false;

            // Finish searching
            IsSearching = false;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var _dte = Vistaghost.VISTAGHOST.VISTAGHOSTPackage.Current.DTE;
            try
            {
                Results = VGOperations.GetFunctionProtFromHistory(_dte, this.FileList, searchType, ref Instance, out totalFileSearched, ref IsCanceled);

                if (IsCanceled && totalFileSearched != this.FileList.Count)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                Logger.LogError(ex, false);
            }
        }

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

        private void Combo_BaseSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sourceType = Combo_BaseSource.SelectedIndex;
            switch (sourceType)
            {
                case 0: // from working history
                    {
                        Combo_Keyword.IsEnabled = true;
                    }
                    break;

                case 1: // from result 1 or result 2
                case 2:
                    {
                        Combo_Keyword.IsEnabled = false;
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

        bool CheckSource(out string message, out List<FileContainer> fileList, out string keyword)
        {
            bool bValid = true;
            message = String.Empty;
            fileList = null;
            keyword = String.Empty;

            switch (Combo_BaseSource.SelectedIndex)
            {
                case 0:
                    {
                        if (VGSetting.ProjectStatus.NotStarted)
                        {
                            message = "Project is not started";
                            bValid = false;
                        }
                        else
                        {
                            fileList = FileManager.Instance.SearchFileFromWorkHistory();
                            if (fileList.Count == 0)
                            {
                                bValid = false;
                                message = "There is no changed items in work history";
                            }
                            else
                            {
                                keyword = Combo_Keyword.Text;
                            }
                        }
                    }
                    break;

                case 1:
                    {
                        fileList = VGOperations.GetFileFromResultWindow(VISTAGHOSTPackage.DteHelper.DTE, EnvDTE.Constants.vsWindowKindFindResults1, FileFilter.ffSource);
                        if (fileList.Count == 0)
                        {
                            bValid = false;
                            message = "Find Results 1 is empty";
                        }
                        else
                        {
                            keyword = VGSetting.Instance.FindWhat;
                        }
                    }
                    break;

                case 2:
                    {
                        fileList = VGOperations.GetFileFromResultWindow(VISTAGHOSTPackage.DteHelper.DTE, EnvDTE.Constants.vsWindowKindFindResults2, FileFilter.ffSource);
                        if (fileList.Count == 0)
                        {
                            bValid = false;
                            message = "Find Results 2 is empty";
                        }
                        else
                        {
                            keyword = VGSetting.Instance.FindWhat;
                        }
                    }
                    break;
            }

            return bValid;
        }

        bool IsKeyWordValid(string keyword)
        {
            bool bValid = true;

            switch (Combo_BaseSource.SelectedIndex)
            {
                case 0:
                    {
                        if (String.IsNullOrEmpty(keyword))
                            bValid = false;
                    }
                    break;

                default:
                    break;
            }

            return bValid;
        }

        private void BtnSearchElement_Click(object sender, RoutedEventArgs e)
        {
            //searching
            if (IsSearching)
                return;

            string _keyWord = String.Empty;

            if (!IsKeyWordValid(Combo_Keyword.Text))
            {
                SearchResultArea.AppendText("Enter key word and try again.\n");
                return;
            }

            string message = String.Empty;

            if (!CheckSource(out message, out FileList, out _keyWord))
            {
                SearchResultArea.AppendText(message + "\n");
                return;
            }

            SearchResultArea.Clear();
            SearchResultArea.AppendText("Find all \"" + get_ElementType() + "\", Source: \"" + Combo_BaseSource.Text + "\", Key word: \"" + _keyWord + "\"\n");

            /*disable some buttons*/
            BtnSearchElement.IsEnabled = false;
            BtnCopyElement.IsEnabled = false;
            BtnStopSearch.IsEnabled = true;

            IsCanceled = false;
            IsSearching = true;
            bw.RunWorkerAsync();
        }

        private void BtnStopSearch_Click(object sender, RoutedEventArgs e)
        {
            bw.CancelAsync();
            IsCanceled = true;

            //enable some controls
            BtnSearchElement.IsEnabled = true;
            Combo_SearchType.IsEnabled = true;
            Combo_ElementType.IsEnabled = true;
            Combo_BaseSource.IsEnabled = true;
            BtnCopyElement.IsEnabled = true;
        }
    }
}
