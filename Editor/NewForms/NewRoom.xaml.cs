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
    /// Interaction logic for NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Window
    {
        public NewRoom()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateRoom();
        }

        private void CreateRoom()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (cmbZones.SelectedItem == null)
                {
                    MessageBox.Show("A zone must be selected.");
                }
                else
                {
                    if (((Zone)cmbZones.SelectedItem).Rooms.Where(a => a.RoomName == txtName.Text).Count() > 0)
                    {
                        MessageBox.Show("A room of this name already exists in the selected zone. Please choose a different name.");
                    }
                    else
                    {
                        Room r = (new Room
                        {
                            RoomName = txtName.Text,
                            RoomID = Guid.NewGuid()
                        });
                        ((Zone)cmbZones.SelectedItem).Rooms.Add(r);
                        var wv = new WindowView
                        {
                            TabName = "Room - " + r.RoomName,
                            Content = (UserControl)new RoomEditor
                            {
                                DataContext = r
                            }
                        };
                        MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                        MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                        this.Close();
                    }
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
                CreateRoom();
            }
        }
        
    }
}
