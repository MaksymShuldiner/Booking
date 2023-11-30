using Booking.Application.Contracts.Requests;
using Booking.Application.Contracts.Responses;
using Booking.Core.Entities.ValueObjects;

namespace Booking.Application.Services.Abstractions
{
    public interface ITimeSlotService
    {
        Task<TimeSlotResponse?> GetWeeklyAvailability(DateOnly weekStartDate);
        Task TakeSlot(TakeSlotRequest request);
    }
}
