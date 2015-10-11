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
            if (!string.IsNullOrEmpty((App.Current.Resources["FileName"] ?? "").ToString()))
            {
                //OpenGame(App.Current.Resources["FileName"].ToString());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if (!string.IsNullOrEmpty((App.Current.Resources["FileName"] ?? "").ToString()))
            {
                OpenGame(App.Current.Resources["FileName"].ToString());
            }
            else if (ofd.ShowDialog().Value)
            {

                OpenGame(ofd.FileName);
            }
        }
        private void OpenGame(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            var sr = new StreamReader(fs);
            var xml = XElement.Parse(sr.ReadToEnd());
            sr.Close();

            var mvm = new MainViewModel();
            mvm.Location = fileName;
            //player.DataContext = mvm;
            mvm.CurrentGame = Game.FromXml(xml);


            Editor.App.Current.Resources["MainViewModelStatic"] = mvm;

            foreach (var room in mvm.CurrentGame.Rooms)
            {
                if (room.Value.RoomBase.StartingRoom)
                    mvm.CurrentGame.CurrentRoom = room.Value;
            }
            if (mvm.CurrentGame.CurrentRoom == null)
            {
                MessageBox.Show("Error:\n\nNo starting room was found. Using the editor, please \nselect one room from this game to be the starting room.");
            }
            else
            {
                var player = new MainPlayer();

                mvm.OutputCurrentRoomDescription();




                player.Show();
            }
            this.Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Save Files (*.ags)|*.ags";
            if (ofd.ShowDialog().Value)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                var sr = new StreamReader(fs);
                var xml = XElement.Parse(StringCompressor.DecompressString(sr.ReadToEnd()));
                sr.Close();
                var mvm = MainViewModel.FromXML(xml,ofd.FileName);
                if (mvm.CurrentGame.CurrentRoom == null)
                {
                    System.Windows.MessageBox.Show("Error:\nThe player's starting room was not found. It may have been deleted");
                }
                else
                {
                    var player = new MainPlayer();

                    mvm.OutputCurrentRoomDescription();




                    player.Show();
                }
                this.Close();
            }
        }
    }
}
