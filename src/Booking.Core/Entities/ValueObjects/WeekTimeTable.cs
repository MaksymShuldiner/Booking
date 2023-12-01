namespace Booking.Core.Entities.ValueObjects;

public record WeekTimeTable
{
    public Facility? Facility { get; set; }
    public int SlotDurationMinutes { get; set; }
    public DayTimeTable? Monday { get; set; }
    public DayTimeTable? Tuesday { get; set; }
    public DayTimeTable? Wednesday { get; set; }
    public DayTimeTable? Thursday { get; set; }
    public DayTimeTable? Friday { get; set; }
    public DayTimeTable? Saturday { get; set; }
    public DayTimeTable? Sunday { get; set; }

    public List<TimeSlot> GetAvailableTimeSlots(DateOnly weekStartDate)
    {
        var availableSlots = new List<TimeSlot>();
        var currentDay = weekStartDate;

        foreach (Enums.DayOfWeek day in Enum.GetValues(typeof(Enums.DayOfWeek)))
        {
            var dayTimeTable = GetDayTimeTable(day);

            if (dayTimeTable != null)
            {
                var daySlots = dayTimeTable.GetAvailableTimeSlots(SlotDurationMinutes, currentDay);
                availableSlots.AddRange(daySlots);
            }

            currentDay = currentDay.AddDays(1);
        }

        return availableSlots;
    }

    private DayTimeTable? GetDayTimeTable(Enums.DayOfWeek day)
    {
        return day switch
        {
            Enums.DayOfWeek.Monday => Monday,
            Enums.DayOfWeek.Tuesday => Tuesday,
            Enums.DayOfWeek.Wednesday => Wednesday,
            Enums.DayOfWeek.Thursday => Thursday,
            Enums.DayOfWeek.Friday => Friday,
            Enums.DayOfWeek.Saturday => Saturday,
            Enums.DayOfWeek.Sunday => Sunday,
            _ => Monday,
        };
    }
}