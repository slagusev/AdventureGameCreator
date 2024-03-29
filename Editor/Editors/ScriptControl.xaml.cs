﻿using Editor.Scripter.Misc;
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
using Editor.Scripter.ItemManagement;
using System.Xml.Linq;
using Editor.Scripter.ConversationFlow;

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
            ScriptEditorsByScriptType.Add(typeof(AddText), typeof(ScriptEditors.AddTextEditor));
            ScriptEditorsByScriptType.Add(typeof(SetVariable), typeof(ScriptEditors.SetVariableEditor));
            ScriptEditorsByScriptType.Add(typeof(AddItemToInventory), typeof(ScriptEditors.AddItemEditor));
            ScriptEditorsByScriptType.Add(typeof(GetItemProperty), typeof(ScriptEditors.GetItemPropertyEditor));
            ScriptEditorsByScriptType.Add(typeof(SetItemProperty), typeof(ScriptEditors.SetItemPropertyEditor));
            ScriptEditorsByScriptType.Add(typeof(GetCurrentItem), typeof(ScriptEditors.GetCurrentItemEditor));
            ScriptEditorsByScriptType.Add(typeof(RunCommonEvent), typeof(ScriptEditors.RunCommonEventEditor));
            ScriptEditorsByScriptType.Add(typeof(ReturnValue), typeof(ScriptEditors.ReturnValueEditor));
            ScriptEditorsByScriptType.Add(typeof(GetEquipmentSlot), typeof(ScriptEditors.GetEquipmentSlotEditor));
            ScriptEditorsByScriptType.Add(typeof(ForceUnequip), typeof(ScriptEditors.ForceUnequipEditor));
            ScriptEditorsByScriptType.Add(typeof(ForceEquip), typeof(ScriptEditors.ForceEquipEditor));
            ScriptEditorsByScriptType.Add(typeof(StartConversation), typeof(ScriptEditors.StartConversationEditor));
            ScriptEditorsByScriptType.Add(typeof(GoToStage), typeof(ScriptEditors.GoToStageEditor));
            ScriptEditorsByScriptType.Add(typeof(ChangeRoom), typeof(ScriptEditors.ChangeRoomEditor));
            ScriptEditorsByScriptType.Add(typeof(CreateEvent), typeof(ScriptEditors.CreateEventEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.Arrays.AddToArray), typeof(ScriptEditors.AddToArray));
            ScriptEditorsByScriptType.Add(typeof(Scripter.Arrays.ForEachInArray), typeof(ScriptEditors.ForEachInArrayEditor));
            ScriptEditorsByScriptType.Add(typeof(ShowImage), typeof(ScriptEditors.ShowImageEditor));
            ScriptEditorsByScriptType.Add(typeof(AddImage), typeof(ScriptEditors.AddImageEditor));
            ScriptEditorsByScriptType.Add(typeof(GetItemName), typeof(ScriptEditors.GetItemNameEditor));
            ScriptEditorsByScriptType.Add(typeof(SetItemName), typeof(ScriptEditors.SetItemNameEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.Arrays.GetAllItems), typeof(ScriptEditors.GetAllItemsEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.Arrays.GetAllItemsOfType), typeof(ScriptEditors.GetAllItemsOfTypeEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.Arrays.ConcatenateArray), typeof(ScriptEditors.ConcatenateArrayEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.StatusEffects.AddStatusEffect), typeof(ScriptEditors.AddStatusEffectEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.StatusEffects.RemoveStatusEffect), typeof(ScriptEditors.RemoveStatusEffectEditor));
            ScriptEditorsByScriptType.Add(typeof(Scripter.StatusEffects.GetArgument), typeof(ScriptEditors.GetArgumentEditor));
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
                Script tempScript = new Script();
                tempScript.ScriptLines.Add(script.SelectedLine);
                tempScript = Script.FromXML(tempScript.ToXML(), script); //Copy the script to have a new reference
                window.DataContext = tempScript.ScriptLines.First();
                window.ShowDialog();
                //Invalidate the form
                if (window.DialogResult == true)
                {
                    var tempLine = tempScript.ScriptLines.First();
                    script.AddBeforeSelected(tempLine);
                    script.ScriptLines.Remove(script.SelectedLine);
                    var selected = tempLine;
                    script.SelectedLine = null;
                    var lines = script.ScriptLines;
                    script.ScriptLines = null;
                    script.ScriptLines = new System.Collections.ObjectModel.ObservableCollection<ScriptLine>();
                    script.ScriptLines = lines;
                    script.SelectedLine = selected;
                }
            }

        }

        private void CopyStep_Click(object sender, RoutedEventArgs e)
        {
            var script = (this.DataContext as Script);
            
            if (script.SelectedLine != null && script.SelectedLine.GetType() != typeof(Editor.Scripter.Misc.Blank))
            {
                var selected = script.SelectedLine;
                TextBox tb = new TextBox();
                tb.Text = selected.ToXML().ToString().Replace("\r\n", "");
                tb.SelectAll();
                tb.Copy();
            } 
            //TextBox tb = new TextBox();
            //tb.Text = 
        }

        private void CopyScript_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Text = (this.DataContext as Script).ToXML().ToString().Replace("\r\n", "");
            tb.SelectAll();
            tb.Copy();
        }

        private void PasteStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var script = (this.DataContext as Script);
                TextBox tb = new TextBox();
                tb.Paste();
                XElement xml = XElement.Parse(tb.Text);
                Script s;
                
                if (xml.Name != "Script")
                {
                    xml = new XElement("Script", xml);
                }
                
                s = Script.FromXML(xml, script);
                
                foreach (var line in s.ScriptLines)
                {
                    if (line.GetType() != typeof(Editor.Scripter.Misc.Blank))
                    {
                        script.AddBeforeSelected(line);
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Clipboard does not contain valid XML.");
            }
        }



         
    }
}
