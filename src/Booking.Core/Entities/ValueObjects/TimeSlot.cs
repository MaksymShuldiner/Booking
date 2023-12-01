namespace Booking.Core.Entities.ValueObjects;

public record TimeSlot
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}