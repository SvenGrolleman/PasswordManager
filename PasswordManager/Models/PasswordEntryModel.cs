using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class PasswordEntryModel
    {
        public int PasswordEntryId { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
