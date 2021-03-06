﻿using Microsoft.VisualStudio;
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
using Vistaghost.VISTAGHOST.Editor;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Lib;
using Vistaghost.VISTAGHOST.VGUserControl;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    /// <summary>
    /// Interaction logic for VistaghostWindowControls.xaml
    /// </summary>
    public partial class VistaghostWindowControls : UserControl, IDisposable
    {
        BackgroundWorker bw;
        bool IsCanceled = false;
        SearchType searchType = SearchType.None;
        public VistaghostWindowControls Instance;
        bool IsSearching = false;
        List<VGCodeElement> Results = new List<VGCodeElement>();
        List<FileContainer> FileList = new List<FileContainer>();
        int totalFileSearched = 0;
        int preLineNumber = 0;

        public VistaghostWindowControls()
        {
            InitializeComponent();

            Combo_SearchType.SelectedIndex = 0;
            Combo_ElementType.SelectedIndex = 0;
            Combo_BaseSource.SelectedIndex = 1;

            SearchResultArea.Document.Blocks.Clear();
            SearchResultArea.Document.PageWidth = 3000;

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            this.Instance = this;
        }

        #region Custom methods

        /// <summary>
        /// Add string to text view, line by line
        /// </summary>
        /// <param name="Text">input text</param>
        public void AddString(string Text)
        {
            Dispatcher.Invoke(() =>
                {
                    Paragraph pLine = new Paragraph();
                    pLine.Inlines.Add(Text);

                    Block emptyBlock = SearchResultArea.Document.Blocks.ElementAt(SearchResultArea.Document.Blocks.Count - 1);
                    SearchResultArea.Document.Blocks.InsertBefore(emptyBlock, pLine);

                    if (GetCurrentLineNumber() == SearchResultArea.Document.Blocks.Count - 1)
                    {
                        SearchResultArea.ScrollToEnd();
                    }

                });
        }

        public void Clear()
        {
            int sType = Combo_SearchType.SelectedIndex;
            switch (sType)
            {
                case 0:
                    SearchResultArea.Document.Blocks.Clear();
                    break;

                case 1:
                    WorkingHistoryArea.Document.Blocks.Clear();
                    break;

                case 2:
                    NotesArea.Clear();
                    break;

                default:
                    break;
            }
        }

        #endregion

        private void TextViewArea_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Combo_ElementType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            switch (combo.SelectedIndex)
            {
                case 0:// Function search
                    {
                        searchType = SearchType.Function;
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

        string get_ElementType()
        {
            switch (searchType)
            {
                case SearchType.Function: return "functions";
                case SearchType.Class: return "class";
                case SearchType.Enumerable: return "enums";
                case SearchType.Structure: return "structs";
                default: return String.Empty;
            }
        }

        bool CheckSource(out string message, out List<FileContainer> fileList, out string keyword)
        {
            bool bValid = true;
            message = String.Empty;
            fileList = new List<FileContainer>();
            keyword = String.Empty;

            switch (Combo_BaseSource.SelectedIndex)
            {
                case 0:
                    {
                        if (!vgSetting.ProjectStatus.Started)
                        {
                            message = "Project is not started";
                            bValid = false;
                        }
                        else
                        {
                            fileList = FileManager.Instance.SearchFileFromWorkHistory(out message);
                            if (fileList.Count == 0)
                            {
                                bValid = false;
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

        /// <summary>
        /// Check keyword
        /// </summary>
        /// <param name="keyword">input keyword</param>
        /// <returns>true - keyword valid, false - other else</returns>
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
                //AddString("Enter key word and try again");
                VISTAGHOSTPackage.Current.DTE.StatusBar.Text = "Enter key word and try again";
                return;
            }

            string message = String.Empty;
            this.FileList.Clear();

            if (!CheckSource(out message, out FileList, out _keyWord))
            {
                //AddString(message);
                VISTAGHOSTPackage.Current.DTE.StatusBar.Text = message;
                return;
            }

            SearchResultArea.Document.Blocks.Clear();
            SearchResultArea.Document.Blocks.Add(new Paragraph(new Run("")));
            AddString("Find all \"" + get_ElementType() + "\", Source: \"" + Combo_BaseSource.Text + "\", Key word: \"" + _keyWord + "\"");

            /*disable some buttons*/
            BtnSearchElement.IsEnabled = false;
            BtnCopyElement.IsEnabled = false;
            BtnStopSearch.IsEnabled = true;
            BtnClearAll.IsEnabled = false;

            searchType = (SearchType)Combo_ElementType.SelectedIndex;

            Results.Clear();
            totalFileSearched = 0;
            IsCanceled = false;
            IsSearching = true;
            bw.RunWorkerAsync();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                AddString(" Results found: " + Results.Count + "    Total files searched: " + totalFileSearched + "    Canceled");
            }
            else
            {
                AddString(" Results found: " + Results.Count + "    Total files searched: " + totalFileSearched);
            }

            /*Enable some buttons*/
            BtnSearchElement.IsEnabled = true;
            BtnCopyElement.IsEnabled = true;
            BtnClearAll.IsEnabled = true;
            BtnStopSearch.IsEnabled = false;

            // Finish searching
            IsSearching = false;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var _dte = Vistaghost.VISTAGHOST.VISTAGHOSTPackage.Current.DTE;

            try
            {
                foreach (var file in this.FileList)
                {
                    foreach (VGCodeElement ce in FileManager.Instance.SearchInFile(_dte, file.FileName, vgSetting.Instance.FindWhat, true))
                    {
                        AddString(" " + ce.Name);
                        Results.Add(ce);

                        if (IsCanceled)
                            break;
                    }

                    totalFileSearched++;
                    _dte.StatusBar.Text = "Searching " + file.FileName;

                    if (IsCanceled)
                        break;
                }

                if (IsCanceled && totalFileSearched != this.FileList.Count)
                    e.Cancel = true;

                //Save found files
                FileManager.Instance.SaveFoundFiles(this.Results);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                Logger.LogError(ex);
            }

            _dte.StatusBar.Text = "";
        }

        private void BtnStopSearch_Click(object sender, RoutedEventArgs e)
        {
            if (bw.IsBusy)
            {
                try
                {
                    bw.CancelAsync();
                    IsCanceled = true;
                    FileManager.Instance.SearchCanceled = true;
                    IsSearching = false;

                    BtnStopSearch.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();
        }

        private void BtnCopyElement_Click(object sender, RoutedEventArgs e)
        {
            if (IsSearching || this.Results.Count == 0)
                return;

            string copiedText = String.Empty;
            foreach (var ce in Results)
            {
                copiedText += ce.Name + "\n";
            }

            if (copiedText.Length != 0)
            {
                copiedText = copiedText.Remove(copiedText.Length - 1);
                try
                {
                    Clipboard.SetText(copiedText, TextDataFormat.UnicodeText);
                }
                catch
                {
                    VISTAGHOSTPackage.Current.DTE.StatusBar.Text = "Copy failed";
                }
            }
        }

        private void SearchResultArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            //SearchResultArea.SelectionStart = SearchResultArea.Text.Length;
            //SearchResultArea.ScrollToEnd();
        }

        private void WorkingHistoryArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            //WorkingHistoryArea.SelectionStart = WorkingHistoryArea.Text.Length;
            //WorkingHistoryArea.ScrollToEnd();
        }

        private void NotesArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            //NotesArea.SelectionStart = NotesArea.Text.Length;
            //NotesArea.ScrollToEnd();
        }

        /// <summary>
        /// Refresh text view, clear all it's properties
        /// </summary>
        void RefreshTextView()
        {
            if (SearchResultArea.Document == null)
                return;

            TextRange documentRange = new TextRange(SearchResultArea.Document.ContentStart, SearchResultArea.Document.ContentEnd);
            documentRange.ClearAllProperties();
        }

        /// <summary>
        /// Get current line number at pointer position
        /// </summary>
        /// <returns>Current line</returns>
        int GetCurrentLineNumber()
        {
            int lineMoved, currentLineNumber;
            SearchResultArea.CaretPosition.GetLineStartPosition(-int.MaxValue, out lineMoved);

            currentLineNumber = -lineMoved;

            return currentLineNumber;
        }

        private void SearchResultArea_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsSearching || SearchResultArea.Selection.Text.Length != 0)
            {
                return;
            }

            int currentLineNumber = GetCurrentLineNumber();

            if (currentLineNumber != preLineNumber && currentLineNumber != 0 &&
                currentLineNumber != SearchResultArea.Document.Blocks.Count - 1 &&
                currentLineNumber != SearchResultArea.Document.Blocks.Count - 2)
            {
                preLineNumber = currentLineNumber;

                try
                {
                    RefreshTextView();
                    TextRange curLineRange = new TextRange(SearchResultArea.CaretPosition.GetLineStartPosition(0), SearchResultArea.CaretPosition.GetLineStartPosition(1) ?? SearchResultArea.CaretPosition.DocumentEnd);
                    curLineRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Color.FromRgb(48, 129, 212)));

                    EditorManager.OpenDocument(Results[currentLineNumber - 1].File);
                    EditorManager.GoTo(Results[currentLineNumber - 1].File, Results[currentLineNumber - 1].BeginLine - 1);
                    this.Focus();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bw != null)
                    bw.Dispose();
            }
        }
    }
}
