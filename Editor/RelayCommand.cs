using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public class RelayCommand<T> : ICommand
    {
        public RelayCommand(Action<T> command)
        {
            Command = command;
        }
        public bool CanExecute(object parameter)
        {
            return (parameter is T);
        }

        public event EventHandler CanExecuteChanged;

        private Action<T> Command;


        public void Execute(object parameter)
        {
            Command((T)parameter);
        }
    }
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action command)
        {
            Command = command;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private Action Command;

        public void Execute(object parameter)
        {
            Command();
        }
    }

}
