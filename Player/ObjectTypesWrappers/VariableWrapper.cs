using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player.ObjectTypesWrappers
{
    class VariableWrapper
    {
        public VariableWrapper(Variable v)
        {
            VariableBase = v;
            if (v.IsDateTime) CurrentDateTimeValue = v.DefaultDateTime;
            else if (v.IsNumber) CurrentNumberValue = v.DefaultNumber;
            else if (v.IsString) CurrentStringValue = v.DefaultString;
            else if (v.IsCommonEventRef) CurrentCommonEventValue = null;
            else if (v.IsItem) CurrentItemValue = null;
        }
        public override string ToString()
        {
            if (VariableBase.IsDateTime) return CurrentDateTimeValue.ToString();
            if (VariableBase.IsNumber) return CurrentNumberValue.ToString();
            if (VariableBase.IsString) return CurrentStringValue.ToString();
            if (VariableBase.IsItem) return CurrentItemValue.ToString();
            return "";
        }
        public Variable VariableBase  { get; set; }
        public DateTime CurrentDateTimeValue { get; set; }
        public int CurrentNumberValue { get; set; }
        public string CurrentStringValue { get; set; }
        public CommonEventRef CurrentCommonEventValue { get; set; }
        public ItemInstance CurrentItemValue { get; set; }

        internal XElement ToXML()
        {
            return new XElement("Variable", "Id",
                new XElement("CurrentNumberValue", CurrentNumberValue),
                new XElement("CurrentStringValue", CurrentStringValue),
                new XElement("CurrentCommonEventValue", CurrentCommonEventValue != null ?  CurrentCommonEventValue.LinkedCommonEventId : Guid.Empty),
                new XElement("CurrentItemValue", CurrentItemValue != null ? CurrentItemValue.ToXML() : null));
        }

        internal static VariableWrapper FromXML(XElement xml, Game g, Variable baseVar)
        {
            return new VariableWrapper(baseVar)
            {
                CurrentNumberValue = Convert.ToInt32(xml.Element("CurrentNumberValue").Value),
                CurrentStringValue = xml.Element("CurrentStringValue").Value,
                CurrentCommonEventValue = new CommonEventRef(Guid.Parse(xml.Element("CurrentCommonEventValue").Value)),
                CurrentItemValue = xml.Element("CurrentItemValue").Value != null && xml.Element("CurrentItemValue").Value != "" ? ItemInstance.FromXML(xml.Element("CurrentItemValue"), g) : null
            };
        }
    }
}
