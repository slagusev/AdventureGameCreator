using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class GetItemName : ScriptLine
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

        /// <summary>
        /// The <see cref="UseDefaultName" /> property's name.
        /// </summary>
        public const string UseDefaultNamePropertyName = "UseDefaultName";

        private bool _useDefaultName = false;

        /// <summary>
        /// Sets and gets the UseDefaultName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool UseDefaultName
        {
            get
            {
                return _useDefaultName;
            }

            set
            {
                if (_useDefaultName == value)
                {
                    return;
                }

                _useDefaultName = value;
                RaisePropertyChanged(UseDefaultNamePropertyName);
            }
        }

        public override string Plaintext
        {
            get
            {
                return "Get the " + (UseDefaultName ? "original " : "") + "name of the item stored in the " 
                    + (ItemVariable != null && ItemVariable.LinkedVariable != null ? ItemVariable.LinkedVariable.Name : "UNKNOWN VARIABLE") + " variable, and store it in " +
                      (StringVariable != null && StringVariable.LinkedVariable != null ? StringVariable.LinkedVariable.Name : "UNKNOWN VARIABLE");
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("GetItemName",
                new XElement("ItemVariable", (ItemVariable != null && ItemVariable.LinkedVarId != null ? ItemVariable.LinkedVarId.ToString() : Guid.Empty.ToString())),
                new XElement("StringVariable", (StringVariable != null && StringVariable.LinkedVarId != null ? StringVariable.LinkedVarId.ToString() : Guid.Empty.ToString())),
                new XElement("UseDefaultName", UseDefaultName));
        }
        public static GetItemName FromXML(XElement xml)
        {
            GetItemName gin = new GetItemName();
            gin.UseDefaultName = Convert.ToBoolean(xml.Element("UseDefaultName").Value);
            gin.ItemVariable = new VarRef(Guid.Parse(xml.Element("ItemVariable").Value));
            gin.StringVariable = new VarRef(Guid.Parse(xml.Element("StringVariable").Value));
            return gin;
        }

    }
}
