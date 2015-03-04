using Editor.ObjectTypes;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Player
{
    class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            WireCommands();
        }
        /// <summary>
        /// The <see cref="CurrentGame" /> property's name.
        /// </summary>
        public const string CurrentGamePropertyName = "CurrentGame";

        private Game _currentGame = null;

        /// <summary>
        /// Sets and gets the CurrentGame property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Game CurrentGame
        {
            get
            {
                return _currentGame;
            }

            set
            {
                if (_currentGame == value)
                {
                    return;
                }

                _currentGame = value;
                RaisePropertyChanged(CurrentGamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="FeedbackText" /> property's name.
        /// </summary>
        public const string FeedbackTextPropertyName = "FeedbackText";

        private string _feedbackText = "";

        /// <summary>
        /// Sets and gets the FeedbackText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string FeedbackText
        {
            get
            {
                return _feedbackText;
            }

            set
            {
                if (_feedbackText == value)
                {
                    return;
                }

                _feedbackText = value;
                RaisePropertyChanged(FeedbackTextPropertyName);
            }
        }

        public void OutputCurrentRoomDescription()
        {
            WriteText("-------------------------------------------");
            var currentRoom = CurrentGame.CurrentRoom;
            currentRoom.RoomDescription();
            //if (currentRoom.RoomBase.HasScriptDescription)
            //{
            //    //Run the script
            //}
            //else
            //{
            //    WriteText(currentRoom.RoomBase.Description);
            //}

        }

        public static void WriteText(string line)
        {
            var mvm = (MainViewModel)Player.App.Current.Resources["MainViewModelStatic"];
            line = Regex.Replace(line, "\\{\\{(?<VarName>.*?)\\}\\}", a =>
            {
                var varName = a.Groups["VarName"].Value;
                string res = "INVALID VARIABLE NAME";
                if (mvm.CurrentGame.VarByName.ContainsKey(varName))
                {
                    var v = mvm.CurrentGame.VarByName[varName];
                    if (v.VariableBase.IsDateTime) return v.CurrentDateTimeValue.ToString();
                    if (v.VariableBase.IsNumber) return v.CurrentNumberValue.ToString();
                    if (v.VariableBase.IsString) return v.CurrentStringValue.ToString();
                }
                return res;
            });

            
            mvm.FeedbackText += line + "\n\n";
        }
        public static MainViewModel GetMainViewModelStatic()
        {
            return (MainViewModel)Player.App.Current.Resources["MainViewModelStatic"];
        }
        public void WireCommands()
        {
            UseExitCommand = new Editor.RelayCommand<ExitWrapper>(UseExit);
            ExamineCommand = new Editor.RelayCommand<InteractableWrapper>(ExamineObject);
            InteractCommand = new Editor.RelayCommand<InteractableWrapper>(InteractObject);
        }
        public Editor.RelayCommand<ExitWrapper> UseExitCommand { get; private set; }
        public Editor.RelayCommand<InteractableWrapper> ExamineCommand { get; private set; }
        public Editor.RelayCommand<InteractableWrapper> InteractCommand { get; private set; }
        public void UseExit(ExitWrapper exit)
        {
            CurrentGame.CurrentRoom = CurrentGame.Rooms[exit.ExitBase.RoomID];
            OutputCurrentRoomDescription();
        }
        public void ExamineObject(InteractableWrapper interactable)
        {
            interactable.Examine();
        }
        public void InteractObject(InteractableWrapper interactable)
        {
            interactable.Interact();
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
        /// The <see cref="IsGameOver" /> property's name.
        /// </summary>
        public const string IsGameOverPropertyName = "IsGameOver";

        private bool _isGameOver = false;

        /// <summary>
        /// Sets and gets the IsGameOver property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return _isGameOver;
            }

            set
            {
                if (_isGameOver == value)
                {
                    return;
                }

                _isGameOver = value;
                RaisePropertyChanged(IsGameOverPropertyName);
            }
        }
    }
}
