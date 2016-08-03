using Editor.Scripter.StatusEffects;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class RemoveStatusEffectWrapper : ScriptLineWrapper
    {
        RemoveStatusEffect RemoveStatusEffectLine = new RemoveStatusEffect();

        public RemoveStatusEffectWrapper(RemoveStatusEffect line)
        {
            RemoveStatusEffectLine = line;
        }
        public override bool? Execute()
        {
            var matchingEffects = new List<StatusEffectWrapper>(from a in MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveStatusEffects
                                                                where a.LinkedEffect.Ref == RemoveStatusEffectLine.AssociatedEffect.Ref
                                                                select a);
            if (matchingEffects.Count() > 0)
            {
                if (RemoveStatusEffectLine.RemoveAllStacks)
                {
                    foreach(var a in matchingEffects)
                    {
                        MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveStatusEffects.Remove(a);
                        if (RemoveStatusEffectLine.RunFinalizeEvent) a.RunFinalize();
                    }
                }
                else
                {
                    MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveStatusEffects.Remove(matchingEffects.First());
                    if (RemoveStatusEffectLine.RunFinalizeEvent) matchingEffects.First().RunFinalize();
                }
            }

            return null;
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return RemoveStatusEffectLine.ToXML();
        }
    }
}
