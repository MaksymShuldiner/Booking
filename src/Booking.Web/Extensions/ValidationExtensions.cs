using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Extensions
{
    public static class ValidationExtensions
    {
        public static IEnumerable<string> Validate<T>(this T obj) where T : class 
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(obj, context, results, true);
            foreach (var validationResult in results)
            {
                yield return validationResult.ErrorMessage!;
            }
        }
    }
}
