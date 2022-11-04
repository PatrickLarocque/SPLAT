using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLAT.Responses
{
    internal class LoginResponse
    {
        public string IdTokens { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }

    }
}
