using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Flow
{
    public class SetVariable : ScriptLine
    {
        /// <summary>
        /// The <see cref="SelectedVariable" /> property's name.
        /// </summary>
        public const string SelectedVariablePropertyName = "SelectedVariable";

        private VarRef _selectedVariable = new VarRef(Guid.Empty);

        /// <summary>
        /// Sets and gets the SelectedVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef SelectedVariable
        {
            get
            {
                return _selectedVariable;
            }

            set
            {
                if (_selectedVariable == value)
                {
                    return;
                }
                
                _selectedVariable = value;
                RaisePropertyChanged(SelectedVariablePropertyName);
                if (SelectedVariable != null && SelectedVariable.LinkedVariable != null)
                {
                    this.IsDateTime = false;
                    this.IsNumber = false;
                    this.IsString = false;
                    this.IsItem = false;
                    this.IsCommonEventRef = false;
                    if (SelectedVariable.LinkedVariable.IsDateTime)
                        this.IsDateTime = true;
                    else if (SelectedVariable.LinkedVariable.IsNumber)
                        this.IsNumber = true;
                    else if (SelectedVariable.LinkedVariable.IsString)
                        this.IsString = true;
                    else if (SelectedVariable.LinkedVariable.IsItem)
                        this.IsItem = true;
                    else if (SelectedVariable.LinkedVariable.IsCommonEventRef)
                        this.IsCommonEventRef = true;
                    //SelectedVariable.PropertyChanged += this.PropertyChanged;
                }
                
            }
        }

        /// <summary>
        /// The <see cref="IsNumber" /> property's name.
        /// </summary>
        public const string IsNumberPropertyName = "IsNumber";

        private bool _isNumber = false;

        /// <summary>
        /// Sets and gets the IsNumber property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsNumber
        {
            get
            {
                return _isNumber;
            }

            set
            {
                if (_isNumber == value)
                {
                    return;
                }

                _isNumber = value;
                if (value)
                {
                    IsDateTimeStatic = false;
                    IsNumberStatic = IsStatic;
                    IsStringStatic = false;

                }
                RaisePropertyChanged(IsNumberPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsItem" /> property's name.
        /// </summary>
        public const string IsItemPropertyName = "IsItem";

        private bool _isItem = false;

        /// <summary>
        /// Sets and gets the IsItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsItem
        {
            get
            {
                return _isItem;
            }

            set
            {
                if (_isItem == value)
                {
                    return;
                }
                if (value)
                    IsSet = true;
                _isItem = value;
                RaisePropertyChanged(IsItemPropertyName);
                if (value)
                {
                    IsDateTimeStatic = false;
                    IsNumberStatic = false;
                    IsStringStatic = false;
                }
            }
        }
        /// <summary>
        /// The <see cref="IsCommonEventRef" /> property's name.
        /// </summary>
        public const string IsCommonEventRefPropertyName = "IsCommonEventRef";

        private bool _isCommonEventRef = false;

        /// <summary>
        /// Sets and gets the IsCommonEventRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsCommonEventRef
        {
            get
            {
                return _isCommonEventRef;
            }

            set
            {
                if (_isCommonEventRef == value)
                {
                    return;
                }

                _isCommonEventRef = value;
                RaisePropertyChanged(IsCommonEventRefPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ItemValue" /> property's name.
        /// </summary>
        public const string ItemValuePropertyName = "ItemValue";

        private ItemRef _itemValue = null;

        /// <summary>
        /// Sets and gets the ItemValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemRef ItemValue
        {
            get
            {
                return _itemValue;
            }

            set
            {
                if (_itemValue == value)
                {
                    return;
                }

                _itemValue = value;
                RaisePropertyChanged(ItemValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CommonEventValue" /> property's name.
        /// </summary>
        public const string CommonEventValuePropertyName = "CommonEventValue";

        private CommonEventRef _commonEventValue = null;

        /// <summary>
        /// Sets and gets the CommonEventValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CommonEventRef CommonEventValue
        {
            get
            {
                return _commonEventValue;
            }

            set
            {
                if (_commonEventValue == value)
                {
                    return;
                }

                _commonEventValue = value;
                RaisePropertyChanged(CommonEventValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsDateTime" /> property's name.
        /// </summary>
        public const string IsDateTimePropertyName = "IsDateTime";

        private bool _isDateTime = false;

        /// <summary>
        /// Sets and gets the IsDateTime property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsDateTime
        {
            get
            {
                return _isDateTime;
            }

            set
            {
                if (_isDateTime == value)
                {
                    return;
                }
                
                if (value && (IsMultiply || IsDivide || IsRemainder))
                    IsSet = true;
                _isDateTime = value;
                if (value)
                {
                    IsDateTimeStatic = IsStatic;
                    IsNumberStatic = false;
                    IsStringStatic = false;
                    
                }
                
                RaisePropertyChanged(IsDateTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsString" /> property's name.
        /// </summary>
        public const string IsStringPropertyName = "IsString";

        private bool _isString = false;

        /// <summary>
        /// Sets and gets the IsString property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsString
        {
            get
            {
                return _isString;
            }

            set
            {
                if (_isString == value)
                {
                    return;
                }
                if (value)
                    IsSet = true;
                _isString = value;
                if (value)
                {
                    IsDateTimeStatic = false;
                    IsNumberStatic = false;
                    IsStringStatic = IsStatic;
                }
                RaisePropertyChanged(IsStringPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsSet" /> property's name.
        /// </summary>
        public const string IsSetPropertyName = "IsSet";

        private bool _isSet = true;

        /// <summary>
        /// Sets and gets the IsSet property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsSet
        {
            get
            {
                return _isSet;
            }

            set
            {
                if (_isSet == value)
                {
                    return;
                }

                _isSet = value;
                if (value)
                {
                    //IsSet = false;
                    IsIncrement = false;
                    IsDecrement = false;
                    IsMultiply = false;
                    IsDivide = false;
                    IsRemainder = false;
                    IsRandomized = false;
                }
                RaisePropertyChanged(IsSetPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsIncrement" /> property's name.
        /// </summary>
        public const string IsIncrementPropertyName = "IsIncrement";

        private bool _isIncrement = false;

        /// <summary>
        /// Sets and gets the IsIncrement property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsIncrement
        {
            get
            {
                return _isIncrement;
            }

            set
            {
                if (_isIncrement == value)
                {
                    return;
                }
                if (value)
                {
                    IsSet = false;
                    //IsIncrement = false;
                    IsDecrement = false;
                    IsMultiply = false;
                    IsDivide = false;
                    IsRemainder = false;
                    IsRandomized = false;
                }
                _isIncrement = value;
                RaisePropertyChanged(IsIncrementPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsDecrement" /> property's name.
        /// </summary>
        public const string IsDecrementPropertyName = "IsDecrement";

        private bool _isDecrement = false;

        /// <summary>
        /// Sets and gets the IsDecrement property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsDecrement
        {
            get
            {
                return _isDecrement;
            }

            set
            {
                if (_isDecrement == value)
                {
                    return;
                }
                if (value)
                {
                    IsSet = false;
                    IsIncrement = false;
                    //IsDecrement = false;
                    IsMultiply = false;
                    IsDivide = false;
                    IsRemainder = false;
                    IsRandomized = false;
                }
                _isDecrement = value;
                RaisePropertyChanged(IsDecrementPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsMultiply" /> property's name.
        /// </summary>
        public const string IsMultiplyPropertyName = "IsMultiply";

        private bool _isMultiply = false;

        /// <summary>
        /// Sets and gets the IsMultiply property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsMultiply
        {
            get
            {
                return _isMultiply;
            }

            set
            {
                if (_isMultiply == value)
                {
                    return;
                }
                if (value)
                {
                    IsSet = false;
                    IsIncrement = false;
                    IsDecrement = false;
                    //IsMultiply = false;
                    IsDivide = false;
                    IsRemainder = false;
                    IsRandomized = false;
                }
                _isMultiply = value;
                RaisePropertyChanged(IsMultiplyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsRandomized" /> property's name.
        /// </summary>
        public const string IsRandomizedPropertyName = "IsRandomized";

        private bool _isRandomized = false;

        /// <summary>
        /// Sets and gets the IsRandomized property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsRandomized
        {
            get
            {
                return _isRandomized;
            }

            set
            {
                if (_isRandomized == value)
                {
                    return;
                }

                _isRandomized = value;
                RaisePropertyChanged(IsRandomizedPropertyName);
                if (value)
                {
                    IsSet = false;
                    IsIncrement = false;
                    IsDecrement = false;
                    IsMultiply = false;
                    IsDivide = false;
                    IsRemainder = false;
                    //IsRandomized = false;
                }
            }
        }
        /// <summary>
        /// The <see cref="IsDivide" /> property's name.
        /// </summary>
        public const string IsDividePropertyName = "IsDivide";

        private bool _isDivide = false;

        /// <summary>
        /// Sets and gets the IsDivide property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsDivide
        {
            get
            {
                return _isDivide;
            }

            set
            {
                if (_isDivide == value)
                {
                    return;
                }

                _isDivide = value;
                if (value)
                {
                    IsSet = false;
                    IsIncrement = false;
                    IsDecrement = false;
                    IsMultiply = false;
                    //IsDivide = false;
                    IsRemainder = false;
                    IsRandomized = false;
                }
                RaisePropertyChanged(IsDividePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsRemainder" /> property's name.
        /// </summary>
        public const string IsRemainderPropertyName = "IsRemainder";

        private bool _isRemainder = false;

        /// <summary>
        /// Sets and gets the IsRemainder property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsRemainder
        {
            get
            {
                return _isRemainder;
            }

            set
            {
                if (_isRemainder == value)
                {
                    return;
                }

                _isRemainder = value;
                if (value)
                {
                    IsSet = false;
                    IsIncrement = false;
                    IsDecrement = false;
                    IsMultiply = false;
                    IsDivide = false;
                    //IsRemainder = false;
                    IsRandomized = false;
                }
                RaisePropertyChanged(IsRemainderPropertyName);
            }
        }
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void RaisePropertyChanged(String propertyName = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        /// <summary>
        /// The <see cref="IsStatic" /> property's name.
        /// </summary>
        public const string IsStaticPropertyName = "IsStatic";

        private bool _isStatic = true;

        /// <summary>
        /// Sets and gets the IsStatic property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return _isStatic;
            }

            set
            {
                if (_isStatic == value)
                {
                    return;
                }

                _isStatic = value;
                if (value)
                {
                    IsDateTimeStatic = IsDateTime;
                    IsNumberStatic = IsNumber;
                    IsStringStatic = IsString;
                    IsVariable = false;
                }
                else
                {
                    IsDateTimeStatic = IsNumberStatic = IsStringStatic = false;
                }
                RaisePropertyChanged(IsStaticPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsVariable" /> property's name.
        /// </summary>
        public const string IsVariablePropertyName = "IsVariable";

        private bool _isVariable = false;

        /// <summary>
        /// Sets and gets the IsVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsVariable
        {
            get
            {
                return _isVariable;
            }

            set
            {
                if (_isVariable == value)
                {
                    return;
                }
                if (value)
                {
                    IsStatic = false;
                }
                _isVariable = value;
                RaisePropertyChanged(IsVariablePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsDateTimeStatic" /> property's name.
        /// </summary>
        public const string IsDateTimeStaticPropertyName = "IsDateTimeStatic";

        private bool _isDateTimeStatic = false;

        /// <summary>
        /// Sets and gets the IsDateTimeStatic property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsDateTimeStatic
        {
            get
            {
                return _isDateTimeStatic;
            }

            set
            {
                if (_isDateTimeStatic == value)
                {
                    return;
                }

                _isDateTimeStatic = value;
                RaisePropertyChanged(IsDateTimeStaticPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsNumberStatic" /> property's name.
        /// </summary>
        public const string IsNumberStaticPropertyName = "IsNumberStatic";

        private bool _isNumberStatic = false;

        /// <summary>
        /// Sets and gets the IsNumberStatic property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsNumberStatic
        {
            get
            {
                return _isNumberStatic;
            }

            set
            {
                if (_isNumberStatic == value)
                {
                    return;
                }

                _isNumberStatic = value;
                RaisePropertyChanged(IsNumberStaticPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsStringStatic" /> property's name.
        /// </summary>
        public const string IsStringStaticPropertyName = "IsStringStatic";

        private bool _isStringStatic = false;

        /// <summary>
        /// Sets and gets the IsStringStatic property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsStringStatic
        {
            get
            {
                return _isStringStatic;
            }

            set
            {
                if (_isStringStatic == value)
                {
                    return;
                }

                _isStringStatic = value;
                RaisePropertyChanged(IsStringStaticPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Days" /> property's name.
        /// </summary>
        public const string DaysPropertyName = "Days";

        private int _days = 0;

        /// <summary>
        /// Sets and gets the Days property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Days
        {
            get
            {
                return _days;
            }

            set
            {
                if (_days == value)
                {
                    return;
                }

                _days = value;
                RaisePropertyChanged(DaysPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Hours" /> property's name.
        /// </summary>
        public const string HoursPropertyName = "Hours";

        private int _hours = 0;

        /// <summary>
        /// Sets and gets the Hours property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Hours
        {
            get
            {
                return _hours;
            }

            set
            {
                if (_hours == value)
                {
                    return;
                }

                _hours = value;
                RaisePropertyChanged(HoursPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Minutes" /> property's name.
        /// </summary>
        public const string MinutesPropertyName = "Minutes";

        private int _minutes = 0;

        /// <summary>
        /// Sets and gets the Minutes property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Minutes
        {
            get
            {
                return _minutes;
            }

            set
            {
                if (_minutes == value)
                {
                    return;
                }

                _minutes = value;
                RaisePropertyChanged(MinutesPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Seconds" /> property's name.
        /// </summary>
        public const string SecondsPropertyName = "Seconds";

        private int _seconds = 0;

        /// <summary>
        /// Sets and gets the Seconds property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Seconds
        {
            get
            {
                return _seconds;
            }

            set
            {
                if (_seconds == value)
                {
                    return;
                }

                _seconds = value;
                RaisePropertyChanged(SecondsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="NumberValue" /> property's name.
        /// </summary>
        public const string NumberValuePropertyName = "NumberValue";

        private int _numberValue = 0;

        /// <summary>
        /// Sets and gets the NumberValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int NumberValue
        {
            get
            {
                return _numberValue;
            }

            set
            {
                if (_numberValue == value)
                {
                    return;
                }

                _numberValue = value;
                RaisePropertyChanged(NumberValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringValue" /> property's name.
        /// </summary>
        public const string StringValuePropertyName = "StringValue";

        private string _stringValue = "";

        /// <summary>
        /// Sets and gets the StringValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string StringValue
        {
            get
            {
                return _stringValue;
            }

            set
            {
                if (_stringValue == value)
                {
                    return;
                }

                _stringValue = value;
                RaisePropertyChanged(StringValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="TargetVar" /> property's name.
        /// </summary>
        public const string TargetVarPropertyName = "TargetVar";

        private VarRef _targetVar = null;

        /// <summary>
        /// Sets and gets the TargetVar property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef TargetVar
        {
            get
            {
                return _targetVar;
            }

            set
            {
                if (_targetVar == value)
                {
                    return;
                }

                _targetVar = value;
                RaisePropertyChanged(TargetVarPropertyName);
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            XElement xml = new XElement("SetVariable");
            xml.Add(new XElement("SourceVarRef", SelectedVariable.LinkedVarId));
            string type = "";
            if (IsDateTime)
                type = "DateTime";
            if (IsNumber)
                type = "Number";
            if (IsString)
                type = "String";
            if (IsItem)
                type = "Item";
            if (IsCommonEventRef)
                type = "CommonEventRef";
            xml.Add(new XElement("Type", type));
            string action = "";
            if (IsSet)
                action = "Set";
            if (IsIncrement)
                action = "Increment";
            if (IsDecrement)
                action = "Decrement";
            if (IsMultiply)
                action = "Multiply";
            if (IsDivide)
                action = "Divide";
            if (IsRemainder)
                action = "Remainder";
            if (IsRandomized)
                action = "Randomized";
            xml.Add(new XElement("Action", action));

            if (IsVariable)
            {
                xml.Add(new XElement("ValueType", "Variable"));
                xml.Add(new XElement("TargetVarRef", TargetVar.LinkedVarId));
            }
            else
            {
                xml.Add(new XElement("ValueType", "Static"));
                if (IsDateTime)
                {
                    xml.Add(new XElement("Days", Days));
                    xml.Add(new XElement("Hours", Hours));
                    xml.Add(new XElement("Minutes", Minutes));
                    xml.Add(new XElement("Seconds", Seconds));
                }
                else if (IsNumber)
                    xml.Add(new XElement("Value", NumberValue));
                else if (IsString)
                    xml.Add(new XElement("Value", StringValue));
                else if (IsItem)
                    xml.Add(new XElement("Value", ItemValue != null && ItemValue.LinkedItemId != null ? ItemValue.LinkedItemId : Guid.Empty));
                else if (IsCommonEventRef)
                    xml.Add(new XElement("Value", CommonEventValue != null && CommonEventValue.LinkedCommonEventId != null ? CommonEventValue.LinkedCommonEventId: Guid.Empty));

            }
            

            return xml;
        }

        public static SetVariable FromXML(XElement xml)
        {
            SetVariable sv = new SetVariable();
            if (xml.Element("SourceVarRef") != null)
            {
                sv.SelectedVariable = new VarRef(Guid.Parse(xml.Element("SourceVarRef").Value));
            }
            if (xml.Element("Type") != null)
            {
                switch (xml.Element("Type").Value)
                {
                    case "DateTime":
                        sv.IsDateTime = true;
                        break;
                    case "Number":
                        sv.IsNumber = true;
                        break;
                    case "String":
                        sv.IsString = true;
                        break;
                    case "Item":
                        sv.IsItem = true;
                        break;
                    case "CommonEventRef":
                        sv.IsCommonEventRef = true;
                        break;
                }
            }
            if (xml.Element("Action") != null)
            {
                switch (xml.Element("Action").Value)
                {
                    case "Set":
                        sv.IsSet = true;
                        break;
                    case "Increment":
                        sv.IsIncrement = true;
                        break;
                    case "Decrement":
                        sv.IsDecrement = true;
                        break;
                    case "Multiply":
                        sv.IsMultiply = true;
                        break;
                    case "Divide":
                        sv.IsDivide = true;
                        break;
                    case "Remainder":
                        sv.IsRemainder = true;
                        break;
                    case "Randomized":
                        sv.IsRandomized = true;
                        break;
                }
            }
            if (xml.Element("ValueType") != null)
            {
                if (xml.Element("ValueType").Value == "Variable")
                {
                    if (xml.Element("SourceVarRef") != null)
                    {
                        sv.IsVariable = true;
                        sv.TargetVar = new VarRef(Guid.Parse(xml.Element("TargetVarRef").Value));
                    }
                }
                else
                {
                    if (sv.IsDateTime)
                    {
                        if (xml.Element("Days") != null)
                        {
                            sv.Days = Convert.ToInt32(xml.Element("Days").Value);
                        }
                        if (xml.Element("Minutes") != null)
                        {
                            sv.Minutes = Convert.ToInt32(xml.Element("Minutes").Value);
                        }
                        if (xml.Element("Hours") != null)
                        {
                            sv.Hours = Convert.ToInt32(xml.Element("Hours").Value);
                        }
                        if (xml.Element("Seconds") != null)
                        {
                            sv.Seconds = Convert.ToInt32(xml.Element("Seconds").Value);
                        }
                    }
                    else if (sv.IsNumber)
                    {
                        if (xml.Element("Value") != null)
                        {
                            sv.NumberValue = Convert.ToInt32(xml.Element("Value").Value);
                        }
                    }
                    else if (sv.IsString)
                    {
                        if (xml.Element("Value") != null)
                        {
                            sv.StringValue = xml.Element("Value").Value;
                        }
                    }
                    else if (sv.IsItem)
                    {
                        if (xml.Element("Value") != null)
                        {
                            sv.ItemValue = new ItemRef(Guid.Parse(xml.Element("Value").Value));
                        }
                    }
                    else if (sv.IsCommonEventRef)
                    {
                        if (xml.Element("Value") != null)
                        {
                            sv.CommonEventValue = new CommonEventRef(Guid.Parse(xml.Element("Value").Value));
                        }
                    }
                }
            }

            return sv;
        }
        public override string Plaintext
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (IsSet)
                    sb.Append("Set ");
                else if (IsIncrement)
                    sb.Append("Increment ");
                else if (IsDecrement)
                    sb.Append("Decrement ");
                else if (IsMultiply)
                    sb.Append("Multiply ");
                else if (IsDivide)
                    sb.Append("Divide ");
                else if (IsRemainder)
                    sb.Append("Set ");
                else if (IsRandomized)
                    sb.Append("Set ");
                if (SelectedVariable != null && SelectedVariable.LinkedVariable != null)
                {
                    sb.Append(SelectedVariable.LinkedVariable.Name + " ");
                }
                if (IsSet)
                {
                    sb.Append("to ");
                }
                else if (IsRemainder)
                {
                    sb.Append("to the remainder of itself divided by ");
                }
                else if (IsRandomized)
                {
                    sb.Append("to a random value of up to ");
                }
                else
                {
                    sb.Append("by ");
                }
                if (IsStatic)
                {
                    if (IsDateTime)
                    {
                        sb.Append(Days + " days, " + Hours + " hours, " + Minutes + " minutes, and " + Seconds + " seconds");
                    }
                    else if (IsNumber)
                    {
                        sb.Append(NumberValue);
                    }
                    else if (IsItem)
                    {
                        sb.Append("a new instance of " + ((ItemValue != null && ItemValue.LinkedItem != null) ? ItemValue.LinkedItem.ItemName : "UNKNOWN"));
                    }
                    else if (IsCommonEventRef)
                    {
                        sb.Append("a reference to the " + ((CommonEventValue != null && CommonEventValue.LinkedCommonEvent != null) ? CommonEventValue.LinkedCommonEvent.Name : "UNKNOWN") + " common event.");
                    }
                    else
                    {
                        sb.Append(StringValue);
                    }
                }
                else if (TargetVar != null && TargetVar.LinkedVariable != null)
                {
                    sb.Append(TargetVar.LinkedVariable.Name);
                }
                return sb.ToString();
            }
            
        }
    }
}
