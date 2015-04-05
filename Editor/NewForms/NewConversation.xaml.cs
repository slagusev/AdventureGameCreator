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
    /// Interaction logic for NewConversation.xaml
    /// </summary>
    public partial class NewConversation : Window
    {
        public NewConversation()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateConversation();
            this.Close();
        }


        private void CreateConversation()
        {
            var name = txtName.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                
                    if (MainViewModel.MainViewModelStatic.Conversations.Where(a => a.Name == name).Count() == 0)
                    {
                        var convo = new Conversation();
                        convo.Name = name;
                        convo.StartingStage = 10;
                        convo.Stages.Add(new ConversationStage { StageId = 10, StageName = "Conversation Start" });

                        MainViewModel.MainViewModelStatic.Conversations.Add(convo);
                        var wv = new WindowView
                        {
                            TabName = "Conversation  - " + convo.Name,
                            Content = (UserControl)new ConversationEditor
                            {
                                DataContext = convo
                            }
                        };
                        MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                        MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("The Conversation " + name + " already exists. Please choose another name.");
                    }
                

            }
            else
            {
                MessageBox.Show("Please choose a name for the item.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CreateConversation();
            }
        }
    }
}
