using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using MMAPI.Interfaces.Data;
using MMAPI.Repository.Exceptions;
using MMAPI.Repository.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Repository
{
    public class DocumentRepository<T> : IDocumentRepository<T> where T : IDocumentEntity
    {
        private IRepositoryConfiguration _config;
        private ConnectionPolicy _connectionPolicy;
        private JsonSerializerSettings _serializerSettings;

        public DocumentRepository(IRepositoryConfiguration configuration,
            ConnectionPolicy connectionPolicy = null,
            JsonSerializerSettings serializerSettings = null)
        {
            this._config = configuration;
            this._connectionPolicy = connectionPolicy;
            this._serializerSettings = serializerSettings;
        }

        public async Task<string> CreateDocumentAsync<TEntity>(TEntity entity) where TEntity : IDocumentEntity
        {
            if (entity == null) throw new ArgumentNullException("entity");

            if (string.IsNullOrEmpty(_config.Collection))
                throw new ArgumentException("Cannot be null or empty.", "collectionId");

            using (var client = GetClient())
            {
                Document result = await client.CreateDocumentAsync(_config.GetDocumentCollectionUri(), entity);

                if (result == null) throw new NullDocumentResultException();

                return result.Id;
            }
        }

        public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : IDocumentEntity
        {
            var result = await FindAsync(condition);

            return result.Any();
        }

        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : IDocumentEntity
        {
            using (var client = GetClient())
            {
                var query = client.CreateDocumentQuery<TEntity>(_config.GetDocumentCollectionUri()).Where(condition);

                return await query.AsDocumentQuery().GetAllResultsAsync();
            }
        }

        public async Task<TEntity> FindById<TEntity>(Guid id) where TEntity : IDocumentEntity
        {
            using (var client = GetClient())
            {
                var query = client.CreateDocumentQuery<TEntity>(_config.GetDocumentCollectionUri()).Where(c => c.Id == id);

                return await query.AsDocumentQuery().FirstOrDefault();
            }
        }

        private DocumentClient GetClient()
        {
            if (_config == null) throw new InvalidOperationException("Null configuration provided.");

            return new DocumentClient(
                _config.Endpoint,
                _config.AuthKey,
                _serializerSettings,
                _connectionPolicy);
        }
    }
}
