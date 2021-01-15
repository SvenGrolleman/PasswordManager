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

        public void OnEditPasswordClicked(object sender, PasswordEntryModel passwordEntry)
        {
            (EditPasswordClicked as EventHandler<PasswordEntryEventArgs>)?.Invoke(sender, new PasswordEntryEventArgs { PasswordEntryModel = passwordEntry });
        }
    }
}
