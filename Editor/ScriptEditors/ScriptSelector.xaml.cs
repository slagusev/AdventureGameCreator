using Editor.Scripter;
using Editor.Scripter.ConversationFlow;
using Editor.Scripter.Flow;
using Editor.Scripter.ItemManagement;
using Editor.Scripter.TextFunctions;
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
                var newComment = new Scripter.Conditions.Conditional(script);
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
                script.SelectedLine = script.ScriptLines[script.ScriptLines.IndexOf(newItem)+1];
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

        private void StopGame_Click(object sender, RoutedEventArgs e)
        {
            var script = this.DataContext as Script;
            var stopGame = new StopGame();
            script.AddBeforeSelected(stopGame);
            this.Close();
        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            var newAddNewItemWindow = new ScriptEditors.AddItemEditor();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var newAddItem = new Scripter.ItemManagement.AddItemToInventory();
                script.AddBeforeSelected(newAddItem);
                newAddNewItemWindow.DataContext = newAddItem;
                newAddNewItemWindow.ShowDialog();
                if (newAddNewItemWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = newAddItem;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(newAddItem);
                }

            }


        }

        private void getItemPropertyClick(object sender, RoutedEventArgs e)
        {
            var newGetItemPropertyWindow = new ScriptEditors.GetItemPropertyEditor();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var getItemProperty = new Scripter.GetItemProperty();
                script.AddBeforeSelected(getItemProperty);
                newGetItemPropertyWindow.DataContext = getItemProperty;
                newGetItemPropertyWindow.ShowDialog();
                if (newGetItemPropertyWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = getItemProperty;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(getItemProperty);
                }

            }
        }

        private void addText_Click(object sender, RoutedEventArgs e)
        {
            var newAddTextWindow = new ScriptEditors.AddTextEditor();
            var script = this.DataContext as Script;
            if (script != null)
            {
                var newAddText = new Scripter.TextFunctions.AddText();
                script.AddBeforeSelected(newAddText);
                newAddTextWindow.DataContext = newAddText;
                newAddTextWindow.ShowDialog();
                if (newAddTextWindow.DialogResult == true)
                {
                    invalidate = true;
                    newItem = newAddText;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(newAddText);
                }
            }
        }

        private void setItemPropertyClick(object sender, RoutedEventArgs e)
        {
            LoadWindow(new SetItemPropertyEditor(), new SetItemProperty());
        }

        private void getCurrentItemClick(object sender, RoutedEventArgs e)
        {
            LoadWindow(new GetCurrentItemEditor(), new GetCurrentItem());
        }

        private void removeItem_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new RemoveItemEditor(), new RemoveItem());
        }

        private void removeCurrentItem_Click(object sender, RoutedEventArgs e)
        {
            var script = this.DataContext as Script;
            var newRemoveCurrentItem = new RemoveThisItem();
            script.AddBeforeSelected(newRemoveCurrentItem);
            this.Close();
        }
        private void LoadWindow(Window editor, ScriptLine line)
        {
            var script = this.DataContext as Script;
            if (script != null)
            {
                script.AddBeforeSelected(line);
                editor.DataContext = line;
                editor.ShowDialog();
                if (editor.DialogResult == true)
                {
                    invalidate = true;
                    newItem = line;
                    this.Close();
                }
                else
                {
                    script.ScriptLines.Remove(line);
                }

            }
        }

        private void runCommonEvent_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWindow(new RunCommonEventEditor(), new RunCommonEvent(this.DataContext as Script));
        }

        private void ReturnValue_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ReturnValueEditor(), new ReturnValue());
        }

        private void getEquippedItem_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new GetEquipmentSlotEditor(), new GetEquipmentSlot());
        }

        private void forceEquip_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ForceEquipEditor(), new ForceEquip());
        }

        private void forceUnequip_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ForceUnequipEditor(), new ForceUnequip());
        }

        private void startConversation_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new StartConversationEditor(), new StartConversation());
        }

        private void goToStage_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWindow(new GoToStageEditor(), new GoToStage());
        }

        private void changeRoom_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ChangeRoomEditor(), new Editor.Scripter.Misc.ChangeRoom());
        }

        private void addEvent_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWindow(new CreateEventEditor(), new CreateEvent());
        }

        private void addToArray_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.AddToArray(), new Scripter.Arrays.AddToArray());
        }

        private void iterateThroughArray_Click(object sender, RoutedEventArgs e)
        {
            var script = this.DataContext as Script;
            LoadWindow(new ScriptEditors.ForEachInArrayEditor(), new Scripter.Arrays.ForEachInArray(script));
        }

        private void displayImage_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.ShowImageEditor(), new Scripter.TextFunctions.ShowImage());
        }

        private void addImage_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.AddImageEditor(), new Scripter.TextFunctions.AddImage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.GetItemNameEditor(), new Scripter.ItemManagement.GetItemName());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.SetItemNameEditor(), new Scripter.ItemManagement.SetItemName());
        }

        private void getAllItems_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.GetAllItemsEditor(), new Scripter.Arrays.GetAllItems());
        }

        private void getSomeItems_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.GetAllItemsOfTypeEditor(), new Scripter.Arrays.GetAllItemsOfType());
        }

        private void concatArray_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow(new ScriptEditors.ConcatenateArrayEditor(), new Scripter.Arrays.ConcatenateArray());
        }

        private void stopScript_Click(object sender, RoutedEventArgs e)
        {
            var script = this.DataContext as Script;
            var newStopProcessing = new StopProcessing();
            script.AddBeforeSelected(newStopProcessing);
            this.Close();
        }

    }
}
