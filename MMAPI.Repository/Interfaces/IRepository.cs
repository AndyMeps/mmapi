using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<string> CreateAsync(TEntity entity);

        Task DeleteAsync(string id);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> condition);

        Task<TEntity> FindByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize = 1);
    }
}
