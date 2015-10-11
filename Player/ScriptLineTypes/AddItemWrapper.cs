using Editor.Scripter.ItemManagement;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class AddItemWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return AddItemLine.ToXML();
        }
        public AddItemToInventory AddItemLine = null;
        public AddItemWrapper(AddItemToInventory addItem)
        {
            AddItemLine = addItem;
        }
        public override bool? Execute()
        {
            var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
            if (!AddItemLine.FromVariable)
            {
                game.PlayerInventory.Add(new ItemInstance(AddItemLine.ItemReference.LinkedItem));
            }
            else
            {
                var item = parent.GetVarById(AddItemLine.VarRef.LinkedVarId).CurrentItemValue;
                if (item != null && !game.PlayerInventory.Contains(item))
                {
                    //Don't re-add the item if it already is in the inventory
                    game.PlayerInventory.Add(item);
                }
            }
            return null;
        }
    }
}
