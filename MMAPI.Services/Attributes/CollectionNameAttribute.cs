using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Services.Attributes
{
    public class CollectionNameAttribute : Attribute
    {
        public string Name { get; private set;}

        public CollectionNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
