using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{
    public class ItemRef : INotifyPropertyChanged
    {

        public ItemRef(Guid id)
        {
            LinkedItemId = id;
        }
        public ItemRef()
        {
        }

        /// <summary>
        /// The <see cref="LinkedItemId" /> property's name.
        /// </summary>
        public const string LinkedItemIdPropertyName = "LinkedItemId";

        private Guid _linkedItemId = Guid.Empty;

        /// <summary>
        /// Sets and gets the LinkedItemId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid LinkedItemId
        {
            get
            {
                return _linkedItemId;
            }

            set
            {
                if (_linkedItemId == value)
                {
                    return;
                }

                _linkedItemId = value;
                RaisePropertyChanged(LinkedItemIdPropertyName);
                LinkedItem = null;
            }
        }

        /// <summary>
        /// The <see cref="LinkedItem" /> property's name.
        /// </summary>
        public const string LinkedItemPropertyName = "LinkedItem";

        private Item _linkedItem = null;

        /// <summary>
        /// Sets and gets the LinkedItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Item LinkedItem
        {
            get
            {
                if (_linkedItem == null && _linkedItemId != Guid.Empty)
                {
                    //Find the variable in the view model
                    var matches = MainViewModel.MainViewModelStatic.Items.Where(a => a.ItemID == _linkedItemId);
                    if (matches.Count() > 0)
                    {
                        LinkedItem = matches.First();
                    }
                }
                return _linkedItem;
            }

            set
            {
                if (_linkedItem == value)
                {
                    return;
                }

                _linkedItem = value;
                RaisePropertyChanged(LinkedItemPropertyName);
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
