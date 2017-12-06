using MMAPI.Common.Validator;
using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static ValidationResult ToValidationResult<T>(this IEnumerable<T> input) where T : class
        {
            // Centralizes boilerplate needed to convert the list of errors into a single string.
            if (input == null)
            {
                return new ValidationResult(false, "Null input is invalid.");
            }

            // Enumerate the errors
            var errors = input.ToList();
            var success = !errors.Any();
            var message = success ? "Validation successful." : string.Join(" ", errors);

            return new ValidationResult(success, message);
        }
    }
}
