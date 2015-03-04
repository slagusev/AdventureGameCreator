using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Misc
{
    public class Comment : ScriptLine
    {
        /// <summary>
        /// The <see cref="Text" /> property's name.
        /// </summary>
        public const string TextPropertyName = "Text";

        private string _comment = "Comment Text";

        /// <summary>
        /// Sets and gets the Text property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string CommentText
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
            get { return "COMMENT: " + _comment; }
        }

        public override XElement ToXML()
        {
            return new XElement("Comment", _comment);
        }

        public static ScriptLine FromXML(XElement xml)
        {
            return new Comment { CommentText = xml.Value };
        }
    }
}
