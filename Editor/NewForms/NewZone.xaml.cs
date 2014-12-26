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
    /// Interaction logic for NewZone.xaml
    /// </summary>
    public partial class NewZone : Window
    {
        public NewZone()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateZone();
        }

        private void CreateZone()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (MainViewModel.MainViewModelStatic.Zones.Where(a => a.ZoneName == txtName.Text).Count() > 0)
                {
                    MessageBox.Show("A zone of this name already exists. Please choose a different name.");
                }
                else
                {
                    Zone z = (new Zone
                    {
                        ZoneName = txtName.Text,
                        ZoneId = Guid.NewGuid()
                    });
                    MainViewModel.MainViewModelStatic.Zones.Add(z);
                    var wv = new WindowView
                    {
                        TabName = "Zone - " + z.ZoneName,
                        Content = (UserControl)new ZoneEditor
                        {
                            DataContext = z
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                    MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Name must contain text.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateZone();
            }
        }
    }
}
