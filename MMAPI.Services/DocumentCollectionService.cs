using MMAPI.Interfaces.Data;
using MMAPI.Services.Factories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Services
{
    public class DocumentCollectionService<TDocumentEntity> : BaseService<TDocumentEntity>, IDocumentCollectionService<TDocumentEntity> where TDocumentEntity : IDocumentEntity
    {
        public DocumentCollectionService()
            : base(Environment.GetEnvironmentVariable("DocumentDbEndpoint"),
                   Environment.GetEnvironmentVariable("DocumentDbAuthKey"),
                   Environment.GetEnvironmentVariable("DocumentDbName"))
        {
        }

        public DocumentCollectionService(string uri, string authKey, string databaseName) : base(uri, authKey, databaseName) { }

        public async Task<string> CreateAsync(TDocumentEntity entity)
        {
            return await repository.CreateDocumentAsync(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TDocumentEntity, bool>> expression)
        {
            return await repository.ExistsAsync(expression);
        }

        public async Task<TDocumentEntity> FindByIdAsync(Guid id)
        {
            return await repository.FindById<TDocumentEntity>(id);
        }

        public string CollectionName
        {
            get
            {
                return CollectionPropertyFactory.GetCollectionName<TDocumentEntity>();
            }
        }
    }
}
