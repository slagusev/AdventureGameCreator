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
    /// Interaction logic for RunCommonEvent.xaml
    /// </summary>
    public partial class RunCommonEventEditor : Window
    {
        public RunCommonEventEditor()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ListBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (this.DataContext as Editor.Scripter.Flow.RunCommonEvent != null)
            {
                (this.DataContext as Editor.Scripter.Flow.RunCommonEvent).RefreshValidEvents();
            }
        }
    }
}
