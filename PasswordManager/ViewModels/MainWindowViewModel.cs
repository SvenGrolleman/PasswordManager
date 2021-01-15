using PasswordManager.EventsManager;
using PasswordManager.HelperClasses;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace PasswordManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        UserControl _passwordListView;
        UserControl _passwordEditView;

        PasswordManagerEventHandler _eventHandler;

        public string VerifyButtonText { get; set; }
        private string _generatedPassword;
        public string GeneratedPassword
        {
            get { return _generatedPassword; }
            set
            {
                if (value != _generatedPassword)
                {
                    _generatedPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Length { get; set; }

        public bool LowerCase { get; set; }
        public bool UpperCase { get; set; }
        public bool Numbers { get; set; }
        public bool Characters { get; set; }

        public CommandBinder GeneratePassword { get; private set; }

        private ContentControl _currentViewModel;
        public ContentControl CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel == null)
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
                else if (!_currentViewModel.Equals(value))
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(PasswordManagerEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
            _passwordListView = new PasswordListView(eventHandler);
            CurrentViewModel = _passwordListView;
            Length = 10;
            SetUpHander();
            SetUpCommands();
        }

        private void SetUpHander()
        {
            _eventHandler.EditPasswordClicked += (s, e) => OnEditPasswordClick(e.PasswordEntryModel);
        }

        private void SetUpCommands()
        {
            GeneratePassword = new CommandBinder(OnGeneratePassword);
        }

        private void OnGeneratePassword()
        {
            List<char> eligibleChars = new List<char>();
            if (LowerCase)
            {
                for (int i = 97; i < 123; i++)
                {
                    eligibleChars.Add(Convert.ToChar(i));
                }
            }
            if (UpperCase)
            {
                for (int i = 65; i < 91; i++)
                {
                    eligibleChars.Add(Convert.ToChar(i));
                }
            }
            if (Characters)
            {
                var charArray = new char[] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };
                foreach (char c in charArray)
                {
                    eligibleChars.Add(c);
                }
            }
            if (Numbers)
            {
                for (int i = 48; i < 58; i++)
                {
                    eligibleChars.Add(Convert.ToChar(i));
                }
            }

            if (eligibleChars.Count == 0)
            {
                GeneratedPassword = $"Please select at least one eligible set of characters";
            } else
            {
                GeneratedPassword = "";
                var rand = new Random();
                var max = eligibleChars.Count;
                int next = 0;
                for (int i = 0; i < Length; i++)
                {
                    next = rand.Next(0, max);
                    GeneratedPassword += eligibleChars[next];
                }
            }
        }

        private void OnEditPasswordClick(PasswordEntryModel passwordEntryModel)
        {
            CurrentViewModel = new PasswordEditView(passwordEntryModel, _eventHandler);
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
