using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Misc
{
    public class ChangeRoom : ScriptLine
    {
        /// <summary>
        /// The <see cref="SelectedRoom" /> property's name.
        /// </summary>
        public const string SelectedRoomPropertyName = "SelectedRoom";

        private GenericRef<Room> _selectedRoom = GenericRef<Room>.GetRoomRef();

        /// <summary>
        /// Sets and gets the SelectedRoom property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GenericRef<Room> SelectedRoom
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

        
        public override string Plaintext
        {
            get
            {
                return "Change the player's room to " + (SelectedRoom.Value ?? new Room { RoomName = "NOT FOUND" }) .RoomName;
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            //return new System.Xml.Linq.XElement
            return new System.Xml.Linq.XElement("ChangeRoom", SelectedRoom.Ref);
        }
        public static ChangeRoom FromXML(XElement xml)
        {
            ChangeRoom cr = new ChangeRoom();
            cr.SelectedRoom.Ref = Guid.Parse(xml.Value);
            return cr;
        }
    }
}
