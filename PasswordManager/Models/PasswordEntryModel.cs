using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class PasswordEntryModel : INotifyPropertyChanged
    {
        public int PasswordEntryId { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if( _password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
