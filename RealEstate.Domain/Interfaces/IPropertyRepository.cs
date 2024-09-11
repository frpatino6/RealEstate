using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task AddAsync(Property property);

        Task<Property> GetByIdAsync(Guid id);

        Task<IEnumerable<Property>> GetAllAsync();

        Task UpdateAsync(Property property);

        Task DeleteAsync(Guid id);
    }
}