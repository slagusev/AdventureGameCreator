using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ConversationFlow
{
    public class GoToStage : ScriptLine
    {
        public override string Plaintext
        {
            get
            {
                return "Stop processing this stage, and immediately go to stage " + Stage + " of the current conversation.";
            }
        }
        /// <summary>
        /// The <see cref="Stage" /> property's name.
        /// </summary>
        public const string StagePropertyName = "Stage";

        private int _stage = 10;

        /// <summary>
        /// Sets and gets the Stage property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Stage
        {
            get
            {
                return _stage;
            }

            set
            {
                if (_stage == value)
                {
                    return;
                }

                _stage = value;
                RaisePropertyChanged(StagePropertyName);
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("GoToStage", Stage);
        }
        public static GoToStage FromXML(XElement xml)
        {
            return new GoToStage { Stage = Convert.ToInt32(xml.Value) };
        }
    }
}
