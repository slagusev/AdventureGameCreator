using Editor.Scripter.TextFunctions;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class AddTextWrapper : ScriptLineWrapper
    {
        AddText line;
        public AddTextWrapper(AddText at)
        {
            line = at;
        }


        public override bool? Execute()
        {
            parent.GetTopParent().TextResult += MainViewModel.FormatText(line.Text, this.parent) + "\n\n";
            return null;
        }
    }
}
