using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Arrays
{
    public class GetAllItemsOfType : ScriptLine
    {

        /// <summary>
        /// The <see cref="ItemType" /> property's name.
        /// </summary>
        public const string ItemTypePropertyName = "ItemType";

        private ItemRef _itemRef = null;

        /// <summary>
        /// Sets and gets the ItemType property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemRef ItemType
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
                RaisePropertyChanged(ItemTypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ArrayRef" /> property's name.
        /// </summary>
        public const string ArrayRefPropertyName = "ArrayRef";

        private GenericRef<VarArray> _arrayRef = null;

        /// <summary>
        /// Sets and gets the ArrayRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GenericRef<VarArray> ArrayRef
        {
            get
            {
                return _arrayRef;
            }

            set
            {
                if (_arrayRef == value)
                {
                    return;
                }

                _arrayRef = value;
                RaisePropertyChanged(ArrayRefPropertyName);
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("GetAllItemsOfType",
                                new XElement("ItemType", this.ItemType != null ? this.ItemType.LinkedItemId : Guid.Empty),
                                new XElement("ArrayRef", this.ArrayRef != null ? this.ArrayRef.Ref : Guid.Empty));
        }

        public static GetAllItemsOfType FromXML(XElement xml)
        {
            GetAllItemsOfType galot = new GetAllItemsOfType
            {
                ArrayRef =  GenericRef<VarArray>.GetArrayRef()
            };

            galot.ArrayRef.Ref = Guid.Parse(xml.Element("ArrayRef").Value);
            galot.ItemType = new ItemRef(Guid.Parse(xml.Element("ItemType").Value));

            return galot;
        }

        public override string Plaintext
        {
            get
            {
                return "Get all items in the player's inventory of type " + (ItemType != null && ItemType.LinkedItem != null ? ItemType.LinkedItem.ItemName : "UNKNOWN") + " " +
                    "and store them in the array " + (ArrayRef != null && ArrayRef.Value != null ? ArrayRef.Value.Name : "UNKNOWN") + ".";
            }
        }
    }
}
