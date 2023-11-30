using System.Text;
using System.Text.Json;

namespace Booking.Application.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string uri, T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(uri, content);
        }
    }
}
