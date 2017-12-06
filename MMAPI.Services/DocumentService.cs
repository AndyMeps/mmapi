using Microsoft.Azure.Documents.Client;
using MMAPI.Repository;
using MMAPI.Repository.Data;
using MMAPI.Repository.Exceptions;
using MMAPI.Repository.Interfaces;
using MMAPI.Services.Factories;
using MMAPI.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Services
{
    public class DocumentService<D> : IService<D> where D : IEntity
    {
        protected IRepository<D> repository;

        public DocumentService()
            : this(Environment.GetEnvironmentVariable("DocumentDbEndpoint"),
                   Environment.GetEnvironmentVariable("DocumentDbAuthKey"),
                   Environment.GetEnvironmentVariable("DocumentDbName")) { }

        public DocumentService(string uri, string authKey, string databaseName)
        {
            repository = new DocumentRepository<D>(
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

        public async Task<string> CreateAsync(D entity)
        {
            try
            {
                return await repository.CreateAsync(entity);
            }
            catch (RepositoryException ex)
            {
                return null;
            }            
        }

        public async Task<bool> ExistsAsync(Expression<Func<D, bool>> expression)
        {
            var result = await repository.FindAsync(expression);
            return result.Any();
        }

        public async Task<D> FindByIdAsync(Guid id)
        {
            return await repository.FindByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await repository.DeleteAsync(id);
        }

        public string CollectionName
        {
            get
            {
                return CollectionPropertyFactory.GetCollectionName<D>();
            }
        }
    }
}
