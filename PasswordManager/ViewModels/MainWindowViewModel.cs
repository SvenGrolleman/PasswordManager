using PasswordManager.Database;
using PasswordManager.Database.Entities;
using PasswordManager.Encryption;
using PasswordManager.EventsManager;
using PasswordManager.Extensions;
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
        PasswordManagerEventHandler _eventHandler;

        private readonly IPwRepository _repository;
        private byte[] _key;
        public string VerifyButtonText { get; private set; }
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
        private bool _enableButton;

        public bool EnableButton
        {
            get { return _enableButton; }
            set
            {
                if (_enableButton != value)
                {
                    _enableButton = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _mainPassword;
        public string MainPassword
        {
            get { return _mainPassword; }
            set
            {
                if (_mainPassword != value)
                {
                    _mainPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public CommandBinder GeneratePassword { get; private set; }
        public CommandBinder VerifyPassword { get; private set; }

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

        public MainWindowViewModel(PasswordManagerEventHandler eventHandler, IPwRepository repository)
        {
            _repository = repository;
            _eventHandler = eventHandler;
            Length = 10;
            EnableButton = true;
            SetUpHander();
            SetUpCommands();
            SetUpVerifyButtonText();
            SetUpEventListeners();
        }

        private void SetUpEventListeners()
        {
            _eventHandler.Cancel += (s,e) => EditPasswordEntryCanceled();
            _eventHandler.CommitPassword += (s, e) => EditPasswordEntryCommited(e.PasswordEntryModel);
        }

        private void EditPasswordEntryCommited(PasswordEntryModel passwordEntryModel)
        {
            if (passwordEntryModel.PasswordEntryId == 0)
            {
                var IV = AESEncryptor.GetIV();
                var encryptedPassword = AESEncryptor.EncryptStringToBytes(passwordEntryModel.Password.ToByteArrayFromString(), _key, IV);
                var passwordEntry = new PasswordEntry()
                {
                    Username = passwordEntryModel.Username,
                    Website = passwordEntryModel.Website,
                    IV = IV.ToBase64FromByteArray(),
                    Password = encryptedPassword.ToBase64FromByteArray(),
                };
                _repository.InsertPasswordEntry(passwordEntry);
                CurrentViewModel = CurrentViewModel = new PasswordListView(_eventHandler, _key, _repository);
            }
            else
            {
                var IV = AESEncryptor.GetIV();
                var encryptedPassword = AESEncryptor.EncryptStringToBytes(passwordEntryModel.Password.ToByteArrayFromString(), _key, IV);
                var passwordEntry = new PasswordEntry()
                {
                    PasswordEntryId = passwordEntryModel.PasswordEntryId,
                    Username = passwordEntryModel.Username,
                    Website = passwordEntryModel.Website,
                    IV = IV.ToBase64FromByteArray(),
                    Password = encryptedPassword.ToBase64FromByteArray(),
                };
                _repository.UpdatePasswordEntry(passwordEntry);
                CurrentViewModel = CurrentViewModel = new PasswordListView(_eventHandler, _key, _repository);
            }
        }

        private void EditPasswordEntryCanceled()
        {
            CurrentViewModel = CurrentViewModel = new PasswordListView(_eventHandler, _key, _repository);
        }

        private void SetUpVerifyButtonText()
        {
            var mainPassword = _repository.GetMainPassword();
            if (mainPassword.MainId == 0)
            {
                VerifyButtonText = "Set mainpassword";
            }
            else
            {
                VerifyButtonText = "Log in";
            }
        }

        private void SetUpHander()
        {
            _eventHandler.EditPasswordClicked += (s, e) => OnEditPasswordClick(e.PasswordEntryModel);
        }

        private void SetUpCommands()
        {
            GeneratePassword = new CommandBinder(OnGeneratePassword);
            VerifyPassword = new CommandBinder(OnVerifyPassword);
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
            }
            else
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
            _eventHandler.OnPasswordGenerated(this, GeneratedPassword);
        }

        private void OnEditPasswordClick(PasswordEntryModel passwordEntryModel)
        {            
            CurrentViewModel = new PasswordEditView(passwordEntryModel, _eventHandler);
        }

        private void OnVerifyPassword()
        {
            var storedMainPassword = _repository.GetMainPassword();
            if (storedMainPassword.MainId == 0)
            {
                if (!string.IsNullOrEmpty(_mainPassword))
                {
                    var saltByteArray = RfcEncryptor.GenerateSalt();
                    var encryptedByteArray = RfcEncryptor.HashWithSalt(_mainPassword.ToByteArrayFromString(), saltByteArray);
                    var mainPassword = new MainPassword()
                    {
                        Password = encryptedByteArray.ToBase64FromByteArray(),
                        MainSalt = saltByteArray.ToBase64FromByteArray(),
                    };
                    var succes = _repository.InsertMainPassword(mainPassword);
                    if (succes > 0)
                    {
                        EnableButton = false;
                        _key = RfcEncryptor.HashWithSalt(_mainPassword.ToByteArrayFromString(), saltByteArray, 1000);
                        CurrentViewModel = new PasswordListView(_eventHandler, _key, _repository);
                        MainPassword = "Main password succesfully set!";
                    }
                }
                else
                {
                    MainPassword = "Please no empty string.";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_mainPassword))
                {
                    var saltByteArray = storedMainPassword.MainSalt;
                    var encryptedByteArray = RfcEncryptor.HashWithSalt(_mainPassword.ToByteArrayFromString(), saltByteArray.ToByteArrayFromBase64());
                    if (ByteArrayComparer.CompareByteArrays(storedMainPassword.Password.ToByteArrayFromBase64(), encryptedByteArray))
                    {
                        EnableButton = false;
                        _key = RfcEncryptor.HashWithSalt(_mainPassword.ToByteArrayFromString(), saltByteArray.ToByteArrayFromBase64(), 1000);
                        CurrentViewModel = new PasswordListView(_eventHandler, _key, _repository);
                        MainPassword = "Succesfully verified!";
                    }
                    else
                    {
                        MainPassword = "Failed to verify.";
                    }
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
