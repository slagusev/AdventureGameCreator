using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class AddItemToInventory : ScriptLine
    {
        /// <summary>
        /// The <see cref="ItemRef" /> property's name.
        /// </summary>
        public const string ItemRefPropertyName = "ItemRef";

        private ItemRef _itemRef = new ItemRef();

        /// <summary>
        /// Sets and gets the ItemRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemRef ItemReference
        {
            get
            {
                return _itemRef;
            }

            set
            {
                if (_itemRef == value)
                {
                    return;
                }

                _itemRef = value;
                RaisePropertyChanged(ItemRefPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="FromVariable" /> property's name.
        /// </summary>
        public const string FromVariablePropertyName = "FromVariable";

        private bool _fromVariable = false;

        /// <summary>
        /// Sets and gets the FromVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool FromVariable
        {
            get
            {
                return _fromVariable;
            }

            set
            {
                if (_fromVariable == value)
                {
                    return;
                }

                _fromVariable = value;
                RaisePropertyChanged(FromVariablePropertyName);
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
            get {
                if (this.FromVariable)
                {
                    return "Add the item stored in" + (VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN") + " to the player's inventory.";
                }
                else
                {
                    return "Add a new " + (ItemReference.LinkedItem != null ? ItemReference.LinkedItem.ItemName : "UNKNOWN") + " to the player's inventory.";
                }
            }
        }

        public override XElement ToXML()
        {
            if (this.FromVariable)
            {
                return new XElement("AddItem", new XElement("VarRef", VarRef.LinkedVarId));
            }
            else
            {
                return new XElement("AddItem", new XElement("ItemRef", ItemReference.LinkedItemId));
            }
        }

        public static ScriptLine FromXML(XElement xml)
        {
            if (xml.Element("ItemRef") != null)
            {
                return new AddItemToInventory { ItemReference = new ItemRef(Guid.Parse(xml.Element("ItemRef").Value)) };
            }
            else if (xml.Element("VarRef") != null)
            {
                return new AddItemToInventory { VarRef = new VarRef(Guid.Parse(xml.Element("VarRef").Value)), FromVariable = true };
            }
            else return new AddItemToInventory();
            //return new AddItemToInventory();
        }
    }
}
