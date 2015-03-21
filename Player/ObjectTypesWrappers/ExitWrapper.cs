using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypesWrappers
{
    class ExitWrapper : INotifyPropertyChanged
    {

        public ExitWrapper(Exit e)
        {
            ExitBase = e;
            if (string.IsNullOrWhiteSpace(ExitBase.ExitName))
            {
                if (ExitBase.Direction == ExitDirection.Other)
                    ExitBase.ExitName = ExitBase.RoomLink.RoomName;
                else
                    ExitBase.ExitName = ExitBase.Direction.ToString();
            }
        }

        /// <summary>
        /// The <see cref="ExitBase" /> property's name.
        /// </summary>
        public const string ExitBasePropertyName = "ExitBase";

        private Exit _exitBase = null;

        /// <summary>
        /// Sets and gets the ExitBase property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Exit ExitBase
        {
            get
            {
                return _exitBase;
            }

            set
            {
                if (_exitBase == value)
                {
                    return;
                }

                _exitBase = value;
                RaisePropertyChanged(ExitBasePropertyName);
            }
        }

        public bool IsVisible()
        {
            var visibility = new ScriptWrapper(ExitBase.ExitVisibility).Execute();
            return (visibility != false);
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
