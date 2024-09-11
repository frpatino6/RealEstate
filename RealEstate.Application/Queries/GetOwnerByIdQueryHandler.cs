using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Queries
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Owner>
    {
        private readonly IOwnerRepository _ownerRepository;

        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _ownerRepository.GetByIdAsync(request.IdOwner);
        }
    }
}