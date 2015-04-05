using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class ConversationChoice : INotifyPropertyChanged
    {
        public ConversationChoice()
        {
            this.ChoiceVisibility.CanAddItem = false;
            this.ChoiceVisibility.CanAddText = false;
            this.ChoiceVisibility.CanComment = true;
            this.ChoiceVisibility.CanConditional = true;
            this.ChoiceVisibility.CanDisplayText = false;
            this.ChoiceVisibility.CanReturn = true;
            this.ChoiceVisibility.CanReturnValue = false;
            this.ChoiceVisibility.CanSetVariable = true;
            this.ChoiceVisibility.CanStopGame = false;
            this.ChoiceVisibility.HasTextFunctionality = true;
            this.ChoiceVisibility.IsItemScript = false;
            this.ChoiceVisibility.IsInConversation = true;
            this.ChoiceVisibility.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeTrueFalse);
        }
        /// <summary>
        /// The <see cref="ChoiceVisibility" /> property's name.
        /// </summary>
        public const string ChoiceVisibilityPropertyName = "ChoiceVisibility";

        private Scripter.Script _choiceVisibility = new Scripter.Script();

        /// <summary>
        /// Sets and gets the ChoiceVisibility property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script ChoiceVisibility
        {
            get
            {
                return _choiceVisibility;
            }

            set
            {
                if (_choiceVisibility == value)
                {
                    return;
                }

                _choiceVisibility = value;
                RaisePropertyChanged(ChoiceVisibilityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ChoiceText" /> property's name.
        /// </summary>
        public const string ChoiceTextPropertyName = "ChoiceText";

        private string _choiceText = "";

        /// <summary>
        /// Sets and gets the ChoiceText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string ChoiceText
        {
            get
            {
                return _choiceText;
            }

            set
            {
                if (_choiceText == value)
                {
                    return;
                }

                _choiceText = value;
                RaisePropertyChanged(ChoiceTextPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Target" /> property's name.
        /// </summary>
        public const string TargetPropertyName = "Target";

        private int _target = 0;

        /// <summary>
        /// Sets and gets the Target property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Target
        {
            get
            {
                return _target;
            }

            set
            {
                if (_target == value)
                {
                    return;
                }

                _target = value;
                RaisePropertyChanged(TargetPropertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public XElement ToXML()
        {
           return new XElement("Choice", 
               new XElement("Target", Target),
               new XElement("Text", ChoiceText),
               new XElement("Visibility", ChoiceVisibility.ToXML()));
        }
        public static ConversationChoice FromXML(XElement xml)
        {
            ConversationChoice choice = new ConversationChoice();
            if (xml.Element("Target") != null)
            {
                choice.Target = Convert.ToInt32(xml.Element("Target").Value);
            }
            if (xml.Element("Text") != null)
            {
                choice.ChoiceText = xml.Element("Text").Value;
            }
            if (xml.Element("Visibility") != null)
            {
                choice.ChoiceVisibility = Scripter.Script.FromXML(xml.Element("Visibility").Element("Script"), choice.ChoiceVisibility);
            }
            return choice;

            
        }
        

    }
}
