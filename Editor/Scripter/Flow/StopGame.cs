using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Scripter.Flow
{
    public class StopGame : ScriptLine
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("StopGame");
        }
        public override string Plaintext
        {
            get
            {
                return "Game Over";
            }
        }
    }
}
