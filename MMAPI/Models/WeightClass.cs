using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Models
{
    public class WeightClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Int16 MinWeight { get; set; }
        public Int16 MaxWeight { get; set; }
    }
}
