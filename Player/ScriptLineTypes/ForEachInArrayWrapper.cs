using Editor.ObjectTypes;
using Editor.Scripter.Arrays;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ForEachInArrayWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }

        ForEachInArray line;

        public ForEachInArrayWrapper(ForEachInArray feia)
        {
            line = feia;
        }

        public override bool? Execute()
        {
            if (line.LinkedArray != null && line.LinkedVar != null && line.LinkedArray.Value != null && line.LinkedVar.LinkedVariable != null)
            {

                var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
                var linkedArray = game.ArraysById[line.LinkedArray.Ref];
                List<object> copiedArray = new List<object>();
                foreach (var a in linkedArray) copiedArray.Add(a);
                if (line.ClearArray) linkedArray.Clear();
                foreach (var a in copiedArray)
                {
                    //First copy the value
                    ScriptWrapper sw = new ScriptWrapper(line.ExecutingScript);
                    sw.parent = this.parent;
                    var variable = sw.GetVarById(line.LinkedVar.LinkedVarId);
                    if (line.LinkedArray.Value.IsCommonEvent) variable.CurrentCommonEventValue = (CommonEventRef)a;
                    if (line.LinkedArray.Value.IsString) variable.CurrentStringValue = a.ToString();
                    if (line.LinkedArray.Value.IsNumber) variable.CurrentNumberValue = (int)a;
                    if (line.LinkedArray.Value.IsItem) variable.CurrentItemValue = (ItemInstance)a;

                    //Then execute the child script

                    var res = sw.Execute();

                    //If a true or false value was returned, finish the script immediately.
                    if (res != null) return res;
                }


                return null;
            }
            else
            {
                MainViewModel.WriteText("Error in Iterate through Array script.", this.parent);
                return false;
            }
        }
    }
}
