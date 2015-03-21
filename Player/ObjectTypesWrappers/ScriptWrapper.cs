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
        public ItemInstance ItemBase = null;
        public string TextResult { get; set; }
        public bool? Execute()
        {
            TextResult = "";
            foreach (var line in ScriptBase.ScriptLines)
            {
                ScriptLineWrapper currentLine = ScriptLineWrapper.GetScriptLineWrapper(line, this);
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

        public ScriptWrapper parent = null;
        public ScriptWrapper GetTopParent()
        {
            var lastParent = this;
            while (lastParent.parent != null)
            {
                lastParent = lastParent.parent;
            }
            return lastParent;
        }
    }
}
