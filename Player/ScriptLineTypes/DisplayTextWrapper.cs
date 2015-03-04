using Editor.Scripter.TextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class DisplayTextWrapper : ScriptLineWrapper
    {

        public DisplayText line = null;
        public DisplayTextWrapper(DisplayText d)
        {
            line = d;

        }
        public override bool? Execute()
        {
            MainViewModel.WriteText(line.Text);
            return null;
        }
    }
}
