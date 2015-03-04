using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Conditions
{
    public class Conditional : Brancher, INotifyPropertyChanged
    {

        private void ClearAllButMe(string property)
        {
            if (property != IsEqualsPropertyName)
                this.IsEquals = false;
            if (property != IsNotEqualsPropertyName)
                this.IsNotEquals = false;
            if (property != IsGreaterThanPropertyName)
                this.IsGreaterThan = false;
            if (property != IsGreaterThanOrEqualToPropertyName)
                this.IsGreaterThanOrEqualTo = false;
            if (property != IsLessThanPropertyName)
                this.IsLessThan = false;
            if (property != IsLessThanOrEqualToPropertyName)
                this.IsLessThanOrEqualTo = false;
        }

        //Condition property
        public Conditional()
        {
            this.Contents = new ObservableCollection<Script>();
            this.Contents.Add(new Script());
            //this.Contents[0].ScriptLines.Add(new Scripter.Misc.Blank());
            this.Contents.Add(new Script());
            //this.Contents[1].ScriptLines.Add(new Scripter.Misc.Blank());
            ThenStatement = this.Contents[0];
            ElseStatement = this.Contents[1];
        }

        /// <summary>
        /// The <see cref="ThenStatement" /> property's name.
        /// </summary>
        public const string ThenStatementPropertyName = "ThenStatement";

        private Script _thenStatement = null;

        /// <summary>
        /// Sets and gets the ThenStatement property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script ThenStatement
        {
            get
            {
                return _thenStatement;
            }

            set
            {
                if (_thenStatement == value)
                {
                    return;
                }

                _thenStatement = value;
                RaisePropertyChanged(ThenStatementPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ElseStatement" /> property's name.
        /// </summary>
        public const string ElseStatementPropertyName = "ElseStatement";

        private Script _elseStatement = null;

        /// <summary>
        /// Sets and gets the ElseStatement property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script ElseStatement
        {
            get
            {
                return _elseStatement;
            }

            set
            {
                if (_elseStatement == value)
                {
                    return;
                }

                _elseStatement = value;
                RaisePropertyChanged(ElseStatementPropertyName);
            }
        }


        /*START COMPARISON THINGS*/

        /// <summary>
        /// The <see cref="IsComparison" /> property's name.
        /// </summary>
        public const string IsComparisonPropertyName = "IsComparison";

        private bool _isComparison = true;

        /// <summary>
        /// Sets and gets the IsComparison property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsComparison
        {
            get
            {
                return _isComparison;
            }

            set
            {
                if (_isComparison == value)
                {
                    return;
                }

                _isComparison = value;
                RaisePropertyChanged(IsComparisonPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsComparisonToConstant" /> property's name.
        /// </summary>
        public const string IsComparisonToConstantPropertyName = "IsComparisonToConstant";

        private bool _isComparisonToConstant = true;

        /// <summary>
        /// Sets and gets the IsComparisonToConstant property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsComparisonToConstant
        {
            get
            {
                return _isComparisonToConstant;
            }

            set
            {
                if (_isComparisonToConstant == value)
                {
                    return;
                }

                _isComparisonToConstant = value;
                if (value)
                {
                    IsComparisonToVariable = false;
                }

                IsComparison = (IsComparisonToConstant || IsComparisonToVariable);
                RaisePropertyChanged(IsComparisonToConstantPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsComparisonToVariable" /> property's name.
        /// </summary>
        public const string IsComparisonToVariablePropertyName = "IsComparisonToVariable";

        private bool _isComparisonToVariable = false;

        /// <summary>
        /// Sets and gets the IsComparisonToString property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsComparisonToVariable
        {
            get
            {
                return _isComparisonToVariable;
            }

            set
            {
                if (_isComparisonToVariable == value)
                {
                    return;
                }
                if (value)
                {
                    IsComparisonToConstant = false;
                }
                _isComparisonToVariable = value;
                IsComparison = (IsComparisonToConstant || IsComparisonToVariable);
                RaisePropertyChanged(IsComparisonToVariablePropertyName);
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

                _isDateTime = value;
                RaisePropertyChanged(IsDateTimePropertyName);
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
                RaisePropertyChanged(IsNumberPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsEquals" /> property's name.
        /// </summary>
        public const string IsEqualsPropertyName = "IsEquals";

        private bool _isEquals = true;

        /// <summary>
        /// Sets and gets the IsEquals property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsEquals
        {
            get
            {
                return _isEquals;
            }

            set
            {
                if (_isEquals == value)
                {
                    return;
                }

                _isEquals = value;
                RaisePropertyChanged(IsEqualsPropertyName);
                if (value) ClearAllButMe(IsEqualsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsNotEquals" /> property's name.
        /// </summary>
        public const string IsNotEqualsPropertyName = "IsNotEquals";

        private bool _isNotEquals = false;

        /// <summary>
        /// Sets and gets the IsNotEquals property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsNotEquals
        {
            get
            {
                return _isNotEquals;
            }

            set
            {
                if (_isNotEquals == value)
                {
                    return;
                }

                _isNotEquals = value;
                RaisePropertyChanged(IsNotEqualsPropertyName);
                if (value) ClearAllButMe(IsNotEqualsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsGreaterThan" /> property's name.
        /// </summary>
        public const string IsGreaterThanPropertyName = "IsGreaterThan";

        private bool _isGreaterThan = false;

        /// <summary>
        /// Sets and gets the IsGreaterThan property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsGreaterThan
        {
            get
            {
                return _isGreaterThan;
            }

            set
            {
                if (_isGreaterThan == value)
                {
                    return;
                }

                _isGreaterThan = value;
                RaisePropertyChanged(IsGreaterThanPropertyName);
                if (value) ClearAllButMe(IsGreaterThanPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsGreaterThanOrEqualTo" /> property's name.
        /// </summary>
        public const string IsGreaterThanOrEqualToPropertyName = "IsGreaterThanOrEqualTo";

        private bool _isGreaterThanOrEqualTo = false;

        /// <summary>
        /// Sets and gets the IsGreaterThanOrEqualTo property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsGreaterThanOrEqualTo
        {
            get
            {
                return _isGreaterThanOrEqualTo;
            }

            set
            {
                if (_isGreaterThanOrEqualTo == value)
                {
                    return;
                }

                _isGreaterThanOrEqualTo = value;
                RaisePropertyChanged(IsGreaterThanOrEqualToPropertyName);
                if (value) ClearAllButMe(IsGreaterThanOrEqualToPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLessThan" /> property's name.
        /// </summary>
        public const string IsLessThanPropertyName = "IsLessThan";

        private bool _isLessThan = false;

        /// <summary>
        /// Sets and gets the IsLessThan property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsLessThan
        {
            get
            {
                return _isLessThan;
            }

            set
            {
                if (_isLessThan == value)
                {
                    return;
                }

                _isLessThan = value;
                RaisePropertyChanged(IsLessThanPropertyName);
                if (value) ClearAllButMe(IsLessThanPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLessThanOrEqualTo" /> property's name.
        /// </summary>
        public const string IsLessThanOrEqualToPropertyName = "IsLessThanOrEqualTo";

        private bool _isLessThanOrEqualTo = false;

        /// <summary>
        /// Sets and gets the IsLessThanOrEqualTo property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsLessThanOrEqualTo
        {
            get
            {
                return _isLessThanOrEqualTo;
            }

            set
            {
                if (_isLessThanOrEqualTo == value)
                {
                    return;
                }

                _isLessThanOrEqualTo = value;
                RaisePropertyChanged(IsLessThanOrEqualToPropertyName);
                if (value) ClearAllButMe(IsLessThanOrEqualToPropertyName);
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
                if (!IsEquals && !IsNotEquals)
                {
                    IsEquals = true;
                }
                _isString = value;
                RaisePropertyChanged(IsStringPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedVariable" /> property's name.
        /// </summary>
        public const string SelectedVariablePropertyName = "SelectedVariable";

        private VarRef _selectedVariable = null;

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
                if (SelectedVariable != null && SelectedVariable.LinkedVariable != null)
                {
                    IsDateTime = SelectedVariable.LinkedVariable.IsDateTime;
                    IsNumber = SelectedVariable.LinkedVariable.IsNumber;
                    IsString = SelectedVariable.LinkedVariable.IsString;
                }
                RaisePropertyChanged(SelectedVariablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="VariableToCompare" /> property's name.
        /// </summary>
        public const string VariableToComparePropertyName = "VariableToCompare";

        private VarRef _variableToCompare = null;

        /// <summary>
        /// Sets and gets the VariableToCompare property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef VariableToCompare
        {
            get
            {
                return _variableToCompare;
            }

            set
            {
                if (_variableToCompare == value)
                {
                    return;
                }

                _variableToCompare = value;
                RaisePropertyChanged(VariableToComparePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DateTimeToCompareTo" /> property's name.
        /// </summary>
        public const string DateTimeToCompareToPropertyName = "DateTimeToCompareTo";

        private DateTime _dateTimeToCompareTo = DateTime.Today;

        /// <summary>
        /// Sets and gets the DateTimeToCompareTo property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public DateTime DateTimeToCompareTo
        {
            get
            {
                return _dateTimeToCompareTo;
            }

            set
            {
                if (_dateTimeToCompareTo == value)
                {
                    return;
                }

                _dateTimeToCompareTo = value;
                RaisePropertyChanged(DateTimeToCompareToPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="NumberToCompareTo" /> property's name.
        /// </summary>
        public const string NumberToCompareToPropertyName = "NumberToCompareTo";

        private int _numberToCompareTo = 0;

        /// <summary>
        /// Sets and gets the NumberToCompareTo property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int NumberToCompareTo
        {
            get
            {
                return _numberToCompareTo;
            }

            set
            {
                if (_numberToCompareTo == value)
                {
                    return;
                }

                _numberToCompareTo = value;
                RaisePropertyChanged(NumberToCompareToPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringToCompareTo" /> property's name.
        /// </summary>
        public const string StringToCompareToPropertyName = "StringToCompareTo";

        private string _stringToCompareTo = "";

        /// <summary>
        /// Sets and gets the StringToCompareTo property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string StringToCompareTo
        {
            get
            {
                return _stringToCompareTo;
            }

            set
            {
                if (_stringToCompareTo == value)
                {
                    return;
                }

                _stringToCompareTo = value;
                RaisePropertyChanged(StringToCompareToPropertyName);
            }
        }
        public override string Plaintext
        {
            get
            {

                StringBuilder sb = new StringBuilder();
                if (IsComparison)
                {
                    sb.Append("if ");
                    if (SelectedVariable != null && SelectedVariable.LinkedVariable != null)
                    {
                        sb.Append(SelectedVariable.LinkedVariable.Name);
                    }
                    else
                    {
                        sb.Append("ERROR RESOLVING VARIABLE");
                    }
                    if (IsEquals)
                    {
                        sb.Append(" is equal to ");
                    }
                    else if (IsNotEquals)
                    {
                        sb.Append(" is not equal to ");
                    }
                    else if (IsLessThan)
                    {
                        sb.Append(" is less than ");
                    }
                    else if (IsLessThanOrEqualTo)
                    {
                        sb.Append(" is less than or equal to ");
                    }
                    else if (IsGreaterThan)
                    {
                        sb.Append(" is greater than ");
                    }
                    else if (IsGreaterThanOrEqualTo)
                    {
                        sb.Append(" is greater than or equal to ");
                    }
                    if (IsComparisonToVariable)
                    {
                        if (VariableToCompare != null && VariableToCompare.LinkedVariable != null)
                        {
                            sb.Append(VariableToCompare.LinkedVariable.Name);
                        }
                        else
                        {
                            sb.Append("ERROR RESOLVING VARIABLE");
                        }
                    }
                    else
                    {
                        if (IsDateTime)
                        {
                            sb.Append(DateTimeToCompareTo.ToString());
                        }
                        else if (IsNumber)
                        {
                            sb.Append(NumberToCompareTo.ToString());
                        }
                        else if (IsString)
                        {
                            sb.Append(StringToCompareTo);
                        }
                    }
                    sb.AppendLine();
                }
                sb.AppendLine("{");
                string[] ifLines = string.Join("\n", from a in this.Contents[0].ScriptLines where a.GetType() != typeof(Editor.Scripter.Misc.Blank) select a.Plaintext).Split('\n');
                foreach (var line in ifLines)
                {
                    var tempLine = line.Replace("\r", "");
                    if (!string.IsNullOrWhiteSpace(tempLine))
                    {
                        sb.AppendLine("  " + tempLine);
                    }
                }
                sb.Append("}");
                if (this.Contents[1].ScriptLines.Count > 1)
                {
                    sb.AppendLine();
                    sb.AppendLine("Else");
                    sb.AppendLine("{");
                    var elseLines = (string.Join("\n", from a in this.Contents[1].ScriptLines where a.GetType() != typeof(Editor.Scripter.Misc.Blank) select a.Plaintext)).Split('\n');
                    foreach (var line in elseLines)
                    {
                        var tempLine = line.Replace("\r", "");
                        if (!string.IsNullOrWhiteSpace(tempLine))
                        {
                            sb.AppendLine("  " + tempLine);
                        }
                    }
                    sb.Append("}");
                }
                return sb.ToString();
            }
        }



        public override XElement ToXML()
        {

            var conditionXml = new XElement("Condition");
            if (IsComparison)
            {
                if (IsComparisonToConstant)
                {
                    conditionXml.Add(new XElement("Type", "ComparisonToConstant"));
                }
                else if (IsComparisonToVariable)
                {
                    conditionXml.Add(new XElement("Type", "ComparisonToVariable"));
                }

                if (this.SelectedVariable != null && this.SelectedVariable.LinkedVariable != null)
                {
                    conditionXml.Add(new XElement("Left", this.SelectedVariable.LinkedVarId));
                }


                if (this.IsEquals)
                {
                    conditionXml.Add(new XElement("Comparison", "EQ"));
                }
                else if (this.IsNotEquals)
                {
                    conditionXml.Add(new XElement("Comparison", "NEQ"));
                } else if (this.IsLessThan)
                {
                    conditionXml.Add(new XElement("Comparison", "LT"));
                }
                else if (this.IsLessThanOrEqualTo)
                {
                    conditionXml.Add(new XElement("Comparison", "LTE"));
                }
                else if (this.IsGreaterThan)
                {
                    conditionXml.Add(new XElement("Comparison", "GT"));
                }
                else if (this.IsGreaterThanOrEqualTo)
                {
                    conditionXml.Add(new XElement("Comparison", "GTE"));
                }

                if (this.IsComparisonToVariable && this.VariableToCompare != null && this.VariableToCompare.LinkedVariable != null)
                {
                    conditionXml.Add(new XElement("Right", this.VariableToCompare.LinkedVarId));
                    if (this.IsDateTime)
                    {
                        conditionXml.Add(new XElement("VarType", "DateTime"));
                    }
                    else if (this.IsNumber)
                    {
                        conditionXml.Add(new XElement("VarType", "Number"));
                    }
                    else if (this.IsString)
                    {
                        conditionXml.Add(new XElement("VarType", "String"));
                    }
                }
                if (this.IsComparisonToConstant)
                {
                    if (this.IsDateTime)
                    {
                        conditionXml.Add(new XElement("Right", this.DateTimeToCompareTo.ToString()));
                        conditionXml.Add(new XElement("VarType", "DateTime"));
                    }
                    else if (this.IsNumber)
                    {
                        conditionXml.Add(new XElement("Right", this.NumberToCompareTo.ToString()));
                        conditionXml.Add(new XElement("VarType", "Number"));
                    }
                    else if (this.IsString)
                    {
                        conditionXml.Add(new XElement("Right", this.StringToCompareTo));
                        conditionXml.Add(new XElement("VarType", "String"));
                    }
                }
            }
            return new XElement("If",conditionXml,
                                     new XElement("Then", ThenStatement.ToXML()),
                                     new XElement("Else", ElseStatement.ToXML()));    
        }

        public static Conditional FromXML(XElement xml)
        {
            Conditional c = new Conditional();
            //TODO: HANDLE CONDITION
            if (xml.Element("Condition") != null)
            {
                var conditionXml = xml.Element("Condition");
                if (conditionXml.Element("Type") != null)
                {
                    if (conditionXml.Element("Type").Value == "ComparisonToConstant")
                    {
                        c.IsComparisonToConstant = true;
                    }
                    if (conditionXml.Element("Type").Value == "ComparisonToVariable")
                    {
                        c.IsComparisonToVariable = true;
                    }
                }
                if (c.IsComparison)
                {
                    if (conditionXml.Element("Left") != null)
                    {
                        c.SelectedVariable = new VarRef(new Guid(conditionXml.Element("Left").Value));
                    }
                    if (conditionXml.Element("VarType") != null)
                    {
                        switch (conditionXml.Element("VarType").Value)
                        {
                            case "DateTime":
                                c.IsDateTime = true;
                                DateTime dt;
                                if (c.IsComparisonToConstant && conditionXml.Element("Right") != null && DateTime.TryParse(conditionXml.Element("Right").Value, out dt))
                                {
                                    c.DateTimeToCompareTo = dt;
                                }
                                break;
                            case "Number":
                                c.IsNumber = true;
                                int num;
                                if (c.IsComparisonToConstant && conditionXml.Element("Right") != null && Int32.TryParse(conditionXml.Element("Right").Value, out num))
                                {
                                    c.NumberToCompareTo = num;
                                }
                                break;
                            case "String":
                                c.IsString = true;

                                if (c.IsComparisonToConstant && conditionXml.Element("Right") != null)
                                {
                                    c.StringToCompareTo = conditionXml.Element("Right").Value;
                                }
                                break;
                        }
                    }
                    if (c.IsComparisonToVariable && conditionXml.Element("Right") != null)
                    {
                        c.VariableToCompare = new VarRef(new Guid(conditionXml.Element("Right").Value));
                    }
                    if (conditionXml.Element("Comparison") != null)
                    {
                        switch (conditionXml.Element("Comparison").Value)
                        {
                            case "EQ":
                                c.IsEquals = true;
                                break;
                            case "NEQ":
                                c.IsNotEquals = true;
                                break;
                            case "GT":
                                c.IsGreaterThan = true;
                                break;
                            case "GTE":
                                c.IsGreaterThanOrEqualTo = true;
                                break;
                            case "LTE":
                                c.IsLessThanOrEqualTo = true;
                                break;
                            case "LT":
                                c.IsLessThan = true;
                                break;
                        }
                    }
                }
            }

            //Handle Then
            c.ThenStatement = Script.FromXML(xml.Element("Then").Element("Script"));
            c.Contents[0] = c.ThenStatement;

            //Handle Else
            c.ElseStatement = Script.FromXML(xml.Element("Else").Element("Script"));
            c.Contents[1] = c.ElseStatement;

            return c;
            

        }

        
    }
}
