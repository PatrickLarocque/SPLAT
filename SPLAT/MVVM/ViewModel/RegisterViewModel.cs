using Firebase.Auth;
using FireSharp.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using SPLAT.Core;
using SPLAT.Extensions;
using SPLAT.MVVM.View;
using SPLAT.Requests;
using SPLAT.Responses;
using SPLAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirebaseConfig = Firebase.Auth.FirebaseConfig;

namespace SPLAT.MVVM.ViewModel
{
    public class RegisterViewModel : ObservableObject
    {
        static readonly string apiKey = "AIzaSyBtDODfvO9X3TGMXz1sRiNqhDjLL7NOJSE";
        private string _email;
        private string _password;
        private string _errorMessage;
        private string _confirmPassword;
        private bool _isViewVisible = true;


        //properties
        public string Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }

        public string ConfirmPassword { get => _confirmPassword; set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsVisible)); } }

        public ICommand RegisterCommand { get; }
        public ICommand ToLogInViewCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            ToLogInViewCommand = new RelayCommand(ExecuteToLogInViewCommand);

        }

        private bool CanExecuteRegisterCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Email) || Email.Length < 3 || Password == null || Password.Length < 6 || Password != ConfirmPassword)
                validData = false;
            else
                validData = true;
            return validData;

        }

        private void ExecuteToLogInViewCommand(object obj)
        {
            var loginView = new LoginView();
            loginView.Show();
            IsVisible = false;

        }

        private void ExecuteRegisterCommand(object obj)
        {
            _ = RegisterWithFirebase();
            MessageBox.Show("Registration sucessful, please login");
        }

        private void ExecuteRegister(object obj)
        {
            try
            {
                _ = RegisterWithFirebaseAlternative();
               
            }
            catch (ApiException ex)
            {
            }
        }

        public async Task RegisterWithFirebase()
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder()
                    .AddFirebaseAuthenticationSDK(apiKey)
                    .Build();
                IFirebaseRegistrationService registrationService = host.Services.GetRequiredService<IFirebaseRegistrationService>();
                RegistrationResponse registrationResponse = await registrationService.Register(new RegistrationRequest
                {
                    Email = Email,
                    Password = Password,
                    ReturnSecureToken = true
                });
                host.Dispose();
            }
            catch (ApiException ex)
            {
                await ex.GetContentAsAsync<string>();
            }
        }


        public async Task RegisterWithFirebaseAlternative()
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(Email, Password);
                string token = a.FirebaseToken;
                var user = a.User;
                if (token != "")
                {

                }

            }
            catch (Exception ex) { }
        }


    }
}
