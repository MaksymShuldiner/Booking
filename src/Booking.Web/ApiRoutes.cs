namespace Booking.Web
{
    public static class ApiRoutes
    {
        public const string Api = "/api";
        public const string Version = "/v1";

        public const string Prefix = Api + Version;

        public static class TimeSlot
        {
            public const string Base = Prefix + "/timeSlot";
            public const string GetWeekly = Base + "/weekly/{weekStartDate}";
        }
    }
}
