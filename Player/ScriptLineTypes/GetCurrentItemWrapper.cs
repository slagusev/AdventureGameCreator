using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetCurrentItemWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        GetCurrentItem line;
        public GetCurrentItemWrapper(GetCurrentItem gci)
        {
            line = gci;
            
        }
        public override bool? Execute()
        {
            var v = parent.GetVarById(line.VarRef.LinkedVarId);
            if (v != null)
            {
                v.CurrentItemValue = this.parent.ItemBase;
            }
            return null;
        }
    }
}
