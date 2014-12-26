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

namespace Editor.Editors
{
    /// <summary>
    /// Interaction logic for RoomEditor.xaml
    /// </summary>
    public partial class RoomEditor : UserControl
    {
        public RoomEditor()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            ((Editor.ObjectTypes.Room)this.DataContext).OpenSelectedExit();
        }



    }
}
