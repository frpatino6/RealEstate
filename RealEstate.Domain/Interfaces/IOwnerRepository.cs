using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task AddAsync(Owner owner);

        Task<Owner> GetByIdAsync(Guid id);

        Task<IEnumerable<Owner>> GetAllAsync();

        Task UpdateAsync(Owner owner);

        Task DeleteAsync(Guid id);
    }
}