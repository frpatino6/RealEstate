using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Commands
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Guid>
    {
        private readonly IOwnerRepository _ownerRepository;

        public CreateOwnerCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Guid> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = new Owner
            {
                IdOwner = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                Photo = request.Photo,
                Birthday = request.Birthday
            };

            await _ownerRepository.AddAsync(owner);

            return owner.IdOwner;
        }
    }
}