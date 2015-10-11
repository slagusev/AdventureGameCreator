using Editor.Scripter.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ChangeRoomWrapper : ScriptLineWrapper
    {
        ChangeRoom line;
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        public ChangeRoomWrapper(ChangeRoom cr)
        {
            line = cr;
        }

        public override bool? Execute()
        {
            if (MainViewModel.GetMainViewModelStatic().CurrentGame.Rooms.ContainsKey(line.SelectedRoom.Ref))
            {
                MainViewModel.GetMainViewModelStatic().CurrentGame.CurrentRoom = MainViewModel.GetMainViewModelStatic().CurrentGame.Rooms[line.SelectedRoom.Ref];
                MainViewModel.WriteText("", this.parent, true);
                MainViewModel.GetMainViewModelStatic().OutputCurrentRoomDescription();
                return null;
            }
            else
            {
                MainViewModel.WriteText("Room not defined on ChangeRoom!", this.parent);
                return false;
            }
        }
    }
}
