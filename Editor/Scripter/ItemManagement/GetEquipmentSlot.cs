using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class GetEquipmentSlot : ScriptLine
    {
        /// <summary>
        /// The <see cref="Slot" /> property's name.
        /// </summary>
        public const string SlotPropertyName = "Slot";

        private EquipmentSlot _slot = null;

        /// <summary>
        /// Sets and gets the Slot property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public EquipmentSlot Slot
        {
            get
            {
                if (_slot == null) return null;
                var mainSlot = MainViewModel.MainViewModelStatic.Settings.EquipmentSlots.Where(a => a.Name == _slot.Name).FirstOrDefault();
                if (mainSlot != null)
                {
                    return mainSlot;
                }
                return _slot;
            }

            set
            {
                if (_slot == value)
                {
                    return;
                }

                _slot = value;
                RaisePropertyChanged(SlotPropertyName);
            }
        }
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
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("GetEquipmentSlot",
                new XElement("Slot", Slot.Name),
                new XElement("VarRef", VarRef.LinkedVarId));
        }

        public static GetEquipmentSlot FromXML(XElement xml)
        {
            GetEquipmentSlot ges = new GetEquipmentSlot();
            if (xml.Element("Slot") != null)
            {
                ges.Slot = new EquipmentSlot() { Name = xml.Element("Slot").Value };
            }
            if (xml.Element("VarRef") != null)
            {
                ges.VarRef = new VarRef(Guid.Parse(xml.Element("VarRef").Value));
            }
            return ges;
        }
        public override string Plaintext
        {
            get
            {

                return "Retrieve the item in the " + (Slot != null ? Slot.Name : "UNKNOWN") + " slot and assign it to the " + (VarRef != null && VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN") + " variable.";
            }
        }
    }
}
