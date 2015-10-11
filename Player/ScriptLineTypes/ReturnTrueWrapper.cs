using Editor.Scripter.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ReturnTrueWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return new ReturnTrue().ToXML();
        }
        public override bool? Execute()
        {
            return true;
        }
    }
}
