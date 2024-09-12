using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services
{
    public class MapperOwnerService : IMapperOwnerService
    {
        public OwnerDto MapToOwner(Owner owner)
        {
            return new OwnerDto(
                IdOwner: owner.IdOwner,
                Name: owner.Name,
                Address: owner.Address,
                Photo: owner.Photo,
                Birthday: owner.Birthday
            );
        }

        public IEnumerable<OwnerDto> MapToOwners(IEnumerable<Owner> owners)
        {
            return owners.Select(owner => new OwnerDto(
                IdOwner: owner.IdOwner,
                Name: owner.Name,
                Address: owner.Address,
                Photo: owner.Photo,
                Birthday: owner.Birthday
            ));
        }
    }
}
