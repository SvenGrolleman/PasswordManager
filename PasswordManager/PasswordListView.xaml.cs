using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    /// Interaction logic for PasswordListView.xaml
    /// </summary>
    public partial class PasswordListView : UserControl
    {
        public PasswordListView()
        {
            InitializeComponent();
            var passwordViewModelEntries = new ObservableCollection<PasswordEntryViewModel>()
            {
                new PasswordEntryViewModel
                {
                    Website = "website one",
                    Username = "username one",
                    Password = "password one",
                },
                new PasswordEntryViewModel
                {
                    Website = "website two",
                    Username = "username two",
                    Password = "password two",
                },
                new PasswordEntryViewModel
                {
                    Website = "website three",
                    Username = "username three",
                    Password = "password three",
                },
                new PasswordEntryViewModel
                {
                    Website = "website four",
                    Username = "username four",
                    Password = "password four",
                },
                new PasswordEntryViewModel
                {
                    Website = "website five",
                    Username = "username five",
                    Password = "password five",
                },
                new PasswordEntryViewModel
                {
                    Website = "website six",
                    Username = "username six",
                    Password = "password six",
                },
            };
            var viewModel = new PasswordEntryListModel()
            {
                Passwords = passwordViewModelEntries
            };
            this.DataContext = viewModel;
        }
    }
}
