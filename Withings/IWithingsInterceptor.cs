using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Withings.API.Portable
{
    public interface IWithingsInterceptor
    {
        Task<HttpResponseMessage> InterceptRequest(HttpRequestMessage request, CancellationToken cancellationToken, WithingsClient invokingClient);
        Task<HttpRequestMessage> InterceptResponse(Task<HttpResponseMessage> response, CancellationToken cancellationToken, WithingsClient invokingClient);
    }
}
