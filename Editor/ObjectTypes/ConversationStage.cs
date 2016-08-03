using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class ConversationStage : INotifyPropertyChanged
    {
        public ConversationStage()
        {
            this.StageAction.CanAddItem = true;
            this.StageAction.CanAddText = false;
            this.StageAction.CanComment = true;
            this.StageAction.CanConditional = true;
            this.StageAction.CanDisplayText = true;
            this.StageAction.CanReturn = false;
            this.StageAction.CanReturnValue = false;
            this.StageAction.CanSetVariable = true;
            this.StageAction.CanStopGame = true;
            this.StageAction.HasTextFunctionality = true;
            this.StageAction.IsItemScript = false;
            this.StageAction.IsInConversation = true;
            this.StageAction.CanStartConversations = true;
            this.StageAction.AllowedCommonEventTypes.Add(CommonEvent.ScriptTypeMovementAndInteractable);
            WireCommands();            
        }

        private void WireCommands()
        {
            this.AddChoiceCommand = new RelayCommand(AddChoice);
            this.RemoveChoiceCommand = new RelayCommand(RemoveChoice);
        }
        /// <summary>
        /// The <see cref="StageId" /> property's name.
        /// </summary>
        public const string StageIdPropertyName = "StageId";

        private int _stageId = 0;

        /// <summary>
        /// Sets and gets the StageId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int StageId
        {
            get
            {
                return _stageId;
            }

            set
            {
                if (_stageId == value)
                {
                    return;
                }

                _stageId = value;
                RaisePropertyChanged(StageIdPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="StageAction" /> property's name.
        /// </summary>
        public const string StageActionPropertyName = "StageAction";

        private Scripter.Script _stageAction = new Scripter.Script();

        /// <summary>
        /// Sets and gets the StageAction property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script StageAction
        {
            get
            {
                return _stageAction;
            }

            set
            {
                if (_stageAction == value)
                {
                    return;
                }

                _stageAction = value;
                RaisePropertyChanged(StageActionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Choices" /> property's name.
        /// </summary>
        public const string ChoicesPropertyName = "Choices";

        private ObservableCollection<ConversationChoice> _choices = new ObservableCollection<ConversationChoice>();

        /// <summary>
        /// Sets and gets the Choices property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ConversationChoice> Choices
        {
            get
            {
                return _choices;
            }

            set
            {
                if (_choices == value)
                {
                    return;
                }

                _choices = value;
                RaisePropertyChanged(ChoicesPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedChoice" /> property's name.
        /// </summary>
        public const string SelectedChoicePropertyName = "SelectedChoice";

        private ConversationChoice _selectedChoice = null;

        /// <summary>
        /// Sets and gets the SelectedChoice property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ConversationChoice SelectedChoice
        {
            get
            {
                return _selectedChoice;
            }

            set
            {
                if (_selectedChoice == value)
                {
                    return;
                }

                _selectedChoice = value;
                RaisePropertyChanged(SelectedChoicePropertyName);
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
        /// <summary>
        /// The <see cref="StageName" /> property's name.
        /// </summary>
        public const string StageNamePropertyName = "StageName";

        private string _stageName = "";

        /// <summary>
        /// Sets and gets the StageName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string StageName
        {
            get
            {
                return _stageName;
            }

            set
            {
                if (_stageName == value)
                {
                    return;
                }

                _stageName = value;
                RaisePropertyChanged(StageNamePropertyName);
            }
        }
        public RelayCommand AddChoiceCommand { get; set; }
        public RelayCommand RemoveChoiceCommand { get; set; }
        public void AddChoice()
        {
            var choice = new ConversationChoice() { ChoiceText = "Choice Text", Target = 10 };
            this.Choices.Add(choice);
            this.SelectedChoice = choice;
        }
        public void RemoveChoice()
        {
            if (this.SelectedChoice != null)
            {
                this.Choices.Remove(this.SelectedChoice);
                this.SelectedChoice = null;
            }
        }

        public XElement ToXML()
        {
            return new XElement("Stage",
                new XElement("Id", this.StageId),
                new XElement("Name", this.StageName),
                new XElement("Action", this.StageAction.ToXML()),
                new XElement("Choices", from a in this.Choices select a.ToXML()));
        }
        public static ConversationStage FromXML(XElement xml)
        {
            ConversationStage stage = new ConversationStage();
            if (xml.Element("Id") != null)
            {
                stage.StageId = Convert.ToInt32(xml.Element("Id").Value);
            }
            if (xml.Element("Name") != null)
            {
                stage.StageName = xml.Element("Name").Value;
            }
            if (xml.Element("Action") != null)
            {
                stage.StageAction = Scripter.Script.FromXML(xml.Element("Action").Element("Script"), stage.StageAction);
            }
            if (xml.Element("Choices") != null)
            {
                stage.Choices = new ObservableCollection<ConversationChoice>(from a in xml.Element("Choices").Elements() select ConversationChoice.FromXML(a));
            }

            return stage;
        }
    }
}
