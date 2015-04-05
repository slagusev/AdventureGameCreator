using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{
    public class ConversationRef : INotifyPropertyChanged
    {

        /// <summary>
        /// The <see cref="LinkedConversationId" /> property's name.
        /// </summary>
        public const string LinkedConversationIdPropertyName = "LinkedConversationId";

        private Guid _linkedConversationId = Guid.Empty;

        /// <summary>
        /// Sets and gets the LinkedConversationId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid LinkedConversationId
        {
            get
            {
                return _linkedConversationId;
            }

            set
            {
                if (_linkedConversationId == value)
                {
                    return;
                }

                _linkedConversationId = value;
                RaisePropertyChanged(LinkedConversationIdPropertyName);
                LinkedConversation = null;
            }
        }

        /// <summary>
        /// The <see cref="LinkedConversation" /> property's name.
        /// </summary>
        public const string LinkedConversationPropertyName = "LinkedConversation";

        private Conversation _linkedConvo = null;

        /// <summary>
        /// Sets and gets the LinkedConversation property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Conversation LinkedConversation
        {
            get
            {
                if (_linkedConvo == null && _linkedConversationId != Guid.Empty)
                {
                    //Find the variable in the view model
                    var matches = MainViewModel.MainViewModelStatic.Conversations.Where(a => a.Id == _linkedConversationId);
                    if (matches.Count() > 0)
                    {
                        LinkedConversation = matches.First();
                    }
                }
                return _linkedConvo;
            }

            set
            {
                if (_linkedConvo == value)
                {
                    return;
                }

                _linkedConvo = value;
                if (value != null && LinkedConversationId != value.Id)
                {
                    LinkedConversationId = value.Id;
                }
                RaisePropertyChanged(LinkedConversationPropertyName);
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
