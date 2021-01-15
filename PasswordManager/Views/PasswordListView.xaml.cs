using PasswordManager.EventsManager;
using PasswordManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for PasswordListView.xaml
    /// </summary>
    public partial class PasswordListView : UserControl
    {
        public PasswordListView(PasswordManagerEventHandler eventHandler)
        {
            InitializeComponent();
            var passwordViewModelEntries = new ObservableCollection<PasswordListItemViewModel>()
            {
                new PasswordListItemViewModel(eventHandler)
                {
                    PasswordEntryModel = new PasswordEntryModel
                    {
                        Website = "website one",
                        Username = "username one",
                        Password = "password one",
                    }
                },
                new PasswordListItemViewModel(eventHandler)
                {
                    PasswordEntryModel = new PasswordEntryModel
                    {
                        Website = "website two",
                        Username = "username two",
                        Password = "password two",
                    }
                },
                new PasswordListItemViewModel(eventHandler)
                {
                    PasswordEntryModel = new PasswordEntryModel
                    {
                        Website = "website three",
                        Username = "username three",
                        Password = "password three",
                    }
                },
                new PasswordListItemViewModel(eventHandler)
                {
                    PasswordEntryModel = new PasswordEntryModel
                    {
                        Website = "website four",
                        Username = "username four",
                        Password = "password five",
                    }
                },
                new PasswordListItemViewModel(eventHandler)
                {
                    PasswordEntryModel = new PasswordEntryModel
                    {
                        Website = "website five",
                        Username = "username five",
                        Password = "password five",
                    }
                },
                new PasswordListItemViewModel(eventHandler)
                {
                    PasswordEntryModel = new PasswordEntryModel
                    {
                        Website = "website six",
                        Username = "username six",
                        Password = "password six",
                    }
                },
            };
            var viewModel = new PasswordListViewViewModel()
            {
                Passwords = passwordViewModelEntries
            };
            this.DataContext = viewModel;
        }
    }
}
