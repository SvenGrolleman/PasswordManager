using PasswordManager.CommandBinding;
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

        public PasswordEditViewViewModel(PasswordEntryModel entryModel)
        {
            PasswordEntryModel = entryModel;
        }
    }
}
