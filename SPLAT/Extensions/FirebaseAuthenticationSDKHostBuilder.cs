using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using SPLAT.Http;
using SPLAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLAT.Extensions
{
    internal static class FirebaseAuthenticationSDKHostBuilder
    {

        private const string IDENTITY_TOOLKIT_BASE_URL = "https://identitytoolkit.googleapis.com";
        private const string SECURE_TOKEN_BASE_URL = "https://securetoken.googleapis.com";

        public static IHostBuilder AddFirebaseAuthenticationSDK(this IHostBuilder hostBuilder, string apiKey)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<FirebaseAPIKeyHttpMessageHandler>(s => new FirebaseAPIKeyHttpMessageHandler(apiKey));

                services.AddRefitClient<IFirebaseRegistrationService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(IDENTITY_TOOLKIT_BASE_URL))
                .AddHttpMessageHandler<FirebaseAPIKeyHttpMessageHandler>();

                services.AddRefitClient<IFirebaseLoginService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(IDENTITY_TOOLKIT_BASE_URL))
                .AddHttpMessageHandler<FirebaseAPIKeyHttpMessageHandler>();

                services.AddRefitClient<IFirebaseRefreshService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(SECURE_TOKEN_BASE_URL))
                .AddHttpMessageHandler<FirebaseAPIKeyHttpMessageHandler>();
            });
            return hostBuilder;
        }

    }
}
