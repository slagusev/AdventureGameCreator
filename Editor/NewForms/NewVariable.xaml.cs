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
    /// Interaction logic for NewVariable.xaml
    /// </summary>
    public partial class NewVariable : Window
    {
        public NewVariable()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateVariable();
        }
        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateVariable();
            }
        }
        private void CreateVariable()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (MainViewModel.MainViewModelStatic.Variables.Where(a => a.Name == txtName.Text).Count() == 0)
                {
                    Variable variable = new Variable { IsNumber = true, Name = txtName.Text, Id = Guid.NewGuid(), DefaultNumber = 0 };
                    MainViewModel.MainViewModelStatic.Variables.Add(variable);
                    var wv = new WindowView
                    {
                        TabName = "Variable - " + variable.Name,
                        Content = (UserControl)new VariableEditor
                        {
                            DataContext = variable
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
