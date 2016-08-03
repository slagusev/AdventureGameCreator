using Editor.Scripter.StatusEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class AddStatusEffectWrapper : ScriptLineWrapper
    {

        AddStatusEffect AddStatusEffectLine = new AddStatusEffect();

        public AddStatusEffectWrapper(AddStatusEffect line)
        {
            AddStatusEffectLine = line;
        }
        public override bool? Execute()
        {
            var se = new ObjectTypesWrappers.StatusEffectWrapper(AddStatusEffectLine.AssociatedEffect, MainViewModel.GetMainViewModelStatic().CurrentGame);
            foreach (var a in AddStatusEffectLine.Arguments)
            {
                if (a.IsNumber)
                {
                    se.numberArguments.Add(a);
                }
                else
                {
                    se.stringArguments.Add(a);
                }
            }
            if (!se.LinkedEffect.Value.CanOccurMultipleTimes)
            {
                var dupes = MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveStatusEffects.Where(a => a.LinkedEffect.Ref == se.LinkedEffect.Ref);
                if (dupes.Count() > 0)
                {
                    dupes.First().RunStack();
                }
                else
                {
                    MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveStatusEffects.Add(se);
                    se.RunInit();
                }
            }
            else
            { 
                MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveStatusEffects.Add(se);
                se.RunInit();
            }

            return null;
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return AddStatusEffectLine.ToXML();
        }
    }
}
