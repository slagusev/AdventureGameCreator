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
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        AddText line;
        public AddTextWrapper(AddText at)
        {
            line = at;
        }


        public override bool? Execute()
        {
            parent.GetTopParent().TextResult.Add(MainViewModel.FormatText(line.Text, this.parent) + "\n");
            return null;
        }
    }
}
