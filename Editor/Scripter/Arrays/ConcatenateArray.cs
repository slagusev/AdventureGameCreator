using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Arrays
{
    public class ConcatenateArray : ScriptLine
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


        /// <summary>
        /// The <see cref="Delimiter" /> property's name.
        /// </summary>
        public const string DelimiterPropertyName = "Delimiter";

        private string _delimiter = ", ";

        /// <summary>
        /// Sets and gets the Delimiter property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Delimiter
        {
            get
            {
                return _delimiter;
            }

            set
            {
                if (_delimiter == value)
                {
                    return;
                }

                _delimiter = value;
                RaisePropertyChanged(DelimiterPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="FinalWord" /> property's name.
        /// </summary>
        public const string FinalWordPropertyName = "FinalWord";

        private string _finalWord = "and ";

        /// <summary>
        /// Sets and gets the FinalWord property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string FinalWord
        {
            get
            {
                return _finalWord;
            }

            set
            {
                if (_finalWord == value)
                {
                    return;
                }

                _finalWord = value;
                RaisePropertyChanged(FinalWordPropertyName);
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("ConcatenateArray", new XElement("VarRef", this.VarRef != null ? this.VarRef.LinkedVarId : Guid.Empty),
                                              new XElement("ArrayRef", this.ArrayRef != null ? this.ArrayRef.Ref : Guid.Empty),
                                              new XElement("Delimiter", this.Delimiter),
                                              new XElement("FinalWord", this.FinalWord));
        }
        public static ConcatenateArray FromXML(XElement xml)
        {
            var ata = new ConcatenateArray
            {
                VarRef = new VarRef(Guid.Parse(xml.Element("VarRef").Value)),
                ArrayRef = GenericRef<AddToArray>.GetArrayRef(),
                Delimiter = xml.Element("Delimiter").Value,
                FinalWord = xml.Element("FinalWord") != null ? xml.Element("FinalWord").Value : ""
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
