using Editor.Scripter;
using Editor.Scripter.Conditions;
using Editor.Scripter.Flow;
using Editor.Scripter.Misc;
using Editor.Scripter.TextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    abstract class ScriptLineWrapper
    {
        public abstract bool? Execute();
        public static ScriptLineWrapper GetScriptLineWrapper(ScriptLine line)
        {
            var lineType = line.GetType();
            if (lineType == typeof(Comment))
            {
                var comment = new CommentWrapper((Comment)line);
                return comment;
            }
            if (lineType == typeof(Conditional))
            {
                var conditional = new ConditionalWrapper((Conditional)line);
                return conditional;
            }
            if (lineType == typeof(DisplayText))
            {
                return new DisplayTextWrapper((DisplayText)line);
            }
            if (lineType == typeof(SetVariable))
            {
                return new SetVariableWrapper((SetVariable)line);
            }
            return null;
        }
    }
}
