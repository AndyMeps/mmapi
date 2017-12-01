using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Interfaces.Data
{
    public interface IDocumentRepository<T> where T : IDocumentEntity
    {
        Task<string> CreateDocumentAsync<TEntity>(TEntity entity) where TEntity : IDocumentEntity;

        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : IDocumentEntity;

        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : IDocumentEntity;

        Task<TEntity> FindById<TEntity>(Guid id) where TEntity : IDocumentEntity;

    }
}
