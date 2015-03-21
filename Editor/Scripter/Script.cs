using Editor.Scripter.ItemManagement;
using Editor.Scripter.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter
{
    public enum ScriptTypes { Interact, None };
    public class Script : INotifyPropertyChanged
    {

        public Script()
        {
            ScriptType = ScriptTypes.None;
            this.ScriptLines.Add(new Blank());
            
        }

        public Script(Script s)
        {
            ScriptType = s.ScriptType;
            this.ScriptLines.Add(new Blank());


            this.CanComment = s.CanComment;
            this.CanConditional = s.CanConditional;
            this.CanDisplayText = s.CanDisplayText;
            this.CanReturn = s.CanReturn;
            this.CanSetVariable = s.CanSetVariable;
            this.CanStopGame = s.CanStopGame;
            this.CanAddItem = s.CanAddItem;
            this.CanAddText = s.CanAddText;
            this.IsVariableScript = s.IsVariableScript;
            this.HasTextFunctionality = s.HasTextFunctionality;

        }
        public ScriptTypes ScriptType { get; set; }
        public Script(ScriptTypes type)
        {
            ScriptType = ScriptTypes.Interact;
            this.ScriptLines.Add(new Blank());
        }
        public void AddBefore(ScriptLine newLine, ScriptLine destination)
        {
            if (destination == null || ScriptLines.IndexOf(destination) == -1)
            {
                destination = ScriptLines.Where(a => a.GetType() == typeof(Blank)).FirstOrDefault();
            }
            var index = ScriptLines.IndexOf(destination);
            ScriptLines.Insert(index, newLine);
        }
        public void AddBeforeSelected(ScriptLine newLine)
        {
            AddBefore(newLine, SelectedLine);
        }
        /// <summary>
        /// The <see cref="ScriptLines" /> property's name.
        /// </summary>
        public const string ScriptLinesPropertyName = "ScriptLines";

        private ObservableCollection<ScriptLine> _lines = new ObservableCollection<ScriptLine>();

        /// <summary>
        /// Sets and gets the ScriptLines property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ScriptLine> ScriptLines
        {
            get
            {
                return _lines;
            }

            set
            {
                if (_lines == value)
                {
                    return;
                }

                _lines = value;
                RaisePropertyChanged(ScriptLinesPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedLine" /> property's name.
        /// </summary>
        public const string SelectedLinePropertyName = "SelectedLine";

        private ScriptLine _selectedLine = null;

        /// <summary>
        /// Sets and gets the SelectedLine property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ScriptLine SelectedLine
        {
            get
            {
                return _selectedLine;   
            }

            set
            {
                if (_selectedLine == value)
                {
                    return;
                }

                _selectedLine = value;
                RaisePropertyChanged(SelectedLinePropertyName);
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
            return new XElement("Script", from a in ScriptLines select a.ToXML());
        }

        public static Script FromXML(XElement xml, Script baseScript)
        {
            Script script = new Script(baseScript);
            script.ScriptLines.Clear();
            foreach (var element in xml.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "Comment":
                        script.ScriptLines.Add(Comment.FromXML(element));
                        break;
                    case "If":
                        script.ScriptLines.Add(Conditions.Conditional.FromXML(element, script));
                        break;
                    case "DisplayText":
                        script.ScriptLines.Add(TextFunctions.DisplayText.FromXML(element));
                        break;
                    case "AddText":
                        script.ScriptLines.Add(TextFunctions.AddText.FromXML(element));
                        break;
                    case "SetVariable":
                        script.ScriptLines.Add(Flow.SetVariable.FromXML(element));
                        break;
                    case "ReturnTrue":
                        script.ScriptLines.Add(new Flow.ReturnTrue());
                        break;
                    case "ReturnFalse":
                        script.ScriptLines.Add(new Flow.ReturnFalse());
                        break;
                    case "StopGame":
                        script.ScriptLines.Add(new Flow.StopGame());
                        break;
                    case "AddItem":
                        script.ScriptLines.Add(AddItemToInventory.FromXML(element));
                        break;
                    case "GetItemProperty":
                        script.ScriptLines.Add(GetItemProperty.FromXML(element));
                        break;
                    case "SetItemProperty":
                        script.ScriptLines.Add(SetItemProperty.FromXML(element));
                        break;
                    case "GetCurrentItem":
                        script.ScriptLines.Add(GetCurrentItem.FromXML(element));
                        break;
                    case "RemoveItem":
                        script.ScriptLines.Add(RemoveItem.FromXML(element));
                        break;
                    case "RemoveThisItem":
                        script.ScriptLines.Add(new RemoveThisItem());
                        break;
                    default:
                        break;
                }
            }
            script.ScriptLines.Add(new Blank());
            return script;
        }








        #region Supported buttons
        /// <summary>
        /// The <see cref="CanComment" /> property's name.
        /// </summary>
        public const string CanCommentPropertyName = "CanComment";

        private bool _canComment = true;

        /// <summary>
        /// Sets and gets the CanComment property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanComment
        {
            get
            {
                return _canComment;
            }

            set
            {
                if (_canComment == value)
                {
                    return;
                }

                _canComment = value;
                RaisePropertyChanged(CanCommentPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CanAddText" /> property's name.
        /// </summary>
        public const string CanAddTextPropertyName = "CanAddText";

        private bool _canAddText = false;

        /// <summary>
        /// Sets and gets the CanAddText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanAddText
        {
            get
            {
                return _canAddText;
            }

            set
            {
                if (_canAddText == value)
                {
                    return;
                }

                _canAddText = value;
                RaisePropertyChanged(CanAddTextPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CanConditional" /> property's name.
        /// </summary>
        public const string CanConditionalPropertyName = "CanConditional";

        private bool _canConditional = true;

        /// <summary>
        /// Sets and gets the CanConditional property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanConditional
        {
            get
            {
                return _canConditional;
            }

            set
            {
                if (_canConditional == value)
                {
                    return;
                }

                _canConditional = value;
                RaisePropertyChanged(CanConditionalPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="CanSetVariable" /> property's name.
        /// </summary>
        public const string CanSetVariablePropertyName = "CanSetVariable";

        private bool _canSetVariable = true;

        /// <summary>
        /// Sets and gets the CanSetVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanSetVariable
        {
            get
            {
                return _canSetVariable;
            }

            set
            {
                if (_canSetVariable == value)
                {
                    return;
                }

                _canSetVariable = value;
                RaisePropertyChanged(CanSetVariablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanReturn" /> property's name.
        /// </summary>
        public const string CanReturnPropertyName = "CanReturn";

        private bool _canReturn = true;

        /// <summary>
        /// Sets and gets the CanReturn property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanReturn
        {
            get
            {
                return _canReturn;
            }

            set
            {
                if (_canReturn == value)
                {
                    return;
                }

                _canReturn = value;
                RaisePropertyChanged(CanReturnPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanStopGame" /> property's name.
        /// </summary>
        public const string CanStopGamePropertyName = "CanStopGame";

        private bool _canStopGame = true;

        /// <summary>
        /// Sets and gets the CanStopGame property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanStopGame
        {
            get
            {
                return _canStopGame;
            }

            set
            {
                if (_canStopGame == value)
                {
                    return;
                }

                _canStopGame = value;
                RaisePropertyChanged(CanStopGamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsVariableScript" /> property's name.
        /// </summary>
        public const string IsVariableScriptPropertyName = "IsVariableScript";

        private bool _isVariableScript = false;

        /// <summary>
        /// Sets and gets the IsVariableScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsVariableScript
        {
            get
            {
                return _isVariableScript;
            }

            set
            {
                if (_isVariableScript == value)
                {
                    return;
                }

                _isVariableScript = value;
                RaisePropertyChanged(IsVariableScriptPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanDisplayText" /> property's name.
        /// </summary>
        public const string CanDisplayTextPropertyName = "CanDisplayText";

        private bool _canDisplayText = true;

        /// <summary>
        /// Sets and gets the CanDisplayText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanDisplayText
        {
            get
            {
                return _canDisplayText;
            }

            set
            {
                if (_canDisplayText == value)
                {
                    return;
                }

                _canDisplayText = value;
                RaisePropertyChanged(CanDisplayTextPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="HasTextFunctionality" /> property's name.
        /// </summary>
        public const string HasTextFunctionalityPropertyName = "HasTextFunctionality";

        private bool _hasTextFunctionality = true;

        /// <summary>
        /// Sets and gets the HasTextFunctionality property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool HasTextFunctionality
        {
            get
            {
                return _hasTextFunctionality;
            }

            set
            {
                if (_hasTextFunctionality == value)
                {
                    return;
                }

                _hasTextFunctionality = value;
                RaisePropertyChanged(HasTextFunctionalityPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CanAddItem" /> property's name.
        /// </summary>
        public const string CanAddItemPropertyName = "CanAddItem";

        private bool _canAddItem = true;

        /// <summary>
        /// Sets and gets the CanAddItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool CanAddItem
        {
            get
            {
                return _canAddItem;
            }

            set
            {
                if (_canAddItem == value)
                {
                    return;
                }

                _canAddItem = value;
                RaisePropertyChanged(CanAddItemPropertyName);
            }
        }
        #endregion
    }
}
