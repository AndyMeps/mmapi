using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Services.Exceptions
{
    public class InvalidResourceIdException : Exception
    {
        public InvalidResourceIdException(string id)
            : base($"Invalid resource id: `{id}`") { }
    }
}
