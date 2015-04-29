using System;
using System.Collections.Generic;
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

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    /// <summary>
    /// Interaction logic for VistaghostWindowControls.xaml
    /// </summary>
    public partial class VistaghostWindowControls : UserControl
    {
        public Vistaghost.VISTAGHOST.Lib.ToolWindowPaneEventHandler OnClicked;
        public VistaghostWindowControls()
        {
            InitializeComponent();
        }

        #region Custom methods

        public void AddString(string stringToAdd)
        {
        }

        public void Clear()
        {
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(OnClicked != null)
            {
                OnClicked(1);
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
                case 0:// Function output
                    {
                        FunctionOutputArea.Visibility = System.Windows.Visibility.Visible;
                        HistoryOutputArea.Visibility = System.Windows.Visibility.Hidden;
                        ErrorListOutputArea.Visibility = System.Windows.Visibility.Hidden;
                    }
                    break;

                case 1:// History output
                    {
                        FunctionOutputArea.Visibility = System.Windows.Visibility.Hidden;
                        HistoryOutputArea.Visibility = System.Windows.Visibility.Visible;
                        ErrorListOutputArea.Visibility = System.Windows.Visibility.Hidden;
                    }
                    break;

                case 2:// Error List output
                    {
                        FunctionOutputArea.Visibility = System.Windows.Visibility.Hidden;
                        HistoryOutputArea.Visibility = System.Windows.Visibility.Hidden;
                        ErrorListOutputArea.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
