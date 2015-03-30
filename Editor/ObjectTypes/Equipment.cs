using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class Equipment : INotifyPropertyChanged
    {
        public Equipment()
        {
            OnEquip.CanAddItem = true;
            OnEquip.CanAddText = false;
            OnEquip.CanComment = true;
            OnEquip.CanConditional = true;
            OnEquip.CanDisplayText = true;
            OnEquip.CanReturn = true;
            OnEquip.CanReturnValue = false;
            OnEquip.CanSetVariable = true;
            OnEquip.CanStopGame = true;
            
            OnEquip.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeItem);
            OnEquip.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeTrueFalse);

            OnUnequip = new Script(OnEquip);

            OnMove.CanAddItem = true;
            OnMove.CanAddText = false;
            OnMove.CanComment = true;
            OnMove.CanConditional = true;
            OnMove.CanDisplayText = true;
            OnMove.CanReturn = true;
            OnMove.CanReturnValue = false;
            OnMove.CanSetVariable = true;
            OnMove.CanStopGame = true;

            OnMove.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeItem);
            OnMove.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeTrueFalse);

            WireCommands();
        }

        public void WireCommands()
        {
            AddCoverSlot = new RelayCommand(AddCoverSlotCommand);
            RemoveCoverSlot = new RelayCommand(RemoveCoverSlotCommand);
            AddEquipSlot = new RelayCommand(AddEquipSlotCommand);
            RemoveEquipSlot = new RelayCommand(RemoveEquipSlotCommand);
        }
        /// <summary>
        /// The <see cref="OccupiesSlots" /> property's name.
        /// </summary>
        public const string OccupiesSlotsPropertyName = "OccupiesSlots";

        private ObservableCollection<EquipmentSlot> _occupiesSlots = new ObservableCollection<EquipmentSlot>();

        /// <summary>
        /// Sets and gets the OccupiesSlots property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<EquipmentSlot> OccupiesSlots
        {
            get
            {
                return _occupiesSlots;
            }

            set
            {
                if (_occupiesSlots == value)
                {
                    return;
                }

                _occupiesSlots = value;
                RaisePropertyChanged(OccupiesSlotsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CoversSlots" /> property's name.
        /// </summary>
        public const string CoversSlotsPropertyName = "CoversSlots";

        private ObservableCollection<EquipmentSlot> _coversSlots = new ObservableCollection<EquipmentSlot>();

        /// <summary>
        /// Sets and gets the CoversSlots property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<EquipmentSlot> CoversSlots
        {
            get
            {
                return _coversSlots;
            }

            set
            {
                if (_coversSlots == value)
                {
                    return;
                }

                _coversSlots = value;
                RaisePropertyChanged(CoversSlotsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedToEquipSlot" /> property's name.
        /// </summary>
        public const string SelectedToEquipSlotPropertyName = "SelectedToEquipSlot";

        private EquipmentSlot _selectedToEquipSlot = null;

        /// <summary>
        /// Sets and gets the SelectedToEquipSlot property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public EquipmentSlot SelectedToEquipSlot
        {
            get
            {
                return _selectedToEquipSlot;
            }

            set
            {
                if (_selectedToEquipSlot == value)
                {
                    return;
                }

                _selectedToEquipSlot = value;
                RaisePropertyChanged(SelectedToEquipSlotPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedToRemoveEquipSlot" /> property's name.
        /// </summary>
        public const string SelectedToRemoveEquipSlotPropertyName = "SelectedToRemoveEquipSlot";

        private EquipmentSlot _selectedToRemoveEquipSlot = null;

        /// <summary>
        /// Sets and gets the SelectedToRemoveEquipSlot property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public EquipmentSlot SelectedToRemoveEquipSlot
        {
            get
            {
                return _selectedToRemoveEquipSlot;
            }

            set
            {
                if (_selectedToRemoveEquipSlot == value)
                {
                    return;
                }

                _selectedToRemoveEquipSlot = value;
                RaisePropertyChanged(SelectedToRemoveEquipSlotPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedToCoverSlot" /> property's name.
        /// </summary>
        public const string SelectedToCoverSlotPropertyName = "SelectedToCoverSlot";

        private EquipmentSlot _selectedToCoverSlot = null;

        /// <summary>
        /// Sets and gets the SelectedToCoverSlot property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public EquipmentSlot SelectedToCoverSlot
        {
            get
            {
                return _selectedToCoverSlot;
            }

            set
            {
                if (_selectedToCoverSlot == value)
                {
                    return;
                }

                _selectedToCoverSlot = value;
                RaisePropertyChanged(SelectedToCoverSlotPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedToUncoverSlot" /> property's name.
        /// </summary>
        public const string SelectedToUncoverSlotPropertyName = "SelectedToUncoverSlot";

        private EquipmentSlot _selectedToUncoverSlot = null;

        /// <summary>
        /// Sets and gets the SelectedToUncoverSlot property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public EquipmentSlot SelectedToUncoverSlot
        {
            get
            {
                return _selectedToUncoverSlot;
            }

            set
            {
                if (_selectedToUncoverSlot == value)
                {
                    return;
                }

                _selectedToUncoverSlot = value;
                RaisePropertyChanged(SelectedToUncoverSlotPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="OnEquip" /> property's name.
        /// </summary>
        public const string OnEquipPropertyName = "OnEquip";

        private Script _onEquip = new Script();

        /// <summary>
        /// Sets and gets the OnEquip property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnEquip
        {
            get
            {
                return _onEquip;
            }

            set
            {
                if (_onEquip == value)
                {
                    return;
                }

                _onEquip = value;
                RaisePropertyChanged(OnEquipPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="OnUnequip" /> property's name.
        /// </summary>
        public const string OnUnequipPropertyName = "OnUnequip";

        private Script _onUnequip = new Script();

        /// <summary>
        /// Sets and gets the OnUnequip property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnUnequip
        {
            get
            {
                return _onUnequip;
            }

            set
            {
                if (_onUnequip == value)
                {
                    return;
                }

                _onUnequip = value;
                RaisePropertyChanged(OnUnequipPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="OnMove" /> property's name.
        /// </summary>
        public const string OnMovePropertyName = "OnMove";

        private Script _onMove = new Script();

        /// <summary>
        /// Sets and gets the OnMove property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnMove
        {
            get
            {
                return _onMove;
            }

            set
            {
                if (_onMove == value)
                {
                    return;
                }

                _onMove = value;
                RaisePropertyChanged(OnMovePropertyName);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public RelayCommand AddEquipSlot { get; set; }
        public RelayCommand RemoveEquipSlot { get; set; }
        public RelayCommand AddCoverSlot { get; set; }
        public RelayCommand RemoveCoverSlot { get; set; }

        private void AddEquipSlotCommand()
        {
            if (SelectedToEquipSlot != null && !OccupiesSlots.Contains(SelectedToEquipSlot) && !CoversSlots.Contains(SelectedToCoverSlot))
            {
                OccupiesSlots.Add(SelectedToEquipSlot);
            }
        }
        private void RemoveEquipSlotCommand()
        {
            if (SelectedToRemoveEquipSlot != null)
            {
                OccupiesSlots.Remove(SelectedToRemoveEquipSlot);
            }
        }
        private void AddCoverSlotCommand()
        {
            if (SelectedToCoverSlot != null && !OccupiesSlots.Contains(SelectedToCoverSlot) && !CoversSlots.Contains(SelectedToCoverSlot))
            {
                CoversSlots.Add(SelectedToCoverSlot);
            }
        }
        private void RemoveCoverSlotCommand()
        {
            if (SelectedToUncoverSlot != null)
            {
                CoversSlots.Remove(SelectedToUncoverSlot);
            }
        }

        public XElement ToXML()
        {
            return new XElement("Equipment",
                new XElement("OccupiesSlots", from a in OccupiesSlots select a.ToXML()),
                new XElement("CoversSlots", from a in CoversSlots select a.ToXML()),
                new XElement("OnEquip", OnEquip.ToXML()),
                new XElement("OnUnequip", OnUnequip.ToXML()),
                new XElement("OnMove", OnMove.ToXML()));
        }

        public static Equipment FromXML(XElement xml)
        {
            Equipment e = new Equipment();
            if (xml.Element("OccupiesSlots") != null)
            {
                foreach (var element in xml.Element("OccupiesSlots").Elements())
                {
                    var slot = MainViewModel.MainViewModelStatic.Settings.EquipmentSlots.Where(a => a.Name == element.Value).FirstOrDefault();
                    if (slot != null)
                        e.OccupiesSlots.Add(slot);
                }
            }
            if (xml.Element("CoversSlots") != null)
            {
                foreach (var element in xml.Element("CoversSlots").Elements())
                {
                    var slot = MainViewModel.MainViewModelStatic.Settings.EquipmentSlots.Where(a => a.Name == element.Value).FirstOrDefault();
                    if (slot != null)
                        e.CoversSlots.Add(slot);
                }
            }
            if (xml.Element("OnEquip") != null)
                e.OnEquip = Script.FromXML(xml.Element("OnEquip").Element("Script"), e.OnEquip);
            if (xml.Element("OnUnequip") != null)
                e.OnUnequip = Script.FromXML(xml.Element("OnUnequip").Element("Script"), e.OnUnequip);
            if (xml.Element("OnMove") != null)
                e.OnMove = Script.FromXML(xml.Element("OnMove").Element("Script"), e.OnMove);

            return e;
        }
    }
}
