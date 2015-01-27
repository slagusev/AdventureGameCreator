using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.TextFunctions
{
    class DisplayText : ScriptLine
    {
        /// <summary>
        /// The <see cref="Text" /> property's name.
        /// </summary>
        public const string TextPropertyName = "Text";

        private string _comment = "";

        /// <summary>
        /// Sets and gets the Text property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Text
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                _comment = value;
                RaisePropertyChanged(TextPropertyName);
                RaisePropertyChanged("Plaintext");
            }
        }
        public override string Plaintext
        {
            get { return "Show the following text: " + _comment; }
        }

        public override XElement ToXML()
        {
            return new XElement("DisplayText", _comment);
        }

        public static ScriptLine FromXML(XElement xml)
        {
            return new DisplayText { Text = xml.Value };
        }
    }
}
