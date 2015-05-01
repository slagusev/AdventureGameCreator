using Editor.ObjectTypes;
using Player.ObjectTypesWrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypeWrappers
{
    class RoomWrapper : INotifyPropertyChanged
    {

        public RoomWrapper(Room r)
        {
            RoomBase = r;
            foreach (var exit in r.Exits)
            {
                Exits.Add(new ExitWrapper(exit));
            }
            foreach (var interactable in r.DefaultInteractables)
            {
                Interactables.Add(new InteractableWrapper(interactable.LinkedInteractable));
            }
        }

        public void RecalculateInteractableVisibility()
        {
            foreach (var interactable in Interactables)
            {
                interactable.RecalculateInteractableVisbility();
            }
        }


        /// <summary>
        /// The <see cref="Interactables" /> property's name.
        /// </summary>
        public const string InteractablesPropertyName = "Interactables";

        private ObservableCollection<InteractableWrapper> _interactables = new ObservableCollection<InteractableWrapper>();

        /// <summary>
        /// Sets and gets the Interactables property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<InteractableWrapper> Interactables
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

        /// <summary>
        /// The <see cref="Exits" /> property's name.
        /// </summary>
        public const string ExitsPropertyName = "Exits";

        private ObservableCollection<ExitWrapper> _exits = new ObservableCollection<ExitWrapper>();

        /// <summary>
        /// Sets and gets the Exits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ExitWrapper> Exits
        {
            get
            {
                return _exits;
            }

            set
            {
                if (_exits == value)
                {
                    return;
                }

                _exits = value;
                RaisePropertyChanged(ExitsPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="RoomBase" /> property's name.
        /// </summary>
        public const string RoomBasePropertyName = "RoomBase";

        private Room _roomBase = null;

        /// <summary>
        /// Sets and gets the RoomBase property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Room RoomBase
        {
            get
            {
                return _roomBase;
            }

            set
            {
                if (_roomBase == value)
                {
                    return;
                }

                _roomBase = value;
                RaisePropertyChanged(RoomBasePropertyName);
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
        public void RoomDescription()
        {
            if (RoomBase.HasPlaintextDescription)
            {
                MainViewModel.WriteText(RoomBase.Description, null);
            }
            else
            {
                new ScriptWrapper(RoomBase.RoomDescriptionScript).Execute();
            }
        }
    }
}
