using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Arrays
{
    public class GetAllItems : ScriptLine
    {
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
            return new XElement("GetAllItems", ArrayRef != null ? ArrayRef.Ref : Guid.Empty);
        }

        public static GetAllItems FromXML(XElement xml)
        {
            var gal = new GetAllItems() { ArrayRef = GenericRef<VarArray>.GetArrayRef() };
            gal.ArrayRef.Ref = Guid.Parse(xml.Value);
            return gal;
        }

        public override string Plaintext
        {
            get
            {
                return "Get all items in the player's inventory and store them in the array " + (ArrayRef != null && ArrayRef.Value != null ? ArrayRef.Value.Name : "UNKNOWN") + ".";;
            }
        }
    }
}
