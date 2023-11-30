using Booking.Web.Extensions;
using Booking.Web.Settings;

namespace Booking.UnitTests.Web
{
    public class TimeSlotServiceSettingsTests
    {
        [Fact]
        public void TimeSlotServiceSettings_Validate_DoesNotThrowException_IfEverythingIsValid()
        {
            // Arrange
            var settings = new TimeSlotServiceSettings
            {
                BaseUrl = "https://example.com",
                UserName = "testUser",
                Password = "testPassword"
            };

            // Act and Assert
            var exception = Record.Exception(settings.ThrowIfInvalid);
            Assert.Null(exception);
        }

        [Fact]
        public void TimeSlotServiceSettings_Validate_ThrowException_IfAnythingIsInvalid()
        {
            // Arrange
            var settings = new TimeSlotServiceSettings
            {
                BaseUrl = "https://example.com",
                Password = "testPassword"
            };

            // Act and Assert
            var exception = Record.Exception(settings.ThrowIfInvalid);
            Assert.NotNull(exception);
        }
    }
}
