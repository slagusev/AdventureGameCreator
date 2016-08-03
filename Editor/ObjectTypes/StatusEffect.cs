using Editor.Scripter;
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
    
    public class StatusEffect : INotifyPropertyChanged
    {
        
        public StatusEffect()
        {
            OnInitialize.CanAddItem = true;
            OnInitialize.CanAddText = false;
            OnInitialize.CanComment = true;
            OnInitialize.CanConditional = true;
            OnInitialize.CanDisplayText = true;
            OnInitialize.CanReturn = false;
            OnInitialize.CanReturnValue = false;
            OnInitialize.CanSetVariable = true;
            OnInitialize.CanStartConversations = true;
            OnInitialize.CanStopGame = true;
            OnInitialize.HasTextFunctionality = true;
            OnInitialize.IsInConversation = false;
            OnInitialize.IsItemScript = false;
            OnInitialize.IsStatusEffect = true;
            OnInitialize.CurrentStatusEffect = this;


            OnStack.CanAddItem = true;
            OnStack.CanAddText = false;
            OnStack.CanComment = true;
            OnStack.CanConditional = true;
            OnStack.CanDisplayText = true;
            OnStack.CanReturn = false;
            OnStack.CanReturnValue = false;
            OnStack.CanSetVariable = true;
            OnStack.CanStartConversations = true;
            OnStack.CanStopGame = true;
            OnStack.HasTextFunctionality = true;
            OnStack.IsInConversation = false;
            OnStack.IsItemScript = false;
            OnStack.IsStatusEffect = true;
            OnStack.CurrentStatusEffect = this;

            OnMove.CanAddItem = true;
            OnMove.CanAddText = false;
            OnMove.CanComment = true;
            OnMove.CanConditional = true;
            OnMove.CanDisplayText = true;
            OnMove.CanReturn = false;
            OnMove.CanReturnValue = false;
            OnMove.CanSetVariable = true;
            OnMove.CanStartConversations = true;
            OnMove.CanStopGame = true;
            OnMove.HasTextFunctionality = true;
            OnMove.IsInConversation = false;
            OnMove.IsItemScript = false;
            OnMove.IsStatusEffect = true;
            OnMove.CurrentStatusEffect = this;

            CheckIfCleared.CanAddItem = true;
            CheckIfCleared.CanAddText = false;
            CheckIfCleared.CanComment = true;
            CheckIfCleared.CanConditional = true;
            CheckIfCleared.CanDisplayText = true;
            CheckIfCleared.CanReturn = true;
            CheckIfCleared.CanReturnValue = false;
            CheckIfCleared.CanSetVariable = true;
            CheckIfCleared.CanStartConversations = true;
            CheckIfCleared.CanStopGame = true;
            CheckIfCleared.HasTextFunctionality = true;
            CheckIfCleared.IsInConversation = false;
            CheckIfCleared.IsItemScript = false;
            CheckIfCleared.IsStatusEffect = true;
            CheckIfCleared.CurrentStatusEffect = this;

            OnFinish.CanAddItem = true;
            OnFinish.CanAddText = false;
            OnFinish.CanComment = true;
            OnFinish.CanConditional = true;
            OnFinish.CanDisplayText = true;
            OnFinish.CanReturn = false;
            OnFinish.CanReturnValue = false;
            OnFinish.CanSetVariable = true;
            OnFinish.CanStartConversations = true;
            OnFinish.CanStopGame = true;
            OnFinish.HasTextFunctionality = true;
            OnFinish.IsInConversation = false;
            OnFinish.IsItemScript = false;
            OnFinish.IsStatusEffect = true;
            OnFinish.CurrentStatusEffect = this;

            WireCommands();
        }

        private void WireCommands()
        {
            AddArgumentCommand = new RelayCommand(AddArgument);
            RemoveArgumentCommand = new RelayCommand(RemoveArgument);
        }
        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private Guid _id = Guid.NewGuid();

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                RaisePropertyChanged(IdPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = "Status Effect";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OnInitialize" /> property's name.
        /// </summary>
        public const string OnInitializePropertyName = "OnInitialize";

        private Script _onInitialize = new Scripter.Script();

        /// <summary>
        /// Sets and gets the OnInitialize property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnInitialize
        {
            get
            {
                return _onInitialize;
            }

            set
            {
                if (_onInitialize == value)
                {
                    return;
                }

                _onInitialize = value;
                RaisePropertyChanged(OnInitializePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanOccurMultipleTimes" /> property's name.
        /// </summary>
        public const string CanOccurMultipleTimesPropertyName = "CanOccurMultipleTimes";

        private bool _canOccurMultipleTimes = true;

        /// <summary>
        /// Sets and gets the CanOccurMultipleTimes property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanOccurMultipleTimes
        {
            get
            {
                return _canOccurMultipleTimes;
            }

            set
            {
                if (_canOccurMultipleTimes == value)
                {
                    return;
                }

                _canOccurMultipleTimes = value;
                RaisePropertyChanged(CanOccurMultipleTimesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OnStack" /> property's name.
        /// </summary>
        public const string OnStackPropertyName = "OnStack";

        private Script _onStack = new Script();

        /// <summary>
        /// Sets and gets the OnStack property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnStack
        {
            get
            {
                return _onStack;
            }

            set
            {
                if (_onStack == value)
                {
                    return;
                }

                _onStack = value;
                RaisePropertyChanged(OnStackPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OnMove" /> property's name.
        /// </summary>
        public const string OnMovePropertyName = "OnMove";

        private Script _onMove = new Script();

        /// <summary>
        /// Sets and gets the OnMove property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnMove
        {
            get
            {
                return _onMove;
            }

            set
            {
                if (_onMove == value)
                {
                    return;
                }

                _onMove = value;
                RaisePropertyChanged(OnMovePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CheckIfCleared" /> property's name.
        /// </summary>
        public const string CheckIfClearedPropertyName = "CheckIfCleared";

        private Script _checkIfCleared = new Script();

        /// <summary>
        /// Sets and gets the CheckIfCleared property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script CheckIfCleared
        {
            get
            {
                return _checkIfCleared;
            }

            set
            {
                if (_checkIfCleared == value)
                {
                    return;
                }

                _checkIfCleared = value;
                RaisePropertyChanged(CheckIfClearedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OnFinish" /> property's name.
        /// </summary>
        public const string OnFinishPropertyName = "OnFinish";

        private Script _onFinish = new Script();

        /// <summary>
        /// Sets and gets the OnFinish property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnFinish
        {
            get
            {
                return _onFinish;
            }

            set
            {
                if (_onFinish == value)
                {
                    return;
                }

                _onFinish = value;
                RaisePropertyChanged(OnFinishPropertyName);
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
        /// The <see cref="Group" /> property's name.
        /// </summary>
        public const string GroupPropertyName = "Group";

        private string _group = "Default";

        /// <summary>
        /// Sets and gets the Group property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Group
        {
            get
            {
                return _group;
            }

            set
            {
                if (_group == value)
                {
                    return;
                }

                _group = value;
                RaisePropertyChanged(GroupPropertyName);
                MainViewModel.MainViewModelStatic.StatusEffectGroups.RefreshInList(this);
            }
        }

        /// <summary>
        /// The <see cref="Arguments" /> property's name.
        /// </summary>
        public const string ArgumentsPropertyName = "Arguments";

        private ObservableCollection<StatusEffectValue> _arguments = new ObservableCollection<StatusEffectValue>();

        /// <summary>
        /// Sets and gets the Arguments property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<StatusEffectValue> Arguments
        {
            get
            {
                return _arguments;
            }

            set
            {
                if (_arguments == value)
                {
                    return;
                }

                _arguments = value;
                RaisePropertyChanged(ArgumentsPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SelectedArgument" /> property's name.
        /// </summary>
        public const string SelectedArgumentPropertyName = "SelectedArgument";
        private StatusEffectValue _selectedArgument = null;

        /// <summary>
        /// Sets and gets the SelectedStatusEffect property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public StatusEffectValue SelectedArgument
        {
            get
            {
                return _selectedArgument;
            }

            set
            {

                _selectedArgument = value;
                RaisePropertyChanged(SelectedArgumentPropertyName);
            }
        }

        public RelayCommand AddArgumentCommand { get; private set; }
        public RelayCommand RemoveArgumentCommand { get; private set; }

        private void AddArgument()
        {
            Arguments.Add(new StatusEffectValue { IsNumber = true, Name = "Argument " + (Arguments.Count() + 1) });
            RaisePropertyChanged(ArgumentsPropertyName);
        }

        private void RemoveArgument()
        {
            if (SelectedArgument != null && Arguments.Contains(SelectedArgument))
            {
                Arguments.Remove(SelectedArgument);
                SelectedArgument = null;
                RaisePropertyChanged(ArgumentsPropertyName);
            }
        }

        public XElement ToXML()
        {
            return new XElement("StatusEffect", 
                new XElement("Name", Name),
                new XElement("OnInitialize", this.OnInitialize.ToXML()),
                new XElement("OnMove", this.OnMove.ToXML()),
                new XElement("OnStack", this.OnStack.ToXML()),
                new XElement("CanOccurMultipleTimes", this.CanOccurMultipleTimes),
                new XElement("CheckIfCleared", this.CheckIfCleared.ToXML()),
                new XElement("OnFinish", this.OnFinish.ToXML()),
                new XElement("Id", Id), new XElement("Group", Group),
                new XElement("Arguments", Arguments.Select(arg =>
                    {
                        string type = "";
                        if (arg.IsNumber)
                        {
                                type = "int";
                        }
                        if (arg.IsString)
                        {
                            type = "string";
                        }
                        
                        return new XElement("Argument", new XElement("Name", arg.Name), new XElement("Type", type), new XElement("Id", arg.Id));
                    })));
        }
        
        public static StatusEffect FromXML(XElement xml)
        {
            StatusEffect se = new StatusEffect();
            se.Id = Guid.Parse(xml.Element("Id").Value);
            se.Name = xml.Element("Name").Value;
            se.Group = xml.Element("Group").Value;
            se.OnInitialize = Script.FromXML(xml.Element("OnInitialize").Element("Script"), se.OnInitialize);
            se.OnMove = Script.FromXML(xml.Element("OnMove").Element("Script"), se.OnMove);
            se.OnStack = Script.FromXML(xml.Element("OnStack").Element("Script"), se.OnStack);
            se.CanOccurMultipleTimes = Boolean.Parse(xml.Element("CanOccurMultipleTimes").Value);
            se.CheckIfCleared = Script.FromXML(xml.Element("CheckIfCleared").Element("Script"), se.CheckIfCleared);
            se.OnFinish = Script.FromXML(xml.Element("OnFinish").Element("Script"), se.CheckIfCleared);
            if (xml.Elements("Arguments").Count() > 0)
            {
                se.Arguments = new ObservableCollection<StatusEffectValue>(xml.Element("Arguments").Elements("Argument").Select(a =>
                {
                    StatusEffectValue val = new StatusEffectValue();
                    val.Name = a.Element("Name").Value;
                    switch (a.Element("Type").Value)
                    {
                        case "int":
                            val.IsNumber = true;
                            break;
                        case "string":
                            val.IsString = true;
                            break;
                    }
                    if (a.Element("Id") != null)
                    {
                        val.Id = Guid.Parse(a.Element("Id").Value);
                    }
                    return val;
                }));
            }
            return se;   
        }
    }
}
