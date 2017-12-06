using MMAPI.Models;
using System;
using System.Threading.Tasks;

namespace MMAPI.Services.Interfaces
{
    public interface IFighterService : IService<Fighter>
    {
        Task<bool> ExistsAsync(Fighter fighter);

        Task<Guid> ValidateAndCreateAsync(Fighter fighter);
    }
}
