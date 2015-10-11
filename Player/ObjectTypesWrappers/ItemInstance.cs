using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player.ObjectTypesWrappers
{
    class ItemInstance : INotifyPropertyChanged
    {
        public ItemInstance(Item i)
        {
            item = i;
            CurrentName = i.DefaultName;
            if (!string.IsNullOrWhiteSpace(i.Icon))
            {
                CurrentIconPath = new ImageRef { Path = Editor.MainViewModel.AbsolutePath(MainViewModel.GetMainViewModelStatic().Location, i.Icon) };
            }
            else
            {
                CurrentIconPath = new ImageRef { Path = "" };
            }
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

        public List<object> GetDescription()
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

        /// <summary>
        /// The <see cref="CurrentIconPath" /> property's name.
        /// </summary>
        public const string CurrentIconPathPropertyName = "CurrentIconPath";

        private ImageRef _currentIconPath = null;

        /// <summary>
        /// Sets and gets the CurrentIconPath property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ImageRef CurrentIconPath
        {
            get
            {
                return _currentIconPath;
            }

            set
            {
                if (_currentIconPath == value)
                {
                    return;
                }

                _currentIconPath = value;
                RaisePropertyChanged(CurrentIconPathPropertyName);
            }
        }

        internal XElement ToXML()
        {
            return new XElement("Item",
                                    new XElement("Properties", from a in this.Properties select new XElement("Property", new XElement("Name", a.Key), new XElement("Value", a.Value.ToXML()))),
                                    new XElement("Name", this.CurrentName != this.item.DefaultName ? this.CurrentName : ""),
                                    new XElement("IconPath", this.CurrentIconPath.Path != this.item.Icon ? this.CurrentIconPath.Path : ""),
                                    new XElement("CanBeDropped",this.CanBeDropped != this.item.Removable ? this.CanBeDropped.ToString() : ""),
                                    new XElement("ItemId", this.item.ItemID));
        }

        internal static ItemInstance FromXML(XElement xml, Game g)
        {
            ItemInstance i = new ItemInstance(new ItemRef { LinkedItemId = Guid.Parse(xml.Element("ItemId").Value) }.LinkedItem);
            if (xml.Element("Name").Value != null && xml.Element("Name").Value != "")
            {
                i.CurrentName = xml.Element("Name").Value;
            }
            if (xml.Element("IconPath").Value != null && xml.Element("IconPath").Value != "")
            {
                i.CurrentIconPath = new ImageRef { Path = xml.Element("IconPath").Value } ;
            }
            if (xml.Element("CanBeDropped").Value != null && xml.Element("CanBeDropped").Value != "")
            {
                i.CanBeDropped = Convert.ToBoolean(xml.Element("IconPath").Value);
            }
            foreach (var property in xml.Element("Properties").Elements("Property").Where(a => i.Properties.ContainsKey(a.Element("Name").Value)))
            {
                
                var propObj = i.Properties[property.Element("Name").Value];
                var propVar = VariableWrapper.FromXML(property.Element("Value").Element("Variable"),g, propObj.VariableBase);
                propObj.CurrentCommonEventValue = propVar.CurrentCommonEventValue;
                propObj.CurrentStringValue = propVar.CurrentStringValue;
                propObj.CurrentNumberValue = propVar.CurrentNumberValue;
                propObj.CurrentItemValue = propVar.CurrentItemValue;
            }

            return i;
        }
    }
}
