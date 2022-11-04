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
    internal interface IFirebaseRefreshService
    {
        [Post("/v1/token")]
        Task<RefreshResponse> Refresh([Body(BodySerializationMethod.UrlEncoded)]RefreshRequest refreshRequest);
    }
}
