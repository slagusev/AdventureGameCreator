using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetEquipmentSlotWrapper : ScriptLineWrapper
    {
        GetEquipmentSlot line;
        public GetEquipmentSlotWrapper(GetEquipmentSlot ges)
        {
            line = ges;
        }
        public override bool? Execute()
        {
            var vars = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById;
            if (line.VarRef != null && vars.ContainsKey(line.VarRef.LinkedVarId))
            {
                vars[line.VarRef.LinkedVarId].CurrentItemValue = MainViewModel.GetMainViewModelStatic().CurrentGame.EquippedItems[line.Slot];
                return null;
            }
            else return false;
            
        }
    }
}
