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
    /// Interaction logic for NewInteractable.xaml
    /// </summary>
    public partial class NewInteractable : Window
    {
        public NewInteractable()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateInteractable();
        }

        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateInteractable();
            }
        }

        private void CreateInteractable()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (string.IsNullOrWhiteSpace(txtGroup.Text))
                {
                    MessageBox.Show("A group name must be specified.");
                }
                else
                {
                    if (MainViewModel.MainViewModelStatic.Interactables.Where(a => a.InteractableName == txtName.Text).Count() > 0)
                    {
                        MessageBox.Show("An interactable of this name already exists. Please choose a different name.");
                    }
                    else
                    {
                        Interactable i = (new Interactable
                        {
                            InteractableName = txtName.Text,
                            InteractableID = Guid.NewGuid(),
                        });
                        MainViewModel.MainViewModelStatic.Interactables.Add(i);
                        i.GroupName = txtGroup.Text;
                        i.DefaultDisplayName = i.InteractableName;
                        var wv = new WindowView
                        {
                            TabName = "Interactable - " + i.InteractableName,
                            Content = (UserControl)new InteractableEditor
                            {
                                DataContext = i
                            }
                        };
                        MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                        MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                        this.Close();
                    }
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
