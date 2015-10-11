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
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        public DisplayText line = null;
        public DisplayTextWrapper(DisplayText d)
        {
            line = d;

        }
        public override bool? Execute()
        {
            MainViewModel.WriteText(line.Text, this.parent);
            return null;
        }
    }
}
