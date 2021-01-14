using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.CommandBinding
{
    public class CommandBinder : ICommand
    {
        Action _targetMethod;
        Func<bool> _targetCanExecute;

        public CommandBinder(Action targetMethod)
        {
            _targetMethod = targetMethod;
        }

        public CommandBinder(Action targetMethod, Func<bool> targetCanExecute)
        {
            _targetMethod = targetMethod;
            _targetCanExecute = targetCanExecute;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            if (_targetCanExecute != null)
            {
                return _targetCanExecute();
            }
            if (_targetMethod != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            if (_targetMethod != null)
            {
                _targetMethod();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    public class CommandBinder<T> : ICommand
    {
        Action<T> _targetMethod;
        Func<T, bool> _targetCanExecute;

        public CommandBinder(Action<T> targetMethod)
        {
            _targetMethod = targetMethod;
        }

        public CommandBinder(Action<T> targetMethod, Func<T, bool> targetCanExecute)
        {
            _targetMethod = targetMethod;
            _targetCanExecute = targetCanExecute;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            if (_targetCanExecute != null)
            {
                T tpram = (T)parameter;
                return _targetCanExecute(tpram);
            }
            if (_targetMethod != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            if (_targetMethod != null)
            {
                T tpram = (T)parameter;
                _targetMethod(tpram);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
