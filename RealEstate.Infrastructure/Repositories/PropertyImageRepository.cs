using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly RealEstateDbContext _dbContext;

        public PropertyImageRepository(RealEstateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PropertyImage propertyImage)
        {
            await _dbContext.PropertyImages.AddAsync(propertyImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PropertyImage> GetByIdAsync(Guid id)
        {
            return await _dbContext.PropertyImages.FindAsync(id);
        }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(Guid propertyId)
        {
            return await _dbContext.PropertyImages
                .Where(img => img.IdProperty == propertyId)
                .ToListAsync();
        }

        public async Task UpdateAsync(PropertyImage propertyImage)
        {
            _dbContext.PropertyImages.Update(propertyImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var image = await GetByIdAsync(id);
            if (image != null)
            {
                _dbContext.PropertyImages.Remove(image);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
