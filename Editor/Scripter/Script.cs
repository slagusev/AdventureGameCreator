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
    internal class Script : INotifyPropertyChanged
    {

        public Script()
        {
            ScriptType = ScriptTypes.None;
            this.ScriptLines.Add(new Blank());
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

        public static Script FromXML(XElement xml)
        {
            Script script = new Script();
            script.ScriptLines.Clear();
            foreach (var element in xml.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "Comment":
                        script.ScriptLines.Add(Comment.FromXML(element));
                        break;
                    case "If":
                        script.ScriptLines.Add(Conditions.Conditional.FromXML(element));
                        break;
                    case "DisplayText":
                        script.ScriptLines.Add(TextFunctions.DisplayText.FromXML(element));
                        break;
                    default:
                        break;
                }
            }
            script.ScriptLines.Add(new Blank());
            return script;
        }
    }
}
