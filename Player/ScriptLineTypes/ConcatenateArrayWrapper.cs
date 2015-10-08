using Editor.Scripter.Arrays;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ConcatenateArrayWrapper : ScriptLineWrapper
    {
        ConcatenateArray line;
        public ConcatenateArrayWrapper(ConcatenateArray ca)
        {
            line = ca;
        }

        public override bool? Execute()
        {
            var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
            if (line.ArrayRef != null && line.ArrayRef.Value != null && line.VarRef != null && line.VarRef.LinkedVariable != null)
            {
                var arrayRef = line.ArrayRef.Value;
                
                var array = game.ArraysById[line.ArrayRef.Ref];
                string result = "";
                var addComma = array.Count();
                foreach (var a in array)
                {
                    if (addComma == 1 && array.Count() != 1) result += line.FinalWord;
                    if (arrayRef.IsString) result += a.ToString();
                    if (arrayRef.IsNumber) result += a.ToString();
                    if (arrayRef.IsItem) result += ((ItemInstance)a).CurrentName;
                    addComma--;
                    if (addComma >= 1) result += line.Delimiter;
                }
                this.parent.GetVarById(line.VarRef.LinkedVarId).CurrentStringValue = result;
                return null;
            }
            else
            {
                game.VarById[line.VarRef.LinkedVarId].CurrentStringValue = "";
                MainViewModel.WriteText("Error in Concatenate rray.", this.parent);
                return false;
            }
        }
    }
}
