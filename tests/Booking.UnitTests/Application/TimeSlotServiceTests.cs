using Booking.Application.Contracts.Requests;
using Booking.Application.Services;
using Booking.Core.Entities.ValueObjects;
using System.Net;

namespace Booking.UnitTests.Application;

public class TimeSlotServiceTests
{
    [Fact]
    public async Task GetWeeklyAvailability_EmptyResponse_ReturnsEmptyTimeSlots()
    {
        // Arrange
        var mondayStartDate = new DateOnly(2023, 1, 2);
        var messageHandler = HttpClientHelper.ConfigureMessageHandler<WeekTimeTable>(HttpStatusCode.OK);
        var httpClient = HttpClientHelper.CreateHttpClient(messageHandler);

        var service = new TimeSlotService(httpClient);

        // Act
        var result = await service.GetWeeklyAvailability(mondayStartDate);

        // Assert
        Assert.Empty(result!.TimeSlots!);
    }

    [Fact]
    public async Task TakeSlot_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var messageHandler = HttpClientHelper.ConfigureMessageHandler<WeekTimeTable>(HttpStatusCode.OK);
        var httpClient = HttpClientHelper.CreateHttpClient(messageHandler);

        var service = new TimeSlotService(httpClient);

        // Act and Assert
        await service.TakeSlot(new TakeSlotRequest());
    }

    [Fact]
    public async Task TakeSlot_UnsuccessfulRequest_ThrowsHttpRequestException()
    {
        // Arrange
        var messageHandler = HttpClientHelper.ConfigureMessageHandler<WeekTimeTable>(HttpStatusCode.InternalServerError);
        var httpClient = HttpClientHelper.CreateHttpClient(messageHandler);

        var service = new TimeSlotService(httpClient);

        // Act and Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => service.TakeSlot(new TakeSlotRequest()));
    }
}