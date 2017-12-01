using MMAPI.Interfaces.Data;
using Newtonsoft.Json;
using System;

namespace MMAPI.Models
{
    public class BaseEntity : IDocumentEntity
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; set; }
    }
}
