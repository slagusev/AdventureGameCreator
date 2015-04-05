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
    public class PlayerSettings : INotifyPropertyChanged
    {
        public PlayerSettings()
        {
            PlayerDescription.CanDisplayText = false;
            PlayerDescription.CanAddText = true;
            PlayerDescription.CanStopGame = false;
            PlayerDescription.CanAddItem = false;
            PlayerDescription.IsItemScript = false;
            PlayerDescription.CanReturn = false;
            PlayerDescription.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeDescription);
            MovementScript.CanAddItem = true;
            MovementScript.CanAddText = false;
            MovementScript.CanComment = true;
            MovementScript.CanConditional = true;
            MovementScript.CanDisplayText = true;
            MovementScript.CanReturn = true;
            MovementScript.CanReturnValue = false;
            MovementScript.CanSetVariable = true;
            MovementScript.CanStopGame = true;
            MovementScript.IsInConversation = false;
            MovementScript.CanStartConversations = true;
            MovementScript.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeMovementAndInteractable);
            MovementScript.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeTrueFalse);
            WireCommands();
        }

        private void WireCommands()
        {
            NewStatisticCommand = new RelayCommand(NewStatistic);
            RemoveSelectedStatisticCommand = new RelayCommand(RemoveSelectedStatistic);
            MoveUpCommand = new RelayCommand(MoveUp);
            MoveDownCommand = new RelayCommand(MoveDown);
            AddEquipmentSlotCommand = new RelayCommand(AddEquipmentSlot);
            RemoveEquipmentSlotCommand = new RelayCommand(RemoveEquipmentSlot);
        }

        /// <summary>
        /// The <see cref="PlayerStatistics" /> property's name.
        /// </summary>
        public const string PlayerStatisticsPropertyName = "PlayerStatistics";

        private ObservableCollection<PlayerStatistic> _playerStatistics = new ObservableCollection<PlayerStatistic>();

        /// <summary>
        /// Sets and gets the PlayerStatistics property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<PlayerStatistic> PlayerStatistics
        {
            get
            {
                return _playerStatistics;
            }

            set
            {
                if (_playerStatistics == value)
                {
                    return;
                }

                _playerStatistics = value;
                RaisePropertyChanged(PlayerStatisticsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedStatistic" /> property's name.
        /// </summary>
        public const string SelectedStatisticPropertyName = "SelectedStatistic";

        private PlayerStatistic _selectedStatistic = null;

        /// <summary>
        /// Sets and gets the SelectedStatistic property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public PlayerStatistic SelectedStatistic
        {
            get
            {
                return _selectedStatistic;
            }

            set
            {
                if (_selectedStatistic == value)
                {
                    return;
                }

                _selectedStatistic = value;
                RaisePropertyChanged(SelectedStatisticPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PlayerDescription" /> property's name.
        /// </summary>
        public const string PlayerDescriptionPropertyName = "PlayerDescription";

        private Script _playerDescription = new Script();

        /// <summary>
        /// Sets and gets the PlayerDescription property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script PlayerDescription
        {
            get
            {
                return _playerDescription;
            }

            set
            {
                if (_playerDescription == value)
                {
                    return;
                }

                _playerDescription = value;
                RaisePropertyChanged(PlayerDescriptionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MovementScript" /> property's name.
        /// </summary>
        public const string MovementScriptPropertyName = "MovementScript";

        private Script _movementScript = new Script();

        /// <summary>
        /// Sets and gets the MovementScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script MovementScript
        {
            get
            {
                return _movementScript;
            }

            set
            {
                if (_movementScript == value)
                {
                    return;
                }

                _movementScript = value;
                RaisePropertyChanged(MovementScriptPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="EquipmentSlots" /> property's name.
        /// </summary>
        public const string EquipmentSlotsPropertyName = "EquipmentSlots";

        private ObservableCollection<EquipmentSlot> _equipmentSlots = new ObservableCollection<EquipmentSlot>();

        /// <summary>
        /// Sets and gets the EquipmentSlots property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<EquipmentSlot> EquipmentSlots
        {
            get
            {
                return _equipmentSlots;
            }

            set
            {
                if (_equipmentSlots == value)
                {
                    return;
                }

                _equipmentSlots = value;
                RaisePropertyChanged(EquipmentSlotsPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedEquipmentSlot" /> property's name.
        /// </summary>
        public const string SelectedEquipmentSlotPropertyName = "SelectedEquipmentSlot";

        private EquipmentSlot _selectedEquipmentSlot = null;

        /// <summary>
        /// Sets and gets the SelectedEquipmentSlot property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public EquipmentSlot SelectedEquipmentSlot
        {
            get
            {
                return _selectedEquipmentSlot;
            }

            set
            {
                if (_selectedEquipmentSlot == value)
                {
                    return;
                }

                _selectedEquipmentSlot = value;
                RaisePropertyChanged(SelectedEquipmentSlotPropertyName);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public XElement ToXML()
        {
            return new XElement("PlayerSettings",
                new XElement("Description", this.PlayerDescription.ToXML()),
                new XElement("MovementScript", this.MovementScript.ToXML()),
                new XElement("Statistics", from a in this.PlayerStatistics select a.ToXML()),
                new XElement("EquipmentSlots", from a in this.EquipmentSlots select a.ToXML()));
            
        }
        public static PlayerSettings FromXML(XElement xml)
        {
            PlayerSettings result = new PlayerSettings();
            if (xml.Element("Description") != null)
            {
                result.PlayerDescription = Script.FromXML(xml.Element("Description").Element("Script"), result.PlayerDescription);
            }
            if (xml.Element("MovementScript") != null)
            {
                result.MovementScript = Script.FromXML(xml.Element("MovementScript").Element("Script"), result.MovementScript);
            }
            if (xml.Element("Statistics") != null)
            {
                result.PlayerStatistics = new ObservableCollection<PlayerStatistic>((from a in xml.Element("Statistics").Elements() select PlayerStatistic.FromXML(a)).ToList());
            }
            if (xml.Element("EquipmentSlots") != null)
            {
                result.EquipmentSlots = new ObservableCollection<EquipmentSlot>((from a in xml.Element("EquipmentSlots").Elements() select EquipmentSlot.FromXML(a)).ToList());
            }
            return result;
        }

        public RelayCommand NewStatisticCommand { get; set; }
        public RelayCommand RemoveSelectedStatisticCommand { get; set; }
        public RelayCommand MoveUpCommand { get; set; }
        public RelayCommand MoveDownCommand { get; set; }
        public RelayCommand AddEquipmentSlotCommand { get; set; }
        public RelayCommand RemoveEquipmentSlotCommand { get; set; }

        public void NewStatistic()
        {
            var newStat = new PlayerStatistic() { Label = "New Statistic" };
            PlayerStatistics.Add(newStat);
            SelectedStatistic = newStat;
        }
        public void RemoveSelectedStatistic()
        {
            if (SelectedStatistic != null && PlayerStatistics.Contains(SelectedStatistic))
            {
                PlayerStatistics.Remove(SelectedStatistic);
                SelectedStatistic = null;
            }
        }

        public void MoveUp()
        {
            if (SelectedStatistic != null && PlayerStatistics.First() != SelectedStatistic)
            {
                int index = PlayerStatistics.IndexOf(SelectedStatistic);
                var stat = SelectedStatistic;
                PlayerStatistics.Remove(SelectedStatistic);
                PlayerStatistics.Insert(index - 1, stat);
                SelectedStatistic = null;
                SelectedStatistic = stat;
            }
        }
        public void MoveDown()
        {
            if (SelectedStatistic != null)
            {
                if (SelectedStatistic != null && PlayerStatistics.Last() != SelectedStatistic)
                {
                    int index = PlayerStatistics.IndexOf(SelectedStatistic);
                    var stat = SelectedStatistic;
                    PlayerStatistics.Remove(SelectedStatistic);
                    PlayerStatistics.Insert(index +1, stat);
                    SelectedStatistic = null;
                    SelectedStatistic = stat;
                }
            }
        }
        public void AddEquipmentSlot()
        {
            EquipmentSlots.Add(new EquipmentSlot { Name = "New Equipment Slot" });
        }
        public void RemoveEquipmentSlot()
        {
            if (SelectedEquipmentSlot != null)
            {
                EquipmentSlots.Remove(SelectedEquipmentSlot);
                foreach (var a in MainViewModel.MainViewModelStatic.Items)
                {
                    if (a.EquipmentRef != null)
                    {
                        var b = a.EquipmentRef;
                        if (b.OccupiesSlots.Contains(SelectedEquipmentSlot))
                            b.OccupiesSlots.Remove(SelectedEquipmentSlot);
                        if (b.CoversSlots.Contains(SelectedEquipmentSlot))
                            b.CoversSlots.Remove(SelectedEquipmentSlot);
                    }
                }
                SelectedEquipmentSlot = null;
            }
        }

    }
}
