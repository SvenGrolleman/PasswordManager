using PasswordManager.HelperClasses;
using PasswordManager.EventsManager;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordManager.ViewModels
{
    public class PasswordEditViewViewModel : INotifyPropertyChanged
    {
        public PasswordEntryModel PasswordEntryModel { get; set; }

        private string _confirmButtonText;
        public string ConfirmButtonText
        {
            get { return _confirmButtonText; }
            set
            {
                if (_confirmButtonText != value)
                {
                    _confirmButtonText = value;
                    OnPropertyChanged();
                }
            }
        }


        public CommandBinder CommitPasswordEntry { get; private set; }
        public CommandBinder CancelPasswordEntry { get; private set;}
        private readonly PasswordManagerEventHandler _eventHandler;

        public event PropertyChangedEventHandler PropertyChanged;

        public PasswordEditViewViewModel(PasswordEntryModel passwordEntryModel, PasswordManagerEventHandler eventHandler)
        {
            PasswordEntryModel = passwordEntryModel;
            _eventHandler = eventHandler;
            if (PasswordEntryModel.PasswordEntryId == 0)
            {
                ConfirmButtonText = "Add password";
            }
            else
            {
                ConfirmButtonText = "Save Changes";
            }
            SetupCommands();

        }

        private void SetupCommands()
        {
            CommitPasswordEntry = new CommandBinder(OnCommit);
            CancelPasswordEntry = new CommandBinder(OnCancel);
        }

        private void OnCommit()
        {
            _eventHandler.OnCommitPassword(this, PasswordEntryModel);
        }

        private void OnCancel()
        {
            _eventHandler.OnCancel(this);
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
