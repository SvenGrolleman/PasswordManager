using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.EventsManager
{
    public class PasswordManagerEventHandler
    {
        public event EventHandler<PasswordEntryEventArgs> EditPasswordEntryClicked;
        public event EventHandler<PasswordEntryEventArgs> CommitPassword;
        public event EventHandler Cancel;
        public event EventHandler<GenereratedPasswordEventArgs> PasswordGenerated;
        public event EventHandler<PasswordEntryEventArgs> DeletePasswordEntry;
        public event EventHandler<NewMainPasswordEventArgs> MainPasswordChanged;

        public void OnEditPasswordEntryClicked(object sender, PasswordEntryModel passwordEntry)
        {
            EditPasswordEntryClicked?.Invoke(sender, new PasswordEntryEventArgs { PasswordEntryModel = passwordEntry });
        }

        public void OnCommitPassword(object sender, PasswordEntryModel passwordEntry)
        {
            CommitPassword?.Invoke(sender, new PasswordEntryEventArgs { PasswordEntryModel = passwordEntry });
        }

        public void OnPasswordGenerated(object sender, string generatedPassword)
        {
            PasswordGenerated?.Invoke(sender, new GenereratedPasswordEventArgs { GeneratedPassword = generatedPassword });
        }

        public void OnCancel(object sender)
        {
            Cancel?.Invoke(sender, null);
        }

        public void OnDeletePasswordEntryClicked(object sender, PasswordEntryModel passwordEntryModel)
        {
            DeletePasswordEntry?.Invoke(sender, new PasswordEntryEventArgs { PasswordEntryModel = passwordEntryModel });
        }

        public void OnMainPasswordChanged(object sender, string newMainPassword)
        {
            MainPasswordChanged?.Invoke(sender, new NewMainPasswordEventArgs { NewMainPassword = newMainPassword });
        }
    }
}
