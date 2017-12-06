using MMAPI.Common.Validator;
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
    public class Fighter : BaseEntity, IValidatable
    {
        /// <summary>
        /// First name of the fighter.
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the fighter.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Nickname of the fighter.
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Height (in inches) of the fighter.
        /// </summary>
        [JsonProperty("height")]
        public ushort Height { get; set; }

        /// <summary>
        /// Reach (in inches) of the fighter.
        /// </summary>
        [JsonProperty("reach")]
        public ushort Reach { get; set; }

        /// <summary>
        /// Gender of the fighter.
        /// </summary>
        [JsonProperty("gender")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        /// <summary>
        /// UTC Date of Birth of the fighter.
        /// </summary>
        [JsonProperty("dateOfBirth")]
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// Weight Classes associated with this fighter.
        /// </summary>
        [JsonProperty("weightClasses")]
        public List<WeightSummary> WeightClasses { get; set; }

        /// <summary>
        /// Professional record of the fighter.
        /// </summary>
        [JsonProperty("record")]
        public FighterRecord Record { get; set; }

        public ValidationResult Validate()
        {
            List<string> messages = new List<string>();
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                messages.Add("FirstName cannot be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                messages.Add("LastName cannot be null, empty or whitespace.");
            }

            if (DateOfBirth == null)
            {
                messages.Add("DateOfBrith cannot be null.");
            }

            if (Height == 0)
            {
                messages.Add("Height must be greater than 0.");
            }

            if (Reach == 0)
            {
                messages.Add("Reach must be greater than 0.");
            }

            return messages.ToValidationResult();
        }
    }
}
