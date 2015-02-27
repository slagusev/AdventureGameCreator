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
        public bool ShowString { get { return (bool)GetValue(ShowStringProperty); } set { SetValue(ShowStringProperty, value); } }
        public bool ShowNumber { get { return (bool)GetValue(ShowNumberProperty); } set { SetValue(ShowNumberProperty, value); } }
        public VariableSelector()
        {
            //ShowDateTime = true;
            //ShowString = true;
            //ShowNumber = true;
            InitializeComponent();
            RefreshListBox();
        }
        ObservableCollection<VarRef> vars = new ObservableCollection<VarRef>();
        private void searchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListBox();
        }

        public void RefreshListBox()
        {
            vars = new ObservableCollection<VarRef>(MainViewModel.MainViewModelStatic.Variables.Where(a => a.Name.ToLower().Contains(searchText.Text.ToLower())).Select(a => new VarRef(a.Id)));
            var DateTimeVars = vars.Where(a => a.LinkedVariable.IsDateTime);
            var StringVars = vars.Where(a => a.LinkedVariable.IsString);
            var NumberVars = vars.Where(a => a.LinkedVariable.IsNumber);
            vars = new ObservableCollection<VarRef>();
            if (ShowDateTime)
            {
                foreach (var a in DateTimeVars)
                {
                    vars.Add(a);
                }
            }
            if (ShowNumber)
            {
                foreach (var a in NumberVars)
                {
                    vars.Add(a);
                }
            }
            if (ShowString)
            {
                foreach (var a in StringVars)
                {
                    vars.Add(a);
                }
            }

    
            this.lstItems.ItemsSource = vars;
            if (SelectedItem != null && lstItems.Items.Contains(SelectedItem))
            {
                lstItems.SelectedItem = SelectedItem;
            }
        }

        private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstItems.SelectedItem as VarRef != null)
            {
                SelectedItem = lstItems.SelectedItem as VarRef;
            }
        }
    }
}
