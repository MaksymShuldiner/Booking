using Newtonsoft.Json;
using System.Net;

namespace Booking.UnitTests;

public class HttpClientHelper
{
    public static CustomHttpMessageHandler ConfigureMessageHandler<T>(HttpStatusCode statusCode, T? response = default)
    {
        var messageHandler = new CustomHttpMessageHandler();
        messageHandler.SetupResponse(statusCode, response);
        return messageHandler;
    }

    public class CustomHttpMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;
        private int _sendAsyncCallCount;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _sendAsyncCallCount++;
            return Task.FromResult(_response);
        }

        public void SetupResponse<T>(HttpStatusCode statusCode, T? response = default)
        {
            var content = response == null ? "{}" : JsonConvert.SerializeObject(response);

            _response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            };
        }

        public int SendAsyncCallCount => _sendAsyncCallCount;
    }

    public static HttpClient CreateHttpClient(HttpMessageHandler messageHandler)
    {
        var httpClient = new HttpClient(messageHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };
        return httpClient;
    }
}