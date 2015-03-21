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
    /// Interaction logic for NewProperty.xaml
    /// </summary>
    public partial class NewProperty : Window
    {
        public NewProperty()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.result = txtName.Text;
            this.Close();
        }
        public string result = "";
        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                this.result = txtName.Text;
                this.Close();
            }
        }
    }
}
