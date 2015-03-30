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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor
{
    /// <summary>
    /// Interaction logic for EditorMenu.xaml
    /// </summary>
    public partial class EditorMenu : UserControl
    {
        public EditorMenu()
        {
            InitializeComponent();
        }

        private void mainTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (mainTree.SelectedItem == null) return;
            WindowView wv = null;
            if (mainTree.SelectedItem.GetType() == typeof(Zone))
            {
                Zone z = mainTree.SelectedItem as Zone;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as ZoneEditor
                                       where b != null && b.DataContext == z
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                        {
                            TabName = "Zone - " + z.ZoneName,
                            Content = (UserControl)new ZoneEditor
                                {
                                    DataContext = z
                                }
                        };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (mainTree.SelectedItem.GetType() == typeof(InteractableGroup))
            {
                InteractableGroup g = mainTree.SelectedItem as InteractableGroup;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as InteractableGroupViewer
                                       where b != null && b.DataContext == g
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Interactable Group - " + g.Name,
                        Content = (UserControl)new InteractableGroupViewer
                        {
                            DataContext = g
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (mainTree.SelectedItem.GetType() == typeof(Variable))
            {
                Variable g = mainTree.SelectedItem as Variable;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as VariableEditor
                                       where b != null && b.DataContext == g
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Variable - " + g.Name,
                        Content = (UserControl)new VariableEditor
                        {
                            DataContext = g
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (mainTree.SelectedItem.GetType() == typeof(ItemClass))
            {
                ItemClass g = mainTree.SelectedItem as ItemClass;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as ItemClassEditor
                                       where b != null && b.DataContext == g
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Item Class - " + g.Name,
                        Content = (UserControl)new ItemClassEditor
                        {
                            DataContext = g
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (mainTree.SelectedItem.GetType() == typeof(Item))
            {
                Item g = mainTree.SelectedItem as Item;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as ItemEditor
                                       where b != null && b.DataContext == g
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Item - " + g.ItemName,
                        Content = (UserControl)new ItemEditor
                        {
                            DataContext = g
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (mainTree.SelectedItem.GetType() == typeof(CommonEvent))
            {
                CommonEvent g = mainTree.SelectedItem as CommonEvent;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as CommonEventEditor
                                       where b != null && b.DataContext == g
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Common Event - " + g.Name,
                        Content = (UserControl)new CommonEventEditor
                        {
                            DataContext = g
                        }
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                }
                else wv = existingWindows.First();
            }
            if (wv != null)
                MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
        }

        private void gameSettings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                   let b = a.Content as PlayerSettingsEditor
                                   where b != null && b.DataContext == MainViewModel.MainViewModelStatic.Settings
                                   select a);
            WindowView wv;
            if (existingWindows.Count() == 0)
            {
                wv = new WindowView
                {
                    TabName = "Game and Character Settings",
                    Content = (UserControl)new PlayerSettingsEditor
                    {
                        DataContext = MainViewModel.MainViewModelStatic.Settings
                    }
                };
                MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
            }
            else
            {
                wv = existingWindows.First();
            }
            MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
        }

    }
}
