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
    /// Interaction logic for NewConversationStage.xaml
    /// </summary>
    public partial class NewConversationStage : Window
    {
        public NewConversationStage()
        {
            InitializeComponent();
        }
        public Conversation associatedConvo;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateStage();
        }

        private void CreateStage()
        {
            int id;
            if (Int32.TryParse(txtId.Text, out id) && id >= 0)
            {
                if (associatedConvo.Stages.Where(a => a.StageId == id).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("A stage with this ID already exists. Choose another ID.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a non-negative integer for the ID.");
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
                CreateStage();
            }
        }
    }
}
