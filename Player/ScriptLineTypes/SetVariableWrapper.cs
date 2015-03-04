﻿using Editor.Scripter.Flow;
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
            if (g.VarById.ContainsKey(line.SelectedVariable.LinkedVarId))
            {
                left = g.VarById[line.SelectedVariable.LinkedVarId];
            }
            else
            {
                MainViewModel.WriteText("ERROR: Can not find variable with ID " + line.SelectedVariable.LinkedVarId + ". Terminating script.");
                return false;
            }
            VariableWrapper rightVar = null;
            if (line.IsVariable)
            {
                if (g.VarById.ContainsKey(line.TargetVar.LinkedVarId))
                {
                   
                }
                else
                {
                    MainViewModel.WriteText("ERROR: Can not find variable with ID " + line.SelectedVariable.LinkedVarId + ". Terminating script.");
                    return false;
                }
            }
            if (line.IsDateTime)
            {
                DateTime right = DateTime.MinValue;
                if (rightVar != null) right = g.VarById[line.TargetVar.LinkedVarId].CurrentDateTimeValue;
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
                if (rightVar != null) right = g.VarById[line.TargetVar.LinkedVarId].CurrentNumberValue;
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
                if (rightVar != null) right = g.VarById[line.TargetVar.LinkedVarId].CurrentStringValue;
                else right = line.StringValue;
                left.CurrentStringValue = right;
            }
            return null;
        }
    }
}