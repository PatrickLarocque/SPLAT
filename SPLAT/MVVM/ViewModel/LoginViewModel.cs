using Firebase.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util;
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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using LoginView = SPLAT.MVVM.View.LoginView;
using MessageBox = System.Windows.MessageBox;

namespace SPLAT.MVVM.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        static readonly string apiKey = "AIzaSyBtDODfvO9X3TGMXz1sRiNqhDjLL7NOJSE";
        private string _email;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;


        //properties
        public string Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsVisible)); } }


        //Commands 
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ToRegisterViewCommand { get; }

        public ICommand LoginWithGoogleCommand { get; }

        //Constructor

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("", ""));
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            RestEmailCommand = new RelayCommand(ExecuteResetEmail, CanExecuteResetCommand);
            ToRegisterViewCommand = new RelayCommand(ExecuteToRegisterViewCommand);
            LoginWithGoogleCommand = new RelayCommand(ExecuteLoginWithGoogleCommand);
        }

        private void ExecuteLoginWithGoogleCommand(object obj)
        {
            try
            {
                LoginWithGoogle();

            }
            catch (ApiException ex)
            {
            }


        }

        private void ExecuteToRegisterViewCommand(object obj)
        {
            IsVisible = false;
            var registerView = new RegisterView();
            registerView.Show();
        }

        private bool CanExecuteResetCommand(object arg)
        {
            {
                bool validData;
                if (string.IsNullOrWhiteSpace(Email) || Email.Length < 3)
                {
                    validData = false;
                }

                else
                    validData = true;
                return validData;

            }
        }

        private void ExecuteResetEmail(object obj)
        {
            try
            {
                _ = ResetEmail();

            }
            catch (ApiException ex)
            {
            }
        }

        private bool CanExecuteRegisterCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Email) || Email.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;

        }
        private void ExecuteRegisterCommand(object obj)
        {
            try
            {
                _ = RegisterWithFirebaseAlternative();

            }
            catch (ApiException ex)
            {
            }


        }


        private bool CanExecuteLoginCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Email) || Email.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;

        }
        private void ExecuteLoginCommand(object obj)
        {
            try
            {
                _ = LoginWithFirebaseAlternative();

            }
            catch (ApiException ex)
            {
            }


        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
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

        public async Task LoginWithFirebase()
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder()
                    .AddFirebaseAuthenticationSDK(apiKey)
                    .Build();
                IFirebaseLoginService loginService = host.Services.GetRequiredService<IFirebaseLoginService>();
                LoginResponse loginResponse = await loginService.Login(new LoginRequest
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

        public async Task RefreshFirebaseToken()
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder()
                    .AddFirebaseAuthenticationSDK(apiKey)
                    .Build();
                IFirebaseRefreshService refreshService = host.Services.GetRequiredService<IFirebaseRefreshService>();
                RefreshResponse refreshResponse = await refreshService.Refresh(new RefreshRequest
                {

                    //RefreshToken = loginResponse.RefreshToken
                });
                host.Dispose();
            }
            catch (ApiException ex)
            {
                await ex.GetContentAsAsync<string>();
            }
        }
        public async Task LoginWithFirebaseAlternative()
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(Email, Password);
                string token = a.FirebaseToken;
                var user = a.User;
                if (token != "")
                {
                    IsVisible = false;
                    var loginView = new LoginView();
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginView.Close();
                }
                else
                {
                    MessageBox.Show("Login unsuccessfull! Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { }
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
                MessageBox.Show("Successfully registered!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { }
        }



        public ICommand RestEmailCommand
        { get;

        }

        private async Task ResetEmail()
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                await auth.SendPasswordResetEmailAsync(Email);


                MessageBox.Show("A password reset option has been sent to " + Email, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { }
        }


        string clientID = "123648363985-niu7pmjinpll61fh45nddd2molc40u5v.apps.googleusercontent.com";
        string clientSecret = "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";

        string[] scopes = { "https://googleapis.com/auth/gmail.readonly" };
       
        public void LoginWithGoogle()
        {
            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientID,
                    ClientSecret = clientSecret,
                },
                scopes, "patricklarocque1@gmail.com", CancellationToken.None).Result;

            if (credentials.Token.IsExpired(SystemClock.Default))
                credentials.RefreshTokenAsync(CancellationToken.None).Wait();

            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentials
            });

        }
    }
}
