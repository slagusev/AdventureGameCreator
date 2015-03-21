using Editor.ObjectTypes;
using Editor.Scripter;
using Editor.Scripter.Misc;
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
    public class MainViewModel : INotifyPropertyChanged
    {
        public static MainViewModel MainViewModelStatic;

        public MainViewModel()
        {
            /*
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
            */
            

            MainViewModelStatic = this;
            //this.OpenWindows.Add(new WindowView { Content = new Editor.Editors.InteractableEditor() });
            #region More debug data
            /*
            var TestInteractable = new Interactable();
            this.Interactables.Add(TestInteractable);
            TestInteractable.InteractableName = "Bookshelf";
            TestInteractable.DefaultDisplayName = "Old Bookshelf";
            TestInteractable.CanExamine = true;
            TestInteractable.CanExamineUsesScript = true;
            TestInteractable.CanInteract = true;
            TestInteractable.CanInteractUsesScript = false;
            Script testIntCanExScript = new Script();
            testIntCanExScript.AddBeforeSelected(new Comment { CommentText = "Test Can Examine Script" });
            Script testIntCanInScript = new Script();
            testIntCanInScript.AddBeforeSelected(new Comment { CommentText = "Test Can Interact Script" });
            Script testIntExScript = new Script();
            testIntExScript.AddBeforeSelected(new Comment { CommentText = "Test Examine Script" });
            Script testIntInScript = new Script();
            testIntInScript.AddBeforeSelected(new Comment { CommentText = "Test Interaction Script" });
            TestInteractable.ExamineScript = testIntExScript;
            TestInteractable.InteractScript = testIntInScript;
            TestInteractable.CanInteractScript = testIntCanInScript;
            TestInteractable.CanExamineScript = testIntCanExScript;
            TestInteractable.GroupName = "Furniture";

            Variables.Add(TestData.TestDateTimeVariable);
            Variables.Add(TestData.TestStringVariable);
            Variables.Add(TestData.TestNumberVariable);
             * */
            #endregion
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
        /// The <see cref="InteractableGroups" /> property's name.
        /// </summary>
        public const string InteractableGroupsPropertyName = "InteractableGroups";

        private ObservableCollection<InteractableGroup> _interactableGroups = new ObservableCollection<InteractableGroup>();

        /// <summary>
        /// Sets and gets the InteractableGroups property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<InteractableGroup> InteractableGroups
        {
            get
            {
                return _interactableGroups;
            }

            set
            {
                if (_interactableGroups == value)
                {
                    return;
                }

                _interactableGroups = value;
                RaisePropertyChanged(InteractableGroupsPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Interactables" /> property's name.
        /// </summary>
        public const string InteractablesPropertyName = "Interactables";

        private ObservableCollection<Interactable> _interactables = new ObservableCollection<Interactable>();

        /// <summary>
        /// Sets and gets the Interactables property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Interactable> Interactables
        {
            get
            {
                return _interactables;
            }

            set
            {
                if (_interactables == value)
                {
                    return;
                }

                _interactables = value;
                RaisePropertyChanged(InteractablesPropertyName);
            }
        }

        public void RecalculateInteractableGroups()
        {
            foreach (var a in InteractableGroups)
            {
                a.Interactables.Clear();
            }
            foreach (var a in Interactables)
            {
                var group = InteractableGroups.Where(b => a.GroupName == b.Name).FirstOrDefault();
                if (group == null)
                {
                    group = new InteractableGroup { Name = a.GroupName };
                    InteractableGroups.Add(group);
                }
                group.Interactables.Add(a);
            }
        }


        /// <summary>
        /// The <see cref="ItemClasses" /> property's name.
        /// </summary>
        public const string ItemClassesPropertyName = "ItemClasses";

        private ObservableCollection<ItemClass> _itemClasses = new ObservableCollection<ItemClass>();

        /// <summary>
        /// Sets and gets the ItemClasses property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ItemClass> ItemClasses
        {
            get
            {
                return _itemClasses;
            }

            set
            {
                if (_itemClasses == value)
                {
                    return;
                }

                _itemClasses = value;
                RaisePropertyChanged(ItemClassesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Items" /> property's name.
        /// </summary>
        public const string ItemsPropertyName = "Items";

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        /// <summary>
        /// Sets and gets the Items property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Item> Items
        {
            get
            {
                return _items;
            }

            set
            {
                if (_items == value)
                {
                    return;
                }

                _items = value;
                RaisePropertyChanged(ItemsPropertyName);
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

        /// <summary>
        /// The <see cref="Variables" /> property's name.
        /// </summary>
        public const string VariablesPropertyName = "Variables";

        private ObservableCollection<Variable> _variables = new ObservableCollection<Variable>();

        /// <summary>
        /// Sets and gets the Variables property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Variable> Variables
        {
            get
            {
                //Test if sorted
                bool sorted = true;
                Variable last = null;
                foreach (var a in _variables)
                {
                    if (last != null && a.Name.CompareTo(last.Name) <= 0)
                    {
                        sorted = false;
                    }
                    
                    last = a;
                    
                }
                if (!sorted)
                {
                    _variables = new ObservableCollection<Variable>(_variables.OrderBy(a => a.Name));
                    RaisePropertyChanged(VariablesPropertyName);
                }
                return _variables;
            }

            set
            {
                if (_variables == value)
                {
                    return;
                }

                _variables = value;
                RaisePropertyChanged(VariablesPropertyName);
            }
        }

        public XElement ToXML()
        {
            return new XElement("Game",
                new XElement("Zones", from a in Zones select a.ToXML()),
                new XElement("Interactables", from a in Interactables select a.ToXML()),
                new XElement("Variables", from a in Variables select a.ToXml()),
                new XElement("ItemClasses", from a in ItemClasses select a.ToXML()),
                new XElement("Items", from a in Items select a.ToXml())
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
            XElement interactables = xml.Element("Interactables");
            if (interactables != null)
            {
                mvm.Interactables = new ObservableCollection<Interactable>(from a in interactables.Elements("Interactable") select Interactable.FromXML(a));
            }
            XElement variables = xml.Element("Variables");
            if (variables != null)
            {
                mvm.Variables = new ObservableCollection<Variable>(from a in variables.Elements("Variable") select Variable.FromXML(a));
            }
            mvm.InteractableGroups.Clear();
            mvm.RecalculateInteractableGroups();
            XElement itemClasses = xml.Element("ItemClasses");
            mvm.ItemClasses = new ObservableCollection<ItemClass>();
            if (itemClasses != null)
            {
                foreach (var a in itemClasses.Elements("ItemClass"))
                {
                    mvm.ItemClasses.Add(ItemClass.FromXML(a));
                } 
            }
            XElement items = xml.Element("Items");
            mvm.Items = new ObservableCollection<Item>();
            if (items != null)
            {
                foreach (var a in items.Elements("Item"))
                {
                    mvm.Items.Add(Item.FromXml(a));
                }
            }
            //Resolve all interactable references
            //foreach (var a in mvm.InteractableRefStack)
            //{
            //    var guid = a.Value;
            //    var list = a.Key;
            //    var interactable = mvm.Interactables.Where(i => i.InteractableID == guid).FirstOrDefault();
            //}
            return mvm;
        }
        public List<KeyValuePair<ObservableCollection<Interactable>, Guid>> InteractableRefStack = new List<KeyValuePair<ObservableCollection<Interactable>, Guid>>();

        public void DeleteRoom(Room r)
        {

            //delete all exits
            foreach (var zone in Zones)
            {
                foreach (var room in zone.Rooms)
                {
                    Exit exitForDeletion = null;
                    foreach (var exit in room.Exits)
                    {
                        if (exit.RoomLink == r)
                        {
                            exitForDeletion = exit;
                            break;
                        }
                    }
                    if (exitForDeletion != null) room.Exits.Remove(exitForDeletion);
                }
            }
            //delete from zone
            foreach (var zone in Zones)
            {
                if (zone.Rooms.Contains(r))
                {
                    zone.Rooms.Remove(r);
                }
            }
            List<WindowView> vws = new List<WindowView>();

            foreach (var vw in this.OpenWindows)
            {
                if (vw.Content.DataContext == r)
                {
                    vws.Add(vw);
                }
            }
            
            foreach (var vw in vws)
            {
                this.OpenWindows.Remove(vw);
            }
        }
        
    }
}
