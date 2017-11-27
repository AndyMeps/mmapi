using MMAPI.Models.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace MMAPI.Models
{
    /// <summary>
    /// A fighter entry including general statistics relating to the fighter.
    /// </summary>
    public class Fighter
    {
        /// <summary>
        /// Unique identifier for the fighter.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name of the fighter.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the fighter.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Nickname of the fighter.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Height (in inches) of the fighter.
        /// </summary>
        public ushort Height { get; set; }

        /// <summary>
        /// Reach (in inches) of the fighter.
        /// </summary>
        public ushort Reach { get; set; }

        /// <summary>
        /// Gender of the fighter.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        /// <summary>
        /// UTC Date of Birth of the fighter.
        /// </summary>
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// Weight Classes associated with this fighter.
        /// </summary>
        public List<WeightSummary> WeightClasses { get; set; }

        /// <summary>
        /// Professional record of the fighter.
        /// </summary>
        public FighterRecord Record { get; set; }        
    }
}
