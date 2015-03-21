using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class SetItemPropertyWrapper : ScriptLineWrapper
    {
        SetItemProperty line;
        public SetItemPropertyWrapper(SetItemProperty sip)
        {
            line = sip;
        }
        public override bool? Execute()
        {
            var vars = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById;
            if (vars.ContainsKey(line.SourceVarRef.LinkedVarId) && vars.ContainsKey(line.VarRef.LinkedVarId))
            {
                var sourceVar = vars[line.SourceVarRef.LinkedVarId];
                var destItem = vars[line.VarRef.LinkedVarId].CurrentItemValue;
                    var propName = line.SelectedItemClassName + ":" + line.SelectedPropertyName;
                    if (destItem.Properties.ContainsKey(propName))
                    {
                        destItem.Properties[propName].CurrentDateTimeValue = sourceVar.CurrentDateTimeValue;
                        destItem.Properties[propName].CurrentNumberValue = sourceVar.CurrentNumberValue;
                        destItem.Properties[propName].CurrentStringValue = sourceVar.CurrentStringValue;
                        destItem.Properties[propName].CurrentItemValue = sourceVar.CurrentItemValue;

                    }
                    else
                    {
                        MainViewModel.WriteText("ERROR: In SetItemProperty, " + propName + " not defined!", true);
                        return false;
                    }
                
            }
            else
            {
                MainViewModel.WriteText("ERROR: In SetItemProperty, variables not defined!", true);
                return false;
            }
            return null;
        }
    }
}
