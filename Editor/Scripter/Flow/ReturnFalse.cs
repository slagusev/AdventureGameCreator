using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Flow
{
    public class ReturnFalse : ScriptLine
    {
        public override string Plaintext
        {
            get { return "Return False"; }
        }

        public override XElement ToXML()
        {
            return new XElement("ReturnFalse");
        }

    }
}
