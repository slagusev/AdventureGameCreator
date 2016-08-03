using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{

    public class StatusEffectValue : INotifyPropertyChanged
    {

        public StatusEffectValue()
        {
            this.Id = Guid.NewGuid() ;
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
                    IsString = false;
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

                _isString = value;
                if (value)
                    IsNumber = false;
                RaisePropertyChanged(IsStringPropertyName);
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
