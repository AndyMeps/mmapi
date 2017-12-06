using MMAPI.Common.Validator;
using MMAPI.Models;
using MMAPI.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Services
{
    public class FighterService : DocumentService<Fighter>, IFighterService
    {
        public FighterService() : base() { }

        public async Task<bool> ExistsAsync(Fighter fighter)
        {
            Expression<Func<Fighter, bool>> existingFighter = d =>
                d.FirstName == fighter.FirstName &&
                d.LastName == fighter.LastName &&
                d.DateOfBirth == fighter.DateOfBirth;

            return await ExistsAsync(existingFighter);
        }

        public async Task<Guid> ValidateAndCreateAsync(Fighter fighter)
        {
            if (fighter.IsValid()) return new Guid(await CreateAsync(fighter));

            throw new Exception(fighter.ValidationMessage());            
        }
    }
}
