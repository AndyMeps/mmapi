using MMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Services
{
    public class FighterService : DocumentCollectionService<Fighter>
    {
        public FighterService() : base() { }

        public async Task<bool> Exists(Fighter fighter)
        {
            Expression<Func<Fighter, bool>> existingFighter = d =>
                d.FirstName == fighter.FirstName &&
                d.LastName == fighter.LastName &&
                d.DateOfBirth == fighter.DateOfBirth;

            return await ExistsAsync(existingFighter);
        }
    }
}
