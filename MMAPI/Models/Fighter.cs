using MMAPI.Models.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace MMAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Fighter
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public Int16? Height { get; set; }

        public Int16? Reach { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public List<WeightSummary> WeightClasses { get; set; }

        public FighterRecord Record { get; set; }        
    }
}
