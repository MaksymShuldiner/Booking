using System.Net.Http.Headers;
using System.Text;

namespace Booking.Web.Authentication
{
    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        public BasicAuthenticationHeaderValue(string userName, string password)
            : base("Basic", Encode(userName, password))
        { }

        private static string Encode(string userName, string? password)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
            password ??= "";

            var encoding = Encoding.UTF8;
            var credential = $"{userName}:{password}";

            return Convert.ToBase64String(encoding.GetBytes(credential));
        }
    }
}
