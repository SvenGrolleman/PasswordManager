using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class PasswordListViewViewModel
    {
        public ObservableCollection<PasswordListItemViewModel> Passwords { get; set; }
    }
}
