using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class TypeExtensions
    {
        public static T FirstAttribute<T>(this Type type, bool inherit = false) where T : Attribute {
            return type.GetCustomAttributes(typeof(T), inherit).FirstOrDefault() as T;
        }
    }
}
