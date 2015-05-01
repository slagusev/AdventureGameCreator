using Editor.ObjectTypes;
using Editor.Scripter.Misc;
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

namespace Editor.ScriptEditors
{
    /// <summary>
    /// Interaction logic for ChangeRoomEditor.xaml
    /// </summary>
    public partial class ChangeRoomEditor : Window
    {
        public ChangeRoomEditor()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

       

        private void TreeView_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = this.DataContext as ChangeRoom;
            if (e.NewValue as Room != null)
                context.SelectedRoom.Value = e.NewValue as Room;
            else
                context.SelectedRoom.Ref = Guid.Empty;
        }
    }
}
