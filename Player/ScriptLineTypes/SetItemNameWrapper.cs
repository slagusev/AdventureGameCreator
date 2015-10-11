using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class SetItemNameWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
         SetItemName line;
         public SetItemNameWrapper(SetItemName gin)
        {
            line = gin;
        }

        public override bool? Execute()
        {
            var ItemVar = parent.GetVarById(line.ItemVariable.LinkedVarId);
            var StringVar = parent.GetVarById(line.StringVariable.LinkedVarId);
            if (ItemVar != null && ItemVar.CurrentItemValue != null && StringVar != null)
            {
                ItemVar.CurrentItemValue.CurrentName = StringVar.CurrentStringValue;
                return null;
            }
            else
            {
                MainViewModel.WriteText("ERROR in SetItemName: Item variable is not set or no variables selected.", this.parent, false);
                return false;
            }
        }
    }
}
