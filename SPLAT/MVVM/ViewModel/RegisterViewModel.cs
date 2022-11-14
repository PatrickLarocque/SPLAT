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
        private bool _isViewVisible = true;


        //properties
        public string Email { get => _email; set { _email = value; OnPropertyChanged("Email"); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged("Password"); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsVisible)); } }


        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegister);
 
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

        public ICommand RegisterCommand
        {
            get;
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
