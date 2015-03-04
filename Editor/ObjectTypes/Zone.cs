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
    public class Zone : INotifyPropertyChanged
    {
        public Zone()
        {
            NewRoomCommand = new RelayCommand(NewRoom);
            DeleteRoomCommand = new RelayCommand(DeleteSelectedRoom);
        }
        /// <summary>
        /// The <see cref="ZoneName" /> property's name.
        /// </summary>
        public const string ZoneNamePropertyName = "ZoneName";

        private string _zoneName = "";

        /// <summary>
        /// Sets and gets the ZoneName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string ZoneName
        {
            get
            {
                return _zoneName;
            }

            set
            {
                if (_zoneName == value)
                {
                    return;
                }

                _zoneName = value;
                RaisePropertyChanged(ZoneNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ZoneId" /> property's name.
        /// </summary>
        public const string ZoneIdPropertyName = "ZoneId";

        private Guid _zoneId = new Guid();

        /// <summary>
        /// Sets and gets the ZoneId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid ZoneId
        {
            get
            {
                return _zoneId;
            }

            set
            {
                if (_zoneId == value)
                {
                    return;
                }

                _zoneId = value;
                RaisePropertyChanged(ZoneIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Rooms" /> property's name.
        /// </summary>
        public const string RoomsPropertyName = "Rooms";

        private ObservableCollection<Room> _rooms = new ObservableCollection<Room>();

        /// <summary>
        /// Sets and gets the Rooms property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Room> Rooms
        {
            get
            {
                return _rooms;
            }

            set
            {
                if (_rooms == value)
                {
                    return;
                }

                _rooms = value;
                RaisePropertyChanged(RoomsPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedRoom" /> property's name.
        /// </summary>
        public const string SelectedRoomPropertyName = "SelectedRoom";

        private Room _selectedRoom = null;

        /// <summary>
        /// Sets and gets the SelectedRoom property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Room SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }

            set
            {
                if (_selectedRoom == value)
                {
                    return;
                }

                _selectedRoom = value;
                RaisePropertyChanged(SelectedRoomPropertyName);
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

        public XElement ToXML()
        {
            return new XElement("Zone",
                new XElement("Name", ZoneName),
                new XElement("Id", ZoneId),
                new XElement("Rooms", from a in Rooms select a.ToXML())
                );

        }
        public static Zone FromXML(XElement xml)
        {
            Zone z = new Zone();
            if (xml.Element("Name") != null)
            {
                z.ZoneName = xml.Element("Name").Value;
            }
            if (xml.Element("Id") != null)
            {
                z.ZoneId = Guid.Parse(xml.Element("Id").Value);
            }
            else
            {
                z.ZoneId = new Guid();
            }
            if (xml.Element("Rooms") != null)
            {
                z.Rooms = new ObservableCollection<Room>(from a in xml.Element("Rooms").Elements("Room") select Room.FromXML(a));
            }
            return z;
        }

        #region Methods
        public void NewRoom()
        {
            var form = new NewForms.NewRoom();
            form.cmbZones.SelectedItem = this;
            form.Show();
        }
        public void DeleteSelectedRoom()
        {
            if (SelectedRoom != null)
            {
                var result = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete " + SelectedRoom.RoomName + "?\nThis will also delete all exits to this room, and all scripts associated.\nYou can not undo this action.", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    MainViewModel.MainViewModelStatic.DeleteRoom(SelectedRoom);
                }
            }
        }
        #endregion Methods
        #region Commands
        public RelayCommand NewRoomCommand { get; private set; }
        public RelayCommand DeleteRoomCommand { get; private set; }
        #endregion
    }
}
