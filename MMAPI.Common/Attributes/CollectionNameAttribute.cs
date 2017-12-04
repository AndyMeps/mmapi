using System;

namespace MMAPI.Common.Attributes
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
