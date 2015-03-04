using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypeWrappers
{
    class ZoneWrapper : INotifyPropertyChanged
    {

        public ZoneWrapper(Zone z)
        {
            ZoneBase = z;
        }

        /// <summary>
        /// The <see cref="ZoneBase" /> property's name.
        /// </summary>
        public const string ZoneBasePropertyName = "ZoneBase";

        private Zone _zoneBase = null;

        /// <summary>
        /// Sets and gets the ZoneBase property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Zone ZoneBase
        {
            get
            {
                return _zoneBase;
            }

            set
            {
                if (_zoneBase == value)
                {
                    return;
                }

                _zoneBase = value;
                RaisePropertyChanged(ZoneBasePropertyName);
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
