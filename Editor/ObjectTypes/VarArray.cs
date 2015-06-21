using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class VarArray : INotifyPropertyChanged
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
                MainViewModel.MainViewModelStatic.RecalculateArrayGroups();
            }
        }

        /// <summary>
        /// The <see cref="IsNumber" /> property's name.
        /// </summary>
        public const string IsNumberPropertyName = "IsNumber";

        private bool _isNumber = true;

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
                if (value)
                {
                    //IsNumber = false;
                    IsString = false;
                    IsCommonEvent = false;
                    IsItem = false;
                }
                RaisePropertyChanged(IsNumberPropertyName);
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
                if (value)
                {
                    IsNumber = false;
                    //IsString = false;
                    IsCommonEvent = false;
                    IsItem = false;
                }
                _isString = value;
                RaisePropertyChanged(IsStringPropertyName);
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
                    IsNumber = false;
                    IsString = false;
                    IsCommonEvent = false;
                    //IsItem = false;
                }
                RaisePropertyChanged(IsItemPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsCommonEvent" /> property's name.
        /// </summary>
        public const string IsCommonEventPropertyName = "IsCommonEvent";

        private bool _isCommonEvent = false;

        /// <summary>
        /// Sets and gets the IsCommonEvent property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsCommonEvent
        {
            get
            {
                return _isCommonEvent;
            }

            set
            {
                if (_isCommonEvent == value)
                {
                    return;
                }
                if (value)
                {
                    IsNumber = false;
                    IsString = false;
                    //IsCommonEvent = false;
                    IsItem = false;
                }
                _isCommonEvent = value;
                RaisePropertyChanged(IsCommonEventPropertyName);
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

        internal XElement ToXML()
        {
            return new XElement("Array", new XElement("Name", Name),
                                         new XElement("Id", Id),
                                         new XElement("Group", Group),
                                         new XElement("IsNumber", IsNumber),
                                         new XElement("IsString", IsString),
                                         new XElement("IsItem", IsItem),
                                         new XElement("IsCommonEvent", IsCommonEvent));
        }

        internal static VarArray FromXML(System.Xml.Linq.XElement element)
        {
            return new VarArray
            {
                Name = element.Element("Name").Value,
                Id = Guid.Parse(element.Element("Id").Value),
                Group = element.Element("Group").Value,
                IsNumber = Boolean.Parse(element.Element("IsNumber").Value),
                IsString = Boolean.Parse(element.Element("IsString").Value),
                IsItem = Boolean.Parse(element.Element("IsItem").Value),
                IsCommonEvent = Boolean.Parse(element.Element("IsCommonEvent").Value)
            };
        }
    }
}
