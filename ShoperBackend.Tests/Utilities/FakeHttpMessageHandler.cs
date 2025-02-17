using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ShoperBackend.Tests.Utilities
{
    class FakeHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response = response;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
