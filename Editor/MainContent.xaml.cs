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

        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                var sp = (StackPanel)sender;
                var textblock = sp.Children.OfType<TextBlock>().FirstOrDefault();
                if (textblock != null)
                {
                    WindowView currentSelectedTab = null;
                    int oldIndex = -1;
                    if (MainViewModel.MainViewModelStatic.SelectedTab >= 0)
                    {
                        currentSelectedTab = MainViewModel.MainViewModelStatic.OpenWindows[MainViewModel.MainViewModelStatic.SelectedTab];
                        oldIndex = MainViewModel.MainViewModelStatic.SelectedTab;
                    }
                    MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(MainViewModel.MainViewModelStatic.OpenWindows.Where(a => a.TabName == textblock.Text).FirstOrDefault());
                    Image_MouseLeftButtonUp_1(sender, e);
                    MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(currentSelectedTab);
                    if (MainViewModel.MainViewModelStatic.SelectedTab == -1)
                    {
                        if (MainViewModel.MainViewModelStatic.OpenWindows.Count() > 0)
                        {
                            MainViewModel.MainViewModelStatic.SelectedTab = oldIndex-1;
                            if (MainViewModel.MainViewModelStatic.SelectedTab == -1)
                                MainViewModel.MainViewModelStatic.SelectedTab = 0;
                        }
                    }
                }
            }
        }
    }
}
