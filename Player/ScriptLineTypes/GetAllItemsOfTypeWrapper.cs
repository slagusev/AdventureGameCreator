using Editor.Scripter.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetAllItemsOfTypeWrapper : ScriptLineWrapper
    {
        GetAllItemsOfType line;
        public GetAllItemsOfTypeWrapper(GetAllItemsOfType gal)
        {
            line = gal;
        }
        public override bool? Execute()
        {
            if (line.ArrayRef != null && line.ArrayRef.Value != null && line.ItemType != null && line.ItemType.LinkedItem != null)
            {
                var arrayRef = line.ArrayRef.Value;
                var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
                var array = game.ArraysById[line.ArrayRef.Ref];
                array.Clear();
                foreach (var a in game.PlayerInventory.Where(b => b.item.ItemID == line.ItemType.LinkedItemId))
                {
                    array.Add(a);

                }
                return null;
            }
            else
            {
                MainViewModel.WriteText("Error in Get All Items.", this.parent);
                return false;
            }
        }
    }
}
