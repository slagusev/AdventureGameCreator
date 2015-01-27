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
    /// Interaction logic for InteractableGroupViewer.xaml
    /// </summary>
    public partial class InteractableGroupViewer : UserControl
    {
        public InteractableGroupViewer()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            WindowView wv = null;
            if (listInteractables.SelectedItem.GetType() == typeof(Interactable))
            {
                
                Interactable g = listInteractables.SelectedItem as Interactable;
                var existingWindows = (from a in MainViewModel.MainViewModelStatic.OpenWindows
                                       let b = a.Content as InteractableEditor
                                       where b != null && b.DataContext == g
                                       select a);
                if (existingWindows.Count() == 0)
                {
                    wv = new WindowView
                    {
                        TabName = "Interactable - " + g.InteractableName,
                        Content = (UserControl)new InteractableEditor
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
    }
}
