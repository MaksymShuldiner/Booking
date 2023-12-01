using System.Text;
using Booking.Web.Authentication;

namespace Booking.UnitTests.Web;

public class BasicAuthenticationHeaderValueTests
{

    [Fact]
    public void BasicAuthenticationHeader_InitializesHeader_Correctly()
    {
        // Arrange
        const string userName = "testUser";
        const string password = "testPassword";
        var expectedBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));

        // Act
        var authenticationHeaderValue = new BasicAuthenticationHeaderValue(userName, password);

        // Assert
        Assert.Equal("Basic", authenticationHeaderValue.Scheme);
        Assert.Equal(expectedBase64, authenticationHeaderValue.Parameter);
    }

    [Fact]
    public void BasicAuthenticationHeader_ThrowsException_IfUserNameIsNull()
    {
        // Arrange
        string userName = null!;
        const string password = "testPassword";

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => new BasicAuthenticationHeaderValue(userName, password));
    }
}