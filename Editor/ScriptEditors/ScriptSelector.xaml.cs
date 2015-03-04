using Editor.Scripter;
using Editor.Scripter.Flow;
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

namespace Editor.ScriptEditors
{
    /// <summary>
    /// Interaction logic for ScriptSelector.xaml
    /// </summary>
    public partial class ScriptSelector : Window
    {
        bool invalidate = false;
        ScriptLine newItem = null;
        public ScriptSelector()
        {
            InitializeComponent();
        }

        private void addComment_Click(object sender, RoutedEventArgs e)
        {
            var newCommentWindow = new ScriptEditors.Comment();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var newComment = new Scripter.Misc.Comment();
                script.AddBeforeSelected(newComment);
                newCommentWindow.DataContext = newComment;
                newCommentWindow.ShowDialog();
                if (newCommentWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = newComment;
                    this.Close();
                    

                }
                else
                {
                    script.ScriptLines.Remove(newComment);
                }
            }
            
        }

        private void IfThenElse_Click(object sender, RoutedEventArgs e)
        {
            var newConditionalWindow = new ScriptEditors.ConditionEditor();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var newComment = new Scripter.Conditions.Conditional();
                script.AddBeforeSelected(newComment);
                newConditionalWindow.DataContext = newComment;
                newConditionalWindow.ShowDialog();
                if (newConditionalWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = newComment;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(newComment);
                }
            }
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            if (invalidate)
            {
                var script = this.DataContext as Script;
                var lines = script.ScriptLines;
                script.ScriptLines = null;
                script.ScriptLines = new System.Collections.ObjectModel.ObservableCollection<ScriptLine>();
                script.ScriptLines = lines;
                script.SelectedLine = newItem;
            }
        }

        private void displayText_Click(object sender, RoutedEventArgs e)
        {
            var newDisplayTextWindow = new ScriptEditors.DisplayTextEditor();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var newDisplayText = new Scripter.TextFunctions.DisplayText();
                script.AddBeforeSelected(newDisplayText);
                newDisplayTextWindow.DataContext = newDisplayText;
                newDisplayTextWindow.ShowDialog();
                if (newDisplayTextWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = newDisplayText;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(newDisplayText);
                }
            }
        }

        private void SetVariable_Click(object sender, RoutedEventArgs e)
        {
            var newSetVariableWindow = new ScriptEditors.SetVariableEditor();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var newSetVariable = new Scripter.Flow.SetVariable();
                script.AddBeforeSelected(newSetVariable);
                newSetVariable.SelectedVariable = new ObjectTypes.VarRef() ;
                newSetVariable.TargetVar = new ObjectTypes.VarRef();
                newSetVariableWindow.DataContext = newSetVariable;
                newSetVariableWindow.ShowDialog();
                if (newSetVariableWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = newSetVariable;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(newSetVariable);
                }

            }
        }

        private void ReturnTrue_Click(object sender, RoutedEventArgs e)
        {
            var script = this.DataContext as Script;
            var newReturnTrue = new ReturnTrue();
            script.AddBeforeSelected(newReturnTrue);
            this.Close();
        }

        private void ReturnFalse_Click(object sender, RoutedEventArgs e)
        {
            var script = this.DataContext as Script;
            var newReturnFalse = new ReturnFalse();
            script.AddBeforeSelected(newReturnFalse);
            this.Close();
        }
    }
}
