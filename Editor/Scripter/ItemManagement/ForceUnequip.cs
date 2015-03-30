using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class ForceUnequip : ScriptLine
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
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("ForceUnequip",
                new XElement("Slot", Slot.Name),
                new XElement("ThrowAwayItem", ThrowAwayItem));
        }

        public static ForceUnequip FromXML(XElement xml)
        {
            ForceUnequip fue = new ForceUnequip();
            if (xml.Element("Slot") != null)
            {
                fue.Slot = new EquipmentSlot() { Name = xml.Element("Slot").Value };
            }
            if (xml.Element("ThrowAwayItem") != null)
            {
                fue.ThrowAwayItem = Convert.ToBoolean(xml.Element("ThrowAwayItem").Value);
            }
            return fue;
        }
        public override string Plaintext
        {
            get
            {
                
                return "Unequip the item in the " + (Slot != null ? Slot.Name : "UNKNOWN") + " slot" + (ThrowAwayItem ? " and remove it from the player's inventory" :"") + ".";
            }
        }
    }
}
