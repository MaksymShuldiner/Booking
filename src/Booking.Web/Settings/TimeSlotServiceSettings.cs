using System.ComponentModel.DataAnnotations;
using System.Runtime;
using Booking.Web.Exceptions;
using Booking.Web.Extensions;

namespace Booking.Web.Settings
{
    public class TimeSlotServiceSettings
    {
        [Required]
        public string BaseUrl { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public void ThrowIfInvalid()
        {
            var errors = this.Validate().ToList();

            if (errors.Any())
            {
                throw new ConfigurationInvalidException(errors);
            }
        }
    }
}
