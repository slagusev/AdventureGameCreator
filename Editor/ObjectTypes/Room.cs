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
    public class Room : INotifyPropertyChanged
    {

        public Room()
        {
            NewExitCommand = new RelayCommand(NewExit);
            RemoveExitCommand = new RelayCommand(RemoveExit);
            LinkExitCommand = new RelayCommand(LinkExit);
            OpenSelectedExitCommand = new RelayCommand(OpenSelectedExit);

            this.RoomDescriptionScript.CanReturn = false;
        }

        /// <summary>
        /// The <see cref="RoomName" /> property's name.
        /// </summary>
        public const string RoomNamePropertyName = "RoomName";

        private string _roomName = "";

        /// <summary>
        /// Sets and gets the RoomName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string RoomName
        {
            get
            {
                return _roomName;
            }

            set
            {
                if (_roomName == value)
                {
                    return;
                }

                _roomName = value;
                RaisePropertyChanged(RoomNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Exits" /> property's name.
        /// </summary>
        public const string ExitsPropertyName = "Exits";

        private ObservableCollection<Exit> _exits = new ObservableCollection<Exit>();

        /// <summary>
        /// Sets and gets the Exits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Exit> Exits
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


        /// <summary>
        /// The <see cref="SelectedExit" /> property's name.
        /// </summary>
        public const string SelectedExitPropertyName = "SelectedExit";

        private Exit _selectedExit = null;

        /// <summary>
        /// Sets and gets the SelectedExit property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Exit SelectedExit
        {
            get
            {
                return _selectedExit;
            }

            set
            {
                if (value == null)
                {
                    PossibleDirections = new ObservableCollection<ExitDirection>();
                }
                else
                {
                    PossibleDirections = new ObservableCollection<ExitDirection>();
                    var exitsMinusSelected = Exits.Where(a => a != value);
                    if (exitsMinusSelected.Where(a => a.Direction == ExitDirection.North).Count() == 0)
                    {
                        PossibleDirections.Add(ExitDirection.North);
                    }
                    if (exitsMinusSelected.Where(a => a.Direction == ExitDirection.South).Count() == 0)
                    {
                        PossibleDirections.Add(ExitDirection.South);
                    }
                    if (exitsMinusSelected.Where(a => a.Direction == ExitDirection.East).Count() == 0)
                    {
                        PossibleDirections.Add(ExitDirection.East);
                    }
                    if (exitsMinusSelected.Where(a => a.Direction == ExitDirection.West).Count() == 0)
                    {
                        PossibleDirections.Add(ExitDirection.West);
                    }
                    PossibleDirections.Add(ExitDirection.Other);
                }
                if (_selectedExit == value)
                {
                    return;
                }

                _selectedExit = value;
                RaisePropertyChanged(SelectedExitPropertyName);
                
            }
        }
        
        /// <summary>
        /// The <see cref="PossibleDirections" /> property's name.
        /// </summary>
        public const string PossibleDirectionsPropertyName = "PossibleDirections";

        private ObservableCollection<ExitDirection> _possibleDirections = new ObservableCollection<ExitDirection>();

        /// <summary>
        /// Sets and gets the PossibleDirections property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ExitDirection> PossibleDirections
        {
            get
            {
                return _possibleDirections;
            }

            set
            {
                if (_possibleDirections == value)
                {
                    return;
                }

                _possibleDirections = value;
                RaisePropertyChanged(PossibleDirectionsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RoomID" /> property's name.
        /// </summary>
        public const string RoomIDPropertyName = "RoomID";

        private Guid _roomId = new Guid();

        /// <summary>
        /// Sets and gets the RoomID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid RoomID
        {
            get
            {
                return _roomId;
            }

            set
            {
                if (_roomId == value)
                {
                    return;
                }

                _roomId = value;
                RaisePropertyChanged(RoomIDPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = "Enter your room description here. This is what is displayed to the player upon entering the room. Enclose variable names with {{}} to replace text with variable names. For example, {{health}} would display a player's health.";

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="HasScriptDescription" /> property's name.
        /// </summary>
        public const string HasScriptDescriptionPropertyName = "HasScriptDescription";

        private bool _hasScriptDescription = false;

        /// <summary>
        /// Sets and gets the HasScriptDescription property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool HasScriptDescription
        {
            get
            {
                return _hasScriptDescription;
            }

            set
            {
                if (_hasScriptDescription == value)
                {
                    return;
                }

                _hasPlaintextDescription = !value;
                _hasScriptDescription = value;
                RaisePropertyChanged(HasPlaintextDescriptionPropertyName);
                RaisePropertyChanged(HasScriptDescriptionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="HasPlaintextDescription" /> property's name.
        /// </summary>
        public const string HasPlaintextDescriptionPropertyName = "HasPlaintextDescription";

        private bool _hasPlaintextDescription = true;

        /// <summary>
        /// Sets and gets the HasPlaintextDescription property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool HasPlaintextDescription
        {
            get
            {
                return _hasPlaintextDescription;
            }

            set
            {
                if (_hasPlaintextDescription == value)
                {
                    return;
                }

                _hasPlaintextDescription = value;
                _hasScriptDescription = !value;
                RaisePropertyChanged(HasPlaintextDescriptionPropertyName);
                RaisePropertyChanged(HasScriptDescriptionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RoomDescriptionScript" /> property's name.
        /// </summary>
        public const string RoomDescriptionScriptPropertyName = "RoomDescriptionScript";

        private Scripter.Script _roomDescriptionScript = new Scripter.Script();

        /// <summary>
        /// Sets and gets the RoomDescriptionScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script RoomDescriptionScript
        {
            get
            {
                return _roomDescriptionScript;
            }

            set
            {
                if (_roomDescriptionScript == value)
                {
                    return;
                }

                _roomDescriptionScript = value;
                RaisePropertyChanged(RoomDescriptionScriptPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DefaultInteractables" /> property's name.
        /// </summary>
        public const string DefaultInteractablesPropertyName = "DefaultInteractables";

        private ObservableCollection<InteractableRef> _defaultInteractables = new ObservableCollection<InteractableRef>();

        /// <summary>
        /// Sets and gets the DefaultInteractables property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<InteractableRef> DefaultInteractables
        {
            get
            {
                return _defaultInteractables;
            }

            set
            {
                if (_defaultInteractables == value)
                {
                    return;
                }

                _defaultInteractables = value;
                RaisePropertyChanged(DefaultInteractablesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StartingRoom" /> property's name.
        /// </summary>
        public const string StartingRoomPropertyName = "StartingRoom";

        private bool _startingRoom = false;

        /// <summary>
        /// Sets and gets the StartingRoom property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool StartingRoom
        {
            get
            {
                return _startingRoom;
            }

            set
            {
                if (_startingRoom == value)
                {
                    return;
                }

                _startingRoom = value;
                RaisePropertyChanged(StartingRoomPropertyName);
                if (value)
                {
                    foreach (var zone in MainViewModel.MainViewModelStatic.Zones)
                    {
                        foreach (var room in zone.Rooms)
                        {
                            if (room != this)
                            {
                                room.StartingRoom = false;
                            }
                        }
                    }
                }
            }
        }
        ///// <summary>
        ///// The <see cref="SelectedInteractable" /> property's name.
        ///// </summary>
        //public const string SelectedInteractablePropertyName = "SelectedInteractable";

        //private Interactable _selectedInteractable = null;

        ///// <summary>
        ///// Sets and gets the SelectedInteractable property.
        ///// Changes to that property's value raise the PropertyChanged event.
        ///// </summary>
        //public Interactable SelectedInteractable
        //{
        //    get
        //    {
        //        return _selectedInteractable;
        //    }

        //    set
        //    {
        //        if (_selectedInteractable == value)
        //        {
        //            return;
        //        }

        //        _selectedInteractable = value;
        //        RaisePropertyChanged(SelectedInteractablePropertyName);
        //    }
        //}



        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public XElement ToXML()
        {
            return new XElement("Room",
                new XElement("Name", RoomName),
                new XElement("Id", RoomID),
                new XElement("Exits", from a in Exits select a.ToXML()),
                new XElement("TextDescription", Description),
                new XElement("HasScriptDescription", HasScriptDescription),
                new XElement("ScriptDescription", RoomDescriptionScript.ToXML()),
                new XElement("StartingRoom", StartingRoom),
                new XElement("DefaultInteractables", (from a in DefaultInteractables select new XElement("InteractableRef", a.LinkedInteractableId)))
                );

        }

        public static Room FromXML(XElement xml)
        {
            Room r = new Room();
            if (xml.Element("Name") != null)
            {
                r.RoomName = xml.Element("Name").Value;
            }
            if (xml.Element("Id") != null)
            {
                r.RoomID = Guid.Parse(xml.Element("Id").Value);
            }
            else
            {
                r.RoomID = new Guid();
            }
            if (xml.Element("Exits") != null)
            {
                r.Exits = new ObservableCollection<Exit>(from a in xml.Element("Exits").Elements("Exit") select Exit.FromXML(a));
            }
            if (xml.Element("TextDescription") != null)
            {
                r.Description = xml.Element("TextDescription").Value;
            }
            if (xml.Element("HasScriptDescription") != null)
            {
                r.HasScriptDescription = Convert.ToBoolean(xml.Element("HasScriptDescription").Value);
                r.HasPlaintextDescription = !r.HasScriptDescription;
            }
            if (xml.Element("ScriptDescription") != null)
            {
                r.RoomDescriptionScript = Script.FromXML(xml.Element("ScriptDescription").Element("Script"), r.RoomDescriptionScript);
            }
            if (xml.Element("StartingRoom") != null)
            {
                r.StartingRoom = Convert.ToBoolean(xml.Element("StartingRoom").Value);
            }
            if (xml.Element("DefaultInteractables") != null)
            {
                foreach (var a in xml.Element("DefaultInteractables").Elements("InteractableRef"))
                {
                    r.DefaultInteractables.Add(new InteractableRef(Guid.Parse(a.Value)));
                }
            }
            return r;
        }

        #region Commands
        public RelayCommand NewExitCommand {get; private set;}
        public RelayCommand RemoveExitCommand { get; private set; }
        public RelayCommand LinkExitCommand { get; private set; }
        public RelayCommand OpenSelectedExitCommand { get; private set; }
        #endregion
        #region Command Methods
        public void NewExit()
        {
            var newExit = new Exit();
            newExit.ZoneLink = MainViewModel.MainViewModelStatic.Zones.Where(a => a.Rooms.Contains(this)).FirstOrDefault();
            newExit.RoomLink = this;
            Exits.Add(newExit);
            SelectedExit = newExit;
        }
        public void RemoveExit()
        {
            if (SelectedExit != null)
                Exits.Remove(SelectedExit);
            SelectedExit = null;
        }
        public void LinkExit()
        {
            if (SelectedExit != null)
            {
                ExitDirection dir = SelectedExit.Direction;
                ExitDirection destDir = ExitDirection.Other;
                switch (dir)
                {
                    case ExitDirection.North:
                        destDir = ExitDirection.South;
                        break;
                    case ExitDirection.South:
                        destDir = ExitDirection.North;
                        break;
                    case ExitDirection.East:
                        destDir = ExitDirection.West;
                        break;
                    case ExitDirection.West:
                        destDir = ExitDirection.East;
                        break;
                }
                if (destDir != ExitDirection.Other)
                {
                    if (SelectedExit.RoomLink.Exits.Where(a => a.Direction == destDir).Count() == 0)
                    {
                        Zone z = MainViewModel.MainViewModelStatic.Zones.Where(a => a.Rooms.Contains(this)).First();
                        SelectedExit.RoomLink.Exits.Add(new Exit
                        {
                            Direction = destDir,
                            ZoneLink = z,
                            RoomLink = this
                        });
                        System.Windows.Forms.MessageBox.Show("New exit created in " + SelectedExit.RoomLink.RoomName + ":\n\n" + destDir.ToString() + " - " + z.ZoneName + " - " + this.RoomName);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to link exit: An exit already exists in target room with the direction '" + destDir.ToString() + "'.");
                    }
                }
                else
                {

                    if (SelectedExit.RoomLink.Exits.Where(a => a.RoomLink == this && a.Direction == destDir).Count() == 0)
                    {
                        Zone z = MainViewModel.MainViewModelStatic.Zones.Where(a => a.Rooms.Contains(this)).First();
                        SelectedExit.RoomLink.Exits.Add(new Exit
                        {
                            Direction = destDir,
                            ZoneLink = z,
                            RoomLink = this
                        });
                        System.Windows.Forms.MessageBox.Show("New exit created in " + SelectedExit.RoomLink.RoomName + ":\n\n" + destDir.ToString() + " - " + z.ZoneName + " - " + this.RoomName);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to link exit: A matching exit already exists.");
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a room, first.");
            }
        }
        public void OpenSelectedExit()
        {
            if (SelectedExit != null)
            {
                var tabs = MainViewModel.MainViewModelStatic.OpenWindows.Where(a => a.Content.DataContext == SelectedExit.RoomLink);
                if (tabs.Count() == 0)
                {
                    var wv = new WindowView
                    {
                        Content = new Editors.RoomEditor
                        {
                            DataContext = SelectedExit.RoomLink
                        },
                        TabName = "Room - " + SelectedExit.RoomLink.RoomName
                    };
                    MainViewModel.MainViewModelStatic.OpenWindows.Add(wv);
                    MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(wv);
                }
                else
                {
                    MainViewModel.MainViewModelStatic.SelectedTab = MainViewModel.MainViewModelStatic.OpenWindows.IndexOf(tabs.FirstOrDefault());
                }
            }
        }
        #endregion

    }
}
