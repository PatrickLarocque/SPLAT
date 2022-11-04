﻿using SPLAT.Core;
using SPLAT.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SPLAT.MVVM.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;


        //properties
        public string Username { get => _username; set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public SecureString Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsVisible)); } }


        //Commands 
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        //Constructor

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("",""));
        }
        private bool CanExecuteLoginCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;

        }
        private void ExecuteLoginCommand(object obj)
        {
            IsVisible = false;
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

    }
}
