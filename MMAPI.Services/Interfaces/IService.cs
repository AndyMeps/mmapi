using MMAPI.Repository.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Services.Interfaces
{
    public interface IService<T> where T : IEntity
    {
        Task<string> CreateAsync(T entity);

        Task DeleteAsync(string id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        Task<T> FindByIdAsync(Guid id);

        string CollectionName { get; }
    }
}
