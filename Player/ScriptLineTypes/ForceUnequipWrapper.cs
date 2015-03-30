using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ForceUnequipWrapper : ScriptLineWrapper
    {
        ForceUnequip line;
        public ForceUnequipWrapper(ForceUnequip fue)
        {
            line = fue;
        }
        public override bool? Execute()
        {
            var item = MainViewModel.GetMainViewModelStatic().CurrentGame.EquippedItems[line.Slot];
            if (item != null)
            {
                MainViewModel.GetMainViewModelStatic().CurrentGame.TryUnequipItem(item, true);
                if (!line.ThrowAwayItem)
                    MainViewModel.GetMainViewModelStatic().CurrentGame.PlayerInventory.Add(item);
                MainViewModel.GetMainViewModelStatic().CurrentGame.RefreshAll();
            }
            return null;
        }
    }
}
