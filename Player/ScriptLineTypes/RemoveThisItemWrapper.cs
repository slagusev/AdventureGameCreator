using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class RemoveThisItemWrapper : ScriptLineWrapper
    {
        RemoveThisItem line;

        public RemoveThisItemWrapper(RemoveThisItem ri)
        {
            line = ri;
        }

        public override bool? Execute()
        {
            var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
            var item = parent.GetTopParent().ItemBase;
            if (game.PlayerInventory.Contains(item))
            {
                game.PlayerInventory.Remove(item);
            }
            return null;
        }
    }
}

