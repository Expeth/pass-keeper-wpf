using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PassKeeper_WPF
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _action;
        readonly Predicate<object> _predicate;

        public RelayCommand(Action<object> a, Predicate<object> p = null)
        {
            _action = a;
            _predicate = p;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null ? true : _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
