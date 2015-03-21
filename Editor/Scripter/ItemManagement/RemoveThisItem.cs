using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class RemoveThisItem : ScriptLine
    {


        public override System.Xml.Linq.XElement ToXML()
        {

            return new System.Xml.Linq.XElement("RemoveThisItem");
        }

        public override string Plaintext
        {
            get
            {
                return "Remove this item from the player's inventory.";
            }
        }

        public static RemoveThisItem FromXML(XElement xml)
        {
            return new RemoveThisItem();
        }
    }
}
