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
using Vistaghost.VISTAGHOST.DataModel;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.VGUserControl;

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
            Combo_ElementType.SelectedIndex = 0;
            Combo_BaseSource.SelectedIndex = 1;

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
                    SearchResultArea.AppendText(" --> " + Text + "\n");
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
                        if (vgSetting.ProjectStatus.NotStarted)
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
                        fileList = vgOperations.GetFileFromResultWindow(VISTAGHOSTPackage.DTEHelper.DTE, EnvDTE.Constants.vsWindowKindFindResults1, FileFilter.ffSource);
                        if (fileList.Count == 0)
                        {
                            bValid = false;
                            message = "Find Results 1 is empty";
                        }
                        else
                        {
                            keyword = vgSetting.Instance.FindWhat;
                        }
                    }
                    break;

                case 2:
                    {
                        fileList = vgOperations.GetFileFromResultWindow(VISTAGHOSTPackage.DTEHelper.DTE, EnvDTE.Constants.vsWindowKindFindResults2, FileFilter.ffSource);
                        if (fileList.Count == 0)
                        {
                            bValid = false;
                            message = "Find Results 2 is empty";
                        }
                        else
                        {
                            keyword = vgSetting.Instance.FindWhat;
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

            IsCanceled = false;
            IsSearching = true;
            bw.RunWorkerAsync();
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

            // Finish searching
            IsSearching = false;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var _dte = Vistaghost.VISTAGHOST.VISTAGHOSTPackage.Current.DTE;
            try
            {
                Results = vgOperations.GetFunctionProtFromHistory(_dte, this.FileList, searchType, ref Instance, out totalFileSearched, ref IsCanceled);

                if (IsCanceled && totalFileSearched != this.FileList.Count)
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

            if (copiedText.Length != 0)
            {
                copiedText = copiedText.Remove(copiedText.Length - 1);
                Clipboard.SetText(copiedText, TextDataFormat.UnicodeText);
            }
        }

        private void SearchResultArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchResultArea.SelectionStart = SearchResultArea.Text.Length;
            SearchResultArea.ScrollToEnd();
        }

        private void WorkingHistoryArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            WorkingHistoryArea.SelectionStart = WorkingHistoryArea.Text.Length;
            WorkingHistoryArea.ScrollToEnd();
        }

        private void NotesArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotesArea.SelectionStart = NotesArea.Text.Length;
            NotesArea.ScrollToEnd();
        }
    }
}
