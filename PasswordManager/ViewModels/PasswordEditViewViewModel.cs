using PasswordManager.HelperClasses;
using PasswordManager.EventsManager;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModels
{
    public class PasswordEditViewViewModel
    {
        public PasswordEntryModel PasswordEntryModel { get; set; }

        public CommandBinder Commit;
        private readonly PasswordManagerEventHandler _eventHandler;

        public PasswordEditViewViewModel(PasswordEntryModel passwordEntryModel, PasswordManagerEventHandler eventHandler)
        {
            PasswordEntryModel = passwordEntryModel;
            _eventHandler = eventHandler;
        }
    }
}
