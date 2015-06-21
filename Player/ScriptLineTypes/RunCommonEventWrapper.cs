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

            if ((line.SelectedEvent != null && line.SelectedEvent.LinkedCommonEvent != null) || (line.RunFromVariable && line.VarScript != null))
            {
                try
                {
                    var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
                    ScriptWrapper wrapper;
                    string scriptType;
                    CommonEventRef cRef = null;
                    if (line.RunFromVariable)
                    {
                        if (parent.GetVarById(line.VarScript.LinkedVarId) != null)
                        {
                            CommonEventRef cer = parent.GetVarById(line.VarScript.LinkedVarId).CurrentCommonEventValue;
                            if (cer != null && cer.LinkedCommonEvent != null)
                            {
                                wrapper = new ScriptWrapper(cer.LinkedCommonEvent.AssociatedScript);
                                scriptType = cer.LinkedCommonEvent.EventType.Item1;
                                cRef = cer;
                            }
                            else
                            {
                                MainViewModel.WriteText("Error: RunCommonEvent associated variable is null.", this.parent);
                                return false;
                            }
                        }
                        else
                        {
                            MainViewModel.WriteText("Error: RunCommonEvent missing associated variable for script.", this.parent);
                            return false;
                        }
                    }
                    else
                    {
                        wrapper = new ScriptWrapper(line.SelectedEvent.LinkedCommonEvent.AssociatedScript);
                        scriptType = line.SelectedEvent.LinkedCommonEvent.EventType.Item1;
                        cRef = line.SelectedEvent;
                    }
                    if (scriptType != CommonEvent.ScriptTypeOverwriteLocals)
                    {
                        wrapper.parent = this.parent;
                        wrapper.DupeVars(this.parent);
                        wrapper.IsRootScript = true;
                    }
                    else
                    {
                        wrapper.isSubscript = true;
                        wrapper.parent = this.parent;
                    }
                    var result = wrapper.Execute();

                    if (wrapper.VariableResult != null && line.VarRef != null && parent.GetVarById(wrapper.VariableResult.LinkedVarId) != null && parent.GetVarById(line.VarRef.LinkedVarId) != null)
                    {
                        var source = wrapper.GetVarById(wrapper.VariableResult.LinkedVarId);
                        var dest = parent.GetVarById(line.VarRef.LinkedVarId);
                        dest.CurrentDateTimeValue = source.CurrentDateTimeValue;
                        dest.CurrentItemValue = source.CurrentItemValue;
                        dest.CurrentNumberValue = source.CurrentNumberValue;
                        dest.CurrentStringValue = source.CurrentStringValue;
                        dest.CurrentCommonEventValue = source.CurrentCommonEventValue;
                    }

                    if (cRef.LinkedCommonEvent.EventType.Item1 == CommonEvent.ScriptTypeTrueFalse)
                        return result;
                    else return null;
                }

                catch (Exception e)
                {
                    MainViewModel.WriteText("Unhandled Exception in " + this.line.SelectedEvent.LinkedCommonEvent.Name + ":\n\n" + e.Message, null);
                }
            }
                MainViewModel.WriteText("Error: RunCommonEvent missing associated script.", this.parent);
                return false;

        }
    }
}