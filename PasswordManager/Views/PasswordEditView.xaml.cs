using PasswordManager.EventsManager;
using PasswordManager.Models;
using PasswordManager.ViewModels;
using System.Windows.Controls;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for PasswordEditView.xaml
    /// </summary>
    public partial class PasswordEditView : UserControl
    {
        public PasswordEditView(PasswordEntryModel passwordEntryModel, PasswordManagerEventHandler eventHandler)
        {
            InitializeComponent();
            this.DataContext = new PasswordEditViewViewModel(passwordEntryModel, eventHandler);
        }
    }
}
