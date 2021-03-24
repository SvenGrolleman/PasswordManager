using PasswordManager.Database;
using PasswordManager.EventsManager;
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

namespace PasswordManager.Views
{
    /// <summary>
    /// Interaction logic for MainPasswordChangeView.xaml
    /// </summary>
    public partial class MainPasswordChangeView : UserControl
    {
        public MainPasswordChangeView(PasswordManagerEventHandler eventHandler)
        {
            InitializeComponent();
            this.DataContext = new MainPasswordChangeViewViewModel(eventHandler);
        }
    }
}
