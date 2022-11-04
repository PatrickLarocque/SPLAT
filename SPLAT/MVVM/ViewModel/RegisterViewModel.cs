using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using SPLAT.Core;
using SPLAT.Extensions;
using SPLAT.Requests;
using SPLAT.Responses;
using SPLAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsVisible)); } }

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

    }
}
