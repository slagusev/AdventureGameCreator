using Editor.NewForms;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void newZone_Click(object sender, RoutedEventArgs e)
        {
            new NewZone().ShowDialog();
        }

        private void newRoom_Click(object sender, RoutedEventArgs e)
        {
            new NewRoom().ShowDialog();
        }

        private void menuSave_Click_1(object sender, RoutedEventArgs e)
        {
            var mvm = (MainViewModel)Editor.App.Current.Resources["MainViewModelStatic"];
            var xml = MainViewModel.MainViewModelStatic.ToXML();
            FileStream fs = new FileStream(mvm.Location, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(xml.ToString());
            sw.Close();
        }

        private void menuExit_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void newInteractable_Click(object sender, RoutedEventArgs e)
        {
            new NewInteractable().ShowDialog();
        }
    }
}
