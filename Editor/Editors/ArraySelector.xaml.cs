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
    /// Interaction logic for ArraySelector.xaml
    /// </summary>
    public partial class ArraySelector : UserControl
    {
        public ArraySelector()
        {
            InitializeComponent();
            RefreshListBox();
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(GenericRef<VarArray>), typeof(ArraySelector), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, SelectedItemChanged));

        public GenericRef<VarArray> SelectedItem
        {
            get
            {
                return (GenericRef<VarArray>)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
                if (value != null && value.Value != null) currentlySelected.Text = value.Value.Name;
            }
        }
        private static void PropertiesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as ArraySelector).RefreshListBox();
        }
        private static void SelectedItemChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && e.NewValue != null)
            {
                ArraySelector vsSource = source as ArraySelector;
                GenericRef<VarArray> newRef = e.NewValue as GenericRef<VarArray>;
                foreach (var a in vsSource.treeItems.Items)
                {
                    GenericRef<ArraySelector> srcRef = a as GenericRef<ArraySelector>;
                    if (newRef != null && srcRef != null && newRef.Ref == srcRef.Ref)
                    {
                        //vsSource.treeItems.SelectedItem = srcRef;
                    }
                }
            }
            //(source as VariableSelector).lstItems.SelectedItem = (source as VariableSelector).lstItems.Items.where ((GenericRef<VarArray>)a).LinkedVarId == ((GenericRef<VarArray>)e.NewValue).LinkedVarId select a).FirstOrDefault();
        }
        ObservableCollection<Tuple<string, ObservableCollection<GenericRef<VarArray>>>> vars = new ObservableCollection<Tuple<string, ObservableCollection<GenericRef<VarArray>>>>();
        private void searchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListBox();
        }
        private void ExpandAll(ItemsControl items, bool expand)
        {
            foreach (object obj in items.Items)
            {
                ItemsControl childControl = items.ItemContainerGenerator.ContainerFromItem(obj) as ItemsControl;
                if (childControl != null)
                {
                    ExpandAll(childControl, expand);
                }
                TreeViewItem item = childControl as TreeViewItem;
                if (item != null)
                    item.IsExpanded = true;
            }
        }
        public void RefreshListBox()
        {
            vars = new ObservableCollection<Tuple<string, ObservableCollection<GenericRef<VarArray>>>>();
            foreach (var a in MainViewModel.MainViewModelStatic.ArrayGroups.Groups)
            {
                bool included = false;
                ObservableCollection<GenericRef<VarArray>> Arrays = new ObservableCollection<GenericRef<VarArray>>();
                foreach (var b in a.Item2)
                {
                    if (b.Name.ToLower().Contains(searchText.Text.ToLower()))
                    {
                        
                        included = true;
                        var arrayRef = GenericRef<VarArray>.GetArrayRef();
                        arrayRef.Ref = b.Id;
                        Arrays.Add(arrayRef);
                    }
                }
                if (included)
                {
                    vars.Add(Tuple.Create<string, ObservableCollection<GenericRef<VarArray>>>(a.Item1, Arrays));
                }
            }
            this.treeItems.ItemsSource = vars;
            //vars[0].Item2[0].LinkedCommonEvent.Name
            ExpandAll(treeItems, true);
            //var DateTimeVars = vars.Where(a => a.LinkedVariable.IsDateTime);
            //var StringVars = vars.Where(a => a.LinkedVariable.IsString);
            //var NumberVars = vars.Where(a => a.LinkedVariable.IsNumber);
            //var ItemVars = vars.Where(a => a.LinkedVariable.IsItem);
            //var CommonEventRefVars = vars.Where(a => a.LinkedVariable.IsCommonEventRef);
            //vars = new ObservableCollection<GenericRef<VarArray>>();
            //if (ShowDateTime)
            //{
            //    foreach (var a in DateTimeVars)
            //    {
            //        vars.Add(a);
            //    }
            //}
            //if (ShowNumber)
            //{
            //    foreach (var a in NumberVars)
            //    {
            //        vars.Add(a);
            //    }
            //}
            //if (ShowString)
            //{
            //    foreach (var a in StringVars)
            //    {
            //        vars.Add(a);
            //    }
            //}
            //if (ShowItems)
            //{
            //    foreach (var a in ItemVars)
            //    {
            //        vars.Add(a);
            //    }
            //}
            //if (ShowCommonEventRefs)
            //{
            //    foreach (var a in CommonEventRefVars)
            //    {
            //        vars.Add(a);
            //    }
            //}
            //this.lstItems.ItemsSource = vars.OrderBy(a => a.LinkedVariable.Name);
            //if (SelectedItem != null && lstItems.Items.Contains(SelectedItem))
            //{
            //    lstItems.SelectedItem = SelectedItem;
            //}
        }

        private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lstItems.SelectedItem as GenericRef<VarArray> != null)
            //{
            //    SelectedItem = lstItems.SelectedItem as GenericRef<VarArray>;
            //}
        }

        private void treeItems_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeItems.SelectedItem as GenericRef<VarArray> != null)
            {
                SelectedItem = treeItems.SelectedItem as GenericRef<VarArray>;
            }
        }
    }
}
