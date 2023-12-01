using System.Net;
using System.Text.Json;

namespace Booking.UnitTests;

public class HttpClientHelper
{
    public static CustomHttpMessageHandler<T> ConfigureMessageHandler<T>(HttpStatusCode statusCode, T? response = default)
    {
        var messageHandler = new CustomHttpMessageHandler<T>(statusCode, response);
        return messageHandler;
    }

    public class CustomHttpMessageHandler<T> : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public CustomHttpMessageHandler(HttpStatusCode statusCode, T? response = default)
        {
            var content = response == null ? "{}" : JsonSerializer.Serialize(response);

            _response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            };
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
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