using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetCurrentItemWrapper : ScriptLineWrapper
    {
        GetCurrentItem line;
        public GetCurrentItemWrapper(GetCurrentItem gci)
        {
            line = gci;
            
        }
        public override bool? Execute()
        {
            var vars = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById;
            if (vars.ContainsKey(line.VarRef.LinkedVarId))
            {
                vars[line.VarRef.LinkedVarId].CurrentItemValue = this.parent.ItemBase;
            }
            return null;
        }
    }
}
