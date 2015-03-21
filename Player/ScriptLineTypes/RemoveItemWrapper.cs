using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class RemoveItemWrapper : ScriptLineWrapper
    {
        RemoveItem line;

        public RemoveItemWrapper(RemoveItem ri)
        {
            line = ri;
        }

        public override bool? Execute()
        {
            var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
            if (line.VarRef != null && line.VarRef.LinkedVariable != null && game.VarById.ContainsKey(line.VarRef.LinkedVarId))
            {
                var v = game.VarById[line.VarRef.LinkedVarId];
                if (v != null && v.CurrentItemValue != null)
                {
                    if (game.PlayerInventory.Contains(v.CurrentItemValue))
                        game.PlayerInventory.Remove(v.CurrentItemValue);
                }
                else
                {
                    MainViewModel.WriteText("ERROR: Variable for RemoveItem has never been set.");
                    return false;
                }
            }
            else
            {
                MainViewModel.WriteText("ERROR: Could not find variable for RemoveItem.");
                return false;
            }
            return null;
        }
    }
}
