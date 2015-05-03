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

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    /// <summary>
    /// Interaction logic for UCVistaghostWindow.xaml
    /// </summary>
    public partial class UCVistaghostWindow : UserControl
    {
        BackgroundWorker bw;
        bool IsCanceled = false;

        public UCVistaghostWindow()
        {
            InitializeComponent();

            Combo_SearchType.SelectedIndex = 0;

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
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

            }
            else
            {
                //enable some controls
                BtnSearchElement.IsEnabled = true;
                Combo_SearchType.IsEnabled = true;
                Combo_ElementType.IsEnabled = true;
                Combo_BaseSource.IsEnabled = true;
                BtnCopyElement.IsEnabled = true;
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var _dte = Vistaghost.VISTAGHOST.VISTAGHOSTPackage.Current.DTE;
            var owP = Vistaghost.VISTAGHOST.VISTAGHOSTPackage.Current.VistaghostOutputWindow;
            try
            {
                Vistaghost.VISTAGHOST.Lib.VGOperations.GetFunctionProtFromHistory(_dte, ref owP, ref IsCanceled);

                if (IsCanceled)
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

        }

        private void Combo_BaseSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnSearchElement_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            // disable some controls
            Combo_SearchType.IsEnabled = false;
            Combo_ElementType.IsEnabled = false;
            Combo_BaseSource.IsEnabled = false;
            BtnCopyElement.IsEnabled = false;

            IsCanceled = false;
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
