using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for VariableSelector.xaml
    /// </summary>
    public partial class ItemSelector : UserControl
    {
       
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(ItemRef), typeof(ItemSelector), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, SelectedItemChanged));


        public ItemRef SelectedItem
        {
            get
            {
                return (ItemRef)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        private static void PropertiesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as ItemSelector).RefreshListBox();
        }
        private static void SelectedItemChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && e.NewValue != null)
            {
                ItemSelector vsSource = source as ItemSelector;
                VarRef newRef = e.NewValue as VarRef;
                foreach (var a in vsSource.lstItems.Items)
                {
                    VarRef srcRef = a as VarRef;
                    if (newRef != null && srcRef != null && newRef.LinkedVarId == srcRef.LinkedVarId)
                    {
                        vsSource.lstItems.SelectedItem = srcRef;
                    }
                }
            }
                //(source as VariableSelector).lstItems.SelectedItem = (source as VariableSelector).lstItems.Items.where ((VarRef)a).LinkedVarId == ((VarRef)e.NewValue).LinkedVarId select a).FirstOrDefault();
        }
        public ItemSelector()
        {
            //ShowDateTime = true;
            //ShowString = true;
            //ShowNumber = true;
            InitializeComponent();
            RefreshListBox();
        }
        ObservableCollection<ItemRef> vars = new ObservableCollection<ItemRef>();
        private void searchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListBox();
        }

        public void RefreshListBox()
        {
            vars = new ObservableCollection<ItemRef>(MainViewModel.MainViewModelStatic.Items.Where(a => a.ItemName.ToLower().Contains(searchText.Text.ToLower())).Select(a => new ItemRef(a.ItemID)));


    
            this.lstItems.ItemsSource = vars;
            if (SelectedItem != null && lstItems.Items.Contains(SelectedItem))
            {
                lstItems.SelectedItem = SelectedItem;
            }
        }

        private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstItems.SelectedItem as ItemRef != null)
            {
                SelectedItem = lstItems.SelectedItem as ItemRef;
            }
        }
    }
}
