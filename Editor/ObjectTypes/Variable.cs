using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class Variable : INotifyPropertyChanged
    {

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _variableName = "";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Name
        {
            get
            {
                return _variableName;
            }

            set
            {
                if (_variableName == value)
                {
                    return;
                }

                _variableName = value;
                RaisePropertyChanged(NamePropertyName);
            }
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
                if (value)
                {
                    IsNumber = false;
                    IsDateTime = false;
                    IsItem = false;
                    
                }
                RaisePropertyChanged(DefaultValuePropertyName);
            }

        }


        /// <summary>
        /// The <see cref="DefaultString" /> property's name.
        /// </summary>
        public const string DefaultStringPropertyName = "DefaultString";

        private string _defaultString = "";

        /// <summary>
        /// Sets and gets the DefaultString property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string DefaultString
        {
            get
            {
                return _defaultString;
            }

            set
            {
                if (_defaultString == value)
                {
                    return;
                }

                _defaultString = value;
                RaisePropertyChanged(DefaultStringPropertyName);
                RaisePropertyChanged(DefaultValuePropertyName);
            }
            
        }

        /// <summary>
        /// The <see cref="IsDateTime" /> property's name.
        /// </summary>
        public const string IsDateTimePropertyName = "IsDateTime";

        private bool _isDateTime = false;

        /// <summary>
        /// Sets and gets the IsDateTime property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsDateTime
        {
            get
            {
                return _isDateTime;
            }

            set
            {
                if (_isDateTime == value)
                {
                    return;
                }

                _isDateTime = value;
                RaisePropertyChanged(IsDateTimePropertyName);
                if (value)
                {
                    IsString = false;
                    IsNumber = false;
                    IsItem = false;
                }
                RaisePropertyChanged(DefaultValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DefaultDateTime" /> property's name.
        /// </summary>
        public const string DefaultDateTimePropertyName = "DefaultDateTime";

        private DateTime _defaultDateTime = DateTime.Today;

        /// <summary>
        /// Sets and gets the DefaultDateTime property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public DateTime DefaultDateTime
        {
            get
            {
                return _defaultDateTime;
            }

            set
            {
                if (_defaultDateTime == value)
                {
                    return;
                }

                _defaultDateTime = value;
                RaisePropertyChanged(DefaultDateTimePropertyName);
                RaisePropertyChanged(DefaultValuePropertyName);
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
                if (value)
                {
                    IsString = false;
                    IsDateTime = false;
                    IsItem = false;
                }
                RaisePropertyChanged(DefaultValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsItem" /> property's name.
        /// </summary>
        public const string IsItemPropertyName = "IsItem";

        private bool _isItem = false;

        /// <summary>
        /// Sets and gets the IsItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsItem
        {
            get
            {
                return _isItem;
            }

            set
            {
                if (_isItem == value)
                {
                    return;
                }

                _isItem = value;
                if (value)
                {
                    IsDateTime = false;
                    IsNumber = false;
                    IsString = false;
                }
                RaisePropertyChanged(IsItemPropertyName);
            }
        }



        public XElement ToXml()
        {
            XElement value;
            string type = "";
            if (IsNumber)
            {
                value = new XElement("Value", DefaultNumber.ToString());
                type = "Number";
            }
            else if (IsDateTime)
            {
                value = new XElement("Value", DefaultDateTime.ToString());
                type = "DateTime";
            }
            else if (IsItem)
            {
                value = new XElement("Value", "");
                type = "Item";
            }
            else
            {
                value = new XElement("Value", DefaultString);
                type = "String";
            }

            return new XElement("Variable", new XElement("Name", Name), new XElement("Type", type), value, new XElement("Id", Id));

        }

        public static Variable FromXML(XElement xml)
        {
            string type = xml.Element("Type").Value;
            Variable v = new Variable();
            switch (type)
            {
                case "Number":
                    v.IsNumber = true;
                    v.IsString = false;
                    v.IsDateTime = false;
                    v.DefaultNumber = Convert.ToInt32(xml.Element("Value").Value);
                    break;
                case "DateTime":
                    v.IsNumber = false;
                    v.IsString = false;
                    v.IsDateTime = true;
                    v.DefaultDateTime = Convert.ToDateTime(xml.Element("Value").Value);
                    break;
                case "Item":
                    v.IsItem = true;
                    break;
                case "String":
                    v.IsNumber = false;
                    v.IsString = true;
                    v.IsDateTime = false;
                    v.DefaultString = xml.Element("Value").Value;
                    break;
            }
            v.Name = xml.Element("Name").Value;
            v.Id = Guid.Parse(xml.Element("Id").Value);
            return v;
        }


        /// <summary>
        /// The <see cref="DefaultNumber" /> property's name.
        /// </summary>
        public const string DefaultNumberPropertyName = "DefaultNumber";

        private int _defaultNumber = 0;

        /// <summary>
        /// Sets and gets the DefaultNumber property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int DefaultNumber
        {
            get
            {
                return _defaultNumber;
            }

            set
            {
                if (_defaultNumber == value)
                {
                    return;
                }

                _defaultNumber = value;
                RaisePropertyChanged(DefaultNumberPropertyName);
                RaisePropertyChanged(DefaultValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="DefaultValue" /> property's name.
        /// </summary>
        public const string DefaultValuePropertyName = "DefaultValue";

        private object _defaultValue = null;

        /// <summary>
        /// Sets and gets the DefaultValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return GetDefaultValue();
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
        public object GetDefaultValue()
        {
            if (this.IsDateTime)
            {
                return this.DefaultDateTime;
            }
            else if (this.IsNumber)
            {
                return this.DefaultNumber;
            }
            else if (this.IsString)
            {
                return this.DefaultString;
            }
            
            else return null;
        }
    }
}
