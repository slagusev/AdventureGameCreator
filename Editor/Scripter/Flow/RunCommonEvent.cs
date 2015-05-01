using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Flow
{
    public class RunCommonEvent : ScriptLine
    {
        public RunCommonEvent(Script s)
        {
            refScript = s;
            RefreshValidEvents();
        }

        public void RefreshValidEvents()
        {
            AvaiableCommonEvents = new ObservableCollection<CommonEventRef>((from a in MainViewModel.MainViewModelStatic.CommonEvents.Where(a => refScript.AllowedCommonEventTypes.Contains(a.EventType.Item1)) select new CommonEventRef(a.Id)).ToList());
        }

        public Script refScript = null;

        /// <summary>
        /// The <see cref="AvaiableCommonEvents" /> property's name.
        /// </summary>
        public const string AvaiableCommonEventsPropertyName = "AvaiableCommonEvents";

        private ObservableCollection<CommonEventRef> _availableCommonEvents = new ObservableCollection<CommonEventRef>();

        /// <summary>
        /// Sets and gets the AvaiableCommonEvents property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<CommonEventRef> AvaiableCommonEvents
        {
            get
            {
                return _availableCommonEvents;
            }

            set
            {
                if (_availableCommonEvents == value)
                {
                    return;
                }

                _availableCommonEvents = value;
                RaisePropertyChanged(AvaiableCommonEventsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedEvent" /> property's name.
        /// </summary>
        public const string SelectedEventPropertyName = "SelectedEvent";

        private CommonEventRef _selectedEvent = null;

        /// <summary>
        /// Sets and gets the SelectedEvent property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CommonEventRef SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }

            set
            {
                if (_selectedEvent == value)
                {
                    return;
                }

                _selectedEvent = value;
                if (SelectedEvent != null && SelectedEvent.LinkedCommonEvent != null)
                {
                    IsReturnVariable = SelectedEvent.LinkedCommonEvent.EventType.Item1 == "Returned Value Script";
                }
                RaisePropertyChanged(SelectedEventPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="RunFromVariable" /> property's name.
        /// </summary>
        public const string RunFromVariablePropertyName = "RunFromVariable";

        private bool _runFromVariable = false;

        /// <summary>
        /// Sets and gets the RunFromVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool RunFromVariable
        {
            get
            {
                return _runFromVariable;
            }

            set
            {
                if (_runFromVariable == value)
                {
                    return;
                }

                _runFromVariable = value;
                RaisePropertyChanged(RunFromVariablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="VarScript" /> property's name.
        /// </summary>
        public const string VarScriptPropertyName = "VarScript";

        private VarRef _varScript = null;

        /// <summary>
        /// Sets and gets the VarScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef VarScript
        {
            get
            {
                return _varScript;
            }

            set
            {
                if (_varScript == value)
                {
                    return;
                }

                _varScript = value;
                RaisePropertyChanged(VarScriptPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsReturnVariable" /> property's name.
        /// </summary>
        public const string IsReturnVariablePropertyName = "IsReturnVariable";

        private bool _isReturnVariable = false;

        /// <summary>
        /// Sets and gets the IsReturnVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsReturnVariable
        {
            get
            {
                return _isReturnVariable;
            }

            set
            {
                if (_isReturnVariable == value)
                {
                    return;
                }

                _isReturnVariable = value;
                RaisePropertyChanged(IsReturnVariablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="VarRef" /> property's name.
        /// </summary>
        public const string VarRefPropertyName = "VarRef";

        private VarRef _varRef = null;

        /// <summary>
        /// Sets and gets the VarRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef VarRef
        {
            get
            {
                return _varRef;
            }

            set
            {
                if (_varRef == value)
                {
                    return;
                }

                _varRef = value;
                RaisePropertyChanged(VarRefPropertyName);
            }
        }

        public override string Plaintext
        {
            get
            {
                string name = "UNKNOWN";
                string varName = "UNKNOWN";
                if (SelectedEvent != null && SelectedEvent.LinkedCommonEvent != null)
                {
                    name = SelectedEvent.LinkedCommonEvent.Name;
                }
                if (VarRef != null && VarRef.LinkedVariable != null)
                {
                    varName = VarRef.LinkedVariable.Name;
                }
                if (this.RunFromVariable)
                {
                    return "Run the common event stored in the " + (this.VarScript != null && this.VarScript.LinkedVariable != null ? this.VarScript.LinkedVariable.Name : "UNKNOWN") + " variable.";
                }
                else if (!IsReturnVariable)
                {
                    return "Run the " + name + " common event.";
                }
                
                else
                {
                    return "Run the " + name + " common event and store the result in " + varName;
                }
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            XElement xml = new XElement("RunCommonEvent");
            if (!RunFromVariable)
            {
                if (this.SelectedEvent != null && this.SelectedEvent.LinkedCommonEvent != null)
                {

                    xml.Add(new XElement("SelectedEvent", this.SelectedEvent.LinkedCommonEventId));
                    if (this.IsReturnVariable && this.VarRef != null && this.VarRef.LinkedVariable != null)
                    {
                        xml.Add(new XElement("VarRef", this.VarRef.LinkedVarId));
                    }


                    if (this.IsReturnVariable && this.VarRef != null && this.VarRef.LinkedVariable != null)
                    {
                        xml.Add(new XElement("VarRef", this.VarRef.LinkedVarId));
                    }

                }
            }
            else
            {
                xml.Add(new XElement("SelectedVariableScript", this.VarScript.LinkedVarId));
            }
            return xml;
        }

        public static RunCommonEvent FromXml(XElement xml, Script baseScript)
        {
            RunCommonEvent rce = new RunCommonEvent(baseScript);
            if (xml.Element("SelectedEvent") != null)
            {
                rce.SelectedEvent = new CommonEventRef(Guid.Parse(xml.Element("SelectedEvent").Value));
            }
            if (xml.Element("SelectedVariableScript") != null)
            {
                rce.VarScript = new VarRef(Guid.Parse(xml.Element("SelectedVariableScript").Value));
                rce.RunFromVariable = true;
            }
            if (xml.Element("VarRef") != null)
            {
                rce.IsReturnVariable = true;
                rce.VarRef = new VarRef(Guid.Parse(xml.Element("VarRef").Value));
            }
            rce.RefreshValidEvents();
            return rce;
        }
    }
}
