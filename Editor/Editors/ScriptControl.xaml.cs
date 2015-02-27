using Editor.Scripter.Misc;
using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Editor.Scripter.Conditions;
using Editor.Scripter.TextFunctions;
using Editor.Scripter.Flow;

namespace Editor.Editors
{
    /// <summary>
    /// Interaction logic for ScriptControl.xaml
    /// </summary>
    public partial class ScriptControl : UserControl
    {
        static Dictionary<Type, Type> ScriptEditorsByScriptType = new Dictionary<Type, Type>();

        static ScriptControl()
        {
            ScriptEditorsByScriptType.Add(typeof(Comment), typeof(ScriptEditors.Comment));
            ScriptEditorsByScriptType.Add(typeof(Conditional), typeof(ScriptEditors.ConditionEditor));
            ScriptEditorsByScriptType.Add(typeof(DisplayText), typeof(ScriptEditors.DisplayTextEditor));
            ScriptEditorsByScriptType.Add(typeof(SetVariable), typeof(ScriptEditors.SetVariableEditor));
        }

        public Window GetScriptEditorByType(Type scriptLineType)
        {
            if (ScriptEditorsByScriptType.ContainsKey(scriptLineType))
            {
                return (Window)(Activator.CreateInstance(ScriptEditorsByScriptType[scriptLineType]));
            }
            return null;
        }

        public ScriptControl()
        {
            InitializeComponent();
            var script = new Script();

            script.ScriptLines.Add(new Comment { CommentText = "Test" });


            this.DataContext = script;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ScriptEditors.ScriptSelector selector = new ScriptEditors.ScriptSelector();
            selector.DataContext = this.DataContext;
            selector.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var script = (this.DataContext as Script);
            if (script.SelectedLine != null && script.SelectedLine.GetType() != typeof(Blank))
            {
                script.ScriptLines.Remove(script.SelectedLine);
            }
        }
        private void ListBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
        
            var script = (this.DataContext as Script);
            Window window = null;
            if (script.SelectedLine != null)
            {
                var selected = script.SelectedLine;
                //Decide what kind of window to open.
                window = GetScriptEditorByType(selected.GetType());
            }
            if (window != null)
            {
                window.DataContext = script.SelectedLine;
                window.ShowDialog();
                //Invalidate the form
                var selected = script.SelectedLine;
                script.SelectedLine = null;
                var lines = script.ScriptLines;
                script.ScriptLines = null;
                script.ScriptLines = new System.Collections.ObjectModel.ObservableCollection<ScriptLine>();
                script.ScriptLines = lines;
                script.SelectedLine = selected;
            }

        }



         
    }
}
