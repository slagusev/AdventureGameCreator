using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{
    class InteractableRef : INotifyPropertyChanged
    {

        public InteractableRef(Guid id)
        {
            LinkedInteractableId = id;
        }

        /// <summary>
        /// The <see cref="LinkedInteractableId" /> property's name.
        /// </summary>
        public const string LinkedInteractableIdPropertyName = "LinkedInteractableId";

        private Guid _linkedInteractableId = Guid.Empty;

        /// <summary>
        /// Sets and gets the LinkedInteractableId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid LinkedInteractableId
        {
            get
            {
                return _linkedInteractableId;
            }

            set
            {
                if (_linkedInteractableId == value)
                {
                    return;
                }

                _linkedInteractableId = value;
                RaisePropertyChanged(LinkedInteractableIdPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="LinkedInteractable" /> property's name.
        /// </summary>
        public const string LinkedInteractableiablePropertyName = "LinkedInteractableiable";

        private Interactable _linkedInteractableiable = null;

        /// <summary>
        /// Sets and gets the LinkedInteractableiable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summar>y
        public Interactable LinkedInteractable

        {
            get
            {
                if (_linkedInteractableiable == null && _linkedInteractableId != Guid.Empty)
                {
                    //Find the variable in the view model
                    var matches = MainViewModel.MainViewModelStatic.Interactables.Where(a => a.InteractableID == _linkedInteractableId);
                    if (matches.Count() > 0)
                    {
                        LinkedInteractable = matches.First();
                    }
                }
                return _linkedInteractableiable;
            }

            set
            {
                if (_linkedInteractableiable == value)
                {
                    return;
                }

                _linkedInteractableiable = value;
                RaisePropertyChanged(LinkedInteractableiablePropertyName);
                if (value != null)
                    this.LinkedInteractableId = value.InteractableID;
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
