using Newtonsoft.Json;
using System;

namespace MMAPI.Interfaces.Data
{
    public interface IDocumentEntity
    {
        [JsonProperty("id")]
        Guid? Id { get; set; }
    }
}
