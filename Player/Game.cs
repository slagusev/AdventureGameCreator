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

        public Dictionary<Guid, RoomWrapper> Rooms = new Dictionary<Guid, RoomWrapper>();
        public Dictionary<Guid, ZoneWrapper> Zones = new Dictionary<Guid, ZoneWrapper>();
        public Dictionary<Guid, VariableWrapper> VarById = new Dictionary<Guid, VariableWrapper>();
        public Dictionary<string, VariableWrapper> VarByName = new Dictionary<string, VariableWrapper>();
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
                RaisePropertyChanged(CurrentRoomPropertyName);
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


        public static Game FromXml(XElement xml)
        {
            Game g = new Game();
            Editor.MainViewModel mvm = Editor.MainViewModel.FromXML(xml);
            foreach (var zone in mvm.Zones)
            {
                g.Zones.Add(zone.ZoneId, new ZoneWrapper(zone));
                foreach (var room in zone.Rooms)
                {
                    var roomWrapper = new RoomWrapper(room);
                    g.Rooms.Add(room.RoomID, roomWrapper);
                    if (room.StartingRoom)
                        g.CurrentRoom = roomWrapper;
                }
            }
            foreach (var v in mvm.Variables)
            {
                g.VarById[v.Id] = new VariableWrapper(v);
                g.VarByName[v.Name] = new VariableWrapper(v);
            }
            return g;

        }
    }
}
