using System;

namespace MMAPI.Repository.Exceptions
{
    public class NullDocumentResultException : Exception
    {
        public NullDocumentResultException() : base("The resulting document is null.") { }
    }
}
