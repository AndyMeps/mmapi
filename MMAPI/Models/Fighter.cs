using MMAPI.Models.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MMAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Fighter
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
