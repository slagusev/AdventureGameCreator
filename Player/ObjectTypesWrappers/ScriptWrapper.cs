using Editor.Scripter;
using Player.ScriptLineTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypesWrappers
{
    class ScriptWrapper
    {
        public ScriptWrapper(Script s)
        {
            ScriptBase = s;
        }
        Script ScriptBase = null;

        public bool? Execute()
        {
            foreach (var line in ScriptBase.ScriptLines)
            {
                ScriptLineWrapper currentLine = ScriptLineWrapper.GetScriptLineWrapper(line);
                if (currentLine != null)
                {
                    var result = currentLine.Execute();
                    //Stop executing the script once a return statement has been triggered.
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
