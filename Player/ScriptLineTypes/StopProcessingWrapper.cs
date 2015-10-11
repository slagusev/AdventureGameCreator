using Editor.Scripter.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class StopProcessingWrapper :   ScriptLineWrapper 
    {
        public override bool? Execute()
        {
            this.parent.StopExecution = true;
            return null;
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return new StopProcessing().ToXML();
        }
    }
}
