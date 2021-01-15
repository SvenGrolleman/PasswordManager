using PasswordManager.EventsManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PasswordManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        UserControl _PasswordListView;
        UserControl _PasswordEditView;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isVerified;

        public bool IsVerified
        {
            get { return IsVerified; }
            set { IsVerified = value; }
        }


        public MainWindowViewModel(PasswordManagerEventHandler eventHandler)
        {
            _PasswordEditView = new PasswordListView(eventHandler);
        }
    }
}
