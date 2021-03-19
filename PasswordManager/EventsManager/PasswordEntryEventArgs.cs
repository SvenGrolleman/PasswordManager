using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.EventsManager
{
    //a single password entry passwordEntry event args should suffice
    //for both creating and editing a passwordEntry
    //based on whether the ID is set or null
    //the right logic for either and insert or update query can be called
    public class PasswordEntryEventArgs : EventArgs
    {
        public PasswordEntryModel PasswordEntryModel { get; set; }
    }

    public class GenereratedPasswordEventArgs : EventArgs
    {
        public string GeneratedPassword { get; set; }
    }
}
