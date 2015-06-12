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
    public partial class VariableSelector : UserControl
    {
        public static readonly DependencyProperty ShowDateTimeProperty = DependencyProperty.Register("ShowDateTime", typeof(bool), typeof(VariableSelector), new PropertyMetadata(true, PropertiesChanged));
        public static readonly DependencyProperty ShowStringProperty = DependencyProperty.Register("ShowString", typeof(bool), typeof(VariableSelector), new PropertyMetadata(true, PropertiesChanged));
        public static readonly DependencyProperty ShowNumberProperty = DependencyProperty.Register("ShowNumber", typeof(bool), typeof(VariableSelector), new PropertyMetadata(true, PropertiesChanged));
        public static readonly DependencyProperty ShowItemsProperty = DependencyProperty.Register("ShowItems", typeof(bool), typeof(VariableSelector), new PropertyMetadata(true, PropertiesChanged));
        public static readonly DependencyProperty ShowCommonEventRefsProperty = DependencyProperty.Register("ShowCommonEventRefs", typeof(bool), typeof(VariableSelector), new PropertyMetadata(true, PropertiesChanged));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(VarRef), typeof(VariableSelector), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, SelectedItemChanged));
        public bool ShowDateTime { get { 
            return (bool)GetValue(ShowDateTimeProperty); 
        } set { 
            SetValue(ShowDateTimeProperty, value); 
        } }

        public VarRef SelectedItem
        {
            get
            {
                return (VarRef)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
                if (value != null && value.LinkedVariable != null) currentlySelected.Text = value.LinkedVariable.Name;
            }
        }

        private static void PropertiesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as VariableSelector).RefreshListBox();
        }
        private static void SelectedItemChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && e.NewValue != null)
            {
                VariableSelector vsSource = source as VariableSelector;
                VarRef newRef = e.NewValue as VarRef;
                foreach (var a in vsSource.treeItems.Items)
                {
                    VarRef srcRef = a as VarRef;
                    if (newRef != null && srcRef != null && newRef.LinkedVarId == srcRef.LinkedVarId)
                    {
                        //vsSource.treeItems.SelectedItem = srcRef;
                    }
                }
            }
                //(source as VariableSelector).lstItems.SelectedItem = (source as VariableSelector).lstItems.Items.where ((VarRef)a).LinkedVarId == ((VarRef)e.NewValue).LinkedVarId select a).FirstOrDefault();
        }
        public bool ShowString { get { return (bool)GetValue(ShowStringProperty); } set { SetValue(ShowStringProperty, value); } }
        public bool ShowNumber { get { return (bool)GetValue(ShowNumberProperty); } set { SetValue(ShowNumberProperty, value); } }
        public bool ShowItems { get { return (bool)GetValue(ShowItemsProperty); } set { SetValue(ShowItemsProperty, value); } }
        public bool ShowCommonEventRefs { get { return (bool)GetValue(ShowCommonEventRefsProperty); } set { SetValue(ShowCommonEventRefsProperty, value); } }
        public VariableSelector()
        {
            //ShowDateTime = true;
            //ShowString = true;
            //ShowNumber = true;
            InitializeComponent();
            RefreshListBox();
        }
        ObservableCollection<Tuple<string, ObservableCollection<VarRef>>> vars = new ObservableCollection<Tuple<string, ObservableCollection<VarRef>>>();
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
            vars = new ObservableCollection<Tuple<string, ObservableCollection<VarRef>>>();
            foreach (var a in MainViewModel.MainViewModelStatic.VariableGroups.Groups)
            {
                bool included = false;
                ObservableCollection<VarRef> Vars = new ObservableCollection<VarRef>();
                foreach (var b in a.Item2)
                {
                    if (b.Name.ToLower().Contains(searchText.Text.ToLower()))
                    {
                        if ((b.IsCommonEventRef && ShowCommonEventRefs) ||
                            (b.IsDateTime && ShowDateTime) ||
                            (b.IsNumber && ShowNumber) ||
                            (b.IsString && ShowString) ||
                            (b.IsItem && ShowItems))
                        {
                            included = true;
                            Vars.Add(new VarRef(b.Id));
                        }
                        else
                        {
                            // :\
                        }
                    }
                }
                if (included)
                {
                    vars.Add(Tuple.Create<string, ObservableCollection<VarRef>>(a.Item1, Vars));
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
            //vars = new ObservableCollection<VarRef>();
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
            //if (lstItems.SelectedItem as VarRef != null)
            //{
            //    SelectedItem = lstItems.SelectedItem as VarRef;
            //}
        }

        private void treeItems_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeItems.SelectedItem as VarRef != null)
            {
                SelectedItem = treeItems.SelectedItem as VarRef;
            }
        }
    }
}
