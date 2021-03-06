﻿using MMAPI.Common.Exceptions;
using MMAPI.Common.Validator;
using MMAPI.Models;
using MMAPI.Repository.Interfaces;
using MMAPI.Services.Exceptions;
using MMAPI.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMAPI.Services
{
    public class FighterService : DocumentService<Fighter>, IFighterService
    {
        public FighterService() : base() { }

        public FighterService(IRepository<Fighter> repo) : base(repo) { }

        public async Task<bool> ExistsAsync(Fighter fighter)
        {
            if (fighter == null) throw new ArgumentNullException("fighter");

            Expression<Func<Fighter, bool>> existingFighter = d =>
                d.FirstName == fighter.FirstName &&
                d.LastName == fighter.LastName &&
                d.DateOfBirth == fighter.DateOfBirth;

            return await ExistsAsync(existingFighter);
        }

        public async Task<Guid> ValidateAndCreateAsync(Fighter fighter)
        {
            var validation = fighter.ValidationResult();
            if (validation.Success) return new Guid(await CreateAsync(fighter));

            throw new ValidationFailedException(validation.Message);            
        }
    }
}
