using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class GetCurrentItem : ScriptLine
    {
        /// <summary>
        /// The <see cref="VarRef" /> property's name.
        /// </summary>
        public const string VarRefPropertyName = "VarRef";

        private VarRef _varRef = new VarRef();

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
            return new System.Xml.Linq.XElement("GetCurrentItem", VarRef.LinkedVarId);
        }
        public override string Plaintext
        {
            get
            {
                return "Assign current item to " + (VarRef != null && VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN");
            }
        }
        public static GetCurrentItem FromXML(XElement xml)
        {
            return new GetCurrentItem() { VarRef = new VarRef(Guid.Parse(xml.Value)) };
        }
    }
}
