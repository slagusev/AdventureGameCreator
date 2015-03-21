using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public enum ExitDirection { North, South, East, West, Other };
    public class Exit : INotifyPropertyChanged
    {
        public Exit()
        {
            ExitVisibility.IsItemScript = false;
            ExitVisibility.CanAddItem = false;
            ExitVisibility.CanAddText = false;
            ExitVisibility.CanComment = true;
            ExitVisibility.CanConditional = true;
            ExitVisibility.CanDisplayText = false;
            ExitVisibility.CanReturn = true;
            ExitVisibility.CanSetVariable = true;
            ExitVisibility.CanStopGame = false;
        }
        /// <summary>
        /// The <see cref="ZoneID" /> property's name.
        /// </summary>
        public const string ZoneIDPropertyName = "ZoneID";

        private Guid _zoneID = new Guid();

        /// <summary>
        /// Sets and gets the ZoneID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid ZoneID
        {
            get
            {
                return _zoneID;
            }

            set
            {
                if (_zoneID == value)
                {
                    return;
                }

                _zoneID = value;
                RaisePropertyChanged(ZoneIDPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RoomID" /> property's name.
        /// </summary>
        public const string RoomIDPropertyName = "RoomID";

        private Guid _roomID = new Guid();

        /// <summary>
        /// Sets and gets the RoomID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid RoomID
        {
            get
            {
                return _roomID;
            }

            set
            {
                if (_roomID == value)
                {
                    return;
                }

                _roomID = value;
                RaisePropertyChanged(RoomIDPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ZoneLink" /> property's name.
        /// </summary>
        public const string ZoneLinkPropertyName = "ZoneLink";

        private Zone _zoneLink = null;

        /// <summary>
        /// Sets and gets the ZoneLink property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Zone ZoneLink
        {
            get
            {
                return _zoneLink;
            }

            set
            {
                if (_zoneLink == value)
                {
                    return;
                }

                _zoneLink = value;
                RaisePropertyChanged(ZoneLinkPropertyName);
                if (ZoneLink != null && _zoneID != _zoneLink.ZoneId)
                {
                    ZoneID = _zoneLink.ZoneId;
                }
                if (ZoneLink != null && !ZoneLink.Rooms.Contains(RoomLink))
                {
                    RoomID = new Guid();
                    RoomLink = null;
                }
            }
        }
        /// <summary>
        /// The <see cref="RoomLink" /> property's name.
        /// </summary>
        public const string RoomLinkPropertyName = "RoomLink";

        private Room _roomLink = null;

        /// <summary>
        /// Sets and gets the RoomLink property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Room RoomLink
        {
            get
            {
                return _roomLink;
            }

            set
            {
                if (_roomLink == value)
                {
                    return;
                }

                _roomLink = value;
                RaisePropertyChanged(RoomLinkPropertyName);
                if (RoomLink != null && _roomID != _roomLink.RoomID)
                {
                    RoomID = _roomLink.RoomID;
                }
            }
        }

        /// <summary>
        /// The <see cref="Direction" /> property's name.
        /// </summary>
        public const string DirectionPropertyName = "Direction";

        private ExitDirection _direction = ExitDirection.Other;

        /// <summary>
        /// Sets and gets the Direction property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ExitDirection Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                if (_direction == value)
                {
                    return;
                }

                _direction = value;
                RaisePropertyChanged(DirectionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ExitName" /> property's name.
        /// </summary>
        public const string ExitNamePropertyName = "ExitName";

        private string _exitName = "";

        /// <summary>
        /// Sets and gets the ExitName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string ExitName
        {
            get
            {
                return _exitName;
            }

            set
            {
                if (_exitName == value)
                {
                    return;
                }

                _exitName = value;
                RaisePropertyChanged(ExitNamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ExitVisibility" /> property's name.
        /// </summary>
        public const string ExitVisibilityPropertyName = "ExitVisibility";

        private Script _exitVisibility = new Script();

        /// <summary>
        /// Sets and gets the ExitVisibility property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script ExitVisibility
        {
            get
            {
                return _exitVisibility;
            }

            set
            {
                if (_exitVisibility == value)
                {
                    return;
                }

                _exitVisibility = value;
                RaisePropertyChanged(ExitVisibilityPropertyName);
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
            return new XElement("Exit",
                new XElement("ZoneID", ZoneID),
                new XElement("RoomID", RoomID),
                new XElement("ExitName", ExitName),
                new XElement("ExitVisibility", ExitVisibility.ToXML()),
                new XElement("Direction", Direction.ToString()));
        }
      
        public static Exit FromXML(XElement xml)
        {
            Exit exit = new Exit();
            if (xml.Element("ZoneID") != null)
            {
                exit.ZoneID = Guid.Parse(xml.Element("ZoneID").Value);
            }
            if (xml.Element("RoomID") != null)
            {
                exit.RoomID = Guid.Parse(xml.Element("RoomID").Value);
            }
            if (xml.Element("ExitName") != null)
            {
                exit.ExitName = xml.Element("ExitName").Value;
            }
            if (xml.Element("Direction") != null)
            {
                ExitDirection d;
                Enum.TryParse<ExitDirection>(xml.Element("Direction").Value, out d);
                exit.Direction = d;
            }
            if (xml.Element("ExitVisibility") != null)
            {
                exit.ExitVisibility = Script.FromXML(xml.Element("ExitVisibility").Element("Script"), exit.ExitVisibility);
            }

            return exit;
        }
    }
}
