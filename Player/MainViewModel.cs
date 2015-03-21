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

        public static void WriteText(string line, bool includeBar = false)
        {
            if (includeBar)
            {
                WriteText("-------------------------------------------");
            }
            if (!string.IsNullOrWhiteSpace(line))
            {
                var mvm = (MainViewModel)Player.App.Current.Resources["MainViewModelStatic"];
                line = FormatText(line);


                mvm.FeedbackText += line + "\n\n";
            }
        }

        public static string FormatText(string line)
        {
            return Regex.Replace(line, "\\{\\{(?<VarName>.*?)\\}\\}", a =>
            {
                var mvm = MainViewModel.GetMainViewModelStatic();
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
            ViewInventoryCommand = new Editor.RelayCommand(ViewInventory);
            ExploreModeCommand = new Editor.RelayCommand(SetExploreMode);
            UseItemCommand = new Editor.RelayCommand(UseItem);
            DropItemCommand = new Editor.RelayCommand(DropItem);
        }
        public Editor.RelayCommand<ExitWrapper> UseExitCommand { get; private set; }
        public Editor.RelayCommand<InteractableWrapper> ExamineCommand { get; private set; }
        public Editor.RelayCommand<InteractableWrapper> InteractCommand { get; private set; }
        public Editor.RelayCommand ViewInventoryCommand { get; private set; }
        public Editor.RelayCommand ExploreModeCommand { get; private set; }
        public Editor.RelayCommand UseItemCommand { get; private set; }
        public Editor.RelayCommand DropItemCommand { get; private set; }
        public void UseExit(ExitWrapper exit)
        {
            CurrentGame.CurrentRoom = CurrentGame.Rooms[exit.ExitBase.RoomID];
            OutputCurrentRoomDescription();
        }
        public void ExamineObject(InteractableWrapper interactable)
        {
            interactable.Examine();
            if (CurrentGame.CurrentRoom != null)
                CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
            CurrentGame.RefreshVisibleExits();
        }
        public void InteractObject(InteractableWrapper interactable)
        {
            interactable.Interact();
            if (CurrentGame.CurrentRoom != null)
                CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
            CurrentGame.RefreshVisibleExits();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void ViewInventory()
        {
            InventoryMode = true;
            ExploreMode = false;
            SelectedItem = null;
        }
        public void SetExploreMode()
        {
            ExploreMode = true;
            InventoryMode = false;
            if (CurrentGame.CurrentRoom != null)
                CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
            CurrentGame.RefreshVisibleExits();
        }
        public void UseItem()
        {
            if (SelectedItem != null)
            {
                WriteText("", true);
                SelectedItem.UseItem();
                SetExploreMode();
            }
        }
        public void DropItem()
        {
            if (SelectedItem != null)
            {
                CurrentGame.PlayerInventory.Remove(SelectedItem);
                SelectedItem = null;
            }
        }

        /// <summary>
        /// The <see cref="ExploreMode" /> property's name.
        /// </summary>
        public const string ExploreModePropertyName = "ExploreMode";

        private bool _exploreMode = true;

        /// <summary>
        /// Sets and gets the ExploreMode property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ExploreMode
        {
            get
            {
                return _exploreMode;
            }

            set
            {
                if (_exploreMode == value)
                {
                    return;
                }

                _exploreMode = value;
                RaisePropertyChanged(ExploreModePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedItem" /> property's name.
        /// </summary>
        public const string SelectedItemPropertyName = "SelectedItem";

        private ItemInstance _selectedItem = null;

        /// <summary>
        /// Sets and gets the SelectedItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemInstance SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem == value)
                {
                    return;
                }

                _selectedItem = value;
                if (SelectedItem != null)
                {
                    CurrentItemDescription = SelectedItem.GetDescription();
                }
                else
                {
                    CurrentItemDescription = "";
                }
                RaisePropertyChanged(SelectedItemPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CurrentItemDescription" /> property's name.
        /// </summary>
        public const string CurrentItemDescriptionPropertyName = "CurrentItemDescription";

        private string _currentItemDescription = "";

        /// <summary>
        /// Sets and gets the CurrentItemDescription property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string CurrentItemDescription
        {
            get
            {
                return _currentItemDescription;
            }

            set
            {
                if (_currentItemDescription == value)
                {
                    return;
                }

                _currentItemDescription = value;
                RaisePropertyChanged(CurrentItemDescriptionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="InventoryMode" /> property's name.
        /// </summary>
        public const string InventoryModePropertyName = "InventoryMode";

        private bool _inventoryMode = false;

        /// <summary>
        /// Sets and gets the InventoryMode property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool InventoryMode
        {
            get
            {
                return _inventoryMode;
            }

            set
            {
                if (_inventoryMode == value)
                {
                    return;
                }

                _inventoryMode = value;
                RaisePropertyChanged(InventoryModePropertyName);
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
