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
    class RunCommonEventWrapper : ScriptLineWrapper
    {
        RunCommonEvent line;
        public RunCommonEventWrapper(RunCommonEvent rce)
        {
            line = rce;
        }
        public override bool? Execute()
        {
            if (line.SelectedEvent != null && line.SelectedEvent.LinkedCommonEvent != null)
            {
                var wrapper = new ScriptWrapper(line.SelectedEvent.LinkedCommonEvent.AssociatedScript);
                var result = wrapper.Execute();
                var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
                if (wrapper.VariableResult != null && line.VarRef != null && game.VarById.ContainsKey(wrapper.VariableResult.LinkedVarId) && game.VarById.ContainsKey(line.VarRef.LinkedVarId))
                {
                    var source = game.VarById[wrapper.VariableResult.LinkedVarId];
                    var dest = game.VarById[line.VarRef.LinkedVarId];
                    dest.CurrentDateTimeValue = source.CurrentDateTimeValue;
                    dest.CurrentItemValue = source.CurrentItemValue;
                    dest.CurrentNumberValue = source.CurrentNumberValue;
                    dest.CurrentStringValue = source.CurrentStringValue;
                }
                if (line.SelectedEvent.LinkedCommonEvent.EventType.Item1 == CommonEvent.ScriptTypeTrueFalse)
                    return result;
                else return null;
            }
            MainViewModel.WriteText("Error: RunCommonEvent missing associated script.");
            return false;
        }
    }
}
