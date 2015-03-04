using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter
{
    public abstract class ScriptLine : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Sets and gets the Plaintext property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public virtual string Plaintext
        {
            get { return ""; }
        }
        public abstract XElement ToXML();
    }
}
