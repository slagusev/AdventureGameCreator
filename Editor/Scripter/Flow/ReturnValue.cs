using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Flow
{
    public class ReturnValue : ScriptLine
    {
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
                RaisePropertyChanged(SelectedVariablePropertyName);
            }
        }
        public override string Plaintext
        {
            get
            {
                return "Return the " + (SelectedVariable != null && SelectedVariable.LinkedVariable != null ? SelectedVariable.LinkedVariable.Name : "UNKNOWN") + " variable to the parent script.";
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("ReturnValue", (SelectedVariable != null ? SelectedVariable.LinkedVarId : Guid.Empty));
        }
        public static ReturnValue FromXML(XElement xml)
        {
            return new ReturnValue() { SelectedVariable = new VarRef(Guid.Parse(xml.Value)) };
        }
    }
}
