using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetItemPropertyWrapper : ScriptLineWrapper
    {

        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        GetItemProperty line;
        public GetItemPropertyWrapper(GetItemProperty gip)
        {
            line = gip;
        }
        public override bool? Execute()
        {
            //var vars = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById;
            //if (vars.ContainsKey(line.SourceVarRef.LinkedVarId) && vars.ContainsKey(line.VarRef.LinkedVarId))
            var sourceVar = parent.GetVarById(line.SourceVarRef.LinkedVarId);
            var destVar = parent.GetVarById(line.VarRef.LinkedVarId);
            if (sourceVar != null && destVar != null)
            {
                var sourceItem = sourceVar.CurrentItemValue;
                
                    var propName = line.SelectedItemClassName + ":" + line.SelectedPropertyName;
                    if (sourceItem.Properties.ContainsKey(propName))
                    {
                        destVar.CurrentDateTimeValue = sourceItem.Properties[propName].CurrentDateTimeValue ;
                        destVar.CurrentNumberValue = sourceItem.Properties[propName].CurrentNumberValue;
                        destVar.CurrentStringValue = sourceItem.Properties[propName].CurrentStringValue ;
                        destVar.CurrentItemValue = sourceItem.Properties[propName].CurrentItemValue;

                    }
                    else
                    {
                        MainViewModel.WriteText("ERROR: In GetItemProperty, " + propName + " not defined!", this.parent, true);
                        return false;
                    }
                
            }
            else
            {
                MainViewModel.WriteText("ERROR: In GetItemProperty, variables not defined!", this.parent, true);
                return false;
            }
            return null;
        }
    }
}
