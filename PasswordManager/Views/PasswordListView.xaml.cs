using PasswordManager.Database;
using PasswordManager.EventsManager;
using PasswordManager.Models;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows.Controls;

namespace PasswordManager.Views
{
    /// <summary>
    /// Interaction logic for PasswordListView.xaml
    /// </summary>
    public partial class PasswordListView : UserControl
    {
        public PasswordListView(PasswordManagerEventHandler eventHandler, byte[] key ,IPwRepository repository)
        {
            InitializeComponent();
            this.DataContext = new PasswordListViewViewModel(eventHandler, key, repository);
        }
    }
}
