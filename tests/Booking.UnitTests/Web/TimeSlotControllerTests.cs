using Booking.Application.Contracts.Requests;
using Booking.Application.Contracts.Requests.Shared;
using Booking.Application.Services.Abstractions;
using Booking.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Booking.UnitTests.Web;

public class TimeSlotControllerTests
{
    [Fact]
    public async Task GetWeekly_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var mondayDate = new DateTime(2023, 1, 2); // A Monday
        var mockTimeSlotService = new Mock<ITimeSlotService>();
        var controller = new TimeSlotController(mockTimeSlotService.Object);

        // Act
        var result = await controller.GetWeekly(mondayDate);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        mockTimeSlotService.Verify(
            x => x.GetWeeklyAvailability(It.IsAny<DateOnly>()),
            Times.Once);
    }

    [Fact]
    public async Task GetWeekly_InvalidRequest_ReturnsBadRequestResult()
    {
        // Arrange
        var notMondayDate = new DateTime(2023, 1, 3);
        var controller = new TimeSlotController(Mock.Of<ITimeSlotService>());

        // Act
        var result = await controller.GetWeekly(notMondayDate);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Post_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var request = new TakeSlotRequest
        {
            Start = DateTime.Now,
            End = DateTime.Now,
            FacilityId = Guid.NewGuid(),
            Patient = new Patient
            {
                Name = "str",
                Email = "str",
                Phone = "str",
                SecondName = "str"
            }
        };

        var mockTimeSlotService = new Mock<ITimeSlotService>();
        var controller = new TimeSlotController(mockTimeSlotService.Object);

        // Act
        var result = await controller.Post(request);

        // Assert
        Assert.IsType<OkResult>(result);
        mockTimeSlotService.Verify(
            x => x.TakeSlot(It.IsAny<TakeSlotRequest>()),
            Times.Once);
    }

    [Fact]
    public async Task Post_InvalidRequest_ReturnsBadRequestResultWithErrors()
    {
        // Arrange
        var request = new TakeSlotRequest();
        var controller = new TimeSlotController(Mock.Of<ITimeSlotService>());

        // Act
        var result = await controller.Post(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<string>>(badRequestResult.Value);
        Assert.NotEmpty(errors);
    }
}