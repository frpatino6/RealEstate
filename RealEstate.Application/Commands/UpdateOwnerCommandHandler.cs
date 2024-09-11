using MediatR;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Commands
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private readonly IOwnerRepository _ownerRepository;

        public UpdateOwnerCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Unit> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.IdOwner);

            if (owner == null)
            {
                throw new Exception("Owner not found");
            }

            owner.Name = request.Name;
            owner.Address = request.Address;
            owner.Photo = request.Photo;
            owner.Birthday = request.Birthday;

            await _ownerRepository.UpdateAsync(owner);

            return Unit.Value;
        }
    }
}
