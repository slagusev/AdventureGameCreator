using Editor.ObjectTypes;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player
{
    class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            WireCommands();
            this.FeedbackText.CollectionChanged += (s, e) => { RaisePropertyChanged(FeedbackTextPropertyName);  };
            this.CurrentItemDescription.CollectionChanged += (s, e) => { RaisePropertyChanged(CurrentItemDescriptionPropertyName); };
            //this.ViewEquipment();
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
        /// The <see cref="Location" /> property's name.
        /// </summary>
        public const string LocationPropertyName = "Location";

        private string _location = "";

        /// <summary>
        /// Sets and gets the Location property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                if (_location == value)
                {
                    return;
                }

                _location = value;
                RaisePropertyChanged(LocationPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FeedbackText" /> property's name.
        /// </summary>
        public const string FeedbackTextPropertyName = "FeedbackText";

        private ObservableCollection<object> _feedbackText = new ObservableCollection<object>();

        /// <summary>
        /// Sets and gets the FeedbackText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<object> FeedbackText
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

        public static void WriteText(string line, ScriptWrapper script, bool includeBar = false)
        {
            if (includeBar)
            {
                WriteText("-------------------------------------------", null);
            }
            if (!string.IsNullOrWhiteSpace(line))
            {
                var mvm = (MainViewModel)Player.App.Current.Resources["MainViewModelStatic"];
                line = FormatText(line, script);


                mvm.FeedbackText.Add(line + "\n\n");
                if (mvm.FeedbackText.Count() > 40)
                {
                    while (mvm.FeedbackText.Count() > 40)
                    {
                        mvm.FeedbackText.Remove(mvm.FeedbackText.First());
                    }
                }
            }
        }
        public static void WriteImage(ImageRef line, ScriptWrapper script, bool includeBar = false)
        {
            if (includeBar)
            {
                WriteText("-------------------------------------------", null);
            }
            
            var mvm = (MainViewModel)Player.App.Current.Resources["MainViewModelStatic"];
            mvm.FeedbackText.Add(line);
            
        }

        
        public static string FormatText(string line, ScriptWrapper script)
        {
            return Regex.Replace(line, "\\{\\{(?<VarName>.*?)\\}\\}", a =>
            {
                var mvm = MainViewModel.GetMainViewModelStatic();
                var varName = a.Groups["VarName"].Value;
                string res = "INVALID VARIABLE NAME";
                VariableWrapper v = null;
                if (script != null)
                {
                    v = script.GetVarByName(varName);
                }
                else if (mvm.CurrentGame.VarByName.ContainsKey(varName))
                {
                    v = mvm.CurrentGame.VarByName[varName];
                    
                    
                }

                if (v != null)
                {
                    if (v.VariableBase.IsDateTime) return v.CurrentDateTimeValue.ToString();
                    if (v.VariableBase.IsNumber) return v.CurrentNumberValue.ToString();
                    if (v.VariableBase.IsString) return v.CurrentStringValue.ToString();
                    if (v.VariableBase.IsItem) return (v.CurrentItemValue != null ? v.CurrentItemValue.CurrentName : "NULL ITEM");
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
            ViewEquipmentCommand = new Editor.RelayCommand(ViewEquipment);
            ExploreModeCommand = new Editor.RelayCommand(SetExploreMode);
            UseItemCommand = new Editor.RelayCommand(UseItem);
            DropItemCommand = new Editor.RelayCommand(DropItem);
            EquipItemCommand = new Editor.RelayCommand(EquipItem);
            UnequipItemCommand = new Editor.RelayCommand(UnequipItem);
            SaveCommand = new Editor.RelayCommand(SaveGame);
        }
        public Editor.RelayCommand<ExitWrapper> UseExitCommand { get; private set; }
        public Editor.RelayCommand<InteractableWrapper> ExamineCommand { get; private set; }
        public Editor.RelayCommand<InteractableWrapper> InteractCommand { get; private set; }
        public Editor.RelayCommand ViewInventoryCommand { get; private set; }
        public Editor.RelayCommand ViewEquipmentCommand { get; private set; }
        public Editor.RelayCommand ExploreModeCommand { get; private set; }
        public Editor.RelayCommand UseItemCommand { get; private set; }
        public Editor.RelayCommand DropItemCommand { get; private set; }
        public Editor.RelayCommand EquipItemCommand { get; private set; }
        public Editor.RelayCommand UnequipItemCommand { get; private set; }
        public Editor.RelayCommand SaveCommand { get; private set; }
        public void UseExit(ExitWrapper exit)
        {
            WriteText("-------------------------------------------", null);
            var movementResult = new ScriptWrapper(CurrentGame.Settings.MovementScript).Execute();
            bool result = true;
            foreach (var a in CurrentGame.EquippedItems.Select(a => a.Value).Where(a => a != null).Distinct())
            {
                var res = new ScriptWrapper(a.item.EquipmentRef.OnMove).Execute();
                if (res == false)
                    result = false;
            }
            if ((movementResult == null || movementResult == true) && result)
            {
                CurrentGame.CurrentRoom = CurrentGame.Rooms[exit.ExitBase.RoomID];

            }
            
            OutputCurrentRoomDescription();
            MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
            GC.Collect();
        }
        public void ExamineObject(InteractableWrapper interactable)
        {
            interactable.Examine();
            if (CurrentGame.CurrentRoom != null)
                CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
            CurrentGame.RefreshAll();
            MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
        }
        public void InteractObject(InteractableWrapper interactable)
        {
            interactable.Interact();
            if (CurrentGame.CurrentRoom != null)
                CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
            CurrentGame.RefreshAll();
            MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
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
            EquipmentMode = false;
            SelectedItem = null;
            ConversationMode = false;
        }
        public void SetExploreMode()
        {
            ExploreMode = true;
            InventoryMode = false;
            EquipmentMode = false;
            ConversationMode = false;
            if (CurrentGame.CurrentRoom != null)
                CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
            CurrentGame.RefreshAll();
            MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
        }
        public void SetConversationMode()
        {
            InventoryMode = false;
            ExploreMode = false;
            EquipmentMode = false;
            ConversationMode = true;
            SelectedItem = null;
        }
        public void ViewEquipment()
        {
            InventoryMode = false;
            ExploreMode = false;
            EquipmentMode = true;
            SelectedItem = null;
            ConversationMode = false;
            RefreshEquippableItems();
        }

        private void RefreshEquippableItems()
        {
            EquippableItems = new ObservableCollection<ItemInstance>(CurrentGame.PlayerInventory.Where(a => a.item.IsEquipment && a.item.EquipmentRef.OccupiesSlots.Count() > 0));
            EquippedItems = new ObservableCollection<ItemInstance>(CurrentGame.EquippedItems.Where(a => a.Value != null).Select(a => a.Value).Distinct());
        }
        public void UseItem()
        {
            if (SelectedItem != null)
            {
                WriteText("", null, true);
                SelectedItem.UseItem();
                if (ConversationMode == false)
                {
                    SetExploreMode();
                    MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
                }
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
        /// The <see cref="EquippableItems" /> property's name.
        /// </summary>
        public const string EquippableItemsPropertyName = "EquippableItems";

        private ObservableCollection<ItemInstance> _equipableItems = new ObservableCollection<ItemInstance>();

        /// <summary>
        /// Sets and gets the EquippableItems property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ItemInstance> EquippableItems
        {
            get
            {
                return _equipableItems;
            }

            set
            {
                if (_equipableItems == value)
                {
                    return;
                }

                _equipableItems = value;
                RaisePropertyChanged(EquippableItemsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="EquippedItems" /> property's name.
        /// </summary>
        public const string EquippedItemsPropertyName = "EquippedItems";

        private ObservableCollection<ItemInstance> _equippedItems = new ObservableCollection<ItemInstance>();

        /// <summary>
        /// Sets and gets the EquippedItems property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ItemInstance> EquippedItems
        {
            get
            {
                return _equippedItems;
            }

            set
            {
                if (_equippedItems == value)
                {
                    return;
                }

                _equippedItems = value;
                RaisePropertyChanged(EquippedItemsPropertyName);
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
                    CurrentItemDescription.Clear();
                    foreach (var res in SelectedItem.GetDescription())
                    {
                        CurrentItemDescription.Add(res);
                    }
                }
                else
                {
                    CurrentItemDescription.Clear();
                }
                RaisePropertyChanged(SelectedItemPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedEquippableItem" /> property's name.
        /// </summary>
        public const string SelectedEquippableItemPropertyName = "SelectedEquippableItem";

        private ItemInstance _selectedEquippableItem = null;

        /// <summary>
        /// Sets and gets the SelectedEquippableItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemInstance SelectedEquippableItem
        {
            get
            {
                return _selectedEquippableItem;
            }

            set
            {
                if (_selectedEquippableItem == value)
                {
                    return;
                }

                _selectedEquippableItem = value;
                RaisePropertyChanged(SelectedEquippableItemPropertyName);
                if (SelectedEquippableItem != null)
                {
                    
                    SelectedEquippedItem = null;
                    CurrentItemDescription.Clear();
                    CurrentItemDescription.Add(SelectedEquippableItem.CurrentName + ":\n\n");
                    foreach (var a in SelectedEquippableItem.GetDescription())
                    {
                        CurrentItemDescription.Add(a);
                    }
                }
                else
                {
                    CurrentItemDescription.Clear();
                }
            }
        }
        /// <summary>
        /// The <see cref="SelectedEquippedItem" /> property's name.
        /// </summary>
        public const string SelectedEquippedItemPropertyName = "SelectedEquippedItem";

        private ItemInstance _selectedEquippedItem = null;

        /// <summary>
        /// Sets and gets the SelectedEquippedItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemInstance SelectedEquippedItem
        {
            get
            {
                return _selectedEquippedItem;
            }

            set
            {
                if (_selectedEquippedItem == value)
                {
                    return;
                }

                _selectedEquippedItem = value;
                RaisePropertyChanged(SelectedEquippedItemPropertyName);
                if (SelectedEquippedItem != null)
                {
                    
                    SelectedEquippableItem = null;
                    CurrentItemDescription.Clear();

                    CurrentItemDescription.Add(SelectedEquippedItem.CurrentName + " (Equipped)\n\n");
                    foreach (var a in SelectedEquippedItem.GetDescription())
                    {
                        CurrentItemDescription.Add(a);
                    }
                }
                else
                {
                    CurrentItemDescription.Clear();
                }
            }
        }
        /// <summary>
        /// The <see cref="CurrentItemDescription" /> property's name.
        /// </summary>
        public const string CurrentItemDescriptionPropertyName = "CurrentItemDescription";

        private ObservableCollection<object> _currentItemDescription = new ObservableCollection<object>();

        /// <summary>
        /// Sets and gets the CurrentItemDescription property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<object> CurrentItemDescription
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
        /// The <see cref="EquipmentMode" /> property's name.
        /// </summary>
        public const string EquipmentModePropertyName = "EquipmentMode";

        private bool _equipmentMode = false;

        /// <summary>
        /// Sets and gets the EquipmentMode property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool EquipmentMode
        {
            get
            {
                return _equipmentMode;
            }

            set
            {
                if (_equipmentMode == value)
                {
                    return;
                }

                _equipmentMode = value;
                RaisePropertyChanged(EquipmentModePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ConversationMode" /> property's name.
        /// </summary>
        public const string ConversationModePropertyName = "ConversationMode";

        private bool _conversationMode = false;

        /// <summary>
        /// Sets and gets the ConversationMode property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ConversationMode
        {
            get
            {
                return _conversationMode;
            }

            set
            {
                if (_conversationMode == value)
                {
                    return;
                }

                _conversationMode = value;
                RaisePropertyChanged(ConversationModePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentConversation" /> property's name.
        /// </summary>
        public const string CurrentConversationPropertyName = "CurrentConversation";

        private ConversationWrapper _currentConversation = null;

        /// <summary>
        /// Sets and gets the CurrentConversation property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ConversationWrapper CurrentConversation
        {
            get
            {
                return _currentConversation;
            }

            set
            {
                if (_currentConversation == value)
                {
                    return;
                }

                _currentConversation = value;
                RaisePropertyChanged(CurrentConversationPropertyName);
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

        public void EquipItem()
        {
            if (SelectedEquippableItem != null)
            {
                var unequipped = CurrentGame.TryEquipItem(SelectedEquippableItem);
                
                CurrentGame.RefreshAll();
                foreach (var a in unequipped)
                {
                    CurrentGame.PlayerInventory.Add(a);
                }
                RefreshEquippableItems();
                ViewEquipment();
                SelectedEquippableItem = null;
                
                
                MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
            }
        }
        public void UnequipItem()
        {
            if (SelectedEquippedItem != null)
            {
                var i = SelectedEquippedItem;
                if (CurrentGame.TryUnequipItem(SelectedEquippedItem))
                {
                    CurrentGame.PlayerInventory.Add(i);
                }
                CurrentGame.RefreshAll();
                RefreshEquippableItems();
                ViewEquipment();
                SelectedEquippedItem = null;
                MainViewModel.GetMainViewModelStatic().CurrentGame.RunActiveEvents();
            }
        }

        public void SaveGame()
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Adventure Game Save Files (*.ags)|*.ags";
            if (sfd.ShowDialog().Value)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                var sw = new StreamWriter(fs);
                sw.Write(ToXML(sfd.FileName).ToString());
                sw.Flush();
                sw.Close();
            }
            
        }

        public XElement ToXML(string SaveLocation)
        {
            var events = from a in CurrentGame.ActiveEvents select a.ToXML();
            var inventory = from a in CurrentGame.PlayerInventory select a.ToXML();
            var equippedItems = from a in CurrentGame.EquippedItems.Distinct() where a.Value != null select a.Value.ToXML();
            var variables = from a in CurrentGame.VarById select new XElement("Variable", new XElement("ID",a.Key), a.Value.ToXML());
            var arrays = from a in CurrentGame.ArraysById select new XElement("Array", new XElement("ID", a.Key), new XElement("Values", (from b in a.Value select 
                                                                                                    (b.GetType() == typeof(int) ? new XElement("Value", new XElement("Type","Number"), new XElement("Value",(int)b)) :
                                                                                                     (b.GetType() == typeof(ItemInstance) ? new XElement("Value", new XElement("Type", "Item" ), new XElement("Value", ((ItemInstance)b).ToXML())) :
                                                                                                     new XElement("Value", new XElement("Type","String"), new XElement("Value", b.ToString())))))));

            return new XElement("SavedGame",
                new XElement("Events", events),
                new XElement("Inventory", inventory),
                new XElement("Equipment", equippedItems),
                new XElement("Variables", variables),
                new XElement("Arrays", arrays),
                new XElement("CurrentRoom",CurrentGame.CurrentRoom.RoomBase.RoomID),
                new XElement("Location", Editor.MainViewModel.GetRelativePath(SaveLocation, Location))
                );
        }
        public static MainViewModel FromXML(XElement saveXml, string fileLocation)
        {
            string location = saveXml.Element("Location").Value;
            string gameLocation = Editor.MainViewModel.AbsolutePath(fileLocation, location);
            FileStream fs = new FileStream(gameLocation, FileMode.Open);
            var sr = new StreamReader(fs);
            var loadXml = XElement.Parse(sr.ReadToEnd());
            sr.Close();

            var mvm = new MainViewModel();
            mvm.Location = gameLocation;
            //player.DataContext = mvm;
            mvm.CurrentGame = Game.FromXml(loadXml);

            var currentRoom = Guid.Parse(saveXml.Element("CurrentRoom").Value);

            Editor.App.Current.Resources["MainViewModelStatic"] = mvm;

            foreach (var room in mvm.CurrentGame.Rooms)
            {
                if (room.Value.RoomBase.RoomID == currentRoom)
                    mvm.CurrentGame.CurrentRoom = room.Value;
            }

            mvm.CurrentGame.ActiveEvents = (from a in saveXml.Element("Events").Elements("Event") select ActiveEvent.FromXML(a)).ToList();
            mvm.CurrentGame.PlayerInventory = new ObservableCollection<ItemInstance>(from a in saveXml.Element("Inventory").Elements() select ItemInstance.FromXML(a, mvm.CurrentGame));
            
            foreach (var a in saveXml.Element("Equipment").Elements())
            {
                mvm.CurrentGame.TryEquipItem(ItemInstance.FromXML(a, mvm.CurrentGame), true);
            }
            foreach (var a in saveXml.Element("Variables").Elements())
            {
                var oldVar = mvm.CurrentGame.VarById[Guid.Parse(a.Element("ID").Value)];
                var newVar = VariableWrapper.FromXML(a.Element("Variable"), mvm.CurrentGame, mvm.CurrentGame.VarById[Guid.Parse(a.Element("ID").Value)].VariableBase);
                oldVar.CurrentCommonEventValue = newVar.CurrentCommonEventValue;
                oldVar.CurrentItemValue = newVar.CurrentItemValue;
                oldVar.CurrentNumberValue = newVar.CurrentNumberValue;
                oldVar.CurrentStringValue = newVar.CurrentStringValue;
            }
            foreach (var a in saveXml.Element("Arrays").Elements())
            {
                var arr = mvm.CurrentGame.ArraysById[Guid.Parse(a.Element("ID").Value)];
                foreach (var b in a.Element("Values").Elements())
                {
                    switch (b.Element("Type").Value)
                    {
                        case "Number":
                            arr.Add(Convert.ToInt32(b.Element("Value").Value));
                            break;
                        case "String":
                            arr.Add(b.Element("Value").Value);
                            break;
                        case "Item":
                            arr.Add(ItemInstance.FromXML(b.Element("Value").Element("Item"),mvm.CurrentGame));
                            break;
                    }
                }
            }
            mvm.CurrentGame.RefreshAll();
            return mvm;

        }

    }
}
