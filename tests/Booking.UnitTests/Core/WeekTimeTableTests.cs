using Booking.Core.Entities.ValueObjects;

namespace Booking.UnitTests.Core
{
    public class WeekTimeTableTests
    {
        private readonly WeekTimeTable _weekTimeTable = new()
        {
            Facility = new Facility
            {
                FacilityId = Guid.NewGuid()
            },
            SlotDurationMinutes = 30,
            Tuesday = new DayTimeTable
            {
                WorkPeriod = new WorkPeriod
                {
                    StartHour = 10,
                    LunchStartHour = 11,
                    LunchEndHour = 13,
                    EndHour = 14
                },
            },
            Thursday = new DayTimeTable
            {
                WorkPeriod = new WorkPeriod
                {
                    StartHour = 10,
                    LunchStartHour = 11,
                    LunchEndHour = 13,
                    EndHour = 14
                },
            }
        };

        [Fact]
        public void GetAvailableTimeSlots_ReturnsTimeSlots_Correctly()
        {
            // Arrange
            var dateNow = DateTime.Parse("2023-11-27");

            // Act
            var timeSlots = _weekTimeTable.GetAvailableTimeSlots(DateOnly.FromDateTime(dateNow));

            // Assert
            const int expectedTimeSlotCount = 8;
            Assert.Equal(expectedTimeSlotCount, timeSlots.Count);

            var expectedStartDateTime = dateNow.AddDays(3).AddHours(10);
            var expectedEndDateTime = dateNow.AddDays(3).AddHours(10).AddMinutes(30);
            Assert.Equal(timeSlots.ElementAt(4).Start, expectedStartDateTime);
            Assert.Equal(timeSlots.ElementAt(4).End, expectedEndDateTime);
        }
    }
}
