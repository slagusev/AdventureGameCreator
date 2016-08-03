using Editor.Scripter.StatusEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class CheckIfEffectsResolvedWrapper : ScriptLineWrapper
    {
        public override bool? Execute()
        {
            MainViewModel.GetMainViewModelStatic().CheckStatusEffectsResolved();
            return null;
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return new CheckIfEffectsResolved().ToXML();
        }
    }
}
