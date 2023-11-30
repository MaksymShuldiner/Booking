using Booking.Application.Contracts.Requests;
using Booking.Application.Contracts.Responses;
using Booking.Application.Extensions;
using Booking.Application.Services.Abstractions;
using Booking.Core.Entities.ValueObjects;
using System.Net.Http.Json;

namespace Booking.Application.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly HttpClient _httpClient;
        private const string WeeklyAvailabilityUrl = "/api/availability/GetWeeklyAvailability/";
        private const string TakeSlotUrl = "/api/availability/TakeSlot";

        public TimeSlotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TimeSlotResponse?> GetWeeklyAvailability(DateOnly weekStartDate)
        {
            var dateString = weekStartDate.ToString("yyyyMMdd");
            var weekTimeTable = await _httpClient.GetFromJsonAsync<WeekTimeTable>($"{WeeklyAvailabilityUrl}{dateString}");
            var slots = weekTimeTable.GetAvailableTimeSlots(weekStartDate);
            return new TimeSlotResponse { FacilityId = weekTimeTable!.Facility?.FacilityId, TimeSlots = slots };
        }

        public async Task TakeSlot(TakeSlotRequest request)
        {
            var response = await _httpClient.PostAsJson(TakeSlotUrl, request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Server Error");
            }
        }
    }
}
