using Editor.Scripter.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ReturnFalseWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return new ReturnFalse().ToXML();
        }
        public override bool? Execute()
        {
            return false;
        }
    }
}
