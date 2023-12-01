using System.ComponentModel.DataAnnotations;
using Booking.Application.Contracts.Requests.Shared;

namespace Booking.Application.Contracts.Requests;

public class TakeSlotRequest
{
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    public string? Comments { get; set; }
    [Required]
    public Patient? Patient { get; set; }
    [Required]
    public Guid FacilityId { get; set; }
}