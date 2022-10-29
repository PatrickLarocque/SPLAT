using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SPLAT.Core
{
    internal class RelayCommand : ICommand    ////aka viewmodelCommand
    {
        private Action<object> _execute; // fields
        private Func<object, bool> _canExecute; //predicate

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //constructor without validation predicate
        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = null;
        }


        //constructor with validation predicate
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
