using Editor.Scripter.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetAllItemsWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        GetAllItems line;
        public GetAllItemsWrapper(GetAllItems gal)
        {
            line = gal;
        }
        public override bool? Execute()
        {
            if (line.ArrayRef != null && line.ArrayRef.Value != null)
            {
                var arrayRef = line.ArrayRef.Value;
                var game = MainViewModel.GetMainViewModelStatic().CurrentGame;
                var array = game.ArraysById[line.ArrayRef.Ref];
                array.Clear();
                foreach (var a in game.PlayerInventory)
                {
                    array.Add(a);

                }
                return null;
            }
            else
            {
                MainViewModel.WriteText("Error in Get All Items.", this.parent);
                return false;
            }
        }
    }
}
