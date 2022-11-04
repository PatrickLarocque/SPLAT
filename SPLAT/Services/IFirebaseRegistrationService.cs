using SPLAT.Requests;
using SPLAT.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLAT.Services
{
    internal interface IFirebaseRegistrationService
    {
        [Post("/v1/accounts:signUp")]
        Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
    }
}
