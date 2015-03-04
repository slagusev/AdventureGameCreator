using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Editor
{
    public class WindowView : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="TabName" /> property's name.
        /// </summary>
        public const string TabNamePropertyName = "TabName";

        private string _tabName = "TAB NAME HERE";

        /// <summary>
        /// Sets and gets the TabName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string TabName
        {
            get
            {
                return _tabName;
            }

            set
            {
                if (_tabName == value)
                {
                    return;
                }

                _tabName = value;
                RaisePropertyChanged(TabNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Content" /> property's name.
        /// </summary>
        public const string ContentPropertyName = "Content";

        private UserControl _content = null;

        /// <summary>
        /// Sets and gets the Content property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public UserControl Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (_content == value)
                {
                    return;
                }

                _content = value;
                RaisePropertyChanged(ContentPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CloseVisibility" /> property's name.
        /// </summary>
        public const string CloseVisibilityPropertyName = "CloseVisibility";

        private Visibility _closeVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the CloseVisibility property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Visibility CloseVisibility
        {
            get
            {
                return _closeVisibility;
            }

            set
            {
                if (_closeVisibility == value)
                {
                    return;
                }

                _closeVisibility = value;
                RaisePropertyChanged(CloseVisibilityPropertyName);
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
