using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Editor
{
    class MainViewModel : INotifyPropertyChanged
    {
        public static MainViewModel MainViewModelStatic;

        public MainViewModel()
        {
#region Debug Data
            this.Zones = new ObservableCollection<Zone>();
            Zone playerHouse = new Zone
            {
                ZoneName = "Player House",
                ZoneId = Guid.NewGuid()
            };
            var bedroom = new Room
            {
                RoomName = "Bedroom",
                RoomID = Guid.NewGuid()
            };
            playerHouse.Rooms.Add(bedroom);
            var hallway = new Room
            {
                RoomName = "Hallway",
                RoomID = Guid.NewGuid()
            };
            
            playerHouse.Rooms.Add(hallway);
            playerHouse.Rooms.Add(new Room
            {
                RoomName = "Living Room",
                RoomID = Guid.NewGuid()
            });
            Zones.Add(playerHouse);
            Zone mall = new Zone
            {
                ZoneName = "Mall",
                ZoneId = Guid.NewGuid()
            };
            mall.Rooms.Add(new Room
            {
                RoomName = "West Entrance",
                RoomID = Guid.NewGuid()
            });
            mall.Rooms.Add(new Room
            {
                RoomName = "East Entrance",
                RoomID = Guid.NewGuid()
            }); 
            mall.Rooms.Add(new Room
            {
                RoomName = "Food Court",
                RoomID = Guid.NewGuid()
            });
            mall.Rooms.Add(new Room
            {
                RoomName = "Department Store",
                RoomID = Guid.NewGuid()
            });
            mall.Rooms.Add(new Room
            {
                RoomName = "Book Store",
                RoomID = Guid.NewGuid()
            }); 
            this.Zones.Add(mall);
            bedroom.Exits.Add(new Exit { ZoneLink = playerHouse, RoomLink = hallway, Direction = ExitDirection.West });
            hallway.Exits.Add(new Exit { ZoneLink = playerHouse, RoomLink = bedroom, Direction = ExitDirection.East });
#endregion



            MainViewModelStatic = this;
        }

        /// <summary>
        /// The <see cref="Zones" /> property's name.
        /// </summary>
        public const string ZonesPropertyName = "Zones";

        private ObservableCollection<Zone> _zones = new ObservableCollection<Zone>();

        /// <summary>
        /// Sets and gets the Zones property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ObjectTypes.Zone> Zones
        {
            get
            {
                return _zones;
            }

            set
            {
                if (_zones == value)
                {
                    return;
                }

                _zones = value;
                RaisePropertyChanged(ZonesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OpenWindows" /> property's name.
        /// </summary>
        public const string OpenWindowsPropertyName = "OpenWindows";

        private ObservableCollection<WindowView> _openWindows = new ObservableCollection<WindowView>();

        /// <summary>
        /// Sets and gets the OpenWindows property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<WindowView> OpenWindows
        {
            get
            {
                return _openWindows;
            }

            set
            {
                if (_openWindows == value)
                {
                    return;
                }

                _openWindows = value;
                RaisePropertyChanged(OpenWindowsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedTab" /> property's name.
        /// </summary>
        public const string SelectedTabPropertyName = "SelectedTab";

        private int _tabItem = -1;

        /// <summary>
        /// Sets and gets the SelectedTab property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int SelectedTab
        {
            get
            {
                return _tabItem;
            }

            set
            {
                if (_tabItem == value)
                {
                    return;
                }

                _tabItem = value;
                RaisePropertyChanged(SelectedTabPropertyName);
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

        /// <summary>
        /// The <see cref="Location" /> property's name.
        /// </summary>
        public const string LocationPropertyName = "Location";

        private string _location = "";

        /// <summary>
        /// Sets and gets the Location property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                if (_location == value)
                {
                    return;
                }

                _location = value;
                RaisePropertyChanged(LocationPropertyName);
            }
        }

        public XElement ToXML()
        {
            return new XElement("Game",
                new XElement("Zones", from a in Zones select a.ToXML())
                );
        }

        public static MainViewModel FromXML(XElement xml)
        {
            MainViewModel mvm = new MainViewModel();

            XElement zones = xml.Element("Zones");
            if (zones != null)
            {
                mvm.Zones = new ObservableCollection<Zone>(from a in zones.Elements("Zone") select Zone.FromXML(a));
            }

            //Link the exits.
            foreach (var sourceZone in mvm.Zones)
            {
                foreach (var sourceRoom in sourceZone.Rooms)
                {
                    foreach (var exit in sourceRoom.Exits)
                    {
                        foreach (var destZone in mvm.Zones)
                        {
                            if (exit.ZoneID == destZone.ZoneId)
                            {
                                foreach (var destRoom in destZone.Rooms)
                                {
                                    if (exit.RoomID == destRoom.RoomID)
                                    {
                                        exit.ZoneLink = destZone;
                                        exit.RoomLink = destRoom;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return mvm;
        }
    }
}
