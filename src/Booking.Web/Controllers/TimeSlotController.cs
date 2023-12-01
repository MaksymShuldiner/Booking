using Booking.Application.Contracts.Requests;
using Booking.Application.Services.Abstractions;
using Booking.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers;

[ApiController]
public class TimeSlotController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;

    public TimeSlotController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }


    [HttpGet(ApiRoutes.TimeSlot.GetWeekly)]
    public async Task<IActionResult> GetWeekly([FromRoute] DateTime weekStartDate)
    {
        if (weekStartDate.DayOfWeek is not DayOfWeek.Monday)
        {
            return BadRequest("Date should be Monday");
        }

        var timeSlotResponse = await _timeSlotService.GetWeeklyAvailability(DateOnly.FromDateTime(weekStartDate));
        return Ok(timeSlotResponse);
    }

    [HttpPost(ApiRoutes.TimeSlot.Base)]
    public async Task<IActionResult> Post([FromBody] TakeSlotRequest request)
    {
        var errors = request.Validate().ToList();

        if (errors.Any())
        {
            return BadRequest(errors);
        }

        await _timeSlotService.TakeSlot(request);
        return Ok();
    }
}