using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{
    public class InteractableGroup : INotifyPropertyChanged
    {

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _groupName = "";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Name
        {
            get
            {
                return _groupName;
            }

            set
            {
                if (_groupName == value)
                {
                    return;
                }

                _groupName = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Interactables" /> property's name.
        /// </summary>
        public const string InteractablesPropertyName = "Interactables";

        private ObservableCollection<Interactable> _interactables = new ObservableCollection<Interactable>();

        /// <summary>
        /// Sets and gets the Interactables property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Interactable> Interactables
        {
            get
            {
                return _interactables;
            }

            set
            {
                if (_interactables == value)
                {
                    return;
                }

                _interactables = value;
                RaisePropertyChanged(InteractablesPropertyName);
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
