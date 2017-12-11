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
    using Exceptions;
    using Microsoft.Azure.Documents;

    public class DocumentService<D> : IService<D> where D : IEntity
    {
        protected IRepository<D> repository;

        public DocumentService()
        {
            repository = new DocumentRepository<D>(
                new DocumentRepositoryConfiguration(
                    Environment.GetEnvironmentVariable("DocumentDbEndpoint"),
                    Environment.GetEnvironmentVariable("DocumentDbAuthKey"),
                    Environment.GetEnvironmentVariable("DocumentDbName"),
                    CollectionName),
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Gateway },
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                });
        }

        public DocumentService(IRepository<D> repo)
        {
            repository = repo;
        }

        public async Task<string> CreateAsync(D entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            try
            {
                return await repository.CreateAsync(entity);
            }
            catch (RepositoryException ex)
            {
                throw new CreateFailedException(CollectionName, ex);
            }            
        }

        public async Task<bool> ExistsAsync(Expression<Func<D, bool>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            var result = await repository.FindAsync(expression);
            if (result == null) return false;

            return result.Any();
        }

        public async Task<D> FindByIdAsync(Guid id)
        {
            return await repository.FindByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId)) throw new InvalidResourceIdException(id);

            await repository.DeleteAsync(id);
        }

        public string CollectionName
        {
            get
            {
                if (_collectionName == null)
                {
                    _collectionName = CollectionPropertyFactory.GetCollectionName<D>();
                }
                return _collectionName;
            }
        }

        private string _collectionName;
    }
}
