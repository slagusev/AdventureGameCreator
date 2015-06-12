using Editor.Scripter.Conditions;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ConditionalWrapper : ScriptLineWrapper
    {
        public Conditional line = null;
        public ConditionalWrapper(Conditional c)
        {
            line = c;
        }
        public override bool? Execute()
        {
            bool? conditionResult = null;
            
            if (line.IsComparison)
                conditionResult = ComparisonCheck();
            if (line.ItemIsNotNull)
            {
                if (line.SelectedVariable != null)
                {
                    var v = parent.GetVarById(line.SelectedVariable.LinkedVarId);
                    if (v != null)
                    {
                        conditionResult = v.CurrentItemValue != null;
                    }
                }
            }
            if (line.ItemIsClass)
            {
                if (line.SelectedVariable == null)
                    conditionResult = false;
                else
                {
                    var variable = parent.GetVarById(line.SelectedVariable.LinkedVarId);
                    if (variable.CurrentItemValue == null)
                    {
                        conditionResult = false;
                    }
                    else
                    {
                        conditionResult = false;
                        var parentClass = variable.CurrentItemValue.item.ItemClassParent;
                        while (parentClass != null && conditionResult == false)
                        {
                            if (parentClass.Name == line.SelectedClassName)
                            {
                                conditionResult = true;
                            }
                            parentClass = parentClass.ParentClass;
                        }
                    }
                }
            }
            if (line.PlayerHasItem)
            {
                if (line.SelectedItem != null && line.SelectedItem.LinkedItemId != null)
                {
                    if (MainViewModel.GetMainViewModelStatic().CurrentGame.PlayerInventory.Where(a => a.item.ItemID == line.SelectedItem.LinkedItemId).Count() > 0)
                    {
                        conditionResult = true;
                    }
                    else conditionResult = false;
                    
                }
                else
                {

                    return false;
                }
            }
            if (conditionResult == null)
                return false;
            bool? result = null;
            if (conditionResult == true)
            {
                ScriptWrapper s = new ScriptWrapper(line.ThenStatement);
                s.parent = parent;
                result = s.Execute();
            }
            else if (conditionResult == false)
            {
                ScriptWrapper s = new ScriptWrapper(line.ElseStatement);
                s.parent = parent;
                result = s.Execute();
            }




            return result;

        }

        public bool? ComparisonCheck()
        {
            var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
            var leftVar = parent.GetVarById(line.SelectedVariable.LinkedVarId);
            if (line.IsDateTime)
            {
                DateTime left = leftVar.CurrentDateTimeValue;
                DateTime right;
                if (line.IsComparisonToVariable)
                {
                    var rightVar = parent.GetVarById(line.VariableToCompare.LinkedVarId);
                    right = rightVar.CurrentDateTimeValue;
                }
                else
                {
                    right = line.DateTimeToCompareTo;
                }
                if (line.IsEquals)
                    return left.Ticks == right.Ticks;
                else if (line.IsNotEquals)
                    return left.Ticks != right.Ticks;
                else if  (line.IsGreaterThan)
                    return left.Ticks > right.Ticks;
                else if  (line.IsGreaterThanOrEqualTo)
                    return left.Ticks >= right.Ticks;
                else if  (line.IsLessThan)
                    return left.Ticks < right.Ticks;
                else if (line.IsLessThanOrEqualTo)
                    return left.Ticks <= right.Ticks;
            }
            if (line.IsNumber)
            {

                int left = leftVar.CurrentNumberValue;
                int right;
                if (line.IsComparisonToVariable)
                {
                    var rightVar = parent.GetVarById(line.VariableToCompare.LinkedVarId);
                    right = rightVar.CurrentNumberValue;
                }
                else
                {
                    right = line.NumberToCompareTo;
                }
                if (line.IsEquals)
                    return left == right;
                else if (line.IsNotEquals)
                    return left != right;
                else if (line.IsGreaterThan)
                    return left > right;
                else if (line.IsGreaterThanOrEqualTo)
                    return left >= right;
                else if (line.IsLessThan)
                    return left < right;
                else if (line.IsLessThanOrEqualTo)
                    return left <= right;
            }
            if (line.IsString)
            {
                string left = leftVar.CurrentStringValue;
                string right;
                if (line.IsComparisonToVariable)
                {
                    var rightVar = parent.GetVarById(line.VariableToCompare.LinkedVarId);
                    right = rightVar.CurrentStringValue;
                }
                else
                {
                    right = line.StringToCompareTo;
                }
                if (line.IsEquals)
                    return left == right;
                else if (line.IsNotEquals)
                    return left != right;
            }
            return null;
        }
    }
}
