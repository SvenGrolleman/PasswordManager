using PasswordManager.CommandBinding;
using PasswordManager.Database.Entities;
using PasswordManager.Encryption;
using PasswordManager.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager.Models
{
    public class PasswordEntryViewModel
    {
        public int PasswordEntryId { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public CommandBinder CopyUsername { get; private set; }
        public CommandBinder CopyPassword { get; private set; }

        public PasswordEntryViewModel()
        {
            SetUpCommands();
        }

        public PasswordEntryViewModel(byte[] Key, PasswordEntry passwordEntry)
        {
            PasswordEntryId = passwordEntry.PasswordEntryId;
            Website = passwordEntry.Website;
            Username = AESEncryptor.DecryptBytesToString(passwordEntry.Username.ToByteArrayFromString(), Key, passwordEntry.IV.ToByteArrayFromBase64()).ToStringFromByteArray();
            Password = AESEncryptor.DecryptBytesToString(passwordEntry.Password.ToByteArrayFromString(), Key, passwordEntry.IV.ToByteArrayFromBase64()).ToStringFromByteArray();
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            CopyUsername = new CommandBinder(OnCopyUsername);
            CopyPassword = new CommandBinder(OnCopyPassword);
        }

        public void OnCopyUsername()
        {
            Clipboard.SetText(Username);
        }

        public void OnCopyPassword()
        {
            Clipboard.SetText(Password);
        }
    }
}
