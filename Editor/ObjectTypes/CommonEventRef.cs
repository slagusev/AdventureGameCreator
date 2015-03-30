using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{
    public class CommonEventRef : INotifyPropertyChanged
    {

        public CommonEventRef(Guid id)
        {
            LinkedCommonEventId = id;
        }

        /// <summary>
        /// The <see cref="LinkedCommonEventId" /> property's name.
        /// </summary>
        public const string LinkedCommonEventIdPropertyName = "LinkedCommonEventId";

        private Guid _linkedCommonEventId = Guid.Empty;

        /// <summary>
        /// Sets and gets the LinkedCommonEventId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid LinkedCommonEventId
        {
            get
            {
                return _linkedCommonEventId;
            }

            set
            {
                if (_linkedCommonEventId == value)
                {
                    return;
                }

                _linkedCommonEventId = value;
                RaisePropertyChanged(LinkedCommonEventIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LinkedCommonEvent" /> property's name.
        /// </summary>
        public const string LinkedCommonEventPropertyName = "LinkedCommonEvent";

        private CommonEvent _linkedCommonEvent = null;

        /// <summary>
        /// Sets and gets the LinkedCommonEvent property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CommonEvent LinkedCommonEvent
        {
            get
            {
                if (_linkedCommonEvent == null && _linkedCommonEventId != Guid.Empty)
                {
                    //Find the variable in the view model
                    var matches = MainViewModel.MainViewModelStatic.CommonEvents.Where(a => a.Id == _linkedCommonEventId);
                    if (matches.Count() > 0)
                    {
                        LinkedCommonEvent = matches.First();
                    }
                }
                return _linkedCommonEvent;
            }

            set
            {
                if (_linkedCommonEvent == value)
                {
                    return;
                }

                _linkedCommonEvent = value;
                RaisePropertyChanged(LinkedCommonEventPropertyName);
                if (value != null)
                    this.LinkedCommonEventId = value.Id;
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
