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
    /// Interaction logic for NewStatusEffect.xaml
    /// </summary>
    public partial class NewStatusEffect : Window
    {
        public NewStatusEffect()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateStatusEffect();
        }
        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateStatusEffect();
            }
        }
        private void CreateStatusEffect()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (MainViewModel.MainViewModelStatic.StatusEffects.Where(a => a.Name == txtName.Text).Count() == 0)
                {
                    StatusEffect statusEffect = new StatusEffect { Name = txtName.Text };
                    MainViewModel.MainViewModelStatic.StatusEffects.Add(statusEffect);
                    statusEffect.Group = "Default";
                    MainViewModel.MainViewModelStatic.StatusEffectGroups.RefreshAll();
                    var wv = new WindowView
                    {
                        TabName = "Status Effect - " + statusEffect.Name,
                        Content = (UserControl)new StatusEffectEditor
                        {
                            DataContext = statusEffect
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
