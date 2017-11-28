using System;
using System.Collections.Generic;

namespace MMAPI.Models
{
    public class WeightClass : IComparable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ushort UpperWeightLimit { get; set; }

        #region Object Overrides
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var wc = obj as WeightClass;

            if (wc == null) throw new Exception("Object is not a WeightClass");

            return UpperWeightLimit.CompareTo(wc.UpperWeightLimit);
        }

        public override bool Equals(object obj)
        {
            return obj is WeightClass && this == (WeightClass)obj;
        }

        public override int GetHashCode()
        {
            var hashCode = -62436748;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + UpperWeightLimit.GetHashCode();
            return hashCode;
        }
        #endregion

        #region Operator Overrides
        public static bool operator ==(WeightClass weightClass1, WeightClass weightClass2)
        {
            if(!ReferenceEquals(weightClass1, null) && !ReferenceEquals(weightClass2, null))
            {
                return weightClass1.UpperWeightLimit == weightClass2.UpperWeightLimit;
            }
            return false;
        }
        public static bool operator !=(WeightClass weightClass1, WeightClass weightClass2)
        {
            return !(weightClass1 == weightClass2);
        }
        #endregion
    }
}
