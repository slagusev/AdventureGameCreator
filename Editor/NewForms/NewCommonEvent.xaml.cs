using Editor.Editors;
using Editor.ObjectTypes;
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
using System.Windows.Shapes;

namespace Editor.NewForms
{
    /// <summary>
    /// Interaction logic for NewCommonEvent.xaml
    /// </summary>
    public partial class NewCommonEvent : Window
    {
        public NewCommonEvent()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateCommonEvent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateCommonEvent();
            }
        }

        private void CreateCommonEvent()
        {
            if (this.DataContext.GetType() == typeof(CommonEvent))
            {
                var dataContext = this.DataContext as CommonEvent;
                if (!string.IsNullOrWhiteSpace(dataContext.Name))
                {
                    if (dataContext.EventType != null)
                    {
                        dataContext.AssociatedScript = new Scripter.Script(dataContext.EventType.Item3);
                        MainViewModel.MainViewModelStatic.CommonEvents.Add(dataContext);
                        var wv = new WindowView
                        {
                            TabName = "Common Event - " + dataContext.Name,
                            Content = (UserControl)new CommonEventEditor
                            {
                                DataContext = dataContext
                            }
                        };
                        MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                        MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a type.");
                    }
                }
                else
                {
                    MessageBox.Show("Please give this common event a non-empty name.");
                }
            }
        }
    }
}
