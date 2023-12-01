using Booking.Core.Entities.ValueObjects;

namespace Booking.UnitTests.Core;

public class DayTimeTableTests
{
    private readonly WorkPeriod _workPeriod = new()
    {
        StartHour = 10,
        LunchStartHour = 11,
        LunchEndHour = 13,
        EndHour = 14
    };

    [Fact]
    public void GetAvailableTimeSlots_ReturnsTimeSlots_Correctly()
    {
        // Arrange
        var dateNow = DateTime.Now.Date;
        var dayTimeTable = new DayTimeTable
        {
            WorkPeriod = _workPeriod
        };

        // Act
        var timeSlots = dayTimeTable.GetAvailableTimeSlots(30, DateOnly.FromDateTime(dateNow));

        // Assert
        const int expectedTimeSlotCount = 4;
        Assert.Equal(expectedTimeSlotCount, timeSlots.Count);

        var expectedStartDateTime = dateNow.AddHours(10);
        var expectedEndDateTime = dateNow.AddHours(10).AddMinutes(30);
        Assert.Equal(timeSlots.First().Start, expectedStartDateTime);
        Assert.Equal(timeSlots.First().End, expectedEndDateTime);
    }

    [Fact]
    public void GetAvailableTimeSlots_ReturnsTimeSlots_Correctly_WhenBusySlotsArePresent()
    {
        // Arrange
        var dateNow = DateTime.Now.Date;
        var busyTimeSlot = new TimeSlot { Start = dateNow.AddHours(10).AddMinutes(30), End = dateNow.AddHours(11) };
        var dayTimeTable = new DayTimeTable
        {
            WorkPeriod = _workPeriod,
            BusySlots = new[] { busyTimeSlot }
        };

        // Act
        var timeSlots = dayTimeTable.GetAvailableTimeSlots(30, DateOnly.FromDateTime(dateNow));

        // Assert
        const int expectedTimeSlotCount = 3;
        Assert.Equal(expectedTimeSlotCount, timeSlots.Count);
    }
}