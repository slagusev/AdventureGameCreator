using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.StatusEffects
{
    public class GetArgument : ScriptLine
    {

        

        public GetArgument(StatusEffect se)
        {
            this.CurrentStatusEffect = se;
           SelectedArgument = new GenericRef<StatusEffectValue>((id) => 
            {
                if (CurrentStatusEffect == null)
                {
                    return null;
                }
                else
                {
                    var found = CurrentStatusEffect.Arguments.Where(a => a.Id == id);
                    if (found.Count() == 0) return null;
                    else return found.First();
                }
            }, sev => {
                if (sev == null) return Guid.Empty;
                else return sev.Id;
            });
        }

        public StatusEffect CurrentStatusEffect { get; set; }

        /// <summary>
        /// The <see cref="VariableRef" /> property's name.
        /// </summary>
        public const string VariableRefPropertyName = "VariableRef";

        private VarRef _variableRef = new VarRef();

        /// <summary>
        /// Sets and gets the VariableRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef VariableRef
        {
            get
            {
                return _variableRef;
            }

            set
            {
                if (_variableRef == value)
                {
                    return;
                }

                _variableRef = value;
                RaisePropertyChanged(VariableRefPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedArgument" /> property's name.
        /// </summary>
        public const string SelectedArgumentPropertyName = "SelectedArgument";

        private GenericRef<StatusEffectValue> _selectedArgument = null;

        /// <summary>
        /// Sets and gets the SelectedArgument property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GenericRef<StatusEffectValue> SelectedArgument
        {
            get
            {
                return _selectedArgument;
            }

            set
            {
                if (_selectedArgument == value)
                {
                    return;
                }

                _selectedArgument = value;
                RaisePropertyChanged(SelectedArgumentPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedArgument_Value" /> property's name.
        /// </summary>
        public const string SelectedArgument_ValuePropertyName = "SelectedArgument_Value";

        private StatusEffectValue _value = null;

        /// <summary>
        /// Sets and gets the SelectedArgument_Value property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public StatusEffectValue SelectedArgument_Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (_value == value)
                {
                    return;
                }
                if (value != null)
                    this.SelectedArgument.Ref = value.Id;
                _value = value;
                RaisePropertyChanged(SelectedArgument_ValuePropertyName);
            }
        }


        public override string Plaintext
        {
            get
            {
                return "Store the argument " + (SelectedArgument != null && SelectedArgument.Value != null ? SelectedArgument.Value.Name : "INVALID REFERENCE") + " in the variable " + (VariableRef != null && VariableRef.LinkedVariable != null ? VariableRef.LinkedVariable.Name : "INVALID REFERENCE");
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("GetArgument", new XElement("SelectedArgument", SelectedArgument.Ref),
                                               new XElement("VariableRef", VariableRef.LinkedVarId));
        }

        public static GetArgument FromXML(XElement xml, StatusEffect se)
        {
            GetArgument ga = new GetArgument(se);
            ga.CurrentStatusEffect = se;
            ga.SelectedArgument.Ref = Guid.Parse(xml.Element("SelectedArgument").Value);
            ga.VariableRef.LinkedVarId = Guid.Parse(xml.Element("VariableRef").Value);
            return ga;
        }


    }
}
