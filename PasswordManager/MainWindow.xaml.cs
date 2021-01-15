using PasswordManager.Database.Entities;
using PasswordManager.EventsManager;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var eventHandler = new PasswordManagerEventHandler();
            eventHandler.EditPasswordClicked += (s, e) => CreateNewEditWindow(e.PasswordEntryModel);
            MainGrid.Children.Add(new PasswordListView(eventHandler));
        }

        private void CreateNewEditWindow(PasswordEntryModel passwordEntryModel)
        {
            
        }
    }
}
