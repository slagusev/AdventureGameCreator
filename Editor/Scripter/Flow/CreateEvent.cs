using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Flow
{
    public class CreateEvent : ScriptLine
    {

        public CreateEvent()
        {
            this.Condition.CanDisplayText = false;
            this.Condition.CanStopGame = false;
            this.Condition.HasTextFunctionality = false;
            this.Condition.CanAddItem = false;
            this.Condition.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeTrueFalse);
            this.Result.CanReturn = false;
            this.Result.CanStartConversations = true;
            this.Result.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeMovementAndInteractable);
        }
        /// <summary>
        /// The <see cref="Condition" /> property's name.
        /// </summary>
        public const string ConditionPropertyName = "Condition";

        private Script _condition = new Script();

        /// <summary>
        /// Sets and gets the Condition property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script Condition
        {
            get
            {
                return _condition;
            }

            set
            {
                if (_condition == value)
                {
                    return;
                }

                _condition = value;
                RaisePropertyChanged(ConditionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Result" /> property's name.
        /// </summary>
        public const string ResultPropertyName = "Result";

        private Script _result = new Script();

        /// <summary>
        /// Sets and gets the Result property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script Result
        {
            get
            {
                return _result;
            }

            set
            {
                if (_result == value)
                {
                    return;
                }

                _result = value;
                RaisePropertyChanged(ResultPropertyName);
            }
        }



        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("CreateEvent", new XElement("Condition", Condition.ToXML()), new XElement("Result", Result.ToXML()));
        }

        public static CreateEvent FromXML(XElement xml)
        {
            CreateEvent ce = new CreateEvent();
            ce.Condition = Script.FromXML(xml.Element("Condition").Element("Script"), ce.Condition);
            ce.Result = Script.FromXML(xml.Element("Result").Element("Script"), ce.Result);
            return ce;
        }
        public override string Plaintext
        {
            get
            {
                return "Create a new triggered event.";
            }
        }
    }
}
