using Newtonsoft.Json;
using System;

namespace MMAPI.Repository.Interfaces
{
    public interface IEntity
    {
        [JsonProperty("id")]
        Guid? Id { get; set; }
    }
}
