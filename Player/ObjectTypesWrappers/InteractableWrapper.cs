using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypesWrappers
{
    class InteractableWrapper : INotifyPropertyChanged
    {

        public InteractableWrapper(Interactable i)
        {
            InteractableBase = i;
            InteractableName = i.DefaultDisplayName;
        }
        /// <summary>
        /// The <see cref="InteractableName" /> property's name.
        /// </summary>
        public const string InteractableNamePropertyName = "InteractableName";

        private string _interactableName = "";

        /// <summary>
        /// Sets and gets the InteractableName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string InteractableName
        {
            get
            {
                return _interactableName;
            }

            set
            {
                if (_interactableName == value)
                {
                    return;
                }

                _interactableName = value;
                RaisePropertyChanged(InteractableNamePropertyName);
            }
        }
        public void RecalculateInteractableVisbility()
        {
            if (InteractableBase.CanExamineUsesScript)
            {
                var scriptResult = new ScriptWrapper(InteractableBase.CanExamineScript).Execute();
                if (scriptResult == true)
                    IsExaminable = true;
                else IsExaminable = false;
            }
            else IsExaminable = InteractableBase.CanExamine;
            if (InteractableBase.CanInteractUsesScript)
            {

                var scriptResult = new ScriptWrapper(InteractableBase.CanInteractScript).Execute();
                if (scriptResult == true)
                    IsInteractable = true;
                else IsInteractable = false;
            }
            else IsInteractable = InteractableBase.CanInteract;
        }
        public void Examine()
        {
            MainViewModel.WriteText("-------------------------------------------");
            new ScriptWrapper(InteractableBase.ExamineScript).Execute();
            MainViewModel.GetMainViewModelStatic().CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
        }
        public void Interact()
        {
            MainViewModel.WriteText("-------------------------------------------");
            new ScriptWrapper(InteractableBase.InteractScript).Execute();
            MainViewModel.GetMainViewModelStatic().CurrentGame.CurrentRoom.RecalculateInteractableVisibility();
        }
        /// <summary>
        /// The <see cref="IsExaminable" /> property's name.
        /// </summary>
        public const string IsExaminablePropertyName = "IsExaminable";

        private bool _isExaminable = false;

        /// <summary>
        /// Sets and gets the IsExaminable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsExaminable
        {
            get
            {
                return _isExaminable;
            }

            set
            {
                if (_isExaminable == value)
                {
                    return;
                }

                _isExaminable = value;
                RaisePropertyChanged(IsExaminablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsInteractable" /> property's name.
        /// </summary>
        public const string IsInteractablePropertyName = "IsInteractable";

        private bool _isInteractable = false;

        /// <summary>
        /// Sets and gets the IsInteractable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsInteractable
        {
            get
            {
                return _isInteractable;
            }

            set
            {
                if (_isInteractable == value)
                {
                    return;
                }

                _isInteractable = value;
                RaisePropertyChanged(IsInteractablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="InteractableBase" /> property's name.
        /// </summary>
        public const string InteractableBasePropertyName = "InteractableBase";

        private Interactable _interactableBase = null;

        /// <summary>
        /// Sets and gets the InteractableBase property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Interactable InteractableBase
        {
            get
            {
                return _interactableBase;
            }

            set
            {
                if (_interactableBase == value)
                {
                    return;
                }

                _interactableBase = value;
                RaisePropertyChanged(InteractableBasePropertyName);
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
