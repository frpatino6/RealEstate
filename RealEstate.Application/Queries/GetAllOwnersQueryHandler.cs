using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Queries
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, IEnumerable<Owner>>
    {
        private readonly IOwnerRepository _ownerRepository;

        public GetAllOwnersQueryHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<IEnumerable<Owner>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            return await _ownerRepository.GetAllAsync();
        }
    }
}
