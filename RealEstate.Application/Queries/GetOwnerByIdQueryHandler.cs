using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Queries
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapperOwnerService _mapperOwnerService;

        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository,IMapperOwnerService mapperOwnerService)
        {
            _ownerRepository = ownerRepository;
            _mapperOwnerService = mapperOwnerService;
        }

        public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.IdOwner);

            return _mapperOwnerService.MapToOwner(owner);
        }
    }
}