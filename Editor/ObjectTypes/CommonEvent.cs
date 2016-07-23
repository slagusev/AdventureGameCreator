using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class CommonEvent : INotifyPropertyChanged
    {
        public static ObservableCollection<Tuple<string, string, Script>> CommonEventTypes { get; set; }

        public static string ScriptTypeGeneral = "General";
        public static string ScriptTypeMovementAndInteractable = "Movement & Interactable Script";
        public static string ScriptTypeItem = "Item Script";
        public static string ScriptTypeDescription = "Description Script";
        public static string ScriptTypeTrueFalse = "True/False Script";
        public static string ScriptTypeReturnedValue = "Returned Value Script";
        public static string ScriptTypeOverwriteLocals = "Subscript";

        static CommonEvent()
        {
            CommonEventTypes = new ObservableCollection<Tuple<string,string,Script>>();

            Script AllSituationsScript = new Script();
            AllSituationsScript.CanAddItem = false;
            AllSituationsScript.CanAddText = false;
            AllSituationsScript.CanComment = true;
            AllSituationsScript.CanConditional = true;
            AllSituationsScript.CanDisplayText = false;
            AllSituationsScript.CanReturn = false;
            AllSituationsScript.CanSetVariable = true;
            AllSituationsScript.CanStopGame = false;
            AllSituationsScript.HasTextFunctionality = false;
            AllSituationsScript.IsItemScript = false;
            AllSituationsScript.IsInConversation = false;
            AllSituationsScript.CanStartConversations = false;
            
            AddCommonEventType("General", "A common event that can be used in any situation. Its functionality is limited to setting variables and checking conditions.", AllSituationsScript);

            Script MovementScript = new Script();
            MovementScript.CanAddItem = true;
            MovementScript.CanAddText = false;
            MovementScript.CanComment = true;
            MovementScript.CanConditional = true;
            MovementScript.CanDisplayText = true;
            MovementScript.CanReturn = true;
            MovementScript.CanSetVariable = true;
            MovementScript.CanStopGame = true;
            MovementScript.HasTextFunctionality = true;
            MovementScript.IsItemScript = false;
            MovementScript.IsInConversation = false;
            MovementScript.CanStartConversations = true;
            MovementScript.AllowedCommonEventTypes.Add(ScriptTypeMovementAndInteractable);
            AddCommonEventType("Movement & Interactable Script", "A common event that is used when transitioning from one room to another, or when used by an interactable.", MovementScript);

            Script ItemScript = new Script();
            ItemScript.CanAddItem = true;
            ItemScript.CanAddText = false;
            ItemScript.CanComment = true;
            ItemScript.CanConditional = true;
            ItemScript.CanDisplayText = true;
            ItemScript.CanReturn = false;
            ItemScript.CanSetVariable = true;
            ItemScript.CanStopGame = false;
            ItemScript.HasTextFunctionality = true;
            ItemScript.IsItemScript = true;
            ItemScript.IsInConversation = true;
            ItemScript.CanStartConversations = false;
            ItemScript.AllowedCommonEventTypes.Add(ScriptTypeItem);
            AddCommonEventType("Item Script", "A common event that can be used on item usage.", ItemScript);

            Script DescriptionScript = new Script();
            DescriptionScript.CanAddItem = false;
            DescriptionScript.CanAddText = true;
            DescriptionScript.CanComment = true;
            DescriptionScript.CanConditional = true;
            DescriptionScript.CanDisplayText = false;
            DescriptionScript.CanReturn = false;
            DescriptionScript.CanSetVariable = true;
            DescriptionScript.CanStopGame = false;
            DescriptionScript.HasTextFunctionality = true;
            DescriptionScript.IsItemScript = false;
            DescriptionScript.IsInConversation = false;
            DescriptionScript.CanStartConversations = false;
            DescriptionScript.AllowedCommonEventTypes.Add(ScriptTypeDescription);
            AddCommonEventType("Description Script", "A common event that can be used for item descriptions, conversations, and player descriptions.", DescriptionScript);

            Script TrueFalseScript = new Script();
            TrueFalseScript.CanAddItem = false;
            TrueFalseScript.CanAddText = false;
            TrueFalseScript.CanComment = true;
            TrueFalseScript.CanConditional = true;
            TrueFalseScript.CanDisplayText = true;
            TrueFalseScript.CanReturn = true;
            TrueFalseScript.CanSetVariable = true;
            TrueFalseScript.CanStopGame = false;
            TrueFalseScript.HasTextFunctionality = true;
            TrueFalseScript.IsItemScript = false;
            TrueFalseScript.AllowedCommonEventTypes.Add(ScriptTypeTrueFalse);
            TrueFalseScript.IsInConversation = false;
            TrueFalseScript.CanStartConversations = false;
            AddCommonEventType("True/False Script", "A common event that returns a True or False value to the parent, typically used in determining visibility. Limited functionality.", TrueFalseScript);

            Script ReturnedValueScript = new Script();
            ReturnedValueScript.CanAddItem = false;
            ReturnedValueScript.CanAddText = false;
            ReturnedValueScript.CanComment = true;
            ReturnedValueScript.CanConditional = true;
            ReturnedValueScript.CanDisplayText = false;
            ReturnedValueScript.CanReturn = false;
            ReturnedValueScript.CanSetVariable = true;
            ReturnedValueScript.CanStopGame = false;
            ReturnedValueScript.HasTextFunctionality = true;
            ReturnedValueScript.IsItemScript = false;
            ReturnedValueScript.CanReturnValue = true;
            TrueFalseScript.IsInConversation = false;
            TrueFalseScript.CanStartConversations = false;
            ReturnedValueScript.AllowedCommonEventTypes.Add(ScriptTypeReturnedValue);
            AddCommonEventType("Returned Value Script", "A common event that returns a value to the parent script. Limited functionality.", ReturnedValueScript);

            Script OverwriteLocalsScript = new Script();
            OverwriteLocalsScript.CanAddItem = false;
            OverwriteLocalsScript.CanAddText = false;
            OverwriteLocalsScript.CanComment = true;
            OverwriteLocalsScript.CanConditional = true;
            OverwriteLocalsScript.CanDisplayText = false;
            OverwriteLocalsScript.CanReturn = false;
            OverwriteLocalsScript.CanSetVariable = true;
            OverwriteLocalsScript.CanStopGame = false;
            OverwriteLocalsScript.HasTextFunctionality = false;
            OverwriteLocalsScript.IsItemScript = false;
            OverwriteLocalsScript.IsInConversation = false;
            OverwriteLocalsScript.CanStartConversations = false;

            AddCommonEventType("Subscript", "A common event that runs as a subscript of its parent. Overwrites its parents' local values, as opposed to duplicating them.", OverwriteLocalsScript);

        }
        private static void AddCommonEventType(string name, string description, Script baseScript)
        {
            CommonEventTypes.Add(Tuple.Create<string, string, Script>(name, description, baseScript));
        }
        public static Script GetCommonEventBase(string name)
        {
            var ev = CommonEventTypes.Where(a => a.Item1 == name).FirstOrDefault();
            if (ev != null)
            {
                return ev.Item3;
            }
            else return null;
        }

        /// <summary>
        /// The <see cref="EventType" /> property's name.
        /// </summary>
        public const string EventTypePropertyName = "EventType";

        private Tuple<string, string, Script> _eventType = Tuple.Create<string, string, Script>("","",null);

        /// <summary>
        /// Sets and gets the EventType property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Tuple<string, string, Script> EventType
        {
            get
            {
                return _eventType;
            }

            set
            {
                if (_eventType == value)
                {
                    return;
                }

                _eventType = value;
                RaisePropertyChanged(EventTypePropertyName);
            }
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
        /// The <see cref="AssociatedScript" /> property's name.
        /// </summary>
        public const string AssociatedScriptPropertyName = "AssociatedScript";

        private Script _associatedScript = null;

        /// <summary>
        /// Sets and gets the AssociatedScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script AssociatedScript
        {
            get
            {
                return _associatedScript;
            }

            set
            {
                if (_associatedScript == value)
                {
                    return;
                }

                _associatedScript = value;
                RaisePropertyChanged(AssociatedScriptPropertyName);
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
                if (MainViewModel.MainViewModelStatic != null)
                {
                    MainViewModel.MainViewModelStatic.CommonEventGroups.RefreshInList(this);
                }
            }
        }

        public XElement ToXML()
        {
            return new XElement("CommonEvent", new XElement("Name", Name), new XElement("Type", EventType.Item1), new XElement("AssociatedScript", AssociatedScript.ToXML()), new XElement("Id", Id), new XElement("Group", Group));
        }

        public static CommonEvent FromXML(XElement xml)
        {
            CommonEvent ce = new CommonEvent();
            ce.Name = xml.Element("Name").Value;
            ce.EventType = Tuple.Create<string, string, Script>(xml.Element("Type").Value, "", GetCommonEventBase(xml.Element("Type").Value));
            ce.AssociatedScript = Script.FromXML(xml.Element("AssociatedScript").Element("Script"), ce.EventType.Item3);
            ce.Id = Guid.Parse(xml.Element("Id").Value);
            if (xml.Element("Group") != null)
            {
                ce.Group = xml.Element("Group").Value;
            }
            return ce;
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
