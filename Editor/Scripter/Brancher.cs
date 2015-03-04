using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Scripter
{
    public abstract class Brancher : ScriptLine
    {
        /// <summary>
        /// The <see cref="Contents" /> property's name.
        /// </summary>
        public const string ContentsPropertyName = "Contents";

        private ObservableCollection<Script> _contents = new ObservableCollection<Script>();

        /// <summary>
        /// Sets and gets the Contents property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Script> Contents
        {
            get
            {
                return _contents;
            }

            set
            {
                if (_contents == value)
                {
                    return;
                }

                _contents = value;
                RaisePropertyChanged(ContentsPropertyName);
            }
        }
        
    }
}
