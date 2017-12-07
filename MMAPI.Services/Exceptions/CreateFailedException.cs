using MMAPI.Repository.Exceptions;
using System;

namespace MMAPI.Services.Exceptions
{
    public class CreateFailedException : Exception
    {
        public CreateFailedException(string collection, RepositoryException innerException)
            : base($"Create failed on `{collection}`", innerException) { }
    }
}
