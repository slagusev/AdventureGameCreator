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

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (MainViewModel.MainViewModelStatic.SelectedTab != -1)
            {
                var indexToRemove = (MainViewModel.MainViewModelStatic.SelectedTab);
                if (indexToRemove + 1 >= tabControl.Items.Count)
                {
                    MainViewModel.MainViewModelStatic.SelectedTab--;
                }

                MainViewModel.MainViewModelStatic.OpenWindows.RemoveAt(indexToRemove);
            }
            
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            foreach (var a in (((TabControl)sender).Items))
            {
                ((WindowView)a).CloseVisibility = Visibility.Collapsed;
            }
            if (tabControl.SelectedItem != null)
            {
                ((WindowView)(((TabControl)sender).SelectedItem)).CloseVisibility = Visibility.Visible;
                MainViewModel.MainViewModelStatic.SelectedTab = tabControl.SelectedIndex;
            }
            else
                MainViewModel.MainViewModelStatic.SelectedTab = -1;
        }
    }
}
