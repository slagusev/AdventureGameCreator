using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    class Interactable : INotifyPropertyChanged
    {

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

        /// <summary>
        /// The <see cref="DefaultDisplayName" /> property's name.
        /// </summary>
        public const string DefaultDisplayNamePropertyName = "DefaultDisplayName";

        private string _defaultDisplayName = "";

        /// <summary>
        /// Sets and gets the DefaultDisplayName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string DefaultDisplayName
        {
            get
            {
                return _defaultDisplayName;
            }

            set
            {
                if (_defaultDisplayName == value)
                {
                    return;
                }

                _defaultDisplayName = value;
                RaisePropertyChanged(DefaultDisplayNamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="GroupName" /> property's name.
        /// </summary>
        public const string GroupNamePropertyName = "GroupName";

        private string _groupName = "";

        /// <summary>
        /// Sets and gets the GroupName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string GroupName
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
                RaisePropertyChanged(GroupNamePropertyName);
                MainViewModel.MainViewModelStatic.RecalculateInteractableGroups();
            }
        }

        /// <summary>
        /// The <see cref="InteractableID" /> property's name.
        /// </summary>
        public const string InteractableIDPropertyName = "InteractableID";

        private Guid _interactableID = Guid.NewGuid();

        /// <summary>
        /// Sets and gets the InteractableID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid InteractableID
        {
            get
            {
                return _interactableID;
            }

            set
            {
                if (_interactableID == value)
                {
                    return;
                }

                _interactableID = value;
                RaisePropertyChanged(InteractableIDPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanExamine" /> property's name.
        /// </summary>
        public const string CanExaminePropertyName = "CanExamine";

        private bool _canExamine = true;

        /// <summary>
        /// Sets and gets the CanExamine property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanExamine
        {
            get
            {
                return _canExamine;
            }

            set
            {
                if (_canExamine == value)
                {
                    return;
                }
                
                _canExamine = value;
                RaisePropertyChanged(CanExaminePropertyName);
                HasExamineScript = CanExamineUsesScript || CanExamine;
            }
        }
        /// <summary>
        /// The <see cref="CanExamineUsesScript" /> property's name.
        /// </summary>
        public const string CanExamineUsesScriptPropertyName = "CanExamineUsesScript";

        private bool _canExamineUsesScript = false;

        /// <summary>
        /// Sets and gets the CanExamineUsesScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanExamineUsesScript
        {
            get
            {
                return _canExamineUsesScript;
            }

            set
            {
                if (_canExamineUsesScript == value)
                {
                    return;
                }

                _canExamineUsesScript = value;
                RaisePropertyChanged(CanExamineUsesScriptPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CanExamineScript" /> property's name.
        /// </summary>
        public const string CanExamineScriptPropertyName = "CanExamineScript";

        private Scripter.Script _canExamineScript = new Scripter.Script();

        /// <summary>
        /// Sets and gets the CanExamineScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script CanExamineScript
        {
            get
            {
                return _canExamineScript;
            }

            set
            {
                if (_canExamineScript == value)
                {
                    return;
                }

                _canExamineScript = value;
                RaisePropertyChanged(CanExamineScriptPropertyName);
                HasExamineScript = CanExamineUsesScript || CanExamine;
            }
        }

        /// <summary>
        /// The <see cref="HasExamineScript" /> property's name.
        /// </summary>
        public const string HasExamineScriptPropertyName = "HasExamineScript";

        private bool _hasExamineScript = true;

        /// <summary>
        /// Sets and gets the HasExamineScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool HasExamineScript
        {
            get
            {
                return _hasExamineScript;
            }

            set
            {
                if (_hasExamineScript == value)
                {
                    return;
                }

                _hasExamineScript = value;
                RaisePropertyChanged(HasExamineScriptPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HasInteractScript" /> property's name.
        /// </summary>
        public const string HasInteractScriptPropertyName = "HasInteractScript";

        private bool _hasInteractScript = true;

        /// <summary>
        /// Sets and gets the HasInteractScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool HasInteractScript
        {
            get
            {
                return _hasInteractScript;
            }

            set
            {
                if (_hasInteractScript == value)
                {
                    return;
                }

                _hasInteractScript = value;
                RaisePropertyChanged(HasInteractScriptPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanInteract" /> property's name.
        /// </summary>
        public const string CanInteractPropertyName = "CanInteract";

        private bool _canInteract = true;

        /// <summary>
        /// Sets and gets the CanInteract property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanInteract
        {
            get
            {
                return _canInteract;
            }

            set
            {
                if (_canInteract == value)
                {
                    return;
                }

                _canInteract = value;
                RaisePropertyChanged(CanInteractPropertyName);
                HasInteractScript = CanInteract || CanInteractUsesScript;
            }
        }


        /// <summary>
        /// The <see cref="CanInteractUsesScript" /> property's name.
        /// </summary>
        public const string CanInteractUsesScriptPropertyName = "CanInteractUsesScript";

        private bool _canInteractUsesScript = false;

        /// <summary>
        /// Sets and gets the CanInteractUsesScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanInteractUsesScript
        {
            get
            {
                return _canInteractUsesScript;
            }

            set
            {
                if (_canInteractUsesScript == value)
                {
                    return;
                }

                _canInteractUsesScript = value;
                RaisePropertyChanged(CanInteractUsesScriptPropertyName);
                HasInteractScript = CanInteract || CanInteractUsesScript;
            }
        }

        /// <summary>
        /// The <see cref="CanInteractScript" /> property's name.
        /// </summary>
        public const string CanInteractScriptPropertyName = "CanInteractScript";

        private Script _canInteractScript = new Scripter.Script();

        /// <summary>
        /// Sets and gets the CanInteractScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script CanInteractScript
        {
            get
            {
                return _canInteractScript;
            }

            set
            {
                if (_canInteractScript == value)
                {
                    return;
                }

                _canInteractScript = value;
                RaisePropertyChanged(CanInteractScriptPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ExamineScript" /> property's name.
        /// </summary>
        public const string ExamineScriptPropertyName = "ExamineScript";

        private Scripter.Script _examineScript = new Scripter.Script();

        /// <summary>
        /// Sets and gets the ExamineScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script ExamineScript
        {
            get
            {
                return _examineScript;
            }

            set
            {
                if (_examineScript == value)
                {
                    return;
                }

                _examineScript = value;
                RaisePropertyChanged(ExamineScriptPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="InteractScript" /> property's name.
        /// </summary>
        public const string InteractScriptPropertyName = "InteractScript";

        private Scripter.Script _interactScript = new Scripter.Script();

        /// <summary>
        /// Sets and gets the InteractScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script InteractScript
        {
            get
            {
                return _interactScript;
            }

            set
            {
                if (_interactScript == value)
                {
                    return;
                }

                _interactScript = value;
                RaisePropertyChanged(InteractScriptPropertyName);
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

        public XElement ToXML()
        {
            return new XElement("Interactable",
                new XElement("Name", this.InteractableName),
                new XElement("Id", this.InteractableID),
                new XElement("DefaultDisplayName", this.DefaultDisplayName),
                new XElement("CanExamine", this.CanExamine),
                new XElement("CanExamineUsesScript", this.CanExamineUsesScript),
                new XElement("CanExamineScript", this.CanExamineScript.ToXML()),
                new XElement("CanInteract", this.CanInteract),
                new XElement("CanInteractUsesScript", this.CanInteractUsesScript),
                new XElement("CanInteractScript", this.CanInteractScript.ToXML()),
                new XElement("ExamineScript", this.ExamineScript.ToXML()),
                new XElement("InteractScript", this.InteractScript.ToXML()),
                new XElement("GroupName", this.GroupName));
        }

        public static Interactable FromXML(XElement xml)
        {
            Interactable i = new Interactable();
            if (xml.Element("Name") != null)
            {
                i.InteractableName = xml.Element("Name").Value;
            }
            Guid id;
            if (xml.Element("Id") != null && Guid.TryParse(xml.Element("Id").Value, out id))
            {
                i.InteractableID = id;
            }
            if (xml.Element("DefaultDisplayName") != null)
            {
                i.DefaultDisplayName = xml.Element("DefaultDisplayName").Value;
            }
            bool tempBool;
            if (xml.Element("CanExamine") != null && Boolean.TryParse(xml.Element("CanExamine").Value, out tempBool))
            {
                i.CanExamine = tempBool;
            }
            if (xml.Element("CanExamineUsesScript") != null && Boolean.TryParse(xml.Element("CanExamineUsesScript").Value, out tempBool))
            {
                i.CanExamineUsesScript = tempBool;
            }
            if (xml.Element("CanExamineScript") != null)
            {
                i.CanExamineScript = Script.FromXML(xml.Element("CanExamineScript").Element("Script"));
            }
            if (xml.Element("CanInteract") != null && Boolean.TryParse(xml.Element("CanInteract").Value, out tempBool))
            {
                i.CanInteract = tempBool;
            }
            if (xml.Element("CanInteractUsesScript") != null && Boolean.TryParse(xml.Element("CanInteractUsesScript").Value, out tempBool))
            {
                i.CanInteractUsesScript = tempBool;
            }
            if (xml.Element("CanInteractScript") != null)
            {
                i.CanInteractScript = Script.FromXML(xml.Element("CanInteractScript").Element("Script"));
            }
            if (xml.Element("ExamineScript") != null)
            {
                i.ExamineScript = Script.FromXML(xml.Element("ExamineScript").Element("Script"));
            }
            if (xml.Element("InteractScript") != null)
            {
                i.InteractScript = Script.FromXML(xml.Element("InteractScript").Element("Script"));
            }
            if (xml.Element("GroupName") != null)
            {
                i._groupName = xml.Element("GroupName").Value;
            }
            return i;
        }
    }
}
