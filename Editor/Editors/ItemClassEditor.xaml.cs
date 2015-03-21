using Editor.NewForms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor.Editors
{
    /// <summary>
    /// Interaction logic for ItemClassEditor.xaml
    /// </summary>
    public partial class ItemClassEditor : UserControl
    {
        public ItemClassEditor()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var form = new NewProperty();
            form.ShowDialog();
            if (!string.IsNullOrWhiteSpace(form.result))
            {
                var context = this.DataContext as ItemClass;
                if (context.ItemProperties.Where(a => a.Name == form.result).Count() == 0)
                {
                    context.ItemProperties.Add(new Variable { Name = form.result });
                    foreach (var a in context.GetAllChildItems())
                    {
                        a.PopulateProperties(); 
                    }
                }
                else
                {
                    MessageBox.Show("A property with that name already exists. Please choose another name.");
                    Button_Click_1(sender, e);
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as ItemClass;
            if (context.SelectedProperty != null)
            {
                foreach (var a in context.GetAllChildItems())
                {
                    var prop = a.ItemProperties.Where(b => b.Name == context.Name + ":" + context.SelectedProperty.Name).FirstOrDefault();
                    if (prop != null)
                        a.ItemProperties.Remove(prop);
                }
                context.ItemProperties.Remove(context.SelectedProperty);
                
                context.SelectedProperty = null;
                
            }
        }
    }
}
