using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDbContext _dbContext;

        public OwnerRepository(RealEstateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Owner owner)
        {
            await _dbContext.Owners.AddAsync(owner);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Owner> GetByIdAsync(Guid id)
        {
            return await _dbContext.Owners.FindAsync(id);
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _dbContext.Owners.ToListAsync();
        }

        public async Task UpdateAsync(Owner owner)
        {
            _dbContext.Owners.Update(owner);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var owner = await GetByIdAsync(id);
            if (owner != null)
            {
                _dbContext.Owners.Remove(owner);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}