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
                Username = AESEncryptor.DecryptBytesToString(passwordEntry.Username.ToByteArrayFromString(), Key, passwordEntry.IV.ToByteArrayFromBase64()).ToStringFromByteArray(),
                Password = AESEncryptor.DecryptBytesToString(passwordEntry.Password.ToByteArrayFromString(), Key, passwordEntry.IV.ToByteArrayFromBase64()).ToStringFromByteArray(),
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
