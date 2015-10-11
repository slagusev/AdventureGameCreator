using Editor.ObjectTypes;
using Player.ObjectTypesWrappers;
using Player.ObjectTypeWrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player
{
    class Game : INotifyPropertyChanged
    {
        public Game()
        {
            this.CurrentlyEquippedText.CollectionChanged += (s, e) => { RaisePropertyChanged(CurrentlyEquippedTextPropertyName); };
            this.PlayerDescription.CollectionChanged += (s, e) => { RaisePropertyChanged(PlayerDescriptionPropertyName); };
            
            
        }
        public Dictionary<Guid, RoomWrapper> Rooms = new Dictionary<Guid, RoomWrapper>();
        public Dictionary<Guid, ZoneWrapper> Zones = new Dictionary<Guid, ZoneWrapper>();
        public Dictionary<Guid, VariableWrapper> VarById = new Dictionary<Guid, VariableWrapper>();
        public Dictionary<Guid, List<object>> ArraysById = new Dictionary<Guid, List<object>>();
        public Dictionary<string, VariableWrapper> VarByName = new Dictionary<string, VariableWrapper>();
        public List<ActiveEvent> ActiveEvents = new List<ActiveEvent>();
        /// <summary>
        /// The <see cref="CurrentRoom" /> property's name.
        /// </summary>
        public const string CurrentRoomPropertyName = "CurrentRoom";

        private RoomWrapper _currentRoom = null;

        /// <summary>
        /// Sets and gets the CurrentRoom property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public RoomWrapper CurrentRoom
        {
            get
            {
                return _currentRoom;
            }

            set
            {
                if (_currentRoom == value)
                {
                    return;
                }
                
                _currentRoom = value;
                value.RecalculateInteractableVisibility();
                RefreshAll();
                RaisePropertyChanged(CurrentRoomPropertyName);
            }
        }

        public void RefreshAll()
        {
            RefreshVisibleExits();
            
            RefreshPlayerDescription();
            RefreshStatistics();
            RefreshCurrentlyEquippedText();
        }

        public void RefreshVisibleExits()
        {
            if (CurrentRoom != null)
            {
                VisibleExits = new ObservableCollection<ExitWrapper>(CurrentRoom.Exits.Where(a => a.IsVisible()));
                ReorderExits();
            }
        }
        public void ReorderExits()
        {
            var exits = new ObservableCollection<ExitWrapper>();
            var exit = VisibleExits.Where(a => a.ExitBase.Direction == ExitDirection.North).FirstOrDefault();
            if (exit != null) exits.Add(exit);
            exit = VisibleExits.Where(a => a.ExitBase.Direction == ExitDirection.South).FirstOrDefault();
            if (exit != null) exits.Add(exit);
            exit = VisibleExits.Where(a => a.ExitBase.Direction == ExitDirection.East).FirstOrDefault();
            if (exit != null) exits.Add(exit);
            exit = VisibleExits.Where(a => a.ExitBase.Direction == ExitDirection.West).FirstOrDefault();
            if (exit != null) exits.Add(exit);
            foreach (var a in VisibleExits.Where(b => b.ExitBase.Direction == ExitDirection.Other))
            {
                exits.Add(a);
            }
            VisibleExits = exits;
        }

        /// <summary>
        /// The <see cref="VisibleExits" /> property's name.
        /// </summary>
        public const string VisibleExitsPropertyName = "VisibleExits";

        private ObservableCollection<ExitWrapper> _visibleExits = new ObservableCollection<ExitWrapper>();

        /// <summary>
        /// Sets and gets the VisibleExits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ExitWrapper> VisibleExits
        {
            get
            {
                return _visibleExits;
            }

            set
            {
                if (_visibleExits == value)
                {
                    return;
                }

                _visibleExits = value;
                RaisePropertyChanged(VisibleExitsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="EquippedItems" /> property's name.
        /// </summary>
        public const string EquippedItemsPropertyName = "EquippedItems";

        private Dictionary<EquipmentSlot, ItemInstance> _equippedItems = new Dictionary<EquipmentSlot, ItemInstance>();

        /// <summary>
        /// Sets and gets the EquippedItems property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Dictionary<EquipmentSlot, ItemInstance> EquippedItems
        {
            get
            {
                return _equippedItems;
            }

            set
            {
                if (_equippedItems == value)
                {
                    return;
                }

                _equippedItems = value;
                RaisePropertyChanged(EquippedItemsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Exits" /> property's name.
        /// </summary>
        public const string ExitsPropertyName = "Exits";

        private ObservableCollection<ExitWrapper> _exits = new ObservableCollection<ExitWrapper>();

        /// <summary>
        /// Sets and gets the Exits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ExitWrapper> Exits
        {
            get
            {
                return _exits;
            }

            set
            {
                if (_exits == value)
                {
                    return;
                }

                _exits = value;
                RaisePropertyChanged(ExitsPropertyName);
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

        public Editor.ObjectTypes.PlayerSettings Settings;
        public static Game FromXml(XElement xml)
        {
            Game g = new Game();
            Editor.MainViewModel mvm = Editor.MainViewModel.FromXML(xml);
            g.Settings = mvm.Settings;
            foreach (var zone in mvm.Zones)
            {
                g.Zones.Add(zone.ZoneId, new ZoneWrapper(zone));
                foreach (var room in zone.Rooms)
                {
                    var roomWrapper = new RoomWrapper(room);
                    g.Rooms.Add(room.RoomID, roomWrapper);

                }
            }
            foreach (var v in mvm.Variables)
            {
                var wrapper = new VariableWrapper(v);
                g.VarById[v.Id] = wrapper;
                g.VarByName[v.Name] = wrapper;
            }
            foreach (var stat in mvm.Settings.PlayerStatistics)
            {
                g.Statistics.Add(new PlayerStatisticWrapper(stat));
            }
            foreach (var a in mvm.Settings.EquipmentSlots)
            {
                g.EquippedItems.Add(a, null);
            }
            foreach (var a in mvm.Arrays)
            {
                g.ArraysById.Add(a.Id, new List<object>());
            }
            return g;

        }
        public void RefreshPlayerDescription()
        {
            var wrapper = new ScriptWrapper(Settings.PlayerDescription);
            wrapper.Execute();
            PlayerDescription.Clear();
            foreach (var res in wrapper.TextResult)
            {
                PlayerDescription.Add(res);
            }
        }
        public void RefreshCurrentlyEquippedText()
        {
            CurrentlyEquippedText.Clear();
            if (this.EquippedItems.Count() > 0)
            {
                CurrentlyEquippedText.Add("Equipped Items:");
                //CurrentlyEquippedText.Add("\n\n");
                ObservableCollection<object> equipped = new ObservableCollection<object>();
                foreach (var a in this.EquippedItems)
                {
                    equipped.Add(a.Key.Name + ": ");
                    if (a.Value != null && a.Value.CurrentIconPath != null && !string.IsNullOrWhiteSpace(a.Value.CurrentIconPath.Path))
                    {
                        equipped.Add(new ImageRef(a.Value.CurrentIconPath, 30, 30));
                    }
                    equipped.Add(a.Value != null ? a.Value.CurrentName : "Nothing");
                    equipped.Add("\n");
                }
                CurrentlyEquippedText.Add(equipped);
            }
        }
        /// <summary>
        /// The <see cref="CurrentlyEquippedText" /> property's name.
        /// </summary>
        public const string CurrentlyEquippedTextPropertyName = "CurrentlyEquippedText";

        private ObservableCollection<object> _currentlyEquippedText = new ObservableCollection<object>();

        /// <summary>
        /// Sets and gets the CurrentlyEquippedText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<object> CurrentlyEquippedText
        {
            get
            {
                return _currentlyEquippedText;
            }

            set
            {
                if (_currentlyEquippedText == value)
                {
                    return;
                }

                _currentlyEquippedText = value;
                RaisePropertyChanged(CurrentlyEquippedTextPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="PlayerDescription" /> property's name.
        /// </summary>
        public const string PlayerDescriptionPropertyName = "PlayerDescription";

        private ObservableCollection<object> _playerDescription = new ObservableCollection<object>();

        /// <summary>
        /// Sets and gets the PlayerDescription property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<object> PlayerDescription
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
        /// The <see cref="PlayerInventory" /> property's name.
        /// </summary>
        public const string PlayerInventoryPropertyName = "PlayerInventory";

        private ObservableCollection<ItemInstance> _inventory = new ObservableCollection<ItemInstance>();

        /// <summary>
        /// Sets and gets the PlayerInventory property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ItemInstance> PlayerInventory
        {
            get
            {
                return _inventory;
            }

            set
            {
                if (_inventory == value)
                {
                    return;
                }

                _inventory = value;
                RaisePropertyChanged(PlayerInventoryPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Statistics" /> property's name.
        /// </summary>
        public const string StatisticsPropertyName = "Statistics";

        private ObservableCollection<PlayerStatisticWrapper> _playerStatisticsWrapper = new ObservableCollection<PlayerStatisticWrapper>();

        /// <summary>
        /// Sets and gets the Statistics property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<PlayerStatisticWrapper> Statistics
        {
            get
            {
                return _playerStatisticsWrapper;
            }

            set
            {
                if (_playerStatisticsWrapper == value)
                {
                    return;
                }

                _playerStatisticsWrapper = value;
                RaisePropertyChanged(StatisticsPropertyName);
            }
        }
        public void RefreshStatistics()
        {
            foreach (var stat in this.Statistics)
            {
                stat.ResetValues();
                
            }
        }

        public List<ItemInstance> TryEquipItem(ItemInstance i, bool force = false)
        {
            List<ItemInstance> markedForRemoval = new List<ItemInstance>();
            foreach (var a in EquippedItems.Where(b => b.Value != null).Select(b => b.Value).Distinct())
            {
                var equip = a.item.EquipmentRef;
                foreach (var slot in i.item.EquipmentRef.OccupiesSlots)
                {
                    if (equip.OccupiesSlots.Contains(slot))
                    {
                        if (!force)
                        {
                            var result = new ScriptWrapper(equip.OnUnequip) { ItemBase = a }.Execute();
                            if (result != false)
                                markedForRemoval.Add(a);
                            else
                            {
                                MainViewModel.WriteText("Unable to equip " + i.CurrentName + " because " + a.CurrentName + " could not be removed!", null);
                                return new List<ItemInstance>();
                            }
                        }
                        else
                        {
                            markedForRemoval.Add(a);
                        }
                    }
                    if (!force && equip.CoversSlots.Contains(slot))
                    {
                        var result = new ScriptWrapper(equip.OnUnequip) { ItemBase = a }.Execute();
                        if (result == false)
                        {
                            MainViewModel.WriteText("Unable to equip " + i.CurrentName + " because " + a.CurrentName + " is covering it and could not be removed!", null);
                            return new List<ItemInstance>();
                        }
                    }
                }
            }
            {
                var result = new ScriptWrapper(i.item.EquipmentRef.OnEquip) { ItemBase = i }.Execute();
                if (result == false)
                {
                    MainViewModel.WriteText("Unable to unequip " + i.CurrentName + " because it is currently unable to be equipped!", null);
                    return new List<ItemInstance>();
                }
            }
            foreach (var a in markedForRemoval)
            {
                foreach (var slot in a.item.EquipmentRef.OccupiesSlots)
                {
                    EquippedItems[slot] = null;
                }
            }
            foreach (var slot in i.item.EquipmentRef.OccupiesSlots)
            {
                EquippedItems[slot] = i;
            }
            if (PlayerInventory.Contains(i)) PlayerInventory.Remove(i);
            return markedForRemoval;
        }
        public bool TryUnequipItem(ItemInstance i, bool force = false)
        {
            if (EquippedItems.Where(a => a.Value == i).Count() == 0)
            {
                MainViewModel.WriteText("ERROR: Item not equipped!", null);
            }
            if (!force)
            {
                foreach (var slot in i.item.EquipmentRef.OccupiesSlots)
                {
                    foreach (var a in EquippedItems.Where(b => b.Value != null && b.Value != i).Select(b => b.Value).Distinct())
                    {
                        var equip = a.item.EquipmentRef;
                        if (equip.CoversSlots.Contains(slot))
                        {
                            var result = new ScriptWrapper(equip.OnUnequip) { ItemBase = a }.Execute();
                            if (result == false)
                            {
                                MainViewModel.WriteText("Unable to unequip " + i.CurrentName + " because " + a.CurrentName + " is covering it and could not be removed!", null);
                                return false;
                            }
                        }
                    }
                }
                {
                    var result = new ScriptWrapper(i.item.EquipmentRef.OnUnequip) { ItemBase = i }.Execute();
                    if (result == false)
                    {
                        MainViewModel.WriteText("Unable to unequip " + i.CurrentName + " because it is currently unable to be removed!", null);
                        return false;
                    }
                }
            }
            
            List<EquipmentSlot> slots = EquippedItems.Where(a => a.Value == i).Select(a => a.Key).ToList();
            foreach (var a in slots)
            {
                EquippedItems[a] = null;
                
            }
            return true;
        }
        public void RunActiveEvents()
        {
            List<ActiveEvent> events = new List<ActiveEvent>();
            foreach (var a in ActiveEvents)
            {
                events.Add(a);
            }
            foreach (var a in events)
            {
                a.Execute();
            }
            ////Compare lists to see if this needs to be run again.
            //if (ActiveEvents.Count() != events.Count())
            //{
            //    RunActiveEvents();
            //}
            //else
            //{
                
            //    for (int i = 0; i < ActiveEvents.Count; i++)
            //    {
            //        if (ActiveEvents[i] != events[i])
            //        {
            //            RunActiveEvents();
            //            break;
            //        }
            //    }
            //}
            RefreshAll();
        }
    }
}
