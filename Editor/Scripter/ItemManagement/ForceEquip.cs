using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class ForceEquip : ScriptLine
    {
        /// <summary>
        /// The <see cref="VarRef" /> property's name.
        /// </summary>
        public const string VarRefPropertyName = "VarRef";

        private VarRef _varRef = new VarRef(Guid.Empty);

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
        /// The <see cref="ThrowAwayItem" /> property's name.
        /// </summary>
        public const string ThrowAwayItemPropertyName = "ThrowAwayItem";

        private bool _throwAwayItem = false;

        /// <summary>
        /// Sets and gets the ThrowAwayItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ThrowAwayItem
        {
            get
            {
                return _throwAwayItem;
            }

            set
            {
                if (_throwAwayItem == value)
                {
                    return;
                }

                _throwAwayItem = value;
                RaisePropertyChanged(ThrowAwayItemPropertyName);
            }
        }

        public static ForceEquip FromXML(XElement xml)
        {
            ForceEquip fue = new ForceEquip();
            if (xml.Element("VarRef") != null)
            {
                fue.VarRef = new VarRef(Guid.Parse(xml.Element("VarRef").Value));
            }
            if (xml.Element("ThrowAwayItem") != null)
            {
                fue.ThrowAwayItem = Convert.ToBoolean(xml.Element("ThrowAwayItem").Value);
            }
            return fue;
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("ForceEquip",
                new XElement("VarRef", VarRef.LinkedVarId),
                new XElement("ThrowAwayItem", ThrowAwayItem));

        }

        public override string Plaintext
        {
            get
            {

                return "Equip the item in the " + (VarRef != null && VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN") + " variable" + (ThrowAwayItem ? " and remove the previously equipped item from the player's inventory." : "") + ".";
            }
        }
    }
}
