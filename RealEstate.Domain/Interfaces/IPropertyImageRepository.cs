using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task AddAsync(PropertyImage propertyImage);
        Task<PropertyImage> GetByIdAsync(Guid id);
        Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(Guid propertyId);
        Task UpdateAsync(PropertyImage propertyImage);
        Task DeleteAsync(Guid id);
    }
}
