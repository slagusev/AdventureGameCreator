using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class RemoveItem : ScriptLine
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
            if (VarRef != null && VarRef.LinkedVariable != null)
            {
                return new System.Xml.Linq.XElement("RemoveItem", VarRef.LinkedVarId);
            }
            else
            {
                return new System.Xml.Linq.XElement("RemoveItem", Guid.Empty);
            }
        }

        public override string Plaintext
        {
            get
            {
                return "Remove " + (VarRef != null && VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN") + " from the player's inventory.";
            }
        }

        public static RemoveItem FromXML(XElement xml)
        {
            return new RemoveItem() { VarRef = new VarRef(Guid.Parse(xml.Value)) };
        }
        
    }
}
