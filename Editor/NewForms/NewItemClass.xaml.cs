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
    /// Interaction logic for NewItemClass.xaml
    /// </summary>
    public partial class NewItemClass : Window
    {
        public NewItemClass()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateNewItemClass();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CreateNewItemClass()
        {
            var name = txtName.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (cmbParentClass.SelectedItem != null)
                {
                    if (MainViewModel.MainViewModelStatic.ItemClasses.Where(a => a.Name == name).Count() == 0)
                    {
                        var itemClass = new ItemClass();
                        itemClass.Name = name;
                        itemClass.ParentClass = cmbParentClass.SelectedItem as ItemClass;
                        MainViewModel.MainViewModelStatic.ItemClasses.Add(itemClass);
                        var wv = new WindowView
                        {
                            TabName = "Item Class - " + itemClass.Name,
                            Content = (UserControl)new ItemClassEditor
                            {
                                DataContext = itemClass
                            }
                        };
                        MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                        MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("The Item Class " + name + " already exists. Please choose another name.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a parent item class.");
                }
            }
            else
            {
                MessageBox.Show("Please choose a name for the item class.");
            }
        }
        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateNewItemClass();
            }
        }
    }
}
