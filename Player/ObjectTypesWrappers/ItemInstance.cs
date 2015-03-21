using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypesWrappers
{
    class ItemInstance : INotifyPropertyChanged
    {
        public ItemInstance(Item i)
        {
            item = i;
            CurrentName = i.DefaultName;
            foreach (var property in i.ItemProperties)
            {
                var baseVar = property.BaseVariable;
                var wrapper = new VariableWrapper(baseVar);
                if (!property.UseDefaultValue)
                {
                    if (baseVar.IsDateTime)
                    {
                        wrapper.CurrentDateTimeValue = Convert.ToDateTime(property.Value);
                    }
                    else if (baseVar.IsNumber)
                    {
                        wrapper.CurrentNumberValue = Convert.ToInt32(property.Value);
                    }
                    else if (baseVar.IsString)
                    {
                        wrapper.CurrentStringValue = Convert.ToString(property.Value);
                    }
                    this.Properties.Add(property.Name, wrapper);
                }
                else
                {
                    wrapper.CurrentDateTimeValue = baseVar.DefaultDateTime;
                    wrapper.CurrentNumberValue = baseVar.DefaultNumber;
                    wrapper.CurrentStringValue = baseVar.DefaultString;
                    this.Properties.Add(property.Name, wrapper);
                    
                }
            }
            this.CanBeDropped = i.Removable;
            
        }

        public string GetDescription()
        {
            ScriptWrapper s = new ScriptWrapper(item.Description) { ItemBase = this };
            s.Execute();
            return s.TextResult;
        }
        public void UseItem()
        {
            ScriptWrapper s = new ScriptWrapper(item.OnUse) { ItemBase = this };
            s.Execute();
        }
        
        public Dictionary<string, VariableWrapper> Properties = new Dictionary<string, VariableWrapper>();

        public Item item = null;




        /// <summary>
        /// The <see cref="CurrentName" /> property's name.
        /// </summary>
        public const string CurrentNamePropertyName = "CurrentName";

        private string _currentName = "";

        /// <summary>
        /// Sets and gets the CurrentName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string CurrentName
        {
            get
            {
                return _currentName;
            }

            set
            {
                if (_currentName == value)
                {
                    return;
                }

                _currentName = value;
                RaisePropertyChanged(CurrentNamePropertyName);
            }
        }
        
        /// <summary>
        /// The <see cref="CanBeDropped" /> property's name.
        /// </summary>
        public const string CanBeDroppedPropertyName = "CanBeDropped";

        private bool _canBeDropped = false;

        /// <summary>
        /// Sets and gets the CanBeDropped property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanBeDropped
        {
            get
            {
                return _canBeDropped;
            }

            set
            {
                if (_canBeDropped == value)
                {
                    return;
                }

                _canBeDropped = value;
                RaisePropertyChanged(CanBeDroppedPropertyName);
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
