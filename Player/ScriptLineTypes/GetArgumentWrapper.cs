using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor.Scripter.StatusEffects;
using Player.ObjectTypesWrappers;


namespace Player.ScriptLineTypes
{
    class GetArgumentWrapper : ScriptLineWrapper
    {
        public GetArgument line;

        public GetArgumentWrapper(GetArgument ga)
        {
            line = ga;
        }

        public override bool? Execute()
        {
            //Get the status effect
            StatusEffectWrapper statusEffect = null;
            ScriptWrapper parent = this.parent.GetTopParent();
            statusEffect = parent.CurrentStatusEffect;
            var variable = this.parent.GetVarById(line.VariableRef.LinkedVarId);
            if (line.SelectedArgument.Value.IsNumber)
            {
                var numberArg = statusEffect.numberArguments.Where(a => a.Id == line.SelectedArgument.Ref).First();
                variable.CurrentNumberValue = numberArg.NumberValue;
            }
            else
            {
                var numberArg = statusEffect.stringArguments.Where(a => a.Id == line.SelectedArgument.Ref).First();
                variable.CurrentStringValue = numberArg.StringValue;
            }
            return null;
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
    }
}
