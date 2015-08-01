using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class SetItemName : ScriptLine
    {
        /// <summary>
        /// The <see cref="ItemVariable" /> property's name.
        /// </summary>
        public const string ItemVariablePropertyName = "ItemVariable";

        private VarRef _item = null;

        /// <summary>
        /// Sets and gets the ItemVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef ItemVariable
        {
            get
            {
                return _item;
            }

            set
            {
                if (_item == value)
                {
                    return;
                }

                _item = value;
                RaisePropertyChanged(ItemVariablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringVariable" /> property's name.
        /// </summary>
        public const string StringVariablePropertyName = "StringVariable";

        private VarRef _stringVariable = null;

        /// <summary>
        /// Sets and gets the StringVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef StringVariable
        {
            get
            {
                return _stringVariable;
            }

            set
            {
                if (_stringVariable == value)
                {
                    return;
                }

                _stringVariable = value;
                RaisePropertyChanged(StringVariablePropertyName);
            }
        }

        public override string Plaintext
        {
            get
            {
                return "Set the " + "name of the item stored in the "
                    + (ItemVariable != null && ItemVariable.LinkedVariable != null ? ItemVariable.LinkedVariable.Name : "UNKNOWN VARIABLE") + " variable to the value of " +
                      (StringVariable != null && StringVariable.LinkedVariable != null ? StringVariable.LinkedVariable.Name : "UNKNOWN VARIABLE");
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("SetItemName",
                new XElement("ItemVariable", (ItemVariable != null && ItemVariable.LinkedVarId != null ? ItemVariable.LinkedVarId.ToString() : Guid.Empty.ToString())),
                new XElement("StringVariable", (StringVariable != null && StringVariable.LinkedVarId != null ? StringVariable.LinkedVarId.ToString() : Guid.Empty.ToString())));
        }
        public static SetItemName FromXML(XElement xml)
        {
            SetItemName sin = new SetItemName();
            
            sin.ItemVariable = new VarRef(Guid.Parse(xml.Element("ItemVariable").Value));
            sin.StringVariable = new VarRef(Guid.Parse(xml.Element("StringVariable").Value));
            return sin;
        }

    }
}
