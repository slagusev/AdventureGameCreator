using Editor.Scripter.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ReturnValueWrapper : ScriptLineWrapper
    {
        public ReturnValue line;
        public ReturnValueWrapper(ReturnValue rv)
        {
            line = rv;
        }
        public override bool? Execute()
        {
            this.parent.GetRoot().VariableResult = line.SelectedVariable;
            return true;
        }
    }
}
