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
        }
        public Variable VariableBase  { get; set; }
        public DateTime CurrentDateTimeValue { get; set; }
        public int CurrentNumberValue { get; set; }
        public string CurrentStringValue { get; set; }
    }
}
