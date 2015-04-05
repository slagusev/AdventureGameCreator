using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
