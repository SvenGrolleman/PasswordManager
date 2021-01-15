using PasswordManager.EventsManager;
using PasswordManager.Models;
using PasswordManager.ViewModels;
using System;
using System.Collections.Generic;
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
