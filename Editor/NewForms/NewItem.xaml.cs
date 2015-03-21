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
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewItem : Window
    {
        public NewItem()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateNewItem();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CreateNewItem()
        {
            var name = txtName.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (cmbParentClass.SelectedItem != null)
                {
                    if (MainViewModel.MainViewModelStatic.Items.Where(a => a.DefaultName == name).Count() == 0)
                    {
                        var item = new Item();
                        item.DefaultName = name;
                        item.ItemName = name;
                        item.ItemID = Guid.NewGuid();
                        item.ItemClassParent = cmbParentClass.SelectedItem as ItemClass;
                        MainViewModel.MainViewModelStatic.Items.Add(item);
                        var wv = new WindowView
                        {
                            TabName = "Item  - " + item.ItemName,
                            Content = (UserControl)new ItemEditor
                            {
                                DataContext = item
                            }
                        };
                        MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                        MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("The Item " + name + " already exists. Please choose another name.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a parent item class.");
                }
            }
            else
            {
                MessageBox.Show("Please choose a name for the item.");
            }
        }
        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateNewItem();
            }
        }
    }
}
