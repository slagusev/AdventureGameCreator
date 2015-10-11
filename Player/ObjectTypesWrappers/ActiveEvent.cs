using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player.ObjectTypesWrappers
{
    class ActiveEvent
    {
        public ScriptWrapper Condition { get; set; }
        public ScriptWrapper Result { get; set; }

        public bool CheckCondition()
        {
            return Condition.Execute() == true;
        }
        public bool? Execute()
        {
            if (CheckCondition())
            {
                var res = Result.Execute();
                MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveEvents.Remove(this);
                return res;
            }
            else return null;
        }

        public XElement ToXML()
        {
            return new XElement("ActiveEvent", new XElement("Condition", Condition.ToXML()), new XElement("Result", Result.ToXML())) ;
        }
        public static ActiveEvent FromXML(XElement xml)
        {
            return new ActiveEvent()
            {
                Condition = new ScriptWrapper(Editor.Scripter.Script.FromXML(xml.Element("Condition"), null)),
                Result = new ScriptWrapper(Editor.Scripter.Script.FromXML(xml.Element("Result"), null))
            };
        }
    }
}
