using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class ItemProperty : INotifyPropertyChanged
    {

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

        /// <summary>
        /// The <see cref="Value" /> property's name.
        /// </summary>
        public const string ValuePropertyName = "Value";

        private object _value = null;

        /// <summary>
        /// Sets and gets the Value property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;
                RaisePropertyChanged(ValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="BaseVariable" /> property's name.
        /// </summary>
        public const string BaseVariablePropertyName = "BaseVariable";

        private Variable _baseVariable = null;

        /// <summary>
        /// Sets and gets the BaseVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Variable BaseVariable
        {
            get
            {
                return _baseVariable;
            }

            set
            {
                if (_baseVariable == value)
                {
                    return;
                }

                _baseVariable = value;
                RaisePropertyChanged(BaseVariablePropertyName);
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
        /// The <see cref="UseDefaultValue" /> property's name.
        /// </summary>
        public const string UseDefaultValuePropertyName = "UseDefaultValue";

        private bool _useDefaultValue = true;

        /// <summary>
        /// Sets and gets the UseDefaultValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool UseDefaultValue
        {
            get
            {
                return _useDefaultValue;
            }

            set
            {
                if (_useDefaultValue == value)
                {
                    return;
                }

                _useDefaultValue = value;
                RaisePropertyChanged(UseDefaultValuePropertyName);
            }
        }

        public XElement ToXml()
        {
            if (_useDefaultValue != true)
            {
                string valType = "String";
                if (BaseVariable.IsDateTime)
                    valType = "DateTime";
                if (BaseVariable.IsNumber)
                    valType = "Number";
                if (BaseVariable.IsItem)
                    valType = "Item";
                var split = Name.Split(':');
                return new XElement("ItemProperty",
                    new XElement("ItemClass", split[0]),
                    new XElement("PropName", split[1]),
                    new XElement("Value", Value.ToString()),
                    new XElement("ValueType", valType));
            }
            else return null;
        }

        public static ItemProperty FromXml(XElement xml)
        {
            ItemProperty prop = new ItemProperty();
            if (xml.Element("ItemClass") != null && xml.Element("PropName") != null)
            {
                string itemClass = xml.Element("ItemClass").Value;
                string propName = xml.Element("PropName").Value;
                prop.Name = itemClass + ":" + propName;
                ItemClass parentClass = MainViewModel.MainViewModelStatic.ItemClasses.Where(a => a.Name == itemClass).FirstOrDefault();
                if (parentClass != null)
                {
                    prop.BaseVariable = parentClass.ItemProperties.Where(a => a.Name == propName).FirstOrDefault();
                    prop.UseDefaultValue = false;
                    
                }
            }
            if (xml.Element("Value") != null && xml.Element("ValueType") != null)
            {
                string type = xml.Element("ValueType").Value;
                string val = xml.Element("Value").Value;
                switch (type)
                {
                    case "String":
                        prop.Value = val;
                        break;
                    case "Number":
                        int num;
                        if (Int32.TryParse(val, out num))
                        {
                            prop.Value = num;
                        }
                        else
                        {
                            prop.UseDefaultValue = true;
                            prop.Value = prop.BaseVariable.DefaultNumber;
                        }
                        break;
                    case "DateTime":
                        DateTime time;
                        if (DateTime.TryParse(val, out time))
                        {
                            prop.Value = time;
                        }
                        else
                        {
                            prop.UseDefaultValue = true;
                            prop.Value = prop.BaseVariable.DefaultDateTime;
                        }
                        break;
                    case "Item":
                        prop.UseDefaultValue = true;
                        break;
                }
            }
            return prop;
        }
    }
}
