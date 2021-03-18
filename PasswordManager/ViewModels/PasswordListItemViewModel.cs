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
        public CommandBinder EditPassword { get; private set; }

        public PasswordListItemViewModel(PasswordManagerEventHandler eventHandler, byte[] Key, PasswordEntry passwordEntry)
        {
            _eventHandler = eventHandler;
            PasswordEntryModel = new PasswordEntryModel
            {
                PasswordEntryId = passwordEntry.PasswordEntryId,
                Website = passwordEntry.Website,
                Username = passwordEntry.Username,
                Password = AESEncryptor.DecryptBytesToString(passwordEntry.Password.ToByteArrayFromBase64(), Key, passwordEntry.IV.ToByteArrayFromBase64()).ToStringFromByteArray(),
            };
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            CopyUsername = new CommandBinder(OnCopyUsername);
            CopyPassword = new CommandBinder(OnCopyPassword);
            EditPassword = new CommandBinder(OnEditPassword);
        }

        public void OnCopyUsername()
        {
            Clipboard.SetText(PasswordEntryModel.Username);
        }

        public void OnCopyPassword()
        {
            Clipboard.SetText(PasswordEntryModel.Password);
        }

        public void OnEditPassword()
        {
            _eventHandler.OnEditPasswordClicked(this, PasswordEntryModel);
        }
    }
}
