using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Queries
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, IEnumerable<OwnerDto>>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapperOwnerService _mapperOwnerService;

        public GetAllOwnersQueryHandler(IOwnerRepository ownerRepository, IMapperOwnerService mapperOwnerService)
        {
            _mapperOwnerService = mapperOwnerService;
            _ownerRepository = ownerRepository;
        }

        public async Task<IEnumerable<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _ownerRepository.GetAllAsync();

            return _mapperOwnerService.MapToOwners(owners);
        }
    }
}
