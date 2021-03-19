using PasswordManager.Database;
using PasswordManager.Encryption;
using PasswordManager.EventsManager;
using PasswordManager.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class PasswordListViewViewModel
    {
        private readonly IPwRepository _repository;
        private readonly PasswordManagerEventHandler _eventHandler;

        public ObservableCollection<PasswordListItemViewModel> Passwords { get; private set; }

        public CommandBinder AddPasswordEntry { get; private set; }

        public PasswordListViewViewModel(PasswordManagerEventHandler eventHandler, byte[] key,IPwRepository repository)
        {
            Passwords = new ObservableCollection<PasswordListItemViewModel>();
            _repository = repository;
            _eventHandler = eventHandler;
            SetUpCommands();
            SetUpPasswords(key);
        }

        private void SetUpCommands()
        {
            AddPasswordEntry = new CommandBinder(OnAddPasswordEntry);
        }

        private void OnAddPasswordEntry()
        {
            _eventHandler.OnEditPasswordEntryClicked(this, new PasswordEntryModel());
        }

        private void SetUpPasswords(byte[] key)
        {
            foreach (var storedPassword in _repository.GetPasswordEntries())
            {
                Passwords.Add(new PasswordListItemViewModel(_eventHandler, key, storedPassword));
            }
        }
    }
}
