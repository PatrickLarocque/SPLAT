using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SPLAT.Responses;
using SPLAT.Requests;


namespace SPLAT.Services
{
    internal interface IFirebaseLoginService
    {
        [Post("/v1/accounts:signInWithPassword")]
        Task<LoginResponse> Login(LoginRequest loginRequest);

    }
}
