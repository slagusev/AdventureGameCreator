using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetItemNameWrapper : ScriptLineWrapper
    {
        GetItemName line;
        public GetItemNameWrapper(GetItemName gin)
        {
            line = gin;
        }

        public override bool? Execute()
        {
            var ItemVar = parent.GetVarById(line.ItemVariable.LinkedVarId);
            var StringVar = parent.GetVarById(line.StringVariable.LinkedVarId);
            if (ItemVar != null && ItemVar.CurrentItemValue != null && StringVar != null)
            {
                if (line.UseDefaultName)
                {
                    StringVar.CurrentStringValue = ItemVar.CurrentItemValue.item.DefaultName;
                }
                else
                {
                    StringVar.CurrentStringValue = ItemVar.CurrentItemValue.CurrentName;
                }
                return null;
            }
            else
            {
                MainViewModel.WriteText("ERROR in GetItemName: Item variable is not set or no variables selected.", this.parent, false);
                return false;
            }
        }
    }
}
