namespace Booking.Web.Exceptions;

public class ConfigurationInvalidException : Exception
{
    public ConfigurationInvalidException(IReadOnlyCollection<string> errors) 
        : base($"Found {errors.Count} configuration error(s) in {errors.GetType().Name}: {errors.Aggregate((a, b) => a + ", " + b)}")
    {
            
    }
}