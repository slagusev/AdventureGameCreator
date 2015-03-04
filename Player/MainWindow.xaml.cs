using Microsoft.Win32;
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
using System.Xml.Linq;

namespace Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if (ofd.ShowDialog().Value)
            {
                
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                var sr = new StreamReader(fs);
                var xml = XElement.Parse(sr.ReadToEnd());
                sr.Close();

                var mvm = new MainViewModel();
                //player.DataContext = mvm;
                mvm.CurrentGame = Game.FromXml(xml);
                Editor.App.Current.Resources["MainViewModelStatic"] = mvm;

                var player = new MainPlayer();

                mvm.OutputCurrentRoomDescription();
                
                
                
                
                player.Show();
                this.Close();
            }
        }
    }
}
