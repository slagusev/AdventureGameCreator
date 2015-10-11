using Editor.Scripter.Flow;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class CreateEventWrapper : ScriptLineWrapper
    {

        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        public CreateEvent line;
        public CreateEventWrapper(CreateEvent ce)
        {
            line = ce;
        }
        public override bool? Execute()
        {
            ActiveEvent ae = new ActiveEvent();
            ae.Condition = new ScriptWrapper(line.Condition);
            ae.Result = new ScriptWrapper(line.Result);
            MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveEvents.Add(ae);
            return null;
        }
    }
}
