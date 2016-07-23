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
    /// Interaction logic for ConversationSelector.xaml
    /// </summary>
    public partial class ConversationSelector : UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(GenericRef<Conversation>), typeof(ConversationSelector), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, SelectedItemChanged));
        public static readonly DependencyProperty OverridesProperty = DependencyProperty.Register("Overrides", typeof(ObservableCollection<GenericRef<Conversation>>), typeof(ConversationSelector), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OverrideChanged));

        public GenericRef<Conversation> SelectedItem
        {
            get
            {
                return (GenericRef<Conversation>)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }
        public ObservableCollection<GenericRef<Conversation>> Overrides
        {
            get
            {
                return (ObservableCollection<GenericRef<Conversation>>)GetValue(OverridesProperty);
            }
            set
            {
                SetValue(OverridesProperty, value);
            }
        }
        private static void PropertiesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as ConversationSelector).RefreshListBox();
        }
        private static void OverrideChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            //overrides = e.NewValue as ObservableCollection<GenericRef<Conversation>>;
            (source as ConversationSelector).RefreshListBox();
        }
        
        private static void SelectedItemChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && e.NewValue != null)
            {
                ConversationSelector vsSource = source as ConversationSelector;
                //VarRef newRef = e.NewValue as VarRef;
                //foreach (var a in vsSource.lstItems.Items)
                //{
                //    VarRef srcRef = a as VarRef;
                //    if (newRef != null && srcRef != null && newRef.LinkedVarId == srcRef.LinkedVarId)
                //    {
                //        vsSource.lstItems.SelectedItem = srcRef;
                //    }
                //}
            }
                //(source as VariableSelector).lstItems.SelectedItem = (source as VariableSelector).lstItems.Items.where ((VarRef)a).LinkedVarId == ((VarRef)e.NewValue).LinkedVarId select a).FirstOrDefault();
        }
        public ConversationSelector()
        {
            //ShowDateTime = true;
            //ShowString = true;
            //ShowNumber = true;
            InitializeComponent();
            RefreshListBox();
        }
        ObservableCollection<Tuple<string, ObservableCollection<GenericRef<Conversation>>>> vars = new ObservableCollection<Tuple<string,ObservableCollection<GenericRef<Conversation>>>>();
        private void searchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListBox();
        }

        public void RefreshListBox()
        {
            //vars = new ObservableCollection<GenericRef<Conversation>>(MainViewModel.MainViewModelStatic.Conversations.Where(a => a.Name.ToLower().Contains(searchText.Text.ToLower())).Select(a => new GenericRef<Conversation>(a.Id)));
            vars = new ObservableCollection<Tuple<string, ObservableCollection<GenericRef<Conversation>>>>();
            foreach (var a in MainViewModel.MainViewModelStatic.ConversationGroups.Groups)
            {
                bool included = false;
                ObservableCollection<GenericRef<Conversation>> Events = new ObservableCollection<GenericRef<Conversation>>();
                foreach (var b in a.Item2)
                {
                    if (b.Name.ToLower().Contains(searchText.Text.ToLower()) && (Overrides == null || Overrides.Where(c => c.Ref == b.Id).Count() > 0))
                    {
                        included = true;
                        var conversation = GenericRef<Conversation>.GetConversationRef();
                        conversation.Ref = b.Id;
                        Events.Add(conversation);
                    }
                }
                if (included)
                {
                    vars.Add(Tuple.Create<string, ObservableCollection<GenericRef<Conversation>>>(a.Item1, Events));
                }
                //Events.First().Value.Name
            }
            this.treeItems.ItemsSource = vars;
            //vars[0].Item2[0].LinkedConversation.Name
            ExpandAll(treeItems, true);

    
            //this.lstItems.ItemsSource = vars;
            //if (SelectedItem != null && lstItems.Items.Contains(SelectedItem))
            //{
            //    lstItems.SelectedItem = SelectedItem;
            //}
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
        //private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (lstItems.SelectedItem as GenericRef<Conversation> != null)
        //    {
        //        SelectedItem = lstItems.SelectedItem as GenericRef<Conversation>;
        //    }
        //}

        private void treeItems_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeItems.SelectedItem as GenericRef<Conversation> != null)
            {
                SelectedItem = treeItems.SelectedItem as GenericRef<Conversation>;
            }
        }
    }
}
