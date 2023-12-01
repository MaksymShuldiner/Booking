namespace Booking.Core.Entities.ValueObjects;

public record Facility
{
    public Guid FacilityId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; } = null;
}