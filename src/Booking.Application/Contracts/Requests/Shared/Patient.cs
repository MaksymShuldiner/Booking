using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Contracts.Requests.Shared
{
    public class Patient
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? SecondName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
    }
}
