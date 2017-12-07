using System;

namespace MMAPI.Repository.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryExceptionType ExceptionType { get; set; }

        public RepositoryException(RepositoryExceptionType exceptionType)
            : this(exceptionType, null) { }

        public RepositoryException(RepositoryExceptionType exceptionType, Exception innerException)
            : base(innerException?.Message ?? Enum.GetName(typeof(RepositoryExceptionType), exceptionType), innerException) { }
    }
}
