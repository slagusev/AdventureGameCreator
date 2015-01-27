using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Scripter.Misc
{
    class Blank : ScriptLine
    {
        public override string Plaintext
        {
            get
            {

                return "<>";
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("Blank");
        }
    }
}
