using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPLAT.Http
{
    internal class FirebaseAPIKeyHttpMessageHandler : DelegatingHandler
    {

        private readonly string _apiKey;

        public FirebaseAPIKeyHttpMessageHandler(string apiKey)
        {
            _apiKey = apiKey;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = new Uri($"{request.RequestUri}?key={_apiKey}");
            return base.SendAsync(request, cancellationToken);  
        }
    }
}
