using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class PlayerStatistic : INotifyPropertyChanged
    {
        public PlayerStatistic()
        {
            DisplayCondition.CanAddItem = false;
            DisplayCondition.CanAddText = false;
            DisplayCondition.CanReturn = true;
            DisplayCondition.CanDisplayText = false;
            DisplayCondition.CanStopGame = false;
            DisplayCondition.HasTextFunctionality = false;
            DisplayCondition.IsItemScript = false;
            DisplayCondition.CanSetVariable = true;
            DisplayCondition.CanConditional = true;
            DisplayCondition.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeTrueFalse);
        }
        /// <summary>
        /// The <see cref="Label" /> property's name.
        /// </summary>
        public const string LabelPropertyName = "Label";

        private string _label = "";

        /// <summary>
        /// Sets and gets the Label property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (_label == value)
                {
                    return;
                }

                _label = value;
                RaisePropertyChanged(LabelPropertyName);
                MainViewModel.MainViewModelStatic.Settings.RaisePropertyChanged(PlayerSettings.PlayerStatisticsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DisplayCondition" /> property's name.
        /// </summary>
        public const string DisplayConditionPropertyName = "DisplayCondition";

        private Script _displayCondition = new Script();

        /// <summary>
        /// Sets and gets the DisplayCondition property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script DisplayCondition
        {
            get
            {
                return _displayCondition;
            }

            set
            {
                if (_displayCondition == value)
                {
                    return;
                }

                _displayCondition = value;
                RaisePropertyChanged(DisplayConditionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AssociatedVariable" /> property's name.
        /// </summary>
        public const string AssociatedVariablePropertyName = "AssociatedVariable";

        private VarRef _associatedVariable = null;

        /// <summary>
        /// Sets and gets the AssociatedVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef AssociatedVariable
        {
            get
            {
                return _associatedVariable;
            }

            set
            {
                if (_associatedVariable == value)
                {
                    return;
                }

                _associatedVariable = value;
                RaisePropertyChanged(AssociatedVariablePropertyName);
                if (AssociatedVariable != null && AssociatedVariable.LinkedVariable != null)
                {
                    if (!AssociatedVariable.LinkedVariable.IsNumber) this.IsPlaintext = true;
                }
            }
        }


        private void ResetAll()
        {
        }
        private bool VerifyDisplayType()
        {
            return _isPlaintext || _isProgressBar || _isBalanceBar;
        }
        private bool VerifyMinValue()
        {
            return _minimumValueConstant || _minimumValueVariable;
        }
        private bool VerifyMaxValue()
        {
            return _maximumValueConstant || _maximumValueVariable;
        }
        /// <summary>
        /// The <see cref="IsPlaintext" /> property's name.
        /// </summary>
        public const string IsPlaintextPropertyName = "IsPlaintext";

        private bool _isPlaintext = true;

        /// <summary>
        /// Sets and gets the IsPlaintext property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsPlaintext
        {
            get
            {
                return _isPlaintext;
            }

            set
            {
                if (_isPlaintext == value)
                {
                    return;
                }

                _isPlaintext = value;
                
                RaisePropertyChanged(IsPlaintextPropertyName);
                if (value)
                {
                    IsProgressBar = false;
                    IsBalanceBar = false;
                }
                if (value == false && !VerifyDisplayType()) IsPlaintext = true;
            }
        }

        /// <summary>
        /// The <see cref="IsProgressBar" /> property's name.
        /// </summary>
        public const string IsProgressBarPropertyName = "IsProgressBar";

        private bool _isProgressBar = false;

        /// <summary>
        /// Sets and gets the IsProgressBar property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsProgressBar
        {
            get
            {
                return _isProgressBar;
            }

            set
            {
                if (_isProgressBar == value)
                {
                    return;
                }

                _isProgressBar = value;
                RaisePropertyChanged(IsProgressBarPropertyName);
                if (value)
                {
                    IsPlaintext = false;
                    IsBalanceBar = false;
                }
                if (value == false && !VerifyDisplayType()) IsProgressBar = true;
            }
        }

        /// <summary>
        /// The <see cref="IsBalanceBar" /> property's name.
        /// </summary>
        public const string IsBalanceBarPropertyName = "IsBalanceBar";

        private bool _isBalanceBar = false;

        /// <summary>
        /// Sets and gets the IsBalanceBar property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsBalanceBar
        {
            get
            {
                return _isBalanceBar;
            }

            set
            {
                if (_isBalanceBar == value)
                {
                    return;
                }
                
                _isBalanceBar = value;
                if (value)
                {
                    IsPlaintext = false;
                    IsProgressBar = false;
                }
                RaisePropertyChanged(IsBalanceBarPropertyName);
                if (value == false && !VerifyDisplayType()) IsBalanceBar = true;
            }
        }
        /// <summary>
        /// The <see cref="BalanceBarLowLabel" /> property's name.
        /// </summary>
        public const string BalanceBarLowLabelPropertyName = "BalanceBarLowLabel";

        private string _balanceBarLowLabel = "";

        /// <summary>
        /// Sets and gets the BalanceBarLowLabel property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string BalanceBarLowLabel
        {
            get
            {
                return _balanceBarLowLabel;
            }

            set
            {
                if (_balanceBarLowLabel == value)
                {
                    return;
                }

                _balanceBarLowLabel = value;
                RaisePropertyChanged(BalanceBarLowLabelPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BalanceBarHighLabel" /> property's name.
        /// </summary>
        public const string BalanceBarHighLabelPropertyName = "BalanceBarHighLabel";

        private string _balanceBarHighLabel = "";

        /// <summary>
        /// Sets and gets the BalanceBarHighLabel property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string BalanceBarHighLabel
        {
            get
            {
                return _balanceBarHighLabel;
            }

            set
            {
                if (_balanceBarHighLabel == value)
                {
                    return;
                }

                _balanceBarHighLabel = value;
                RaisePropertyChanged(BalanceBarHighLabelPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="HighWarning" /> property's name.
        /// </summary>
        public const string HighWarningPropertyName = "HighWarning";

        private bool _highWarning = false;

        /// <summary>
        /// Sets and gets the HighWarning property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool HighWarning
        {
            get
            {
                return _highWarning;
            }

            set
            {
                if (_highWarning == value)
                {
                    return;
                }

                _highWarning = value;
                RaisePropertyChanged(HighWarningPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LowWarning" /> property's name.
        /// </summary>
        public const string LowWarningPropertyName = "LowWarning";

        private bool _lowWarning = false;

        /// <summary>
        /// Sets and gets the LowWarning property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool LowWarning
        {
            get
            {
                return _lowWarning;
            }

            set
            {
                if (_lowWarning == value)
                {
                    return;
                }

                _lowWarning = value;
                RaisePropertyChanged(LowWarningPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MinimumValueConstant" /> property's name.
        /// </summary>
        public const string MinimumValueConstantPropertyName = "MinimumValueConstant";

        private bool _minimumValueConstant = true;

        /// <summary>
        /// Sets and gets the MinimumValueConstant property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool MinimumValueConstant
        {
            get
            {
                return _minimumValueConstant;
            }

            set
            {
                if (_minimumValueConstant == value)
                {
                    return;
                }

                _minimumValueConstant = value;
                RaisePropertyChanged(MinimumValueConstantPropertyName);
                if (value)
                {
                    MinimumValueVariable = false;
                }
                if (!value && !VerifyMinValue())
                {
                    MinimumValueConstant = true;
                }
            }
        }

        /// <summary>
        /// The <see cref="MinimumValueVariable" /> property's name.
        /// </summary>
        public const string MinimumValueVariablePropertyName = "MinimumValueVariable";

        private bool _minimumValueVariable = false;

        /// <summary>
        /// Sets and gets the MinimumValueVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool MinimumValueVariable
        {
            get
            {
                return _minimumValueVariable;
            }

            set
            {
                if (_minimumValueVariable == value)
                {
                    return;
                }

                _minimumValueVariable = value;
                RaisePropertyChanged(MinimumValueVariablePropertyName);
                if (value)
                {
                    MinimumValueConstant = false;
                }
                if (!value && !VerifyMinValue())
                {
                    MinimumValueVariable = true;
                }
            }
        }

        /// <summary>
        /// The <see cref="MaximumValueConstant" /> property's name.
        /// </summary>
        public const string MaximumValueConstantPropertyName = "MaximumValueConstant";

        private bool _maximumValueConstant = true;

        /// <summary>
        /// Sets and gets the MaximumValueConstant property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool MaximumValueConstant
        {
            get
            {
                return _maximumValueConstant;
            }

            set
            {
                if (_maximumValueConstant == value)
                {
                    return;
                }

                _maximumValueConstant = value;
                RaisePropertyChanged(MaximumValueConstantPropertyName);
                if (value)
                {
                    MaximumValueVariable = false;
                }
                if (!value && !VerifyMaxValue())
                {
                    MaximumValueConstant = true;
                }
            }
        }

        /// <summary>
        /// The <see cref="MaximumValueVariable" /> property's name.
        /// </summary>
        public const string MaximumValueVariablePropertyName = "MaximumValueVariable";

        private bool _maximumValueVariable = false;

        /// <summary>
        /// Sets and gets the MaximumValueVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool MaximumValueVariable
        {
            get
            {
                return _maximumValueVariable;
            }

            set
            {
                if (_maximumValueVariable == value)
                {
                    return;
                }

                _maximumValueVariable = value;
                RaisePropertyChanged(MaximumValueVariablePropertyName);
                if (value)
                {
                    MaximumValueConstant = false;
                }
                if (!value && !VerifyMaxValue())
                {
                    MaximumValueVariable = true;
                }
            }
        }

        /// <summary>
        /// The <see cref="MinimumValueConstantValue" /> property's name.
        /// </summary>
        public const string MinimumValueConstantValuePropertyName = "MinimumValueConstantValue";

        private int _minimumValueConstantValue = 0;

        /// <summary>
        /// Sets and gets the MinimumValueConstantValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int MinimumValueConstantValue
        {
            get
            {
                return _minimumValueConstantValue;
            }

            set
            {
                if (_minimumValueConstantValue == value)
                {
                    return;
                }

                _minimumValueConstantValue = value;
                RaisePropertyChanged(MinimumValueConstantValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="MaximumValueConstantValue" /> property's name.
        /// </summary>
        public const string MaximumValueConstantValuePropertyName = "MaximumValueConstantValue";

        private int _maximumValueConstantValue = 100;

        /// <summary>
        /// Sets and gets the MaximumValueConstantValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int MaximumValueConstantValue
        {
            get
            {
                return _maximumValueConstantValue;
            }

            set
            {
                if (_maximumValueConstantValue == value)
                {
                    return;
                }

                _maximumValueConstantValue = value;
                RaisePropertyChanged(MaximumValueConstantValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MinimumValueVariableValue" /> property's name.
        /// </summary>
        public const string MinimumValueVariableValuePropertyName = "MinimumValueVariableValue";

        private VarRef _minimumValueVariableValue = null;

        /// <summary>
        /// Sets and gets the MinimumValueVariableValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef MinimumValueVariableValue
        {
            get
            {
                return _minimumValueVariableValue;
            }

            set
            {
                if (_minimumValueVariableValue == value)
                {
                    return;
                }

                _minimumValueVariableValue = value;
                RaisePropertyChanged(MinimumValueVariableValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MaximumValueVariableValue" /> property's name.
        /// </summary>
        public const string MaximumValueVariableValuePropertyName = "MaximumValueVariableValue";

        private VarRef _maximumValueVariableValue = null;

        /// <summary>
        /// Sets and gets the MaximumValueVariableValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef MaximumValueVariableValue
        {
            get
            {
                return _maximumValueVariableValue;
            }

            set
            {
                if (_maximumValueVariableValue == value)
                {
                    return;
                }

                _maximumValueVariableValue = value;
                RaisePropertyChanged(MaximumValueVariableValuePropertyName);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public XElement ToXML()
        {
            XElement label = new XElement("Label", this.Label);
            XElement associatedVariable = new XElement("AssociatedVariable", this.AssociatedVariable.LinkedVarId);
            XElement visibility = new XElement("Visibility", this.DisplayCondition.ToXML());
            if (IsPlaintext)
            {
                return new XElement("PlayerStat", label, associatedVariable, visibility, new XElement("DisplayType", "Plaintext"));
            }
            else
            {
                XElement minValue;
                XElement maxValue;
                if (MinimumValueConstant)
                {
                    minValue = new XElement("MinValue", new XAttribute("Type", "Constant"), MinimumValueConstantValue);
                }
                else
                {
                    minValue = new XElement("MinValue", new XAttribute("Type", "Variable"), MinimumValueVariableValue.LinkedVarId);
                }
                if (MaximumValueConstant)
                {
                    maxValue = new XElement("MaxValue", new XAttribute("Type", "Constant"), MaximumValueConstantValue);
                }
                else
                {
                    maxValue = new XElement("MaxValue", new XAttribute("Type", "Variable"), MaximumValueVariableValue.LinkedVarId);
                }
                if (IsProgressBar)
                {
                    return new XElement("PlayerStat", label, associatedVariable, visibility, minValue, maxValue, new XElement("DisplayType", "ProgressBar"), new XElement("HighWarning", HighWarning), new XElement("LowWarning", LowWarning));
                }
                else if (IsBalanceBar)
                {
                    return new XElement("PlayerStat", label, associatedVariable, visibility, minValue, maxValue, new XElement("LowLabel", BalanceBarLowLabel), new XElement("HighLabel", BalanceBarHighLabel), new XElement("DisplayType", "BalanceBar"), new XElement("HighWarning", HighWarning), new XElement("LowWarning", LowWarning));
                }
            }
            return null;
        }
        public static PlayerStatistic FromXML(XElement xml)
        {
            PlayerStatistic stat = new PlayerStatistic();
            if (xml.Element("Label") != null)
            {
                stat.Label = xml.Element("Label").Value;
            }
            if (xml.Element("AssociatedVariable") != null)
            {
                stat.AssociatedVariable = new VarRef(Guid.Parse(xml.Element("AssociatedVariable").Value));
            }
            if (xml.Element("Visibility") != null)
            {
                stat.DisplayCondition = Script.FromXML(xml.Element("Visibility").Element("Script"), stat.DisplayCondition);
            }
            if (xml.Element("DisplayType") != null)
            {
                string type = xml.Element("DisplayType").Value;
                if (type == "Plaintext")
                {
                    stat.IsPlaintext = true;
                }
                else if (type == "ProgressBar" || type == "BalanceBar")
                {
                    if (type == "ProgressBar")
                        stat.IsProgressBar = true;
                    else
                        stat.IsBalanceBar = true;
                    if (xml.Element("HighWarning") != null)
                    {
                        stat.HighWarning = Convert.ToBoolean(xml.Element("HighWarning").Value);
                    }
                    if (xml.Element("LowWarning") != null)
                    {
                        stat.LowWarning = Convert.ToBoolean(xml.Element("LowWarning").Value);
                    }
                    if (xml.Element("MinValue") != null)
                    {
                        var minValueXml = xml.Element("MinValue");
                        if (minValueXml.Attribute("Type") != null)
                        {
                            var minType = minValueXml.Attribute("Type").Value;
                            if (minType == "Constant")
                            {
                                stat.MinimumValueConstant = true;
                                stat.MinimumValueConstantValue = Int32.Parse(minValueXml.Value);
                            }
                            else if (minType == "Variable")
                            {
                                stat.MinimumValueVariable = true;
                                stat.MinimumValueVariableValue = new VarRef(Guid.Parse(minValueXml.Value));
                            }
                        }
                    }
                    if (xml.Element("MaxValue") != null)
                    {
                        var maxValueXml = xml.Element("MaxValue");
                        if (maxValueXml.Attribute("Type") != null)
                        {
                            var maxType = maxValueXml.Attribute("Type").Value;
                            if (maxType == "Constant")
                            {
                                stat.MaximumValueConstant = true;
                                stat.MaximumValueConstantValue = Int32.Parse(maxValueXml.Value);
                            }
                            else if (maxType == "Variable")
                            {
                                stat.MaximumValueVariable = true;
                                stat.MaximumValueVariableValue = new VarRef(Guid.Parse(maxValueXml.Value));
                            }
                        }
                    }
                    if (xml.Element("LowLabel") != null)
                    {
                        stat.BalanceBarLowLabel = xml.Element("LowLabel").Value;
                    }
                    if (xml.Element("HighLabel") != null)
                    {
                        stat.BalanceBarHighLabel = xml.Element("HighLabel").Value;
                    }
                }
            }
            return stat;
        }
    }
}
