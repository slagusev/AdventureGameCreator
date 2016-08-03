using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Scripter.StatusEffects
{
    public class CheckIfEffectsResolved : ScriptLine
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("CheckIfEffectsResolved");
        }
        public override string Plaintext
        {
            get
            {
                return "Check if status effects have resolved.";
            }
        }
    }
}
