using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SPLAT.MVVM.View;
using SPLAT.Services;
using Refit;
using SPLAT.Http;
using SPLAT.Requests;
using System.Security.Cryptography.X509Certificates;

namespace SPLAT
{

    public partial class App : Application
    {
        protected async void OnStartup(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginView.Close();
                }
            };

            await Firebase();
        }

        static async Task Firebase()
        {
            string apiKey = "AIzaSyBtDODfvO9X3TGMXz1sRiNqhDjLL7NOJSE";
            string identityToolkitBaseURL = "https://identitytoolkit.googleapis.com";

            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<FirebaseAPIKeyHttpMessageHandler>(s => new FirebaseAPIKeyHttpMessageHandler(apiKey));

                    services.AddRefitClient<IFirebaseRegistrationService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(identityToolkitBaseURL))
                    .AddHttpMessageHandler<FirebaseAPIKeyHttpMessageHandler>();
                })
                .Build();

            IFirebaseRegistrationService registrationService = host.Services.GetRequiredService<IFirebaseRegistrationService>();
            await registrationService.Register(new RegistrationRequest
            {
                Email = "patl@gmail.com",
                Password = "123456",
                ReturnSecureToken = true
            });
        }
    }

}
