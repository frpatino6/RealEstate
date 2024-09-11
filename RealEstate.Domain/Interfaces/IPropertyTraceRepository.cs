using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Repositories
{
    public interface IPropertyTraceRepository
    {
        Task AddAsync(PropertyTrace propertyTrace);

        Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(Guid propertyId);

        Task<PropertyTrace> GetByIdAsync(Guid id);

        Task UpdateAsync(PropertyTrace propertyTrace);

        Task DeleteAsync(Guid id);
    }
}