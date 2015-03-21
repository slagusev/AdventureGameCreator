using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Reference to a variable that may not be defined yet. Will attempt to resolve on usage.
namespace Editor.ObjectTypes
{
    public class VarRef : INotifyPropertyChanged
    {

        public VarRef(Guid id)
        {
            LinkedVarId = id;
        }

        public VarRef()
        {
        }

        /// <summary>
        /// The <see cref="LinkedVarId" /> property's name.
        /// </summary>
        public const string LinkedVarIdPropertyName = "LinkedVarId";

        private Guid _linkedVarId = Guid.Empty;

        /// <summary>
        /// Sets and gets the LinkedVarId property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid LinkedVarId
        {
            get
            {
                return _linkedVarId;
            }

            set
            {
                if (_linkedVarId == value)
                {
                    return;
                }

                _linkedVarId = value;
                LinkedVariable = null;
                RaisePropertyChanged(LinkedVarIdPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="LinkedVariable" /> property's name.
        /// </summary>
        public const string LinkedVariablePropertyName = "LinkedVariable";

        private Variable _linkedVariable = null;

        /// <summary>
        /// Sets and gets the LinkedVariable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Variable LinkedVariable

        {
            get
            {
                if (_linkedVariable == null && _linkedVarId != Guid.Empty)
                {
                    //Find the variable in the view model
                    var matches = MainViewModel.MainViewModelStatic.Variables.Where(a => a.Id == _linkedVarId);
                    if (matches.Count() > 0)
                    {
                        LinkedVariable = matches.First();
                    }
                }
                return _linkedVariable;
            }

            set
            {
                if (_linkedVariable == value)
                {
                    return;
                }

                _linkedVariable = value;
                RaisePropertyChanged(LinkedVariablePropertyName);
                if (value != null)
                    this.LinkedVarId = value.Id;
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
