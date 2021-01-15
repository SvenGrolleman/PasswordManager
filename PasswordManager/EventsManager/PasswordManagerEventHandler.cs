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
        public event EventHandler<PasswordEntryEventArgs> EditPasswordClicked;
        public event EventHandler<PasswordEntryEventArgs> CommitPassword;

        public void OnEditPasswordClicked(object sender, PasswordEntryModel passwordEntry)
        {
            EditPasswordClicked?.Invoke(sender, new PasswordEntryEventArgs { PasswordEntryModel = passwordEntry });
        }

        public void OnCommitPasswrd(object sender, PasswordEntryModel passwordEntry)
        {
            CommitPassword?.Invoke(sender, new PasswordEntryEventArgs { PasswordEntryModel = passwordEntry });
        }
    }
}
