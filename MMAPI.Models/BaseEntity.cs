using MMAPI.Repository.Interfaces;
using Newtonsoft.Json;
using System;

namespace MMAPI.Models
{
    public class BaseEntity : IEntity
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; set; }
    }
}
