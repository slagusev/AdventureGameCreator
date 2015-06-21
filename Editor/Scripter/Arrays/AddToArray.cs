using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Arrays
{
    public class AddToArray : ScriptLine
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



        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("AddToArray", new XElement("VarRef", this.VarRef != null ? this.VarRef.LinkedVarId : Guid.Empty),
                                              new XElement("ArrayRef", this.ArrayRef != null ? this.ArrayRef.Ref : Guid.Empty));
        }
        public static AddToArray FromXML(XElement xml)
        {
            var ata = new AddToArray
            {
                VarRef = new VarRef(Guid.Parse(xml.Element("VarRef").Value)),
                ArrayRef = GenericRef<AddToArray>.GetArrayRef()
            };
            ata.ArrayRef.Ref = Guid.Parse(xml.Element("ArrayRef").Value);
            return ata;
        }

        public override string Plaintext
        {
            get
            {
                return "Add the object stored in " + (VarRef != null && VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN VARIABLE")
                    + " to the " + (ArrayRef != null && ArrayRef.Value != null ? ArrayRef.Value.Name : "UNKNOWN ARRAY") + " array.";
            }
        }
    }
}
