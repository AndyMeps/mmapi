using System;

namespace MMAPI.Common.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException(string message) : base(message) { }
    }
}
