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
    /// Interaction logic for NewArray.xaml
    /// </summary>
    public partial class NewArray : Window
    {
        public NewArray()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateArray();
        }
        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateArray();
            }
        }
        private void CreateArray()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (MainViewModel.MainViewModelStatic.Variables.Where(a => a.Name == txtName.Text).Count() == 0)
                {
                    VarArray array = new VarArray { IsNumber = true, Name = txtName.Text, Id = Guid.NewGuid() };
                    MainViewModel.MainViewModelStatic.Arrays.Add(array);
                    array.Group = "Default";
                    var wv = new WindowView
                    {
                        TabName = "Array - " + array.Name,
                        Content = (UserControl)new ArrayEditor
                        {
                            DataContext = array
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                    MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("A variable with this name already exists. Please choose another name.");
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
    }
}
