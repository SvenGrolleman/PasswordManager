using PasswordManager.Database;
using PasswordManager.EventsManager;
using PasswordManager.Models;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows.Controls;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for PasswordListView.xaml
    /// </summary>
    public partial class PasswordListView : UserControl
    {
        public PasswordListView(PasswordManagerEventHandler eventHandler, byte[] key ,IPwRepository repository)
        {
            InitializeComponent();
            var viewModel = new PasswordListViewViewModel(eventHandler,key, repository);

            this.DataContext = viewModel;
        }
    }
}
