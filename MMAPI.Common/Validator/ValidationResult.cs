using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Common.Validator
{
    public class ValidationResult
    {
        public ValidationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success;
        public string Message;
    }
}
