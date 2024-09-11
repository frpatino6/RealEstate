using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly RealEstateDbContext _dbContext;

        public PropertyTraceRepository(RealEstateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PropertyTrace propertyTrace)
        {
            await _dbContext.PropertyTraces.AddAsync(propertyTrace);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(Guid propertyId)
        {
            return await _dbContext.PropertyTraces
                .Where(trace => trace.IdProperty == propertyId)
                .ToListAsync();
        }

        public async Task<PropertyTrace> GetByIdAsync(Guid id)
        {
            return await _dbContext.PropertyTraces.FindAsync(id);
        }

        public async Task UpdateAsync(PropertyTrace propertyTrace)
        {
            _dbContext.PropertyTraces.Update(propertyTrace);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var trace = await GetByIdAsync(id);
            if (trace != null)
            {
                _dbContext.PropertyTraces.Remove(trace);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}