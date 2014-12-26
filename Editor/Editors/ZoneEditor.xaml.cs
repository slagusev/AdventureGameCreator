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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor
{
    /// <summary>
    /// Interaction logic for ZoneEditor.xaml
    /// </summary>
    public partial class ZoneEditor : UserControl
    {
        public ZoneEditor()
        {
            InitializeComponent();
        }
        private void mainTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstRooms.SelectedItem == null) return;
            WindowView wv = null;
            if (lstRooms.SelectedItem.GetType() == typeof(Room))
            {
                Room r = lstRooms.SelectedItem as Room;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as RoomEditor
                                       where b != null && b.DataContext == r
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Room - " + r.RoomName,
                        Content = (UserControl)new RoomEditor
                        {
                            DataContext = r
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (wv != null)
                MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
        }
    }
}
