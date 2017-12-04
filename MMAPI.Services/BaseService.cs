using Microsoft.Azure.Documents.Client;
using MMAPI.Interfaces.Data;
using MMAPI.Repository;
using MMAPI.Repository.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MMAPI.Services
{
    public abstract class BaseService<TDocumentEntity> where TDocumentEntity : IDocumentEntity
    {
        protected IDocumentRepository<TDocumentEntity> repository;

        public BaseService(string uri, string authKey, string databaseName)
        {
            repository = new DocumentRepository<TDocumentEntity>(
                new DocumentRepositoryConfiguration(uri, authKey, "mmapidb", "fighters"),
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Gateway },
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                }
                );
        }
    }
}
