using PasswordManager.Database;
using PasswordManager.EventsManager;
using PasswordManager.HelperClasses;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PasswordManager.Models
{
    public class PasswordListViewViewModel : INotifyPropertyChanged
    {
        private readonly IPwRepository _repository;
        private readonly PasswordManagerEventHandler _eventHandler;
        private readonly ObservableCollection<PasswordListItemViewModel> _passwordsBackup = new ObservableCollection<PasswordListItemViewModel>();

        private ObservableCollection<PasswordListItemViewModel> _passwords;
        public ObservableCollection<PasswordListItemViewModel> Passwords
        {
            get { return _passwords; }
            set
            {
                if (_passwords != value)
                {
                    _passwords = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnFilterChanged();
                }
            }
        }

        public CommandBinder AddPasswordEntry { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public PasswordListViewViewModel(PasswordManagerEventHandler eventHandler, byte[] key, IPwRepository repository)
        {
            Passwords = new ObservableCollection<PasswordListItemViewModel>();
            _repository = repository;
            _eventHandler = eventHandler;
            SetUpCommands();
            SetUpPasswords(key);
        }

        private void SetUpCommands()
        {
            AddPasswordEntry = new CommandBinder(OnAddPasswordEntry);
        }

        private void OnAddPasswordEntry()
        {
            _eventHandler.OnEditPasswordEntryClicked(this, new PasswordEntryModel());
        }

        private void SetUpPasswords(byte[] key)
        {
            foreach (var storedPassword in _repository.GetPasswordEntries())
            {
                _passwordsBackup.Add(new PasswordListItemViewModel(_eventHandler, key, storedPassword));
            }
            //save all the passwordListitem viewmodes for filtering.
            Passwords = _passwordsBackup;
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnFilterChanged()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                Passwords = _passwordsBackup;
            }
            else
            {
                var query = from pwListItemViewModel in _passwordsBackup
                            where pwListItemViewModel.PasswordEntryModel.Username.Contains(Filter) || pwListItemViewModel.PasswordEntryModel.Website.Contains(Filter)
                            select pwListItemViewModel;
                Passwords = new ObservableCollection<PasswordListItemViewModel>(query.ToList());
            }
        }
    }
}
