using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ForceEquipWrapper: ScriptLineWrapper
    {
        ForceEquip line;
        public ForceEquipWrapper(ForceEquip fue)
        {
            line = fue;
        }
        public override bool? Execute()
        {
            //var vars = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById;
            if (line.VarRef != null && parent.GetVarById(line.VarRef.LinkedVarId) != null)
            {
                var value = parent.GetVarById(line.VarRef.LinkedVarId).CurrentItemValue;
                var items = MainViewModel.GetMainViewModelStatic().CurrentGame.TryEquipItem(value, true);
                if (!line.ThrowAwayItem)
                {
                    foreach (var a in items)
                    {
                        MainViewModel.GetMainViewModelStatic().CurrentGame.PlayerInventory.Add(a);
                        
                    }
                }
                MainViewModel.GetMainViewModelStatic().CurrentGame.RefreshAll();
                return null;
            }
            else return false;
        }
    }
}
