namespace Booking.Core.Entities.ValueObjects;

public record DayTimeTable
{
    public WorkPeriod? WorkPeriod { get; set; }
    public IEnumerable<TimeSlot>? BusySlots { get; set; }

    public List<TimeSlot> GetAvailableTimeSlots(int slotDuration, DateOnly currentDay)
    {
        var availableSlots = new List<TimeSlot>();
        var busySlots = BusySlots?.ToList();

        if (WorkPeriod is null)
        {
            return availableSlots;
        }

        var startMorning = new DateTime(currentDay, new TimeOnly(WorkPeriod.StartHour, 0, 0));
        var endMorning = new DateTime(currentDay, new TimeOnly(WorkPeriod.LunchStartHour, 0, 0));
        var startAfternoon = new DateTime(currentDay, new TimeOnly(WorkPeriod.LunchEndHour, 0, 0));
        var endAfternoon = new DateTime(currentDay, new TimeOnly(WorkPeriod.EndHour, 0, 0));

        AddAvailableSlots(availableSlots, startMorning, endMorning, busySlots, slotDuration);
        AddAvailableSlots(availableSlots, startAfternoon, endAfternoon, busySlots, slotDuration);

        return availableSlots;
    }

    private static void AddAvailableSlots(ICollection<TimeSlot> availableSlots, DateTime start, DateTime end, IReadOnlyCollection<TimeSlot>? busySlots, int slotDuration)
    {
        while (start.AddMinutes(slotDuration) <= end)
        {
            var currentEnd = start.AddMinutes(slotDuration);

            if (!IsTimeSlotOccupied(start, currentEnd, busySlots))
            {
                availableSlots.Add(new TimeSlot { Start = start, End = currentEnd });
            }

            start = start.AddMinutes(slotDuration);
        }
    }

    private static bool IsTimeSlotOccupied(DateTime start, DateTime end, IEnumerable<TimeSlot>? busySlots)
    {
        return busySlots?.Any(busySlot => (start >= busySlot.Start && start < busySlot.End) || (end > busySlot.Start && end <= busySlot.End)) == true;
    }
}