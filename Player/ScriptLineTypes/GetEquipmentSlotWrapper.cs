﻿using Editor.Scripter.ItemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GetEquipmentSlotWrapper : ScriptLineWrapper
    {
        GetEquipmentSlot line;
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
        public GetEquipmentSlotWrapper(GetEquipmentSlot ges)
        {
            line = ges;
        }
        public override bool? Execute()
        {
            var v = parent.GetVarById(line.VarRef.LinkedVarId);
            if (v != null)
            {
                v.CurrentItemValue = MainViewModel.GetMainViewModelStatic().CurrentGame.EquippedItems[line.Slot];
                return null;
            }
            else return false;
            
        }
    }
}
