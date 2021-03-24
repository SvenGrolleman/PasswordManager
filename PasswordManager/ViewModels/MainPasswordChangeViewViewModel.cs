using PasswordManager.Database;
using PasswordManager.EventsManager;
using PasswordManager.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModels
{
    public class MainPasswordChangeViewViewModel
    {
        private readonly PasswordManagerEventHandler _eventHandler;

        public CommandBinder ChangeMainPassword { get; private set; }
        public CommandBinder Cancel { get; private set; }
        public string NewMainPassword { get; set; }

        public MainPasswordChangeViewViewModel(PasswordManagerEventHandler eventHandler)
        {
            SetupCommands();
            _eventHandler = eventHandler;
        }

        private void SetupCommands()
        {
            Cancel = new CommandBinder(OnCancel);
            ChangeMainPassword = new CommandBinder(OnNewMainPassword);
        }

        private void OnCancel()
        {
            _eventHandler.OnCancel(this);
        }

        private void OnNewMainPassword()
        {
            _eventHandler.OnMainPasswordChanged(this, NewMainPassword);
        }
    }
}
