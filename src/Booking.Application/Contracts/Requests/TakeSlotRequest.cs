using System.ComponentModel.DataAnnotations;

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