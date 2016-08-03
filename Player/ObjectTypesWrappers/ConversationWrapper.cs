using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypesWrappers
{
    class ConversationWrapper : INotifyPropertyChanged
    {
        Conversation Convo;
        public ConversationWrapper(Conversation c)
        {
            Convo = c;
            CurrentStage = c.StartingStage;
            GoToStageCommand = new Editor.RelayCommand<int>(GoToStage);
        }

        public void GoToStage(int stage)
        {
            
            var stageInstance = Convo.Stages.Where(a => a.StageId == stage).FirstOrDefault();
            if (stageInstance != null)
            {
                var script = new ScriptWrapper(stageInstance.StageAction);
                var result = script.Execute();
                if (result != true)
                {
                    CalculateChoices(stageInstance);
                    if (ChoicesColumn1.Count() == 0) ConversationFinished = true;
                }

            }
            else ConversationFinished = true;
            MainViewModel.GetMainViewModelStatic().CurrentGame.RefreshAll();
            if (ConversationFinished && MainViewModel.GetMainViewModelStatic().CurrentConversation == this)
            {
                MainViewModel.GetMainViewModelStatic().SetExploreMode();
            }
        }
        public Editor.RelayCommand<int> GoToStageCommand { get; set; }
        public void CalculateChoices(ConversationStage s)
        {
            ChoicesColumn1.Clear();
            ChoicesColumn2.Clear();
            ChoicesColumn3.Clear();
            int current = 0;
            foreach (var choice in s.Choices)
            {
                var script = new ScriptWrapper(choice.ChoiceVisibility);
                var result = script.Execute();
                if (result == null || result == true)
                {
                    if (current == 0)
                    {
                        ChoicesColumn1.Add(new KeyValuePair<int, string>(choice.Target, choice.ChoiceText));
                    }
                    if (current == 1)
                    {
                        ChoicesColumn2.Add(new KeyValuePair<int, string>(choice.Target, choice.ChoiceText));
                    }
                    if (current == 2)
                    {
                        ChoicesColumn3.Add(new KeyValuePair<int, string>(choice.Target, choice.ChoiceText));
                    }
                    current = (current + 1) % 3;
                }
            }
        }
        /// <summary>
        /// The <see cref="ChoicesColumn1" /> property's name.
        /// </summary>
        public const string ChoicesColumn1PropertyName = "ChoicesColumn1";

        private ObservableCollection<KeyValuePair<int, string>> _choices1 = new ObservableCollection<KeyValuePair<int,string>>();

        /// <summary>
        /// Sets and gets the ChoicesColumn1 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<KeyValuePair<int, string>> ChoicesColumn1
        {
            get
            {
                return _choices1;
            }

            set
            {
                if (_choices1 == value)
                {
                    return;
                }

                _choices1 = value;
                RaisePropertyChanged(ChoicesColumn1PropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ChoicesColumn2" /> property's name.
        /// </summary>
        public const string ChoicesColumn2PropertyName = "ChoicesColumn2";

        private ObservableCollection<KeyValuePair<int, string>> _choicesColumn2 = new ObservableCollection<KeyValuePair<int,string>>();

        /// <summary>
        /// Sets and gets the ChoicesColumn2 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<KeyValuePair<int, string>> ChoicesColumn2
        {
            get
            {
                return _choicesColumn2;
            }

            set
            {
                if (_choicesColumn2 == value)
                {
                    return;
                }

                _choicesColumn2 = value;
                RaisePropertyChanged(ChoicesColumn2PropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ChoicesColumn3" /> property's name.
        /// </summary>
        public const string ChoicesColumn3PropertyName = "ChoicesColumn3";

        private ObservableCollection<KeyValuePair<int, string>> _choicesColumn3 = new ObservableCollection<KeyValuePair<int,string>>();

        /// <summary>
        /// Sets and gets the ChoicesColumn3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<KeyValuePair<int, string>> ChoicesColumn3
        {
            get
            {
                return _choicesColumn3;
            }

            set
            {
                if (_choicesColumn3 == value)
                {
                    return;
                }

                _choicesColumn3 = value;
                RaisePropertyChanged(ChoicesColumn3PropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ConversationFinished" /> property's name.
        /// </summary>
        public const string ConversationFinishedPropertyName = "ConversationFinished";

        private bool _conversationFinished = false;

        /// <summary>
        /// Sets and gets the ConversationFinished property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ConversationFinished
        {
            get
            {
                return _conversationFinished;
            }

            set
            {
                if (_conversationFinished == value)
                {
                    return;
                }

                _conversationFinished = value;
                RaisePropertyChanged(ConversationFinishedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentStage" /> property's name.
        /// </summary>
        public const string CurrentStagePropertyName = "CurrentStage";

        private int _currentStage = 0;

        /// <summary>
        /// Sets and gets the CurrentStage property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int CurrentStage
        {
            get
            {
                return _currentStage;
            }

            set
            {
                if (_currentStage == value)
                {
                    return;
                }

                _currentStage = value;
                RaisePropertyChanged(CurrentStagePropertyName);
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
    }
}
