using PasswordManager.Database;
using PasswordManager.EventsManager;
using System.Configuration;
using System.Windows;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            var repository = new PwRepository(ConfigurationManager.ConnectionStrings["PasswordManager"].ConnectionString);
            var eventHandler = new PasswordManagerEventHandler();
            var viewModel = new MainWindowViewModel(eventHandler, repository);
            this.DataContext = viewModel;
        }
    }
}
