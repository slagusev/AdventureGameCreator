using Editor.Scripter;
using Editor.Scripter.Arrays;
using Editor.Scripter.Conditions;
using Editor.Scripter.ConversationFlow;
using Editor.Scripter.Flow;
using Editor.Scripter.ItemManagement;
using Editor.Scripter.Misc;
using Editor.Scripter.TextFunctions;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    abstract class ScriptLineWrapper
    {
        public abstract bool? Execute();
        public static ScriptLineWrapper GetScriptLineWrapper(ScriptLine line, ScriptWrapper parent)
        {
            var lineType = line.GetType();
            ScriptLineWrapper lineWrapper = null;
            if (lineType == typeof(Comment))
            {
                lineWrapper = new CommentWrapper((Comment)line);
                //return comment;
            }
            if (lineType == typeof(Conditional))
            {
                lineWrapper = new ConditionalWrapper((Conditional)line);
            }
            if (lineType == typeof(DisplayText))
            {
                lineWrapper = new DisplayTextWrapper((DisplayText)line);
            }
            if (lineType == typeof(SetVariable))
            {
                lineWrapper = new SetVariableWrapper((SetVariable)line);
            }
            if (lineType == typeof(ReturnTrue))
            {
                lineWrapper = new ReturnTrueWrapper();
            }
            if (lineType == typeof(ReturnFalse))
            {
                lineWrapper = new ReturnFalseWrapper();
            }
            if (lineType == typeof(StopGame))
            {
                lineWrapper = new StopGameWrapper();
            }
            if (lineType == typeof(AddText))
            {
                lineWrapper = new AddTextWrapper((AddText)line);
            }
            if (lineType == typeof(AddItemToInventory))
            {
                lineWrapper = new AddItemWrapper((AddItemToInventory)line);
            }
            if (lineType == typeof(SetItemProperty))
            {
                lineWrapper = new SetItemPropertyWrapper((SetItemProperty)line);
            }
            if (lineType == typeof(GetItemProperty))
            {
                lineWrapper = new GetItemPropertyWrapper((GetItemProperty)line);
            }
            if (lineType == typeof(GetCurrentItem))
            {
                lineWrapper = new GetCurrentItemWrapper((GetCurrentItem)line);
            }
            if (lineType == typeof(RemoveItem))
            {
                lineWrapper = new RemoveItemWrapper((RemoveItem)line);
            }
            if (lineType == typeof(RemoveThisItem))
            {
                lineWrapper = new RemoveThisItemWrapper((RemoveThisItem)line);
            }
            if (lineType == typeof(RunCommonEvent))
            {
                lineWrapper = new RunCommonEventWrapper((RunCommonEvent)line);
            }
            if (lineType == typeof(ReturnValue))
            {
                lineWrapper = new ReturnValueWrapper((ReturnValue)line);
            }
            if (lineType == typeof(GetEquipmentSlot))
            {
                lineWrapper = new GetEquipmentSlotWrapper((GetEquipmentSlot)line);
            }
            if (lineType == typeof(ForceEquip))
            {
                lineWrapper = new ForceEquipWrapper((ForceEquip)line);
            }
            if (lineType == typeof(ForceUnequip))
            {
                lineWrapper = new ForceUnequipWrapper((ForceUnequip)line);
            }
            if (lineType == typeof(StartConversation))
            {
                lineWrapper = new StartConversationWrapper((StartConversation)line);
            }
            if (lineType == typeof(GoToStage))
            {
                lineWrapper = new GoToStageWrapper((GoToStage)line);
            }
            if (lineType == typeof(ChangeRoom))
            {
                lineWrapper = new ChangeRoomWrapper((ChangeRoom)line);
            }
            if (lineType == typeof(CreateEvent))
            {
                lineWrapper = new CreateEventWrapper((CreateEvent)line);
            }
            if (lineType == typeof(AddToArray))
            {
                lineWrapper = new AddToArrayWrapper((AddToArray)line);
            }
            if (lineType == typeof(ForEachInArray))
            {
                lineWrapper = new ForEachInArrayWrapper((ForEachInArray)line);
            }
            if (lineWrapper != null)
            {
                lineWrapper.parent = parent;
            }
            return lineWrapper;
        }
        public ScriptWrapper parent {get; set; }
        
    }
}
