using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.StatusEffects
{
    public class RemoveStatusEffect : ScriptLine
    {
        /// <summary>
        /// The <see cref="AssociatedEffect" /> property's name.
        /// </summary>
        public const string AssociatedStatusEffectPropertyName = "AssociatedEffect";

        private GenericRef<StatusEffect> _statusEffects = GenericRef<StatusEffect>.GetStatusEffectRef();

        /// <summary>
        /// Sets and gets the AssociatedStatusEffect property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GenericRef<StatusEffect> AssociatedEffect
        {
            get
            {
                return _statusEffects;
            }

            set
            {
                if (_statusEffects == value)
                {
                    return;
                }

                _statusEffects = value;
                RaisePropertyChanged(AssociatedStatusEffectPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RemoveAllStacks" /> property's name.
        /// </summary>
        public const string RemoveAllStackPropertyName = "RemoveAllStacks";

        private bool _removeAllStacks = false;

        /// <summary>
        /// Sets and gets the RemoveAllStack property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool RemoveAllStacks
        {
            get
            {
                return _removeAllStacks;
            }

            set
            {
                if (_removeAllStacks == value)
                {
                    return;
                }

                _removeAllStacks = value;
                RaisePropertyChanged(RemoveAllStackPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="RunFinalizeEvent" /> property's name.
        /// </summary>
        public const string RunFinalizeEventPropertyName = "RunFinalizeEvent";

        private bool _runFinalizeEvent = true;

        /// <summary>
        /// Sets and gets the RunFinalizeEvent property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool RunFinalizeEvent
        {
            get
            {
                return _runFinalizeEvent;
            }

            set
            {
                if (_runFinalizeEvent == value)
                {
                    return;
                }

                _runFinalizeEvent = value;
                RaisePropertyChanged(RunFinalizeEventPropertyName);
            }
        }
        public override string Plaintext
        {
            get
            {
                return "Remove " + (RemoveAllStacks ? "all instances of" : "the first instance of") + " the " + (AssociatedEffect != null && AssociatedEffect.Value != null ? AssociatedEffect.Value.Name : "INVALID REFERENCE") + " status effect from the player.";
            }
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("RemoveStatusEffect",
               new XElement("AssociatedEffect", this.AssociatedEffect.Ref),
               new XElement("RemoveAllStacks", this.RemoveAllStacks),
               new XElement("RunFinalizeEvent", this.RunFinalizeEvent));

        }

        public static RemoveStatusEffect FromXML(XElement xml)
        {
            RemoveStatusEffect rse = new RemoveStatusEffect();
            rse.AssociatedEffect.Ref = Guid.Parse(xml.Element("AssociatedEffect").Value);
            rse.RemoveAllStacks = Boolean.Parse(xml.Element("RemoveAllStacks").Value);
            if (xml.Element("RunFinalizeEvent") != null)
            {
                rse.RunFinalizeEvent = Boolean.Parse(xml.Element("RunFinalizeEvent").Value);
            }
            return rse;
        }
    }
}
