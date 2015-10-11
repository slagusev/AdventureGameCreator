using Editor.Scripter.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class AddToArrayWrapper : ScriptLineWrapper
    {
        AddToArray line = null;
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        public AddToArrayWrapper(AddToArray ata)
        {
            line = ata;
        }

        public override bool? Execute()
        {
            if (line.ArrayRef != null && line.ArrayRef.Value != null && line.VarRef != null && line.VarRef.LinkedVariable != null)
            {
                var arrayRef = line.ArrayRef.Value;
                var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
                if (arrayRef.IsCommonEvent) game.ArraysById[line.ArrayRef.Ref].Add(this.parent.GetVarById(line.VarRef.LinkedVarId).CurrentCommonEventValue);
                if (arrayRef.IsString) game.ArraysById[line.ArrayRef.Ref].Add(this.parent.GetVarById(line.VarRef.LinkedVarId).CurrentStringValue);
                if (arrayRef.IsNumber) game.ArraysById[line.ArrayRef.Ref].Add(this.parent.GetVarById(line.VarRef.LinkedVarId).CurrentNumberValue);
                if (arrayRef.IsItem) game.ArraysById[line.ArrayRef.Ref].Add(this.parent.GetVarById(line.VarRef.LinkedVarId).CurrentItemValue);
                return null;
            }
            else
            {
                MainViewModel.WriteText("Error in Add To Array.", this.parent);
                return false;
            }
            
        }
    }
}
