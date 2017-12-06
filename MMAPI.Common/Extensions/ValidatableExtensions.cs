namespace MMAPI.Common.Validator
{
    public static class ValidatableExtensions
    {
        public static bool IsValid(this IValidatable input)
        {
            // Sometimes all you care about is a boolean
            return input.ValidationResult().Success;
        }

        public static string ValidationMessage(this IValidatable input)
        {
            // Other times you just want the message. This is much more rare.
            return input.ValidationResult().Message;
        }

        public static ValidationResult ValidationResult(this IValidatable input)
        {
            // This avoids needing a null check in our code when we validate nullable objects
            return input == null ? new ValidationResult(false, "Null input is invalid.") : input.Validate();
        }
    }
}
