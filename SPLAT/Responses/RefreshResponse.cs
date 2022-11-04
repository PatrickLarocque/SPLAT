using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLAT.Responses
{
    internal class RefreshResponse
    {
        [JsonProperty("id_token")]
        public string IdTokens { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
