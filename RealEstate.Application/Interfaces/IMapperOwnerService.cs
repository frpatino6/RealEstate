using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Interfaces
{
    public interface IMapperOwnerService
    {
        OwnerDto MapToOwner(Owner owner);
        IEnumerable<OwnerDto> MapToOwners(IEnumerable<Owner> owners);
    }
}
