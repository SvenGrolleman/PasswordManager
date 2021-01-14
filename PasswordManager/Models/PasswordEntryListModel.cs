using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class PasswordEntryListModel
    {
        public ObservableCollection<PasswordEntryViewModel> Passwords { get; set; }
    }
}
