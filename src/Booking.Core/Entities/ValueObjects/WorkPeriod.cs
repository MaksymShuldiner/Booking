﻿namespace Booking.Core.Entities.ValueObjects
{
    public record WorkPeriod
    {
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public int LunchStartHour { get; set; }
        public int LunchEndHour { get; set; }
    }
}