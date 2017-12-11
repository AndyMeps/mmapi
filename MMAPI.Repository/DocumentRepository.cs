using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using MMAPI.Repository.Exceptions;
using MMAPI.Repository.Helpers;
using MMAPI.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MMAPI.Repository
{
    public class DocumentRepository<D> : IRepository<D> where D : IEntity
    {
        #region Private Members
        private IRepositoryConfiguration _config;
        private ConnectionPolicy _connectionPolicy;
        private JsonSerializerSettings _serializerSettings;
        #endregion

        #region ctor
        public DocumentRepository(IRepositoryConfiguration configuration,
            ConnectionPolicy connectionPolicy = null,
            JsonSerializerSettings serializerSettings = null)
        {
            this._config = configuration;
            this._connectionPolicy = connectionPolicy;
            this._serializerSettings = serializerSettings;
        }
        #endregion

        public async Task<string> CreateAsync(D entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            if (string.IsNullOrEmpty(_config.Collection))
                throw new ArgumentException("Cannot be null or empty.", "collectionId");

            using (var client = GetClient())
            {
                try
                {
                    Document result = await client.CreateDocumentAsync(_config.GetDocumentCollectionUri(), entity);

                    if (result == null) throw new RepositoryException(RepositoryExceptionType.NullResult);

                    return result.Id;
                }
                catch (DocumentClientException ex)
                {
                    throw new RepositoryException(RepositoryExceptionType.ServerError, ex);
                }                
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var client = GetClient())
            {
                try
                {
                    await client.DeleteDocumentAsync(_config.GetDocumentUri(id));
                }
                catch (DocumentClientException ex)
                {
                    // If the exception is that the resource doesn't exist, that's fine as we want to delete it anyway.
                    if (ex.StatusCode != null && ex.StatusCode == HttpStatusCode.NotFound) return;

                    // Otherwise throw the exception.
                    throw new RepositoryException(RepositoryExceptionType.ServerError, ex);
                }
            }
        }

        public async Task<IEnumerable<D>> FindAsync(Expression<Func<D, bool>> condition)
        {
            using (var client = GetClient())
            {
                try
                {
                    var query = client.CreateDocumentQuery<D>(_config.GetDocumentCollectionUri()).Where(condition);

                    return await query.AsDocumentQuery().GetAllResultsAsync();
                }
                catch (DocumentClientException ex)
                {
                    throw new RepositoryException(RepositoryExceptionType.ServerError, ex);
                }                
            }
        }

        public async Task<D> FindByIdAsync(Guid id)
        {
            using (var client = GetClient())
            {
                try
                {
                    var query = client.CreateDocumentQuery<D>(_config.GetDocumentCollectionUri()).Where(c => c.Id == id).Take(1);

                    return await query.AsDocumentQuery().FirstOrDefault();
                }
                catch (DocumentClientException ex)
                {
                    throw new RepositoryException(RepositoryExceptionType.ServerError, ex);
                }
            }
        }

        public async Task<IEnumerable<D>> GetAllAsync(int page, int pageSize = 20)
        {
            using (var client = GetClient())
            {
                try
                {
                    var skipAmount = page - 1 * pageSize;
                    var query = client.CreateDocumentQuery<D>(_config.GetDocumentCollectionUri()).Skip(skipAmount).Take(pageSize);

                    return await query.AsDocumentQuery().GetAllResultsAsync();
                }
                catch (DocumentClientException ex)
                {
                    throw new RepositoryException(RepositoryExceptionType.ServerError, ex);
                }                
            }
        }

        public Task<D> UpdateAsync(D entity)
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException" />
        private DocumentClient GetClient()
        {
            if (_config == null) throw new InvalidOperationException("Null configuration provided.");

            return new DocumentClient(
                _config.Endpoint,
                _config.AuthKey,
                _serializerSettings,
                _connectionPolicy);
        }
        #endregion
    }
}
