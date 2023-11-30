using Booking.Core.Entities.ValueObjects;

namespace Booking.Application.Contracts.Responses
{
    public class TimeSlotResponse
    {
        public Guid? FacilityId { get; set; }
        public List<TimeSlot>? TimeSlots { get; set; }
    }
}
