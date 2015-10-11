using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Flow
{
    public class StopProcessing : ScriptLine
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("StopProcessing");
        }

        public override string Plaintext
        {
            get
            {
                return "Stop running this script.";
            }
        }
    }
}
