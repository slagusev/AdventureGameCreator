using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.StatusEffects
{
    public class AddStatusEffect : ScriptLine
    {

        public static Dictionary<StatusEffect, List<AddStatusEffect>> AllAddStatusEffect = new Dictionary<StatusEffect, List<AddStatusEffect>>();

        /// <summary>
        /// The <see cref="AssociatedEffect" /> property's name.
        /// </summary>
        public const string AssociatedEffectPropertyName = "AssociatedEffect";

        private GenericRef<StatusEffect> _statusEffect = GenericRef<StatusEffect>.GetStatusEffectRef();

        /// <summary>
        /// Sets and gets the AssociatedEffect property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GenericRef<StatusEffect> AssociatedEffect
        {
            get
            {
                return _statusEffect;
            }

            set
            {
                if (_statusEffect == value)
                {
                    return;
                }
                if (_statusEffect.Ref != Guid.Empty && _statusEffect != null && AllAddStatusEffect.ContainsKey(_statusEffect.Value) && AllAddStatusEffect[_statusEffect.Value].Contains(this))
                {
                    AllAddStatusEffect[_statusEffect.Value].Remove(this);
                }
                _statusEffect = value;
                RaisePropertyChanged(AssociatedEffectPropertyName);
                if (value != null)
                {
                    if (AllAddStatusEffect.ContainsKey(value.Value))
                    {
                        AllAddStatusEffect[_statusEffect.Value].Add(this);
                    }
                    UpdateArguments(_statusEffect.Value, this);
                }
                
            }
        }

        /// <summary>
        /// The <see cref="Arguments" /> property's name.
        /// </summary>
        public const string ArgumentsPropertyName = "Arguments";

        private ObservableCollection<ScriptStatusEffectArgumentValue> _arguments = new ObservableCollection<ScriptStatusEffectArgumentValue>();

        /// <summary>
        /// Sets and gets the Arguments property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ScriptStatusEffectArgumentValue> Arguments
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

        public static void UpdateAllArguments(StatusEffect se)
        {
            if (AllAddStatusEffect.ContainsKey(se))
            {
                foreach (var script in AllAddStatusEffect[se])
                {
                    UpdateArguments(se, script);
                }
            }
        }


        private static void UpdateArguments(StatusEffect se, AddStatusEffect script)
        {
            //New arguments
            foreach (var arg in se.Arguments)
            {
                if (script.Arguments.Where(a => a.Id == arg.Id).Count() == 0)
                {
                    script.Arguments.Add(new ScriptStatusEffectArgumentValue
                    {
                        Id = arg.Id,
                        IsNumber = arg.IsNumber,
                        IsString = arg.IsString

                    });
                }
                
            }

            //Removed Arguments
            List<ScriptStatusEffectArgumentValue> markForDeletion = new List<ScriptStatusEffectArgumentValue>();
            foreach (var arg in script.Arguments)
            {
                if (se.Arguments.Where(a => a.Id == arg.Id).Count() == 0)
                {
                    markForDeletion.Add(arg);
                }
            }
            foreach (var arg in markForDeletion)
            {
                script.Arguments.Remove(arg);
            }

            //Update Argument Types and names if neccessary.

            foreach (var arg in se.Arguments)
            {
                var matching = script.Arguments.Where(a => a.Id == arg.Id).FirstOrDefault();
                if (matching != null)
                {
                    matching.Name = arg.Name;
                    matching.IsNumber = arg.IsNumber;
                    matching.IsString = arg.IsString;
                }
            }
        }


        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("AddStatusEffect",
                new XElement("AssociatedEffect", this.AssociatedEffect.Ref),
                new XElement("Arguments",this.Arguments.Select(a => new XElement("Argument",
                                                                new XElement("Id", a.Id),
                                                                new XElement("Value", a.IsNumber ? a.NumberValue.ToString() : a.StringValue)))));
        }

        public override string Plaintext
        {
            get
            {
                return "Add the " + (AssociatedEffect != null && AssociatedEffect.Value != null ? AssociatedEffect.Value.Name : "INVALID REFERENCE") + " status effect to the player.";
            }
        }

        internal static ScriptLine FromXML(XElement xml)
        {
            AddStatusEffect ase = new AddStatusEffect();
            ase.AssociatedEffect.Ref = Guid.Parse(xml.Element("AssociatedEffect").Value);
            if (ase.AssociatedEffect.Value != null)
            {
                UpdateArguments(ase.AssociatedEffect.Value,ase);
                foreach (var arg in ase.Arguments)
                {
                    var element = xml.Element("Arguments").Elements("Argument").Where(a => 
                        {
                            Guid id;
                            if (Guid.TryParse(a.Element("Id").Value, out id))
                            {
                                return id == arg.Id;
                            }
                            return false;
                        });
                    if (element.Count() > 0)
                    {
                        if (arg.IsNumber)
                        {
                            int parse = 0;
                            if (Int32.TryParse(element.First().Element("Value").Value, out parse))
                            {
                                arg.NumberValue = parse;
                            }

                        }
                        else
                        {
                            arg.StringValue = element.First().Element("Value").Value;
                        }
                    }
                }
            }
            return ase;
        }
    }

    public class ScriptStatusEffectArgumentValue : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="IsString" /> property's name.
        /// </summary>
        public const string IsStringPropertyName = "IsString";

        private bool _isString = false;

        /// <summary>
        /// Sets and gets the IsString property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsString
        {
            get
            {
                return _isString;
            }

            set
            {
                if (_isString == value)
                {
                    return;
                }

                _isString = value;
                RaisePropertyChanged(IsStringPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsNumber" /> property's name.
        /// </summary>
        public const string IsNumberPropertyName = "IsNumber";

        private bool _isNumber = false;

        /// <summary>
        /// Sets and gets the IsNumber property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsNumber
        {
            get
            {
                return _isNumber;
            }

            set
            {
                if (_isNumber == value)
                {
                    return;
                }

                _isNumber = value;
                RaisePropertyChanged(IsNumberPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringValue" /> property's name.
        /// </summary>
        public const string StringValuePropertyName = "StringValue";

        private string _stringValue = "";

        /// <summary>
        /// Sets and gets the StringValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string StringValue
        {
            get
            {
                return _stringValue;
            }

            set
            {
                if (_stringValue == value)
                {
                    return;
                }

                _stringValue = value;
                RaisePropertyChanged(StringValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="NumberValue" /> property's name.
        /// </summary>
        public const string NumberValuePropertyName = "NumberValue";

        private int _numberValue = 0;

        /// <summary>
        /// Sets and gets the NumberValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int NumberValue
        {
            get
            {
                return _numberValue;
            }

            set
            {
                if (_numberValue == value)
                {
                    return;
                }

                _numberValue = value;
                RaisePropertyChanged(NumberValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private Guid _id = Guid.Empty;

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

        private string _name = "";

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
