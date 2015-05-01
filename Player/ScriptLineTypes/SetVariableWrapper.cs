using Editor.ObjectTypes;
using Editor.Scripter.Flow;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class SetVariableWrapper : ScriptLineWrapper
    {
        public SetVariable line = null;
        public SetVariableWrapper(SetVariable sv)
        {
            line = sv;
            
        }
        public override bool? Execute()
        {
            Game g = MainViewModel.GetMainViewModelStatic().CurrentGame;
            VariableWrapper left = null;
            if (parent.GetVarById(line.SelectedVariable.LinkedVarId) != null)
            {
                left = parent.GetVarById(line.SelectedVariable.LinkedVarId);
            }
            else
            {
                MainViewModel.WriteText("ERROR: Can not find variable with ID " + line.SelectedVariable.LinkedVarId + ". Terminating script.", this.parent);
                return false;
            }
            VariableWrapper rightVar = null;
            if (line.IsVariable)
            {
                if (parent.GetVarById(line.TargetVar.LinkedVarId) != null)
                {
                    rightVar = parent.GetVarById(line.TargetVar.LinkedVarId);
                }
                else
                {
                    MainViewModel.WriteText("ERROR: Can not find variable with ID " + line.SelectedVariable.LinkedVarId + ". Terminating script.", this.parent);
                    return false;
                }
            }
            if (line.IsDateTime)
            {
                DateTime right = DateTime.MinValue;
                if (rightVar != null) right = parent.GetVarById(line.TargetVar.LinkedVarId).CurrentDateTimeValue;
                else
                {
                    right = new DateTime(new TimeSpan(line.Days, line.Hours, line.Minutes, line.Seconds).Ticks);
                }
                if (line.IsSet)
                {
                    left.CurrentDateTimeValue = right;
                }
                if (line.IsIncrement)
                {
                    left.CurrentDateTimeValue = left.CurrentDateTimeValue.AddTicks(right.Ticks);
                }
                if (line.IsDecrement)
                {
                    left.CurrentDateTimeValue = left.CurrentDateTimeValue.AddTicks(right.Ticks);
                }
                if (line.IsRandomized)
                {
                    left.CurrentDateTimeValue = new DateTime(RandNums.GetRandomTimespan(right.Ticks).Ticks);
                }
            }
            else if (line.IsNumber)
            {
                int right = 0;
                if (rightVar != null) right = parent.GetVarById(line.TargetVar.LinkedVarId).CurrentNumberValue;
                else right = line.NumberValue;
                if (line.IsSet)
                    left.CurrentNumberValue = right;
                else if (line.IsIncrement)
                    left.CurrentNumberValue += right;
                else if (line.IsDecrement)
                    left.CurrentNumberValue -= right;
                else if (line.IsMultiply)
                    left.CurrentNumberValue *= right;
                else if (line.IsDivide)
                    left.CurrentNumberValue /= right;
                else if (line.IsRemainder)
                    left.CurrentNumberValue %= right;
                else if (line.IsRandomized)
                    left.CurrentNumberValue = RandNums.GetNextRandomInt(right);
            }
            else if (line.IsString)
            {
                string right = "";
                if (rightVar != null) right = parent.GetVarById(line.TargetVar.LinkedVarId).CurrentStringValue;
                else right = line.StringValue;
                left.CurrentStringValue = right;
            }
            else if (line.IsItem)
            {

                ItemInstance right = null;
                if (rightVar != null) right = parent.GetVarById(line.TargetVar.LinkedVarId).CurrentItemValue;
                else
                {
                    if (line.ItemValue != null && line.ItemValue.LinkedItem != null)
                    {
                        right = new ItemInstance(line.ItemValue.LinkedItem);
                        left.CurrentItemValue = right;
                    }
                    else
                    {
                        MainViewModel.WriteText("ERROR: Item " + line.ItemValue.LinkedItemId + " Unknown!", this.parent);
                        return false;
                    }
                }

                //if (rightVar == null)
                //{
                //    if (line.ItemValue != null && line.ItemValue.LinkedItem != null)
                //    {
                //        ItemInstance right = new ItemInstance(line.ItemValue.LinkedItem);
                //        left.CurrentItemValue = right;
                //    }
                //    else
                //    {
                //        MainViewModel.WriteText("ERROR: Item " + line.ItemValue.LinkedItemId + " Unknown!");
                //        return false;
                //    }
                //}
                //else
                //{
                //    left.CurrentItemValue = rightVar.CurrentItemValue;
                //}

            }
            else if (line.IsCommonEventRef)
            {
                CommonEventRef right = null;
                if (rightVar != null) right = parent.GetVarById(line.TargetVar.LinkedVarId).CurrentCommonEventValue;
                else
                {
                    right = line.CommonEventValue;
                }
                left.CurrentCommonEventValue = right;
            }
            return null;
        }
    }
}
