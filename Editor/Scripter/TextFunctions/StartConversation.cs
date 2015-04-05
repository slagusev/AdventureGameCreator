﻿using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.TextFunctions
{
    public class StartConversation : ScriptLine
    {

        /// <summary>
        /// The <see cref="ConversationID" /> property's name.
        /// </summary>
        public const string ConversationIDPropertyName = "ConversationID";

        private ConversationRef _conversationId = new ConversationRef();

        /// <summary>
        /// Sets and gets the ConversationID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ConversationRef ConversationID
        {
            get
            {
                return _conversationId;
            }

            set
            {
                if (_conversationId == value)
                {
                    return;
                }

                _conversationId = value;
                RaisePropertyChanged(ConversationIDPropertyName);
            }
        }
        public override string Plaintext
        {
            get
            {
                return "Start the conversation: " + (ConversationID != null && ConversationID.LinkedConversation != null ? ConversationID.LinkedConversation.Name : "UNKNOWN");
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("StartConversation", ConversationID.LinkedConversationId);
        }

        public static StartConversation FromXML(XElement element)
        {
            return new StartConversation() { ConversationID = new ConversationRef() { LinkedConversationId = Guid.Parse(element.Value) } };
        }
    }
}
