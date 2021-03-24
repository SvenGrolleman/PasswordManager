using PasswordManager.HelperClasses;
using PasswordManager.Database.Entities;
using PasswordManager.Encryption;
using PasswordManager.EventsManager;
using PasswordManager.Extensions;
using System;
using System.Windows;

namespace PasswordManager.Models
{
    public class PasswordListItemViewModel
    {
        public PasswordEntryModel PasswordEntryModel { get; set; }

        private PasswordManagerEventHandler _eventHandler;

        public CommandBinder CopyUsername { get; private set; }
        public CommandBinder CopyPassword { get; private set; }
        public CommandBinder EditPasswordEntry { get; private set; }
        public CommandBinder DeletePasswordEntry { get; private set; }

        public PasswordListItemViewModel(PasswordManagerEventHandler eventHandler, byte[] Key, PasswordEntryModel passwordEntryModel)
        {
            _eventHandler = eventHandler;
            PasswordEntryModel = passwordEntryModel;
        }

        private void SetUpCommands()
        {
            CopyUsername = new CommandBinder(OnCopyUsername);
            CopyPassword = new CommandBinder(OnCopyPassword);
            EditPasswordEntry = new CommandBinder(OnEditPasswordEntry);
            DeletePasswordEntry = new CommandBinder(OnDeletePasswordEntry);
        }

        public void OnCopyUsername()
        {
            Clipboard.SetText(PasswordEntryModel.Username);
        }

        public void OnCopyPassword()
        {
            Clipboard.SetText(PasswordEntryModel.Password);
        }

        public void OnEditPasswordEntry()
        {
            _eventHandler.OnEditPasswordEntryClicked(this, PasswordEntryModel);
        }

        public void OnDeletePasswordEntry()
        {
            _eventHandler.OnDeletePasswordEntryClicked(this, PasswordEntryModel);
        }
    }
}
